using System.Collections.Generic;

public abstract class DerivedDataObjHeader : DataObjHeader, IDerivedDataObjMeta {
    // Encapsulation - updater is responsible for initialising and populating these data
    List<ISourceDataObjMeta> m_dependsOn;
    IObjUpdater m_updater;

    public IObjUpdater Updater { get=>m_updater; set=>m_updater=value; }
    public List<ISourceDataObjMeta> DependsOn { get=>m_dependsOn; set=>m_dependsOn=value; }
    public bool Stale() { return m_updater == null; }
    public bool UpToDate() {
        if (m_updater == null) { return false; }
        foreach(IObj dataObj in m_dependsOn) {
            if (dataObj.MTag > m_mtag) { return false; }
        }
        return true;
    }
    public bool UpdateDerived() {
        if (m_updater == null) {
            return false;
        }
        return m_updater.PerformUpdatesFor(this);
    }
    public DerivedDataObjHeader(
        string name,
        IObjRegistry parent = null,
        IObjUpdater updater = null
    ) : base(name, parent) {
        m_updater = updater;
        if (m_updater != null) {
            m_updater.InitDependsOnFor(this, out m_dependsOn);
        }
    }
    public DerivedDataObjHeader(DerivedDataObjHeader obj) : base(obj) {
        // Do not init m_dependsOn - we expect our updater to do it
    }
    public DerivedDataObjHeader() {}
}
