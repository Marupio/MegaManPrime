public interface IDataSetObject<L, C> : IDataSetObjectHeader, IDataObject<L> {
    public ITraits<L, C> Traits { get; }
    public C this[int elem] { get; set; }
    public C GetComponent(int elem);
    public C GetComponent(string elem);
    public void SetComponent(int elem, C value);
    public void SetComponent(string elem, C value);
}
