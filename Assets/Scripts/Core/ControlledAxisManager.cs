using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AxisStoredAt {
    Dict1D,
    Dict2D,
    Dict3D,
    Active1D,
    Active2D,
    Active3D
}

// 2D : V = Vector3
// 3D : V = Vector2
public class ControlledAxisManager<V> {

    string m_name; // Name of associated entity's GameObject
    Dictionary<string, AxisStoredAt> m_axisIndex;
    Dictionary<string, AxisProfile<float, V>> m_axes1D;
    Dictionary<string, AxisProfile<Vector2, V>> m_axes2D;
    Dictionary<string, AxisProfile<Vector3, V>> m_axes3D;
    List<AxisProfile<float, V>> m_activeAxes1D;
    List<AxisProfile<Vector2, V>> m_activeAxes2D;
    List<AxisProfile<Vector3, V>> m_activeAxes3D;

    // *** Access
    public Dictionary<string, AxisStoredAt> AxisIndex { get => m_axisIndex; }
    public Dictionary<string, AxisProfile<float, V>> Axes1D { get => m_axes1D; }
    public Dictionary<string, AxisProfile<Vector2, V>> Axes2D { get => m_axes2D; }
    public Dictionary<string, AxisProfile<Vector3, V>> Axes3D { get => m_axes3D; }
    public List<AxisProfile<float, V>> ActiveAxes1D { get => m_activeAxes1D; }
    public List<AxisProfile<Vector2, V>> ActiveAxes2D { get => m_activeAxes2D; }
    public List<AxisProfile<Vector3, V>> ActiveAxes3D { get => m_activeAxes3D; }

    public bool AddAxis(AxisProfile<float, V> newAxis, bool makeActive, bool overwrite) {
        if (!overwrite && m_axisIndex.ContainsKey(newAxis.Name)) {
            Debug.LogError("AxisManager " + m_name + " - axis name collision: " + newAxis.Name);
            return false;
        }
        AxisStoredAt asa = AxisStoredAt.Dict1D;
        if (makeActive) {
            m_activeAxes1D.Add(newAxis);
            asa = AxisStoredAt.Active1D;
        }
        m_axisIndex.Add(newAxis.Name, asa);
        return true;
    }
    public bool AddAxis(AxisProfile<Vector2, V> newAxis, bool makeActive, bool overwrite) {
        if (!overwrite && m_axisIndex.ContainsKey(newAxis.Name)) {
            Debug.LogError("AxisManager " + m_name + " - axis name collision: " + newAxis.Name);
            return false;
        }
        AxisStoredAt asa = AxisStoredAt.Dict2D;
        if (makeActive) {
            m_activeAxes2D.Add(newAxis);
            asa = AxisStoredAt.Active2D;
        }
        m_axisIndex.Add(newAxis.Name, asa);
        return true;
    }
    public bool AddAxis(AxisProfile<Vector3, V> newAxis, bool makeActive, bool overwrite) {
        if (!overwrite && m_axisIndex.ContainsKey(newAxis.Name)) {
            Debug.LogError("AxisManager " + m_name + " - axis name collision: " + newAxis.Name);
            return false;
        }
        AxisStoredAt asa = AxisStoredAt.Dict3D;
        if (makeActive) {
            m_activeAxes3D.Add(newAxis);
            asa = AxisStoredAt.Active3D;
        }
        m_axisIndex.Add(newAxis.Name, asa);
        return true;
    }
    public bool RemoveAxis(string name) {
        AxisStoredAt storedAt;
        if (!m_axisIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case AxisStoredAt.Dict1D:
                m_axes1D.Remove(name);
                break;
            case AxisStoredAt.Dict2D:
                m_axes2D.Remove(name);
                break;
            case AxisStoredAt.Dict3D:
                m_axes3D.Remove(name);
                break;
            case AxisStoredAt.Active1D:
                m_axes1D.Remove(name);
                m_activeAxes1D.RemoveAll(axis => axis.Name == name);
                break;
            case AxisStoredAt.Active2D:
                m_axes2D.Remove(name);
                m_activeAxes2D.RemoveAll(axis => axis.Name == name);
                break;
            case AxisStoredAt.Active3D:
                m_axes3D.Remove(name);
                m_activeAxes3D.RemoveAll(axis => axis.Name == name);
                break;
        }
        return true;
    }
    public bool ActivateAxis(string name) {
        AxisStoredAt storedAt;
        if (!m_axisIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case AxisStoredAt.Dict1D: {
                AxisProfile<float, V> activeAxis = m_axes1D[name];
                m_activeAxes1D.Add(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Active1D);
                return true;
            }
            case AxisStoredAt.Dict2D: {
                AxisProfile<Vector2, V> activeAxis = m_axes2D[name];
                m_activeAxes2D.Add(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Active2D);
                return true;
            }
            case AxisStoredAt.Dict3D: {
                AxisProfile<Vector3, V> activeAxis = m_axes3D[name];
                m_activeAxes3D.Add(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Active3D);
                return true;
            }
            case AxisStoredAt.Active1D: {
                return true;
            }
            case AxisStoredAt.Active2D: {
                return true;
            }
            case AxisStoredAt.Active3D: {
                return true;
            }
            default: {
                Debug.LogError("Unhandled case");
                return false;
            }
        }
    }
    public bool DeactivateAxis(string name) {
        AxisStoredAt storedAt;
        if (!m_axisIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case AxisStoredAt.Dict1D: {
                return true;
            }
            case AxisStoredAt.Dict2D: {
                return true;
            }
            case AxisStoredAt.Dict3D: {
                return true;
            }
            case AxisStoredAt.Active1D: {
                AxisProfile<float, V> activeAxis = m_axes1D[name];
                m_activeAxes1D.Remove(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Dict1D);
                return true;
            }
            case AxisStoredAt.Active2D: {
                AxisProfile<Vector2, V> activeAxis = m_axes2D[name];
                m_activeAxes2D.Remove(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Dict1D);
                return true;
            }
            case AxisStoredAt.Active3D: {
                AxisProfile<Vector3, V> activeAxis = m_axes3D[name];
                m_activeAxes3D.Remove(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Dict1D);
                return true;
            }
            default: {
                Debug.LogError("Unhandled case");
                return false;
            }
        }
    }

    // *** Constructors
    ControlledAxisManager(string name) {
        m_name = name;
    }
}