using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines max and min limits for all KVariableEnum variable types.  A variable without a limit has Infinite as its limit values.
/// See also the KvLimiter class - it applies these limits on variable instances.
/// </summary>
/// <seealso cref="KvLimiter"/>
public class KVariableLimits {// : PipelineExecutableBase {
// TODO - Bring in the data model here as you had planned... sometime in the future
    // KVariablesExtFloatSourceDataObj m_maxVars; // make parent same as me, name, mine+"max", kind of thing
    // KVariablesExtFloatSourceDataObj m_minVars; // make parent same as me
    KVariablesExt<float> m_maxVars; // make parent same as me, name, mine+"max", kind of thing
    KVariablesExt<float> m_minVars; // make parent same as me

    // *** Derived data
//    KVariableTypeSetDerivedDataObj m_limitedVars;
//    KveFiniteFilterUpdater m_limitedVarsUpdater;
    KVariableTypeSet m_limitedVars;
    
    // *** Access
//    public KVariablesExt<float> Max { get => m_maxVars.Data; set => m_maxVars.Data = value; }
//    public KVariablesExt<float> Min { get => m_minVars.Data; set => m_minVars.Data = value; }
    public KVariablesExt<float> Max { get => m_maxVars; set => m_maxVars = value; }
    public KVariablesExt<float> Min { get => m_minVars; set => m_minVars = value; }
    public KVariableTypeSet LimitedVars {
        get { // TODO - this is the main project
            // if (m_upToDateFrame < 0) {
            //     UpdateDerived();
            // }
            return m_limitedVars;
        }
    }

