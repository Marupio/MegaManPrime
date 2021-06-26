using UnityEngine;

public interface ITraits<T> {
    public T Zero { get; }
}

public class TraitsInt : ITraits<int> {
    public int Zero { get=>0; }
}
public class TraitsFloat : ITraits<float> {
    public float Zero { get=>0f; }
}
public class TraitsVector2 : ITraits<Vector2> {
    public Vector2 Zero { get=>Vector2.zero; }
}
public class TraitsVector2Int : ITraits<Vector2Int> {
    public Vector2Int Zero { get=>Vector2Int.zero; }
}
public class TraitsVector3 : ITraits<Vector3> {
    public Vector3 Zero { get=>Vector3.zero; }
}
public class TraitsVector3Int : ITraits<Vector3Int> {
    public Vector3Int Zero { get=>Vector3Int.zero; }
}
public class TraitsVector4 : ITraits<Vector4> {
    public Vector4 Zero { get=>Vector4.zero; }
}
