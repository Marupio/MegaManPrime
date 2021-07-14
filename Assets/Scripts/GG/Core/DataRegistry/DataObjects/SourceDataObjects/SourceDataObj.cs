using UnityEngine;

public abstract class SourceDataObj<L> : SourceDataObjHeader, IDataObj<L> {
    public L m_data;
    public virtual ITraitsSimple<L> TraitsSimple { get; }
    public L Data { get=>m_data; set { TraitsSimple.SetEqual(ref m_data, value); SetModified(); }  }
    public SourceDataObj(
        string name,
        IObjRegistry parent = null,
        L data = default(L)
    ) : base (name, parent) { TraitsSimple.SetEqual(ref m_data, data); }
    public SourceDataObj(SourceDataObj<L> obj) : base(obj) {
        TraitsSimple.SetEqual(ref m_data, obj.m_data);
    }
    public SourceDataObj() {}
}
