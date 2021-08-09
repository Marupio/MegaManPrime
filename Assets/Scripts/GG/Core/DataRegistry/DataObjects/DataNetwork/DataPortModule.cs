using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPortModule<I, O> : ObjHeader, IDataPortModule<I, O> where I : DataObjList where O : DataObjList
{
    // TODO - Refactor to be a Dictionary<string, DataPortProfile> - simplify
    Dictionary<string, DataPortProfile> m_profiles;
    protected bool m_enabled = true;

    // Dictionary<string, int> m_index; // Contains key=profileName, value=profileIndex
    // protected List<DataPortProfile> m_profiles;
    // protected int m_activeProfileIndex;
    protected DataPortProfile m_activeProfile;
    protected ActiveDataPortConnections<I, O> m_connections;
    protected DataProfileActivityTracker<I, O> m_tracker;

    public bool Enabled { get=>m_enabled; set=>m_enabled=value; }
    public Dictionary<string, DataPortProfile> Profiles { get=>m_profiles; set=>m_profiles=value; }
    public int NProfiles { get=>m_profiles.Count; }
    public DataPortProfile ActiveProfile { get=>m_activeProfile; }
    public ActiveDataPortConnections<I, O> Connections { get=>m_connections; }
    public DataProfileActivityTracker<I, O> Tracker { get=>m_tracker; }
    public bool Ready { get=>m_connections.Ready; }
    /// <summary>
    /// Sets the active profile to the given name
    /// </summary>
    /// <returns>True if profile exists. False indicates no such profile exists, nothing has changed.</returns>
    public bool SetActiveProfileName(string profileName) {
        DataPortProfile retrieve;
        if (m_profiles.TryGetValue(profileName, out retrieve)) {
            m_activeProfile = retrieve;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Sets the active profile to the given profile.
    /// </summary>
    /// <returns>True if success, false if profile does not exist on this module</returns>
    public bool SetActiveProfile(DataPortProfile profile) {
        if (m_profiles.ContainsKey(profile.Name)) {
            if (profile != m_profiles[profile.Name]) {
                // Name exists, but it is not the same profile
                return false;
            }
            m_activeProfile = profile;
            return true;
        }
        return false;
    }
    public void UnsetActiveProfile() {
        ClearActiveProfile();
    }
    public bool InputTracking { get=>m_tracker.InputEnabled; set=>m_tracker.InputEnabled = value; }
    public bool OutputTracking { get=>m_tracker.OutputEnabled; set=>m_tracker.OutputEnabled = value; }

    // Select methods to pass-through to List
    public int Count { get=>m_profiles.Count; }
    /// <summary>
    /// Add a profile
    /// </summary>
    /// <returns>True if added, false if already exists</returns>
    public bool Add(DataPortProfile profile) {
        DataPortProfile localProfile;
        if (m_profiles.TryGetValue(profile.Name, out localProfile)) {
            // Name already exists
            if (localProfile == profile) {
                // All is okay, but nothing was added
                return false;
            }
            if (m_activeProfile == localProfile) {
                // We are about to overwrite the active profile
                Debug.LogError(
                    "DataPortModule " + Name + " already has a different profile named " + profile.Name + ", and it is active.  Overwriting and " +
                    "clearing active."
                );
                ClearActiveProfile();
            } else {
                Debug.LogWarning("DataPortModule " + Name + " already has a different profile named " + profile.Name + ", overwriting.");
            }
        }
        m_profiles[profile.Name] = profile;
        return true;
    }
    /// <summary>
    /// Add a range of profiles
    /// </summary>
    /// <returns>The number actually added, including ovewrites</returns>
    public int AddRange(IEnumerable<DataPortProfile> profiles) {
        int nAdded = 0;
        int nOverwritten = 0;
        bool activeOvewritten = false;
        string collisionNames = "";
        foreach(DataPortProfile profile in profiles) {
            DataPortProfile localProfile;
            if (m_profiles.TryGetValue(profile.Name, out localProfile)) {
                // A profile already exists under this name
                if (profile == localProfile) {
                    // All is okay, but nothing added
                    continue;
                }
                if (localProfile == m_activeProfile) {
                    // We are overwriting the active profile
                    activeOvewritten = true;
                    ClearActiveProfile();
                }
                ++nOverwritten;
                collisionNames += " " + profile.Name;
            }
            ++nAdded;
            m_profiles[profile.Name] = profile;
        }
        if (nOverwritten > 0) {
            if (activeOvewritten) {
                Debug.LogError(
                    nOverwritten + " profile names already existed and were overwritten:" + collisionNames + ", including the active, which was " +
                    "cleared."
                );
            } else {
                Debug.LogWarning(nOverwritten + " profile names already existed and were overwritten:" + collisionNames + ".");
            }
        }
        return nAdded;
    }
    /// <summary>
    /// Remove the given profile
    /// </summary>
    /// <returns>True if profile was removed, false if profile does not exist here</returns>
    public bool Remove(DataPortProfile profile) {
        DataPortProfile localProfile;
        if (m_profiles.TryGetValue(profile.Name, out localProfile)) {
            if (localProfile == profile) {
                m_profiles.Remove(profile.Name);
                if (profile == m_activeProfile) {
                    ClearActiveProfile();
                }
                return true;
            } else {
                Debug.LogWarning("Profile named " + profile.Name + " but it is not the same.  No profiles removed.");
            }
        }
        return false;
    }
    /// <summary>
    /// Remove all profiles that meet the given predicate
    /// </summary>
    /// <returns>The number of profiles removed</returns>
    public int RemoveAll(Predicate<DataPortProfile> predicate) {
        List<string> keys = new List<string>(m_profiles.Count);
        foreach(KeyValuePair<string, DataPortProfile> entry in m_profiles) {
            if (predicate(entry.Value)) {
                keys.Add(entry.Key);
            }
        }
        foreach(string key in keys) {
            if (m_profiles[key] == m_activeProfile) {
                ClearActiveProfile();
            }
            m_profiles.Remove(key);
        }
        return keys.Count;
    }
    /// <summary>
    /// Set the active profile to the null DataPortProfile
    /// </summary>
    public void ClearActiveProfile() {
        m_activeProfile = DataPortProfile.Null;
        m_connections.ChangeProfileAndDetachAll(m_activeProfile);
    }
    public bool Contains(string name) {
        return m_profiles.ContainsKey(name);
    }
    public bool Contains(DataPortProfile profile) {
        return m_profiles.ContainsValue(profile);
    }
    public DataPortProfile this[string name] {
        get {
            return m_profiles[name];
        }
        set {
            DataPortProfile localProfile;
            if (m_profiles.TryGetValue(name, out localProfile)) {
                if (localProfile == value) {
                    // Already set, nothing to do
                    return;
                }
                if (localProfile == m_activeProfile) {
                    ClearActiveProfile();
                }
            }
            m_profiles[name] = value;
        }
    }

    /// <summary>
    /// Initialise fields
    /// </summary>
    protected void InternalNullInit() {
        m_profiles = new Dictionary<string, DataPortProfile>();
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
    protected Dictionary<string, DataPortProfile> InternalCheckValidProfile(IEnumerable<DataPortProfile> profiles, ActiveDataPortConnections<I, O> connections) {
        Dictionary<string, DataPortProfile> returnProfiles = new Dictionary<string, DataPortProfile>();
        foreach(DataPortProfile profile in profiles) {
            returnProfiles.Add(profile.Name, profile);
        }
        if (connections == null || connections.Profile == DataPortProfile.Null || returnProfiles.ContainsValue(connections.Profile)) {
            return returnProfiles;
        }
        string connectionsProfileName = connections.Profile == null ? "null" : connections.Profile.Name;
        Debug.LogException(
            new System.DataMisalignedException(
                "ActiveDataPortConnections active profile '" + connectionsProfileName + "' is not contained in the provided profile list."
            )
        );
        // Error condition - tolerant way to handle it - add connections.Profile to list
        returnProfiles.Add(connections.Profile.Name, connections.Profile);
        return returnProfiles;
    }

    // *** Constructors
    /// <summary>
    /// Given a single profile, assume it is active
    /// </summary>
    public DataPortModule(DataPortProfile profile, ActiveDataPortConnections<I, O> connections = null) {
        if (profile == null) {
            if (connections != null) {
                m_profiles = new Dictionary<string, DataPortProfile>(){{connections.Profile.Name, connections.Profile}};
                m_activeProfile = connections.Profile;
                m_connections = connections;
                m_tracker = new DataProfileActivityTracker<I, O>(this);
                return;
            }
            InternalNullInit();
            return;
        }
        if (connections != null) {
            // profile != null, connections != null --> Both are valid, check for conflict
            DataPortProfile validProfile = InternalCheckValidProfile(profile, connections);
            if (validProfile == null) {
                // Error condition - tolerant way to handle this is to add both and set active the 'connections' profile
                m_profiles = new Dictionary<string, DataPortProfile>(){{connections.Profile.Name, connections.Profile}};
                m_activeProfile = connections.Profile;
                m_connections = connections;
                m_tracker = new DataProfileActivityTracker<I, O>(this);
                return;
            }
            m_profiles = new Dictionary<string, DataPortProfile>(){{profile.Name, profile}};
            m_activeProfile = profile;
            m_connections = connections;
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        }
        // profile != null, connections == null --> Get all data from profile
        m_profiles = new Dictionary<string, DataPortProfile>(){{profile.Name, profile}};;
        m_activeProfile = profile;
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
            m_activeProfile = m_connections.Profile;
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        }
        m_activeProfile = DataPortProfile.Null;
        if (m_profiles.Count > 0) {
            m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
            m_tracker = new DataProfileActivityTracker<I, O>(this);
            return;
        }
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Construct from profile list and active profile indicator (name)
    /// </summary>
    public DataPortModule(IEnumerable<DataPortProfile> profiles, string activeProfileName) {
        m_profiles = new Dictionary<string, DataPortProfile>();
        foreach(DataPortProfile profile in profiles) {
            m_profiles.Add(profile.Name, profile);
        }
        if (!m_profiles.TryGetValue(activeProfileName, out m_activeProfile)) {
            // Name not found
            Debug.LogException(new System.ArgumentOutOfRangeException("Profile name " + activeProfileName + " not found in list, setting to null."));
            m_activeProfile = DataPortProfile.Null;
        }
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Construct from profile list and active profile indicator (profile instance)
    /// </summary>
    public DataPortModule(IEnumerable<DataPortProfile> profiles, DataPortProfile activeProfile) {
        m_profiles = new Dictionary<string, DataPortProfile>();
        foreach(DataPortProfile profile in profiles) {
            m_profiles.Add(profile.Name, profile);
        }
        if (activeProfile != null && activeProfile != DataPortProfile.Null) {
            if (!m_profiles.ContainsValue(activeProfile)) {
                m_profiles.Add(activeProfile.Name, activeProfile);
                m_activeProfile = activeProfile;
            }
        } else {
            m_activeProfile = DataPortProfile.Null;
        }
        m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
    /// <summary>
    /// Copy constructor, with ActiveDataPortConnections, as we won't copy variable references
    /// </summary>
    public DataPortModule(DataPortModule<I, O> module, ActiveDataPortConnections<I, O> connections = null) {
        m_profiles = new Dictionary<string, DataPortProfile>(module.m_profiles);
        if (connections != null) {
            m_connections = connections;
            m_activeProfile = m_connections.Profile;
        } else {
            m_activeProfile = DataPortProfile.Null;
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
                m_profiles = new Dictionary<string, DataPortProfile>{{connections.Profile.Name, connections.Profile}};
                m_activeProfile = connections.Profile;
                m_tracker = new DataProfileActivityTracker<I, O>(this);
                return;
            }
        } else {
            m_connections = new ActiveDataPortConnections<I, O>(DataPortProfile.Null);
        }
        m_profiles = new Dictionary<string, DataPortProfile>();
        m_activeProfile = DataPortProfile.Null;
        m_tracker = new DataProfileActivityTracker<I, O>(this);
    }
}
