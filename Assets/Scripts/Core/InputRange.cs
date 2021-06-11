using UnityEngine;

/// <summary>
/// InputRange base class, expects ControlScalar between -1 and 1, retuns InputValue according to prescribed behaviour
/// </summary>
public abstract class InputRange
{
    protected float m_controlScalar;
    //- Records highest ControlScalar set since last InputValue call
    protected float m_unqueriedMaxControlValue;
    //- Records lowest ControlScalar set since last InputValue call
    protected float m_unqueriedMinControlValue;
    /// <summary>
    /// Returns largest input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    public virtual float UnqueriedMaxInputValue { get => InternalInput(m_unqueriedMaxControlValue); }
    /// <summary>
    /// Returns smallest input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    public virtual float UnqueriedMinInputValue { get => InternalInput(m_unqueriedMinControlValue); }
    /// <summary>
    /// Returns larest magnitude input value that was seen since the last call to InputValue() or ClearStatistics()
    /// </summary>
    /// <value></value>
    public virtual float UnqueriedMaxMagnitudeInputValue
    {
        get
        {
            float newMaxValue = UnqueriedMaxInputValue;
            float newMinValue = UnqueriedMinInputValue;
            float newValue;
            if (Mathf.Abs(newMaxValue) > Mathf.Abs(newMinValue))
            {
                newValue = newMaxValue;
            }
            else
            {
                newValue = newMinValue;
            }
            return newValue;
        }
    }
    /// <summary>
    /// Returns true if ControlVector reached or crossed zero since the last call to InputValue() or ClearStatistics()
    /// </summary>
    /// <value></value>
    public virtual bool UnqueriedZeroCrossingInputValue
    {
        get 
        {
            return m_unqueriedMaxControlValue*m_unqueriedMinControlValue <= 0;
        }
    }
    //- Control value - input to this class
    public virtual float ControlScalar
    {
        get { return m_controlScalar; }
        set
        {
            m_controlScalar = value;
            m_unqueriedMinControlValue = Mathf.Min(m_unqueriedMinControlValue, m_controlScalar);
            m_unqueriedMaxControlValue = Mathf.Max(m_unqueriedMaxControlValue, m_controlScalar);
        }
    }
    public virtual float InputValue
    {
        get
        {
            ClearStatistics();
            return InternalInput(m_controlScalar);
        }
    }
    public void ClearStatistics()
    {
        m_unqueriedMaxControlValue = m_controlScalar;
        m_unqueriedMinControlValue = m_controlScalar;
    }
    protected abstract float InternalInput(float controlScalar);
}


/// <summary>
/// Returns 0 or m_maxValue
/// </summary>
public class UnsignedFixedValueInputRange : InputRange
{
    public float m_inputValueMax;
    public float m_analogueDeadZone;
    protected override float InternalInput(float controlScalar)
    {
        return controlScalar > m_analogueDeadZone ? m_inputValueMax : 0;
    }
    public UnsignedFixedValueInputRange(float maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}


/// <summary>
/// Returns -m_inputValueMax, 0 or m_inputValueMax
/// </summary>
public class FixedValueInputRange : InputRange
{
    public float m_inputValueMax;
    public float m_analogueDeadZone;
    protected override float InternalInput(float controlScalar)
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
    public FixedValueInputRange(float maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}


/// <summary>
/// Return an analogue value between 0 and m_inputValueMax, and allows for an analogue dead zone
/// </summary>
public class UnsignedAnalogueInputRange : InputRange
{
    public float m_inputValueMax;
    public float m_analogueDeadZone;
    protected override float InternalInput(float controlScalar)
    {
        return controlScalar > m_analogueDeadZone ? controlScalar * m_inputValueMax : 0;
    }
    public UnsignedAnalogueInputRange(float maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}


/// <summary>
/// Return an analogue value between -m_inputValueMax and m_inputValueMax, and allows for an analogue dead zone
/// </summary>
public class AnalogueInputRange : InputRange
{
    public float m_inputValueMax;
    public float m_analogueDeadZone;
    protected override float InternalInput(float controlScalar)
    {
        if (controlScalar > -m_analogueDeadZone && controlScalar < m_analogueDeadZone)
        {
            return 0;
        }
        return controlScalar * m_inputValueMax;
    }
    public AnalogueInputRange(float maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}
