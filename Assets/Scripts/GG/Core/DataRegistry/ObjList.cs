using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjConstraint {
    public abstract bool Test(IObj obj);
}

public class ObjConstraintMustBeClass<T> : ObjConstraint where T : class, IObj {
    public override bool Test(IObj obj) {
        return obj is T;
    }
}

public class ObjConstraintList : ObjConstraint {
    public List<ObjConstraint> constraints;
    public override bool Test(IObj obj) {
        foreach(ObjConstraint constraint in constraints) {
            if (!constraint.Test(obj)) { return false; }
        }
        return true;
    }
}

public class ObjList : IEnumerable<IObj> {
    protected List<IObj> m_objList;
    protected ObjConstraint m_constraint;

    public ObjConstraint Constraint {
        get=>m_constraint;
        set {
            m_constraint = value;
            if (m_constraint != null) {
                m_objList.RemoveAll(obj => !m_constraint.Test(obj));
            }
        }
    }

    public int Capacity { get=>m_objList.Capacity; }
    public int Count { get=>m_objList.Count; }
    public virtual IObj this[int index] {
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

    public void Add(IObj obj) {
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
    public void AddRange(IEnumerable<IObj> objs) {
        InsertRange(m_objList.Count, objs);
    }

    // TODO - ReadOnlyCollection
    // TODO - BinarySearch - need Comparisons between IObjs
    
    public void Clear() {
        m_objList.Clear();
    }
    public bool Contains(IObj obj) {
        return m_objList.Contains(obj);
    }
    public List<TOutput> ConvertAll<TOutput>(Converter<IObj, TOutput> converter) {
        return m_objList.ConvertAll<TOutput>(converter);
    }
    public void CopyTo(int index, IObj[] array, int arrayIndex, int count) {
        m_objList.CopyTo(index, array, arrayIndex, count);
    }
    public void CopyTo(IObj[] array, int arrayIndex) {
        m_objList.CopyTo(array, arrayIndex);
    }
    public void CopyTo(IObj[] array) {
        m_objList.CopyTo(array);
    }
    public bool Exists(Predicate<IObj> match) {
        return m_objList.Exists(match);
    }
    public IObj Find(Predicate<IObj> match) {
        return m_objList.Find(match);
    }
    public List<IObj> FindAll(Predicate<IObj> match) {
        return m_objList.FindAll(match);
    }
    public int FindIndex(int startIndex, int count, Predicate<IObj> match) {
        return m_objList.FindIndex(startIndex, count, match);
    }
    public int FindIndex(int startIndex, Predicate<IObj> match) {
        return m_objList.FindIndex(startIndex, match);
    }
    public int FindIndex(Predicate<IObj> match) {
        return m_objList.FindIndex(match);
    }
    public IObj FindLast(Predicate<IObj> match) {
        return m_objList.FindLast(match);
    }
    public int FindLastIndex(int startIndex, int count, Predicate<IObj> match) {
        return m_objList.FindLastIndex(startIndex, count, match);
    }
    public int FindLastIndex(int startIndex, Predicate<IObj> match) {
        return m_objList.FindLastIndex(startIndex, match);
    }
    public int FindLastIndex(Predicate<IObj> match) {
        return m_objList.FindLastIndex(match);
    }
    public void ForEach(Action<IObj> action) {
        m_objList.ForEach(action);
    }

    // *** IEnumerable interface
    public IEnumerator<IObj> GetEnumerator()
    {
        return m_objList.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int IndexOf(IObj item, int index, int count) {
        return m_objList.IndexOf(item, index, count);
    }
    public int IndexOf(IObj item, int index) {
        return m_objList.IndexOf(item, index);
    }
    public int IndexOf(IObj item) {
        return m_objList.IndexOf(item);
    }
    public void Insert(int index, IObj item) {
        if (m_constraint == null) {
            m_objList.Insert(index, item);
        } else {
            if (m_constraint.Test(item)) {
                m_objList.Insert(index, item);
            } else {
                #if DEBUG
                    Debug.LogWarning("Filtered " + obj.Name + " from ObjList");
                #endif
            }
        }
    }
    public void InsertRange(int atIndex, IEnumerable<IObj> objs) {
        if (m_constraint == null) {
            m_objList.InsertRange(atIndex, objs);
            return;
        }
        if (objs == null) {
            Debug.LogException(new System.ArgumentNullException("objs"));
            return;
        }
        if (atIndex >= m_objList.Count) {
            Debug.LogException(new System.ArgumentOutOfRangeException("atIndex " + atIndex + " out of range [0.." + (m_objList.Count-1) + "]");
            return;
        }
        ICollection<IObj> c = objs as ICollection<IObj>;
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
            using(IEnumerator<IObj> en = objs.GetEnumerator()) {
                while(en.MoveNext()) {
                    if (m_constraint.Test(en.Current)) {
                        m_objList.Add(en.Current);
                    }
                }
            }
        }
    }
    public int LastIndexOf(IObj item) {
        return m_objList.LastIndexOf(item);
    }
    public int LastIndexOf(IObj item, int index) {
        return m_objList.LastIndexOf(item, index);
    }
    public int LastIndexOf(IObj item, int index, int count) {
        return m_objList.LastIndexOf(item, index, count);
    }
    public bool Remove(IObj item) {
        return m_objList.Remove(item);
    }
    public int RemoveAll(Predicate<IObj> match) {
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

    public IObj[] ToArray() {
        return m_objList.ToArray();
    }
    public void TrimExcess() {
        m_objList.TrimExcess();
    }
    public bool TrueForAll(Predicate<IObj> match) {
        return m_objList.TrueForAll(match);
    }
    // TODO public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
    // Look into syntax for this
    // Can I do something like: using m_objList.Enumerator?

    // *** Constructors - patterned after List ctors
    public ObjList(ObjConstraint constraint = null) {
        m_constraint = constraint;
        m_objList = new List<IObj>();
    }
    public ObjList(IEnumerable<IObj> collection, ObjConstraint constraint = null) {
        if (constraint == null) {
            m_objList = new List<IObj>(collection);
            return;
        }
        m_constraint = constraint;
        if (collection == null) {
            Debug.LogException(new System.ArgumentNullException("collection"));
            m_objList = new List<IObj>();
            return;
        }
        ICollection<IObj> c = collection as ICollection<IObj>;
        if (c != null) {
            m_objList = new List<IObj>(
                from obj in c
                where m_constraint.Test(obj)
                select obj
            );
        } else {
            m_objList = new List<IObj>();
            using(IEnumerator<IObj> en = collection.GetEnumerator()) {
                while(en.MoveNext()) {
                    if (m_constraint.Test(en.Current)) {
                        m_objList.Add(en.Current);
                    }
                }
            }
        }
    }
    public ObjList(int capacity, ObjConstraint constraint = null) {
        m_constraint = constraint;
        m_objList = new List<IObj>(capacity);
    }
}







// RUBBISH CODE

// public abstract class ObjConstraint {
//     public abstract bool Test(IObj obj);
// }

// public class ObjMustBeClass<T> : ObjConstraint where T : class, IObj {
//     public override bool Test(IObj obj) {
//         return obj is T;
//     }
// }

// public class CombineObjConstraint : ObjConstraint {
//     List<ObjConstraint> m_constraints;
//     public override bool Test(IObj obj) {
//         foreach (ObjConstraint constraint in m_constraints) {
//             if (!constraint.Test(obj)) return false;
//         }
//         return true;
//     }
// }

// // public class ObjMustBeTypeEnum : ObjConstraint {
// // }

// public class ObjList : /* IEnumerable<maybeGeneric> */ {
//     protected List<IObj> m_objList;
//     protected ObjConstraint m_constraint;
//     public virtual IObj this[int index] {
//         get=>m_objList[index];
//         set {
//             if (!m_constraint.Test(value)) {
//                 Debug.LogWarning("Object " + value.Name + " filtered from list");
//                 return;
//             }
//             m_objList.Add(value);
//         }
//     }
// }

// public class DataObjList : ObjList {
//     List<IDataObjMeta> m_dataObjList;
//     public override IObj this[int index] {
//         get=>m_dataObjList[index];
//         set {
//             if (!m_constraint.Test(value)) {
//                 Debug.LogWarning("Object " + value.Name + " filtered from list");
//                 return;
//             }
//             m_objList.Add(value);
//         }
//     }
// }


