public abstract class SourceDataSetObj<L, C> : DataSetObjHeader, IDataSetObj<L, C> {
    public L m_data;
    public virtual ITraitsSimple<L> TraitsSimple { get; }
    public virtual ITraits<L, C> Traits { get; }
    public L Data { get => m_data; set { TraitsSimple.SetEqual(ref m_data, value); SetModified(); } }
    public C this[int elem] { get; set; }
    public C this[string elem] { get; set; }
    public SourceDataObj(
        string name,
        IObjRegistry parent = null,
        L data = default(L)
    ) : base(name, parent) { TraitsSimple.SetEqual(ref m_data, data); }
    public SourceDataObj(SourceDataObj<L> obj) : base(obj) {
        TraitsSimple.SetEqual(ref m_data, obj.m_data);
    }
    public SourceDataObj() { }
}
