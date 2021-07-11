// L = main type, C = component type
// e.g. L=Vector2, C = float, T = TraitsFloat
public interface ITraits<L,C> : ITraitsSimple<L> {
    public DataTypeEnum ComponentType { get; }
    public L Zeroes(int nElems=1);
    public bool ElementAccessByIndex { get; }
    public bool ElementAccessByString { get; }
    public L PositiveInfinities(int nElems=1);
    public C GetComponent(L data, int elem);
    public C GetComponent(L data, string elem);
    public void SetComponent(ref L data, int elem, C value);
    public void SetComponent(ref L data, string elem, C value);
}
