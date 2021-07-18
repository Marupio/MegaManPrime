// Currently unused
// Not used by SourceData... and DerivedData...

public abstract class DataSetObjHeader : DataObjHeader, IDataSetObjMeta {
    public abstract DataTypeEnum ComponentType { get; }
    public abstract ComponentAccessType PreferredAccessType { get; }
    public abstract bool ElementAccessByIndex();
    public abstract bool ElementAccessByString();
    public abstract string GetComponentName(int elem);
    public abstract int GetComponentIndex(string elem);
    public abstract int NComponents { get; } // -1 = use size query
    public DataSetObjHeader(string name, IObjRegistry parent = null) : base(name, parent) {}
    public DataSetObjHeader(DataSetObjHeader obj) : base(obj) {}
    public DataSetObjHeader() {}
}
