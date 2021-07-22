using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// I manage a list of possible DataPortProfiles, letting everyone know which is the "active" profile.  For the active profile, I have an
/// ActiveDataPortConnections object that holds the actual data objects connected to me.
/// </summary>
public class DataPortProfileList /*: IEnumerable<DataPortProfile>*/ {
    public List<DataPortProfile> m_profiles;
    int m_activeProfileIndex;
    DataPortProfile m_activeProfile;
    ActiveDataPortConnections m_connections;

    public DataPortProfile ActiveProfile { get=>m_activeProfile; }
    public ActiveDataPortConnections Connections { get=>m_connections; }
    public bool SetActiveProfileAt(int index) {
        if (index < m_profiles.Count && index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        return false;
    }
    public bool SetActiveProfileName(string profileName) {
        int index = NameToIndex(profileName);
        if (index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        return false;
    }
    public bool SetActiveProfile(DataPortProfile dpp) {
        int index = m_profiles.IndexOf(dpp);
        if (index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        // Add it first
        m_profiles.Add(dpp);
        InternalSetIndex(m_profiles.Count - 1);
        return true;
    }
    public void UnsetActiveProfile() {
        ClearActiveProfile();
    }

    // Select methods to pass-through to List
    public int Count { get=>m_profiles.Count; }
    public void Add(DataPortProfile dpp) {
        m_profiles.Add(dpp);
    }
    public void AddRange(List<DataPortProfile> dpps) {
        m_profiles.AddRange(dpps);
    }
    public void Remove(DataPortProfile dpp) {
        int index = m_profiles.IndexOf(dpp);
        if (index >= 0) {
            if (index == m_activeProfileIndex) {
                m_activeProfileIndex = -1;
                ClearActiveProfile();
            } else if (index < m_activeProfileIndex) {
                --m_activeProfileIndex;
            }
        }
        m_profiles.Remove(dpp);
    }

    public void ClearActiveProfile() {
        m_activeProfile = DataPortProfile.Null;
        m_connections.ChangeProfileAndDetachAll(m_activeProfile);
    }

    public void RemoveAll(Predicate<DataPortProfile> predicate) {
        m_profiles.RemoveAll(predicate);
        m_activeProfileIndex = m_profiles.IndexOf(m_activeProfile);
        if (m_activeProfileIndex < 0) {
            ClearActiveProfile();
        }
    }
    public void RemoveAt(int index) {
        if (index < 0 || index > m_profiles.Count) {
            Debug.LogError("Index " + index + " out of range [0.." + (m_profiles.Count-1) + "]");
            return;
        }
        if (index == m_activeProfileIndex) {
            m_activeProfileIndex = -1;
            ClearActiveProfile();
        } else if (index > m_activeProfileIndex) {
            --m_activeProfileIndex;
        }
        m_profiles.RemoveAt(index);
    }

    public int NameToIndex(string name) {
        return m_profiles.IndexOf(m_profiles.Where(p => p.Name == name).FirstOrDefault());
    }
    public bool Contains(string name) {
        return NameToIndex(name) >= 0;
    }
    public bool Contains(DataPortProfile profile) {
        return m_profiles.Contains(profile);
    }
    public DataPortProfile this[int index] {
        get { return m_profiles[index]; }
        set {
            m_profiles[index] = value;
            if (index == m_activeProfileIndex) {
                m_activeProfile = value;
            }
        }
    }
    public DataPortProfile this[string name] {
        get {
            int index = NameToIndex(name);
            return index >= 0 ? m_profiles[index] : null;
        }
        set {
            int index = NameToIndex(name);
            if (index < 0) {
                Debug.LogError(name + " not in DataProfileList");
                return;
            }
            m_profiles[index] = value;
            if (index == m_activeProfileIndex) {
                m_activeProfile = value;
            }
        }
    }

    // I think I shouldn't allow this - I need to control access - for m_activeProfile data
    // *** IEnumerable interface
    // public IEnumerator<DataPortProfile> GetEnumerator()
    // {
    //     return m_profiles.GetEnumerator();
    // }
    // IEnumerator IEnumerable.GetEnumerator()
    // {
    //     return this.GetEnumerator();
    // }

    protected void InternalSetIndex(int index) { // No checks
        m_activeProfileIndex = index;
        m_activeProfile = m_profiles[index];
        m_connections.ChangeProfileAndDetachAll(m_activeProfile);
    }

    // *** Constructors
    public DataPortProfileList(DataPortProfile dpp) {
        m_profiles = new List<DataPortProfile>{dpp};
        m_activeProfileIndex = 0;
        m_activeProfile = dpp;
        m_connections = new ActiveDataPortConnections(dpp);
    }
    public DataPortProfileList(List<DataPortProfile> dpps, int activeProfileIndex = 0) {
        m_profiles = dpps;
        m_activeProfileIndex = activeProfileIndex;
        m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
        m_connections = new ActiveDataPortConnections(m_activeProfile);
    }
    public DataPortProfileList(DataPortProfileList dpl) {
        m_profiles = new List<DataPortProfile>(dpl.m_profiles);
        m_activeProfileIndex = dpl.m_activeProfileIndex;
        m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
        m_connections = new ActiveDataPortConnections(m_activeProfile);
    }
    public DataPortProfileList() {
        m_profiles = new List<DataPortProfile>();
        m_activeProfileIndex = -1;
        m_activeProfile = DataPortProfile.Null;
    }
}