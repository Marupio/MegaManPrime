using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PipelineExecutableBase : DataPortModule<DataObjList, DataObjList>, IPipelineExecutableObj {

    /// <summary>
    /// Defined in inheriting class to define its supported profiles.  Called by all DerivedUpdaterBase constructors.
    /// </summary>
    public abstract List<DataPortProfile> SupportedProfiles();
    /// <summary>
    /// Defined in inheriting class to define pipeline execution routine
    /// </summary>
    public abstract void InternalExecute(DataPortProfile profile, ActiveDataPortConnections<DataObjList, DataObjList> connections);

    // *** Internal methods
    bool CheckProfileAndCondition(DataPortProfile profile) {
        if (!Profiles.ContainsValue(profile)) {
            Debug.LogError("Attempting to execute missing DataPortProfile " + profile.Name + " on PipelineExecutableBase object " + m_name);
            return false;
        }
        return m_enabled;
    }
    bool CheckProfileAndCondition(string profileName) {
        if (!Profiles.ContainsKey(profileName)) {
            Debug.LogError("Attempting to execute missing DataPortProfile " + profileName + " on PipelineExecutableBase object " + m_name);
            return false;
        }
        return m_enabled;
    }

    void ExecuteDispatch(DataPortProfile profile, ActiveDataPortConnections<DataObjList, DataObjList> connections) {
        if (Tracker.HaveInputsChanged(profile, connections)) {
            InternalExecute(profile, connections);
        }
    }

    // *** Execute
    public void ExecuteAttached() {
        ExecuteDispatch(ActiveProfile, Connections);
    }
    public void ExecuteProfile(DataPortProfile profile) {
        if (!CheckProfileAndCondition(profile)) return;
        ExecuteDispatch(profile, new ActiveDataPortConnections<DataObjList, DataObjList>(profile));
    }
    public void ExecuteProfile(DataPortProfile profile, ActiveDataPortConnections<DataObjList, DataObjList> connections) {
        if (!CheckProfileAndCondition(profile)) return;
        ExecuteDispatch(profile, connections);
    }
    public void ExecuteProfile(string profileName) {
        if (!CheckProfileAndCondition(profileName)) return;
        DataPortProfile profile = this[profileName];
        ExecuteDispatch(profile, new ActiveDataPortConnections<DataObjList, DataObjList>(profile));
    }
    public void ExecuteProfile(string profileName, ActiveDataPortConnections<DataObjList, DataObjList> connections) {
        if (!CheckProfileAndCondition(profileName)) return;
        DataPortProfile profile = this[profileName];
        ExecuteDispatch(profile, connections);
    }

    // *** Constructors

    public PipelineExecutableBase(string name, IObjRegistry parent, ActiveDataPortConnections<DataObjList, DataObjList> connections) : base(connections) {
        AddRange(SupportedProfiles());
        // TODO - this will add duplicate profiles
    }

    public PipelineExecutableBase(DataPortModule<DataObjList, DataObjList> dataModule) : base(dataModule) {}
    public PipelineExecutableBase(DataPortProfile profile) : base(profile) {}
    public PipelineExecutableBase(List<DataPortProfile> dpps, int activeProfileIndex = 0): base(dpps, dpps[activeProfileIndex]) {}
    public PipelineExecutableBase() : base() {}
}
