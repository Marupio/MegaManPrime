using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A list for IObj elements, with a built-in Predicate for filtering elements as they are added
/// The predicate (ObjPass) has Null types (i.e. pass everything).  Rather than test each element for no useful effect, this class stores a Null
/// type of predicate as null, and uses if (m_constraint != null) before ever using it.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjListBase<T> : IEnumerable<T> where T : class, IObj {
    protected List<T> m_objList;
    protected IObjPass<T> m_constraint;
    // TODO - add hashed indexing based on (key=IObj.Id, value=index)
    // protected Dictionary<long, int> m_contents;

    public IObjPass<T> Constraint { get=>m_constraint; }
    public int Capacity { get=>m_objList.Capacity; }
    public int Count { get=>m_objList.Count; }
    public virtual T this[int index] {
        get=>m_objList[index];
        set {
            if (m_constraint.Pass(value)) {
                m_objList.Add(value);
            } else {
                #if DEBUG
                    Debug.LogWarning("Filtered " + value.Name + " from ObjList");
                #endif
            }
        }
    }

    public List<T> Data { get=>m_objList; }

    public bool Add(T obj) {
        if (m_constraint == null || obj == null || m_constraint.Pass(obj)) {
            m_objList.Add(obj);
            return true;
        } else {
            // #if DEBUG
            //     Debug.LogWarning("Filtered " + obj.Name + " from ObjList");
            // #endif
            return false;
        }
    }
    public int AddRange(IEnumerable<T> objs) {
        return InsertRange(m_objList.Count, objs);
    }
    public void AddUnsafe(T obj) {
        m_objList.Add(obj);
    }
    public void AddRangeUnsafe(IEnumerable<T> objs) {
        m_objList.AddRange(objs);
    }

    // TODO - ReadOnlyCollection
    // TODO - BinarySearch - need Comparisons between IObjs
    
    public void Clear() {
        m_objList.Clear();
    }
    public bool Contains(T obj) {
        return m_objList.Contains(obj);
    }
    public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) {
        return m_objList.ConvertAll<TOutput>(converter);
    }
    public void CopyTo(int index, T[] array, int arrayIndex, int count) {
        m_objList.CopyTo(index, array, arrayIndex, count);
    }
    public void CopyTo(T[] array, int arrayIndex) {
        m_objList.CopyTo(array, arrayIndex);
    }
    public void CopyTo(T[] array) {
        m_objList.CopyTo(array);
    }
    public bool Exists(Predicate<T> match) {
        return m_objList.Exists(match);
    }
    public bool Fill(T item) {
        if (m_constraint == null || item == null || m_constraint.Pass(item)) {
            for (int i = 0; i < m_objList.Count; ++i) {
                m_objList[i] = item;
            }
            return true;
        }
        return false;
    }
    public T Find(Predicate<T> match) {
        return m_objList.Find(match);
    }
    public List<T> FindAll(Predicate<T> match) {
        return m_objList.FindAll(match);
    }
    public int FindIndex(int startIndex, int count, Predicate<T> match) {
        return m_objList.FindIndex(startIndex, count, match);
    }
    public int FindIndex(int startIndex, Predicate<T> match) {
        return m_objList.FindIndex(startIndex, match);
    }
    public int FindIndex(Predicate<T> match) {
        return m_objList.FindIndex(match);
    }
    public T FindLast(Predicate<T> match) {
        return m_objList.FindLast(match);
    }
    public int FindLastIndex(int startIndex, int count, Predicate<T> match) {
        return m_objList.FindLastIndex(startIndex, count, match);
    }
    public int FindLastIndex(int startIndex, Predicate<T> match) {
        return m_objList.FindLastIndex(startIndex, match);
    }
    public int FindLastIndex(Predicate<T> match) {
        return m_objList.FindLastIndex(match);
    }
    public void ForEach(Action<T> action) {
        m_objList.ForEach(action);
    }

    // *** IEnumerable interface
    public IEnumerator<T> GetEnumerator()
    {
        return m_objList.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int IndexOf(T item, int index, int count) {
        return m_objList.IndexOf(item, index, count);
    }
    public int IndexOf(T item, int index) {
        return m_objList.IndexOf(item, index);
    }
    public int IndexOf(T item) {
        return m_objList.IndexOf(item);
    }
    public bool Insert(int index, T item) {
        if (m_constraint == null || item == null || m_constraint.Pass(item)) {
            m_objList.Insert(index, item);
            return true;
        }
        // #if DEBUG
        //     Debug.LogWarning("Filtered " + item.Name + " from ObjList");
        // #endif
        return false;
    }
    public int InsertRange(int atIndex, IEnumerable<T> objs) {
        if (m_constraint == null) {
            m_objList.InsertRange(atIndex, objs);
            return objs.Count() - atIndex;
        }
        if (objs == null) {
            Debug.LogException(new System.ArgumentNullException("objs"));
            return 0;
        }
        if (atIndex >= m_objList.Count) {
            Debug.LogException(new System.ArgumentOutOfRangeException("atIndex " + atIndex + " out of range [0.." + (m_objList.Count-1) + "]"));
            return 0;
        }
        ICollection<T> c = objs as ICollection<T>;
        if (c != null) {
            int count = c.Count;
            if (count > 0) {
                int sizeBefore = m_objList.Count;
                m_objList.AddRange(
                    from obj in c
                    where obj == null || m_constraint.Pass(obj)
                    select obj
                );
                return m_objList.Count - sizeBefore;
            }
            return 0;
        } else {
            int count = 0;
            using(IEnumerator<T> en = objs.GetEnumerator()) {
                while(en.MoveNext()) {
                    if (en.Current == null || m_constraint.Pass(en.Current)) {
                        m_objList.Add(en.Current);
                        ++count;
                    }
                }
            }
            return count;
        }
    }
    public void InsertUnsafe(int index, T item) {
        m_objList.Insert(index, item);
    }
    public void InsertRangeUnsafe(int atIndex, IEnumerable<T> objs) {
        m_objList.InsertRange(atIndex, objs);
    }
    public int LastIndexOf(T item) {
        return m_objList.LastIndexOf(item);
    }
    public int LastIndexOf(T item, int index) {
        return m_objList.LastIndexOf(item, index);
    }
    public int LastIndexOf(T item, int index, int count) {
        return m_objList.LastIndexOf(item, index, count);
    }
    public bool Remove(T item) {
        return m_objList.Remove(item);
    }
    public int RemoveAll(Predicate<T> match) {
        return m_objList.RemoveAll(match);
    }
    public void RemoveAt(int index) {
        m_objList.RemoveAt(index);
    }
    public void RemoveRange(int index, int count) {
        m_objList.RemoveRange(index, count);
    }
    public bool ResizeAndFill(int size, T item) {
        if (m_constraint == null || item == null || m_constraint.Pass(item)) {
            if (size < m_objList.Count) {
                RemoveRange(size, m_objList.Count - size);
                Fill(item);
                return true;
            }
            if (size == m_objList.Count) {
                Fill(item);
                return true;
            }
            // size > count
            Fill(item);
            int nDiff = size - m_objList.Count;
            for (int i = 0; i < nDiff; ++i) {
                m_objList.Add(item);
            }
            return true;
        }
        // Fails constraint - use null
        if (size < m_objList.Count) {
            RemoveRange(size, m_objList.Count - size);
            Fill(null);
            return false;
        }
        if (size == m_objList.Count) {
            Fill(null);
            return false;
        }
        // size > count
        Fill(null);
        int nnDiff = size - m_objList.Count;
        for (int i = 0; i < nnDiff; ++i) {
            m_objList.Add(null);
        }
        return false;
    }
    public void Reverse(int index, int count) {
        m_objList.Reverse(index, count);
    }
    public void Reverse() {
        m_objList.Reverse();
    }

    // A replacement for this[i] = value that provides feedback if the value passed
    public bool SetAt(int index, T item) {
        if (m_constraint == null || item == null || m_constraint.Pass(item)) {
            m_objList[index] = item;
            return true;
        }
        return false;
    }

    public void SetEqual(IEnumerable<T> collection) {
        // if (constraint == null || constraint.Null) {
        //     m_objList = new List<T>(collection);
        //     return;
        // }
        m_objList.Clear();
        if (collection == null) {
            Debug.LogException(new System.ArgumentNullException("collection"));
            return;
        }
        ICollection<T> c = collection as ICollection<T>;
        if (c != null) {
            if (m_constraint == null) {
                m_objList.AddRange(c);
            } else {
                m_objList.AddRange(
                    from obj in c
                    where obj == null || m_constraint.Pass(obj)
                    select obj
                );
            }
        } else {
            if (m_constraint == null) {
                using(IEnumerator<T> en = collection.GetEnumerator()) {
                    while(en.MoveNext()) {
                        m_objList.Add(en.Current);
                    }
                }
            } else {
                using(IEnumerator<T> en = collection.GetEnumerator()) {
                    while(en.MoveNext()) {
                        if (en.Current == null || m_constraint.Pass(en.Current)) {
                            m_objList.Add(en.Current);
                        }
                    }
                }
            }
        }
    }


    // TODO - Comparisons between IObjs
    //  including: by Id, by MTag, by Name
    //  then expand to DataObjs: by value, by Magnitude, etc..

    public T[] ToArray() {
        return m_objList.ToArray();
    }
    public void TrimExcess() {
        m_objList.TrimExcess();
    }
    public bool TrueForAll(Predicate<T> match) {
        return m_objList.TrueForAll(match);
    }
    // TODO public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
    // Look into syntax for this
    // Can I do something like: using m_objList.Enumerator?

    // *** Constructors - patterned after List ctors
    public ObjListBase(IObjPass<T> constraint = null) {
        if (constraint != null && !constraint.Null) {
            m_constraint = constraint;
        }
        m_objList = new List<T>();
    }
    public ObjListBase(IEnumerable<T> collection, IObjPass<T> constraint = null) {
        if (constraint == null || constraint.Null) {
            m_objList = new List<T>(collection);
            return;
        }
        m_constraint = constraint;
        if (collection == null) {
            Debug.LogException(new System.ArgumentNullException("collection"));
            m_objList = new List<T>();
            return;
        }
        ICollection<T> c = collection as ICollection<T>;
        if (c != null) {
            m_objList = new List<T>(
                from obj in c
                where m_constraint.Pass(obj)
                select obj
            );
        } else {
            m_objList = new List<T>();
            using(IEnumerator<T> en = collection.GetEnumerator()) {
                while(en.MoveNext()) {
                    if (m_constraint.Pass(en.Current)) {
                        m_objList.Add(en.Current);
                    }
                }
            }
        }
    }
    public ObjListBase(int capacity, IObjPass<T> constraint = null) {
        if (constraint != null && !constraint.Null) {
            m_constraint = constraint;
        }
        m_objList = new List<T>(capacity);
    }
    /// <summary>
    /// Wrap existing list with ObjList
    /// </summary>
    public ObjListBase(List<T> lst, IObjPass<T> constraint = null) {
        if (constraint != null && !constraint.Null) {
            m_constraint = constraint;
        }
        m_objList = lst;
    }
}

