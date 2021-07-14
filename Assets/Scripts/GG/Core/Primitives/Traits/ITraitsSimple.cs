public interface ITraitsSimple<L> {
    DataTypeEnum DataType { get; }
    bool TestEquals(L lhs, L rhs);
    void SetEqual(ref L lhs, L rhs);
    L Zero { get; }
    bool HasInfinity { get; }
    L PositiveInfinity { get; }
}
