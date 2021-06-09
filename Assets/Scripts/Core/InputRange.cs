
/// <summary>
/// InputRange base class, expects ControlScalar between -1 and 1, retuns InputValue according to prescribed behaviour
/// </summary>
public abstract class InputRange
{
    protected float m_controlScalar;
    virtual public float ControlScalar { get => m_controlScalar; set => m_controlScalar = value; }
    public abstract float InputValue { get; }
}

/// <summary>
/// Return an analogue value between -m_inputValueMax and m_inputValueMax, and allows for an analogue dead zone
/// </summary>
public class AnalogueInputRange : InputRange
{
    public float m_inputValueMax;
    public float m_analogueDeadZone;
    public override float InputValue
    {
        get
        {
            if (m_controlScalar > -m_analogueDeadZone && m_controlScalar < m_analogueDeadZone)
            {
                return 0;
            }
            return m_controlScalar*m_inputValueMax;
        }
    }
    public AnalogueInputRange(float maxInputValue, float analogueDeadZone)
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
    public override float InputValue
    {
        get { return m_controlScalar > m_analogueDeadZone ? m_controlScalar*m_inputValueMax : 0; }
    }
    public UnsignedAnalogueInputRange(float maxInputValue, float analogueDeadZone)
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
    public override float InputValue
    {
        get
        {
            if (m_controlScalar > m_analogueDeadZone)
            {
                return m_inputValueMax;
            }
            else if (m_controlScalar < -m_analogueDeadZone)
            {
                return -m_inputValueMax;
            }
            else
            {
                return 0;
            }
        }
    }
    public FixedValueInputRange(float maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}

/// <summary>
/// Returns 0 or m_maxValue
/// </summary>
public class UnsignedFixedValueInputRange : InputRange
{
    public float m_inputValueMax;
    public float m_analogueDeadZone;
    public override float InputValue
    {
        get { return m_controlScalar > m_analogueDeadZone ? m_inputValueMax : 0; }
    }
    public UnsignedFixedValueInputRange(float maxInputValue, float analogueDeadZone)
    {
        m_inputValueMax = maxInputValue;
        m_analogueDeadZone = analogueDeadZone;
    }
}
