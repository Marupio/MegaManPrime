using UnityEngine;

/// <summary>
/// Returns -m_maxValue..-m_analogueDeadZone,0,m_analogueDeadZone..m_maxValue
/// </summary>
public class AnalogueValueInputRangeFloat : InputRange<float>
{
    protected float m_inputValueMax;
    protected float m_analogueDeadZone;
    protected float m_correctedFactor;
    protected override float InternalInput(float controlScalar) {
        if (controlScalar > m_analogueDeadZone) {
            return (controlScalar-m_analogueDeadZone)*m_correctedFactor;
        }
        else if (controlScalar < -m_analogueDeadZone) {
            return (controlScalar+m_analogueDeadZone)*m_correctedFactor;
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
    public AnalogueValueInputRangeFloat(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}


public class AnalogueValueInputRangeVector2 : InputRange<Vector2>
{
    protected float m_inputValueMax;
    protected float m_analogueDeadZone;
    protected float m_analogueDeadZoneSqr;
    protected float m_correctedFactor;
    protected override Vector2 InternalInput(Vector2 controlVector) {
        float sqrMag = controlVector.sqrMagnitude;
        if (sqrMag > m_analogueDeadZoneSqr) {
            float mag = (Mathf.Sqrt(sqrMag) - m_analogueDeadZone)*m_correctedFactor;
            return controlVector*mag;
        }
        else {
            return Vector2.zero;
        }
    }
    public void ChangeParameters(float inputValueMax, float analogueDeadZone = 0) {
        m_inputValueMax = inputValueMax;
        m_analogueDeadZone = analogueDeadZone;
        m_analogueDeadZoneSqr = analogueDeadZone*analogueDeadZone;
        if (analogueDeadZone < 0 || analogueDeadZone > m_inputValueMax) {
            Debug.LogError("Analogue dead zone out of range: 0 .. " + m_inputValueMax);
            m_analogueDeadZone = 0;
        }
        m_correctedFactor = m_inputValueMax/(1-m_analogueDeadZone);
    }
    public AnalogueValueInputRangeVector2(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}


public class AnalogueValueInputRangeVector3 : InputRange<Vector3>
{
    protected float m_inputValueMax;
    protected float m_analogueDeadZone;
    protected float m_analogueDeadZoneSqr;
    protected float m_correctedFactor;
    protected override Vector3 InternalInput(Vector3 controlVector) {
        float sqrMag = controlVector.sqrMagnitude;
        if (sqrMag > m_analogueDeadZoneSqr) {
            float mag = (Mathf.Sqrt(sqrMag) - m_analogueDeadZone)*m_correctedFactor;
            return controlVector*mag;
        }
        else {
            return Vector3.zero;
        }
    }
    public void ChangeParameters(float inputValueMax, float analogueDeadZone = 0) {
        m_inputValueMax = inputValueMax;
        m_analogueDeadZone = analogueDeadZone;
        m_analogueDeadZoneSqr = analogueDeadZone*analogueDeadZone;
        if (analogueDeadZone < 0 || analogueDeadZone > m_inputValueMax) {
            Debug.LogError("Analogue dead zone out of range: 0 .. " + m_inputValueMax);
            m_analogueDeadZone = 0;
        }
        m_correctedFactor = m_inputValueMax/(1-m_analogueDeadZone);
    }
    public AnalogueValueInputRangeVector3(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}
