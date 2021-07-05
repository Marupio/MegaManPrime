using System;
using System.Collections.Generic;
using UnityEngine;

// TODO - Verify that all DataEnum types are in the Traits and TraitsSimple classes

public static class ComponentNames {
    public static readonly Dictionary<string, int> Vector2Names = new Dictionary<string, int>{
        {"X", 0}, {"x", 0}, {"Y", 1}, {"y", 1}
    };
    public static readonly Dictionary<string, int> Vector3Names = new Dictionary<string, int>{
        {"X", 0}, {"x", 0}, {"Y", 1}, {"y", 1}, {"Z", 2}, {"z", 2}
    };
    public static readonly Dictionary<string, int> Vector4Names = new Dictionary<string, int>{
        {"X", 0}, {"x", 0}, {"Y", 1}, {"y", 1}, {"Z", 2}, {"z", 2}, {"W", 3}, {"w", 3}
    };
}

// Say we know components are floats, but we don't know if its a float, vector2, vector3, quat... what do?
public interface ITraitsSimple<T> {
    public DataTypeEnum DataType { get; }
    public T Zero { get; }
    public bool HasInfinite { get; }
    public T PositiveInfinite { get; }
}

public class TraitsSimpleNone : ITraitsSimple<object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public object Zero { get=>null; }
    public bool HasInfinite { get=>false; }
    public object PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleTrigger : ITraitsSimple<Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public Trigger Zero { get=>new Trigger(); }
    public bool HasInfinite { get=>false; }
    public Trigger PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleBool : ITraitsSimple<bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public bool Zero { get=>false; }
    public bool HasInfinite { get=>false; }
    public bool PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleChar : ITraitsSimple<char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public char Zero { get=>'\0'; }
    public bool HasInfinite { get=>false; }
    public char PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleInt : ITraitsSimple<int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public int Zero { get=>0; }
    public bool HasInfinite { get=>false; }
    public int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleFloat : ITraitsSimple<float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public float Zero { get=>0f; }
    public bool HasInfinite { get=>true; }
    public float PositiveInfinite { get=>float.PositiveInfinity; }
}
public class TraitsSimpleVector2Int : ITraitsSimple<Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public Vector2Int Zero { get=>Vector2Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector2Int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleVector2 : ITraitsSimple<Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public Vector2 Zero { get=>Vector2.zero; }
    public bool HasInfinite { get=>true; }
    public Vector2 PositiveInfinite { get=>Vector2.positiveInfinity; }
}
public class TraitsSimpleVector3Int : ITraitsSimple<Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public Vector3Int Zero { get=>Vector3Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector3Int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleVector3 : ITraitsSimple<Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public Vector3 Zero { get=>Vector3.zero; }
    public bool HasInfinite { get=>true; }
    public Vector3 PositiveInfinite { get=>Vector3.positiveInfinity; }
}
public class TraitsSimpleVector4 : ITraitsSimple<Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public Vector4 Zero { get=>Vector4.zero; }
    public bool HasInfinite { get=>true; }
    public Vector4 PositiveInfinite { get=>Vector4.positiveInfinity; }
}
public class TraitsSimpleQuaternion : ITraitsSimple<Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public Quaternion Zero { get=>Quaternion.identity; }
    public bool HasInfinite { get=>false; }
    public Quaternion PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}


// L = main type, C = component type
// e.g. L=Vector2, C = float, T = TraitsFloat
public interface ITraits<L,C> {
    public DataTypeEnum DataType { get; }
    public DataTypeEnum ComponentType { get; }
    public L Zero(int nElems=1);
    public bool HasInfinite { get; }
    public L PositiveInfinite(int nElems=1);
    public bool ElementAccessByIndex { get; }
    public bool ElementAccessByString { get; }
    public C GetComponent(L data, int elem);
    public C GetComponent(L data, string elem);
    public void SetComponent(ref L data, int elem, C value);
    public void SetComponent(ref L data, string elem, C value);
}

