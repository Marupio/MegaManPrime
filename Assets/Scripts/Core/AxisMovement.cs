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
    public abstract KinematicVariable IndependentVariable { get; }
    public virtual float PositionTarget() { Debug.LogError("Not an independent variable"); return float.NaN; }
    public virtual float SpeedTarget() { Debug.LogError("Not an independent variable"); return float.NaN; }
    public virtual float AccelerationTarget() { Debug.LogError("Not an independent variable"); return float.NaN; }
    public virtual float JerkTarget() { Debug.LogError("Not an independent variable"); return float.NaN; }

    protected AxisMovement(KinematicLimits limits, InputRange inputRange)
        : base (limits)
    {
        m_inputRange = inputRange;
    }
}


/*
    AxisMovement (required variables / settings)
        Uncontrolled
        ControlledPosition
        ControlledSpeed
            Speed (inputRange)
            AccelerationMax
            Smoothing
        ControlledAcceleration
            Acceleration (inputRange)
            SpeedMax
            JerkSmoothing
            TaperOffSmoothing
        ControlledForce
            Force (inputRange)
            SpeedMax
            JerkSmoothing
            TaperOffSmoothing
        ImpulseInitialSpeed
            InitialSpeed (inputRange)
        ImpulseSpeedAndDuration
            Speed
            Duration (inputRange)
        ImpulseAccelerationAndDuration
        ImpulseForceAndDuration
*/
public class UncontrolledAxisMovement : AxisMovement
{
    public override void ApplyControlScalar(float value) { /* Do nothing */ }
    public override KinematicVariable IndependentVariable { get => KinematicVariable.None; }
    public UncontrolledAxisMovement(KinematicLimits limits) : base(limits, null) {}
}

public class ControlledPositionAxisMovement : AxisMovement
{
    public override KinematicVariable IndependentVariable { get => KinematicVariable.Position; }
    public override float PositionTarget()
    {
        return m_inputRange.InputValue;
    }
    public ControlledPositionAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    {}
}
public class ControlledSpeedAxisMovement : AxisMovement
{
    public override KinematicVariable IndependentVariable { get => KinematicVariable.Speed; }
    public override float SpeedTarget()
    {
        return m_inputRange.InputValue;
    }
    public ControlledSpeedAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    {}
}
public class ControlledAccelerationAxisMovement : AxisMovement
{
    public override KinematicVariable IndependentVariable { get => KinematicVariable.Acceleration; }
    public override float AccelerationTarget()
    {
        return m_inputRange.InputValue;
    }
    public ControlledAccelerationAxisMovement(KinematicLimits limits, InputRange inputRange)
        : base(limits, inputRange)
    { }
}

//         ControlledForce
//             Force (inputRange)
//             SpeedMax
//             JerkSmoothing
//             TaperOffSmoothing
//         ImpulseInitialSpeed
//             InitialSpeed (inputRange)
//         ImpulseSpeedAndDuration
//             Speed
//             Duration (inputRange)
//         ImpulseAccelerationAndDuration
//         ImpulseForceAndDuration
