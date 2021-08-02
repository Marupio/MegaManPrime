using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPortModule<I, O> : ObjHeader, IDataPortModule<I, O>, IEnumerable<DataPortProfile>
    where I : class, IObjPass<IDataObjMeta>
    where O : class, IObjPass<IDataObjMeta>
{
    // bool Enabled { get; set; }
    // DataPortProfileList<I, O> Profiles { get; set; }
    // int NProfiles { get; }
    // DataPortProfile ActiveProfile { get; }
    // ActiveDataPortConnections<I, O> Connections { get; }
    // bool Ready { get; }
    // protected DataPortProfileList<I, O> m_profiles;
    // protected bool m_enabled = true;

    // public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    // public DataPortProfileList<I, O> Profiles { get=>m_profiles; set=>m_profiles=value; }
    // public int NProfiles { get=>m_profiles.Count; }
    // public DataPortProfile ActiveProfile { get=>m_profiles.ActiveProfile; }
    // public ActiveDataPortConnections<I, O> Connections { get=>m_profiles.Connections; }

    protected bool m_enabled = true;
    public List<DataPortProfile> m_profiles;
    protected int m_activeProfileIndex;
    protected DataPortProfile m_activeProfile;
    protected ActiveDataPortConnections<I, O> m_connections;
    protected DataProfileActivityTracker<I, O> m_tracker;

    public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    public List<DataPortProfile> Profiles { get=>m_profiles; set=>m_profiles=value; }
    public int NProfiles { get=>m_profiles.Count; }
    public DataPortProfile ActiveProfile { get=>m_activeProfile; }
    public ActiveDataPortConnections<I, O> Connections { get=>m_connections; }
    public DataProfileActivityTracker<I, O> Tracker { get=>m_tracker; }
    public bool Ready { get=>m_connections.Ready; }

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
    /// <summary>
    /// Sets the active profile to the given profile.  Adds it to the profile list if it is missing.
    /// </summary>
    public bool SetActiveProfile(DataPortProfile profile) {
        int index = m_profiles.IndexOf(profile);
        if (index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        // Add it first
        m_profiles.Add(profile);
        InternalSetIndex(m_profiles.Count - 1);
        return true;
    }
    public void UnsetActiveProfile() {
        ClearActiveProfile();
    }
    public bool InputTracking { get=>m_tracker.InputEnabled; set=>m_tracker.InputEnabled = value; }
    public bool OutputTracking { get=>m_tracker.OutputEnabled; set=>m_tracker.OutputEnabled = value; }

    // Select methods to pass-through to List
    public int Count { get=>m_profiles.Count; }
    public void Add(DataPortProfile profile) {
        m_profiles.Add(profile);
    }
    public void AddRange(IEnumerable<DataPortProfile> profiles) {
        m_profiles.AddRange(profiles);
    }
    public void Remove(DataPortProfile profile) {
        int index = m_profiles.IndexOf(profile);
        if (index >= 0) {
            if (index == m_activeProfileIndex) {
                m_activeProfileIndex = -1;
                ClearActiveProfile();
            } else if (index < m_activeProfileIndex) {
                --m_activeProfileIndex;
            }
        }
        m_profiles.Remove(profile);
    }

    public void RemoveAll(Predicate<DataPortProfile> predicate) {
        m_profiles.RemoveAll(predicate);
        m_activeProfileIndex = m_profiles.IndexOf(m_activeProfile);
        if (m_activeProfileIndex < 0) {
            ClearActiveProfile();
        }
    }
    public void RemoveAt(int index) {
        if (index < 0 || index >= m_profiles.Count) {
                Debug.LogException(new System.IndexOutOfRangeException("Index " + index + " out of range [0.." + (m_profiles.Count-1) + "]"));
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

    public void ClearActiveProfile() {
        m_activeProfile = DataPortProfile.Null;
        m_connections.ChangeProfileAndDetachAll(m_activeProfile);
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
            if (index == m_profiles.Count) {
                // Treat it as an append
                m_profiles.Add(value);
                return;
            } else if (index < 0 || index > m_profiles.Count) {
                Debug.LogException(new System.IndexOutOfRangeException("Index " + index + " out of range [0.." + (m_profiles.Count-1) + "]"));
                return;
            }
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
                // Treat it as an append
                m_profiles.Add(value);
                return;
            }
            m_profiles[index] = value;
            if (index == m_activeProfileIndex) {
                m_activeProfile = value;
            }
        }
    }

    // *** IEnumerable interface
    public IEnumerator<DataPortProfile> GetEnumerator()
    {
        return m_profiles.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    protected void InternalSetIndex(int index) {
        // No checking - this is internal, all checking is done
        m_activeProfileIndex = index;
        m_activeProfile = m_profiles[index];
        m_connections.ChangeProfileAndDetachAll(m_activeProfile);
    }
    protected void InternalNullInit() {
        m_profiles = new List<DataPortProfile>();
        m_activeProfileIndex = -1;
        m_activeProfile = DataPortProfile.Null;
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    protected DataPortProfile InternalCheckValidProfile(DataPortProfile profile, ActiveDataPortConnections<I, O> connections) {
        if (profile == DataPortProfile.Null) {
            if (connections.Profile == DataPortProfile.Null) {
                return DataPortProfile.Null;
            }
            return connections.Profile;
        }
        if (connections.Profile == DataPortProfile.Null) {
            return profile;
        }
        if (profile == connections.Profile) {
            return profile;
        }
        string connectionsProfileName = connections.Profile == null ? "null" : connections.Profile.Name;
        string profileName = profile == null ? "null" : profile.Name;
        Debug.LogException(
            new System.DataMisalignedException(
                "ActiveDataPortConnections active profile '" + connectionsProfileName + "' is not the same as the provided profile '" +
                profileName + "'."
            )
        );
        return null;
    }
    protected List<DataPortProfile> InternalCheckValidProfile(IEnumerable<DataPortProfile> profiles, ActiveDataPortConnections<I, O> connections) {
        List<DataPortProfile> profileList = new List<DataPortProfile>(profiles);
        if (connections == null || connections.Profile == DataPortProfile.Null || profileList.Contains(connections.Profile)) {
            return profileList;
        }
        string connectionsProfileName = connections.Profile == null ? "null" : connections.Profile.Name;
        Debug.LogException(
            new System.DataMisalignedException(
                "ActiveDataPortConnections active profile '" + connectionsProfileName + "' is not contained in the provided profile list."
            )
        );
        // Error condition - tolerant way to handle it - add connections.Profile to list
        profileList.Add(connections.Profile);
        return profileList;
    }

    // *** Constructors
    /// <summary>
    /// Given a single profile, assume it is active
    /// </summary>
    public DataPortModule(DataPortProfile profile, ActiveDataPortConnections<I, O> connections = null) {
        if (profile == null) {
            if (connections != null) {
                // profile == null, connections != null --> Get everything from connections
                m_profiles = new List<DataPortProfile>{connections.Profile};
                m_activeProfileIndex = 0;
                m_activeProfile = connections.Profile;
                m_connections = connections;
                m_tracker = new DataProfileActivityTracker<I, O>(this);
                return;
            }
            //  profile == null, connections == null --> All are null, basically the null constructor
            InternalNullInit();
            return;
        }
        if (connections != null) {
            // profile != null, connections != null --> Both are valid, check for conflict
            DataPortProfile validProfile = InternalCheckValidProfile(profile, connections);
            if (validProfile == null) {
                // Error condition - tolerant way to handle this is to add both and set active the 'connections' profile
                m_profiles = new List<DataPortProfile>{connections.Profile, profile};
                m_activeProfileIndex = 0;
                m_activeProfile = m_profiles[m_activeProfileIndex];
                m_connections = connections;
                m_tracker = new DataProfileActivityTracker<I, O>(this);
                return;
            }
            m_profiles = new List<DataPortProfile>{profile};
            m_activeProfileIndex = 0;
            m_activeProfile = m_profiles[m_activeProfileIndex];
            m_connections = connections;
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        }
        // profile != null, connections == null --> Get all data from profile
        m_profiles = new List<DataPortProfile>{profile};
        m_activeProfileIndex = 0;
        m_activeProfile = m_profiles[m_activeProfileIndex];
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Construct from profile list and optional connections.  If connections is not available, assume index 0 is active.
    /// </summary>
    public DataPortModule(IEnumerable<DataPortProfile> profiles, ActiveDataPortConnections<I, O> connections = null) {
        m_profiles = InternalCheckValidProfile(profiles, connections);
        if (connections != null) {
            m_connections = connections;
            m_activeProfileIndex = m_profiles.IndexOf(m_connections.Profile);
            m_activeProfile = m_connections.Profile;
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        }
        if (m_profiles.Count > 0) {
            m_activeProfileIndex = 0;
            m_activeProfile = m_profiles[m_activeProfileIndex];
            m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        }
        m_activeProfileIndex = -1;
        m_activeProfile = DataPortProfile.Null;
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Construct from profile list and active profile indicator (index)
    /// </summary>
    public DataPortModule(IEnumerable<DataPortProfile> profiles, int activeProfileIndex) {
        m_profiles = new List<DataPortProfile>(profiles);
        if (activeProfileIndex == -1) {
            m_activeProfileIndex = activeProfileIndex;
            m_activeProfile = DataPortProfile.Null;
            m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        } else if (activeProfileIndex >= 0 && activeProfileIndex < m_profiles.Count) {
            m_activeProfileIndex = activeProfileIndex;
            m_activeProfile = m_profiles[m_activeProfileIndex];
            m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
            m_tracker = new DataProfileActivityTracker<I, O>(this);
        } else {
            // Bad index
            Debug.LogException(
                new System.IndexOutOfRangeException("Index " + activeProfileIndex + " out of range [0.." + (m_profiles.Count-1) + "]")
            );
            m_activeProfileIndex = -1;
            m_activeProfile = DataPortProfile.Null;
            m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
            m_tracker = new DataProfileActivityTracker<I, O>(this);
        }
    }
    /// <summary>
    /// Construct from profile list and active profile indicator (name)
    /// </summary>
    public DataPortModule(IEnumerable<DataPortProfile> profiles, string activeProfileName) {
        m_profiles = new List<DataPortProfile>(profiles);
        m_activeProfileIndex = NameToIndex(activeProfileName);
        if (m_activeProfileIndex < 0) {
            // Name not found
            Debug.LogException(new System.ArgumentOutOfRangeException("Profile name " + activeProfileName + " not found in list, setting to null."));
            m_activeProfile = DataPortProfile.Null;
        } else {
            m_activeProfile = m_profiles[m_activeProfileIndex];
        }
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Construct from profile list and active profile indicator (profile instance)
    /// </summary>
    public DataPortModule(IEnumerable<DataPortProfile> profiles, DataPortProfile activeProfile) {
        m_profiles = new List<DataPortProfile>(profiles);
        m_activeProfileIndex = m_profiles.IndexOf(activeProfile);
        if (m_activeProfileIndex < 0) {
            // Name not found
            Debug.LogException(new System.ArgumentOutOfRangeException("Profile '" + activeProfile.Name + "' not found in list, appending it."));
            m_activeProfileIndex = m_profiles.Count;
            m_profiles.Add(activeProfile);
        } else {
            m_activeProfile = activeProfile;
        }
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Copy constructor, with ActiveDataPortConnections, as we won't copy variable references
    /// </summary>
    public DataPortModule(DataPortModule<I, O> dpl, ActiveDataPortConnections<I, O> connections = null) {
        m_profiles = new List<DataPortProfile>(dpl.m_profiles);
        if (connections != null) {
            m_connections = connections;
            m_activeProfile = m_connections.Profile;
            m_activeProfileIndex = m_profiles.IndexOf(m_activeProfile);
        } else {
            m_activeProfileIndex = 0;
            m_activeProfile = m_profiles[m_activeProfileIndex];
            m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        }
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Null constructor, with ActiveDataPortConnections, if necessary
    /// </summary>
    public DataPortModule(ActiveDataPortConnections<I, O> connections = null) {
        if (connections != null) {
            m_connections = connections;
            if (connections.Profile != DataPortProfile.Null) {
                m_profiles = new List<DataPortProfile>{connections.Profile};
                m_activeProfileIndex = 0;
                m_activeProfile = m_profiles[m_activeProfileIndex];
                m_tracker = new DataProfileActivityTracker<I, O>(this);
                return;
            }
        } else {
            m_connections = new ActiveDataPortConnections<I, O>(DataPortProfile.Null);
        }
        m_profiles = new List<DataPortProfile>();
        m_activeProfileIndex = -1;
        m_activeProfile = DataPortProfile.Null;
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
}
