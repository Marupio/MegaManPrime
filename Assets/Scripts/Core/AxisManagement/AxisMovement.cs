using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// I turn a controlValue input into a target position/velocity/acceleration
/// </summary>
public abstract class AxisMovement<T> : KVariableLimits {
    // WARNING - m_inputRange is null in some classes
    protected InputRange<T> m_inputRange;
    KVariableTypeSet m_controlledVariable;
    Dictionary<string, AxisSource> m_axisSources;
    bool m_smoothingEnabled;
    float m_smoothingTime;

    /// <summary>
    /// Change the control value
    /// </summary>
    public virtual void ApplyControlValue(T value) {
        m_inputRange.ControlValue = value;
    }

    public AxisSource GetSource(string name) { return m_axisSources[name]; }
    /// <summary>
    /// Add a new axis source, by name
    /// </summary>
    /// <returns>True if source already existed and was overwritten</returns>
    public bool AddSource(string name, AxisSource newSource) {
        bool overwritten = false;
        if (m_axisSources.ContainsKey(name)) {
            overwritten = true;
        }
        m_axisSources.Add(name, newSource);
        return overwritten;
    }
    /// <summary>
    /// Remove an axis source, by name
    /// </summary>
    /// <returns>True if the source existed and was removed</returns>
    public bool RemoveSource(string name) {
        if (m_axisSources.ContainsKey(name)) {
            m_axisSources.Remove(name);
            return true;
        }
        return false;
    }
    public int RemoveAllSources() {
        int nRemoved = m_axisSources.Count;
        m_axisSources.Clear();
        return nRemoved;
    }

    /// <summary>
    /// Returns the 'ImpulseMovement' class for this, if it is one.  ImpulseMovement is the base class for impulse axis movement types.
    /// </summary>
    /// <returns></returns>
    public virtual ImpulseAxisMovement<T> ImpulseType() {
        if (this is ImpulseAxisMovement<T>) {
            return (ImpulseAxisMovement<T>)this;
        } else {
            return null;
        }
    }
    public virtual KVariableTypeSet ControlledVariable { get => m_controlledVariable; }
    /// <summary>
    /// Output of this class - this is the value I want for my controlled variables
    /// </summary>
    /// <value></value>
    public abstract T Target { get; }
    // /// <summary>
    // /// Perform kinematic calculations on provided variables
    // /// </summary>
    // public abstract void Update
    // (
    //     ref T position,
    //     ref T velocity,
    //     ref T acceleration,
    //     ref T appliedForce,
    //     ref T instantaneousForce,
    //     ref KVariableTypeSet involvedVariables,
    //     Dictionary<string, AxisSource> sources
    // );

    // *** Special properties
    /// <summary>
    /// Returns how this controller interacts with a RigidBody|2D - either by applying force, or by setting kinematic variables
    /// </summary>
    public RigidBodyActorType ActorType {
        get {
            bool forceUser = KVariableTypeInfo.AllForceTypes.Contains(ControlledVariable);
            bool stateSetter = KVariableTypeInfo.AllStateSetterTypes.Contains(ControlledVariable);
            if (forceUser) {
                if (stateSetter) {
                    return RigidBodyActorType.Both;
                } else {
                    return RigidBodyActorType.ForceUser;
                }
            } else if (stateSetter) {
                return RigidBodyActorType.StateSetter;
            } else {
                return RigidBodyActorType.None;
            }
        }
    }
    public bool ForceUser() {
        return ControlledVariable.ForceUser();
    }
    public bool StateSetter() {
        return ControlledVariable.StateSetter();
    }
    public bool SmoothingEnabled
    {
        get => m_smoothingEnabled;
        set { m_smoothingEnabled = InternalSmoothingAllowed() ? value : false; }
    }
    public float SmoothingTime
    {
        get => m_smoothingTime;
        set { m_smoothingTime = InternalSmoothingAllowed() ? value : 0;}
    }

    // *** Internal functions
    /// <summary>
    /// Enforces no smoothing for Instantaneous movement control
    /// </summary>
    protected bool InternalSmoothingAllowed()
    {
        ImpulseAxisMovement<T> impulseType = ImpulseType();
        if (impulseType != null && impulseType.Instantaneous)
        {
            return false;
        }
        return true;
    }

