using UnityEngine;
using System.Collections.Generic;

public interface IControlFieldToolset<T> {
    public T Zero {get;}
    public T SmoothDamp(T current, T target, ref T currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
}

public class ControlFieldToolsetFloat : IControlFieldToolset<float> {
    public float Zero { get=>0f; }
    public float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
        return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
    }
}

public class ControlFieldToolsetVector2 : IControlFieldToolset<Vector2> {
    public Vector2 Zero { get=>Vector2.zero; }
    public Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
        return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
    }
}

public class ControlFieldToolsetVector3 : IControlFieldToolset<Vector3> {
    public Vector3 Zero { get=>Vector3.zero; }
    public Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
        return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
    }
}

/// <summary>
/// I take an n-dimensional control input (InputRange), and produce an n-dimensional target for a kinematic variable, such as position or velocity.
/// I can control any number of dimensions, I don't care which dimensions they are, nor do I care where they are in world space.  I don't even know
/// if I'm controlling linear movement or rotational movement.  All I know is the type of variable, e.g.:
///     * 'variable' - position or rotation angle,
///     * 'firstDerivative' - velocity or angular velocity,
/// and so on. The full list is: all KVariableControllableEnum types. These can be either linear or rotating type variables.
/// </summary>
public abstract class ControlField<T> {
    protected IControlFieldToolset<T> m_toolset;
    protected string m_name;
    protected InputRange<T> m_inputRange; // null is okay!
    protected KVariableEnum_Controllable m_controlledVariable;
    private KVariableTypeSet m_controlledVariableAsTypeSet;

    // TODO add controlledVariableIntegralMax
    protected float m_controlledVariableMax = float.PositiveInfinity;
    protected float m_controlledVariableDerivativeMax = float.PositiveInfinity; // Only needed when smoothing enabled

    protected bool m_smoothingEnabled;
    protected float m_smoothingTime;

    /// <summary>
    /// Access to toolset with <T>-specific functionality
    /// </summary>
    public IControlFieldToolset<T> Toolset {get=>m_toolset; set=>m_toolset=value;}

    /// <summary>
    /// Access the name of this controlField
    /// </summary>
    public string Name { get=>m_name; set=>m_name=value; }

    /// <summary>
    /// Change the control value - the main controlling functionality
    /// </summary>
    public virtual void ApplyControlValue(T value) {
        m_inputRange.ControlValue = value;
    }

    /// <summary>
    /// Controlled variable type, as a TypeSet (Single-restricted)
    /// </summary>
    public KVariableTypeSet ControlledVariable { get => m_controlledVariableAsTypeSet; }
    /// <summary>
    /// Controlled variable type, as an enum (subset that includes only controllable variables)
    /// </summary>
    /// <value></value>
    public KVariableEnum_Controllable ControlledVariableEnum { get => m_controlledVariable; }

    /// <summary>
    /// True when motion smoothing is enabled (uses SmoothDamp functionality instead of directly setting the variable)
    /// Not allowed for ImpulseTypes with instantaneous effect.
    /// </summary>
    public bool SmoothingEnabled
    {
        get => m_smoothingEnabled;
        set { m_smoothingEnabled = InternalSmoothingAllowed() ? value : false; }
    }

    /// <summary>
    /// Smoothing time - larger increases smoothness (and sluggishness)
    /// </summary>
    public float SmoothingTime
    {
        get => m_smoothingTime;
        set { m_smoothingTime = InternalSmoothingAllowed() ? value : 0;}
    }
    /// <summary>
    /// Controlled variable specific limits
    /// </summary>
    public float OutputVariableMax { get => m_controlledVariableMax; set => m_controlledVariableMax = value; }
    /// <summary>
    /// Limit applied to derivative of controlled variable when smoothing.
    /// Tight restriction here gives an 'ice level' type effect.
    /// </summary>
    /// <value></value>
    public float OutputDerivativeMax { get => m_controlledVariableDerivativeMax; set => m_controlledVariableDerivativeMax = value; }

    /// <summary>
    /// Returns the 'ImpulseMovement' class for this, if it is one.  ImpulseMovement is the base class for impulse axis movement types.
    /// </summary>
    /// <returns></returns>
    public ImpulseControlField<T> ImpulseType() {
        if (this is ImpulseControlField<T>) {
            return (ImpulseControlField<T>)this;
        } else {
            return null;
        }
    }

