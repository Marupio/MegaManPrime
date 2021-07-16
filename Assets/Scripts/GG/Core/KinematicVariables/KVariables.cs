using System;
using System.Collections.Generic;
using UnityEngine;

// A value and type together is a KVariable
public struct KVariable<V> {
    public V value;
    public KVariableEnum type;
    public KVariable(KVariable<V> kvIn) {
        value = kvIn.value;
        type = kvIn.type;
    }
    public KVariable(KVariableEnum typeIn, V valueIn) {
        value = valueIn;
        type = typeIn;
    }
    public static KVariable<V> Variable(V value) {
        return new KVariable<V>(KVariableEnum.Variable, value);
    }
    public static KVariable<V> Derivative(V value) {
        return new KVariable<V>(KVariableEnum.Derivative, value);
    }
    public static KVariable<V> SecondDerivative(V value) {
        return new KVariable<V>(KVariableEnum.SecondDerivative, value);
    }
    public static KVariable<V> ThirdDerivative(V value) {
        return new KVariable<V>(KVariableEnum.ThirdDerivative, value);
    }
    public static KVariable<V> AppliedForce(V value) {
        return new KVariable<V>(KVariableEnum.AppliedForce, value);
    }
    public static KVariable<V> ImpulseForce(V value) {
        return new KVariable<V>(KVariableEnum.ImpulseForce, value);
    }
    public static KVariable<V> AppliedForceDerivative(V value) {
        return new KVariable<V>(KVariableEnum.AppliedForceDerivative, value);
    }
    public static KVariable<V> ImpulseForceDerivative(V value) {
        return new KVariable<V>(KVariableEnum.ImpulseForceDerivative, value);
    }
}

// Base class encompasses all controllable variable types
public class KVariables<V> : IEquatable<KVariables<V>> {
    // public ITraits<V> m_traits;
    // public IKVariablesToolset<V> m_toolset;

    // *** Direct access to variables if you need it
    public V m_variable;
    public V m_derivative;
    public V m_secondDerivative;
    public V m_appliedForce;
    public V m_impulseForce;
    
    // *** Access - no checks
    public V Variable { get => m_variable; set => m_variable = value; }
    public V Derivative { get => m_derivative; set => m_derivative = value; }
    public V SecondDerivative { get => m_secondDerivative; set => m_secondDerivative = value; }
    public V AppliedForce { get => m_appliedForce; set => m_appliedForce = value; }
    public V ImpulseForce { get => m_impulseForce; set => m_impulseForce = value; }

    // *** Get functionality
    public virtual void Get(KVariableEnum_Controllable variableEnumC, out V value) {
        switch (variableEnumC) {
            case KVariableEnum_Controllable.Variable:
                value = m_variable;
                break;
            case KVariableEnum_Controllable.Derivative:
                value = m_derivative;
                break;
            case KVariableEnum_Controllable.SecondDerivative:
                value = m_secondDerivative;
                break;
            case KVariableEnum_Controllable.AppliedForce:
                value = m_appliedForce;
                break;
            case KVariableEnum_Controllable.ImpulseForce:
                value = m_impulseForce;
                break;
            case KVariableEnum_Controllable.None:
                // Fail silently
                value = default(V);
                break;
            default:
                Debug.LogError("Unhandled case");
                value = default(V);
                break;
        }
    }
    public virtual void Get(KVariableEnum variableEnum, out V value) {
        switch (variableEnum) {
            case KVariableEnum.Variable:
                value = m_variable;
                break;
            case KVariableEnum.Derivative:
                value = m_derivative;
                break;
            case KVariableEnum.SecondDerivative:
                value = m_secondDerivative;
                break;
            case KVariableEnum.AppliedForce:
                value = m_appliedForce;
                break;
            case KVariableEnum.ImpulseForce:
                value = m_impulseForce;
                break;
            case KVariableEnum.None:
            case KVariableEnum.ThirdDerivative:
            case KVariableEnum.AppliedForceDerivative:
            case KVariableEnum.ImpulseForceDerivative:
                // Fail silently
                value = default(V);
                break;
            default:
                Debug.LogError("Unhandled case");
                value = default(V);
                break;
        }
    }
    public virtual void Get(string variableName, out V value) {
        KVariableEnum variableEnum;
        if (KVariableTypeInfo.Aliases.TryGetValue(variableName, out variableEnum)) {
            Get(variableEnum, out value);
        } else {
            Debug.LogError("Unrecognized variable type string: " + variableName);
            value = default(V);
        }
    }
    public virtual void Get(ref KVariable<V> kvIn) {
        Get(kvIn.type, out kvIn.value);
    }

