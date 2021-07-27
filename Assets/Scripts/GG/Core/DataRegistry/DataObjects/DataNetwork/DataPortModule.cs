using System.Collections.Generic;

public abstract class DataPortModule<I, O> : ObjHeader, IDataPortModule<I, O>
    where I : class, IObjPass<IDataObjMeta>
    where O : class, IObjPass<IDataObjMeta>
{
    protected DataPortProfileList<I, O> m_profiles;
    protected bool m_enabled = true;

    public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    public DataPortProfileList<I, O> Profiles { get=>m_profiles; set=>m_profiles=value; }
    public int NProfiles { get=>m_profiles.Count; }
    public DataPortProfile ActiveProfile { get=>m_profiles.ActiveProfile; }
    public ActiveDataPortConnections<I, O> Connections { get=>m_profiles.Connections; }
    public bool Ready { get=>m_profiles.Connections.Ready; }

    // *** Constructors
    public DataPortModule(DataPortProfileList<I, O> profiles) {
        m_profiles = profiles;
    }
    public DataPortModule(DataPortProfile profile) {
        m_profiles = new DataPortProfileList<I, O>(profile);
    }
    public DataPortModule(List<DataPortProfile> dpps, int activeProfileIndex = 0) {
        m_profiles = new DataPortProfileList<I, O>(dpps, activeProfileIndex);
    }
    public DataPortModule() {
        m_profiles = new DataPortProfileList<I, O>();
    }
}

