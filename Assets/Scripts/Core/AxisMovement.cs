using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// I turn a controlValue input into a target position/velocity/acceleration
/// </summary>
public abstract class AxisMovement<T> : KVariableLimits {
    // WARNING - m_inputRange is null in some classes
    protected InputRange<T> m_inputRange;
    Dictionary<string, AxisSource> m_axisSources;
    bool m_smoothingEnabled;
    float m_smoothingTime;

    /// <summary>
    /// Change the control value
    /// </summary>
    public virtual void ApplyControlValue(T value) {
        m_inputRange.ControlValue = value;
    }

    public AxisSource GetSource(string name) { return m_axisSources[name]; }
    /// <summary>
    /// Add a new axis source, by name
    /// </summary>
    /// <returns>True if source already existed and was overwritten</returns>
    public bool AddSource(string name, AxisSource newSource) {
        bool overwritten = false;
        if (m_axisSources.ContainsKey(name)) {
            overwritten = true;
        }
        m_axisSources.Add(name, newSource);
        return overwritten;
    }
    /// <summary>
    /// Remove an axis source, by name
    /// </summary>
    /// <returns>True if the source existed and was removed</returns>
    public bool RemoveSource(string name) {
        if (m_axisSources.ContainsKey(name)) {
            m_axisSources.Remove(name);
            return true;
        }
        return false;
    }
    public int RemoveAllSources() {
        int nRemoved = m_axisSources.Count;
        m_axisSources.Clear();
        return nRemoved;
    }

    /// <summary>
    /// Returns the 'ImpulseMovement' class for this, if it is one.  ImpulseMovement is the base class for impulse axis movement types.
    /// </summary>
    /// <returns></returns>
    public virtual ImpulseMovement<T> ImpulseType() {
        if (this is ImpulseMovement<T>) {
            return (ImpulseMovement<T>)this;
        } else {
            return null;
        }
    }
    public abstract KVariableTypeSet IndependentVariable { get; }
    public abstract T Target { get; }
    /// <summary>
    /// Perform kinematic calculations on provided variables
    /// </summary>
    public abstract void Update
    (
        ref T position,
        ref T velocity,
        ref T acceleration,
        ref T appliedForce,
        ref T instantaneousForce,
        ref KVariableTypeSet involvedVariables,
        Dictionary<string, AxisSource> sources
    );

    public bool SmoothingEnabled
    {
        get => m_smoothingEnabled;
        set { m_smoothingEnabled = InternalSmoothingAllowed() ? value : false; }
    }
    public float SmoothingTime
    {
        get => m_smoothingTime;
        set { m_smoothingTime = InternalSmoothingAllowed() ? value : 0;}
    }
    protected AxisMovement(KVariableLimits limits, InputRange<T> inputRange)
        : base (limits) {
        m_inputRange = inputRange;
    }
    // protected InputRange<T> m_inputRange;
    // Dictionary<string, AxisSource> m_axisSources;
    // bool m_smoothingEnabled;
    // float m_smoothingTime;

    /// <summary>
    /// Enforces no smoothing for Instantaneous movement control
    /// </summary>
    protected bool InternalSmoothingAllowed()
    {
        ImpulseMovement<T> impulseType = ImpulseType();
        if (impulseType != null && impulseType.Instantaneous)
        {
            return false;
        }
        return true;
    }
}


public class UncontrolledAxisMovement<T> : AxisMovement<T>
{
    public override void ApplyControlValue(T value) { /* Do nothing */ }
    public override KVariableTypeSet IndependentVariable { get => KVariableTypeSet.None; }
    public override T Target => throw new System.NotImplementedException();
    public UncontrolledAxisMovement(KVariableLimits limits) : base(limits, null) {}
}


public class ControlledAxisMovement<T> : AxisMovement<T>
{
    KVariableTypeSet m_kinematicVariable;
    public override KVariableTypeSet IndependentVariable { get => m_kinematicVariable; }
    public override T Target { get { return m_inputRange.InputValue; } }
    public ControlledAxisMovement(KVariableLimits limits, InputRange<T> inputRange, KVariableTypeSet kinematicVariable)
        : base(limits, inputRange)
    {
        m_kinematicVariable = kinematicVariable;
    }
}