    /// <summary>
    /// Output of this class - this is the value I want for my controlled variables
    /// </summary>
    /// <value></value>
    public abstract T Target { get; }
    // /// <summary>
    // /// Perform kinematic calculations on provided variables
    // /// </summary>
    public abstract void Update(ref T cVar, ref T cVarDeriv, float deltaTime);

    // *** Special properties
    /// <summary>
    /// Returns how this controller interacts with a RigidBody|2D - either by applying force, or by setting kinematic variables
    /// </summary>
    public RigidBodyActorType ActorType {
        get {
            if (KVariableTypeInfo.AllRigidBodyVarSetters.Contains(m_controlledVariable)) {
                return RigidBodyActorType.StateSetter;
            } else if (KVariableTypeInfo.AllRigidBodyVarAdders.Contains(m_controlledVariable)) {
                return RigidBodyActorType.ForceUser;
            }
            return RigidBodyActorType.None;
        }
    }
    public bool ForceUser() {
        return ControlledVariable.ForceUser();
    }
    public bool StateSetter() {
        return ControlledVariable.StateSetter();
    }

    // *** Internal functions
    /// <summary>
    /// Enforces no smoothing for ImpulseForce variables, nor for Instantaneous movement control
    /// </summary>
    protected bool InternalSmoothingAllowed()
    {
        ImpulseControlField<T> impulseType = ImpulseType();
        if (m_controlledVariable == KVariableEnum_Controllable.ImpulseForce || impulseType != null && impulseType.Instantaneous) {
            return false;
        }
        return true;
    }

    protected void InitControlledVariableTypeSet() {
        m_controlledVariableAsTypeSet = new KVariableTypeSet(m_controlledVariable);
        m_controlledVariableAsTypeSet.SetRestrictionToControllable();
        m_controlledVariableAsTypeSet.SetRestrictionToSingle();
    }

    // *** Constructors
    protected ControlField(
        IControlFieldToolset<T> toolset,
        string name,
        InputRange<T> inputRange,
        float smoothingTime,
        float controlledVariableMax = float.PositiveInfinity,
        float controlledVariableDerivativeMax = float.PositiveInfinity
    ) {
        m_toolset = toolset;
        m_inputRange = inputRange;
        m_smoothingTime = smoothingTime;
        m_smoothingEnabled = m_smoothingTime > 0;
        m_name = name;
        m_controlledVariable = KVariableTypeInfo.None;
        m_controlledVariableMax = controlledVariableMax;
        m_controlledVariableDerivativeMax = controlledVariableDerivativeMax;
        InitControlledVariableTypeSet();
    }
    protected ControlField(
        IControlFieldToolset<T> toolset,
        string name,
        InputRange<T> inputRange,
        KVariableTypeSet controlledVariable,
        float smoothingTime,
        float controlledVariableMax = float.PositiveInfinity,
        float controlledVariableDerivativeMax = float.PositiveInfinity
    ) {
        m_toolset = toolset;
        m_name = name;
        m_inputRange = inputRange;
        m_smoothingTime = smoothingTime;
        m_smoothingEnabled = m_smoothingTime > 0;

        if (controlledVariable.IsMultiple()) {
            Debug.LogError("ControlField cannot contain more than one controlled variable, setting to None.");
            m_controlledVariable = KVariableEnum_Controllable.None;
        } else if (controlledVariable.ContainsAny(KVariableTypeInfo.AllNonControllableTypes)) {
            Debug.LogError
            (
                "Attempting to make ControlField with KVariables that cannot be applied to control.\n" +
                "Requesting:\n" +
                "\t" + controlledVariable + "\n" +
                "Cannot contain:\n" +
                "\t" + KVariableTypeInfo.AllNonControllableTypes + "\n" +
                "Setting to none."
            );
            m_controlledVariable = KVariableEnum_Controllable.None;
        } else {
            m_controlledVariable = controlledVariable;
        }
        m_controlledVariableMax = controlledVariableMax;
        m_controlledVariableDerivativeMax = controlledVariableDerivativeMax;
        InitControlledVariableTypeSet();
    }
}


public class UncontrolledField<T> : ControlField<T>
{
    public override void ApplyControlValue(T value) { /* Do nothing */ }
    public override T Target => throw new System.NotImplementedException();
    public override void Update(ref T cVar, ref T cVarDeriv, float deltaTime) { /* Do nothing */ }
    public UncontrolledField(
        IControlFieldToolset<T> toolset,
        string name,
        float controlledVariableMax = float.PositiveInfinity,
        float controlledVariableDerivativeMax = float.PositiveInfinity
    ) : base(toolset, name, null, 0f, controlledVariableMax, controlledVariableDerivativeMax) {}
}


