using UnityEngine;

/// <summary>
/// I turn a controlScalar input into a target position/velocity/acceleration
/// </summary>
public abstract class AxisMovement : KinematicLimits
{
    // WARNING - m_inputRange may be null in some derived classes
    protected InputRange m_inputRange;
    public virtual void ApplyControlScalar(float value)
    {
        m_inputRange.ControlScalar = value;
    }
    /// <summary>
    /// Returns the 'ImpulseMovement' class for this, if it is one.  ImpulseMovement is the base class for impulse axis movement types.
    /// </summary>
    /// <returns></returns>
    public virtual ImpulseMovement ImpulseType()
    {
        if (this is ImpulseMovement)
        {
            return (ImpulseMovement)this;
        }
        else
        {
            return null;
        }
    }
    public abstract KinematicVariables IndependentVariable { get; }
    public virtual float PositionTarget { get {Debug.LogError("Not an independent variable"); return float.NaN; } }
    public virtual float SpeedTarget { get {Debug.LogError("Not an independent variable"); return float.NaN; } }
    public virtual float AccelerationTarget { get {Debug.LogError("Not an independent variable"); return float.NaN; } }
    public virtual float ForceTarget { get {Debug.LogError("Not an independent variable"); return float.NaN; } }
    public virtual float JerkTarget { get {Debug.LogError("Not an independent variable"); return float.NaN; } }

    protected AxisMovement(KinematicLimits limits, InputRange inputRange)
        : base (limits)
    {
        m_inputRange = inputRange;
    }
}


public class UncontrolledAxisMovement : AxisMovement
{
    public override void ApplyControlScalar(float value) { /* Do nothing */ }
    public override KinematicVariables IndependentVariable { get => KinematicVariables.None; }
    public UncontrolledAxisMovement(KinematicLimits limits) : base(limits, null) {}
}


public class ControlledPositionAxisMovement : AxisMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Position; }
    public override float PositionTarget { get { return m_inputRange.InputValue; } }
    public ControlledPositionAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    {}
}


public class ControlledSpeedAxisMovement : AxisMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Speed; }
    public override float SpeedTarget { get { return m_inputRange.InputValue; } }
    public ControlledSpeedAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    {}
}


public class ControlledAccelerationAxisMovement : AxisMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Acceleration; }
    public override float AccelerationTarget { get { return m_inputRange.InputValue; } }
    public ControlledAccelerationAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    { }
}


public class ControlledForceAxisMovement : AxisMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Force; }
    public override float ForceTarget { get { return m_inputRange.InputValue; } }
    public ControlledForceAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    { }
}
