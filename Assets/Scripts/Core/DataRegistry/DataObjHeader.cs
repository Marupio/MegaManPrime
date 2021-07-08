public interface IDataObjMeta : IObj {
    DataTypeEnum DataType { get; }
    // Sideways
    ISourceDataObjMeta SourceDataObjMeta();
    IDerivedDataObjMeta DerivedDataObjMeta();
    // Down
    IDataSetObjMeta DataSetObjMeta();
}

public class DataObjHeader : ObjHeader, IDataObjMeta {
    public virtual DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public ISourceDataObjMeta SourceDataObjMeta() { return this as ISourceDataObjMeta; }
    public IDerivedDataObjMeta DerivedDataObjMeta() { return this as IDerivedDataObjMeta; }
    public IDataSetObjMeta DataSetObjMeta() { return this as IDataSetObjMeta; }
    public override IObj Clone(IObjRegistry parent) {
        DataObjHeader newObj = new DataObjHeader(this);
        return (IObj)newObj;
    }

    public DataObjHeader(string name, IObjRegistry parent = null) : base (name, parent) {}
    public DataObjHeader(DataObjHeader obj) : base(obj) {}
    public DataObjHeader() {}
}