    // *** Set functionality
    public virtual void Set(KVariableEnum variableEnum, V value) {
        switch (variableEnum) {
            case KVariableEnum.Variable:
                m_variable = value;
                break;
            case KVariableEnum.Derivative:
                m_derivative = value;
                break;
            case KVariableEnum.SecondDerivative:
                m_secondDerivative = value;
                break;
            case KVariableEnum.AppliedForce:
                m_appliedForce = value;
                break;
            case KVariableEnum.ImpulseForce:
                m_impulseForce = value;
                break;
            case KVariableEnum.None:
            case KVariableEnum.ThirdDerivative:
            case KVariableEnum.AppliedForceDerivative:
            case KVariableEnum.ImpulseForceDerivative:
                // Fail silently
                break;
        }
    }
    public virtual void Set(string variableName, V value) {
        KVariableEnum variableEnum;
        if (KVariableTypeInfo.Aliases.TryGetValue(variableName, out variableEnum)) {
            Set(variableEnum, value);
        } else {
            Debug.LogError("Unrecognized variable type string: " + variableName);
        }
    }
    public virtual void Set(KVariable<V> kvIn) {
        Set(kvIn.type, kvIn.value);
    }

    public virtual void SetEqual(KVariables<V> varIn) {
        m_variable = varIn.m_variable;
        m_derivative = varIn.m_derivative;
        m_secondDerivative = varIn.m_derivative;
        m_appliedForce = varIn.m_appliedForce;
        m_impulseForce = varIn.m_impulseForce;
    }

    // *** Indexer
    public V this[int index] {
        get {
            V value;
            Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);
            return value;
        }
        set {
            Set(KVariableTypeInfo.IndexToKVariableEnum(index), value);
        }
    }
    public V this[string elem] {
        get {
            V value;
            Get(KVariableTypeInfo.StringToKVariableEnum(elem), out value);
            return value;
        }
        set {
            Set(KVariableTypeInfo.StringToKVariableEnum(elem), value);
        }
    }
    public virtual int Size() {
        return KVariableTypeInfo.NKVariableEnum_Controllable;
    }
    public bool Equals(KVariables<V> kv)
    {
        EqualityComparer<V> ec = EqualityComparer<V>.Default; /* return ec.Equals(lhs, rhs); */
        return
            ec.Equals(kv.m_variable,         m_variable) &&
            ec.Equals(kv.m_derivative,       m_derivative) &&
            ec.Equals(kv.m_secondDerivative, m_secondDerivative) &&
            ec.Equals(kv.m_appliedForce,     m_appliedForce) &&
            ec.Equals(kv.m_impulseForce,     m_impulseForce);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is KVariables<V>))
           return false;

        KVariables<V> kv = (KVariables<V>)obj;
        if (kv == this) {
            return true;
        }
        return false;
    }
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            hash = hash * 486187739 + m_variable.GetHashCode();
            hash = hash * 486187739 + m_derivative.GetHashCode();
            hash = hash * 486187739 + m_secondDerivative.GetHashCode();
            hash = hash * 486187739 + m_appliedForce.GetHashCode();
            hash = hash * 486187739 + m_impulseForce.GetHashCode();
            return hash;
        }
    }

    // *** Constructors
    public KVariables(KVariables<V> kvIn) {
        m_variable = kvIn.m_variable;
        m_derivative = kvIn.m_derivative;
        m_secondDerivative = kvIn.m_secondDerivative;
        m_appliedForce = kvIn.m_appliedForce;
        m_impulseForce = kvIn.m_impulseForce;
    }
    public KVariables(
        V variable,
        V derivative,
        V secondDerivative,
        V appliedForce,
        V impulseForce
    ) {
        m_variable = variable;
        m_derivative = derivative;
        m_secondDerivative = secondDerivative;
        m_appliedForce = appliedForce;
        m_impulseForce = impulseForce;
    }
    public KVariables(V allNonForces, V allForces) {
        m_variable = allNonForces;
        m_derivative = allNonForces;
        m_secondDerivative = allNonForces;
        m_appliedForce = allForces;
        m_impulseForce = allForces;
    }
    public KVariables(V allVars) {
        m_variable = allVars;
        m_derivative = allVars;
        m_secondDerivative = allVars;
        m_appliedForce = allVars;
        m_impulseForce = allVars;
    }
    public KVariables() {}
}

