using System.Collections.Generic;
using UnityEngine;

// public interface IDerivedDataObj<L> : IDataObj<L>, IDerivedDataObjMeta {
//     IDerivedUpdater<L> Updater { get; set; }
//     void UpdateDerived();
// }

public abstract class DerivedDataObj<L> : DerivedDataObjHeader, IDerivedDataObj<L> {
    protected L m_data;
    public virtual ITraitsSimple<L> TraitsSimple { get; }
    public L Data {
        get {
            if (!UpToDate()) {
                UpdateDerived();
            }
            return m_data;
        }
    }
    public L DataNoUpdate { get=>m_data; }
    public DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        L data = default(L)
    ) : base (name, parent, updater) {
        // Even though I'm derived, I take a value because I can be 'stale'
        m_data = TraitsSimple.Zero;
        if (!TraitsSimple.TestEquals(m_data, data)) {
            TraitsSimple.SetEqual(ref m_data, data);
        }
    }
    public DerivedDataObj(DerivedDataObj<L> obj) : base(obj) {
        TraitsSimple.SetEqual(ref m_data, obj.m_data);
    }
    public DerivedDataObj() {}
}