/// <summary>
/// ObjList - explicitly uses IObjPass<IObj> constraints
/// </summary>
public class ObjList : ObjListBase<IObj> {
    public ObjList(IObjPass<IObj> constraint = null) {
        if (constraint != null && !constraint.Null) {
            m_constraint = constraint;
        }
        m_objList = new List<IObj>();
    }
    public ObjList(IEnumerable<IObj> collection, IObjPass<IObj> constraint = null) : base(collection, constraint) {}
    public ObjList(int capacity, IObjPass<IObj> constraint = null) : base(capacity, constraint) {}
    public ObjList(List<IObj> lst, IObjPass<IObj> constraint = null) : base(lst, constraint) {}
}
public class TypedPassObjList<T> : ObjList where T : class, IObjPass<IObj> {
    public TypedPassObjList() : base(Activator.CreateInstance<T>()) {}
    public TypedPassObjList(IEnumerable<IObj> collection) : base (collection, Activator.CreateInstance<T>()) {}
    public TypedPassObjList(int capacity) : base(capacity, Activator.CreateInstance<T>()) {}
    public TypedPassObjList(List<IObj> lst) : base(lst, Activator.CreateInstance<T>()) {}
}

public class ObjList_AllPass : TypedPassObjList<ObjPassNull> {
    public ObjList_AllPass() : base() {}
    public ObjList_AllPass(IEnumerable<IObj> collection) : base(collection) {}
    public ObjList_AllPass(int capacity) : base(capacity) {}
    public ObjList_AllPass(List<IObj> lst) : base(lst) {}
}

