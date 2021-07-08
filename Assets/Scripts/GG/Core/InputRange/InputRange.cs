using UnityEngine;

/// <summary>
/// InputRange base class, expects ControlScalar between -1 and 1, retuns InputValue according to prescribed behaviour
/// </summary>
public abstract class InputRange<T>
{
    protected T m_controlValue;
    // Keeps watch for largest and smallest values and zero crossings
    protected MagMinMax<T> m_magMinMax;

    /// <summary>
    /// Returns largest input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    public virtual T UnqueriedMaxInputValue { get => InternalInput(m_magMinMax.MaxValue); }
    /// <summary>
    /// Returns smallest input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    public virtual T UnqueriedMinInputValue { get => InternalInput(m_magMinMax.MinValue); }
    /// <summary>
    /// Returns larest magnitude input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    /// <value></value>
    public virtual T UnqueriedMaxMagnitudeInputValue { get => InternalInput(m_magMinMax.MaxMagValue); }
    /// <summary>
    /// Returns true if ControlVector reached or crossed zero since the last call to InputValue() or ClearStatistics()
    /// </summary>
    /// <value></value>
    public virtual bool UnqueriedZeroInputValue { get => m_magMinMax.WasZero(); }
    //- Control value - input to this class
    public virtual T ControlValue { 
        get => m_controlValue;
        set {
            m_magMinMax.ObserveValue(value);
            m_controlValue = value;
        }
    }
    public virtual T InputValue {
        get {
            ClearStatistics(m_controlValue);
            return InternalInput(m_controlValue);
        }
    }
    /// <summary>
    /// Clears memory of control value's min, max and zero-crossings
    /// </summary>
    public void ClearStatistics(T value) {
        m_magMinMax.Reset(value);
    }
    protected abstract T InternalInput(T controlValue);
}
