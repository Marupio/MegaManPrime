using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ContlolFieldStoredAt {
    Dict1D,
    Dict2D,
    Dict3D,
    Active1D,
    Active2D,
    Active3D
}

public interface IControlFieldProfileManager {
    public string GameObjectName { get; set; }
    public int SpatialDimensions { get; }
    public int RotationalDimensions { get; }
    public int NControlFields { get; }
    public Dictionary<string, ContlolFieldStoredAt> ControlFieldIndex { get; }
}

// 3D : Q = Quaternion, V = Vector3, T = Vector3
// 2D : Q = float ,     V = Vector2, T = float
public class ControlFieldProfileManager<Q, V, T> : IControlFieldProfileManager {
    protected MovementController<Q, V, T> m_entity;
    protected string m_gameObjectName; // Name of associated entity's GameObject
    protected int m_spatialDimensions;
    protected int m_rotationalDimensions;
    protected KVariableLimits m_limits_controlManagerLevel; // Limits applying to all controlFieldProfiles under this manager
    protected KVariableLimits m_limits_cachedArgument; // Cache of last kvl supplied to ApplyLimit call
    protected KVariableLimits m_limits_cache;
    protected int m_limits_cacheUtdf; // UpToDateFrame for m_limits_cache
    protected KvLimiter<float> m_limiterFloat;
    protected KvLimiter<Vector2> m_limiterVector2;
    protected KvLimiter<Vector3> m_limiterVector3;
    protected Dictionary<string, ContlolFieldStoredAt> m_controlFieldIndex;
    protected Dictionary<string, ControlFieldProfile<float>> m_controlFields1D;
    protected Dictionary<string, ControlFieldProfile<Vector2>> m_controlFields2D;
    protected Dictionary<string, ControlFieldProfile<Vector3>> m_controlFields3D;
    protected List<ControlFieldProfile<float>> m_activeControlFields1D;
    protected List<ControlFieldProfile<Vector2>> m_activeControlFields2D;
    protected List<ControlFieldProfile<Vector3>> m_activeControlFields3D;

    // *** Access
    public string GameObjectName { get=>m_gameObjectName; set=>m_gameObjectName=value; }
    public int SpatialDimensions { get=>m_spatialDimensions; }
    public int RotationalDimensions { get=>m_rotationalDimensions; }
    public int NControlFields { get=>ControlFieldIndex.Count; }
    public Dictionary<string, ContlolFieldStoredAt> ControlFieldIndex { get => m_controlFieldIndex; }
    public Dictionary<string, ControlFieldProfile<float>> ControlFields1D { get => m_controlFields1D; }
    public Dictionary<string, ControlFieldProfile<Vector2>> ControlFields2D { get => m_controlFields2D; }
    public Dictionary<string, ControlFieldProfile<Vector3>> ControlFields3D { get => m_controlFields3D; }
    public List<ControlFieldProfile<float>> ActiveControlFields1D { get => m_activeControlFields1D; }
    public List<ControlFieldProfile<Vector2>> ActiveControlFields2D { get => m_activeControlFields2D; }
    public List<ControlFieldProfile<Vector3>> ActiveControlFields3D { get => m_activeControlFields3D; }

