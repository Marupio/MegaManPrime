using UnityEngine;

public abstract class MagMinMax<T>{
    // Clean history
    public abstract void Reset(T value);
    // Max limit - largest positive value, float, and I guess largest mag value for a vector
    public abstract T MaxValue();
    // Min limit - negative values for float, closest to zero for vector
    public abstract T MinValue();
    // For float, largest magnitude (positive or negative), for a vector, same as MaxValue
    public abstract T MaxMagValue();
    // True if I ever was equal to zero, within tolerance
    public abstract bool CrossedZero();
}

public class MagMinMaxFloat : MagMinMax<float> {
    float m_maxValue;
    float m_minValue;
    float m_zeroTolerance;
    // Clean history
    public override void Reset(float value) {
        m_maxValue = value;
        m_minValue = value;
    }
    // Max limit - largest positive value, float, and I guess largest mag value for a vector
    public override float MaxValue() {
        return m_maxValue;
    }
    // Min limit - negative values for float, closest to zero for vector
    public override float MinValue() {
        return m_minValue;
    }
    // For float, largest magnitude (positive or negative), for a vector, same as MaxValue
    public override float MaxMagValue() {
        if (Mathf.Abs(m_minValue) > m_maxValue) {
            return m_minValue;
        } else {
            return m_maxValue;
        }
    }
    // True if I ever was equal to zero, within tolerance
    public override bool CrossedZero() {
        return (m_maxValue * m_minValue <= 0);
    }
}



// public class MagMinMax<float> {
//     float m_maxValue;
//     float m_minValue;
//     float m_tolerance;
//     // Clean history
//     void abstract void Reset(T value);
//     // Max limit - largest positive value, float, and I guess largest mag value for a vector
//     abstract T MaxValue();
//     // Min limit - negative values for float, closest to zero for vector
//     abstract T MinValue();
//     // For float, largest magnitude (positive or negative), for a vector, same as MaxValue
//     abstract T MaxMagValue();
//     // True if I ever was equal to zero, within tolerance
//     abstract bool CrossedZero();
// }


/// <summary>
/// InputRange base class, expects ControlScalar between -1 and 1, retuns InputValue according to prescribed behaviour
/// </summary>
public abstract class InputRange<T>
{
    protected T m_controlValue;
    protected MagMinMax<T> m_magMinMax;

    //- Records highest ControlValue set since last InputValue call
    protected T m_unqueriedMaxControlValue;
    //- Records lowest ControlValue set since last InputValue call
    protected T m_unqueriedMinControlValue;
    /// <summary>
    /// Returns largest input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    public virtual T UnqueriedMaxInputValue { get => InternalInput(m_unqueriedMaxControlValue); }
    /// <summary>
    /// Returns smallest input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    public virtual T UnqueriedMinInputValue { get => InternalInput(m_unqueriedMinControlValue); }
    /// <summary>
    /// Returns larest magnitude input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    /// <value></value>
    public abstract T UnqueriedMaxMagnitudeInputValue { get; }
    // {
    //     get
    //     {
    //         T newMaxValue = UnqueriedMaxInputValue;
    //         T newMinValue = UnqueriedMinInputValue;
    //         T newValue;
    //         if (Mathf.Abs(newMaxValue) > Mathf.Abs(newMinValue))
    //         {
    //             newValue = newMaxValue;
    //         }
    //         else
    //         {
    //             newValue = newMinValue;
    //         }
    //         return newValue;
    //     }
    // }
    /// <summary>
    /// Returns true if ControlVector reached or crossed zero since the last call to InputValue() or ClearStatistics()
    /// </summary>
    /// <value></value>
    public virtual bool UnqueriedZeroCrossingInputValue { get; }
    // {
    //     get 
    //     {
    //         return m_unqueriedMaxControlValue*m_unqueriedMinControlValue <= 0;
    //     }
    // }
    //- Control value - input to this class
    public abstract T ControlValue
    {
        get;// { return m_controlValue; }
        set;
        // {
        //     m_controlValue = value;
        //     m_unqueriedMinControlValue = Mathf.Min(m_unqueriedMinControlValue, m_controlValue);
        //     m_unqueriedMaxControlValue = Mathf.Max(m_unqueriedMaxControlValue, m_controlValue);
        // }
    }
    public virtual T InputValue
    {
        get
        {
            ClearStatistics();
            return InternalInput(m_controlValue);
        }
    }
    public void ClearStatistics()
    {
        m_unqueriedMaxControlValue = m_controlValue;
        m_unqueriedMinControlValue = m_controlValue;
    }
    protected abstract T InternalInput(T controlValue);
}


/// <summary>
/// Returns 0 or m_maxValue
/// </summary>
public abstract class UnsignedFixedValueInputRange<T> : InputRange<T>
{
    public T m_inputValueMax;
    public T m_analogueDeadZone;
    //protected override T InternalInput(T controlValue);
    // {
    //     return controlValue > m_analogueDeadZone ? m_inputValueMax : 0;
    // }
    public UnsignedFixedValueInputRange<T>(T maxInputValue, T analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}


/// <summary>
/// Returns -m_inputValueMax, 0 or m_inputValueMax
/// </summary>
public class FixedValueInputRange<T> : InputRange<T>
{
    public T m_inputValueMax;
    public T m_analogueDeadZone;
    protected override T InternalInput(T controlScalar)
    {
        if (controlScalar > m_analogueDeadZone)
        {
            return m_inputValueMax;
        }
        else if (controlScalar < -m_analogueDeadZone)
        {
            return -m_inputValueMax;
        }
        else
        {
            return 0;
        }
    }
    public FixedValueInputRange(T maxInputValue, T analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}


/// <summary>
/// Return an analogue value between 0 and m_inputValueMax, and allows for an analogue dead zone
/// </summary>
public class UnsignedAnalogueInputRange : InputRange<T>
{
    public T m_inputValueMax;
    public T m_analogueDeadZone;
    protected override T InternalInput(T controlScalar)
    {
        return controlScalar > m_analogueDeadZone ? controlScalar * m_inputValueMax : 0;
    }
    public UnsignedAnalogueInputRange(T maxInputValue, T analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}


/// <summary>
/// Return an analogue value between -m_inputValueMax and m_inputValueMax, and allows for an analogue dead zone
/// </summary>
public class AnalogueInputRange : InputRange<T>
{
    public T m_inputValueMax;
    public T m_analogueDeadZone;
    protected override T InternalInput(T controlScalar)
    {
        if (controlScalar > -m_analogueDeadZone && controlScalar < m_analogueDeadZone)
        {
            return 0;
        }
        return controlScalar * m_inputValueMax;
    }
    public AnalogueInputRange(T maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}
