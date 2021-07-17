using System;
using System.Collections.Generic;

public interface IObj : IEquatable<IObj> {  // --> See ObjHeader for implementation
    string Name { get; }
    long Id { get; }
    void SetId(long id);
    // Equatability stuff
    int GetHashCode();
    bool Equals(object obj);
    IObjRegistry Parent { get; }
    ModTag MTag { get; }
    void SetModified();
    void RegisterToParent(IObjRegistry newParent);
    void UnregisterFromParent();
    bool Clonable { get; set; }
    IObj Clone(IObjRegistry parent = null);
    // Sideways
    IObjRegistry ObjRegistry();
    // Down
    IDataObjMeta DataObjMeta();
}
public interface IObjRegistry : IObj {
    Dictionary<long, IObj> Children { get; }
    List<IObj> ChildrenList { get; }
    Dictionary<long, IObj> GetAllChildren(); // Recursive
    List<IObj> GetAllChildrenList(); // Recursive
    HashSet<IObjRegistry> SubRegistries { get; }
    List<IObjRegistry> SubRegistriesList { get; }
    IObjRegistry SubRegistry(ObjFilter filter);
    IObjRegistry SubRegistry(long id);
    IObjRegistry SubRegistry(string name);
    /// <summary>
    /// Find the first IObj that meet given criteria.
    /// </summary>
    IObj FindObj(ObjFilter filter, bool recursive=true);
    IObj FindObj(long id, bool recursive=true);
    IObj FindObj(string name, bool recursive=true);
    /// <summary>
    /// Find the first IDataObjMeta types that meet the given criteria
    /// </summary>
    IDataObjMeta FindDataObj(bool recursive=true);
    IDataObjMeta FindDataObj(DataTypeEnum dataType, bool recursive=true);
    IDataObjMeta FindDataObj(DataTypeEnum dataType, string name, bool recursive=true);
    IDataObjMeta FindDataObj(DataTypeEnum dataType, ObjFilter filter, bool recursive=true);
    /// <summary>
    /// Find the first derived T IObj that meet given criteria.
    /// </summary>
    T FindObjOfType<T>(bool recursive=true) where T : class, IObj;
    T FindObjOfType<T>(ObjFilter filter, bool recursive=true) where T : class, IObj;
    T FindObjOfType<T>(string name, bool recursive=true) where T : class, IObj;
    /// <summary>
    /// Finds all IObjs that meet given criteria.
    /// </summary>
    List<IObj> FindObjs(ObjFilter filter, bool recursive=true);
    List<IObj> FindObjs(string name, bool recursive=true);
    /// <summary>
    /// Find all IDataObjMeta types that meet the given criteria
    /// </summary>
    List<IDataObjMeta> FindDataObjs(bool recursive=true);
    List<IDataObjMeta> FindDataObjs(DataTypeEnum dataType, bool recursive=true);
    List<IDataObjMeta> FindDataObjs(DataTypeEnum dataType, string name, bool recursive=true);
    List<IDataObjMeta> FindDataObjs(DataTypeEnum dataType, ObjFilter filter, bool recursive=true);
    /// <summary>
    /// Finds all derived T IObjs that meet given criteria.
    /// </summary>
    List<T> FindObjsOfType<T>(bool recursive=true) where T : class, IObj;
    List<T> FindObjsOfType<T>(ObjFilter filter, bool recursive=true) where T : class, IObj;
    List<T> FindObjsOfType<T>(string name, bool recursive=true) where T : class, IObj;
    /// <summary>
    /// Provides list of all object names registered to this IObjRegistry (and optionally its children), including duplicates
    /// </summary>
    List<string> AllNames(bool recursive=true);
    List<string> AllNamesOfDataObjs(bool recursive=true);
    List<string> AllNamesOfType<T>(bool recursive=true) where T : class, IObj;
    /// <summary>
    /// Provides set of all object names registered to this IObjRegistry (and optionally its children), no duplicates
    /// </summary>
    HashSet<string> UniqueNames(bool recursive=true);
    HashSet<string> UniqueNamesOfDataObjs(bool recursive=true);
    HashSet<string> UniqueNamesOfType<T>(bool recursive=true) where T : class, IObj;
    /// <summary>
    /// Provides list of all object IDs registered to this IObjRegistry (and optionally its children).
    /// </summary>
    /// <param name="recursive"></param>
    /// <returns></returns>
    List<long> Index(bool recursive=true); // List of child ids
    List<long> IndexOfDataObjs(bool recursive=true);
    List<long> IndexOfType<T>(bool recursive=true) where T : class, IObj;
    /// <summary>
    /// Register the given object to this IObjRegistry. Must be actual object, not a copy of the header.
    /// </summary>
    void RegisterChild(IObj obj);
    /// <summary>
    /// Unregister the IObj, identified by various criteria.  If found and unregistered, returns true.
    /// </summary>
    bool UnregisterChild(IObj obj);
    bool UnregisterChild(long id);
    CloneResult CloneFamily(IObjRegistry parent = null);
}
public interface IExecutableObjMeta : IObj { // TODO
    // I do stuff to data
}
// In a pipeline workflow, inputs and outputs are 'DataObj', not restricted to Source/Derived
public interface IPipelineExecutableObj : IExecutableObjMeta {
    bool Enabled { get; set; }
    int NProfiles { get; } // A profile is an arrangement of inputs and outputs
    PipelineProfile GetProfile(int index);
    PipelineProfile ActiveProfile { get; }
    int ActiveProfileIndex { get; set; }
    void AttachInput(IDataObjMeta obj, int port);
    void AttachOutput(IDataObjMeta obj, int port);
    void DetachInput(int port);
    void DetachOutput(int port);
    void DetachAllInputs();
    void DetachAllOutputs();
    void DetachAllPorts();
    // Requires variables attached to ports,
    void ExecuteAttached(); // Requires variables attached to ports
    void Execute(IDataObjMeta obj0);
    void Execute(IDataObjMeta obj0, IDataObjMeta obj1);
    void Execute(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2);
    void Execute(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3);
    void Execute(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4);
    void Execute(params IDataObjMeta[] objs);
}
// In a derived updater workflow, inputs are Source/Derived and outputs are only Derived
public interface IDerivedUpdater : IExecutableObjMeta {
    List<IDerivedDataObjMeta> AllDerivedData { get; }
    bool PerformUpdatesFor(IDerivedDataObjMeta target);
    void PerformAllUpdates();
    // 'Init' the 'DependsOn' list 'For' the given target derivedDataObj
    bool InitDependsOnFor(IDerivedDataObjMeta target, out List<ISourceDataObjMeta> dependsOn);
    void InitAllDependsOn();
    bool SpawnAllDerived();
}
// public interface IControllerObj : IExecutableObjMeta { // TODO
//     // I control stuff
// }
public interface IDataObjMeta : IObj { // --> DataObjHeader abstract implementation
    DataTypeEnum DataType { get; }
    // Sideways
    ISourceDataObjMeta SourceDataObjMeta();
    IDerivedDataObjMeta DerivedDataObjMeta();
    // Down
    IDataSetObjMeta DataSetObjMeta();
}
public interface ISourceDataObjMeta : IDataObjMeta {  // --> No direct implementations
    // For now, nothing, but in the future, maybe:
    // IControllerObj ControlledBy { get; set; }
}
public interface IDerivedDataObjMeta : IDataObjMeta {  // --> // TODO
    List<ISourceDataObjMeta> DependsOn { get; set; }
    IDerivedUpdater Updater { get; set; }
    bool Stale();
    bool UpToDate();
    bool UpdateDerived();
}
public interface IDataObj<L> : IDataObjMeta {  // --> No direct implementations
    ITraitsSimple<L> TraitsSimple { get; }
    L Data { get; }
}
public interface ISourceDataObj<L> : IDataObj<L>, ISourceDataObjMeta {  // --> SourceDataObj abstract implementation
    new L Data { get; set; }                                            // --> SourceDataObjs derived implementations
}
public interface IDerivedDataObj<L> : IDataObj<L>, IDerivedDataObjMeta {
    L DataNoUpdate { get; }
}
public interface IDataSetObjMeta : IDataObjMeta {  // --> DataSetObjHeader abstract implementation
    DataTypeEnum ComponentType { get; }
    ComponentAccessType PreferredAccessType { get; }
    bool ElementAccessByIndex();
    bool ElementAccessByString();
    string GetComponentName(int index);
    int GetComponentIndex(string elem);
    int NComponents { get; } // -1 = use size query
}
public interface IDataSetObj<L, C> : IDataSetObjMeta, IDataObj<L> {
    ITraits<L, C> Traits { get; }
    C this[int index] { get; }
    C this[string elem] { get; }
}
public interface ISourceDataSetObj<L, C> : IDataSetObj<L, C>, ISourceDataObjMeta {
    new L Data { get; set; }
    new C this[int index] { get; set; }
    new C this[string elem] { get; set; }
}
public interface IDerivedDataSetObj<L, C> : IDataSetObj<L, C>, IDerivedDataObjMeta {
    C GetComponentNoUpdate(int index);
    C GetComponentNoUpdate(string elem);
}

// *** Supporting definitions
public enum ComponentAccessType {
    None,
    Index,
    String
}
