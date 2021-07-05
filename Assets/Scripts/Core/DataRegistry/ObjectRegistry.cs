using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectRegistry : ObjectHeader, IObjectRegistry {
    // *** Tables for faster access
    // Name collisions are allowed - hence returns List<long>
    protected Dictionary<string, List<long>> m_nameIndex;
    protected HashSet<long> m_dataObjects;
    // protected Dictionary<DataTypeEnum, HashSet<long>> m_dataTypeIndex; // DataObject / DataSetObjects only
    protected List<HashSet<long>> m_idHashSetsByDataType;

    // References to IObjects are only contained in these lists, public for direct access when needed
    public Dictionary<long, IObject> m_children;
    // If child is also an ObjectRegistry, it will also appear here: (convenience for recursive searches)
    public HashSet<IObjectRegistry> m_subRegistries;

    public Dictionary<long, IObject> Children() { return m_children; }
    public List<IObject> ChildrenList { get=>new List<IObject>(m_children.Values); }
    public HashSet<IObjectRegistry> SubRegistries() { return m_subRegistries; }
    public List<IObjectRegistry> SubRegistriesList { get=>m_subRegistries.ToList(); }
    public IObjectRegistry SubRegistry(ObjectFilter filter) {
        foreach(IObjectRegistry subRegistry in m_subRegistries) {
            if (filter.Pass(subRegistry)) {
                return subRegistry;
            }
        }
        return null;
    }
    public IObjectRegistry SubRegistry(long id) {
        IObject obj;
        if (m_children.TryGetValue(id, out obj)) {
            IObjectRegistry registry = obj.ObjectRegistry();
            return registry;
        }
        return null;
    }
    public IObjectRegistry SubRegistry(string name) {
        List<long> idList;
        // IObject obj;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            foreach (long id in idList) {
                IObjectRegistry registry = m_children[id].ObjectRegistry();
                if (registry != null) {
                    return registry;
                }
            }
        }
        return null;
    }
    public IObject FindObject(ObjectFilter filter, bool recursive=true) {
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            if (filter.Pass(entry.Value)) {
                return entry.Value;
            }
        }
        if (recursive) {
            foreach(IObjectRegistry registry in m_subRegistries) {
                IObject obj = registry.FindObject(filter, true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public IObject FindObject(long id, bool recursive=true) {
        IObject obj;
        if (m_children.TryGetValue(id, out obj)) {
            return obj;
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                obj = subReg.FindObject(id, true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public IObject FindObject(string name, bool recursive=true) {
// protected Dictionary<string, List<long>> m_nameIndex;
        List<long> idList;
        if(m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            return m_children[idList[0]];
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                IObject obj = subReg.FindObject(name, true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public IDataObjectHeader FindDataObject(bool recursive=true) {
        HashSet<long>.Enumerator iter = m_dataObjects.GetEnumerator();
        if (iter.MoveNext()) { return m_children[iter.Current].DataObjectHeader(); }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                IDataObjectHeader dataObject = subReg.FindDataObject(true);
                if (dataObject != null) {
                    return dataObject;
                }
            }
        }
        return null;
    }
    public IDataObjectHeader FindDataObject(DataTypeEnum dataType, bool recursive=true) {
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        if (iter.MoveNext()) { return m_children[iter.Current].DataObjectHeader(); }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                IDataObjectHeader dataObject = subReg.FindDataObject(dataType, true);
                if (dataObject != null) {
                    return dataObject;
                }
            }
        }
        return null;
    }
    public IDataObjectHeader FindDataObject(DataTypeEnum dataType, string name, bool recursive=true) {
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObject obj = m_children[iter.Current];
            if (obj.Name == name) {
                return obj.DataObjectHeader();
            }
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                IDataObjectHeader dataObject = subReg.FindDataObject(dataType, name, true);
                if (dataObject != null) {
                    return dataObject;
                }
            }
        }
        return null;
    }
    public IDataObjectHeader FindDataObject(DataTypeEnum dataType, ObjectFilter filter, bool recursive=true) {
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObject obj = m_children[iter.Current];
            if (filter.Pass(obj)) {
                return obj.DataObjectHeader();
            }
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                IDataObjectHeader dataObject = subReg.FindDataObject(dataType, filter, true);
                if (dataObject != null) {
                    return dataObject;
                }
            }
        }
        return null;
    }
    public T FindObjectOfType<T>(bool recursive=true) where T : class, IObject {
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            if (entry.Value is T) { return (T)entry.Value; }
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                T obj = subReg.FindObjectOfType<T>(true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public T FindObjectOfType<T>(ObjectFilter filter, bool recursive=true) where T : class, IObject {
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            IObject obj = entry.Value;
            if (obj is T && filter.Pass(obj)) { return (T)obj; }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                T objT = subReg.FindObjectOfType<T>(true);
                if (objT != null) {
                    return objT;
                }
            }
        }
        return null;
    }
    public T FindObjectOfType<T>(string name, bool recursive=true) where T : class, IObject {
        List<long> idList;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            foreach(long id in idList) {
                IObject obj = m_children[id];
                if (obj is T) { return (T) obj; }
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                T objT = subReg.FindObjectOfType<T>(name, true);
                if (objT != null) {
                    return objT;
                }
            }
        }
        return null;
    }
    public List<IObject> FindObjects(ObjectFilter filter, bool recursive=true) {
        List<IObject> objects = new List<IObject>();
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            IObject obj = entry.Value;
            if (filter.Pass(obj)) {
                objects.Add(obj);
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjects(filter, true));
            }
        }
        return objects;
    }
    public List<IObject> FindObjects(string name, bool recursive=true) {
        List<IObject> objects = new List<IObject>();
        List<long> idList;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            foreach(long id in idList) {
                objects.Add(m_children[id]);
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjects(name, true));
            }
        }
        return objects;
    }
    public List<IDataObjectHeader> FindDataObjects(bool recursive=true) {
        List<IDataObjectHeader> dataObjects = new List<IDataObjectHeader>();
        HashSet<long>.Enumerator iter = m_dataObjects.GetEnumerator();
        while (iter.MoveNext()) { dataObjects.Add(m_children[iter.Current].DataObjectHeader()); }

        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                dataObjects.AddRange(subReg.FindDataObjects(true));
            }
        }
        return dataObjects;
    }
    public List<IDataObjectHeader> FindDataObjects(DataTypeEnum dataType, bool recursive=true) {
        List<IDataObjectHeader> dataObjects = new List<IDataObjectHeader>();
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) { dataObjects.Add(m_children[iter.Current].DataObjectHeader()); }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                dataObjects.AddRange(subReg.FindDataObjects(dataType, true));
            }
        }
        return dataObjects;
    }
    public List<IDataObjectHeader> FindDataObjects(DataTypeEnum dataType, string name, bool recursive=true) {
        List<IDataObjectHeader> dataObjects = new List<IDataObjectHeader>();
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObject obj = m_children[iter.Current];
            if (obj.Name == name) { dataObjects.Add(obj.DataObjectHeader()); }
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                dataObjects.AddRange(subReg.FindDataObjects(dataType, name, true));
            }
        }
        return dataObjects;
    }
    public List<IDataObjectHeader> FindDataObjects(DataTypeEnum dataType, ObjectFilter filter, bool recursive=true) {
        List<IDataObjectHeader> dataObjects = new List<IDataObjectHeader>();
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObject obj = m_children[iter.Current];
            if (filter.Pass(obj)) {
                dataObjects.Add(obj.DataObjectHeader());
            }
        }
        if (recursive) {
            foreach (IObjectRegistry subReg in m_subRegistries) {
                dataObjects.AddRange(subReg.FindDataObjects(dataType, filter, true));
            }
        }
        return dataObjects;
    }
    public List<T> FindObjectsOfType<T>(bool recursive=true) where T : class, IObject  {
        List<T> objects = new List<T>();
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            IObject obj = entry.Value;
            if (obj is T) {
                objects.Add(obj as T);
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjectsOfType<T>(true));
            }
        }
        return objects;
    }
    public List<T> FindObjects<T>(ObjectFilter filter, bool recursive=true) where T : class, IObject {
        List<T> objects = new List<T>();
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            IObject obj = entry.Value;
            if (obj is T && filter.Pass(obj)) {
                objects.Add(obj as T);
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjectsOfType<T>(filter, true));
            }
        }
        return objects;
    }
    public List<T> FindObjects<T>(string name, bool recursive=true) where T : class, IObject {
        List<T> objects = new List<T>();
        List<long> idList;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            foreach(long id in idList) {
                IObject obj = m_children[id];
                if (obj is T) {
                    objects.Add(obj as T);
                }
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjectsOfType<T>(name, true));
            }
        }
        return objects;
    }

    public List<string> AllNames(bool recursive=true) {
        List<string> toc = new List<string>();
        foreach(KeyValuePair<string, List<long>> entry in m_nameIndex) {
            for (int i = 0; i < entry.Value.Count; ++i) { toc.Add(entry.Key); }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                toc.AddRange(subReg.AllNames(true));
            }
        }
        return toc;
    }
    public List<string> AllNamesOfDataObjects(bool recursive=true) {
        List<string> names = new List<string>();
        foreach(long id in m_dataObjects) { names.Add(m_children[id].Name); }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                names.AddRange(subReg.AllNamesOfDataObjects(true));
            }
        }
        return names;
    }
    public List<string> AllNamesOfType<T>(bool recursive=true) where T : class, IObject {
        List<string> toc = new List<string>();
        foreach(KeyValuePair<string, List<long>> entry in m_nameIndex) {
            string name = entry.Key;
            List<long> idList = entry.Value;
            foreach(long id in idList) {
                if (m_children[id] is T) {
                    toc.Add(name);
                }
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                toc.AddRange(subReg.AllNamesOfType<T>(true));
            }
        }
        return toc;
    }
    public HashSet<string> UniqueNames(bool recursive=true) {
        HashSet<string> toc = new HashSet<string>();
        foreach(KeyValuePair<string, List<long>> entry in m_nameIndex) { toc.Add(entry.key); }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                toc.UnionWith(subReg.UniqueNames(true));
            }
        }
        return toc;
    }
    public HashSet<string> UniqueNamesOfDataObjects(bool recursive=true) {
        HashSet<string> names = new HashSet<string>();
        foreach(long id in m_dataObjects) { names.Add(m_children[id].Name); }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                names.UnionWith(subReg.UniqueNamesOfDataObjects(true));
            }
        }
        return names;
    }
    public HashSet<string> UniqueNamesOfType<T>(bool recursive=true) where T : class, IObject {
        HashSet<string> toc = new HashSet<string>();
        foreach(KeyValuePair<string, List<long>> entry in m_nameIndex) {
            string name = entry.Key;
            List<long> idList = entry.Value;
            foreach(long id in idList) {
                if (m_children[id] is T) {
                    toc.Add(name);
                    break;
                }
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                toc.UnionWith(subReg.UniqueNamesOfType<T>(true));
            }
        }
        return toc;
    }
    public List<long> Index(bool recursive=true) {
        List<long> index = new List<long>(m_children.Keys);
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                index.AddRange(subReg.Index(true));
            }
        }
        return index;
    }
    public List<long> IndexOfDataObjects(bool recursive=true) {
        List<long> index = m_dataObjects.ToList();
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                index.AddRange(subReg.IndexOfDataObjects(true));
            }
        }
        return index;
    }
    public List<long> IndexOfType<T>(bool recursive=true) where T : class, IObject {
        List<long> index = new List<long>();
        foreach(KeyValuePair<long, IObject> entry in m_children) {
            if (entry.Value is T) {
                index.Add(entry.Key);
            }
        }
        if (recursive) {
            foreach(IObjectRegistry subReg in m_subRegistries) {
                index.AddRange(subReg.IndexOfType<T>(true));
            }
        }
        return index;
    }
    public void Register(IObject obj) {
        if (obj.Id == GlobalRegistrar.IdAnonymous) {
            // Needs an ID
            obj.SetId(GlobalRegistrar.GetNextId());
        } else {
            // Check for ID collisions
            if (m_children.ContainsKey(obj.Id)) {
                throw new System.FieldAccessException("Registry already contains ID " + obj.Id));
            }
        }
        m_children.Add(obj.Id, obj);

        IObjectRegistry subRegistry = obj.ObjectRegistry();
        if (subRegistry != null) {
            m_subRegistries.Add(subRegistry);
        }
        List<long> idList;
        if (m_nameIndex.TryGetValue(obj.Name, out idList)) {
            idList.Add(obj.Id);
        } else {
            idList = new List<long>();
            idList.Add(obj.Id);
            m_nameIndex.Add(obj.Name, idList);
        }
        IDataObjectHeader dataHeader = obj.DataObjectHeader();
        if (dataHeader != null) {
            m_dataObjects.Add(obj.Id);
            m_idHashSetsByDataType[(int)dataHeader.DataType].Add(obj.Id);
        }
    }
    public bool Unregister(IObject obj) {
        IDataObjectHeader dataHeader = obj.DataObjectHeader();
        if (dataHeader != null) {
            m_dataObjects.Remove(obj.Id);
            m_idHashSetsByDataType[(int)dataHeader.DataType].Remove(obj.Id);
        }
        List<long> idList;
        if (m_nameIndex.TryGetValue(obj.Name, out idList)) {
            if (idList.Remove(obj.Id)) {
                if (idList.Count == 0) {
                    m_nameIndex.Remove(obj.Name);
                }
            } else {
                Debug.LogWarning("Unregistering object name " + obj.Name + ", id " + obj.Id + " missing from NameIndex IDList");
            }
        } else {
            Debug.LogWarning("Unregistering object name " + obj.Name + ", id " + obj.Id + " missing from NameIndex");
        }
        IObjectRegistry subRegistry = obj.ObjectRegistry();
        if (subRegistry != null) {
            m_subRegistries.Remove(subRegistry);
        }
        return m_children.Remove(obj.Id);
    }
    public bool Unregister(long id) {
        IObject obj;
        if (m_children.TryGetValue(id, out obj)) {
            return Unregister(obj);
        }
        return false;
    }

    // Initialise member fields
    protected void Init() {
        m_nameIndex = new Dictionary<string, List<long>>();
        m_dataObjects = new HashSet<long>();
        m_idHashSetsByDataType = new List<HashSet<long>>(DataTypeStaticData.nDataTypeEnums);
        for(int i = 0; i < m_idHashSetsByDataType.Count; ++i) {
            m_idHashSetsByDataType[i] = new HashSet<long>();
        }
        m_children = new Dictionary<long, IObject>();
    }

    // TODO Add constructors
    ~ObjectRegistry() {
        // I don't need to unregister myself, but my I need to unregister my children.
        List<long> keys = m_children.Keys.ToList();
        foreach(long id in keys) {
            m_children[id].InternalSetOrphan();
        }
    }
}
