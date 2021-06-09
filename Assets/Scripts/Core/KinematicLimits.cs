using System.Collections.Generic;
using UnityEngine;

public enum KinematicVariable
{
    None,
    Position,
    Speed,
    Acceleration,
    Jerk

    // For now, we will ignore any higher derivatives
    // Snap,
    // Crackle,
    // Pop
}

public class KinematicLimit
{
    public KinematicVariable Type;
    public float Max;
    public float Min;
    public KinematicLimit()
    {
        Type = KinematicVariable.None;
        Max = float.PositiveInfinity;
        Min = float.NegativeInfinity;
    }
    public KinematicLimit(KinematicVariable typeIn, float maxIn, float minIn)
    {
        Type = typeIn;
        Max = maxIn;
        Min = minIn;
    }
}

public class KinematicLimits
{
    private float m_positionMax = float.PositiveInfinity;
    private float m_positionMin = float.NegativeInfinity;
    private float m_speedMax = float.PositiveInfinity;
    private float m_speedMin = float.NegativeInfinity;
    private float m_accelerationMax = float.PositiveInfinity;
    private float m_accelerationMin = float.NegativeInfinity;
    private float m_jerkMax = float.PositiveInfinity;
    private float m_jerkMin = float.NegativeInfinity;

    public virtual float PositionMax { get => m_positionMax; set => m_positionMax = value; }
    public virtual float PositionMin { get => m_positionMin; set => m_positionMin = value; }
    public virtual float SpeedMax { get => m_speedMax; set => m_speedMax = value; }
    public virtual float SpeedMin { get => m_speedMin; set => m_speedMin = value; }
    public virtual float AccelerationMax { get => m_accelerationMax; set => m_accelerationMax = value; }
    public virtual float AccelerationMin { get => m_accelerationMin; set => m_accelerationMin = value; }
    public virtual float JerkMax { get => m_jerkMax; set => m_jerkMax = value; }
    public virtual float JerkMin { get => m_jerkMin; set => m_jerkMin = value; }
    
    public KinematicLimits() {}
    public KinematicLimits(KinematicLimits limits)
    {
        m_positionMax = limits.m_positionMax;
        m_positionMin = limits.m_positionMin;
        m_speedMax = limits.m_speedMax;
        m_speedMin = limits.m_speedMin;
        m_accelerationMax = limits.m_accelerationMax;
        m_accelerationMin = limits.m_accelerationMin;
        m_jerkMax = limits.m_jerkMax;
        m_jerkMin = limits.m_jerkMin;
    }
    public KinematicLimits(KinematicLimit limit)
    {
        SetLimit(limit);
    }
    public KinematicLimits(KinematicLimit[] limits)
    {
        if (limits.Length > 4)
        {
            Debug.LogWarning("Constructing kinematic limits from more than kinematic variables");
        }
        foreach (KinematicLimit limit in limits)
        {
            SetLimit(limit);
        }
    }
    public KinematicLimits(List<KinematicLimit> limits)
    {
        if (limits.Count > 4)
        {
            Debug.LogWarning("Constructing kinematic limits from more than kinematic variables");
        }
        foreach (KinematicLimit limit in limits)
        {
            SetLimit(limit);
        }
    }
    public KinematicLimits(KinematicVariable varType, float max, float min)
    {
        SetLimit(varType, max, min);
    }
    public KinematicLimits(KinematicVariable varType, float max, float min, KinematicVariable varType1, float max1, float min1)
    {
        if (varType == varType1)
        {
            Debug.LogWarning("Setting limits to same variable type more than once");
        }
        SetLimit(varType, max, min);
        SetLimit(varType1, max1, min1);
    }
    public KinematicLimits
    (
        KinematicVariable varType, float max, float min,
        KinematicVariable varType1, float max1, float min1,
        KinematicVariable varType2, float max2, float min2
    )
    {
        if (varType == varType1 || varType == varType2 || varType1 == varType2)
        {
            Debug.LogWarning("Setting limits to same variable type more than once");
        }
        SetLimit(varType, max, min);
        SetLimit(varType1, max1, min1);
        SetLimit(varType2, max2, min2);
    }
    public KinematicLimits
    (
        float positionMin, float positionMax,
        float speedMin, float speedMax,
        float accelerationMin, float accelerationMax,
        float jerkMin, float jerkMax
    )
    {
        m_positionMax = positionMax;
        m_positionMin = positionMin;
        m_speedMax = speedMax;
        m_speedMin = speedMin;
        m_accelerationMax = accelerationMax;
        m_accelerationMin = accelerationMin;
        m_jerkMax = jerkMax;
        m_jerkMin = jerkMin;
    }

    public void SetLimit(KinematicLimit limit)
    {
        SetLimit(limit.Type, limit.Max, limit.Min);
    }
    public void SetLimit(KinematicVariable varType, float max, float min)
    {
        switch (varType)
        {
            case KinematicVariable.None:
            {
                Debug.LogWarning("Attempting to set None to limits");
                break;
            }
            case KinematicVariable.Position:
            {
                m_positionMax = max;
                m_positionMin = min;
                break;
            }
            case KinematicVariable.Speed:
            {
                m_speedMax = max;
                m_speedMin = min;
                break;
            }
            case KinematicVariable.Acceleration:
            {
                m_accelerationMax = max;
                m_accelerationMin = min;
                break;
            }
            case KinematicVariable.Jerk:
            {
                m_jerkMax = max;
                m_jerkMin = min;
                break;
            }
            default:
            {
                Debug.LogError("Unhandled case");
                break;
            }
        }
    }
    public void RemoveLimit(KinematicVariable varType)
    {
        switch (varType)
        {
            case KinematicVariable.None:
                {
                    Debug.LogWarning("Attempting to remove None from limits");
                    break;
                }
            case KinematicVariable.Position:
                {
                    m_positionMax = float.PositiveInfinity;
                    m_positionMin = float.NegativeInfinity;
                    break;
                }
            case KinematicVariable.Speed:
                {
                    m_speedMax = float.PositiveInfinity;
                    m_speedMin = float.NegativeInfinity;
                    break;
                }
            case KinematicVariable.Acceleration:
                {
                    m_accelerationMax = float.PositiveInfinity;
                    m_accelerationMin = float.NegativeInfinity;
                    break;
                }
            case KinematicVariable.Jerk:
                {
                    m_jerkMax = float.PositiveInfinity;
                    m_jerkMin = float.NegativeInfinity;
                    break;
                }
            default:
                {
                    Debug.LogError("Unhandled case");
                    break;
                }
        }
    }
    public void RemoveAllLimits()
    {
        m_positionMax = float.PositiveInfinity;
        m_positionMin = float.NegativeInfinity;
        m_speedMax = float.PositiveInfinity;
        m_speedMin = float.NegativeInfinity;
        m_accelerationMax = float.PositiveInfinity;
        m_accelerationMin = float.NegativeInfinity;
        m_jerkMax = float.PositiveInfinity;
        m_jerkMin = float.NegativeInfinity;
    }
}
