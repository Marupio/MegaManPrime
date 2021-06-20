using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// I take an n-dimensional control input (InputRange), and produce an n-dimensional target for a kinematic variable, such as position or velocity.
/// I can control any number of dimensions, I don't care which dimensions they are, nor do I care where they are in world space.  I don't even know
/// if I'm controlling linear movement or rotational movement.  All I know is the type of variable, e.g.:
///     * 'variable' - position or rotation angle
///     * 'firstDerivative' - velocity or angular velocity
/// The full list is: all KVariableEnum types, excluding KVariableTypeInfo.ExcludedFromControl.
/// These can be either linear or rotating type variables.
/// </summary>
public abstract class ControlField<T> : KVariableLimits {
    // WARNING - m_inputRange is null in some classes
    protected InputRange<T> m_inputRange;
    protected KVariableTypeSet m_controlledVariable;
    protected bool m_smoothingEnabled;
    protected float m_smoothingTime;

    /// <summary>
    /// Change the control value
    /// </summary>
    public virtual void ApplyControlValue(T value) {
        m_inputRange.ControlValue = value;
    }

    /// <summary>
    /// Returns the 'ImpulseMovement' class for this, if it is one.  ImpulseMovement is the base class for impulse axis movement types.
    /// </summary>
    /// <returns></returns>
    public virtual ImpulseControlField<T> ImpulseType() {
        if (this is ImpulseControlField<T>) {
            return (ImpulseControlField<T>)this;
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
    public abstract void Update(KVariables<T> vars);

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
        ImpulseControlField<T> impulseType = ImpulseType();
        if (impulseType != null && impulseType.Instantaneous)
        {
            return false;
        }
        return true;
    }

    // *** Constructors
    protected ControlField(KVariableLimits limits, InputRange<T> inputRange)
        : base (limits) {
        m_inputRange = inputRange;
        m_controlledVariable = KVariableTypeInfo.None;
    }
    protected ControlField(KVariableLimits limits, InputRange<T> inputRange, KVariableTypeSet controlledVariable)
        : base (limits) {
        m_inputRange = inputRange;

        if (controlledVariable.Contains(KVariableTypeInfo.ExcludedFromControl)) {
            Debug.LogError
            (
                "Attempting to make ControlField with KVariables that cannot be applied to control.\n" +
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


public class UncontrolledField<T> : ControlField<T>
{
    public override void ApplyControlValue(T value) { /* Do nothing */ }
    public override T Target => throw new System.NotImplementedException();
    public override void Update(KVariables<T> vars) { /* Do nothing */ }
    public UncontrolledField(KVariableLimits limits) : base(limits, null) {}
}


public class ContinuousControlField<T> : ControlField<T>
{
    public override T Target { get { return m_inputRange.InputValue; } }
    public override void Update(KVariables<T> vars) {
        
    }
    public ContinuousControlField(KVariableLimits limits, InputRange<T> inputRange, KVariableTypeSet controlledVariable)
        : base(limits, inputRange, controlledVariable) {}
}


public abstract class ImpulseControlField<T> : ControlField<T>
{
    // *** Protected fields
    protected float m_maxDuration = 0;
    protected float m_startTime = -1;
    protected bool m_enabled = true;
    protected bool m_activated = false;
    protected bool m_interruptable = false;

    // *** ControlField Interface
    public override T Target { get { return m_inputRange.InputValue; } }

    /// <summary>
    /// Set to true when Impulse is ready to use, false when it cannot be used
    /// </summary>
    public virtual bool Enabled { get => m_enabled; set => m_enabled = value; }
    /// <summary>
    /// When true, impulse movement is underway (enabled is now false)
    /// </summary>
    public virtual bool Activated { get {return m_activated; } }
    public virtual bool Instantaneous { get => m_maxDuration <= 0; }
    /// <summary>
    /// Can the impulse be controlled to stop early?
    /// </summary>
    /// <value></value>
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
    public ImpulseControlField(
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
// TODO - I don't think this pattern works - C# can't seem to figure out generics like this:
//      public class base<T> {}
//      public class derived : base<float> {}
//      public class usesBaseOnly {
//          public Update<T>(base<T> b) {
//              // Do something with base
//          }
//      }
//      main() {
//          derived d = new derived();
//          usesBaseOnly.Update(d);  // <----- Error here
//      }
//      // C# can't seem to use derived through a base<T> reference
//      // Further testing and I cannot duplicate the problem... it may work afterall...
// \TODO
//
// public class UncontrolledField1D : UncontrolledField<float> {
//     public UncontrolledField1D(KVariableLimits limits) : base(limits) {}
// }
// public class UncontrolledField2D : UncontrolledField<Vector2> {
//     public UncontrolledField2D(KVariableLimits limits) : base(limits) {}
// }
// public class UncontrolledField3D : UncontrolledField<Vector3> {
//     public UncontrolledField3D(KVariableLimits limits) : base(limits) {}
// }
// public class ContinuousControlField1D : ContinuousControlField<float> {
//     public ContinuousControlField1D(KVariableLimits limits, InputRange<float> inputRange, KVariableTypeSet controlledVariable)
//         : base(limits, inputRange, controlledVariable) {}
// }
// public class ContinuousControlField2D : ContinuousControlField<Vector2> {
//     public ContinuousControlField2D(KVariableLimits limits, InputRange<Vector2> inputRange, KVariableTypeSet controlledVariable)
//         : base(limits, inputRange, controlledVariable) {}
// }
// public class ContinuousControlField3D : ContinuousControlField<Vector3> {
//     public ContinuousControlField3D(KVariableLimits limits, InputRange<Vector3> inputRange, KVariableTypeSet controlledVariable)
//         : base(limits, inputRange, controlledVariable) {}
// }
// public class ImpulseControlField1D : ImpulseControlField<float> {
//     public ImpulseControlField1D(
//         KVariableLimits limits,
//         InputRange<float> inputRange,
//         KVariableTypeSet controlledVariable,
//         float maxDuration,
//         bool interruptable,
//         bool enabled = true
//     ) : base(limits, inputRange, controlledVariable, maxDuration, interruptable, enabled) {}
// }
// public class ImpulseControlField2D : ImpulseControlField<Vector2> {
//     public ImpulseControlField2D(
//         KVariableLimits limits,
//         InputRange<Vector2> inputRange,
//         KVariableTypeSet controlledVariable,
//         float maxDuration,
//         bool interruptable,
//         bool enabled = true
//     ) : base(limits, inputRange, controlledVariable, maxDuration, interruptable, enabled) {}
// }
// public class ImpulseControlField3D : ImpulseControlField<Vector3> {
//     public ImpulseControlField3D(
//         KVariableLimits limits,
//         InputRange<Vector3> inputRange,
//         KVariableTypeSet controlledVariable,
//         float maxDuration,
//         bool interruptable,
//         bool enabled = true
//     ) : base(limits, inputRange, controlledVariable, maxDuration, interruptable,enabled) {}
// }