    // *** Constructors
    protected AxisMovement(KVariableLimits limits, InputRange<T> inputRange)
        : base (limits) {
        m_inputRange = inputRange;
        m_controlledVariable = KVariableTypeInfo.None;
    }
    protected AxisMovement(KVariableLimits limits, InputRange<T> inputRange, KVariableTypeSet controlledVariable)
        : base (limits) {
        m_inputRange = inputRange;

        if (controlledVariable.Contains(KVariableTypeInfo.ExcludedFromControl)) {
            Debug.LogError
            (
                "Attempting to make axis with Kinematic Variables that cannot be applied to control.\n" +
                "Requesting:\n" +
                "\t" + controlledVariable + "\n" +
                "Cannot contain:\n" +
                "\t" + KVariableTypeInfo.ExcludedFromControl + "\n" +
                "Removing disallowed variable flags."
            );
            m_controlledVariable = controlledVariable & ~KVariableTypeInfo.ExcludedFromControl;
        } else {
            m_controlledVariable = controlledVariable;
        }
    }
}


public class UncontrolledAxisMovement<T> : AxisMovement<T>
{
    public override void ApplyControlValue(T value) { /* Do nothing */ }
    public override T Target => throw new System.NotImplementedException();
    public UncontrolledAxisMovement(KVariableLimits limits) : base(limits, null) {}
}


public class ControlledAxisMovement<T> : AxisMovement<T>
{
    public override T Target { get { return m_inputRange.InputValue; } }
    public ControlledAxisMovement(KVariableLimits limits, InputRange<T> inputRange, KVariableTypeSet controlledVariable)
        : base(limits, inputRange, controlledVariable) {}
}


public abstract class ImpulseAxisMovement<T> : AxisMovement<T>
{
    // *** Protected fields
    protected float m_maxDuration = 0;
    protected float m_startTime = -1;
    protected bool m_enabled = true;
    protected bool m_activated = false;
    protected bool m_interruptable = false;

    // *** AxisMovement interface
    public override T Target { get { return m_inputRange.InputValue; } }

    /// <summary>
    /// Set to true when Impulse is ready to use, false when it cannot be used
    /// </summary>
    public virtual bool Enabled { get => m_enabled; set => m_enabled = value; }
    /// <summary>
    /// When true, impulse movement is underway (enabled is now false)
    /// </summary>
    public virtual bool Activated { get {return m_activated; } }
    /// <summary>
    /// Can the impulse be controlled to stop early?
    /// </summary>
    /// <value></value>
    public virtual bool Instantaneous { get => m_maxDuration <= 0; }
    public virtual bool Interruptable { get => m_interruptable; }
    public virtual float StartTime { get => m_startTime; }
    public virtual float MaxDuration { get => m_maxDuration; }
    public virtual float MaxRemaining
    {
        get
        {
            if (Instantaneous || m_startTime < 0)
            {
                return 0;
            }
            return MaxDuration - (Time.time - m_startTime);
        }
    }

    // *** Constructors
    public ImpulseAxisMovement(
        KVariableLimits limits,
        InputRange<T> inputRange,
        KVariableTypeSet controlledVariable,
        float maxDuration,
        bool interruptable,
        bool enabled = true
    ) : base(limits, inputRange, controlledVariable) {
        m_maxDuration = maxDuration;
        m_interruptable = interruptable;
        m_enabled = enabled;
    }


    // *** Protected member functions

    /// <summary>
    /// Returns true if valA equals valB. Work-around for C# generic class limitations.
    /// </summary>
    public static bool Equals(T param1, T param2) {
        return EqualityComparer<T>.Default.Equals(param1, param2);
    }

