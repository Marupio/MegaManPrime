using System.Collections.Generic;
using System.Linq;
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

public abstract class DerivedUpdaterBase : ObjRegistry, IDerivedUpdater {
    protected DataObjList_DerivedPass m_allDerivedData;
    protected DataObjList m_allDependsOnDirectly;
    protected DataObjList_SourcePass m_allDependsOnSources;

    // *** IDataPortModule interface
    protected DataPortModule<DataObjPassNull, DataObjPassDerivedData> m_module;
    public bool Enabled { get=>m_module.Enabled; set=>m_module.Enabled=value; }
    public List<DataPortProfile> Profiles {
        get=>m_module.Profiles;
        set=>m_module.Profiles=value;
    }
    public int NProfiles { get=>m_module.NProfiles; }
    public DataPortProfile ActiveProfile { get=>m_module.ActiveProfile; }
    public ActiveDataPortConnections<DataObjPassNull, DataObjPassDerivedData> Connections { get=>m_module.Connections; }
    public bool Ready { get=>m_module.Ready; }

    /// <summary>
    /// Defined in inheriting class to define its supported profiles.  Called by all DerivedUpdaterBase constructors.
    /// </summary>
    public abstract List<DataPortProfile> SupportedProfiles();

    // Unneeded because derived outputs will be created first
    // Need to tag all derived data with me as updater
    // public bool SetDerivedReferences(out IDerivedDataObjMeta out0);
    // public bool SetDerivedReferences(out IDerivedDataObjMeta out0, out IDerivedDataObjMeta out1);
    // public bool SetDerivedReferences(out IDerivedDataObjMeta out0, out IDerivedDataObjMeta out1, out IDerivedDataObjMeta out2);
    // public bool SetDerivedReferences(
    //     out IDerivedDataObjMeta out0, out IDerivedDataObjMeta out1, out IDerivedDataObjMeta out2, out IDerivedDataObjMeta out3
    // );
    // public bool SetDerivedReferences(
    //     out IDerivedDataObjMeta out0, out IDerivedDataObjMeta out1, out IDerivedDataObjMeta out2, out IDerivedDataObjMeta out3,
    //     out IDerivedDataObjMeta out4
    // );
    // public bool SetDerivedReferenceRange(out IDerivedDataObjMeta[] outs);

    // *** IDerivedUpdater interface
    public DataObjList_DerivedPass AllDerivedData { get=>m_allDerivedData; }
    public DataObjList AllDependsOnDirectly { get=>m_allDependsOnDirectly; }
    public DataObjList_SourcePass AllDependsOnSources { get=>m_allDependsOnSources; }
    public virtual DataObjList GetDirectDependsOnFor(IDerivedDataObjMeta outputObj) {
        return m_allDependsOnDirectly;
    }
    public virtual DataObjList_SourcePass GetSourceDependsOnFor(IDerivedDataObjMeta outputObj) {
        return m_allDependsOnSources;
    }
    public abstract bool UpToDateAll();
    public virtual bool UpToDateFor(IDerivedDataObjMeta obj) {
        return UpToDateAll();
    }
    public abstract bool PerformUpdatesAll();
    public virtual bool PerformUpdatesFor(IDerivedDataObjMeta outputObj) {
        return PerformUpdatesAll();
    }
    // Add functionality to automatically spawn derived
    // Make work flow for classes inheriting this one

