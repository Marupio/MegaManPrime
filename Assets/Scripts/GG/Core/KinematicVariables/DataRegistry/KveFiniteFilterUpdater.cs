// using System.Collections.Generic;
// using UnityEngine;

// // TODO - Implement this class
// class KveFiniteFilterUpdater : PipelineExecutableBase {


//     public abstract void InternalExecute(DataPortProfile profile, ActiveDataPortConnections<DataObjList, DataObjList> connections);


//     // OLD STUFF BELOW

//     // *** Data
//     // protected DataPortProfile m_dataProfile;

//     // // Reverse lookup
//     // protected Dictionary<IDerivedDataObjMeta, int> m_index;

//     // // Indexed data - all 'indexed' are associated between identical indices
//     // protected List<IDerivedDataObjMeta> m_outputs;
//     // protected List<List<IDataObjMeta>> m_directInputs;
//     // protected List<List<ISourceDataObjMeta>> m_sourceInputs;
//     // protected List<bool> m_derivedVarInitComplete;

//     // public DataPortProfile DataProfile { get=>m_dataProfile; }
//     // public List<IDerivedDataObjMeta> AllDerivedData { get=>m_outputs; }
//     // public List<List<IDataObjMeta>> DirectDependsOn { get=>m_directInputs; }
//     // public List<List<ISourceDataObjMeta>> SourceDependsOn { get=>m_sourceInputs; }

//     public abstract bool PerformUpdatesFor(IDerivedDataObjMeta target); // TODO - or internally change this into an index, and make that call abstract
//     public abstract void PerformAllUpdates();

//     KveFiniteFilterUpdater() : base() {}
// }
