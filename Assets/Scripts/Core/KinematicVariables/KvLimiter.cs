using System.Collections.Generic;
using UnityEngine;

// *** Interface
public interface IKvLimiter<T> {
    /// <summary>
    /// Apply limits to the single variable specified by type and value, or by KVariable
    /// </summary>
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref T singleIn);
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref T singleIn);
    public void ApplyLimits(KVariableLimits kvl, KVariable<T> kvIn);
    /// <summary>
    /// Apply limits to the provided variable sets
    /// </summary>
    public void ApplyLimits(KVariableLimits kvl, KVariables<T> kvIn);
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<T> kvIn);
    /// <summary>
    /// Apply limits to the provided variable sets, but only to those variables in the supplied KVariableTypeSets
    /// </summary>
    public void ApplyLimits(KVariableLimits kvl, KVariables<T> kvIn, KVariableTypeSet kvts);
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<T> kvIn, KVariableTypeSet kvts);
}

// *** Generic class
public class KvLimiter<T> : IKvLimiter<T> {
    public static readonly IKvLimiter<T> P = KvLimiter<T>.P as IKvLimiter<T> ?? new KvLimiter<T>();
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref T singleIn) { throw new System.NotImplementedException(); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref T singleIn) { throw new System.NotImplementedException(); }
    public void ApplyLimits(KVariableLimits kvl, KVariable<T> kvIn) { throw new System.NotImplementedException(); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<T> kvIn) { throw new System.NotImplementedException(); }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<T> kvIn) { throw new System.NotImplementedException(); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<T> kvIn, KVariableTypeSet kvts) { throw new System.NotImplementedException(); }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<T> kvIn, KVariableTypeSet kvts) { throw new System.NotImplementedException(); }
}

