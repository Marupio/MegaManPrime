using System.Collections.Generic;
using UnityEngine;

public abstract class DerivedDataSetObj<L, C> : DerivedDataObjHeader, IDerivedDataSetObj<L, C> {
    public L m_data; // protected because it is derived, but maybe it needs to be public for ObjUpdater?
    public abstract DataTypeEnum ComponentType { get; }
    public abstract ComponentAccessType PreferredAccessType { get; }
    public abstract bool ElementAccessByIndex();
    public abstract bool ElementAccessByString();
    public abstract string GetComponentName(int elem);
    public abstract int GetComponentIndex(string elem);
    public abstract int NComponents { get; } // -1 = use size query
    public virtual ITraitsSimple<L> TraitsSimple { get; }
    public virtual ITraits<L, C> Traits { get; }
    public L Data {
        get {
            if (!UpToDate()) {
                UpdateDerived();
            }
            return m_data;
        }
    }
    public L DataNoUpdate { get=>m_data; }
    public abstract C this[int elem] { get; }
    public abstract C this[string elem] { get; }
    public abstract C GetComponentNoUpdate(int index);
    public abstract C GetComponentNoUpdate(string elem);
    public DerivedDataSetObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        L data = default(L)
    ) : base(name, parent, updater) {
        // Even though I'm derived, I take a value because I can be 'stale'
        m_data = TraitsSimple.Zero;
        if (!TraitsSimple.TestEquals(m_data, data)) {
            TraitsSimple.SetEqual(ref m_data, data);
        }
    }
    public DerivedDataSetObj(DerivedDataSetObj<L, C> obj) : base(obj) {
        TraitsSimple.SetEqual(ref m_data, obj.m_data);
    }
    public DerivedDataSetObj() {}
}
