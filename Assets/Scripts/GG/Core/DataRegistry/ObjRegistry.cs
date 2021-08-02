using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjRegistry : ObjHeader, IObjRegistry {
    // *** Tables for faster access
    // Name collisions are allowed - hence returns List<long>
    protected Dictionary<string, List<long>> m_nameIndex;
    protected HashSet<long> m_dataObjs;
    // protected Dictionary<DataTypeEnum, HashSet<long>> m_dataTypeIndex; // DataObj / DataSetObjs only
    protected List<HashSet<long>> m_idHashSetsByDataType;
    // If child is also an ObjRegistry, it will also appear here: (convenience for recursive searches)
    public HashSet<IObjRegistry> m_subRegistries;
    public Dictionary<long, IObj> m_children;

    public Dictionary<long, IObj> Children { get=>m_children; }
    public List<IObj> ChildrenList { get=>new List<IObj>(m_children.Values); }
    public Dictionary<long, IObj> GetAllChildren() {
        Dictionary<long, IObj> allChildren = new Dictionary<long, IObj>(m_children);
        foreach(IObjRegistry subRegistry in m_subRegistries) {
            allChildren.Concat(subRegistry.GetAllChildren()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        return allChildren;
    }
    public List<IObj> GetAllChildrenList() {
        List<IObj> allChildren = ChildrenList;
        foreach(IObjRegistry subRegistry in m_subRegistries) {
            allChildren.AddRange(subRegistry.ChildrenList);
        }
        return allChildren;
    }
    public HashSet<IObjRegistry> SubRegistries { get=>m_subRegistries; }
    public List<IObjRegistry> SubRegistriesList { get=>m_subRegistries.ToList(); }
    public IObjRegistry SubRegistry(ObjFilter filter) {
        foreach(IObjRegistry subRegistry in m_subRegistries) {
            if (filter.Pass(subRegistry)) {
                return subRegistry;
            }
        }
        return null;
    }
    public IObjRegistry SubRegistry(long id) {
        IObj obj;
        if (m_children.TryGetValue(id, out obj)) {
            IObjRegistry registry = obj.ObjRegistry();
            return registry;
        }
        return null;
    }
    public IObjRegistry SubRegistry(string name) {
        List<long> idList;
        // IObj obj;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            foreach (long id in idList) {
                IObjRegistry registry = m_children[id].ObjRegistry();
                if (registry != null) {
                    return registry;
                }
            }
        }
        return null;
    }
    public IObj FindObj(ObjFilter filter, bool recursive=true) {
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            if (filter.Pass(entry.Value)) {
                return entry.Value;
            }
        }
        if (recursive) {
            foreach(IObjRegistry registry in m_subRegistries) {
                IObj obj = registry.FindObj(filter, true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public IObj FindObj(long id, bool recursive=true) {
        IObj obj;
        if (m_children.TryGetValue(id, out obj)) {
            return obj;
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                obj = subReg.FindObj(id, true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public IObj FindObj(string name, bool recursive=true) {
        List<long> idList;
        if(m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            return m_children[idList[0]];
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                IObj obj = subReg.FindObj(name, true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public IDataObjMeta FindDataObj(bool recursive=true) {
        HashSet<long>.Enumerator iter = m_dataObjs.GetEnumerator();
        if (iter.MoveNext()) { return m_children[iter.Current].DataObjMeta(); }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                IDataObjMeta dataObj = subReg.FindDataObj(true);
                if (dataObj != null) {
                    return dataObj;
                }
            }
        }
        return null;
    }
    public IDataObjMeta FindDataObj(DataTypeEnum dataType, bool recursive=true) {
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        if (iter.MoveNext()) { return m_children[iter.Current].DataObjMeta(); }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                IDataObjMeta dataObj = subReg.FindDataObj(dataType, true);
                if (dataObj != null) {
                    return dataObj;
                }
            }
        }
        return null;
    }
    public IDataObjMeta FindDataObj(DataTypeEnum dataType, string name, bool recursive=true) {
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObj obj = m_children[iter.Current];
            if (obj.Name == name) {
                return obj.DataObjMeta();
            }
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                IDataObjMeta dataObj = subReg.FindDataObj(dataType, name, true);
                if (dataObj != null) {
                    return dataObj;
                }
            }
        }
        return null;
    }
    public IDataObjMeta FindDataObj(DataTypeEnum dataType, ObjFilter filter, bool recursive=true) {
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObj obj = m_children[iter.Current];
            if (filter.Pass(obj)) {
                return obj.DataObjMeta();
            }
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                IDataObjMeta dataObj = subReg.FindDataObj(dataType, filter, true);
                if (dataObj != null) {
                    return dataObj;
                }
            }
        }
        return null;
    }
    public T FindObjOfType<T>(bool recursive=true) where T : class, IObj {
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            if (entry.Value is T) { return (T)entry.Value; }
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                T obj = subReg.FindObjOfType<T>(true);
                if (obj != null) {
                    return obj;
                }
            }
        }
        return null;
    }
    public T FindObjOfType<T>(ObjFilter filter, bool recursive=true) where T : class, IObj {
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            IObj obj = entry.Value;
            if (obj is T && filter.Pass(obj)) { return (T)obj; }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                T objT = subReg.FindObjOfType<T>(true);
                if (objT != null) {
                    return objT;
                }
            }
        }
        return null;
    }
    public T FindObjOfType<T>(string name, bool recursive=true) where T : class, IObj {
        List<long> idList;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            foreach(long id in idList) {
                IObj obj = m_children[id];
                if (obj is T) { return (T) obj; }
            }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                T objT = subReg.FindObjOfType<T>(name, true);
                if (objT != null) {
                    return objT;
                }
            }
        }
        return null;
    }
    public List<IObj> FindObjs(ObjFilter filter, bool recursive=true) {
        List<IObj> objects = new List<IObj>();
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            IObj obj = entry.Value;
            if (filter.Pass(obj)) {
                objects.Add(obj);
            }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjs(filter, true));
            }
        }
        return objects;
    }
    public List<IObj> FindObjs(string name, bool recursive=true) {
        List<IObj> objects = new List<IObj>();
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
            foreach(IObjRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjs(name, true));
            }
        }
        return objects;
    }
    public List<IDataObjMeta> FindDataObjs(bool recursive=true) {
        List<IDataObjMeta> dataObjs = new List<IDataObjMeta>();
        HashSet<long>.Enumerator iter = m_dataObjs.GetEnumerator();
        while (iter.MoveNext()) { dataObjs.Add(m_children[iter.Current].DataObjMeta()); }

        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                dataObjs.AddRange(subReg.FindDataObjs(true));
            }
        }
        return dataObjs;
    }
    public List<IDataObjMeta> FindDataObjs(DataTypeEnum dataType, bool recursive=true) {
        List<IDataObjMeta> dataObjs = new List<IDataObjMeta>();
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) { dataObjs.Add(m_children[iter.Current].DataObjMeta()); }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                dataObjs.AddRange(subReg.FindDataObjs(dataType, true));
            }
        }
        return dataObjs;
    }
    public List<IDataObjMeta> FindDataObjs(DataTypeEnum dataType, string name, bool recursive=true) {
        List<IDataObjMeta> dataObjs = new List<IDataObjMeta>();
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObj obj = m_children[iter.Current];
            if (obj.Name == name) { dataObjs.Add(obj.DataObjMeta()); }
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                dataObjs.AddRange(subReg.FindDataObjs(dataType, name, true));
            }
        }
        return dataObjs;
    }
    public List<IDataObjMeta> FindDataObjs(DataTypeEnum dataType, ObjFilter filter, bool recursive=true) {
        List<IDataObjMeta> dataObjs = new List<IDataObjMeta>();
        HashSet<long> idList = m_idHashSetsByDataType[(int)dataType];
        HashSet<long>.Enumerator iter = idList.GetEnumerator();
        while (iter.MoveNext()) {
            IObj obj = m_children[iter.Current];
            if (filter.Pass(obj)) {
                dataObjs.Add(obj.DataObjMeta());
            }
        }
        if (recursive) {
            foreach (IObjRegistry subReg in m_subRegistries) {
                dataObjs.AddRange(subReg.FindDataObjs(dataType, filter, true));
            }
        }
        return dataObjs;
    }
    public List<T> FindObjsOfType<T>(bool recursive=true) where T : class, IObj  {
        List<T> objects = new List<T>();
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            IObj obj = entry.Value;
            if (obj is T) {
                objects.Add(obj as T);
            }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjsOfType<T>(true));
            }
        }
        return objects;
    }
    public List<T> FindObjsOfType<T>(ObjFilter filter, bool recursive=true) where T : class, IObj {
        List<T> objects = new List<T>();
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            IObj obj = entry.Value;
            if (obj is T && filter.Pass(obj)) {
                objects.Add(obj as T);
            }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjsOfType<T>(filter, true));
            }
        }
        return objects;
    }
    public List<T> FindObjsOfType<T>(string name, bool recursive=true) where T : class, IObj {
        List<T> objects = new List<T>();
        List<long> idList;
        if (m_nameIndex.TryGetValue(name, out idList)) {
            #if DEBUG
                if (idList.Count == 0) { throw new System.IndexOutOfRangeException("Zero-sized list should never be zero-sized here"); }
            #endif
            foreach(long id in idList) {
                IObj obj = m_children[id];
                if (obj is T) {
                    objects.Add(obj as T);
                }
            }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                objects.AddRange(subReg.FindObjsOfType<T>(name, true));
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
            foreach(IObjRegistry subReg in m_subRegistries) {
                toc.AddRange(subReg.AllNames(true));
            }
        }
        return toc;
    }
    public List<string> AllNamesOfDataObjs(bool recursive=true) {
        List<string> names = new List<string>();
        foreach(long id in m_dataObjs) { names.Add(m_children[id].Name); }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                names.AddRange(subReg.AllNamesOfDataObjs(true));
            }
        }
        return names;
    }
    public List<string> AllNamesOfType<T>(bool recursive=true) where T : class, IObj {
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
            foreach(IObjRegistry subReg in m_subRegistries) {
                toc.AddRange(subReg.AllNamesOfType<T>(true));
            }
        }
        return toc;
    }
    public HashSet<string> UniqueNames(bool recursive=true) {
        HashSet<string> toc = new HashSet<string>();
        foreach(KeyValuePair<string, List<long>> entry in m_nameIndex) { toc.Add(entry.Key); }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                toc.UnionWith(subReg.UniqueNames(true));
            }
        }
        return toc;
    }
    public HashSet<string> UniqueNamesOfDataObjs(bool recursive=true) {
        HashSet<string> names = new HashSet<string>();
        foreach(long id in m_dataObjs) { names.Add(m_children[id].Name); }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                names.UnionWith(subReg.UniqueNamesOfDataObjs(true));
            }
        }
        return names;
    }
    public HashSet<string> UniqueNamesOfType<T>(bool recursive=true) where T : class, IObj {
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
            foreach(IObjRegistry subReg in m_subRegistries) {
                toc.UnionWith(subReg.UniqueNamesOfType<T>(true));
            }
        }
        return toc;
    }
    public List<long> Index(bool recursive=true) {
        List<long> index = new List<long>(m_children.Keys);
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                index.AddRange(subReg.Index(true));
            }
        }
        return index;
    }
    public List<long> IndexOfDataObjs(bool recursive=true) {
        List<long> index = m_dataObjs.ToList();
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                index.AddRange(subReg.IndexOfDataObjs(true));
            }
        }
        return index;
    }
    public List<long> IndexOfType<T>(bool recursive=true) where T : class, IObj {
        List<long> index = new List<long>();
        foreach(KeyValuePair<long, IObj> entry in m_children) {
            if (entry.Value is T) {
                index.Add(entry.Key);
            }
        }
        if (recursive) {
            foreach(IObjRegistry subReg in m_subRegistries) {
                index.AddRange(subReg.IndexOfType<T>(true));
            }
        }
        return index;
    }
    public void RegisterChild(IObj obj) {
        if (obj.Id == GlobalRegistrar.IdAnonymous) {
            // Needs an ID
            obj.SetId(GlobalRegistrar.GetNextId());
        } else {
            // Check for ID collisions
            if (m_children.ContainsKey(obj.Id)) {
                throw new System.FieldAccessException("Registry already contains ID " + obj.Id);
            }
        }
        m_children.Add(obj.Id, obj);

        IObjRegistry subRegistry = obj.ObjRegistry();
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
        IDataObjMeta dataMeta = obj.DataObjMeta();
        if (dataMeta != null) {
            m_dataObjs.Add(obj.Id);
            m_idHashSetsByDataType[(int)dataMeta.DataType].Add(obj.Id);
        }
    }
    public bool UnregisterChild(IObj obj) {
        IDataObjMeta dataMeta = obj.DataObjMeta();
        if (dataMeta != null) {
            m_dataObjs.Remove(obj.Id);
            m_idHashSetsByDataType[(int)dataMeta.DataType].Remove(obj.Id);
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
        IObjRegistry subRegistry = obj.ObjRegistry();
        if (subRegistry != null) {
            m_subRegistries.Remove(subRegistry);
        }
        return m_children.Remove(obj.Id);
    }
    public bool UnregisterChild(long id) {
        IObj obj;
        if (m_children.TryGetValue(id, out obj)) {
            return UnregisterChild(obj);
        }
        return false;
    }
    public override IObj Clone(IObjRegistry parent = null) {
        if (m_clonable) {
            IObjRegistry newRegistry = new ObjRegistry(m_name, parent);
            return newRegistry;
        }
        return this;
    }
    public virtual CloneResult CloneFamily(IObjRegistry parent = null) {
        if (m_clonable) {
            IObjRegistry newThis = new ObjRegistry(m_name, parent);
            CloneResult cr = new CloneResult(this, newThis);
            foreach(KeyValuePair<long, IObj> entry in m_children) {
                IObj child = entry.Value;
                IObjRegistry childReg = child.ObjRegistry();
                if (childReg != null) {
                    cr.Add(childReg.CloneFamily(newThis));
                } else {
                    cr.Add(child, child.Clone(newThis));
                }
            }
            return cr;
        }
        return new CloneResult(this, this);
    }

    // Initialise member fields
    protected void Init() {
        m_nameIndex = new Dictionary<string, List<long>>();
        m_dataObjs = new HashSet<long>();
        m_idHashSetsByDataType = new List<HashSet<long>>(DataTypeStaticData.nDataTypeEnums);
        m_subRegistries = new HashSet<IObjRegistry>();
        for(int i = 0; i < m_idHashSetsByDataType.Count; ++i) {
            m_idHashSetsByDataType[i] = new HashSet<long>();
        }
        if (m_children == null) {
            m_children = new Dictionary<long, IObj>();
        }
    }

    // *** Constructors
    // Can never be in an invalid state - all constructors fully build the instance
    // Stream (future), Components, Copy, Null
    public ObjRegistry(string name, IObjRegistry parent = null, List<IObj> children = null)
    : base (name, parent) {
        Init();
        if (children != null) {
            foreach (IObj child in children) {
                RegisterChild(child);
            }
        }
    }
    public ObjRegistry(ObjRegistry reg, out CloneResult cr) : base(reg) {
        Init();
        cr = new CloneResult(reg, this);
        foreach(KeyValuePair<long, IObj> entry in reg.m_children) {
            IObj child = entry.Value;
            IObjRegistry childReg = child.ObjRegistry();
            if (childReg != null) {
                cr.Add(childReg.CloneFamily(this));
            } else {
                cr.Add(child, child.Clone(this));
            }
        }
    }
    public ObjRegistry(ObjRegistry reg) : base(reg) {
        Init();
        foreach(KeyValuePair<long, IObj> entry in reg.m_children) {
            IObj child = entry.Value;
            IObjRegistry childReg = child.ObjRegistry();
            if (childReg != null) {
                childReg.CloneFamily(this);
            } else {
                child.Clone(this);
            }
        }
    }
    public ObjRegistry() : base () { Init(); }

    ~ObjRegistry() {
        // I don't need to unregister myself, but my I need to unregister my children.
        List<long> keys = m_children.Keys.ToList();
        foreach(long id in keys) {
            m_children[id].UnregisterFromParent();
        }
    }
}
