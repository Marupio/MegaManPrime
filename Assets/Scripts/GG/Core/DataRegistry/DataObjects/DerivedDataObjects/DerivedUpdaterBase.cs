using System.Collections.Generic;
using UnityEngine;

// public class GeneralObjUpdater : IDerivedUpdater {
//     protected List<ISourceDataObjMeta> m_inputs;
//     protected List<IDerivedDataObjMeta> m_outputs;

//     void PerformUpdatesFor<L>(IDerivedDataObj<L> target);
//     void PerformAllUpdates();
//     void InitDependsOnFor<L>(IDerivedDataObj<L> target, out List<ISourceDataObjMeta> dependsOn);
//     void InitAllDependsOn();

//     // protected Vector2 GetVector2(string name);
// }

public abstract class DerivedUpdaterBase : ObjHeader, IDerivedUpdater {
    protected DataPortProfile m_dataProfile;

    // Reverse lookup
    protected Dictionary<IDerivedDataObjMeta, int> m_index;

    // Indexed data - all 'indexed' are associated between identical indices
    protected List<IDerivedDataObjMeta> m_outputs;
    protected List<List<IDataObjMeta>> m_directInputs;
    protected List<List<ISourceDataObjMeta>> m_sourceInputs;
    protected List<bool> m_derivedVarInitComplete;

    public DataPortProfile DataProfile { get=>m_dataProfile; }
    public List<IDerivedDataObjMeta> AllDerivedData { get=>m_outputs; }
    public List<List<IDataObjMeta>> DirectDependsOn { get=>m_directInputs; }
    public List<List<ISourceDataObjMeta>> SourceDependsOn { get=>m_sourceInputs; }

    public abstract bool PerformUpdatesFor(IDerivedDataObjMeta target); // TODO - or internally change this into an index, and make that call abstract
    public abstract void PerformAllUpdates();
    public bool InitDependsOnFor(IDerivedDataObjMeta target, out List<IDataObjMeta> directDependsOn, out List<ISourceDataObjMeta> sourceDependsOn) {
        int index;
        if (m_index.TryGetValue(target, out index)) {
            directDependsOn = m_directInputs[index];
            sourceDependsOn = m_sourceInputs[index];
            m_derivedVarInitComplete[index] = true;
            return true;
        }
        directDependsOn = null;
        sourceDependsOn = null;
        return false;
    }
    public void InitAllDependsOn() {
        for (int index = 0; index < m_directInputs.Count; ++index) {
            IDerivedDataObjMeta obj = m_outputs[index];
            obj.Updater = this;
            obj.DirectDependsOn = m_directInputs[index];
            obj.SourceDependsOn = m_sourceInputs[index];
        }
    }
    public virtual bool SpawnAllDerived() {
        if (m_outputs == null) {
            m_outputs = new List<IDerivedDataObjMeta>();
        } else if (m_outputs.Count > 0) {
            Debug.LogError("Attempting to SpawnAllDerived more than once.");
            return false;
        }
        if (m_dataProfile == null) {
            Debug.LogError("Attempting to SpawnAllDerived with null DataProfile.");
            return false;
        }
        List<string> DerivedNames = DataProfile.OutputNames;
        List<DataTypeEnum> DerivedTypes = DataProfile.OutputTypes;
        for (int i = 0; i < DerivedNames.Count; ++i) {
            DerivedDataObjHeader newDerived = DerivedDataObjHeader.Spawn(
                DerivedTypes[i],
                DerivedNames[i],
                m_parent,
                this
            );
            m_outputs.Add(newDerived);
        }
    }

    DerivedUpdaterBase(string name, IObjRegistry parent = null, DataPortProfile dataProfile = null)
    : base(name, parent) {
        m_dataProfile = dataProfile;
    }
}
