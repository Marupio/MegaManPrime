// using System.Collections.Generic;
// using UnityEngine;

// /// <summary>
// /// Same as ActiveDataPortConnections except I constrain outputs to be IDerivedDataObjMeta
// /// May have been easier to use a generic type for base, and simply constrain it.
// /// </summary>
// public class ActiveDerivedDataPortConnections : ActiveDataPortConnections {
//     List<IDerivedDataObjMeta> m_outputsAsDerived;

//     // *** Access
//     public List<IDataObjMeta> OutputsAsDerived { get=>m_outputs; }

//     // *** Edit - Switch profiles
//     public override void ChangeProfileAndDetachAll(DataPortProfile profile) {
//         base.ChangeProfileAndDetachAll(profile);
//         InternalChangeProfileAndDetachAll(profile.NOutputs, ref m_outputsAsDerived);
//     }

//     // *** Edit - Attach outputs
//     public override void AttachOutput(IDataObjMeta obj, int port=0) {
//         #if DEBUG
//             if (port > m_outputs.Count || port < 0) {
//                 Debug.LogException(new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_outputs.Count + "]"));
//                 return;
//             }
//             if (m_outputs[port] != null) {
//                 Debug.LogWarning("Overwriting existing port connection " + port);
//             }
//         #endif
//         if (!CheckDerived(obj)) return;
//         m_outputs[port] = obj;
//         m_outputsAsDerived[port] = (IDerivedDataObjMeta)obj;
//     }
//     public override void AttachOutput(IDataObjMeta obj, string outputName) {
//         int port = m_profile.GetOutputPortFromName(outputName);
//         #if DEBUG
//             if (port < 0) {
//                 Debug.LogException(new System.ArgumentOutOfRangeException(outputName + " is not a valid outputName for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         if (!CheckDerived(obj)) return;
//         m_outputs[port] = obj;
//         m_outputsAsDerived[port] = (IDerivedDataObjMeta)obj;
//     }
//     public override void AttachAllOutputs(List<IDataObjMeta> objs) {
//         foreach (IDataObjMeta obj in objs) {
//             if (!CheckDerived(obj)) return;
//         }
//         #if DEBUG
//             if (objs.Count != m_outputs.Count) {
//                 Debug.LogException(new System.ArgumentOutOfRangeException("Cannot attach " + objs.Count + " connections to " + m_outputs.Count + " ports."));
//                 return;
//             }
//             int nOverwrites = 0;
//             foreach (IDataObjMeta obj in m_outputs) {
//                 if (obj != null) {
//                     ++nOverwrites;
//                 }
//             }
//             if (nOverwrites > 0) {
//                 Debug.LogWarning("Ovewriting " + nOverwrites + " existing port connections");
//             }
//         #endif
//         for (int i = 0; i < m_outputs.Count; ++i) {
//             m_outputs[i] = objs[i];
//             m_outputsAsDerived[i] = (IDerivedDataObjMeta)objs[i];
//         }
//     }