public class ContinuousControlField<T> : ControlField<T>
{
    public override T Target { get { return m_inputRange.InputValue; } }
    public override void Update(ref T cVar, ref T cVarDeriv, float deltaTime) {
        T target = Target;
        if (m_smoothingEnabled && m_smoothingTime > 0) {
            // Use smoothing algorithm
            cVar = m_toolset.SmoothDamp(
                cVar,
                target,
                ref cVarDeriv,
                m_smoothingTime,
                m_controlledVariableDerivativeMax,
                deltaTime
            );
        } else {
            cVar = Target;
        }
    }
    public ContinuousControlField(
        IControlFieldToolset<T> toolset,
        InputRange<T> inputRange,
        string name,
        KVariableTypeSet controlledVariable,
        float smoothingTime,
        float controlledVariableMax = float.PositiveInfinity,
        float controlledVariableDerivativeMax = float.PositiveInfinity
    ) : base(toolset, name, inputRange, controlledVariable, smoothingTime, controlledVariableMax, controlledVariableDerivativeMax) {}
}


public class ImpulseControlField<T> : ControlField<T>
{
    // *** Protected fields
    protected float m_maxDuration = 0;
    protected float m_startTime = -1;
    protected bool m_enabled = true;
    protected bool m_activated = false;
    protected bool m_interruptable = false;

    // *** ControlField Interface
    public override T Target { get { return InternalGetInput(); } }
    public override void Update(ref T cVar, ref T cVarDeriv, float deltaTime) {
        T target = Target;
        if (m_smoothingEnabled && m_smoothingTime > 0) {
            // Use smoothing algorithm
            cVar = m_toolset.SmoothDamp(
                cVar,
                target,
                ref cVarDeriv,
                m_smoothingTime,
                m_controlledVariableDerivativeMax,
                deltaTime
            );
        } else {
            cVar = Target;
        }
    }
    
    /// <summary>
    /// Set to true when Impulse is ready to use, false when it cannot be used
    /// </summary>
    public bool Enabled { get => m_enabled; set => m_enabled = value; }
    /// <summary>
    /// When true, impulse movement is underway (enabled is now false)
    /// </summary>
    public bool Activated { get {return m_activated; } }
    public bool Instantaneous { get => m_maxDuration <= 0; }
    /// <summary>
    /// Can the impulse be controlled to stop early?
    /// </summary>
    /// <value></value>
    public bool Interruptable { get => m_interruptable; }
    public float StartTime { get => m_startTime; }
    public float MaxDuration { get => m_maxDuration; }
    public float MaxRemaining
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
        IControlFieldToolset<T> toolset,
        string name,
        InputRange<T> inputRange,
        KVariableTypeSet controlledVariable,
        float smoothingTime,
        float maxDuration,
        bool interruptable,
        bool enabled = true,
        float controlledVariableMax = float.PositiveInfinity,
        float controlledVariableDerivativeMax = float.PositiveInfinity
    ) : base(toolset, name, inputRange, controlledVariable, smoothingTime, controlledVariableMax, controlledVariableDerivativeMax) {
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
    protected bool Activate() {
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
    protected bool Deactivate() {
        if (!m_activated) {
            return false;
        }
        m_activated = false;
        m_startTime = 0;
        return true;
    }
    protected T InternalGetInput() {
        if (Enabled) {
            // Ready to fire, check for max historical input
            T newValue = m_inputRange.UnqueriedMaxMagnitudeInputValue;
            if (Equals(newValue, m_toolset.Zero)) {
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
                return (m_toolset.Zero);
            }
            T newValue = m_inputRange.UnqueriedMaxMagnitudeInputValue;
            m_inputRange.ClearStatistics(m_inputRange.ControlValue);
            return newValue;
        } else {
            return (m_toolset.Zero);
        }
    }
}


// *** Concrete classes
// TODO - I don't think this pattern works - C# can't seem to figure out generics like this:
//      public class base<T> {}
//      public class derived : base<float> {}
//      public class usesBaseOnly {
//          public Update<T>(ref base<T> b) {
//              // Do something with base
//          }
//      }
//      main() {
//          derived d = new derived();
//          usesBaseOnly.Update(ref d);  // <----- Error here
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
