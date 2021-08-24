using System;
using System.Collections.Generic;

/// <summary>
/// Base interface for all tracked objects - everyone gets a unique ID, even unused IObjs
/// Constructors always build a valid object - objects are never in a uninitialized state
/// </summary>
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
/// <summary>
/// This IObj contains, registers and tracks child IObjs, and since it is an IObj itself, it allows for a hierarchical object structure
/// </summary>
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
/// <summary>
/// A classification type of interface - IObjs in this category 'do stuff', whereas other IObjs 'are stuff'
/// </summary>
public interface IExecutableObjMeta : IObj { // TODO
    // I do stuff to data
}
/// <summary>
/// Classes that implement this interface take input and output IDataObjMetas.
/// This interface:
///     * defines one or many valid arrangements of inputs and outputs
///     * inputs and outputs are loosely specified by their dataTypes and names
///     * has a single 'active' input/output arrangement, on to which actual IDataObjMeta objects attach
/// The input data are constrained to conform to I, an IObjPass - a predicate class
/// The output data are constrained to conform to O, also an IObjPass
/// </summary>
public interface IDataPortModule<I, O> : IObj where I : DataObjList where O : DataObjList {
    bool Enabled { get; set; }
    Dictionary<string, DataPortProfile> Profiles { get; set; }
    int NProfiles { get; }
    DataPortProfile ActiveProfile { get; }
    ActiveDataPortConnections<I, O> Connections { get; }
    bool Ready { get; }
}
/// <summary>
/// IPipelineExecutableObj classes have the input/output functionality of the IDataPortModule classes, and operate on the inputs to produce the
/// outputs.  A single instance of this class can operate on as many sets of inputs and outputs as desired, and as many times in a row as desired.
/// </summary>
public interface IPipelineExecutableObj : IDataPortModule<DataObjList, DataObjList>, IExecutableObjMeta {
    void ExecuteAttached();
    void ExecuteProfile(DataPortProfile profile);
    void ExecuteProfile(
        DataPortProfile profile,
        ActiveDataPortConnections<DataObjList, DataObjList> connections
    );
    void ExecuteProfile(string profileName);
    void ExecuteProfile(string profileName, ActiveDataPortConnections<DataObjList, DataObjList> connections);
}

// In a derived updater workflow, inputs are Source/Derived and outputs are only Derived
public interface IDerivedUpdater : IDataPortModule<DataObjList, DataObjList_DerivedPass>, IObjRegistry {
    // Option 0
    //  DataObjList AllDerivedData {get;} // constraints are hidden from interface and types
    //  DataObjList DependsOnData {get;}  // but we directly use the ObjLists and the ObjLists are compatible
    // Option 1
    //  List<IDerivedDataObjMeta> AllDerivedData {get;} // Convert ObjList to another storage list type, constraints are visible
    //  List<IDataObjMeta> DirectDependsOn {get;}
    // Option 2 Put constraints as generic parameter to ObjList

    DataObjList_DerivedPass AllDerivedData { get; }
    DataObjList AllDependsOnDirectly { get; }
    DataObjList_SourcePass AllDependsOnSources { get; }
    DataObjList GetDirectDependsOnFor(IDerivedDataObjMeta outputObj); // The sources used in Update, may include other DerivedDataObj
    DataObjList_SourcePass GetSourceDependsOnFor(IDerivedDataObjMeta outputObj); // The sources, resolved down to SourceDataObj level, hierarchically flattened
    bool UpToDateAll();
    bool UpToDateFor(IDerivedDataObjMeta obj);
    bool PerformUpdatesAll();
    bool PerformUpdatesFor(IDerivedDataObjMeta outputObj);
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
