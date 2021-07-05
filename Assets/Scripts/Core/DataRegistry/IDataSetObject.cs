// L main type, C component type.  e.g. Vector2, float.  List<Vector2>, Vector2.
public interface IDataSetObjectId : IDataObjectHeader {
    public DataTypeEnum ComponentType { get; }
    public string GetComponentName(int elem);
    public string GetComponentName(string elem);
    public bool ElementAccessByIndex { get; }
    public bool ElementAccessByString { get; }
}
public interface IDataSetObject<L, C> : IDataSetObjectId, IDataObject<L> {
    public C this[int elem] { get; set; }
    public C GetComponent(int elem);
    public C GetComponent(string elem);
    public void SetComponent(int elem, C value);
    public void SetComponent(string elem, C value);
}
