public interface ITraitsSimple<T> {
    DataTypeEnum DataType { get; }
    void SetEqual(ref T lhs, T rhs);
    T Zero { get; }
    bool HasInfinity { get; }
    T PositiveInfinity { get; }
}
