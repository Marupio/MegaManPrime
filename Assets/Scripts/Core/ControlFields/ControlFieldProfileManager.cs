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

// 3D : Q = Quaternion, V = Vector3, T = Vector3
// 2D : Q = float ,     V = Vector2, T = float
public class ControlFieldProfileManager<Q, V, T> {
    MovementController<Q, V, T> m_entity;
    string m_name; // Name of associated entity's GameObject
    Dictionary<string, AxisStoredAt> m_axisIndex;
    Dictionary<string, ControlFieldProfile<float, V>> m_axes1D;
    Dictionary<string, ControlFieldProfile<Vector2, V>> m_axes2D;
    Dictionary<string, ControlFieldProfile<Vector3, V>> m_axes3D;
    List<ControlFieldProfile<float, V>> m_activeAxes1D;
    List<ControlFieldProfile<Vector2, V>> m_activeAxes2D;
    List<ControlFieldProfile<Vector3, V>> m_activeAxes3D;

    // *** Access
    public Dictionary<string, AxisStoredAt> AxisIndex { get => m_axisIndex; }
    public Dictionary<string, ControlFieldProfile<float, V>> Axes1D { get => m_axes1D; }
    public Dictionary<string, ControlFieldProfile<Vector2, V>> Axes2D { get => m_axes2D; }
    public Dictionary<string, ControlFieldProfile<Vector3, V>> Axes3D { get => m_axes3D; }
    public List<ControlFieldProfile<float, V>> ActiveAxes1D { get => m_activeAxes1D; }
    public List<ControlFieldProfile<Vector2, V>> ActiveAxes2D { get => m_activeAxes2D; }
    public List<ControlFieldProfile<Vector3, V>> ActiveAxes3D { get => m_activeAxes3D; }

    public bool AddAxis(ControlFieldProfile<float, V> newAxis, bool makeActive, bool overwrite = true) {
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
    public bool AddAxis(ControlFieldProfile<Vector2, V> newAxis, bool makeActive, bool overwrite = true) {
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
    public bool AddAxis(ControlFieldProfile<Vector3, V> newAxis, bool makeActive, bool overwrite = true) {
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
    public void RemoveAllAxes() {
        m_axes1D.Clear();
        m_axes2D.Clear();
        m_axes3D.Clear();
        m_activeAxes1D.Clear();
        m_activeAxes2D.Clear();
        m_activeAxes3D.Clear();
        m_axisIndex.Clear();
    }
    public bool ActivateAxis(string name) {
        AxisStoredAt storedAt;
        if (!m_axisIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case AxisStoredAt.Dict1D: {
                ControlFieldProfile<float, V> activeAxis = m_axes1D[name];
                m_activeAxes1D.Add(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Active1D);
                return true;
            }
            case AxisStoredAt.Dict2D: {
                ControlFieldProfile<Vector2, V> activeAxis = m_axes2D[name];
                m_activeAxes2D.Add(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Active2D);
                return true;
            }
            case AxisStoredAt.Dict3D: {
                ControlFieldProfile<Vector3, V> activeAxis = m_axes3D[name];
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
                ControlFieldProfile<float, V> activeAxis = m_axes1D[name];
                m_activeAxes1D.Remove(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Dict1D);
                return true;
            }
            case AxisStoredAt.Active2D: {
                ControlFieldProfile<Vector2, V> activeAxis = m_axes2D[name];
                m_activeAxes2D.Remove(activeAxis);
                m_axisIndex.Add(name, AxisStoredAt.Dict1D);
                return true;
            }
            case AxisStoredAt.Active3D: {
                ControlFieldProfile<Vector3, V> activeAxis = m_axes3D[name];
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

    // *** Internal functions
    bool CheckSetup() {
        int nFreedoms = m_entity.NSpatialFreedoms + m_entity.NRotationalFreedoms;
        int nControls = m_activeAxes1D.Count + 2*m_activeAxes2D.Count + 3*m_activeAxes3D.Count;
        // This check is not informative, because some control axes may be ForceUsers, which can overlap with other ForceUsers and one StateSetters
        // if (nFreedoms - nControls < 0) {
        //     Debug.LogError("Overconstrained system: " + nFreedoms + " freedoms, " + nControls + " controls.");
        //     return false;
        // }
        Vector3Int fixedSpatial = Vector3Int.zero;
        Vector3Int fixedRotational = Vector3Int.zero;
        int nSpatial = m_entity.NSpatialFreedoms;
        int nRotational = m_entity.NRotationalFreedoms;
        foreach (ControlFieldProfile<float, V> axis in m_activeAxes1D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Control.StateSetter()) {
                if (axis.Type == AxisType.Spatial) {
                    nSpatial -= axis.NControlledDimensions;
                } else if (axis.Type == AxisType.Rotational) {
                    nRotational -= axis.NControlledDimensions;
                }
            }
        }
        foreach (ControlFieldProfile<Vector2, V> axis in m_activeAxes2D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Control.StateSetter()) {
                if (axis.Type == AxisType.Spatial) {
                    nSpatial -= axis.NControlledDimensions;
                } else if (axis.Type == AxisType.Rotational) {
                    nRotational -= axis.NControlledDimensions;
                }
            }
        }
        foreach (ControlFieldProfile<Vector3, V> axis in m_activeAxes3D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Control.StateSetter()) {
                if (axis.Type == AxisType.Spatial) {
                    nSpatial -= axis.NControlledDimensions;
                } else if (axis.Type == AxisType.Rotational) {
                    nRotational -= axis.NControlledDimensions;
                }
            }
        }
        // Now check results of summations
        bool pass = true;
        if (nSpatial < 0) {
            Debug.LogError("Spatially overconstrained with " + nSpatial + " too many spatial axes controlled by StateSetter types.");
            pass = false;
        }
        if (nRotational < 0) {
            Debug.LogError("Rotationally overconstrained with " + nRotational + " too many rotational axes controlled by StateSetter types.");
            pass = false;
        }
        bool passSpace = true;
        bool passRotate = true;
        for (int i = 0; i < 3; ++i) {
            if (fixedSpatial[i] > 1) { passSpace = false; }
            if (fixedRotational[i] > 1) { passRotate = false;}
        }
        if (passSpace && passRotate) {
            return pass;
        }
        string spaceFail = "";
        string rotateFail = "";
        if (!passSpace) {
            spaceFail = " Spatial assignment = " + fixedSpatial;
        }
        if (!passRotate) {
            rotateFail = " Rotation assignment = " + fixedRotational;
        }
        Debug.Log("Number of StateSetter type axis controllers aligned to each axis cannot exceed 1." + spaceFail + rotateFail);
        return false;
    }

    // *** Constructors
    ControlFieldProfileManager(MovementController<Q,V,T> entity, string name) {
        m_entity = entity;
        m_name = name;
    }
}
