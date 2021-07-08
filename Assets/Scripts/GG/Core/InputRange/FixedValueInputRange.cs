using UnityEngine;

/// <summary>
/// Returns -maxValue, 0 or m_maxValue
/// </summary>
public class FixedValueInputRangeFloat : InputRange<float> {
    protected float m_inputValueMax;
    protected float m_analogueDeadZone;
    protected override float InternalInput(float controlScalar) {
        if (controlScalar > m_analogueDeadZone) {
            return m_inputValueMax;
        }
        else if (controlScalar < -m_analogueDeadZone) {
            return -m_inputValueMax;
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
    public FixedValueInputRangeFloat(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}


/// <summary>
/// Returns -maxValue, 0 or m_maxValue
/// </summary>
public class FixedValueInputRangeVector2 : InputRange<Vector2> {
    protected float m_inputValueMax;
    protected float m_analogueDeadZoneSqr;
    protected override Vector2 InternalInput(Vector2 controlScalar) {
        float magSqr = controlScalar.sqrMagnitude;
        if (magSqr > m_analogueDeadZoneSqr) {
            // Since we already have magSqr, let's not waste calculations
            Vector2 controlScalarNormalized = controlScalar / Mathf.Sqrt(magSqr);
            return controlScalarNormalized*m_inputValueMax;
        } else {
            return Vector2.zero;
        }
    }
    public void ChangeParameters(float inputValueMax, float analogueDeadZone = 0) {
        m_inputValueMax = inputValueMax;
        if (analogueDeadZone < 0 || analogueDeadZone > m_inputValueMax) {
            Debug.LogError("Analogue dead zone out of range: 0 .. " + m_inputValueMax);
            m_analogueDeadZoneSqr = 0;
        }
        m_analogueDeadZoneSqr = analogueDeadZone*analogueDeadZone;
    }
    public FixedValueInputRangeVector2(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}


/// <summary>
/// Returns -maxValue, 0 or m_maxValue
/// </summary>
public class FixedValueInputRangeVector3 : InputRange<Vector3> {
    protected float m_inputValueMax;
    protected float m_analogueDeadZoneSqr;
    protected override Vector3 InternalInput(Vector3 controlScalar) {
        float magSqr = controlScalar.sqrMagnitude;
        if (magSqr > m_analogueDeadZoneSqr) {
            // Since we already have magSqr, let's not waste calculations
            Vector3 controlScalarNormalized = controlScalar / Mathf.Sqrt(magSqr);
            return controlScalarNormalized*m_inputValueMax;
        } else {
            return Vector3.zero;
        }
    }
    public void ChangeParameters(float inputValueMax, float analogueDeadZone = 0) {
        m_inputValueMax = inputValueMax;
        if (analogueDeadZone < 0 || analogueDeadZone > m_inputValueMax) {
            Debug.LogError("Analogue dead zone out of range: 0 .. " + m_inputValueMax);
            m_analogueDeadZoneSqr = 0;
        }
        m_analogueDeadZoneSqr = analogueDeadZone*analogueDeadZone;
    }
    public FixedValueInputRangeVector3(float maxInputValue, float analogueDeadZone) {
        ChangeParameters(maxInputValue, analogueDeadZone);
    }
}

