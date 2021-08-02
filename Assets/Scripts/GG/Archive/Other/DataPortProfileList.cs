// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// /// <summary>
// /// I manage a list of possible DataPortProfiles, letting everyone know which is the "active" profile.  For the active profile, I have an
// /// ActiveDataPortConnections object that holds the actual data objects connected to me.
// /// </summary>
// public class DataPortProfileList<I, O> : ObjHeader, IEnumerable<DataPortProfile>
//     where I : class, IObjPass<IDataObjMeta>
//     where O : class, IObjPass<IDataObjMeta>
// {
//     public List<DataPortProfile> m_profiles;
//     protected int m_activeProfileIndex;
//     protected DataPortProfile m_activeProfile;
//     protected ActiveDataPortConnections<I, O> m_connections;
//     protected DataProfileActivityTracker<I, O> m_tracker;
//     protected bool m_enabled = true;

//     public List<DataPortProfile> Profiles { get=>m_profiles; set=>m_profiles=value; }
//     public int NProfiles { get=>m_profiles.Count; }
//     public DataPortProfile ActiveProfile { get=>m_activeProfile; }
//     public ActiveDataPortConnections<I, O> Connections { get=>m_connections; }
//     public DataProfileActivityTracker<I, O> Tracker { get=>m_tracker; }
//     public bool Enabled { get=>m_enabled; set=>m_enabled=value; }

//     public bool SetActiveProfileAt(int index) {
//         if (index < m_profiles.Count && index >= 0) {
//             InternalSetIndex(index);
//             return true;
//         }
//         return false;
//     }
//     public bool SetActiveProfileName(string profileName) {
//         int index = NameToIndex(profileName);
//         if (index >= 0) {
//             InternalSetIndex(index);
//             return true;
//         }
//         return false;
//     }
//     public bool SetActiveProfile(DataPortProfile dpp) {
//         int index = m_profiles.IndexOf(dpp);
//         if (index >= 0) {
//             InternalSetIndex(index);
//             return true;
//         }
//         // Add it first
//         m_profiles.Add(dpp);
//         InternalSetIndex(m_profiles.Count - 1);
//         return true;
//     }
//     public void UnsetActiveProfile() {
//         ClearActiveProfile();
//     }
//     public bool InputTracking { get=>m_tracker.InputEnabled; set=>m_tracker.InputEnabled = value; }
//     public bool OutputTracking { get=>m_tracker.OutputEnabled; set=>m_tracker.OutputEnabled = value; }

//     // Select methods to pass-through to List
//     public int Count { get=>m_profiles.Count; }
//     public void Add(DataPortProfile dpp) {
//         m_profiles.Add(dpp);
//     }
//     public void AddRange(IEnumerable<DataPortProfile> dpps) {
//         m_profiles.AddRange(dpps);
//     }
//     public void Remove(DataPortProfile dpp) {
//         int index = m_profiles.IndexOf(dpp);
//         if (index >= 0) {
//             if (index == m_activeProfileIndex) {
//                 m_activeProfileIndex = -1;
//                 ClearActiveProfile();
//             } else if (index < m_activeProfileIndex) {
//                 --m_activeProfileIndex;
//             }
//         }
//         m_profiles.Remove(dpp);
//     }

//     public void RemoveAll(Predicate<DataPortProfile> predicate) {
//         m_profiles.RemoveAll(predicate);
//         m_activeProfileIndex = m_profiles.IndexOf(m_activeProfile);
//         if (m_activeProfileIndex < 0) {
//             ClearActiveProfile();
//         }
//     }
//     public void RemoveAt(int index) {
//         if (index < 0 || index > m_profiles.Count) {
//             Debug.LogError("Index " + index + " out of range [0.." + (m_profiles.Count-1) + "]");
//             return;
//         }
//         if (index == m_activeProfileIndex) {
//             m_activeProfileIndex = -1;
//             ClearActiveProfile();
//         } else if (index > m_activeProfileIndex) {
//             --m_activeProfileIndex;
//         }
//         m_profiles.RemoveAt(index);
//     }

//     public void ClearActiveProfile() {
//         m_activeProfile = DataPortProfile.Null;
//         m_connections.ChangeProfileAndDetachAll(m_activeProfile);
//     }