public class TraitsNone : ITraits<object, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public object Zero(int nElems=1) { return null; }
    public bool HasInfinite { get=>false; }
    public object PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(object data, int elem) { throw new System.InvalidOperationException(); }
    public object GetComponent(object data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref object data, int elem, object value) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref object data, string elem, object value) { throw new System.InvalidOperationException(); }
}
public class TraitsTrigger : ITraits<Trigger, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public Trigger Zero(int nElems=1) { return new Trigger(); }
    public bool HasInfinite { get=>false; }
    public Trigger PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(Trigger data, int elem) { throw new System.InvalidOperationException(); }
    public object GetComponent(Trigger data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref Trigger data, int elem, object value) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref Trigger data, string elem, object value) { throw new System.InvalidOperationException(); }
}
public class TraitsBool : ITraits<bool, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool Zero(int nElems=1) { return false; }
    public bool HasInfinite { get=>false; }
    public bool PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(bool data, int elem) { throw new System.InvalidOperationException(); }
    public object GetComponent(bool data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref bool data, int elem, object value) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref bool data, string elem, object value) { throw new System.InvalidOperationException(); }
}
public class TraitsChar : ITraits<char, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public char Zero(int nElems=1) { return '\0'; }
    public bool HasInfinite { get=>false; }
    public char PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(char data, int elem) { throw new System.InvalidOperationException(); }
    public object GetComponent(char data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref char data, int elem, object value) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref char data, string elem, object value) { throw new System.InvalidOperationException(); }
}
public class TraitsString : ITraits<string, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public string Zero(int nElems=1) { return ""; }
    public bool HasInfinite { get=>false; }
    public string PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public char GetComponent(string data, int elem) { return data[elem]; }
    public char GetComponent(string data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref string data, int elem, char value) {
        data = data.Substring(0,elem-1) + value + data.Substring(elem+1, data.Length-elem-1);
    }
    public void SetComponent(ref string data, string elem, char value) { /* do nothing */ }
}
public class TraitsInt : ITraits<int, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public int Zero(int nElems=1) { return 0; }
    public bool HasInfinite { get=>false; }
    public int PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(int data, int elem) { throw new System.InvalidOperationException(); }
    public object GetComponent(int data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref int data, int elem, object value) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref int data, string elem, object value) { throw new System.InvalidOperationException(); }
}
public class TraitsFloat : ITraits<float, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public float Zero(int nElems=1) { return 0f; }
    public bool HasInfinite { get=>true; }
    public float PositiveInfinite(int nElems=1) { return float.PositiveInfinity; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(float data, int elem) { throw new System.InvalidOperationException(); }
    public object GetComponent(float data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref float data, int elem, object value) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref float data, string elem, object value) { throw new System.InvalidOperationException(); }
}
public class TraitsVector2Int : ITraits<Vector2Int, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public Vector2Int Zero(int nElems=1) { return Vector2Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector2Int PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(Vector2Int data, int elem) { return data[elem]; }
    public int GetComponent(Vector2Int data, string elem) { return data[ComponentNames.Vector2Names[elem]]; }
    public void SetComponent(ref Vector2Int data, int elem, int value) {
        switch (elem) {
            case 0:
                data.x = value;
                break;
            case 1:
                data.y = value;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("elem");
        }
    }
    public void SetComponent(ref Vector2Int data, string elem, int value) { SetComponent(ref data, ComponentNames.Vector2Names[elem], value); }
}
public class TraitsVector2 : ITraits<Vector2, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public Vector2 Zero(int nElems=1) { return Vector2.zero; }
    public bool HasInfinite { get=>true; }
    public Vector2 PositiveInfinite(int nElems=1) { return Vector2.positiveInfinity; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Vector2 data, int elem) { return data[elem]; }
    public float GetComponent(Vector2 data, string elem) { return data[ComponentNames.Vector2Names[elem]]; }
    public void SetComponent(ref Vector2 data, int elem, float value) {
        switch (elem) {
            case 0:
                data.x = value;
                break;
            case 1:
                data.y = value;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("elem");
        }
    }
    public void SetComponent(ref Vector2 data, string elem, float value) { SetComponent(ref data, ComponentNames.Vector2Names[elem], value); }
}
public class TraitsVector3Int : ITraits<Vector3Int, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public Vector3Int Zero(int nElems=1) { return Vector3Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector3Int PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(Vector3Int data, int elem) { return data[elem]; }
    public int GetComponent(Vector3Int data, string elem) { return data[ComponentNames.Vector3Names[elem]]; }
    public void SetComponent(ref Vector3Int data, int elem, int value) {
        switch (elem) {
            case 0:
                data.x = value;
                break;
            case 1:
                data.y = value;
                break;
            case 2:
                data.z = value;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("elem");
        }
    }
    public void SetComponent(ref Vector3Int data, string elem, int value) { SetComponent(ref data, ComponentNames.Vector3Names[elem], value); }
}
public class TraitsVector3 : ITraits<Vector3, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public Vector3 Zero(int nElems=1) { return Vector3.zero; }
    public bool HasInfinite { get; }
    public Vector3 PositiveInfinite(int nElems=1) { return Vector3.positiveInfinity; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Vector3 data, int elem) { return data[elem]; }
    public float GetComponent(Vector3 data, string elem) { return data[ComponentNames.Vector3Names[elem]]; }
    public void SetComponent(ref Vector3 data, int elem, float value) {
        switch (elem) {
            case 0:
                data.x = value;
                break;
            case 1:
                data.y = value;
                break;
            case 2:
                data.z = value;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("elem");
        }
    }
    public void SetComponent(ref Vector3 data, string elem, float value) { SetComponent(ref data, ComponentNames.Vector3Names[elem], value); }
}
public class TraitsVector4 : ITraits<Vector4, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public Vector4 Zero(int nElems=1) { return Vector4.zero; }
    public bool HasInfinite { get=>true; }
    public Vector4 PositiveInfinite(int nElems=1) { return Vector4.positiveInfinity; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Vector4 data, int elem) { return data[elem]; }
    public float GetComponent(Vector4 data, string elem) { return data[ComponentNames.Vector4Names[elem]]; }
    public void SetComponent(ref Vector4 data, int elem, float value) {
        switch (elem) {
            case 0:
                data.x = value;
                break;
            case 1:
                data.y = value;
                break;
            case 2:
                data.z = value;
                break;
            case 3:
                data.w = value;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("elem");
        }
    }
    public void SetComponent(ref Vector4 data, string elem, float value) { SetComponent(ref data, ComponentNames.Vector4Names[elem], value); }
}
public class TraitsQuaternion : ITraits<Quaternion, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public Quaternion Zero(int nElems=1) { return Quaternion.identity; }
    public bool HasInfinite { get=>false; }
    public Quaternion PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Quaternion data, int elem) { return data[elem]; }
    public float GetComponent(Quaternion data, string elem) { return data[ComponentNames.Vector4Names[elem]]; }
    public void SetComponent(ref Quaternion data, int elem, float value) {
        switch (elem) {
            case 0:
                data.x = value;
                break;
            case 1:
                data.y = value;
                break;
            case 2:
                data.z = value;
                break;
            case 3:
                data.w = value;
                break;
            default:
                throw new System.ArgumentOutOfRangeException("elem");
        }
    }
    public void SetComponent(ref Quaternion data, string elem, float value) { SetComponent(ref data, ComponentNames.Vector4Names[elem], value); }
}
public class TraitsListTrigger : ITraits<List<Trigger>, Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Trigger; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public List<Trigger> Zero(int nElems=1) { return new List<Trigger>(nElems); }
    public bool HasInfinite { get=>false; }
    public List<Trigger> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Trigger GetComponent(List<Trigger> data, int elem) { return data[elem]; }
    public Trigger GetComponent(List<Trigger> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Trigger> data, int elem, Trigger value) { data[elem] = value; }
    public void SetComponent(ref List<Trigger> data, string elem, Trigger value) { throw new System.InvalidOperationException(); }
}
public class TraitsListBool : ITraits<List<bool>, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public List<bool> Zero(int nElems=1) {
        List<bool> zeroList = new List<bool>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = false;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<bool> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public bool GetComponent(List<bool> data, int elem) { return data[elem]; }
    public bool GetComponent(List<bool> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<bool> data, int elem, bool value) { data[elem] = value; }
    public void SetComponent(ref List<bool> data, string elem, bool value) { throw new System.InvalidOperationException(); }
}
public class TraitsListChar : ITraits<List<char>, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public List<char> Zero(int nElems=1) { return new List<char>(nElems); }
    public bool HasInfinite { get=>false; }
    public List<char> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public char GetComponent(List<char> data, int elem) { return data[elem]; }
    public char GetComponent(List<char> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<char> data, int elem, char value) { data[elem] = value; }
    public void SetComponent(ref List<char> data, string elem, char value) { throw new System.InvalidOperationException(); }
}
public class TraitsListString : ITraits<List<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public List<string> Zero(int nElems=1) {
        List<string> zeroList = new List<string>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = "";
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<string> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public string GetComponent(List<string> data, int elem) { return data[elem]; }
    public string GetComponent(List<string> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<string> data, int elem, string value) { data[elem] = value; }
    public void SetComponent(ref List<string> data, string elem, string value) { throw new System.InvalidOperationException(); }
}
public class TraitsListInt : ITraits<List<int>, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public List<int> Zero(int nElems=1) {
        List<int> zeroList = new List<int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = 0;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public int GetComponent(List<int> data, int elem) { return data[elem]; }
    public int GetComponent(List<int> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<int> data, int elem, int value) { data[elem] = value; }
    public void SetComponent(ref List<int> data, string elem, int value) { throw new System.InvalidOperationException(); }
}
public class TraitsListFloat : ITraits<List<float>, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
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
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public float GetComponent(List<float> data, int elem) { return data[elem]; }
    public float GetComponent(List<float> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<float> data, int elem, float value) { data[elem] = value; }
    public void SetComponent(ref List<float> data, string elem, float value) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector2Int : ITraits<List<Vector2Int>, Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public List<Vector2Int> Zero(int nElems=1) {
        List<Vector2Int> zeroList = new List<Vector2Int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector2Int.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Vector2Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector2Int GetComponent(List<Vector2Int> data, int elem) { return data[elem]; }
    public Vector2Int GetComponent(List<Vector2Int> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Vector2Int> data, int elem, Vector2Int value) { data[elem] = value; }
    public void SetComponent(ref List<Vector2Int> data, string elem, Vector2Int value) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector2 : ITraits<List<Vector2>, Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
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
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector2 GetComponent(List<Vector2> data, int elem) { return data[elem]; }
    public Vector2 GetComponent(List<Vector2> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Vector2> data, int elem, Vector2 value) { data[elem] = value; }
    public void SetComponent(ref List<Vector2> data, string elem, Vector2 value) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector3Int : ITraits<List<Vector3Int>, Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public List<Vector3Int> Zero(int nElems=1) {
        List<Vector3Int> zeroList = new List<Vector3Int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector3Int.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Vector3Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector3Int GetComponent(List<Vector3Int> data, int elem) { return data[elem]; }
    public Vector3Int GetComponent(List<Vector3Int> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Vector3Int> data, int elem, Vector3Int value) { data[elem] = value; }
    public void SetComponent(ref List<Vector3Int> data, string elem, Vector3Int value) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector3 : ITraits<List<Vector3>, Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
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
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector3 GetComponent(List<Vector3> data, int elem) { return data[elem]; }
    public Vector3 GetComponent(List<Vector3> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Vector3> data, int elem, Vector3 value) { data[elem] = value; }
    public void SetComponent(ref List<Vector3> data, string elem, Vector3 value) { throw new System.InvalidOperationException(); }
}
public class TraitsListVector4 : ITraits<List<Vector4>, Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
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
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector4 GetComponent(List<Vector4> data, int elem) { return data[elem]; }
    public Vector4 GetComponent(List<Vector4> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Vector4> data, int elem, Vector4 value) { data[elem] = value; }
    public void SetComponent(ref List<Vector4> data, string elem, Vector4 value) { throw new System.InvalidOperationException(); }
}
public class TraitsListQuaternion : ITraits<List<Quaternion>, Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public List<Quaternion> Zero(int nElems=1) {
        List<Quaternion> zeroList = new List<Quaternion>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Quaternion.identity;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Quaternion> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Quaternion GetComponent(List<Quaternion> data, int elem) { return data[elem]; }
    public Quaternion GetComponent(List<Quaternion> data, string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(ref List<Quaternion> data, int elem, Quaternion value) { data[elem] = value; }
    public void SetComponent(ref List<Quaternion> data, string elem, Quaternion value) { throw new System.InvalidOperationException(); }
}
public class TraitsKVariablesTrigger : ITraits<KVariables<Trigger>, Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Trigger; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public KVariables<Trigger> Zero(int nElems=1) { return new KVariables<Trigger>(); }
    public bool HasInfinite { get=>false; }
    public KVariables<Trigger> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Trigger GetComponent(KVariables<Trigger> data, int elem) { return data[elem]; }
    public Trigger GetComponent(KVariables<Trigger> data, string elem) {
        Trigger value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Trigger> data, int elem, Trigger value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Trigger> data, string elem, Trigger value) { data.Set(elem, value); }
}
public class TraitsKVariablesBool : ITraits<KVariables<bool>, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public KVariables<bool> Zero(int nElems=1) { return new KVariables<bool>(false); }
    public bool HasInfinite { get=>false; }
    public KVariables<bool> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public bool GetComponent(KVariables<bool> data, int elem) { return data[elem]; }
    public bool GetComponent(KVariables<bool> data, string elem) {
        bool value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<bool> data, int elem, bool value) { data[elem] = value; }
    public void SetComponent(ref KVariables<bool> data, string elem, bool value) { data.Set(elem, value); }
}
public class TraitsKVariablesChar : ITraits<KVariables<char>, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public KVariables<char> Zero(int nElems=1) { return new KVariables<char>(''); }
    public bool HasInfinite { get=>false; }
    public KVariables<char> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public char GetComponent(KVariables<char> data, int elem) { return data[elem]; }
    public char GetComponent(KVariables<char> data, string elem) {
        char value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<char> data, int elem, char value) { data[elem] = value; }
    public void SetComponent(ref KVariables<char> data, string elem, char value) { data.Set(elem, value); }
}
public class TraitsKVariablesString : ITraits<KVariables<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public KVariables<string> Zero(int nElems=1) { return new KVariables<string>(""); }
    public bool HasInfinite { get=>false; }
    public KVariables<string> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public string GetComponent(KVariables<string> data, int elem) { return data[elem]; }
    public string GetComponent(KVariables<string> data, string elem) {
        string value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<string> data, int elem, string value) { data[elem] = value; }
    public void SetComponent(ref KVariables<string> data, string elem, string value) { data.Set(elem, value); }
}
public class TraitsKVariablesInt : ITraits<KVariables<int>, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public KVariables<int> Zero(int nElems=1) { return new KVariables<int>(0); }
    public bool HasInfinite { get=>false; }
    public KVariables<int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(KVariables<int> data, int elem) { return data[elem]; }
    public int GetComponent(KVariables<int> data, string elem) {
        int value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<int> data, int elem, int value) { data[elem] = value; }
    public void SetComponent(ref KVariables<int> data, string elem, int value) { data.Set(elem, value); }
}
public class TraitsKVariablesFloat : ITraits<KVariables<float>, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public KVariables<float> Zero(int nElems=1) { return new KVariables<float>(0f); }
    public bool HasInfinite { get=>true; }
    public KVariables<float> PositiveInfinite(int nElems=1) { return new KVariables<float>(float.PositiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(KVariables<float> data, int elem) { return data[elem]; }
    public float GetComponent(KVariables<float> data, string elem) {
        float value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<float> data, int elem, float value) { data[elem] = value; }
    public void SetComponent(ref KVariables<float> data, string elem, float value) { data.Set(elem, value); }
}
public class TraitsKVariablesVector2Int : ITraits<KVariables<Vector2Int>, Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public KVariables<Vector2Int> Zero(int nElems=1) { return new KVariables<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariables<Vector2Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2Int GetComponent(KVariables<Vector2Int> data, int elem) { return data[elem]; }
    public Vector2Int GetComponent(KVariables<Vector2Int> data, string elem) {
        Vector2Int value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Vector2Int> data, int elem, Vector2Int value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Vector2Int> data, string elem, Vector2Int value) { data.Set(elem, value); }
}
public class TraitsKVariablesVector2 : ITraits<KVariables<Vector2>, Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public KVariables<Vector2> Zero(int nElems=1) { return new KVariables<Vector2>(Vector2.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector2> PositiveInfinite(int nElems=1) { return new KVariables<Vector2>(Vector2.positiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2 GetComponent(KVariables<Vector2> data, int elem) { return data[elem]; }
    public Vector2 GetComponent(KVariables<Vector2> data, string elem) {
        Vector2 value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Vector2> data, int elem, Vector2 value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Vector2> data, string elem, Vector2 value) { data.Set(elem, value); }
}
public class TraitsKVariablesVector3Int : ITraits<KVariables<Vector3Int>, Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public KVariables<Vector3Int> Zero(int nElems=1) { return new KVariables<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariables<Vector3Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3Int GetComponent(KVariables<Vector3Int> data, int elem) { return data[elem]; }
    public Vector3Int GetComponent(KVariables<Vector3Int> data, string elem) {
        Vector3Int value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Vector3Int> data, int elem, Vector3Int value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Vector3Int> data, string elem, Vector3Int value) { data.Set(elem, value); }
}
public class TraitsKVariablesVector3 : ITraits<KVariables<Vector3>, Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public KVariables<Vector3> Zero(int nElems=1) { return new KVariables<Vector3>(Vector3.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector3> PositiveInfinite(int nElems=1) { return new KVariables<Vector3>(Vector3.positiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3 GetComponent(KVariables<Vector3> data, int elem) { return data[elem]; }
    public Vector3 GetComponent(KVariables<Vector3> data, string elem) {
        Vector3 value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Vector3> data, int elem, Vector3 value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Vector3> data, string elem, Vector3 value) { data.Set(elem, value); }
}
public class TraitsKVariablesVector4 : ITraits<KVariables<Vector4>, Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public KVariables<Vector4> Zero(int nElems=1) { return new KVariables<Vector4>(Vector4.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector4> PositiveInfinite(int nElems=1) { return new KVariables<Vector4>(Vector4.positiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector4 GetComponent(KVariables<Vector4> data, int elem) { return data[elem]; }
    public Vector4 GetComponent(KVariables<Vector4> data, string elem) {
        Vector4 value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Vector4> data, int elem, Vector4 value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Vector4> data, string elem, Vector4 value) { data.Set(elem, value); }
}
public class TraitsKVariablesQuaternion : ITraits<KVariables<Quaternion>, Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public KVariables<Quaternion> Zero(int nElems=1) { return new KVariables<Quaternion>(Quaternion.identity); }
    public bool HasInfinite { get=>false; }
    public KVariables<Quaternion> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Quaternion GetComponent(KVariables<Quaternion> data, int elem) { return data[elem]; }
    public Quaternion GetComponent(KVariables<Quaternion> data, string elem) {
        Quaternion value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariables<Quaternion> data, int elem, Quaternion value) { data[elem] = value; }
    public void SetComponent(ref KVariables<Quaternion> data, string elem, Quaternion value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtTrigger : ITraits<KVariablesExt<Trigger>, Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Trigger; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public KVariablesExt<Trigger> Zero(int nElems=1) { return new KVariablesExt<Trigger>(); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Trigger> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Trigger GetComponent(KVariablesExt<Trigger> data, int elem) { return data[elem]; }
    public Trigger GetComponent(KVariablesExt<Trigger> data, string elem) {
        Trigger value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Trigger> data, int elem, Trigger value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Trigger> data, string elem, Trigger value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtBool : ITraits<KVariablesExt<bool>, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public KVariablesExt<bool> Zero(int nElems=1) { return new KVariablesExt<bool>(false); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<bool> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public bool GetComponent(KVariablesExt<bool> data, int elem) { return data[elem]; }
    public bool GetComponent(KVariablesExt<bool> data, string elem) {
        bool value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<bool> data, int elem, bool value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<bool> data, string elem, bool value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtString : ITraits<KVariablesExt<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public KVariablesExt<string> Zero(int nElems=1) { return new KVariablesExt<string>(""); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<string> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public string GetComponent(KVariablesExt<string> data, int elem) { return data[elem]; }
    public string GetComponent(KVariablesExt<string> data, string elem) {
        string value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<string> data, int elem, string value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<string> data, string elem, string value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtInt : ITraits<KVariablesExt<int>, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public KVariablesExt<int> Zero(int nElems=1) { return new KVariablesExt<int>(0); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(KVariablesExt<int> data, int elem) { return data[elem]; }
    public int GetComponent(KVariablesExt<int> data, string elem) {
        int value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<int> data, int elem, int value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<int> data, string elem, int value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtFloat : ITraits<KVariablesExt<float>, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public KVariablesExt<float> Zero(int nElems=1) { return new KVariablesExt<float>(0f); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<float> PositiveInfinite(int nElems=1) { return new KVariablesExt<float>(float.PositiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(KVariablesExt<float> data, int elem) { return data[elem]; }
    public float GetComponent(KVariablesExt<float> data, string elem) {
        float value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<float> data, int elem, float value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<float> data, string elem, float value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtVector2Int : ITraits<KVariablesExt<Vector2Int>, Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public KVariablesExt<Vector2Int> Zero(int nElems=1) { return new KVariablesExt<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Vector2Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2Int GetComponent(KVariablesExt<Vector2Int> data, int elem) { return data[elem]; }
    public Vector2Int GetComponent(KVariablesExt<Vector2Int> data, string elem) {
        Vector2Int value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Vector2Int> data, int elem, Vector2Int value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Vector2Int> data, string elem, Vector2Int value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtVector2 : ITraits<KVariablesExt<Vector2>, Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public KVariablesExt<Vector2> Zero(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector2> PositiveInfinite(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.positiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2 GetComponent(KVariablesExt<Vector2> data, int elem) { return data[elem]; }
    public Vector2 GetComponent(KVariablesExt<Vector2> data, string elem) {
        Vector2 value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Vector2> data, int elem, Vector2 value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Vector2> data, string elem, Vector2 value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtVector3Int : ITraits<KVariablesExt<Vector3Int>, Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public KVariablesExt<Vector3Int> Zero(int nElems=1) { return new KVariablesExt<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Vector3Int> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3Int GetComponent(KVariablesExt<Vector3Int> data, int elem) { return data[elem]; }
    public Vector3Int GetComponent(KVariablesExt<Vector3Int> data, string elem) {
        Vector3Int value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Vector3Int> data, int elem, Vector3Int value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Vector3Int> data, string elem, Vector3Int value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtVector3 : ITraits<KVariablesExt<Vector3>, Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public KVariablesExt<Vector3> Zero(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector3> PositiveInfinite(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.positiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3 GetComponent(KVariablesExt<Vector3> data, int elem) { return data[elem]; }
    public Vector3 GetComponent(KVariablesExt<Vector3> data, string elem) {
        Vector3 value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Vector3> data, int elem, Vector3 value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Vector3> data, string elem, Vector3 value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtVector4 : ITraits<KVariablesExt<Vector4>, Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public KVariablesExt<Vector4> Zero(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector4> PositiveInfinite(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.positiveInfinity); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector4 GetComponent(KVariablesExt<Vector4> data, int elem) { return data[elem]; }
    public Vector4 GetComponent(KVariablesExt<Vector4> data, string elem) {
        Vector4 value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Vector4> data, int elem, Vector4 value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Vector4> data, string elem, Vector4 value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtQuaternion : ITraits<KVariablesExt<Quaternion>, Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public KVariablesExt<Quaternion> Zero(int nElems=1) { return new KVariablesExt<Quaternion>(Quaternion.identity); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Quaternion> PositiveInfinite(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Quaternion GetComponent(KVariablesExt<Quaternion> data, int elem) { return data[elem]; }
    public Quaternion GetComponent(KVariablesExt<Quaternion> data, string elem) {
        Quaternion value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<Quaternion> data, int elem, Quaternion value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<Quaternion> data, string elem, Quaternion value) { data.Set(elem, value); }
}