    // *** Edit
    public bool AddControlField(ControlFieldProfile<float> newControlField, bool makeActive, bool overwrite = true) {
        if (!overwrite && m_controlFieldIndex.ContainsKey(newControlField.Name)) {
            Debug.LogError("ControlFieldManager " + m_gameObjectName + " - controlField name collision: " + newControlField.Name);
            return false;
        }
        ContlolFieldStoredAt asa = ContlolFieldStoredAt.Dict1D;
        if (makeActive) {
            m_activeControlFields1D.Add(newControlField);
            asa = ContlolFieldStoredAt.Active1D;
            newControlField.InternalActivate();
        }
        m_controlFields1D.Add(newControlField.Name, newControlField);
        m_controlFieldIndex.Add(newControlField.Name, asa);
        newControlField.InternalSetManager(this);
        int nDimsCompare = newControlField.Type == ControlFieldType.Rotational ? m_rotationalDimensions : m_spatialDimensions;
        if (newControlField.NAvailableDimensions != nDimsCompare) {
            Debug.LogError(
                "nDimensions mismatch, newControlField " + newControlField.Name + " expects " + newControlField.NAvailableDimensions +
                " available dimensions, but there are " + nDimsCompare + " " + newControlField.Type + " dimensions"
            );
            return false;
        }
        return true;
    }
    public bool AddControlField(ControlFieldProfile<Vector2> newControlField, bool makeActive, bool overwrite = true) {
        if (!overwrite && m_controlFieldIndex.ContainsKey(newControlField.Name)) {
            Debug.LogError("ControlFieldManager " + m_gameObjectName + " - controlField name collision: " + newControlField.Name);
            return false;
        }
        ContlolFieldStoredAt asa = ContlolFieldStoredAt.Dict2D;
        if (makeActive) {
            m_activeControlFields2D.Add(newControlField);
            asa = ContlolFieldStoredAt.Active2D;
            newControlField.InternalActivate();
        }
        m_controlFields2D.Add(newControlField.Name, newControlField);
        m_controlFieldIndex.Add(newControlField.Name, asa);
        newControlField.InternalSetManager(this);
        int nDimsCompare = newControlField.Type == ControlFieldType.Rotational ? m_rotationalDimensions : m_spatialDimensions;
        if (newControlField.NAvailableDimensions != nDimsCompare) {
            Debug.LogError(
                "nDimensions mismatch, newControlField " + newControlField.Name + " expects " + newControlField.NAvailableDimensions +
                " available dimensions, but there are " + nDimsCompare + " " + newControlField.Type + " dimensions"
            );
            return false;
        }
        return true;
    }
    public bool AddControlField(ControlFieldProfile<Vector3> newControlField, bool makeActive, bool overwrite = true) {
        if (!overwrite && m_controlFieldIndex.ContainsKey(newControlField.Name)) {
            Debug.LogError("ControlFieldManager " + m_gameObjectName + " - controlField name collision: " + newControlField.Name);
            return false;
        }
        ContlolFieldStoredAt asa = ContlolFieldStoredAt.Dict3D;
        if (makeActive) {
            m_activeControlFields3D.Add(newControlField);
            asa = ContlolFieldStoredAt.Active3D;
            newControlField.InternalActivate();
        }
        m_controlFields3D.Add(newControlField.Name, newControlField);
        m_controlFieldIndex.Add(newControlField.Name, asa);
        newControlField.InternalSetManager(this);
        int nDimsCompare = newControlField.Type == ControlFieldType.Rotational ? m_rotationalDimensions : m_spatialDimensions;
        if (newControlField.NAvailableDimensions != nDimsCompare) {
            Debug.LogError(
                "nDimensions mismatch, newControlField " + newControlField.Name + " expects " + newControlField.NAvailableDimensions +
                " available dimensions, but there are " + nDimsCompare + " " + newControlField.Type + " dimensions"
            );
            return false;
        }
        return true;
    }
    public bool RemoveControlField(string name) {
        ContlolFieldStoredAt storedAt;
        if (!m_controlFieldIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case ContlolFieldStoredAt.Dict1D: {
                ControlFieldProfile<float> controlField;
                if (m_controlFields1D.TryGetValue(name, out controlField)) {
                    m_controlFields1D.Remove(name);
                    controlField.InternalSetManager(null);
                }
                break;
            }
            case ContlolFieldStoredAt.Dict2D: {
                ControlFieldProfile<Vector2> controlField;
                if (m_controlFields2D.TryGetValue(name, out controlField)) {
                    m_controlFields2D.Remove(name);
                    controlField.InternalSetManager(null);
                }
                break;
            }
            case ContlolFieldStoredAt.Dict3D: {
                ControlFieldProfile<Vector3> controlField;
                if (m_controlFields3D.TryGetValue(name, out controlField)) {
                    m_controlFields3D.Remove(name);
                    controlField.InternalSetManager(null);
                }
                break;
            }
            case ContlolFieldStoredAt.Active1D: {
                ControlFieldProfile<float> controlField;
                if (m_controlFields1D.TryGetValue(name, out controlField)) {
                    m_controlFields1D.Remove(name);
                    controlField.InternalSetManager(null);
                    m_activeControlFields1D.Remove(controlField);
                } else {
                    m_activeControlFields1D.RemoveAll(controlField => controlField.Name == name);
                }
                break;
            }
            case ContlolFieldStoredAt.Active2D: {
                ControlFieldProfile<Vector2> controlField;
                if (m_controlFields2D.TryGetValue(name, out controlField)) {
                    m_controlFields2D.Remove(name);
                    controlField.InternalSetManager(null);
                    m_activeControlFields2D.Remove(controlField);
                } else {
                    m_activeControlFields2D.RemoveAll(controlField => controlField.Name == name);
                }
                break;
            }
            case ContlolFieldStoredAt.Active3D: {
                ControlFieldProfile<Vector3> controlField;
                if (m_controlFields3D.TryGetValue(name, out controlField)) {
                    m_controlFields3D.Remove(name);
                    controlField.InternalSetManager(null);
                    m_activeControlFields3D.Remove(controlField);
                } else {
                    m_activeControlFields3D.RemoveAll(controlField => controlField.Name == name);
                }
                break;
            }
        }
        return true;
    }
    public void RemoveAllControlFields() {
        for (int i = 0; i < m_controlFields1D.Count; ++i) {
            m_controlFields1D.ElementAt(i).Value.InternalSetManager(null);
        }
        m_controlFields1D.Clear();
        for (int i = 0; i < m_controlFields2D.Count; ++i) {
            m_controlFields2D.ElementAt(i).Value.InternalSetManager(null);
        }
        m_controlFields2D.Clear();
        for (int i = 0; i < m_controlFields3D.Count; ++i) {
            m_controlFields3D.ElementAt(i).Value.InternalSetManager(null);
        }
        m_controlFields3D.Clear();
        m_activeControlFields1D.Clear();
        m_activeControlFields2D.Clear();
        m_activeControlFields3D.Clear();
        m_controlFieldIndex.Clear();
    }
    public bool ActivateControlField(string name) {
        ContlolFieldStoredAt storedAt;
        if (!m_controlFieldIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case ContlolFieldStoredAt.Dict1D: {
                ControlFieldProfile<float> activeControlField = m_controlFields1D[name];
                m_activeControlFields1D.Add(activeControlField);
                m_controlFieldIndex.Add(name, ContlolFieldStoredAt.Active1D);
                activeControlField.InternalActivate();
                return true;
            }
            case ContlolFieldStoredAt.Dict2D: {
                ControlFieldProfile<Vector2> activeControlField = m_controlFields2D[name];
                m_activeControlFields2D.Add(activeControlField);
                m_controlFieldIndex.Add(name, ContlolFieldStoredAt.Active2D);
                activeControlField.InternalActivate();
                return true;
            }
            case ContlolFieldStoredAt.Dict3D: {
                ControlFieldProfile<Vector3> activeControlField = m_controlFields3D[name];
                m_activeControlFields3D.Add(activeControlField);
                m_controlFieldIndex.Add(name, ContlolFieldStoredAt.Active3D);
                activeControlField.InternalActivate();
                return true;
            }
            case ContlolFieldStoredAt.Active1D: {
                return true;
            }
            case ContlolFieldStoredAt.Active2D: {
                return true;
            }
            case ContlolFieldStoredAt.Active3D: {
                return true;
            }
            default: {
                Debug.LogError("Unhandled case");
                return false;
            }
        }
    }
    public bool DeactivateControlField(string name) {
        ContlolFieldStoredAt storedAt;
        if (!m_controlFieldIndex.TryGetValue(name, out storedAt)) {
            return false;
        }
        switch (storedAt) {
            case ContlolFieldStoredAt.Dict1D: {
                return true;
            }
            case ContlolFieldStoredAt.Dict2D: {
                return true;
            }
            case ContlolFieldStoredAt.Dict3D: {
                return true;
            }
            case ContlolFieldStoredAt.Active1D: {
                ControlFieldProfile<float> activeControlField = m_controlFields1D[name];
                m_activeControlFields1D.Remove(activeControlField);
                m_controlFieldIndex.Add(name, ContlolFieldStoredAt.Dict1D);
                activeControlField.InternalDeactivate();
                return true;
            }
            case ContlolFieldStoredAt.Active2D: {
                ControlFieldProfile<Vector2> activeControlField = m_controlFields2D[name];
                m_activeControlFields2D.Remove(activeControlField);
                m_controlFieldIndex.Add(name, ContlolFieldStoredAt.Dict1D);
                activeControlField.InternalDeactivate();
                return true;
            }
            case ContlolFieldStoredAt.Active3D: {
                ControlFieldProfile<Vector3> activeControlField = m_controlFields3D[name];
                m_activeControlFields3D.Remove(activeControlField);
                m_controlFieldIndex.Add(name, ContlolFieldStoredAt.Dict1D);
                activeControlField.InternalDeactivate();
                return true;
            }
            default: {
                Debug.LogError("Unhandled case");
                return false;
            }
        }
    }
    // private void UpdateLimitCache(KVariableLimits kvl) {
    //     // TODO - I need a registry to keep track of derived objects and their dependents
    // }
    // public void ApplyLimits(KVariableLimits kvl, KVariables<float> kv) {
    //     UpdateLimitCache(kvl);
    //     for (int i = 0; i < m_activeControlFields1D.Count; ++i) {
    //         m_activeControlFields1D[i].ApplyLimits(kvl, kvts);
    //     }
    //     for (int i = 0; i < m_activeControlFields1D.Count; ++i) {
    //         m_activeControlFields2D[i].ApplyLimits(kvl, kvts);
    //     }
    //     for (int i = 0; i < m_activeControlFields1D.Count; ++i) {
    //         m_activeControlFields3D[i].ApplyLimits(kvl, kvts);
    //     }
    // }
    // public void ApplyLimits(KVariableLimits kvl) {
    //     for (int i = 0; i < m_activeControlFields1D.Count; ++i) {
    //         m_activeControlFields1D[i].ApplyLimits(kvl);
    //     }
    //     for (int i = 0; i < m_activeControlFields1D.Count; ++i) {
    //         m_activeControlFields2D[i].ApplyLimits(kvl);
    //     }
    //     for (int i = 0; i < m_activeControlFields1D.Count; ++i) {
    //         m_activeControlFields3D[i].ApplyLimits(kvl);
    //     }
    // }