//     public int NameToIndex(string name) {
//         return m_profiles.IndexOf(m_profiles.Where(p => p.Name == name).FirstOrDefault());
//     }
//     public bool Contains(string name) {
//         return NameToIndex(name) >= 0;
//     }
//     public bool Contains(DataPortProfile profile) {
//         return m_profiles.Contains(profile);
//     }
//     public DataPortProfile this[int index] {
//         get { return m_profiles[index]; }
//         set {
//             m_profiles[index] = value;
//             if (index == m_activeProfileIndex) {
//                 m_activeProfile = value;
//             }
//         }
//     }
//     public DataPortProfile this[string name] {
//         get {
//             int index = NameToIndex(name);
//             return index >= 0 ? m_profiles[index] : null;
//         }
//         set {
//             int index = NameToIndex(name);
//             if (index < 0) {
//                 Debug.LogError(name + " not in DataProfileList");
//                 return;
//             }
//             m_profiles[index] = value;
//             if (index == m_activeProfileIndex) {
//                 m_activeProfile = value;
//             }
//         }
//     }

//     // *** IEnumerable interface
//     public IEnumerator<DataPortProfile> GetEnumerator()
//     {
//         return m_profiles.GetEnumerator();
//     }
//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return this.GetEnumerator();
//     }

//     protected void InternalSetIndex(int index) {
//         m_activeProfileIndex = index;
//         m_activeProfile = m_profiles[index];
//         m_connections.ChangeProfileAndDetachAll(m_activeProfile);
//     }

//     // *** Constructors
//     /// <summary>
//     /// Given a single profile, assume it is active
//     /// </summary>
//     public DataPortProfileList(DataPortProfile dpp, ActiveDataPortConnections<I, O> connections = null) {
//         m_profiles = new List<DataPortProfile>{dpp};
//         m_activeProfileIndex = 0;
//         m_activeProfile = dpp;
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new ActiveDataPortConnections<I, O>(dpp);
//         }
//         m_tracker = new DataProfileActivityTracker<I, O>(this);
//     }
//     /// <summary>
//     /// Construct from profile list, active profile indicator (index), and connections, if available
//     /// </summary>
//     public DataPortProfileList(IEnumerable<DataPortProfile> profiles, int activeProfileIndex = 0, ActiveDataPortConnections<I, O> connections = null) {
//         m_profiles = new List<DataPortProfile>(profiles);
//         m_activeProfileIndex = activeProfileIndex;
//         m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
//         }
//         m_tracker = new DataProfileActivityTracker<I, O>(this);
//     }
//     /// <summary>
//     /// Construct from profile list, active profile indicator (name), and connections, if available
//     /// </summary>
//     public DataPortProfileList(IEnumerable<DataPortProfile> profiles, string activeProfileName, ActiveDataPortConnections<I, O> connections = null) {
//         m_profiles = new List<DataPortProfile>(profiles);
//         m_activeProfileIndex = NameToIndex(activeProfileName);
//         m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
//         }
//         m_tracker = new DataProfileActivityTracker<I, O>(this);
//     }
//     /// <summary>
//     /// Construct from profile list, active profile indicator (profile instance), and connections, if available
//     /// </summary>
//     public DataPortProfileList(IEnumerable<DataPortProfile> profiles, DataPortProfile activeProfile, ActiveDataPortConnections<I, O> connections = null) {
//         m_profiles = new List<DataPortProfile>(profiles);
//         m_activeProfileIndex = m_profiles.IndexOf(activeProfile);
//         m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
//         }
//         m_tracker = new DataProfileActivityTracker<I, O>(this);
//     }
//     /// <summary>
//     /// Copy constructor, with ActiveDataPortConnections, as we won't copy variable references
//     /// </summary>
//     public DataPortProfileList(DataPortProfileList<I, O> dpl, ActiveDataPortConnections<I, O> connections = null) {
//         m_profiles = new List<DataPortProfile>(dpl.m_profiles);
//         m_activeProfileIndex = dpl.m_activeProfileIndex;
//         m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
//         }
//         m_tracker = new DataProfileActivityTracker<I, O>(this);
//     }
//     /// <summary>
//     /// Null constructor, with ActiveDataPortConnections, if necessary
//     /// </summary>
//     public DataPortProfileList(ActiveDataPortConnections<I, O> connections = null) {
//         m_profiles = new List<DataPortProfile>();
//         m_activeProfileIndex = -1;
//         m_activeProfile = DataPortProfile.Null;
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new ActiveDataPortConnections<I, O>(m_activeProfile);
//         }
//         m_tracker = new DataProfileActivityTracker<I, O>(this);
//     }
// }