// Extended class adds the remaining variable types in KVariableEnum that aren't already included in base.
public class KVariablesExt<V> : KVariables<V>, IEquatable<KVariablesExt<V>> {
    // *** Direct access to variables if you need it
    public V m_thirdDerivative;
    public V m_appliedForceDerivative;
    public V m_impulseForceDerivative;

    public V ThirdDerivative { get => m_thirdDerivative; set => m_thirdDerivative = value; }
    public V AppliedForceDerivative { get => m_appliedForceDerivative; set => m_appliedForceDerivative = value; }
    public V ImpulseForceDerivative { get => m_impulseForceDerivative; set => m_impulseForceDerivative = value; }

    // *** Get functionality
    public override void Get(KVariableEnum variableEnum, out V value) {
        switch (variableEnum) {
            case KVariableEnum.Variable:
                value = m_variable;
                break;
            case KVariableEnum.Derivative:
                value = m_derivative;
                break;
            case KVariableEnum.SecondDerivative:
                value = m_secondDerivative;
                break;
            case KVariableEnum.ThirdDerivative:
                value = m_thirdDerivative;
                break;
            case KVariableEnum.AppliedForce:
                value = m_appliedForce;
                break;
            case KVariableEnum.ImpulseForce:
                value = m_impulseForce;
                break;
            case KVariableEnum.AppliedForceDerivative:
                value = m_appliedForceDerivative;
                break;
            case KVariableEnum.ImpulseForceDerivative:
                value = m_impulseForceDerivative;
                break;
            case KVariableEnum.None:
            default:
                Debug.LogError("Unhandled case");
                value = default(V);
                break;
        }
    }
    public override void Get(ref KVariable<V> kvIn) {
        Get(kvIn.type, out kvIn.value);
    }
    // Reveal shadows
    public override void Get(KVariableEnum_Controllable variableEnumC, out V value) { base.Get(variableEnumC, out value); }
    public override void Get(string variableName, out V value) { base.Get(variableName, out value); }

    // *** Set functionality
    public override void Set(KVariableEnum variableEnum, V value) {
        switch (variableEnum) {
            case KVariableEnum.Variable:
                value = m_variable;
                break;
            case KVariableEnum.Derivative:
                value = m_derivative;
                break;
            case KVariableEnum.SecondDerivative:
                value = m_secondDerivative;
                break;
            case KVariableEnum.ThirdDerivative:
                value = m_thirdDerivative;
                break;
            case KVariableEnum.AppliedForce:
                value = m_appliedForce;
                break;
            case KVariableEnum.ImpulseForce:
                value = m_impulseForce;
                break;
            case KVariableEnum.AppliedForceDerivative:
                value = m_appliedForceDerivative;
                break;
            case KVariableEnum.ImpulseForceDerivative:
                value = m_impulseForceDerivative;
                break;
            case KVariableEnum.None:
            default:
                Debug.LogError("Unhandled case");
                break;
        }
    }
    public override void Set(KVariable<V> kvIn) {
        Set(kvIn.type, kvIn.value);
    }
    // Reveal shadows
    public override void Set(string variableName, V value) { base.Set(variableName, value); }