    /// <summary>
    /// Iterate through my direct depends-on list and update my source depends-on list.
    /// </summary>
    /// <returns>true if something changed</returns>
    public bool UpdateSourceDependsOn() {
        bool changed = false;
        HashSet<long> sourcesAdded = m_allDependsOnSources.Select(dataObj=>dataObj.Id).ToHashSet();
        HashSet<long> updatersDone = new HashSet<long>();
        foreach(IDataObjMeta dataObj in m_allDependsOnDirectly) {
            if (dataObj is IDerivedDataObjMeta) {
                IDerivedDataObjMeta derivedDataObj = (IDerivedDataObjMeta)dataObj;
                IDerivedUpdater objUpdater = derivedDataObj.Updater;
                if (updatersDone.Contains(objUpdater.Id)) {
                    continue;
                }
                updatersDone.Add(objUpdater.Id);
                DataObjList_SourcePass objSources = objUpdater.AllDependsOnSources;
                foreach (IDataObjMeta subDataObj in objSources) {
                    if (sourcesAdded.Contains(subDataObj.Id) {
                        continue;
                    }
                    changed = true;
                    sourcesAdded.Add(subDataObj.Id);
                    m_allDependsOnSources.Add(subDataObj);
                }
            } else if (dataObj is ISourceDataObjMeta) {
                if (sourcesAdded.Contains(dataObj.Id)) {
                    continue;
                }
                changed = true;
                sourcesAdded.Add(dataObj.Id);
                m_allDependsOnSources.Add(dataObj);
            } else {
                Debug.LogException(new System.NotImplementedException("Unknown IDataObj type for Obj " + dataObj.Name + ", Id " + dataObj.Id));
                continue;
            }
        }
        return changed;
    }

    // *** ObjRegistry-related functionality
    // Cloning

    // Pass on constructors supporting DPM, ObjRegistry

    // public ObjRegistry(string name, IObjRegistry parent = null, List<IObj> children = null)
    // public ObjRegistry(ObjRegistry reg, out CloneResult cr)
    // public ObjRegistry(ObjRegistry reg)
    // public ObjRegistry()
    // public DataPortModule(DataPortProfile profile, ActiveDataPortConnections<I, O> connections = null)
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, ActiveDataPortConnections<I, O> connections = null)
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, int activeProfileIndex)
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, string activeProfileName)
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, DataPortProfile activeProfile)
    // public DataPortModule(DataPortModule<I, O> dpl, ActiveDataPortConnections<I, O> connections = null)
    // public DataPortModule(ActiveDataPortConnections<I, O> connections = null)
    DerivedUpdaterBase(string name, IObjRegistry parent, ActiveDataPortConnections<DataObjPassNull, DataObjPassDerivedData> connections) : base(name, parent) {
        m_module = new DataPortModule<DataObjPassNull, DataObjPassDerivedData>(SupportedProfiles(), connections);
        m_allDerivedData = new DataObjList_DerivedPass(m_module.Connections.Outputs);
        m_allDependsOnDirectly = new DataObjList(m_module.Connections.Inputs);
        m_allDependsOnSources = new DataObjList_SourcePass();
        UpdateSourceDependsOn();
        // TODO - think about these m_allD... will they work when connections changes?  etc.
        // All my data needs to be cleared and needs a central update function every time the associated DataModule changes
        // LazyEvaluation pattern around these data:
        //      * protected DataObjList_DerivedPass m_allDerivedData;  <-- this needs to be not present, defer to DataModule, no need to copy
        //      * protected DataObjList m_allDependsOnDirectly;  <-- this needs to be not present, defer to DataModule, no need to copy
        //      * protected DataObjList_SourcePass m_allDependsOnSources;  <-- This is the only one that needs updating
        // using MTag and UpToDate with m_dataModule or m_module or whatever

    }



    // DPM
    // protected DataPortProfileList<I, O> m_profiles;
    // protected bool m_enabled = true;
    // public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    // public DataPortProfileList<I, O> Profiles { get=>m_profiles; set=>m_profiles=value; }
    // public int NProfiles { get=>m_profiles.Count; }
    // public DataPortProfile ActiveProfile { get=>m_profiles.ActiveProfile; }
    // public ActiveDataPortConnections<I, O> Connections { get=>m_profiles.Connections; }
    // public bool Ready { get=>m_profiles.Connections.Ready; }




// // OLD RUBBIISH

//     // May have multiple pre-defined inputs / outputs - these are all encapsulated in a DataPortProfileList

//     // These are all encapsulated in ActiveDataPortConnections
//     protected DataObjList m_inputs;     // Input from pipeline -> not constrained - any IDataObjMeta
//     protected DataObjList m_outputs;    // Output to pipeline  -> internally constrained to IDerivedDataObjMeta