// *** Template specialisation pattern
public class KvLimiter :
    IKvLimiter<int>, IKvLimiter<float>,
    IKvLimiter<Vector2Int>, IKvLimiter<Vector2>,
    IKvLimiter<Vector3Int>, IKvLimiter<Vector3>,
    IKvLimiter<Vector4>, IKvLimiter<Quaternion>
{
    public static KvLimiter P = new KvLimiter(); 

    // *** int types
    public int Max(float f, int i) { return i >= (int)f ? i : (int)f; }
    public int Min(float f, int i) { return i < (int)f ? i : (int)f; }
    public void MaxMin(float max, float min, ref int singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref int singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref int singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<int> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<int> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<int> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<int> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<int> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** float types
    public float Max(float fA, float fB) { return fA >= fB ? fA : fB; }
    public float Min(float fA, float fB) { return fA < fB ? fA : fB; }
    public void MaxMin(float max, float min, ref float singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref float singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref float singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<float> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<float> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<float> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<float> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<float> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** Vector2Int types
    public Vector2Int Max(float f, Vector2Int v) { return new Vector2Int(v.x >= (int)f ? v.x : (int)f, v.y >= (int)f ? v.y : (int)f); }
    public Vector2Int Min(float f, Vector2Int v) { return new Vector2Int(v.x < (int)f ? v.x : (int)f, v.y < (int)f ? v.y : (int)f); }
    public void MaxMin(float max, float min, ref Vector2Int singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref Vector2Int singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref Vector2Int singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<Vector2Int> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector2Int> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector2Int> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector2Int> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector2Int> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** Vector2 types
    public Vector2 Max(float f, Vector2 v) {  return new Vector2(v.x >= f ? v.x : f, v.y >= f ? v.y : v.y); }
    public Vector2 Min(float f, Vector2 v) { return new Vector2(v.x < f ? v.x : f, v.y < f ? v.y : v.y); }
    public void MaxMin(float max, float min, ref Vector2 singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref Vector2 singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref Vector2 singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<Vector2> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector2> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector2> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector2> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector2> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** Vector3Int types
    public Vector3Int Max(float f, Vector3Int v) { return new Vector3Int(v.x >= (int)f ? v.x : (int)f, v.y >= (int)f ? v.y : (int)f, v.z >= (int)f ? v.z : (int)f); }
    public Vector3Int Min(float f, Vector3Int v) { return new Vector3Int(v.x < (int)f ? v.x : (int)f, v.y < (int)f ? v.y : (int)f, v.z < (int)f ? v.z : (int)f); }
    public void MaxMin(float max, float min, ref Vector3Int singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref Vector3Int singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref Vector3Int singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<Vector3Int> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector3Int> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector3Int> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector3Int> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector3Int> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** Vector3 types
    public Vector3 Max(float f, Vector3 v) { return new Vector3(v.x >= f ? v.x : f, v.y >= f ? v.y : f, v.z >= f ? v.z : f); }
    public Vector3 Min(float f, Vector3 v) { return new Vector3(v.x < f ? v.x : f, v.y < f ? v.y : f, v.z < f ? v.z : f); }
    public void MaxMin(float max, float min, ref Vector3 singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref Vector3 singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref Vector3 singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<Vector3> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector3> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector3> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector3> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector3> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** Vector4 types
    public Vector4 Max(float f, Vector4 v) { return new Vector4(v.x >= f ? v.x : f, v.y >= f ? v.y : f, v.z >= f ? v.z : f, v.w >= f ? v.w : f); }
    public Vector4 Min(float f, Vector4 v) { return new Vector4(v.x < f ? v.x : f, v.y < f ? v.y : f, v.z < f ? v.z : f, v.w < f ? v.w : f); }
    public void MaxMin(float max, float min, ref Vector4 singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref Vector4 singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref Vector4 singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<Vector4> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector4> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector4> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Vector4> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Vector4> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Variable, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }

    // *** Quaternion types
    public Quaternion Max(float f, Quaternion q) { return new Quaternion(q.x >= f ? q.x : f, q.y >= f ? q.y : f, q.z >= f ? q.z : f, q.w >= f ? q.w : f); }
    public Quaternion Min(float f, Quaternion q) { return new Quaternion(q.x < f ? q.x : f, q.y < f ? q.y : f, q.z < f ? q.z : f, q.w < f ? q.w : f); }
    public void MaxMin(float max, float min, ref Quaternion singleIn) { singleIn = Max(min, Min(max, singleIn)); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum_Controllable typeIn, ref Quaternion singleIn) { ApplyLimits(kvl, (KVariableEnum)typeIn, ref singleIn); }
    public void ApplyLimits(KVariableLimits kvl, KVariableEnum typeIn, ref Quaternion singleIn) {
        switch (typeIn) {
            case KVariableEnum.None: return;
            case KVariableEnum.Variable: MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref singleIn); return;
            case KVariableEnum.Derivative: MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref singleIn); return;
            case KVariableEnum.SecondDerivative: MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref singleIn); return;
            case KVariableEnum.ThirdDerivative: MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref singleIn); return;
            case KVariableEnum.AppliedForce: MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref singleIn); return;
            case KVariableEnum.ImpulseForce: MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref singleIn); return;
            case KVariableEnum.AppliedForceDerivative: MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref singleIn); return;
            case KVariableEnum.ImpulseForceDerivative: MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref singleIn); return;
            default: { Debug.LogError("Unhandled case"); return; }
        }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariable<Quaternion> kvIn) { ApplyLimits(kvl, kvIn.type, ref kvIn.value); }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Quaternion> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Quaternion> kvIn) {
        MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable);
        MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref kvIn.m_derivative);
        MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative);
        MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative);
        MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce);
        MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative);
        MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce);
        MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative);
    }
    public void ApplyLimits(KVariableLimits kvl, KVariables<Quaternion> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
    }
    public void ApplyLimits(KVariableLimits kvl, KVariablesExt<Quaternion> kvIn, KVariableTypeSet kvts) {
        if (kvts.Contains(KVariableEnum.Variable)) { MaxMin(kvl.Max.Variable, kvl.Min.Variable, ref kvIn.m_variable); }
        if (kvts.Contains(KVariableEnum.Derivative)) { MaxMin(kvl.Max.Derivative, kvl.Min.Derivative, ref kvIn.m_derivative); }
        if (kvts.Contains(KVariableEnum.SecondDerivative)) { MaxMin(kvl.Max.SecondDerivative, kvl.Min.SecondDerivative, ref kvIn.m_secondDerivative); }
        if (kvts.Contains(KVariableEnum.ThirdDerivative)) { MaxMin(kvl.Max.ThirdDerivative, kvl.Min.ThirdDerivative, ref kvIn.m_thirdDerivative); }
        if (kvts.Contains(KVariableEnum.AppliedForce)) { MaxMin(kvl.Max.AppliedForce, kvl.Min.AppliedForce, ref kvIn.m_appliedForce); }
        if (kvts.Contains(KVariableEnum.AppliedForceDerivative)) { MaxMin(kvl.Max.AppliedForceDerivative, kvl.Min.AppliedForceDerivative, ref kvIn.m_appliedForceDerivative); }
        if (kvts.Contains(KVariableEnum.ImpulseForce)) { MaxMin(kvl.Max.ImpulseForce, kvl.Min.ImpulseForce, ref kvIn.m_impulseForce); }
        if (kvts.Contains(KVariableEnum.ImpulseForceDerivative)) { MaxMin(kvl.Max.ImpulseForceDerivative, kvl.Min.ImpulseForceDerivative, ref kvIn.m_impulseForceDerivative); }
    }
}
