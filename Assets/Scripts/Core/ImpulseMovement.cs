using UnityEngine;

/// <summary>
/// A type of movement that needs to be enabled before using, and once firing is complete, automatically disables itself again
/// Useful for things like jumping
/// </summary>
public abstract class ImpulseMovement<T> : AxisMovement<T>
{
    // *** Protected member variables

    protected float m_maxDuration = 0;
    protected float m_startTime = -1;
    protected bool m_enabled = true;
    protected bool m_activated = false;
    protected bool m_interruptable = false;

    /// <summary>
    /// Set to true when ImpulseMovement is ready to use, false when it cannot be used
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

    public ImpulseMovement(KinematicLimits limits, InputRange<T> inputRange, float maxDuration, bool interruptable, bool enabled = true)
        : base(limits, inputRange)
    {
        m_maxDuration = maxDuration;
        m_interruptable = interruptable;
        m_enabled = enabled;
    }


    // *** Protected member functions

    /// <summary>
    /// Performs bookkeeping actions to activate this impulse.  Can be used when already active - resets the start timer.
    /// </summary>
    /// <returns>true if activated successfully, false if it wasn't enabled</returns>
    protected virtual bool Activate()
    {
        if (!m_enabled)
        {
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
    protected virtual bool Deactivate()
    {
        if (!m_activated)
        {
            return false;
        }
        m_activated = false;
        m_startTime = 0;
        return true;
    }
    protected virtual T InternalGetInput()
    {
        if (Enabled)
        {
            // Ready to fire, check for max historical input
            T newValue = m_inputRange.UnqueriedMaxMagnitudeInputValue;
            if (newValue != 0)
            {
                Activate();
                m_inputRange.ClearStatistics();
            }
            return newValue;
        }
        else if (Activated)
        {
            if
            (
                (Interruptable && m_inputRange.UnqueriedZeroCrossingInputValue) ||  // Control shut it off
                (Instantaneous || Time.time - m_startTime > m_maxDuration)          // Timed out
            )
            {
                Deactivate();
                return 0;
            }
            float newValue = m_inputRange.UnqueriedMaxMagnitudeInputValue;
            m_inputRange.ClearStatistics();
            return newValue;
        }
        else
        {
            return 0;
        }
    }
}


public class PositionImpulseMovement : ImpulseMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Position; }
    public override float ValueTarget { get => InternalGetInput(); }
    public PositionImpulseMovement(KinematicLimits limits, InputRange inputRange, float maxDuration, bool interruptable, bool enabled = true)
        : base(limits, inputRange, maxDuration, interruptable, enabled)
    { }
}


public class SpeedImpulseMovement : ImpulseMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Speed; }
    public override float DerivativeTarget { get => InternalGetInput(); }
    public SpeedImpulseMovement(KinematicLimits limits, InputRange inputRange, float maxDuration, bool interruptable, bool enabled = true)
        : base(limits, inputRange, maxDuration, interruptable, enabled)
    { }
}


public class AccelerationImpulseMovement : ImpulseMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Acceleration; }
    public override float SecondDerivativeTarget { get => InternalGetInput(); }
    public AccelerationImpulseMovement(KinematicLimits limits, InputRange inputRange, float maxDuration, bool interruptable, bool enabled = true)
        : base(limits, inputRange, maxDuration, interruptable, enabled)
    { }
}


public class ForceImpulseMovement : ImpulseMovement
{
    public override KinematicVariables IndependentVariable { get => KinematicVariables.Force; }
    public override float ForceTarget { get => InternalGetInput(); }
    public ForceImpulseMovement(KinematicLimits limits, InputRange inputRange, float maxDuration, bool interruptable, bool enabled = true)
        : base(limits, inputRange, maxDuration, interruptable, enabled)
    { }
}