//     // *** Edit - Attach all
//     public override void AttachAll(IDataObjMeta obj0) {
//         #if DEBUG
//             int nPorts = m_inputs.Count + m_outputs.Count;
//             if (nPorts != 1) {
//                 Debug.LogException(new System.MissingFieldException("1 connection given for " + nPorts + " ports for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         if (m_inputs.Count > 0) {
//             m_inputs[0] = obj0;
//         } else {
//             if (!CheckDerived(obj0)) return;
//             m_outputs[0] = obj0;
//         }
//     }
//     public override void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1) {
//         #if DEBUG
//             int nPorts = m_inputs.Count + m_outputs.Count;
//             if (nPorts != 2) {
//                 Debug.LogException(new System.MissingFieldException("2 connections given for " + nPorts + " ports for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         switch (m_inputs.Count) {
//             case 0:
//                 if (!CheckDerived(obj0, obj1)) return;
//                 m_outputs[0] = obj0;
//                 m_outputs[1] = obj1;
//                 return;
//             case 1:
//                 m_inputs[0] = obj0;
//                 if (!CheckDerived(obj1)) return;
//                 m_outputs[0] = obj1;
//                 return;
//             default:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 return;
//         }
//     }
//     public override void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2) {
//         #if DEBUG
//             int nPorts = m_inputs.Count + m_outputs.Count;
//             if (nPorts != 3) {
//                 Debug.LogException(new System.MissingFieldException("3 connections given for " + nPorts + " ports for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         switch (m_inputs.Count) {
//             case 0:
//                 if (!CheckDerived(obj0, obj1, obj2)) return;
//                 m_outputs[0] = obj0;
//                 m_outputs[1] = obj1;
//                 m_outputs[2] = obj2;
//                 return;
//             case 1:
//                 m_inputs[0] = obj0;
//                 if (!CheckDerived(obj1, obj2)) return;
//                 m_outputs[0] = obj1;
//                 m_outputs[1] = obj2;
//                 return;
//             case 2:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 if (!CheckDerived(obj2)) return;
//                 m_outputs[0] = obj2;
//                 return;
//             default:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 m_inputs[2] = obj2;
//                 return;
//         }
//     }
//     public override void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3) {
//         #if DEBUG
//             int nPorts = m_inputs.Count + m_outputs.Count;
//             if (nPorts != 4) {
//                 Debug.LogException(new System.MissingFieldException("4 connections given for " + nPorts + " ports for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         switch (m_inputs.Count) {
//             case 0:
//                 if (!CheckDerived(obj0, obj1, obj2, obj3)) return;
//                 m_outputs[0] = obj0;
//                 m_outputs[1] = obj1;
//                 m_outputs[2] = obj2;
//                 m_outputs[3] = obj3;
//                 return;
//             case 1:
//                 m_inputs[0] = obj0;
//                 if (!CheckDerived(obj1, obj2, obj3)) return;
//                 m_outputs[0] = obj1;
//                 m_outputs[1] = obj2;
//                 m_outputs[2] = obj3;
//                 return;
//             case 2:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 if (!CheckDerived(obj2, obj3)) return;
//                 m_outputs[0] = obj2;
//                 m_outputs[1] = obj3;
//                 return;
//             case 3:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 m_inputs[2] = obj2;
//                 if (!CheckDerived(obj3)) return;
//                 m_outputs[0] = obj3;
//                 return;
//             default:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 m_inputs[2] = obj2;
//                 m_inputs[3] = obj3;
//                 return;
//         }
//     }
//     public override void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4) {
//         #if DEBUG
//             int nPorts = m_inputs.Count + m_outputs.Count;
//             if (nPorts != 5) {
//                 Debug.LogException(new System.MissingFieldException("5 connections given for " + nPorts + " ports for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         switch (m_inputs.Count) {
//             case 0:
//                 if (!CheckDerived(obj0, obj1, obj2, obj3, obj4)) return;
//                 m_outputs[0] = obj0;
//                 m_outputs[1] = obj1;
//                 m_outputs[2] = obj2;
//                 m_outputs[3] = obj3;
//                 m_outputs[4] = obj4;
//                 return;
//             case 1:
//                 m_inputs[0] = obj0;
//                 if (!CheckDerived(obj1, obj2, obj3, obj4)) return;
//                 m_outputs[0] = obj1;
//                 m_outputs[1] = obj2;
//                 m_outputs[2] = obj3;
//                 m_outputs[3] = obj4;
//                 return;
//             case 2:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 if (!CheckDerived(obj2, obj3, obj4)) return;
//                 m_outputs[0] = obj2;
//                 m_outputs[1] = obj3;
//                 m_outputs[2] = obj4;
//                 return;
//             case 3:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 m_inputs[2] = obj2;
//                 if (!CheckDerived(obj3, obj4)) return;
//                 m_outputs[0] = obj3;
//                 m_outputs[1] = obj4;
//                 return;
//             case 4:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 m_inputs[2] = obj2;
//                 m_inputs[3] = obj3;
//                 if (!CheckDerived(obj4)) return;
//                 m_outputs[1] = obj4;
//                 return;
//             default:
//                 m_inputs[0] = obj0;
//                 m_inputs[1] = obj1;
//                 m_inputs[2] = obj2;
//                 m_inputs[3] = obj3;
//                 m_inputs[4] = obj4;
//                 return;
//         }
//     }
//     public override void AttachAll(params IDataObjMeta[] objs) {
//         #if DEBUG
//             int nPorts = m_inputs.Count + m_outputs.Count;
//             if (nPorts != objs.Length) {
//                 Debug.LogException(new System.MissingFieldException(objs.Length + " connections given for " + nPorts + " ports for profile " + m_profile.Name));
//                 return;
//             }
//         #endif
//         for (int i = 0; i < m_inputs.Count; ++i) {
//             m_inputs[i] = objs[i];
//         }
//         for (int i = 0; i < m_outputs.Count; ++i) {
//             IDataObjMeta obj = objs[i + m_inputs.Count];
//             if (!CheckDerived(obj)) return;
//             m_outputs[i] = obj;
//         }
//     }

