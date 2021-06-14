using System.Collections.Generic;
using UnityEngine;

public struct KVariableLimit {
    public KVariableTypeSet Type;
    public float Value;
    public bool Max; // true = this is a maximum; false = this is a minimum

    //*** Constructors
    public KVariableLimit(KVariableTypeSet kv, float value, bool max) {
        Type = kv;
        Type.SingularOnly = true;
        Value = value;
        Max = max;
    }
}

public class KVariableLimits {
    KVariableSetExt1D m_maxVars;
    KVariableSetExt1D m_minVars;

    // *** Access
    public KVariableSetExt1D Max { get => m_maxVars; set => m_maxVars = value; }
    public KVariableSetExt1D Min { get => m_minVars; set => m_minVars = value; }

    // *** Edit
    public bool AddMax(KVariableTypeSet type, float value) {
        return Add(new KVariableLimit(type, value, true));
    }
    public bool AddMin(KVariableTypeSet type, float value) {
        return Add(new KVariableLimit(type, value, false));
    }
    public bool Add(KVariableTypeSet type, float max, float min) {
        bool ret = Add(new KVariableLimit(type, max, true));
        return ret && Add(new KVariableLimit(type, min, false));
    }
    public bool Add(KVariableTypeSet type, float value, bool max) {
        return Add(new KVariableLimit(type, value, max));
    }
    public bool AddMax(string name, float value) {
        return m_maxVars.Set(name, value);
    }
    public bool AddMin(string name, float value) {
        return m_minVars.Set(name, value);
    }
    public bool Add(string name, float value, bool max) {
        if (max) {
            return m_maxVars.Set(name, value);
        } else {
            return m_minVars.Set(name, value);
        }
    }
    public bool Add(KVariableLimit kvl) {
        switch (kvl.Type.Value) {
            case KVariableTypeInfo.NoneEnum:
                Debug.LogWarning("Attempting to add None type kinematic variable limit");
                return false;
            case KVariableTypeInfo.VariableEnum:
                if (kvl.Max) {
                    m_maxVars.Variable = kvl.Value;
                } else {
                    m_minVars.Variable = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.DerivativeEnum:
                if (kvl.Max) {
                    m_maxVars.Derivative = kvl.Value;
                } else {
                    m_minVars.Derivative = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.SecondDerivativeEnum:
                if (kvl.Max) {
                    m_maxVars.SecondDerivative = kvl.Value;
                } else {
                    m_minVars.SecondDerivative = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.AppliedForceEnum:
                if (kvl.Max) {
                    m_maxVars.AppliedForce = kvl.Value;
                } else {
                    m_minVars.AppliedForce = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.ImpulseForceEnum:
                if (kvl.Max) {
                    m_maxVars.ImpulseForce = kvl.Value;
                } else {
                    m_minVars.ImpulseForce = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.DragEnum:
                if (kvl.Max) {
                    m_maxVars.Drag = kvl.Value;
                } else {
                    m_minVars.Drag = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.ThirdDerivativeEnum:
                if (kvl.Max) {
                    m_maxVars.ThirdDerivative = kvl.Value;
                } else {
                    m_minVars.ThirdDerivative = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.AppliedForceDerivativeEnum:
                if (kvl.Max) {
                    m_maxVars.AppliedForceDerivative = kvl.Value;
                } else {
                    m_minVars.AppliedForceDerivative = kvl.Value;
                }
                return true;
            case KVariableTypeInfo.ImpulseForceDerivativeEnum:
                if (kvl.Max) {
                    m_maxVars.ImpulseForceDerivative = kvl.Value;
                } else {
                    m_minVars.ImpulseForceDerivative = kvl.Value;
                }
                return true;
            default:
                Debug.LogError("Unhandled case");
                return false;
        }
    }
    public bool Remove(KVariableTypeSet type, bool max) {
        float value = max ? float.PositiveInfinity : float.NegativeInfinity;
        return Add(new KVariableLimit(type, value, max));
    }
    public bool RemoveMax(KVariableTypeSet type) {
        return Add(new KVariableLimit(type, float.PositiveInfinity, true));
    }
    public bool RemoveMin(KVariableTypeSet type) {
        return Add(new KVariableLimit(type, float.NegativeInfinity, false));
    }
    public bool Remove(KVariableLimit kvl) {
        if (kvl.Max) {
            kvl.Value = float.PositiveInfinity;
        } else {
            kvl.Value = float.NegativeInfinity;
        }
        return Add(kvl);
    }
    public bool RemoveMax(string name) {
        return m_maxVars.Set(name, float.PositiveInfinity);
    }
    public bool RemoveMin(string name) {
        return m_minVars.Set(name, float.NegativeInfinity);
    }
    public bool Remove(string name, bool max) {
        if (max) {
            return m_maxVars.Set(name, float.PositiveInfinity);
        } else {
            return m_minVars.Set(name, float.NegativeInfinity);
        }
    }


    // *** Constructors
    public KVariableLimits() {
        m_maxVars = new KVariableSetExt1D(float.PositiveInfinity);
        m_minVars = new KVariableSetExt1D(float.NegativeInfinity);
    }
    public KVariableLimits(KVariableLimit kvl) {
        m_maxVars = new KVariableSetExt1D(float.PositiveInfinity);
        m_minVars = new KVariableSetExt1D(float.NegativeInfinity);
        Add(kvl);
    }
    public KVariableLimits(KVariableLimit[] kvlArray) {
        m_maxVars = new KVariableSetExt1D(float.PositiveInfinity);
        m_minVars = new KVariableSetExt1D(float.NegativeInfinity);
        foreach (KVariableLimit kvl in kvlArray) {
            Add(kvl);
        }
    }
    public KVariableLimits(List<KVariableLimit> kvlList) {
        m_maxVars = new KVariableSetExt1D(float.PositiveInfinity);
        m_minVars = new KVariableSetExt1D(float.NegativeInfinity);
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
        float impulseForceDerivativeMax, float impulseForceDerivativeMin,
        float dragMax, float dragMin
    ) {
        m_maxVars = new KVariableSetExt1D (
            variableMax,
            derivativeMax,
            secondDerivativeMax,
            thirdDerivativeMax,
            appliedForceMax,
            appliedForceDerivativeMax,
            impulseForceMax,
            impulseForceDerivativeMax,
            dragMax
        );
        m_minVars = new KVariableSetExt1D (
            variableMin,
            derivativeMin,
            secondDerivativeMin,
            thirdDerivativeMin,
            appliedForceMin,
            appliedForceDerivativeMin,
            impulseForceMin,
            impulseForceDerivativeMin,
            dragMin
        );
    }
}

