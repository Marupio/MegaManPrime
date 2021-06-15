using UnityEngine;

public abstract class MagMinMax<T>{
    // Clean history, giving current value
    public abstract void Reset(T value);
    // Max limit - largest positive value, float, and I guess largest mag value for a vector
    public virtual T MaxValue { get; }
    // Min limit - negative values for float, closest to zero for vector
    public virtual T MinValue { get; }
    // For float, largest magnitude (positive or negative), for a vector, same as MaxValue
    public virtual T MaxMagValue { get; }
    // True if I ever was equal to zero, within tolerance
    public abstract bool WasZero();
    // True if I ever was non zero, within tolerance
    public abstract void ObserveValue(T value);
}

public class MagMinMaxFloat : MagMinMax<float> {
    float m_maxValue;
    float m_minValue;
    public override void Reset(float value) {
        m_maxValue = value;
        m_minValue = value;
    }
    public override float MaxValue {
        get { return m_maxValue; }
    }
    public override float MinValue {
        get { return m_minValue; }
    }
    public override float MaxMagValue {
        get {
            if (Mathf.Abs(m_minValue) > Mathf.Abs(m_maxValue)) {
                return m_minValue;
            } else {
                return m_maxValue;
            }
        }
    }
    public override bool WasZero() {
        return (m_maxValue * m_minValue <= 0);
    }
    public override void ObserveValue(float value)
    {
        m_maxValue = Mathf.Max(value, m_maxValue);
        m_minValue = Mathf.Min(value, m_minValue);
    }
}


public class MagMinMaxVector2 : MagMinMax<Vector2>
{
    Vector2 m_maxValue;
    Vector2 m_minValue;
    float m_magSqrMax = 0;
    float m_magSqrMin = Mathf.Infinity;
    float m_sqrTolerance = 0;
    public void SetTolerance(float value) {
        m_sqrTolerance = value*value;
    }
    public override void Reset(Vector2 value) {
        m_maxValue = value;
        m_minValue = value;
        float magSqr = Vector2.SqrMagnitude(value);
        m_magSqrMax = magSqr;
        m_magSqrMin = magSqr;
    }
    public override Vector2 MaxValue {
        get { return m_maxValue; }
    }
    public override Vector2 MinValue {
        get { return m_minValue; }
    }
    public override Vector2 MaxMagValue {
        get { return m_maxValue; }
    }
    public override bool WasZero() {
        return Vector2.SqrMagnitude(m_minValue) < m_sqrTolerance;
    }
    public override void ObserveValue(Vector2 value) {
        float magSqr = Vector2.SqrMagnitude(value);
        if (magSqr > m_magSqrMax) {
            m_magSqrMax = magSqr;
            m_maxValue = value;
        }
        else if (magSqr < m_magSqrMax) {
            m_magSqrMin = magSqr;
            m_minValue = value;
        }
    }
}


public class MagMinMaxVector3 : MagMinMax<Vector3>
{
    Vector3 m_maxValue;
    Vector3 m_minValue;
    float m_magSqrMax = 0;
    float m_magSqrMin = Mathf.Infinity;
    float m_sqrTolerance = 0;
    public void SetTolerance(float value)
    {
        m_sqrTolerance = value * value;
    }
    public override void Reset(Vector3 value)
    {
        m_maxValue = value;
        m_minValue = value;
        float magSqr = Vector3.SqrMagnitude(value);
        m_magSqrMax = magSqr;
        m_magSqrMin = magSqr;
    }
    public override Vector3 MaxValue {
        get { return m_maxValue; }
    }
    public override Vector3 MinValue {
        get { return m_minValue; }
    }
    public override Vector3 MaxMagValue {
        get { return m_maxValue; }
    }
    public override bool WasZero()
    {
        return Vector3.SqrMagnitude(m_minValue) < m_sqrTolerance;
    }
    public override void ObserveValue(Vector3 value)
    {
        float magSqr = Vector3.SqrMagnitude(value);
        if (magSqr > m_magSqrMax)
        {
            m_magSqrMax = magSqr;
            m_maxValue = value;
        }
        else if (magSqr < m_magSqrMax)
        {
            m_magSqrMin = magSqr;
            m_minValue = value;
        }
    }
}
