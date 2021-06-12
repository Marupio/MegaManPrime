using UnityEngine;

/// <summary>
/// I turn a controlValue input into a target position/velocity/acceleration
/// </summary>
public abstract class AxisMovement<T> : KinematicLimits
{
    // WARNING - m_inputRange may be null in some derived classes
    protected InputRange<T> m_inputRange;
    public virtual void ApplyControlValue(T value)
    {
        m_inputRange.ControlValue = value;
    }
    /// <summary>
    /// Returns the 'ImpulseMovement' class for this, if it is one.  ImpulseMovement is the base class for impulse axis movement types.
    /// </summary>
    /// <returns></returns>
    public virtual ImpulseMovement<T> ImpulseType()
    {
        if (this is ImpulseMovement<T>)
        {
            return (ImpulseMovement<T>)this;
        }
        else
        {
            return null;
        }
    }
    public abstract KinematicVariables IndependentVariable { get; }
    public abstract T Target { get; }

    protected AxisMovement(KinematicLimits limits, InputRange<T> inputRange)
        : base (limits)
    {
        m_inputRange = inputRange;
    }
}


public class UncontrolledAxisMovement<T> : AxisMovement<T>
{
    public override void ApplyControlValue(T value) { /* Do nothing */ }
    public override KinematicVariables IndependentVariable { get => KinematicVariables.None; }
    public override T Target => throw new System.NotImplementedException();
    public UncontrolledAxisMovement(KinematicLimits limits) : base(limits, null) {}
}


public class ControlledAxisMovement<T> : AxisMovement<T>
{
    KinematicVariables m_kinematicVariable;
    public override KinematicVariables IndependentVariable { get => m_kinematicVariable; }
    public override T Target { get { return m_inputRange.InputValue; } }
    public ControlledAxisMovement(KinematicLimits limits, InputRange<T> inputRange, KinematicVariables kinematicVariable)
        : base(limits, inputRange)
    {
        m_kinematicVariable = kinematicVariable;
    }
}
