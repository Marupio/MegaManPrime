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
    protected DataObjList_SourcePass m_allDependsOnSources;

    // *** IDataPortModule interface
    protected DataPortModule<DataObjList, DataObjList_DerivedPass> m_module;
    protected ModTag m_lastModuleTag;
    public bool Enabled { get=>m_module.Enabled; set=>m_module.Enabled=value; }
    public Dictionary<string, DataPortProfile> Profiles {
        get=>m_module.Profiles;
        set=>m_module.Profiles=value;
    }
    public int NProfiles { get=>m_module.NProfiles; }
    public DataPortProfile ActiveProfile { get=>m_module.ActiveProfile; }
    public ActiveDataPortConnections<DataObjList, DataObjList_DerivedPass> Connections { get=>m_module.Connections; }
    public bool Ready { get=>m_module.Ready; }

    /// <summary>
    /// Defined in inheriting class to define its supported profiles.  Called by all DerivedUpdaterBase constructors.
    /// </summary>
    public abstract List<DataPortProfile> SupportedProfiles();

    // *** IDerivedUpdater interface
    public DataObjList_DerivedPass AllDerivedData { get=>m_module.Connections.Outputs; }
    public DataObjList AllDependsOnDirectly { get=>m_module.Connections.Inputs; }
    public DataObjList_SourcePass AllDependsOnSources { 
        /* TODO - Lazy evaluation here */
        get {
            if (m_lastModuleTag != m_module.MTag) {
                UpdateSourceDependsOn();
            }
            return m_allDependsOnSources;
        }
    }
    public virtual DataObjList GetDirectDependsOnFor(IDerivedDataObjMeta outputObj) {
        return AllDependsOnDirectly;
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
        m_lastModuleTag = m_module.MTag;
        bool changed = false;
        HashSet<long> sourcesAdded = m_allDependsOnSources.Select(dataObj=>dataObj.Id).ToHashSet();
        HashSet<long> updatersDone = new HashSet<long>();
        foreach(IDataObjMeta dataObj in AllDependsOnDirectly) {
            if (dataObj is IDerivedDataObjMeta) {
                IDerivedDataObjMeta derivedDataObj = (IDerivedDataObjMeta)dataObj;
                IDerivedUpdater objUpdater = derivedDataObj.Updater;
                if (updatersDone.Contains(objUpdater.Id)) {
                    continue;
                }
                updatersDone.Add(objUpdater.Id);
                DataObjList_SourcePass objSources = objUpdater.AllDependsOnSources;
                foreach (IDataObjMeta subDataObj in objSources) {
                    if (sourcesAdded.Contains(subDataObj.Id)) {
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
    // public DataPortModule(DataPortProfile profile, ActiveDataPortConnections<I, O> connections = null) {
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, ActiveDataPortConnections<I, O> connections = null) {
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, int activeProfileIndex) {
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, string activeProfileName) {
    // public DataPortModule(IEnumerable<DataPortProfile> profiles, DataPortProfile activeProfile) {
    // public DataPortModule(DataPortModule<I, O> dpl, ActiveDataPortConnections<I, O> connections = null) {
    // public DataPortModule(ActiveDataPortConnections<I, O> connections = null) {
    DerivedUpdaterBase(string name, IObjRegistry parent, ActiveDataPortConnections<DataObjList, DataObjList_DerivedPass> connections) : base(name, parent) {
        m_module = new DataPortModule<DataObjList, DataObjList_DerivedPass>(SupportedProfiles(), connections);
        m_allDependsOnSources = new DataObjList_SourcePass();
        UpdateSourceDependsOn();
    }
    DerivedUpdaterBase(string name, IObjRegistry parent, int activeProfileIndex) : base(name, parent) {
        List<DataPortProfile> supportedProfiles = SupportedProfiles();
        m_module = new DataPortModule<DataObjList, DataObjList_DerivedPass>(supportedProfiles, supportedProfiles[activeProfileIndex]);
        m_allDependsOnSources = new DataObjList_SourcePass();
        UpdateSourceDependsOn();
    }
    DerivedUpdaterBase(string name, IObjRegistry parent, string activeProfileName) : base(name, parent) {
        m_module = new DataPortModule<DataObjList, DataObjList_DerivedPass>(SupportedProfiles(), activeProfileName);
        m_allDependsOnSources = new DataObjList_SourcePass();
        UpdateSourceDependsOn();
    }
    DerivedUpdaterBase(string name, IObjRegistry parent, DataPortProfile activeProfile) : base(name, parent) {
        m_module = new DataPortModule<DataObjList, DataObjList_DerivedPass>(SupportedProfiles(), activeProfile);
        m_allDependsOnSources = new DataObjList_SourcePass();
        UpdateSourceDependsOn();
    }
    DerivedUpdaterBase(string name = null, IObjRegistry parent = null) : base(name, parent) {
        m_module = new DataPortModule<DataObjList, DataObjList_DerivedPass>(SupportedProfiles());
        m_allDependsOnSources = new DataObjList_SourcePass();
        UpdateSourceDependsOn();
    }
    // TODO Copy constructor, Copy constructor + out CloneResult
}