/// <summary>
/// DataObjList - explicitly uses IObjPass<IDataObjMeta> constraints
/// </summary>
public class DataObjList : ObjListBase<IDataObjMeta> {
    public DataObjList(IObjPass<IDataObjMeta> constraint = null) {
        if (constraint != null && !constraint.Null) {
            m_constraint = constraint;
        }
        m_objList = new List<IDataObjMeta>();
    }
    public DataObjList(IEnumerable<IDataObjMeta> collection, IObjPass<IDataObjMeta> constraint = null) : base(collection, constraint) {}
    public DataObjList(int capacity, IObjPass<IDataObjMeta> constraint = null) : base(capacity, constraint) {}
    public DataObjList(List<IDataObjMeta> lst, IObjPass<IDataObjMeta> constraint = null) : base(lst, constraint) {}
}

public class TypedPassDataObjList<T> : DataObjList where T : class, IDataObjPass<IDataObjMeta> {
    public TypedPassDataObjList() : base(Activator.CreateInstance<T>()) {}
    public TypedPassDataObjList(IEnumerable<IDataObjMeta> collection) : base (collection, Activator.CreateInstance<T>()) {}
    public TypedPassDataObjList(int capacity) : base(capacity, Activator.CreateInstance<T>()) {}
    public TypedPassDataObjList(List<IDataObjMeta> lst) : base(lst, Activator.CreateInstance<T>()) {}
    public TypedPassDataObjList(DataObjList lst) : base(Activator.CreateInstance<T>()) {
        if (lst.Constraint.GetType() == typeof(T)) {
            m_objList = lst.Data;
        } else {
            m_objList = new List<IDataObjMeta>();
            AddRange(lst.Data);
        }
    }
}

