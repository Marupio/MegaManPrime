using System.Collections.Generic;
using UnityEngine;

public class KVariables<V> {
    protected V m_variable;
    protected V m_derivative;
    protected V m_secondDerivative;
    protected V m_appliedForce;
    protected V m_impulseForce;

    // *** Access
    public V Variable { get => m_variable; set => m_variable = value; }
    public V Derivative { get => m_derivative; set => m_derivative = value; }
    public V SecondDerivative { get => m_secondDerivative; set => m_secondDerivative = value; }
    public V AppliedForce { get => m_appliedForce; set => m_appliedForce = value; }
    public V ImpulseForce { get => m_impulseForce; set => m_impulseForce = value; }

    // *** Get functionality
    public virtual void Get(string variableName, out V value) {
        KVariableEnum variableEnum;
        if (KVariableTypeInfo.Aliases.TryGetValue(variableName, out variableEnum)) {
            Get(variableEnum, out value);
        } else {
            Debug.LogError("Unrecognized variable type string: " + variableName);
            value = default(V);
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
            case KVariableEnum.Drag:
                Debug.LogError("Attempting to get drag with wrong type");
                value = default(V);
                break;
            default:
                Debug.LogError("Unhandled case");
                value = default(V);
                break;
        }
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
            case KVariableEnum.Drag:
                Debug.LogError("Attempting to set drag with wrong type");
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
    public virtual void SetEqual(KVariables<V> varIn) {
        m_variable = varIn.m_variable;
        m_derivative = varIn.m_derivative;
        m_secondDerivative = varIn.m_derivative;
        m_appliedForce = varIn.m_appliedForce;
        m_impulseForce = varIn.m_impulseForce;
    }

    // *** Constructors
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
    public KVariables(V allVars) {
        m_variable = allVars;
        m_derivative = allVars;
        m_secondDerivative = allVars;
        m_appliedForce = allVars;
        m_impulseForce = allVars;
    }
    public KVariables() {}
}

public class KVariablesExt<V> : KVariables<V> {
    protected V m_thirdDerivative;
    protected V m_appliedForceDerivative;
    protected V m_impulseForceDerivative;

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
            case KVariableEnum.Drag:
                Debug.LogWarning("Attempting to get " + variableEnum + " from KVariableExtendedSet");
                value = default(V);
                break;
            default:
                Debug.LogError("Unhandled case");
                value = default(V);
                break;
        }
    }
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
            case KVariableEnum.Drag:
                Debug.LogWarning("Attempting to get " + variableEnum + " from KVariableExtendedSet");
                break;
            default:
                Debug.LogError("Unhandled case");
                break;
        }
    }

    // *** Constructors
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
    public KVariablesExt(V allVars)
    : base(allVars) {
        m_thirdDerivative = allVars;
        m_appliedForceDerivative = allVars;
        m_impulseForceDerivative = allVars;
    }
    public KVariablesExt() {}
}
