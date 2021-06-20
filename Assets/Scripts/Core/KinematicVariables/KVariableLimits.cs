using System.Collections.Generic;
using UnityEngine;

public struct KVariableLimit {
    public KVariableEnum Type;
    public float Value;
    public bool Max; // true = this is a maximum; false = this is a minimum

    //*** Constructors
    public KVariableLimit(KVariableEnum kv, float value, bool max) {
        Type = kv;
        Value = value;
        Max = max;
    }
}

public class KVariableLimits {
    KVariablesExt<float> m_maxVars;
    KVariablesExt<float> m_minVars;

    // *** Access
    public KVariablesExt<float> Max { get => m_maxVars; set => m_maxVars = value; }
    public KVariablesExt<float> Min { get => m_minVars; set => m_minVars = value; }

    // *** Edit
    public void AddMax(KVariableEnum type, float value) {
        Add(new KVariableLimit(type, value, true));
    }
    public void AddMin(KVariableEnum type, float value) {
        Add(new KVariableLimit(type, value, false));
    }
    public void Add(KVariableEnum type, float max, float min) {
        Add(new KVariableLimit(type, max, true));
        Add(new KVariableLimit(type, min, false));
    }
    public void Add(KVariableEnum type, float value, bool max) {
        Add(new KVariableLimit(type, value, max));
    }
    public void AddMax(string name, float value) {
        m_maxVars.Set(name, value);
    }
    public void AddMin(string name, float value) {
        m_minVars.Set(name, value);
    }
    public void Add(string name, float value, bool max) {
        if (max) {
            m_maxVars.Set(name, value);
        } else {
            m_minVars.Set(name, value);
        }
    }
    public void Add(KVariableLimit kvl) {
        switch (kvl.Type) {
            case KVariableTypeInfo.NoneEnum:
                Debug.LogWarning("Attempting to add None type kinematic variable limit");
                break;
            case KVariableEnum.Variable:
                if (kvl.Max) {
                    m_maxVars.Variable = kvl.Value;
                } else {
                    m_minVars.Variable = kvl.Value;
                }
                break;
            case KVariableEnum.Derivative:
                if (kvl.Max) {
                    m_maxVars.Derivative = kvl.Value;
                } else {
                    m_minVars.Derivative = kvl.Value;
                }
                break;
            case KVariableEnum.SecondDerivative:
                if (kvl.Max) {
                    m_maxVars.SecondDerivative = kvl.Value;
                } else {
                    m_minVars.SecondDerivative = kvl.Value;
                }
                break;
            case KVariableEnum.AppliedForce:
                if (kvl.Max) {
                    m_maxVars.AppliedForce = kvl.Value;
                } else {
                    m_minVars.AppliedForce = kvl.Value;
                }
                break;
            case KVariableEnum.ImpulseForce:
                if (kvl.Max) {
                    m_maxVars.ImpulseForce = kvl.Value;
                } else {
                    m_minVars.ImpulseForce = kvl.Value;
                }
                break;
            case KVariableEnum.ThirdDerivative:
                if (kvl.Max) {
                    m_maxVars.ThirdDerivative = kvl.Value;
                } else {
                    m_minVars.ThirdDerivative = kvl.Value;
                }
                break;
            case KVariableEnum.AppliedForceDerivative:
                if (kvl.Max) {
                    m_maxVars.AppliedForceDerivative = kvl.Value;
                } else {
                    m_minVars.AppliedForceDerivative = kvl.Value;
                }
                break;
            case KVariableEnum.ImpulseForceDerivative:
                if (kvl.Max) {
                    m_maxVars.ImpulseForceDerivative = kvl.Value;
                } else {
                    m_minVars.ImpulseForceDerivative = kvl.Value;
                }
                break;
            case KVariableEnum.Drag:
                Debug.LogWarning("Attempting to set drag type limit");
                break;
            default:
                Debug.LogError("Unhandled case");
                break;
        }
    }
    public void Remove(KVariableEnum type, bool max) {
        float value = max ? float.PositiveInfinity : float.NegativeInfinity;
        Add(new KVariableLimit(type, value, max));
    }
    public void RemoveMax(KVariableEnum type) {
        Add(new KVariableLimit(type, float.PositiveInfinity, true));
    }
    public void RemoveMin(KVariableEnum type) {
        Add(new KVariableLimit(type, float.NegativeInfinity, false));
    }
    public void Remove(KVariableLimit kvl) {
        if (kvl.Max) {
            kvl.Value = float.PositiveInfinity;
        } else {
            kvl.Value = float.NegativeInfinity;
        }
        Add(kvl);
    }
    public void RemoveMax(string name) {
        m_maxVars.Set(name, float.PositiveInfinity);
    }
    public void RemoveMin(string name) {
        m_minVars.Set(name, float.NegativeInfinity);
    }
    public void Remove(string name, bool max) {
        if (max) {
            m_maxVars.Set(name, float.PositiveInfinity);
        } else {
            m_minVars.Set(name, float.NegativeInfinity);
        }
    }
    // *** Constructors
    public KVariableLimits() {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
    }
    public KVariableLimits(KVariableLimits kvl) {
        m_maxVars = kvl.m_maxVars;
        m_minVars = kvl.m_minVars;
    }
    public KVariableLimits(KVariableLimit kvl) {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        Add(kvl);
    }
    public KVariableLimits(KVariableLimit[] kvlArray) {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        foreach (KVariableLimit kvl in kvlArray) {
            Add(kvl);
        }
    }
    public KVariableLimits(List<KVariableLimit> kvlList) {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        foreach (KVariableLimit kvl in kvlList) {
            Add(kvl);
        }
    }
    public KVariableLimits(
        float variableMax, float variableMin,
        float derivativeMax, float derivativeMin,
        float secondDerivativeMax, float secondDerivativeMin,
        float thirdDerivativeMax, float thirdDerivativeMin,
        float appliedForceMax, float appliedForceMin,
        float appliedForceDerivativeMax, float appliedForceDerivativeMin,
        float impulseForceMax, float impulseForceMin,
        float impulseForceDerivativeMax, float impulseForceDerivativeMin
    ) {
        m_maxVars = new KVariablesExt<float> (
            variableMax,
            derivativeMax,
            secondDerivativeMax,
            thirdDerivativeMax,
            appliedForceMax,
            appliedForceDerivativeMax,
            impulseForceMax,
            impulseForceDerivativeMax
        );
        m_minVars = new KVariablesExt<float> (
            variableMin,
            derivativeMin,
            secondDerivativeMin,
            thirdDerivativeMin,
            appliedForceMin,
            appliedForceDerivativeMin,
            impulseForceMin,
            impulseForceDerivativeMin
        );
    }
}

