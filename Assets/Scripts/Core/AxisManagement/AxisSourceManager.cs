using System.Collections.Generic;
using UnityEngine;

public class AxisSourceManager {
    Dictionary<string, AxisSource> m_sources;

    // *** Access
    Dictionary<string, AxisSource> Sources {get => m_sources; set => m_sources = value;}

    // *** Edit
    public bool AddSource(AxisSource source, bool overwrite = true) {
        if (!overwrite && m_sources.ContainsKey(source.Name)) {
            Debug.LogError(source.Name + " already exists, ignoring");
            return false;
        }
        m_sources.Add(source.Name, source);
        return true;
    }
    public bool RemoveSource(AxisSource source) {
        bool hadIt = m_sources.ContainsKey(source.Name);
        m_sources.Remove(source.Name);
        return hadIt;
    }
    public void RemoveAllSources() {
        m_sources.Clear();
    }

    // *** Operators
    public AxisSource this[string str]
    {
        get { return m_sources[str]; }
        set { m_sources[str] = value; }
    }
}