using UnityEngine;

/// <summary>
/// Returns 0 or m_maxValue
/// </summary>
public class UnsignedFixedValueInputRangeFloat : InputRange<float> {
    protected float m_inputValueMax;
    protected float m_analogueDeadZone;
    protected override float InternalInput(float controlScalar) {
        if (controlScalar > m_analogueDeadZone) {
            return m_inputValueMax;
        } else {
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
    }
    public UnsignedFixedValueInputRangeFloat(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}


// There are no UnsignedFixedValueInputRange Vectors, just use FixedValueInputRangeVector[2|3]