    // *** Edit
    public void AddMax(KVariableEnum type, float value) {
        Add(new KVariable<float>(type, value), true);
    }
    public void AddMin(KVariableEnum type, float value) {
        Add(new KVariable<float>(type, value), false);
    }
    public void Add(KVariableEnum type, float max, float min) {
        Add(new KVariable<float>(type, max), true);
        Add(new KVariable<float>(type, min), false);
    }
    public void Add(KVariableEnum type, float value, bool max) {
        Add(new KVariable<float>(type, value), max);
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
    public void Add(KVariable<float> kv, bool max) {
        
        switch (kv.type) {
            case KVariableTypeInfo.NoneEnum:
                Debug.LogWarning("Attempting to add None type kinematic variable limit");
                break;
            case KVariableEnum.Variable:
                if (max) {
                    m_maxVars.m_variable = kv.value;
                } else {
                    m_minVars.m_variable = kv.value;
                }
                break;
            case KVariableEnum.Derivative:
                if (max) {
                    m_maxVars.m_derivative = kv.value;
                } else {
                    m_minVars.m_derivative = kv.value;
                }
                break;
            case KVariableEnum.SecondDerivative:
                if (max) {
                    m_maxVars.m_secondDerivative = kv.value;
                } else {
                    m_minVars.m_secondDerivative = kv.value;
                }
                break;
            case KVariableEnum.AppliedForce:
                if (max) {
                    m_maxVars.m_appliedForce = kv.value;
                } else {
                    m_minVars.m_appliedForce = kv.value;
                }
                break;
            case KVariableEnum.ImpulseForce:
                if (max) {
                    m_maxVars.m_impulseForce = kv.value;
                } else {
                    m_minVars.m_impulseForce = kv.value;
                }
                break;
            case KVariableEnum.ThirdDerivative:
                if (max) {
                    m_maxVars.m_thirdDerivative = kv.value;
                } else {
                    m_minVars.m_thirdDerivative = kv.value;
                }
                break;
            case KVariableEnum.AppliedForceDerivative:
                if (max) {
                    m_maxVars.m_appliedForceDerivative = kv.value;
                } else {
                    m_minVars.m_appliedForceDerivative = kv.value;
                }
                break;
            case KVariableEnum.ImpulseForceDerivative:
                if (max) {
                    m_maxVars.m_impulseForceDerivative = kv.value;
                } else {
                    m_minVars.m_impulseForceDerivative = kv.value;
                }
                break;
            default:
                Debug.LogError("Unhandled case");
                break;
        }
        // m_upToDateFrame = -1;
    }
    public void Remove(KVariableEnum type, bool max) {
        float value = max ? float.PositiveInfinity : float.NegativeInfinity;
        Add(new KVariable<float>(type, value), max);
    }
    public void RemoveMax(KVariableEnum type) {
        Add(new KVariable<float>(type, float.PositiveInfinity), true);
    }
    public void RemoveMin(KVariableEnum type) {
        Add(new KVariable<float>(type, float.NegativeInfinity), false);
    }
    public void Remove(KVariable<float> kv, bool max) {
        kv.value = max ? float.PositiveInfinity : float.NegativeInfinity;
        Add(kv, max);
    }
    public void RemoveMax(string name) {
        m_maxVars.Set(name, float.PositiveInfinity);
        // m_upToDateFrame = -1;
    }
    public void RemoveMin(string name) {
        m_minVars.Set(name, float.NegativeInfinity);
        // m_upToDateFrame = -1;
    }
    public void Remove(string name, bool max) {
        if (max) {
            m_maxVars.Set(name, float.PositiveInfinity);
        } else {
            m_minVars.Set(name, float.NegativeInfinity);
        }
        // m_upToDateFrame = -1;
    }
    /// <summary>
    /// Combine two KVariableLimits, taking the smallest maxima and largest minima
    /// </summary>
    public void Combine(KVariableLimits kvl) {
        // Use accessor to trigger demand-driven data
        // if (m_upToDateFrame < 0) { UpdateDerived(); }
        m_limitedVars |= kvl.LimitedVars;
        if (m_limitedVars.Contains(KVariableEnum.Variable)) {
            m_maxVars.Variable = Mathf.Min(m_maxVars.Variable, kvl.m_maxVars.Variable);
            m_minVars.Variable = Mathf.Min(m_minVars.Variable, kvl.m_minVars.Variable);
        }
        if (m_limitedVars.Contains(KVariableEnum.Derivative)) {
            m_maxVars.Derivative = Mathf.Min(m_maxVars.Derivative, kvl.m_maxVars.Derivative);
            m_minVars.Derivative = Mathf.Min(m_minVars.Derivative, kvl.m_minVars.Derivative);
        }
        if (m_limitedVars.Contains(KVariableEnum.SecondDerivative)) {
            m_maxVars.SecondDerivative = Mathf.Min(m_maxVars.SecondDerivative, kvl.m_maxVars.SecondDerivative);
            m_minVars.SecondDerivative = Mathf.Min(m_minVars.SecondDerivative, kvl.m_minVars.SecondDerivative);
        }
        if (m_limitedVars.Contains(KVariableEnum.ThirdDerivative)) {
            m_maxVars.ThirdDerivative = Mathf.Min(m_maxVars.ThirdDerivative, kvl.m_maxVars.ThirdDerivative);
            m_minVars.ThirdDerivative = Mathf.Min(m_minVars.ThirdDerivative, kvl.m_minVars.ThirdDerivative);
        }
        if (m_limitedVars.Contains(KVariableEnum.AppliedForce)) {
            m_maxVars.AppliedForce = Mathf.Min(m_maxVars.AppliedForce, kvl.m_maxVars.AppliedForce);
            m_minVars.AppliedForce = Mathf.Min(m_minVars.AppliedForce, kvl.m_minVars.AppliedForce);
        }
        if (m_limitedVars.Contains(KVariableEnum.ImpulseForce)) {
            m_maxVars.ImpulseForce = Mathf.Min(m_maxVars.ImpulseForce, kvl.m_maxVars.ImpulseForce);
            m_minVars.ImpulseForce = Mathf.Min(m_minVars.ImpulseForce, kvl.m_minVars.ImpulseForce);
        }
        if (m_limitedVars.Contains(KVariableEnum.AppliedForceDerivative)) {
            m_maxVars.AppliedForceDerivative = Mathf.Min(m_maxVars.AppliedForceDerivative, kvl.m_maxVars.AppliedForceDerivative);
            m_minVars.AppliedForceDerivative = Mathf.Min(m_minVars.AppliedForceDerivative, kvl.m_minVars.AppliedForceDerivative);
        }
        if (m_limitedVars.Contains(KVariableEnum.ImpulseForceDerivative)) {
            m_maxVars.ImpulseForceDerivative = Mathf.Min(m_maxVars.ImpulseForceDerivative, kvl.m_maxVars.ImpulseForceDerivative);
            m_minVars.ImpulseForceDerivative = Mathf.Min(m_minVars.ImpulseForceDerivative, kvl.m_minVars.ImpulseForceDerivative);
        }
    }

    // *** Execute
    // TODO
    //public void ApplyLimits(KVariables<>)

    // *** Operators
    /// <summary>
    /// Combine two KVariableLimits, taking the smallest maxima and largest minima - Combine method put in operator form because why not?
    /// </summary>
    public static KVariableLimits operator+(KVariableLimits kvA, KVariableLimits kvB) {
        KVariableLimits kvRes = new KVariableLimits(kvA);
        kvRes.Combine(kvB);
        return kvRes;
    }


    // *** Internal methods
    private void UpdateDerived() {
        #if DEBUG
            // if (m_upToDateFrame >= 0) {
            //     Debug.LogError("Rebuilding up-to-date derived data");
            // }
        #endif
        // // Alternative iteration method    
        // m_limitedVars = KVariableTypeInfo.None;
        // for (System.Int32 i = 1; i < KVariableTypeInfo.MaxValue; i *= 2) {
        //     KVariableEnum kve = (KVariableEnum)i;
        //     float val;
        //     m_maxVars.Get(kve, out val);
        //     if (float.IsPositiveInfinity(val)) {
        //         m_limitedVars.Add(kve);
        //         continue;
        //     }
        //     m_minVars.Get(kve, out val);
        //     if (float.IsNegativeInfinity(val)) {
        //         m_limitedVars.Add(kve);
        //     }
        // }
        m_limitedVars = KVariableTypeInfo.None;
        if (!float.IsPositiveInfinity(m_maxVars.Variable) || !float.IsNegativeInfinity(m_minVars.Variable)) { m_limitedVars.Add(KVariableEnum.Variable); }
        if (!float.IsPositiveInfinity(m_maxVars.Derivative) || !float.IsNegativeInfinity(m_minVars.Derivative)) { m_limitedVars.Add(KVariableEnum.Derivative); }
        if (!float.IsPositiveInfinity(m_maxVars.SecondDerivative) || !float.IsNegativeInfinity(m_minVars.SecondDerivative)) { m_limitedVars.Add(KVariableEnum.SecondDerivative); }
        if (!float.IsPositiveInfinity(m_maxVars.ThirdDerivative) || !float.IsNegativeInfinity(m_minVars.ThirdDerivative)) { m_limitedVars.Add(KVariableEnum.ThirdDerivative); }
        if (!float.IsPositiveInfinity(m_maxVars.AppliedForce) || !float.IsNegativeInfinity(m_minVars.AppliedForce)) { m_limitedVars.Add(KVariableEnum.AppliedForce); }
        if (!float.IsPositiveInfinity(m_maxVars.ImpulseForce) || !float.IsNegativeInfinity(m_minVars.ImpulseForce)) { m_limitedVars.Add(KVariableEnum.ImpulseForce); }
        if (!float.IsPositiveInfinity(m_maxVars.AppliedForceDerivative) || !float.IsNegativeInfinity(m_minVars.AppliedForceDerivative)) { m_limitedVars.Add(KVariableEnum.AppliedForceDerivative); }
        if (!float.IsPositiveInfinity(m_maxVars.ImpulseForceDerivative) || !float.IsNegativeInfinity(m_minVars.ImpulseForceDerivative)) { m_limitedVars.Add(KVariableEnum.ImpulseForceDerivative); }
        // m_upToDateFrame = m_time.frameCount;
    }
    // public override void InternalExecute(List<IDataObjMeta> inputs, List<IDataObjMeta> outputs) {
    //     // TODO
    // }

    // *** Constructors
    // TODO - Add DataPortProfiles to base construction, add base construction
    public KVariableLimits() {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        // m_upToDateFrame = m_time.frameCount;
        m_limitedVars = KVariableTypeInfo.None;
    }
    public KVariableLimits(KVariableLimits kvl) {
        m_maxVars = kvl.m_maxVars;
        m_minVars = kvl.m_minVars;
        // m_upToDateFrame = kvl.m_upToDateFrame;
        m_limitedVars = kvl.m_limitedVars;
    }
    public KVariableLimits(KVariable<float> kv, bool max) {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        // m_upToDateFrame = -1;
        m_limitedVars = KVariableTypeInfo.None;
        Add(kv, max);
    }
    public KVariableLimits(KVariable<float>[] kvArray, bool[] maxArray) {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        // m_upToDateFrame = -1;
        m_limitedVars = KVariableTypeInfo.None;
        for (int i = 0; i < kvArray.Length; ++i) {
            KVariable<float> kv = new KVariable<float>(kvArray[i]);
            Add(kv, maxArray[i]);
        }
    }
    public KVariableLimits(List<KVariable<float>> kvArray, List<bool> maxArray) {
        m_maxVars = new KVariablesExt<float>(float.PositiveInfinity);
        m_minVars = new KVariablesExt<float>(float.NegativeInfinity);
        // m_upToDateFrame = -1;
        m_limitedVars = KVariableTypeInfo.None;
        for (int i = 0; i < kvArray.Count; ++i) {
            KVariable<float> kv = new KVariable<float>(kvArray[i]);
            Add(kv, maxArray[i]);
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
        // m_upToDateFrame = -1;
        m_limitedVars = KVariableTypeInfo.None;
    }
}