//     void SpawnDerived() {
        
//     }


//     // Reverse lookup
//     protected Dictionary<IDerivedDataObjMeta, int> m_index;

//     // Indexed data - all 'indexed' are associated between identical indices
//     protected List<IDerivedDataObjMeta> m_outputs;
//     protected List<List<IDataObjMeta>> m_directInputs;
//     protected List<List<ISourceDataObjMeta>> m_sourceInputs;
//     protected List<bool> m_derivedVarInitComplete;

//     public List<IDerivedDataObjMeta> AllDerivedData { get=>m_outputs; }
//     public List<List<IDataObjMeta>> DirectDependsOn { get=>m_directInputs; }
//     public List<List<ISourceDataObjMeta>> SourceDependsOn { get=>m_sourceInputs; }

//     // List<IDerivedDataObjMeta> AllDerivedData { get; }
//     // List<List<IDataObjMeta>> DirectDependsOn { get; } // The sources used in Update, may include other DerivedDataObj
//     // List<List<ISourceDataObjMeta>> SourceDependsOn { get; } // The sources, resolved down to SourceDataObj level, hierarchically flattened
//     // bool PerformUpdatesFor(IDerivedDataObjMeta target);
//     // void PerformAllUpdates();
//     // // 'Init' the 'DependsOn' list 'For' the given target derivedDataObj
//     // bool InitDependsOnFor(IDerivedDataObjMeta target, out List<IDataObjMeta> directDependsOn, out List<ISourceDataObjMeta> sourceDependsOn);
//     // void InitAllDependsOn();
//     // bool SpawnAllDerived();

//     public abstract bool PerformUpdatesFor(IDerivedDataObjMeta target); // TODO - or internally change this into an index, and make that call abstract
//     public abstract void PerformAllUpdates();
//     public bool InitDependsOnFor(IDerivedDataObjMeta target, out List<IDataObjMeta> directDependsOn, out List<ISourceDataObjMeta> sourceDependsOn) {
//         int index;
//         if (m_index.TryGetValue(target, out index)) {
//             directDependsOn = m_directInputs[index];
//             sourceDependsOn = m_sourceInputs[index];
//             m_derivedVarInitComplete[index] = true;
//             return true;
//         }
//         directDependsOn = null;
//         sourceDependsOn = null;
//         return false;
//     }
//     public void InitAllDependsOn() {
//         for (int index = 0; index < m_directInputs.Count; ++index) {
//             IDerivedDataObjMeta obj = m_outputs[index];
//             obj.Updater = this;
//             obj.DirectDependsOn = m_directInputs[index];
//             obj.SourceDependsOn = m_sourceInputs[index];
//         }
//     }
//     public virtual bool SpawnAllDerived() {
//         if (m_outputs == null) {
//             m_outputs = new List<IDerivedDataObjMeta>();
//         } else if (m_outputs.Count > 0) {
//             Debug.LogError("Attempting to SpawnAllDerived more than once.");
//             return false;
//         }
//         if (m_dataProfile == null) {
//             Debug.LogError("Attempting to SpawnAllDerived with null DataProfile.");
//             return false;
//         }
//         List<string> DerivedNames = DataProfile.OutputNames;
//         List<DataTypeEnum> DerivedTypes = DataProfile.OutputTypes;
//         for (int i = 0; i < DerivedNames.Count; ++i) {
//             DerivedDataObjHeader newDerived = DerivedDataObjHeader.Spawn(
//                 DerivedTypes[i],
//                 DerivedNames[i],
//                 m_parent,
//                 this
//             );
//             m_outputs.Add(newDerived);
//         }
//         return true;
//     }

    // *** Constructors
    DerivedUpdaterBase(string name, IObjRegistry parent = null, DataPortProfile dataProfile = null)
    : base(name, parent) {
        m_dataProfile = dataProfile;
    }
    DerivedUpdaterBase(DerivedUpdaterBase obj) : base(obj) {}
    DerivedUpdaterBase() : base() {}
}