    /// <summary>
    /// Performs bookkeeping actions to activate this impulse.  Can be used when already active - resets the start timer.
    /// </summary>
    /// <returns>true if activated successfully, false if it wasn't enabled</returns>
    protected virtual bool Activate() {
        if (!m_enabled) {
            return false;
        }
        m_enabled = false;
        m_startTime = Time.time;
        m_activated = true;
        return true;
    }
    /// <summary>
    /// Performs bookkeeping actions to deactivate this impulse
    /// </summary>
    /// <returns>true if successfully deactived, false if it wasn't active</returns>
    protected virtual bool Deactivate() {
        if (!m_activated) {
            return false;
        }
        m_activated = false;
        m_startTime = 0;
        return true;
    }
    protected virtual T InternalGetInput() {
        if (Enabled) {
            // Ready to fire, check for max historical input
            T newValue = m_inputRange.UnqueriedMaxMagnitudeInputValue;
            if (Equals(newValue, (new Traits<T>()).Zero)) {
                Activate();
                m_inputRange.ClearStatistics(m_inputRange.ControlValue);
            }
            return newValue;
        }
        else if (Activated) {
            if (
                (Interruptable && m_inputRange.UnqueriedZeroInputValue) ||  // Control shut it off
                (Instantaneous || Time.time - m_startTime > m_maxDuration)          // Timed out
            ) {
                Deactivate();
                return (new Traits<T>()).Zero;
            }
            T newValue = m_inputRange.UnqueriedMaxMagnitudeInputValue;
            m_inputRange.ClearStatistics(m_inputRange.ControlValue);
            return newValue;
        } else {
            return (new Traits<T>()).Zero;
        }
    }
}

// *** Concrete classes
public class UncontrolledAxisMovement1D : UncontrolledAxisMovement<float> {
    public UncontrolledAxisMovement1D(KVariableLimits limits) : base(limits) {}
}
public class UncontrolledAxisMovement2D : UncontrolledAxisMovement<Vector2> {
    public UncontrolledAxisMovement2D(KVariableLimits limits) : base(limits) {}
}
public class UncontrolledAxisMovement3D : UncontrolledAxisMovement<Vector3> {
    public UncontrolledAxisMovement3D(KVariableLimits limits) : base(limits) {}
}
public class ControlledAxisMovement1D : ControlledAxisMovement<float> {
    public ControlledAxisMovement1D(KVariableLimits limits, InputRange<float> inputRange, KVariableTypeSet controlledVariable)
        : base(limits, inputRange, controlledVariable) {}
}
public class ControlledAxisMovement2D : ControlledAxisMovement<Vector2> {
    public ControlledAxisMovement2D(KVariableLimits limits, InputRange<Vector2> inputRange, KVariableTypeSet controlledVariable)
        : base(limits, inputRange, controlledVariable) {}
}
public class ControlledAxisMovement3D : ControlledAxisMovement<Vector3> {
    public ControlledAxisMovement3D(KVariableLimits limits, InputRange<Vector3> inputRange, KVariableTypeSet controlledVariable)
        : base(limits, inputRange, controlledVariable) {}
}
public class ImpulseAxisMovement1D : ImpulseAxisMovement<float> {
    public ImpulseAxisMovement1D(
        KVariableLimits limits,
        InputRange<float> inputRange,
        KVariableTypeSet controlledVariable,
        float maxDuration,
        bool interruptable,
        bool enabled = true
    ) : base(limits, inputRange, controlledVariable, maxDuration, interruptable, enabled) {}
}
public class ImpulseAxisMovement2D : ImpulseAxisMovement<Vector2> {
    public ImpulseAxisMovement2D(
        KVariableLimits limits,
        InputRange<Vector2> inputRange,
        KVariableTypeSet controlledVariable,
        float maxDuration,
        bool interruptable,
        bool enabled = true
    ) : base(limits, inputRange, controlledVariable, maxDuration, interruptable, enabled) {}
}
public class ImpulseAxisMovement3D : ImpulseAxisMovement<Vector3> {
    public ImpulseAxisMovement3D(
        KVariableLimits limits,
        InputRange<Vector3> inputRange,
        KVariableTypeSet controlledVariable,
        float maxDuration,
        bool interruptable,
        bool enabled = true
    ) : base(limits, inputRange, controlledVariable, maxDuration, interruptable,enabled) {}
}
