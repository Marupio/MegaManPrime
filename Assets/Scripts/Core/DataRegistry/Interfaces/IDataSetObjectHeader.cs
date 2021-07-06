// L main type, C component type.  e.g. Vector2, float.  List<Vector2>, Vector2.
public interface IDataSetObjectHeader : IDataObjectHeader {
    public DataTypeEnum ComponentType { get; }
    public string GetComponentName(int elem);
    public int GetComponentIndex(string elem);
    public bool ElementAccessByIndex { get; }
    public bool ElementAccessByString { get; }
}

public class DataSetObjectHeader : DataObjectHeader, IDataSetObjectHeader {
    public virtual DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public virtual string GetComponentName(int elem) { return elem.ToString(); }
    public virtual int GetComponentIndex(string elem) { return int.Parse(elem); }
    public virtual bool ElementAccessByIndex { get=>true; }
    public virtual bool ElementAccessByString { get=>false; }
    public DataSetObjectHeader(string name, IObjectRegistry parent = null) : base(name, parent) {}
    public DataSetObjectHeader(IDataSetObjectHeader obj) : base(obj) {}
    public DataSetObjectHeader(DataSetObjectHeader obj) : base(obj) {}
    public DataSetObjectHeader() : base() {}
}
