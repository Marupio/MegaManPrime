public abstract class DataObjHeader : ObjHeader, IDataObjMeta {
    public abstract DataTypeEnum DataType { get; }
    public ISourceDataObjMeta SourceDataObjMeta() { return this as ISourceDataObjMeta; }
    public IDerivedDataObjMeta DerivedDataObjMeta() { return this as IDerivedDataObjMeta; }
    public IDataSetObjMeta DataSetObjMeta() { return this as IDataSetObjMeta; }
    public DataObjHeader(string name, IObjRegistry parent = null) : base(name, parent) {}
    public DataObjHeader(DataObjHeader obj) : base(obj) {}
    public DataObjHeader() {}
}
