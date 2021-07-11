public abstract class DataSetObjHeader : DataObjHeader, IDataSetObjMeta {
    public virtual DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public ComponentAccessType PreferredAccessType { get; }
    public abstract bool ElementAccessByIndex();
    public abstract bool ElementAccessByString();
    public abstract string GetComponentName(int elem);
    public abstract int GetComponentIndex(string elem);
    public int NComponents { get; } // -1 = use size query
}
