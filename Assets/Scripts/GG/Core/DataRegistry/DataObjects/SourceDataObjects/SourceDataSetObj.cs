using UnityEngine;

public abstract class SourceDataSetObj<L, C> : SourceDataObjHeader, IDataSetObj<L, C> {
    public L m_data;
    public virtual ITraitsSimple<L> TraitsSimple { get; }
    public virtual ITraits<L, C> Traits { get; }
    public L Data { get => m_data; set { TraitsSimple.SetEqual(ref m_data, value); SetModified(); } }
    public abstract DataTypeEnum ComponentType { get; }
    public abstract ComponentAccessType PreferredAccessType { get; }
    public abstract int NComponents { get; } // -1 = use size query
    public abstract bool ElementAccessByIndex();
    public abstract bool ElementAccessByString();
    public abstract string GetComponentName(int elem);
    public abstract int GetComponentIndex(string elem);
    public abstract C this[int elem] { get; set; }
    public abstract C this[string elem] { get; set; }
    public SourceDataSetObj(string name, IObjRegistry parent = null, L data = default(L)) : base(name, parent) { TraitsSimple.SetEqual(ref m_data, data); }
    public SourceDataSetObj(SourceDataSetObj<L, C> obj) : base(obj) {
        TraitsSimple.SetEqual(ref m_data, obj.m_data);
    }
    public SourceDataSetObj() { }
}
