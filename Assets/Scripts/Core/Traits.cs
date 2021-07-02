using System.Collections.Generic;
using UnityEngine;

public enum DataTypeEnum {
    Bool,
    String,
    Int,
    Float,
    Vector2IntType, // ..Type appended to avoid name collision
    Vector2Type,    // ..Type appended to avoid name collision
    Vector3IntType, // ..Type appended to avoid name collision
    Vector3Type,    // ..Type appended to avoid name collision
    Vector4Type,    // ..Type appended to avoid name collision
    QuaternionType, // ..Type appended to avoid name collision
    List_Bool,
    List_String,
    List_Int,
    List_Float,
    List_Vector2Int,
    List_Vector2,
    List_Vector3Int,
    List_Vector3,
    List_Vector4,
    List_Quaternion,
    KVariables_Bool,
    KVariables_String,
    KVariables_Int,
    KVariables_Float,
    KVariables_Vector2Int,
    KVariables_Vector2,
    KVariables_Vector3Int,
    KVariables_Vector3,
    KVariables_Vector4,
    KVariables_Quaternion,
    KVariablesExt_Bool,
    KVariablesExt_String,
    KVariablesExt_Int,
    KVariablesExt_Float,
    KVariablesExt_Vector2Int,
    KVariablesExt_Vector2,
    KVariablesExt_Vector3Int,
    KVariablesExt_Vector3,
    KVariablesExt_Vector4,
    KVariablesExt_Quaternion
}

public interface ITraits<T> {
    public DataTypeEnum DataType { get; }
    public T Zero(int nElems=1);
    public bool HasInfinite { get; }
    public T PositiveInfinite(int nElems=1);
}

