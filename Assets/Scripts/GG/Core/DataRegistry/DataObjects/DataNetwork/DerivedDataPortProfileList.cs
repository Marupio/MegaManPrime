// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// /// <summary>
// /// I manage a list of possible DataPortProfiles, letting everyone know which is the "active" profile.  For the active profile, I have an
// /// ActiveDataPortConnections object that holds the actual data objects connected to me.
// /// </summary>
// public class DerivedDataPortProfileList : DataPortProfileList {
//     ActiveDerivedDataPortConnections m_derivedConnections;
//     public ActiveDerivedDataPortConnections DerivedConnections { get=>m_derivedConnections; }

//     // *** Constructors
//     public DerivedDataPortProfileList(DataPortProfile dpp, DerivedActiveDataPortConnections connections = null) {
//         m_profiles = new List<DataPortProfile>{dpp};
//         m_activeProfileIndex = 0;
//         m_activeProfile = dpp;
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new DerivedActiveDataPortConnections(dpp);
//         }
//     }
//     public DerivedDataPortProfileList(List<DataPortProfile> dpps, int activeProfileIndex = 0, DerivedActiveDataPortConnections connections = null) {
//         m_profiles = dpps;
//         m_activeProfileIndex = activeProfileIndex;
//         m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new DerivedActiveDataPortConnections(m_activeProfile);
//         }
//     }
//     public DerivedDataPortProfileList(DerivedDataPortProfileList dpl, DerivedActiveDataPortConnections connections = null) {
//         m_profiles = new List<DataPortProfile>(dpl.m_profiles);
//         m_activeProfileIndex = dpl.m_activeProfileIndex;
//         m_activeProfile = m_activeProfileIndex < 0 ? DataPortProfile.Null : m_profiles[m_activeProfileIndex];
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new DerivedActiveDataPortConnections(m_activeProfile);
//         }
//     }
//     public DerivedDataPortProfileList(DerivedActiveDataPortConnections connections = null) {
//         m_profiles = new List<DataPortProfile>();
//         m_activeProfileIndex = -1;
//         m_activeProfile = DataPortProfile.Null;
//         if (connections != null) {
//             m_connections = connections;
//         } else {
//             m_connections = new DerivedActiveDataPortConnections();
//         }
//     }
// }
