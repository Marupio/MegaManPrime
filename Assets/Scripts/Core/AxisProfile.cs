// using System.Collections.Generic;

// public class AxisProfile
// {
//     string m_name;
//     Dictionary<string, AxisSource> m_axisSources;
//     AxisMovement m_axisMovement;
//     bool m_smoothingEnabled;
//     float m_smoothingTime;

//     public string Name { get => m_name; set => m_name = value; }
//     public Dictionary<string, AxisSource> Sources { get=> m_axisSources; }
//     public AxisSource GetSource(string name) { return m_axisSources[name]; }
//     /// <summary>
//     /// Add a new axis source, by name
//     /// </summary>
//     /// <returns>True if source already existed and was overwritten</returns>
//     public bool AddSource(string name, AxisSource newSource) {
//         bool overwritten = false;
//         if (m_axisSources.ContainsKey(name)) {
//             overwritten = true;
//         }
//         m_axisSources.Add(name, newSource);
//         return overwritten;
//     }
//     /// <summary>
//     /// Remove an axis source, by name
//     /// </summary>
//     /// <returns>True if the source existed and was removed</returns>
//     public bool RemoveSource(string name) {
//         if (m_axisSources.ContainsKey(name)) {
//             m_axisSources.Remove(name);
//             return true;
//         }
//         return false;
//     }
//     public int RemoveAllSources() {
//         int nRemoved = m_axisSources.Count;
//         m_axisSources.Clear();
//         return nRemoved;
//     }
//     public AxisMovement AxisMovement { get => m_axisMovement; set => m_axisMovement = value; }
//     public bool SmoothingEnabled
//     {
//         get => m_smoothingEnabled;
//         set { m_smoothingEnabled = InternalSmoothingAllowed() ? value : false; }
//     }
//     public float SmoothingTime
//     {
//         get => m_smoothingTime;
//         set { m_smoothingTime = InternalSmoothingAllowed() ? value : 0;}
//     }

//     public AxisProfile(string name, Dictionary<string, AxisSource> axisSources, AxisMovement axisMovement, bool smoothing)
//     {
//         m_name = name;
//         m_axisSources = axisSources;
//         m_axisMovement = axisMovement;
//         SmoothingEnabled = smoothing;
//     }

//     /// <summary>
//     /// Enforces no smoothing for Instantaneous movement control
//     /// </summary>
//     protected bool InternalSmoothingAllowed()
//     {
//         ImpulseMovement impulseType = m_axisMovement.ImpulseType();
//         if (impulseType != null && impulseType.Instantaneous)
//         {
//             return false;
//         }
//         return true;
//     }
// }