//     // *** Internal methods
//     protected bool CheckDerived(IDataObjMeta obj) {
//         if (obj is IDerivedDataObjMeta == false) {
//             Debug.LogException(new System.InvalidCastException(obj.Name + " is not an IDerivedDataObjMeta, all outputs must be."));
//             return false;
//         }
//         return true;
//     }
//     protected bool CheckDerived(IDataObjMeta obj0, IDataObjMeta obj1) {
//         if (obj0 is IDerivedDataObjMeta == false || obj1 is IDerivedDataObjMeta == false) {
//             Debug.LogException(
//                 new System.InvalidCastException(
//                     obj0.Name + " or " + obj1.Name + " is not an IDerivedDataObjMeta, all outputs must be."
//                 )
//             );
//             return false;
//         }
//         return true;
//     }
//     protected bool CheckDerived(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2) {
//         if (obj0 is IDerivedDataObjMeta == false || obj1 is IDerivedDataObjMeta == false || obj2 is IDerivedDataObjMeta == false) {
//             Debug.LogException(
//                 new System.InvalidCastException(
//                     obj0.Name + " or " + obj1.Name + " or " + obj2.Name + " is not an IDerivedDataObjMeta, all outputs must be."
//                 )
//             );
//             return false;
//         }
//         return true;
//     }
//     protected bool CheckDerived(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3) {
//         if (
//             obj0 is IDerivedDataObjMeta == false || obj1 is IDerivedDataObjMeta == false || obj2 is IDerivedDataObjMeta == false ||
//             obj3 is IDerivedDataObjMeta == false
//         ) {
//             Debug.LogException(
//                 new System.InvalidCastException(
//                     obj0.Name + " or " + obj1.Name + " or " + obj2.Name + " or " + obj3.Name + " is not an IDerivedDataObjMeta, all outputs must be."
//                 )
//             );
//             return false;
//         }
//         return true;
//     }
//     protected bool CheckDerived(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4) {
//         if (
//             obj0 is IDerivedDataObjMeta == false || obj1 is IDerivedDataObjMeta == false || obj2 is IDerivedDataObjMeta == false ||
//             obj3 is IDerivedDataObjMeta == false || obj4 is IDerivedDataObjMeta == false
//         ) {
//             Debug.LogException(
//                 new System.InvalidCastException(
//                     obj0.Name + " or " + obj1.Name + " or " + obj2.Name + " or " + obj3.Name + " or " + obj4.Name +
//                     " is not an IDerivedDataObjMeta, all outputs must be."
//                 )
//             );
//             return false;
//         }
//         return true;
//     }

//     void Init() {
//         m_inputs = new List<IDataObjMeta>();
//         m_outputs = new List<IDataObjMeta>();
//     }

//     // *** Constructors
//     public ActiveDerivedDataPortConnections(DataPortProfile profile) : base(profile) {}
//     public ActiveDerivedDataPortConnections(DataPortProfile profile, IDataObjMeta obj0) : base(profile, obj0) {}
//     public ActiveDerivedDataPortConnections(DataPortProfile profile, IDataObjMeta obj0, IDataObjMeta obj1) : base(profile, obj0, obj1) {}
//     public ActiveDerivedDataPortConnections(
//         DataPortProfile profile,
//         IDataObjMeta obj0,
//         IDataObjMeta obj1,
//         IDataObjMeta obj2
//     ) : base(profile, obj0, obj1, obj2) {}
//     public ActiveDerivedDataPortConnections(
//         DataPortProfile profile,
//         IDataObjMeta obj0,
//         IDataObjMeta obj1,
//         IDataObjMeta obj2,
//         IDataObjMeta obj3
//     ) : base(profile, obj0, obj1, obj2, obj3) {}
//     public ActiveDerivedDataPortConnections(
//         DataPortProfile profile,
//         IDataObjMeta obj0,
//         IDataObjMeta obj1,
//         IDataObjMeta obj2,
//         IDataObjMeta obj3,
//         IDataObjMeta obj4
//     ) : base(profile, obj0, obj1, obj2, obj3, obj4) {}
//     public ActiveDerivedDataPortConnections(DataPortProfile profile, params IDataObjMeta[] objs) : base(profile, objs) {
//         Init();
//         ChangeProfileAndDetachAll(profile);
//         AttachAll(objs);
//     }
// }