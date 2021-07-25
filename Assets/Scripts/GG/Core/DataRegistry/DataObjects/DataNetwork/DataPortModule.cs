using System.Collections.Generic;

public abstract class DataPortModule : ObjHeader, IDataPortModule {
    protected DataPortProfileList m_profiles;
    protected bool m_enabled = true;

    public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    public DataPortProfileList Profiles { get=>m_profiles; set=>m_profiles=value; }
    public int NProfiles { get=>m_profiles.Count; }
    public DataPortProfile ActiveProfile { get=>m_profiles.ActiveProfile; }
    public ActiveDataPortConnections Connections { get=>m_profiles.Connections; }
    public bool Ready { get=>m_profiles.Connections.Ready; }

    // *** Constructors
    public DataPortModule(DataPortProfileList profiles) {
        m_profiles = profiles;
    }
    public DataPortModule(DataPortProfile profile) {
        m_profiles = new DataPortProfileList(profile);
    }
    public DataPortModule(List<DataPortProfile> dpps, int activeProfileIndex = 0) {
        m_profiles = new DataPortProfileList(dpps, activeProfileIndex);
    }
    public DataPortModule() {
        m_profiles = new DataPortProfileList();
    }
}