public class TraitsBool : ITraits<bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public bool Zero(int nElems=1) { return false; }
    public bool HasInfinite { get=>false; }
    public bool PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsString : ITraits<string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public string Zero(int nElems=1) { return ""; }
    public bool HasInfinite { get=>false; }
    public string PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsInt : ITraits<int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public int Zero(int nElems=1) { return 0; }
    public bool HasInfinite { get=>false; }
    public int PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsFloat : ITraits<float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public float Zero(int nElems=1) { return 0f; }
    public bool HasInfinite { get=>true; }
    public float PositiveInfinite(int nElems=1) { return float.PositiveInfinity; }
}
public class TraitsVector2Int : ITraits<Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public Vector2Int Zero(int nElems=1) { return Vector2Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector2Int PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsVector2 : ITraits<Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public Vector2 Zero(int nElems=1) { return Vector2.zero; }
    public bool HasInfinite { get=>true; }
    public Vector2 PositiveInfinite(int nElems=1) { return Vector2.positiveInfinity; }
}
public class TraitsVector3Int : ITraits<Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public Vector3Int Zero(int nElems=1) { return Vector3Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector3Int PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsVector3 : ITraits<Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public Vector3 Zero(int nElems=1) { return Vector3.zero; }
    public bool HasInfinite { get; }
    public Vector3 PositiveInfinite(int nElems=1) { return Vector3.positiveInfinity; }
}
public class TraitsVector4 : ITraits<Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public Vector4 Zero(int nElems=1) { return Vector4.zero; }
    public bool HasInfinite { get=>true; }
    public Vector4 PositiveInfinite(int nElems=1) { return Vector4.positiveInfinity; }
}
public class TraitsQuaternion : ITraits<Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public Quaternion Zero(int nElems=1) { return Quaternion.identity; }
    public bool HasInfinite { get=>false; }
    public Quaternion PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsListBool : ITraits<List<bool>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public List<bool> Zero(int nElems=1) {
        List<bool> zeroList = new List<bool>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = false;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<bool> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsListString : ITraits<List<string>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public List<string> Zero(int nElems=1) {
        List<string> zeroList = new List<string>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = "";
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<string> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsListInt : ITraits<List<int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public List<int> Zero(int nElems=1) {
        List<int> zeroList = new List<int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = 0;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsListFloat : ITraits<List<float>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public List<float> Zero(int nElems=1) {
        List<float> zeroList = new List<float>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = 0f;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<float> PositiveInfinite(int nElems=1) {
        List<float> infList = new List<float>(nElems);
        for (int i = 0; i < nElems; ++i) {
            infList[i] = float.PositiveInfinity;
        }
        return infList;
    }
}
public class TraitsListVector2Int : ITraits<List<Vector2Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public List<Vector2Int> Zero(int nElems=1) {
        List<Vector2Int> zeroList = new List<Vector2Int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector2Int.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Vector2Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector2 : ITraits<List<Vector2>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public List<Vector2> Zero(int nElems=1) {
        List<Vector2> zeroList = new List<Vector2>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector2.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<Vector2> PositiveInfinite(int nElems=1) {
        List<Vector2> infList = new List<Vector2>(nElems);
        for (int i = 0; i < nElems; ++i) {
            infList[i] = Vector2.positiveInfinity;
        }
        return infList;
    }
}
public class TraitsListVector3Int : ITraits<List<Vector3Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public List<Vector3Int> Zero(int nElems=1) {
        List<Vector3Int> zeroList = new List<Vector3Int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector3Int.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Vector3Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector3 : ITraits<List<Vector3>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public List<Vector3> Zero(int nElems=1) {
        List<Vector3> zeroList = new List<Vector3>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector4.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<Vector3> PositiveInfinite(int nElems=1) {
        List<Vector3> infList = new List<Vector3>(nElems);
        for (int i = 0; i < nElems; ++i) {
            infList[i] = Vector3.positiveInfinity;
        }
        return infList;
    }
}
public class TraitsListVector4 : ITraits<List<Vector4>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public List<Vector4> Zero(int nElems=1) {
        List<Vector4> zeroList = new List<Vector4>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector4.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<Vector4> PositiveInfinite(int nElems=1) {
        List<Vector4> infList = new List<Vector4>(nElems);
        for (int i = 0; i < nElems; ++i) {
            infList[i] = Vector4.positiveInfinity;
        }
        return infList;
    }
}
public class TraitsListQuaternion : ITraits<List<Quaternion>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public List<Quaternion> Zero(int nElems=1) {
        List<Quaternion> zeroList = new List<Quaternion>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Quaternion.identity;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Quaternion> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesBool : ITraits<KVariables<bool>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public KVariables<bool> Zero(int nElems=1) { return new KVariables<bool>(false); }
    public bool HasInfinite { get=>false; }
    public KVariables<bool> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesString : ITraits<KVariables<string>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public KVariables<string> Zero(int nElems=1) { return new KVariables<string>(""); }
    public bool HasInfinite { get=>false; }
    public KVariables<string> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesInt : ITraits<KVariables<int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public KVariables<int> Zero(int nElems=1) { return new KVariables<int>(0); }
    public bool HasInfinite { get=>false; }
    public KVariables<int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesFloat : ITraits<KVariables<float>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public KVariables<float> Zero(int nElems=1) { return new KVariables<float>(0f); }
    public bool HasInfinite { get=>true; }
    public KVariables<float> PositiveInfinite(int nElems=1) { return new KVariables<float>(float.PositiveInfinity); }
}
public class TraitsKVariablesVector2Int : ITraits<KVariables<Vector2Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public KVariables<Vector2Int> Zero(int nElems=1) { return new KVariables<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariables<Vector2Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesVector2 : ITraits<KVariables<Vector2>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public KVariables<Vector2> Zero(int nElems=1) { return new KVariables<Vector2>(Vector2.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector2> PositiveInfinite(int nElems=1) { return new KVariables<Vector2>(Vector2.positiveInfinity); }
}
public class TraitsKVariablesVector3Int : ITraits<KVariables<Vector3Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public KVariables<Vector3Int> Zero(int nElems=1) { return new KVariables<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariables<Vector3Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesVector3 : ITraits<KVariables<Vector3>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public KVariables<Vector3> Zero(int nElems=1) { return new KVariables<Vector3>(Vector3.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector3> PositiveInfinite(int nElems=1) { return new KVariables<Vector3>(Vector3.positiveInfinity); }
}
public class TraitsKVariablesVector4 : ITraits<KVariables<Vector4>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector4; }
    public KVariables<Vector4> Zero(int nElems=1) { return new KVariables<Vector4>(Vector4.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector4> PositiveInfinite(int nElems=1) { return new KVariables<Vector4>(Vector4.positiveInfinity); }
}
public class TraitsKVariablesQuaternion : ITraits<KVariables<Quaternion>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public KVariables<Quaternion> Zero(int nElems=1) { return new KVariables<Quaternion>(Quaternion.identity); }
    public bool HasInfinite { get=>false; }
    public KVariables<Quaternion> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesExtBool : ITraits<KVariablesExt<bool>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public KVariablesExt<bool> Zero(int nElems=1) { return new KVariablesExt<bool>(false); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<bool> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesExtString : ITraits<KVariablesExt<string>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public KVariablesExt<string> Zero(int nElems=1) { return new KVariablesExt<string>(""); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<string> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesExtInt : ITraits<KVariablesExt<int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public KVariablesExt<int> Zero(int nElems=1) { return new KVariablesExt<int>(0); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesExtFloat : ITraits<KVariablesExt<float>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public KVariablesExt<float> Zero(int nElems=1) { return new KVariablesExt<float>(0f); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<float> PositiveInfinite(int nElems=1) { return new KVariablesExt<float>(float.PositiveInfinity); }
}
public class TraitsKVariablesExtVector2Int : ITraits<KVariablesExt<Vector2Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public KVariablesExt<Vector2Int> Zero(int nElems=1) { return new KVariablesExt<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Vector2Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesExtVector2 : ITraits<KVariablesExt<Vector2>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public KVariablesExt<Vector2> Zero(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector2> PositiveInfinite(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.positiveInfinity); }
}
public class TraitsKVariablesExtVector3Int : ITraits<KVariablesExt<Vector3Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public KVariablesExt<Vector3Int> Zero(int nElems=1) { return new KVariablesExt<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Vector3Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesExtVector3 : ITraits<KVariablesExt<Vector3>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public KVariablesExt<Vector3> Zero(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector3> PositiveInfinite(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.positiveInfinity); }
}
public class TraitsKVariablesExtVector4 : ITraits<KVariablesExt<Vector4>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector4; }
    public KVariablesExt<Vector4> Zero(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector4> PositiveInfinite(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.positiveInfinity); }
}
public class TraitsKVariablesExtQuaternion : ITraits<KVariablesExt<Quaternion>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public KVariablesExt<Quaternion> Zero(int nElems=1) { return new KVariablesExt<Quaternion>(Quaternion.identity); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Quaternion> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
}
