using System.Collections.Generic;
using UnityEngine;

public abstract class PipelineExecutableBase : DataPortModule, IPipelineExecutableObj {
    // *** Internal methods
    public abstract void InternalExecute(DataPortProfile profile, ActiveDataPortConnections connections);
    bool CheckProfileAndCondition(DataPortProfile profile) {
        if (!m_profiles.Contains(profile)) {
            Debug.LogError("Attempting to execute missing DataPortProfile " + profile.Name + " on PipelineExecutableBase object " + m_name);
            return false;
        }
        return m_enabled;
    }
    bool CheckProfileAndCondition(string profileName) {
        if (!m_profiles.Contains(profileName)) {
            Debug.LogError("Attempting to execute missing DataPortProfile " + profileName + " on PipelineExecutableBase object " + m_name);
            return false;
        }
        return m_enabled;
    }
    bool CheckProfileAndCondition(int profileIndex) {
        if (profileIndex < 0 || profileIndex >= m_profiles.Count) {
            Debug.LogError("Index " + profileIndex + " out of range [0.." + (m_profiles.Count - 1) + "] on PipelineExecutableBase object " + m_name);
            return false;
        }
        return m_enabled;
    }

    // *** Execute
    public void ExecuteAttached() {
        InternalExecute(ActiveProfile, Connections);
    }
    public void ExecuteProfile(DataPortProfile profile) {
        if (!CheckProfileAndCondition(profile)) return;
        InternalExecute(profile, new ActiveDataPortConnections(profile));
    }
    public void ExecuteProfile(DataPortProfile profile, ActiveDataPortConnections connections) {
        if (!CheckProfileAndCondition(profile)) return;
        InternalExecute(profile, connections);
    }
    public void ExecuteProfile(string profileName) {
        if (!CheckProfileAndCondition(profileName)) return;
        DataPortProfile profile = m_profiles[profileName];
        InternalExecute(profile, new ActiveDataPortConnections(profile));
    }
    public void ExecuteProfile(string profileName, ActiveDataPortConnections connections) {
        if (!CheckProfileAndCondition(profileName)) return;
        DataPortProfile profile = m_profiles[profileName];
        InternalExecute(profile, connections);
    }
    public void ExecuteProfile(int profileIndex) {
        if (!CheckProfileAndCondition(profileIndex)) return;
        DataPortProfile profile = m_profiles[profileIndex];
        InternalExecute(profile, new ActiveDataPortConnections(profile));
    }
    public void ExecuteProfile(int profileIndex, ActiveDataPortConnections connections) {
        if (!CheckProfileAndCondition(profileIndex)) return;
        DataPortProfile profile = m_profiles[profileIndex];
        InternalExecute(profile, connections);
    }

    // *** Constructors
    public PipelineExecutableBase(DataPortProfileList profiles) : base(profiles) {}
    public PipelineExecutableBase(DataPortProfile profile) : base(profile) {}
    public PipelineExecutableBase(List<DataPortProfile> dpps, int activeProfileIndex = 0): base(dpps, activeProfileIndex) {}
    public PipelineExecutableBase() : base() {}
}