    public override int Size() {
        return KVariableTypeInfo.NKVariableEnums;
    }
    public virtual void SetEqual(KVariablesExt<V> varIn) {
        base.SetEqual(varIn);
        m_thirdDerivative = varIn.m_thirdDerivative;
        m_appliedForceDerivative = varIn.m_appliedForceDerivative;
        m_impulseForceDerivative = varIn.m_impulseForceDerivative;
    }
    public bool Equals(KVariablesExt<V> kv)
    {
        EqualityComparer<V> ec = EqualityComparer<V>.Default; /* return ec.Equals(lhs, rhs); */
        return
            ec.Equals(kv.m_variable,               m_variable) &&
            ec.Equals(kv.m_derivative,             m_derivative) &&
            ec.Equals(kv.m_secondDerivative,       m_secondDerivative) &&
            ec.Equals(kv.m_appliedForce,           m_appliedForce) &&
            ec.Equals(kv.m_impulseForce,           m_impulseForce) &&
            ec.Equals(kv.m_thirdDerivative,        m_thirdDerivative) &&
            ec.Equals(kv.m_appliedForceDerivative, m_appliedForceDerivative) &&
            ec.Equals(kv.m_impulseForceDerivative, m_impulseForceDerivative);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is KVariablesExt<V>))
           return false;

        KVariablesExt<V> kv = (KVariablesExt<V>)obj;
        if (kv == this) {
            return true;
        }
        return false;
    }
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            hash = hash * 486187739 + m_variable.GetHashCode();
            hash = hash * 486187739 + m_derivative.GetHashCode();
            hash = hash * 486187739 + m_secondDerivative.GetHashCode();
            hash = hash * 486187739 + m_appliedForce.GetHashCode();
            hash = hash * 486187739 + m_impulseForce.GetHashCode();
            hash = hash * 486187739 + m_thirdDerivative.GetHashCode();
            hash = hash * 486187739 + m_appliedForceDerivative.GetHashCode();
            hash = hash * 486187739 + m_impulseForceDerivative.GetHashCode();
            return hash;
        }
    }

    // *** Constructors
    public KVariablesExt(KVariables<V> kvIn, V others)
    : base (kvIn) {
        m_thirdDerivative = others;
        m_appliedForceDerivative = others;
        m_impulseForceDerivative = others;
    }
    public KVariablesExt(KVariablesExt<V> kvIn)
    : base (kvIn) {
        m_thirdDerivative = kvIn.m_thirdDerivative;
        m_appliedForceDerivative = kvIn.m_appliedForceDerivative;
        m_impulseForceDerivative = kvIn.m_impulseForceDerivative;
    }
    public KVariablesExt(
        V variable,
        V derivative,
        V secondDerivative,
        V thirdDerivative,
        V appliedForce,
        V impulseForce,
        V appliedForceDerivative,
        V impulseForceDerivative
    ) : base(variable, derivative, secondDerivative, appliedForce, impulseForce) {
        m_thirdDerivative = thirdDerivative;
        m_appliedForceDerivative = appliedForceDerivative;
        m_impulseForceDerivative = impulseForceDerivative;
    }
    public KVariablesExt(V allNonForces, V allForces)
    : base(allNonForces, allForces) {
        m_thirdDerivative = allNonForces;
        m_appliedForceDerivative = allForces;
        m_impulseForceDerivative = allForces;
    }
    public KVariablesExt(V allVars)
    : base(allVars) {
        m_thirdDerivative = allVars;
        m_appliedForceDerivative = allVars;
        m_impulseForceDerivative = allVars;
    }
    public KVariablesExt() {}
}
