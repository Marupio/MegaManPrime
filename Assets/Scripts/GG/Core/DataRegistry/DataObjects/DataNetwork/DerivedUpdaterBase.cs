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

public abstract class DerivedUpdaterBase : DataPortModule, IDerivedUpdater {
    //DPM
    // protected DataPortProfileList<I, O> m_profiles;
    // protected bool m_enabled = true;
    // public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    // public DataPortProfileList<I, O> Profiles { get=>m_profiles; set=>m_profiles=value; }
    // public int NProfiles { get=>m_profiles.Count; }
    // public DataPortProfile ActiveProfile { get=>m_profiles.ActiveProfile; }
    // public ActiveDataPortConnections<I, O> Connections { get=>m_profiles.Connections; }
    // public bool Ready { get=>m_profiles.Connections.Ready; }

    // DPM Contains all pre-defined input/output configurations
    // It also contains the currently selected one
    // For that one, it has:
    //      a list of inputs (IDataObjMeta)
    //      a list of outputs (IDataObMeta filter only IDerivedDataObjMeta)


    //IDU
    // List<IDerivedDataObjMeta> AllDerivedData { get; }
        // DataPortModule.ActiveDataPortConnections -> protected DataObjList m_inputs;
        // This is 1:1 the list of DerivedData, but all cast as IDataObjMeta types
        // Is this okay, or do we need to maintain a separate MetaData type?
    // List<List<IDataObjMeta>> DirectDependsOn { get; } // The sources used in Update, may include other DerivedDataObj
        // For each Derived, gives the dependent data.  Is this too complicated?  Treat the entire updater as a black box.
        // Yes, we will treat the entire updater as a black box, no need for this level of resolution
    // List<List<ISourceDataObjMeta>> SourceDependsOn { get; } // The sources, resolved down to SourceDataObj level, hierarchically flattened
    // bool PerformUpdatesFor(IDerivedDataObjMeta target);
    // void PerformAllUpdates();
    // // 'Init' the 'DependsOn' list 'For' the given target derivedDataObj
    // bool InitDependsOnFor(IDerivedDataObjMeta target, out List<IDataObjMeta> directDependsOn, out List<ISourceDataObjMeta> sourceDependsOn);
    // void InitAllDependsOn();
    // bool SpawnAllDerived();

    DataPortModule<

    // May have multiple pre-defined inputs / outputs - these are all encapsulated in a DataPortProfileList

    // These are all encapsulated in ActiveDataPortConnections
    protected DataObjList m_inputs;     // Input from pipeline -> not constrained - any IDataObjMeta
    protected DataObjList m_outputs;    // Output to pipeline  -> internally constrained to IDerivedDataObjMeta




    // Reverse lookup
    protected Dictionary<IDerivedDataObjMeta, int> m_index;

    // Indexed data - all 'indexed' are associated between identical indices
    protected List<IDerivedDataObjMeta> m_outputs;
    protected List<List<IDataObjMeta>> m_directInputs;
    protected List<List<ISourceDataObjMeta>> m_sourceInputs;
    protected List<bool> m_derivedVarInitComplete;

    public List<IDerivedDataObjMeta> AllDerivedData { get=>m_outputs; }
    public List<List<IDataObjMeta>> DirectDependsOn { get=>m_directInputs; }
    public List<List<ISourceDataObjMeta>> SourceDependsOn { get=>m_sourceInputs; }

    // List<IDerivedDataObjMeta> AllDerivedData { get; }
    // List<List<IDataObjMeta>> DirectDependsOn { get; } // The sources used in Update, may include other DerivedDataObj
    // List<List<ISourceDataObjMeta>> SourceDependsOn { get; } // The sources, resolved down to SourceDataObj level, hierarchically flattened
    // bool PerformUpdatesFor(IDerivedDataObjMeta target);
    // void PerformAllUpdates();
    // // 'Init' the 'DependsOn' list 'For' the given target derivedDataObj
    // bool InitDependsOnFor(IDerivedDataObjMeta target, out List<IDataObjMeta> directDependsOn, out List<ISourceDataObjMeta> sourceDependsOn);
    // void InitAllDependsOn();
    // bool SpawnAllDerived();

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
        return true;
    }

    // *** Constructors
    DerivedUpdaterBase(string name, IObjRegistry parent = null, DataPortProfile dataProfile = null)
    : base(name, parent) {
        m_dataProfile = dataProfile;
    }
    DerivedUpdaterBase(DerivedUpdaterBase obj) : base(obj) {}
    DerivedUpdaterBase() : base() {}
}
