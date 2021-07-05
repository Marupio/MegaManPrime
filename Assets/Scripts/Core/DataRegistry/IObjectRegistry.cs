using System.Collections.Generic;
using UnityEngine;

public interface IObjectRegistry : IObject {
    public Dictionary<long, IObject> Children { get; }
    public List<IObject> ChildrenList { get; }
    public HashSet<IObjectRegistry> SubRegistries { get; }
    public List<IObjectRegistry> SubRegistriesList { get; }
    public IObjectRegistry SubRegistry(ObjectFilter filter);
    public IObjectRegistry SubRegistry(long id);
    public IObjectRegistry SubRegistry(string name);
    /// <summary>
    /// Find the first IObject that meet given criteria.
    /// </summary>
    public IObject FindObject(ObjectFilter filter, bool recursive=true);
    public IObject FindObject(long id, bool recursive=true);
    public IObject FindObject(string name, bool recursive=true);
    /// <summary>
    /// Find the first IDataObjectHeader types that meet the given criteria
    /// </summary>
    public IDataObjectHeader FindDataObject(bool recursive=true);
    public IDataObjectHeader FindDataObject(DataTypeEnum dataType, bool recursive=true);
    public IDataObjectHeader FindDataObject(DataTypeEnum dataType, string name, bool recursive=true);
    public IDataObjectHeader FindDataObject(DataTypeEnum dataType, ObjectFilter filter, bool recursive=true);
    /// <summary>
    /// Find the first derived T IObject that meet given criteria.
    /// </summary>
    public T FindObjectOfType<T>(bool recursive=true) where T : class, IObject;
    public T FindObjectOfType<T>(ObjectFilter filter, bool recursive=true) where T : class, IObject;
    public T FindObjectOfType<T>(string name, bool recursive=true) where T : class, IObject;
    /// <summary>
    /// Finds all IObjects that meet given criteria.
    /// </summary>
    public List<IObject> FindObjects(ObjectFilter filter, bool recursive=true);
    public List<IObject> FindObjects(string name, bool recursive=true);
    /// <summary>
    /// Find all IDataObjectHeader types that meet the given criteria
    /// </summary>
    public List<IDataObjectHeader> FindDataObjects(bool recursive=true);
    public List<IDataObjectHeader> FindDataObjects(DataTypeEnum dataType, bool recursive=true);
    public List<IDataObjectHeader> FindDataObjects(DataTypeEnum dataType, string name, bool recursive=true);
    public List<IDataObjectHeader> FindDataObjects(DataTypeEnum dataType, ObjectFilter filter, bool recursive=true);
    /// <summary>
    /// Finds all derived T IObjects that meet given criteria.
    /// </summary>
    public List<T> FindObjectsOfType<T>(bool recursive=true) where T : class, IObject;
    public List<T> FindObjectsOfType<T>(ObjectFilter filter, bool recursive=true) where T : class, IObject;
    public List<T> FindObjectsOfType<T>(string name, bool recursive=true) where T : class, IObject;
    /// <summary>
    /// Provides list of all object names registered to this IObjectRegistry (and optionally its children), including duplicates
    /// </summary>
    public List<string> AllNames(bool recursive=true);
    public List<string> AllNamesOfDataObjects(bool recursive=true);
    public List<string> AllNamesOfType<T>(bool recursive=true) where T : class, IObject;
    /// <summary>
    /// Provides set of all object names registered to this IObjectRegistry (and optionally its children), no duplicates
    /// </summary>
    public HashSet<string> UniqueNames(bool recursive=true);
    public HashSet<string> UniqueNamesOfDataObjects(bool recursive=true);
    public HashSet<string> UniqueNamesOfType<T>(bool recursive=true) where T : class, IObject;
    /// <summary>
    /// Provides list of all object IDs registered to this IObjectRegistry (and optionally its children).
    /// </summary>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public List<long> Index(bool recursive=true); // List of child ids
    public List<long> IndexOfDataObjects(bool recursive=true);
    public List<long> IndexOfType<T>(bool recursive=true) where T : class, IObject;
    /// <summary>
    /// Register the given object to this IObjectRegistry. Must be actual object, not a copy of the header.
    /// </summary>
    public void Register(IObject obj);
    /// <summary>
    /// Unregister the IObject, identified by various criteria.  If found and unregistered, returns true.
    /// </summary>
    public bool Unregister(IObject obj);
    public bool Unregister(long id);
}
