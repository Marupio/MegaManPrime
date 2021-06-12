using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// I turn a controlValue input into a target position/velocity/acceleration
/// </summary>
public abstract class AxisMovement<T> : KinematicLimits {
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
    public abstract KinematicVariables IndependentVariable { get; }
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
        ref KinematicVariables involvedVariables
    );

    protected AxisMovement(KinematicLimits limits, InputRange<T> inputRange)
        : base (limits) {
        m_inputRange = inputRange;
    }
    // protected InputRange<T> m_inputRange;
    // Dictionary<string, AxisSource> m_axisSources;
    // bool m_smoothingEnabled;
    // float m_smoothingTime;
}


public class UncontrolledAxisMovement<T> : AxisMovement<T>
{
    public override void ApplyControlValue(T value) { /* Do nothing */ }
    public override KinematicVariables IndependentVariable { get => KinematicVariables.None; }
    public override T Target => throw new System.NotImplementedException();
    public UncontrolledAxisMovement(KinematicLimits limits) : base(limits, null) {}
}


public class ControlledAxisMovement<T> : AxisMovement<T>
{
    KinematicVariables m_kinematicVariable;
    public override KinematicVariables IndependentVariable { get => m_kinematicVariable; }
    public override T Target { get { return m_inputRange.InputValue; } }
    public ControlledAxisMovement(KinematicLimits limits, InputRange<T> inputRange, KinematicVariables kinematicVariable)
        : base(limits, inputRange)
    {
        m_kinematicVariable = kinematicVariable;
    }
}
