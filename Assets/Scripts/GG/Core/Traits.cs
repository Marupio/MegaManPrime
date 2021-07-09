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
    DataTypeEnum DataType { get; }
    void SetEqual(ref T target, T source);
    T Zero { get; }
    bool HasInfinite { get; }
    T PositiveInfinite { get; }
}

public class TraitsSimpleNone : ITraitsSimple<object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public void SetEqual(ref object target, object source) { /* Do nothing */ }
    public object Zero { get=>null; }
    public bool HasInfinite { get=>false; }
    public object PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleTrigger : ITraitsSimple<Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public void SetEqual(ref Trigger target, Trigger source) { target = source; }
    public Trigger Zero { get=>new Trigger(); }
    public bool HasInfinite { get=>false; }
    public Trigger PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleBool : ITraitsSimple<bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public void SetEqual(ref bool target, bool source) { target = source; }
    public bool Zero { get=>false; }
    public bool HasInfinite { get=>false; }
    public bool PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleChar : ITraitsSimple<char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public void SetEqual(ref char target, char source) { target = source; }
    public char Zero { get=>'\0'; }
    public bool HasInfinite { get=>false; }
    public char PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleString : ITraitsSimple<string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public void SetEqual(ref string target, string source) { target = source; }
    public string Zero { get=>""; }
    public bool HasInfinite { get=>false; }
    public string PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleInt : ITraitsSimple<int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public void SetEqual(ref int target, int source) { target = source; }
    public int Zero { get=>0; }
    public bool HasInfinite { get=>false; }
    public int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleFloat : ITraitsSimple<float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public void SetEqual(ref float target, float source) { target = source; }
    public float Zero { get=>0f; }
    public bool HasInfinite { get=>true; }
    public float PositiveInfinite { get=>float.PositiveInfinity; }
}
public class TraitsSimpleVector2Int : ITraitsSimple<Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public void SetEqual(ref Vector2Int target, Vector2Int source) { target = source; }
    public Vector2Int Zero { get=>Vector2Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector2Int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleVector2 : ITraitsSimple<Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public void SetEqual(ref Vector2 target, Vector2 source) { target = source; }
    public Vector2 Zero { get=>Vector2.zero; }
    public bool HasInfinite { get=>true; }
    public Vector2 PositiveInfinite { get=>Vector2.positiveInfinity; }
}
public class TraitsSimpleVector3Int : ITraitsSimple<Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public void SetEqual(ref Vector3Int target, Vector3Int source) { target = source; }
    public Vector3Int Zero { get=>Vector3Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector3Int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}
public class TraitsSimpleVector3 : ITraitsSimple<Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public void SetEqual(ref Vector3 target, Vector3 source) { target = source; }
    public Vector3 Zero { get=>Vector3.zero; }
    public bool HasInfinite { get=>true; }
    public Vector3 PositiveInfinite { get=>Vector3.positiveInfinity; }
}
public class TraitsSimpleVector4 : ITraitsSimple<Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public void SetEqual(ref Vector4 target, Vector4 source) { target = source; }
    public Vector4 Zero { get=>Vector4.zero; }
    public bool HasInfinite { get=>true; }
    public Vector4 PositiveInfinite { get=>Vector4.positiveInfinity; }
}
public class TraitsSimpleQuaternion : ITraitsSimple<Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public void SetEqual(ref Quaternion target, Quaternion source) { target = source; }
    public Quaternion Zero { get=>Quaternion.identity; }
    public bool HasInfinite { get=>false; }
    public Quaternion PositiveInfinite { get { throw new System.InvalidOperationException(); } }
}


// L = main type, C = component type
// e.g. L=Vector2, C = float, T = TraitsFloat
public interface ITraits<L,C> : ITraitsSimple<L> {
    public DataTypeEnum ComponentType { get; }
    public L Zeroes(int nElems=1);
    public bool ElementAccessByIndex { get; }
    public bool ElementAccessByString { get; }
    public L PositiveInfinites(int nElems=1);
    public C GetComponent(L data, int elem);
    public C GetComponent(L data, string elem);
    public void SetComponent(ref L data, int elem, C value);
    public void SetComponent(ref L data, string elem, C value);
}

