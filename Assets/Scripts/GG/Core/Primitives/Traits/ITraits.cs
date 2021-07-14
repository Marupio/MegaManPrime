using System.Collections.Generic;

public static class ComponentNames {
    public static readonly Dictionary<string, int> Vector2NameToIndex = new Dictionary<string, int>{
        {"X", 0}, {"x", 0}, {"Y", 1}, {"y", 1}
    };
    public static readonly Dictionary<string, int> Vector3NameToIndex = new Dictionary<string, int>{
        {"X", 0}, {"x", 0}, {"Y", 1}, {"y", 1}, {"Z", 2}, {"z", 2}
    };
    public static readonly Dictionary<string, int> Vector4NameToIndex = new Dictionary<string, int>{
        {"X", 0}, {"x", 0}, {"Y", 1}, {"y", 1}, {"Z", 2}, {"z", 2}, {"W", 3}, {"w", 3}
    };
    public static readonly Dictionary<int, string> Vector2IndexToName = new Dictionary<int, string>{
        {0, "X"}, {1, "Y"}
    };
    public static readonly Dictionary<int, string> Vector3IndexToName = new Dictionary<int, string>{
        {0, "X"}, {1, "Y"}, {2, "Z"}
    };
    public static readonly Dictionary<int, string> Vector4IndexToName = new Dictionary<int, string>{
        {0, "X"}, {1, "Y"}, {2, "Z"}, {3, "W"}
    };
}

// L = main type, C = component type
// e.g. L=Vector2, C = float, T = TraitsFloat
public interface ITraits<L,C> : ITraitsSimple<L> {
    public DataTypeEnum ComponentType { get; }
    bool TestEqualsComponent(C lhs, C rhs);
    public L Zeroes(int nElems=1);
    public bool ElementAccessByIndex { get; }
    public bool ElementAccessByString { get; }
    public L PositiveInfinities(int nElems=1);
    public C GetComponent(L data, int elem);
    public C GetComponent(L data, string elem);
    public void SetComponent(ref L data, int elem, C value);
    public void SetComponent(ref L data, string elem, C value);
}