    void Update() {
        // TODO
        // This is where I go through all my 'interactions', such as:
        //      entity.TouchingGround() == true  ---> m_controlFields1D["jumping"].ImpulseType().Enabled=true;
        //      entity.TouchingGround() == false ---> m_controlFields1D["jumping"].ImpulseType().Enabled=false;
        //      entity.OnLadder() == true ---> ensure m_controlFields1D["climbing"] is active and m_controlFields1D["normal"] is inactive
        // and so on.
    }

    // *** Internal functions
    bool CheckSetup() {
        int nFreedoms = m_entity.NSpatialFreedoms + m_entity.NRotationalFreedoms;
        int nControls = m_activeControlFields1D.Count + 2*m_activeControlFields2D.Count + 3*m_activeControlFields3D.Count;
        // This check is not informative, because some control axes may be ForceUsers, which can overlap with other ForceUsers and one StateSetters
        // if (nFreedoms - nControls < 0) {
        //     Debug.LogError("Overconstrained system: " + nFreedoms + " freedoms, " + nControls + " controls.");
        //     return false;
        // }
        Vector3Int fixedSpatial = Vector3Int.zero;
        Vector3Int fixedRotational = Vector3Int.zero;
        int nSpatial = m_entity.NSpatialFreedoms;
        int nRotational = m_entity.NRotationalFreedoms;
        foreach (ControlFieldProfile<float> controlField in m_activeControlFields1D) {
            if (controlField.Type == ControlFieldType.Spatial) {
                fixedSpatial += controlField.InternalCheckUsedAxes;
                if (controlField.Control.StateSetter()) {
                    nSpatial -= controlField.NControlledDimensions;
                }
            } else if (controlField.Type == ControlFieldType.Rotational) {
                fixedRotational += controlField.InternalCheckUsedAxes;
                if (controlField.Control.StateSetter()) {
                    nRotational -= controlField.NControlledDimensions;
                }
            } else {
                Debug.LogError("ControlField " + controlField.Name + " has no Type");
            }
        }
        foreach (ControlFieldProfile<Vector2> controlField in m_activeControlFields2D) {
            if (controlField.Type == ControlFieldType.Spatial) {
                fixedSpatial += controlField.InternalCheckUsedAxes;
                if (controlField.Control.StateSetter()) {
                    nSpatial -= controlField.NControlledDimensions;
                }
            } else if (controlField.Type == ControlFieldType.Rotational) {
                fixedRotational += controlField.InternalCheckUsedAxes;
                if (controlField.Control.StateSetter()) {
                    nRotational -= controlField.NControlledDimensions;
                }
            } else {
                Debug.LogError("ControlField " + controlField.Name + " has no Type");
            }
        }
        foreach (ControlFieldProfile<Vector3> controlField in m_activeControlFields3D) {
            if (controlField.Type == ControlFieldType.Spatial) {
                fixedSpatial += controlField.InternalCheckUsedAxes;
                if (controlField.Control.StateSetter()) {
                    nSpatial -= controlField.NControlledDimensions;
                }
            } else if (controlField.Type == ControlFieldType.Rotational) {
                fixedRotational += controlField.InternalCheckUsedAxes;
                if (controlField.Control.StateSetter()) {
                    nRotational -= controlField.NControlledDimensions;
                }
            } else {
                Debug.LogError("ControlField " + controlField.Name + " has no Type");
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
        Debug.Log("Number of StateSetter type controlFields aligned to each axis cannot exceed 1." + spaceFail + rotateFail);
        return false;
    }

    // *** Constructors
    // TODO - Add constructors, file read/write
    ControlFieldProfileManager(MovementController<Q, V, T> entity, string gameObjectName) {
        m_entity = entity;
        m_gameObjectName = gameObjectName;
        m_spatialDimensions = GeneralTools.NDimensions<V>();
        m_rotationalDimensions = GeneralTools.NDimensions<T>();
        m_limits_controlManagerLevel = null;
        m_limits_cache = null;
        m_limits_cacheUtdf = -1;
        m_limiterFloat = new KvLimiter<float>();
        m_limiterVector2 = new KvLimiter<Vector2>();
        m_limiterVector3 = new KvLimiter<Vector3>();
    }
}
