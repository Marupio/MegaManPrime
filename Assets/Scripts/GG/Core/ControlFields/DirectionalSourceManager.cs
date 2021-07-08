using System.Collections.Generic;
using UnityEngine;

public class DirectionalSourceManager {
    Dictionary<string, DirectionalSource> m_sources;

    // *** Access
    public Dictionary<string, DirectionalSource> Sources {get => m_sources; set => m_sources = value;}
    public DirectionalSource GetSource(string name) { return m_sources[name]; }


    // *** Edit
    public bool AddSource(DirectionalSource source, bool overwrite = true) {
        if (!overwrite && m_sources.ContainsKey(source.Name)) {
            Debug.LogError(source.Name + " already exists, ignoring");
            return false;
        }
        m_sources.Add(source.Name, source);
        return true;
    }
    public bool RemoveSource(DirectionalSource source) {
        bool hadIt = m_sources.ContainsKey(source.Name);
        m_sources.Remove(source.Name);
        return hadIt;
    }
    public bool RemoveSource(string name) {
        if (m_sources.ContainsKey(name)) {
            m_sources.Remove(name);
            return true;
        }
        return false;
    }
    public void RemoveAllSources() {
        m_sources.Clear();
    }

    // *** Operators
    public DirectionalSource this[string str]
    {
        get { return m_sources[str]; }
        set { m_sources[str] = value; }
    }
}