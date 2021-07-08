using System.Collections.Generic;

public interface IObj {
    string Name { get; }
    long Id { get; }
    void SetId(long id);
    IObjRegistry Parent { get; }
    ModTag MTag { get; }
    void SetModified();
    void RegisterToParent(IObjRegistry newParent);
    void UnregisterFromParent();
    IObj Clone(IObjRegistry parent = null);
    // Sideways
    IObjRegistry ObjRegistry();
    // Down
    IDataObjMeta DataObjMeta();
}
public interface IObjRegistry : IObj {
    Dictionary<long, IObj> Children { get; }
    HashSet<IObjRegistry> SubRegistries { get; }
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
    IObjRegistry CloneFamily(IObjRegistry parent = null);
}
public interface IDataObjMeta : IObj {
    DataTypeEnum DataType { get; }
    // Sideways
    ISourceDataObjMeta SourceDataObjMeta();
    IDerivedDataObjMeta DerivedDataObjMeta();
    // Down
    IDataSetObjMeta DataSetObjMeta();
}
public interface ISourceDataObjMeta : IDataObjMeta {
    // Nothing
}
public interface IDerivedDataObjMeta : IDataObjMeta {
    List<IDataObjMeta> DependsOn { get; }
    bool UpToDate();
}
public interface IDataObj<L> : IDataObjMeta {
    ITraitsSimple<L> TraitsSimple { get; }
    L Data { get; }
}
public interface ISourceDataObj<L> : IDataObj<L>, ISourceDataObjMeta {
    new L Data { get; set; }
}
public interface IDerivedDataObj<L> : IDataObj<L>, IDerivedDataObjMeta {
    IObjUpdater<L> Updater { get; set; }
    void UpdateDerived();
}
public interface IDataSetObjMeta : IDataObjMeta {
    DataTypeEnum ComponentType { get; }
    ComponentAccessType PreferredAccessType { get; } // index | string | noPreference
    bool ElementAccessByIndex { get; }
    bool ElementAccessByString { get; }
    string GetComponentName(int elem);
    int GetComponentIndex(string elem);
    int NComponents { get; } // -1 = use size query
}
public interface IDataSetObj<L, C> : IDataSetObjMeta, IDataObj<L> {
    ITraits<L, C> Traits { get; }
    C this[int elem] { get; set; }
    C this[string elem] { get; set; }
}

// *** Supporting definitions
public interface IObjUpdater<L> {
    void UpdateDerivedOn(IDerivedDataObj<L> target, ref L data);
}
public enum ComponentAccessType {
    NoPreference,
    Index,
    String
}
