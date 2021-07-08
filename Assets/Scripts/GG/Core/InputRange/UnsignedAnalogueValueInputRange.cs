using UnityEngine;

/// <summary>
/// Returns 0..m_maxValue
/// </summary>
public class UnsignedAnalogueValueInputRangeFloat : InputRange<float>
{
    protected float m_inputValueMax;
    protected float m_analogueDeadZone;
    protected float m_correctedFactor;
    protected override float InternalInput(float controlScalar) {
        if (controlScalar > m_analogueDeadZone) {
            return (controlScalar-m_analogueDeadZone)*m_correctedFactor;
        }
        else {
            return 0;
        }
    }
    public void ChangeParameters(float inputValueMax, float analogueDeadZone = 0) {
        m_inputValueMax = inputValueMax;
        m_analogueDeadZone = analogueDeadZone;
        if (m_analogueDeadZone < 0 || m_analogueDeadZone > m_inputValueMax) {
            Debug.LogError("Analogue dead zone out of range: 0 .. " + m_inputValueMax);
            m_analogueDeadZone = 0;
        }
        m_correctedFactor = m_inputValueMax/(1-m_analogueDeadZone);
    }
    public UnsignedAnalogueValueInputRangeFloat(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}


// There is no UnsignedAnalogueValueInputRange for vectors - just use AnalogueValueInputRange