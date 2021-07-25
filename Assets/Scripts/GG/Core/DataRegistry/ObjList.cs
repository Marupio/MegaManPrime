using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjListBase<T> : IEnumerable<T> where T : class, IObj {
    protected List<T> m_objList;
    protected ObjConstraintBase<T> m_constraint;

    public ObjConstraintBase<T> Constraint {
        get=>m_constraint;
        set {
            m_constraint = value;
            if (m_constraint != null) {
                m_objList.RemoveAll(obj => !m_constraint.Test(obj));
            }
        }
    }
    public void SetConstraintUnsafe(ObjConstraintBase<T> constraint) {
        m_constraint = constraint;
    }

    public int Capacity { get=>m_objList.Capacity; }
    public int Count { get=>m_objList.Count; }
    public virtual T this[int index] {
        get=>m_objList[index];
        set {
            if (m_constraint.Test(value)) {
                m_objList.Add(value);
            } else {
                #if DEBUG
                    Debug.LogWarning("Filtered " + value.Name + " from ObjList");
                #endif
            }
        }
    }

    public void Add(T obj) {
        if (m_constraint == null) {
            m_objList.Add(obj);
            return;
        }
        if (m_constraint.Test(obj)) {
            m_objList.Add(obj);
        } else {
            #if DEBUG
                Debug.LogWarning("Filtered " + obj.Name + " from ObjList");
            #endif
        }
    }
    public void AddRange(IEnumerable<T> objs) {
        InsertRange(m_objList.Count, objs);
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
    public void Insert(int index, T item) {
        if (m_constraint == null) {
            m_objList.Insert(index, item);
        } else {
            if (m_constraint.Test(item)) {
                m_objList.Insert(index, item);
            } else {
                #if DEBUG
                    Debug.LogWarning("Filtered " + item.Name + " from ObjList");
                #endif
            }
        }
    }
    public void InsertRange(int atIndex, IEnumerable<T> objs) {
        if (m_constraint == null) {
            m_objList.InsertRange(atIndex, objs);
            return;
        }
        if (objs == null) {
            Debug.LogException(new System.ArgumentNullException("objs"));
            return;
        }
        if (atIndex >= m_objList.Count) {
            Debug.LogException(new System.ArgumentOutOfRangeException("atIndex " + atIndex + " out of range [0.." + (m_objList.Count-1) + "]"));
            return;
        }
        ICollection<T> c = objs as ICollection<T>;
        if (c != null) {
            int count = c.Count;
            if (count > 0) {
                m_objList.AddRange(
                    from obj in c
                    where m_constraint.Test(obj)
                    select obj
                );
                return;
            }
        } else {
            using(IEnumerator<T> en = objs.GetEnumerator()) {
                while(en.MoveNext()) {
                    if (m_constraint.Test(en.Current)) {
                        m_objList.Add(en.Current);
                    }
                }
            }
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
    public void Reverse(int index, int count) {
        m_objList.Reverse(index, count);
    }
    public void Reverse() {
        m_objList.Reverse();
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
    public ObjListBase(ObjConstraintBase<T> constraint = null) {
        m_constraint = constraint;
        m_objList = new List<T>();
    }
    public ObjListBase(IEnumerable<T> collection, ObjConstraintBase<T> constraint = null) {
        if (constraint == null) {
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
                where m_constraint.Test(obj)
                select obj
            );
        } else {
            m_objList = new List<T>();
            using(IEnumerator<T> en = collection.GetEnumerator()) {
                while(en.MoveNext()) {
                    if (m_constraint.Test(en.Current)) {
                        m_objList.Add(en.Current);
                    }
                }
            }
        }
    }
    public ObjListBase(int capacity, ObjConstraintBase<T> constraint = null) {
        m_constraint = constraint;
        m_objList = new List<T>(capacity);
    }
}

public class ObjList : ObjListBase<IObj> {
    public ObjList(ObjConstraintBase<IObj> constraint = null) {
        m_constraint = constraint;
        m_objList = new List<IObj>();
    }
    public ObjList(IEnumerable<IObj> collection, ObjConstraintBase<IObj> constraint = null) : base(collection, constraint) {}
    public ObjList(int capacity, ObjConstraintBase<IObj> constraint = null) : base(capacity, constraint) {}
}
public class DataObjList : ObjListBase<IDataObjMeta> {
    public DataObjList(ObjConstraintBase<IDataObjMeta> constraint = null) {
        m_constraint = constraint;
        m_objList = new List<IDataObjMeta>();
    }
    public DataObjList(IEnumerable<IDataObjMeta> collection, ObjConstraintBase<IDataObjMeta> constraint = null) : base(collection, constraint) {}
    public DataObjList(int capacity, ObjConstraintBase<IDataObjMeta> constraint = null) : base(capacity, constraint) {}
}