public class DataObjList_AllPass : TypedPassDataObjList<DataObjPassNull> {
    public DataObjList_AllPass() : base() {}
    public DataObjList_AllPass(IEnumerable<IDataObjMeta> collection) : base(collection) {}
    public DataObjList_AllPass(int capacity) : base(capacity) {}
    public DataObjList_AllPass(List<IDataObjMeta> lst) : base(lst) {}
    public DataObjList_AllPass(TypedPassDataObjList<DataObjPassNull> lst) : base() { m_objList = lst.Data; }
    public DataObjList_AllPass(DataObjList lst) : base() {
        if (lst.Constraint.GetType() == typeof(DataObjPassNull) ) {
            m_objList = lst.Data;
        } else {
            m_objList = new List<IDataObjMeta>();
            AddRange(lst.Data);
        }
    }
}
public class DataObjList_SourcePass : TypedPassDataObjList<DataObjPassSourceData> {
    public DataObjList_SourcePass() : base() {}
    public DataObjList_SourcePass(IEnumerable<IDataObjMeta> collection) : base(collection) {}
    public DataObjList_SourcePass(int capacity) : base(capacity) {}
    public DataObjList_SourcePass(List<IDataObjMeta> lst) : base(lst) {}
    public DataObjList_SourcePass(TypedPassDataObjList<DataObjPassSourceData> lst) : base() { m_objList = lst.Data; }
    public DataObjList_SourcePass(DataObjList lst) : base() {
        if (lst.Constraint.GetType() == typeof(DataObjPassSourceData) ) {
            m_objList = lst.Data;
        } else {
            m_objList = new List<IDataObjMeta>();
            AddRange(lst.Data);
        }
    }
}
public class DataObjList_DerivedPass : TypedPassDataObjList<DataObjPassDerivedData> {
    public DataObjList_DerivedPass() : base() {}
    public DataObjList_DerivedPass(IEnumerable<IDataObjMeta> collection) : base(collection) {}
    public DataObjList_DerivedPass(int capacity) : base(capacity) {}
    public DataObjList_DerivedPass(List<IDataObjMeta> lst) : base(lst) {}
    public DataObjList_DerivedPass(TypedPassDataObjList<DataObjPassDerivedData> lst) : base() { m_objList = lst.Data; }
    public DataObjList_DerivedPass(DataObjList lst) : base() {
        if (lst.Constraint.GetType() == typeof(DataObjPassDerivedData) ) {
            m_objList = lst.Data;
        } else {
            m_objList = new List<IDataObjMeta>();
            AddRange(lst.Data);
        }
    }
}