public class TraitsNone : ITraits<object, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public void SetEqual(ref object target, object source) { /* do nothing */ }
    public object Zero { get=>null; }
    public object Zeroes(int nElems=1) { return null; }
    public bool HasInfinite { get=>false; }
    public object PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public object PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref Trigger target, Trigger source) { target = source; }
    public Trigger Zero { get=>new Trigger(); }
    public Trigger Zeroes(int nElems=1) { return new Trigger(); }
    public bool HasInfinite { get=>false; }
    public Trigger PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public Trigger PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref bool target, bool source) { target = source; }
    public bool Zero { get=>false; }
    public bool Zeroes(int nElems=1) { return false; }
    public bool HasInfinite { get=>false; }
    public bool PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public bool PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref char target, char source) { target = source; }
    public char Zero { get=>'\0'; }
    public char Zeroes(int nElems=1) { return '\0'; }
    public bool HasInfinite { get=>false; }
    public char PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public char PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref string target, string source) { target = source; }
    public string Zero { get=>""; }
    public string Zeroes(int nElems=1) { return ""; }
    public bool HasInfinite { get=>false; }
    public string PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public string PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref int target, int source) { target = source; }
    public int Zero { get=>0; }
    public int Zeroes(int nElems=1) { return 0; }
    public bool HasInfinite { get=>false; }
    public int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public int PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref float target, float source) { target = source; }
    public float Zero { get=>0f; }
    public float Zeroes(int nElems=1) { return 0f; }
    public bool HasInfinite { get=>true; }
    public float PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public float PositiveInfinites(int nElems=1) { return float.PositiveInfinity; }
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
    public void SetEqual(ref Vector2Int target, Vector2Int source) { target = source; }
    public Vector2Int Zero { get=>Vector2Int.zero; }
    public Vector2Int Zeroes(int nElems=1) { return Vector2Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector2Int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public Vector2Int PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref Vector2 target, Vector2 source) { target = source; }
    public Vector2 Zero { get=>Vector2.zero; }
    public Vector2 Zeroes(int nElems=1) { return Vector2.zero; }
    public bool HasInfinite { get=>true; }
    public Vector2 PositiveInfinite { get=>Vector2.positiveInfinity; }
    public Vector2 PositiveInfinites(int nElems=1) { return Vector2.positiveInfinity; }
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
    public void SetEqual(ref Vector3Int target, Vector3Int source) { target = source; }
    public Vector3Int Zero { get=>Vector3Int.zero; }
    public Vector3Int Zeroes(int nElems=1) { return Vector3Int.zero; }
    public bool HasInfinite { get=>false; }
    public Vector3Int PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public Vector3Int PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref Vector3 target, Vector3 source) { target = source; }
    public Vector3 Zero { get=>Vector3.zero; }
    public Vector3 Zeroes(int nElems=1) { return Vector3.zero; }
    public bool HasInfinite { get; }
    public Vector3 PositiveInfinite { get=>Vector3.positiveInfinity; }
    public Vector3 PositiveInfinites(int nElems=1) { return Vector3.positiveInfinity; }
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
    public void SetEqual(ref Vector4 target, Vector4 source) { target = source; }
    public Vector4 Zero { get=>Vector4.zero; }
    public Vector4 Zeroes(int nElems=1) { return Vector4.zero; }
    public bool HasInfinite { get=>true; }
    public Vector4 PositiveInfinite { get=>Vector4.positiveInfinity; }
    public Vector4 PositiveInfinites(int nElems=1) { return Vector4.positiveInfinity; }
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
    public void SetEqual(ref Quaternion target, Quaternion source) { target = source; }
    public Quaternion Zero { get=>Quaternion.identity; }
    public Quaternion Zeroes(int nElems=1) { return Quaternion.identity; }
    public bool HasInfinite { get=>false; }
    public Quaternion PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public Quaternion PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<Trigger> target, List<Trigger> source) { target = source; }
    public List<Trigger> Zero { get=>new List<Trigger>(); }
    public List<Trigger> Zeroes(int nElems=1) { return new List<Trigger>(nElems); }
    public bool HasInfinite { get=>false; }
    public List<Trigger> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<Trigger> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<bool> target, List<bool> source) { target = source; }
    public List<bool> Zero { get=>new List<bool>(); }
    public List<bool> Zeroes(int nElems=1) {
        List<bool> zeroList = new List<bool>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = false;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<bool> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<bool> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<char> target, List<char> source) { target = source; }
    public List<char> Zero { get=>new List<char>(); }
    public List<char> Zeroes(int nElems=1) { return new List<char>(nElems); }
    public bool HasInfinite { get=>false; }
    public List<char> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<char> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<string> target, List<string> source) { target = source; }
    public List<string> Zero { get=>new List<string>(); }
    public List<string> Zeroes(int nElems=1) {
        List<string> zeroList = new List<string>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = "";
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<string> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<string> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<int> target, List<int> source) { target = source; }
    public List<int> Zero { get=>new List<int>(); }
    public List<int> Zeroes(int nElems=1) {
        List<int> zeroList = new List<int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = 0;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<float> target, List<float> source) { target = source; }
    public List<float> Zero { get=>new List<float>(); }
    public List<float> Zeroes(int nElems=1) {
        List<float> zeroList = new List<float>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = 0f;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<float> PositiveInfinite {
        get {
            List<float> lst = new List<float>();
            lst.Add(float.PositiveInfinity);
            return lst;
        }
    }
    public List<float> PositiveInfinites(int nElems=1) {
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
    public void SetEqual(ref List<Vector2Int> target, List<Vector2Int> source) { target = source; }
    public List<Vector2Int> Zero { get=>new List<Vector2Int>(); }
    public List<Vector2Int> Zeroes(int nElems=1) {
        List<Vector2Int> zeroList = new List<Vector2Int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector2Int.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Vector2Int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<Vector2Int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<Vector2> target, List<Vector2> source) { target = source; }
    public List<Vector2> Zero { get=>new List<Vector2>(); }
    public List<Vector2> Zeroes(int nElems=1) {
        List<Vector2> zeroList = new List<Vector2>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector2.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<Vector2> PositiveInfinite {
        get {
            List<Vector2> lst = new List<Vector2>();
            lst.Add(Vector2.positiveInfinity);
            return lst;
        }
    }
    public List<Vector2> PositiveInfinites(int nElems=1) {
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
    public void SetEqual(ref List<Vector3Int> target, List<Vector3Int> source) { target = source; }
    public List<Vector3Int> Zero { get=>new List<Vector3Int>(); }
    public List<Vector3Int> Zeroes(int nElems=1) {
        List<Vector3Int> zeroList = new List<Vector3Int>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector3Int.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Vector3Int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<Vector3Int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref List<Vector3> target, List<Vector3> source) { target = source; }
    public List<Vector3> Zero { get=>new List<Vector3>(); }
    public List<Vector3> Zeroes(int nElems=1) {
        List<Vector3> zeroList = new List<Vector3>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector4.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<Vector3> PositiveInfinite {
        get {
            List<Vector3> lst = new List<Vector3>();
            lst.Add(Vector3.positiveInfinity);
            return lst;
        }
    }
    public List<Vector3> PositiveInfinites(int nElems=1) {
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
    public void SetEqual(ref List<Vector4> target, List<Vector4> source) { target = source; }
    public List<Vector4> Zero { get=>new List<Vector4>(); }
    public List<Vector4> Zeroes(int nElems=1) {
        List<Vector4> zeroList = new List<Vector4>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Vector4.zero;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>true; }
    public List<Vector4> PositiveInfinite {
        get {
            List<Vector4> lst = new List<Vector4>();
            lst.Add(Vector4.positiveInfinity);
            return lst;
        }
    }
    public List<Vector4> PositiveInfinites(int nElems=1) {
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
    public void SetEqual(ref List<Quaternion> target, List<Quaternion> source) { target = source; }
    public List<Quaternion> Zero { get=>new List<Quaternion>(); }
    public List<Quaternion> Zeroes(int nElems=1) {
        List<Quaternion> zeroList = new List<Quaternion>(nElems);
        for (int i = 0; i < nElems; ++i) {
            zeroList[i] = Quaternion.identity;
        }
        return zeroList;
    }
    public bool HasInfinite { get=>false; }
    public List<Quaternion> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public List<Quaternion> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<Trigger> target, KVariables<Trigger> source) { target = source; }
    public KVariables<Trigger> Zero { get=>new KVariables<Trigger>(); }
    public KVariables<Trigger> Zeroes(int nElems=1) { return new KVariables<Trigger>(); }
    public bool HasInfinite { get=>false; }
    public KVariables<Trigger> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<Trigger> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<bool> target, KVariables<bool> source) { target = source; }
    public KVariables<bool> Zero { get=>new KVariables<bool>(); }
    public KVariables<bool> Zeroes(int nElems=1) { return new KVariables<bool>(false); }
    public bool HasInfinite { get=>false; }
    public KVariables<bool> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<bool> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<char> target, KVariables<char> source) { target = source; }
    public KVariables<char> Zero { get=>new KVariables<char>(); }
    public KVariables<char> Zeroes(int nElems=1) { return new KVariables<char>('\0'); }
    public bool HasInfinite { get=>false; }
    public KVariables<char> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<char> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<string> target, KVariables<string> source) { target = source; }
    public KVariables<string> Zero { get=>new KVariables<string>(); }
    public KVariables<string> Zeroes(int nElems=1) { return new KVariables<string>(""); }
    public bool HasInfinite { get=>false; }
    public KVariables<string> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<string> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<int> target, KVariables<int> source) { target = source; }
    public KVariables<int> Zero { get=>new KVariables<int>(); }
    public KVariables<int> Zeroes(int nElems=1) { return new KVariables<int>(0); }
    public bool HasInfinite { get=>false; }
    public KVariables<int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<float> target, KVariables<float> source) { target = source; }
    public KVariables<float> Zero { get=>new KVariables<float>(); }
    public KVariables<float> Zeroes(int nElems=1) { return new KVariables<float>(0f); }
    public bool HasInfinite { get=>true; }
    public KVariables<float> PositiveInfinite { get=>new KVariables<float>(float.PositiveInfinity); }
    public KVariables<float> PositiveInfinites(int nElems=1) { return new KVariables<float>(float.PositiveInfinity); }
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
    public void SetEqual(ref KVariables<Vector2Int> target, KVariables<Vector2Int> source) { target = source; }
    public KVariables<Vector2Int> Zero { get=>new KVariables<Vector2Int>(); }
    public KVariables<Vector2Int> Zeroes(int nElems=1) { return new KVariables<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariables<Vector2Int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<Vector2Int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<Vector2> target, KVariables<Vector2> source) { target = source; }
    public KVariables<Vector2> Zero { get=>new KVariables<Vector2>(); }
    public KVariables<Vector2> Zeroes(int nElems=1) { return new KVariables<Vector2>(Vector2.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector2> PositiveInfinite { get=>new KVariables<Vector2>(Vector2.positiveInfinity); }
    public KVariables<Vector2> PositiveInfinites(int nElems=1) { return new KVariables<Vector2>(Vector2.positiveInfinity); }
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
    public void SetEqual(ref KVariables<Vector3Int> target, KVariables<Vector3Int> source) { target = source; }
    public KVariables<Vector3Int> Zero { get=>new KVariables<Vector3Int>(); }
    public KVariables<Vector3Int> Zeroes(int nElems=1) { return new KVariables<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariables<Vector3Int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<Vector3Int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariables<Vector3> target, KVariables<Vector3> source) { target = source; }
    public KVariables<Vector3> Zero { get=>new KVariables<Vector3>(); }
    public KVariables<Vector3> Zeroes(int nElems=1) { return new KVariables<Vector3>(Vector3.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector3> PositiveInfinite { get=>new KVariables<Vector3>(Vector3.positiveInfinity); }
    public KVariables<Vector3> PositiveInfinites(int nElems=1) { return new KVariables<Vector3>(Vector3.positiveInfinity); }
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
    public void SetEqual(ref KVariables<Vector4> target, KVariables<Vector4> source) { target = source; }
    public KVariables<Vector4> Zero { get=>new KVariables<Vector4>(); }
    public KVariables<Vector4> Zeroes(int nElems=1) { return new KVariables<Vector4>(Vector4.zero); }
    public bool HasInfinite { get=>true; }
    public KVariables<Vector4> PositiveInfinite { get=>new KVariables<Vector4>(Vector4.positiveInfinity); }
    public KVariables<Vector4> PositiveInfinites(int nElems=1) { return new KVariables<Vector4>(Vector4.positiveInfinity); }
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
    public void SetEqual(ref KVariables<Quaternion> target, KVariables<Quaternion> source) { target = source; }
    public KVariables<Quaternion> Zero { get=>new KVariables<Quaternion>(); }
    public KVariables<Quaternion> Zeroes(int nElems=1) { return new KVariables<Quaternion>(Quaternion.identity); }
    public bool HasInfinite { get=>false; }
    public KVariables<Quaternion> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariables<Quaternion> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariablesExt<Trigger> target, KVariablesExt<Trigger> source) { target = source; }
    public KVariablesExt<Trigger> Zero { get=>new KVariablesExt<Trigger>(); }
    public KVariablesExt<Trigger> Zeroes(int nElems=1) { return new KVariablesExt<Trigger>(); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Trigger> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<Trigger> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariablesExt<bool> target, KVariablesExt<bool> source) { target = source; }
    public KVariablesExt<bool> Zero { get=>new KVariablesExt<bool>(); }
    public KVariablesExt<bool> Zeroes(int nElems=1) { return new KVariablesExt<bool>(false); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<bool> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<bool> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
public class TraitsKVariablesExtChar : ITraits<KVariablesExt<char>, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public void SetEqual(ref KVariablesExt<char> target, KVariablesExt<char> source) { target = source; }
    public KVariablesExt<char> Zero { get=>new KVariablesExt<char>(); }
    public KVariablesExt<char> Zeroes(int nElems=1) { return new KVariablesExt<char>('\0'); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<char> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<char> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public char GetComponent(KVariablesExt<char> data, int elem) { return data[elem]; }
    public char GetComponent(KVariablesExt<char> data, string elem) {
        char value;
        data.Get(elem, out value);
        return value;
    }
    public void SetComponent(ref KVariablesExt<char> data, int elem, char value) { data[elem] = value; }
    public void SetComponent(ref KVariablesExt<char> data, string elem, char value) { data.Set(elem, value); }
}
public class TraitsKVariablesExtString : ITraits<KVariablesExt<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public void SetEqual(ref KVariablesExt<string> target, KVariablesExt<string> source) { target = source; }
    public KVariablesExt<string> Zero { get=>new KVariablesExt<string>(""); }
    public KVariablesExt<string> Zeroes(int nElems=1) { return new KVariablesExt<string>(""); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<string> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<string> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariablesExt<int> target, KVariablesExt<int> source) { target = source; }
    public KVariablesExt<int> Zero { get=>new KVariablesExt<int>(); }
    public KVariablesExt<int> Zeroes(int nElems=1) { return new KVariablesExt<int>(0); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariablesExt<float> target, KVariablesExt<float> source) { target = source; }
    public KVariablesExt<float> Zero { get=>new KVariablesExt<float>(); }
    public KVariablesExt<float> Zeroes(int nElems=1) { return new KVariablesExt<float>(0f); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<float> PositiveInfinite { get=>new KVariablesExt<float>(float.PositiveInfinity); }
    public KVariablesExt<float> PositiveInfinites(int nElems=1) { return new KVariablesExt<float>(float.PositiveInfinity); }
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
    public void SetEqual(ref KVariablesExt<Vector2Int> target, KVariablesExt<Vector2Int> source) { target = source; }
    public KVariablesExt<Vector2Int> Zero { get=>new KVariablesExt<Vector2Int>(); }
    public KVariablesExt<Vector2Int> Zeroes(int nElems=1) { return new KVariablesExt<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Vector2Int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<Vector2Int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariablesExt<Vector2> target, KVariablesExt<Vector2> source) { target = source; }
    public KVariablesExt<Vector2> Zero { get=>new KVariablesExt<Vector2>(); }
    public KVariablesExt<Vector2> PositiveInfinite { get=>new KVariablesExt<Vector2>(Vector2.positiveInfinity); }
    public KVariablesExt<Vector2> Zeroes(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector2> PositiveInfinites(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.positiveInfinity); }
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
    public void SetEqual(ref KVariablesExt<Vector3Int> target, KVariablesExt<Vector3Int> source) { target = source; }
    public KVariablesExt<Vector3Int> Zero { get=>new KVariablesExt<Vector3Int>(); }
    public KVariablesExt<Vector3Int> Zeroes(int nElems=1) { return new KVariablesExt<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Vector3Int> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<Vector3Int> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
    public void SetEqual(ref KVariablesExt<Vector3> target, KVariablesExt<Vector3> source) { target = source; }
    public KVariablesExt<Vector3> Zero { get=>new KVariablesExt<Vector3>(); }
    public KVariablesExt<Vector3> PositiveInfinite { get=>new KVariablesExt<Vector3>(Vector3.positiveInfinity); }
    public KVariablesExt<Vector3> Zeroes(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector3> PositiveInfinites(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.positiveInfinity); }
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
    public void SetEqual(ref KVariablesExt<Vector4> target, KVariablesExt<Vector4> source) { target = source; }
    public KVariablesExt<Vector4> Zero { get=>new KVariablesExt<Vector4>(); }
    public KVariablesExt<Vector4> Zeroes(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.zero); }
    public bool HasInfinite { get=>true; }
    public KVariablesExt<Vector4> PositiveInfinite { get=>new KVariablesExt<Vector4>(Vector2.positiveInfinity); }
    public KVariablesExt<Vector4> PositiveInfinites(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.positiveInfinity); }
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
    public void SetEqual(ref KVariablesExt<Quaternion> target, KVariablesExt<Quaternion> source) { target = source; }
    public KVariablesExt<Quaternion> Zero { get=>new KVariablesExt<Quaternion>(); }
    public KVariablesExt<Quaternion> Zeroes(int nElems=1) { return new KVariablesExt<Quaternion>(Quaternion.identity); }
    public bool HasInfinite { get=>false; }
    public KVariablesExt<Quaternion> PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariablesExt<Quaternion> PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
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
public class TraitsKVariableTypeSet : ITraits<KVariableTypeSet, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariableTypeSetType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public void SetEqual(ref KVariableTypeSet target, KVariableTypeSet source) { target = source; }
    public KVariableTypeSet Zero { get=>KVariableTypeInfo.None; }
    public KVariableTypeSet Zeroes(int nElems=1) { return KVariableTypeInfo.None; }
    public bool HasInfinite { get=>false; }
    public KVariableTypeSet PositiveInfinite { get { throw new System.InvalidOperationException(); } }
    public KVariableTypeSet PositiveInfinites(int nElems=1) { throw new System.InvalidOperationException(); }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public bool GetComponent(KVariableTypeSet data, int index) {
        KVariableEnum kvType = KVariableTypeInfo.IndexToKVariableEnum(index);
        return data.Contains(kvType);
    }
    public bool GetComponent(KVariableTypeSet data, string elem) {
        return data.Contains(elem);
    }
    public void SetComponent(ref KVariableTypeSet data, int index, bool value) {
        KVariableEnum kvType = KVariableTypeInfo.IndexToKVariableEnum(index);
        if (value) {
            data.Add(kvType);
        } else {
            data.Remove(kvType);
        }
    }
    public void SetComponent(ref KVariableTypeSet data, string elem, bool value) {
        if (value) {
            data.Add(elem);
        } else {
            data.Remove(elem);
        }
    }
}
