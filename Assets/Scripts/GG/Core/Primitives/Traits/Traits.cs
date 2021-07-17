using System.Collections.Generic;
using UnityEngine;

// THIS IS AN AUTOMATICALLY GENERATED FILE (ACTUALLY THE MANUAL-KIND OF AUTOMATIC)
// DO NOT EDIT

public class TraitsNone : ITraits<object, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool TestEquals(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref object lhs, object rhs) { /* do nothing */ }
    public object Zero { get { return null; } }
    public object Zeroes(int nElems=1) { return null; }
    public bool HasInfinity { get=>false; }
    public object PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public object PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(object data, int index) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public object GetComponent(object data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref object data, int index, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
    public void SetComponent(ref object data, string elem, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsBool : ITraits<bool, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool TestEquals(bool lhs, bool rhs) { EqualityComparer<bool> ec = EqualityComparer<bool>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref bool lhs, bool rhs) { lhs = rhs; }
    public bool Zero { get { return false; } }
    public bool Zeroes(int nElems=1) { return false; }
    public bool HasInfinity { get=>false; }
    public bool PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return false;
    #endif
    } }
    public bool PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return false;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(bool data, int index) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return false;
    #endif
    }
    public object GetComponent(bool data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return false;
    #endif
    }
    public void SetComponent(ref bool data, int index, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
    public void SetComponent(ref bool data, string elem, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsTrigger : ITraits<Trigger, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool TestEquals(Trigger lhs, Trigger rhs) { EqualityComparer<Trigger> ec = EqualityComparer<Trigger>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Trigger lhs, Trigger rhs) { lhs = rhs; }
    public Trigger Zero { get { return Trigger.Default; } }
    public Trigger Zeroes(int nElems=1) { return Trigger.Default; }
    public bool HasInfinity { get=>false; }
    public Trigger PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Trigger.Default;
    #endif
    } }
    public Trigger PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Trigger.Default;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(Trigger data, int index) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public object GetComponent(Trigger data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref Trigger data, int index, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
    public void SetComponent(ref Trigger data, string elem, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsChar : ITraits<char, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool TestEquals(char lhs, char rhs) { EqualityComparer<char> ec = EqualityComparer<char>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref char lhs, char rhs) { lhs = rhs; }
    public char Zero { get { return default(char); } }
    public char Zeroes(int nElems=1) { return default(char); }
    public bool HasInfinity { get=>false; }
    public char PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return default(char);
    #endif
    } }
    public char PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return default(char);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(char data, int index) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public object GetComponent(char data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref char data, int index, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
    public void SetComponent(ref char data, string elem, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsString : ITraits<string, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public bool TestEquals(string lhs, string rhs) { EqualityComparer<string> ec = EqualityComparer<string>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(char lhs, char rhs) { EqualityComparer<char> ec = EqualityComparer<char>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref string lhs, string rhs) { lhs = rhs; }
    public string Zero { get { return default(string); } }
    public string Zeroes(int nElems=1) { return default(string); }
    public bool HasInfinity { get=>false; }
    public string PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return default(string);
    #endif
    } }
    public string PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return default(string);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public char GetComponent(string data, int index) { return data[index]; }
    public char GetComponent(string data, string elem) { return default(char); }
    public void SetComponent(ref string data, int index, char value) { data=data.Substring(0,index-1)+value+data.Substring(index+1,data.Length-index-1); }
    public void SetComponent(ref string data, string elem, char value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsInt : ITraits<int, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool TestEquals(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref int lhs, int rhs) { lhs = rhs; }
    public int Zero { get { return 0; } }
    public int Zeroes(int nElems=1) { return 0; }
    public bool HasInfinity { get=>false; }
    public int PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return 0;
    #endif
    } }
    public int PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return 0;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(int data, int index) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public object GetComponent(int data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref int data, int index, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
    public void SetComponent(ref int data, string elem, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsFloat : ITraits<float, object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public bool TestEquals(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref float lhs, float rhs) { lhs = rhs; }
    public float Zero { get { return 0f; } }
    public float Zeroes(int nElems=1) { return 0f; }
    public bool HasInfinity { get=>true; }
    public float PositiveInfinity { get { return float.PositiveInfinity; } }
    public float PositiveInfinities(int nElems=1) { return float.PositiveInfinity; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.None; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public object GetComponent(float data, int index) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public object GetComponent(float data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref float data, int index, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
    public void SetComponent(ref float data, string elem, object value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    /* do nothing */
    #endif
    }
}
public class TraitsVector2Int : ITraits<Vector2Int, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public bool TestEquals(Vector2Int lhs, Vector2Int rhs) { EqualityComparer<Vector2Int> ec = EqualityComparer<Vector2Int>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector2Int lhs, Vector2Int rhs) { lhs = rhs; }
    public Vector2Int Zero { get { return Vector2Int.zero; } }
    public Vector2Int Zeroes(int nElems=1) { return Vector2Int.zero; }
    public bool HasInfinity { get=>false; }
    public Vector2Int PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Vector2Int.zero;
    #endif
    } }
    public Vector2Int PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Vector2Int.zero;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(Vector2Int data, int index) { return data[index]; }
    public int GetComponent(Vector2Int data, string elem) { return data[ComponentNames.Vector2NameToIndex[elem]]; }
    public void SetComponent(ref Vector2Int data, int index, int value) { data[index] = value; }
    public void SetComponent(ref Vector2Int data, string elem, int value) { data[ComponentNames.Vector2NameToIndex[elem]]=value; }
}
public class TraitsVector2 : ITraits<Vector2, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(Vector2 lhs, Vector2 rhs) { EqualityComparer<Vector2> ec = EqualityComparer<Vector2>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector2 lhs, Vector2 rhs) { lhs = rhs; }
    public Vector2 Zero { get { return Vector2.zero; } }
    public Vector2 Zeroes(int nElems=1) { return Vector2.zero; }
    public bool HasInfinity { get=>true; }
    public Vector2 PositiveInfinity { get { return Vector2.positiveInfinity; } }
    public Vector2 PositiveInfinities(int nElems=1) { return Vector2.positiveInfinity; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Vector2 data, int index) { return data[index]; }
    public float GetComponent(Vector2 data, string elem) { return data[ComponentNames.Vector2NameToIndex[elem]]; }
    public void SetComponent(ref Vector2 data, int index, float value) { data[index]=value; }
    public void SetComponent(ref Vector2 data, string elem, float value) { data[ComponentNames.Vector2NameToIndex[elem]]=value; }
}
public class TraitsVector3Int : ITraits<Vector3Int, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public bool TestEquals(Vector3Int lhs, Vector3Int rhs) { EqualityComparer<Vector3Int> ec = EqualityComparer<Vector3Int>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector3Int lhs, Vector3Int rhs) { lhs = rhs; }
    public Vector3Int Zero { get { return Vector3Int.zero; } }
    public Vector3Int Zeroes(int nElems=1) { return Vector3Int.zero; }
    public bool HasInfinity { get=>false; }
    public Vector3Int PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Vector3Int.zero;
    #endif
    } }
    public Vector3Int PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Vector3Int.zero;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(Vector3Int data, int index) { return data[index]; }
    public int GetComponent(Vector3Int data, string elem) { return data[ComponentNames.Vector3NameToIndex[elem]]; }
    public void SetComponent(ref Vector3Int data, int index, int value) { data[index]=value; }
    public void SetComponent(ref Vector3Int data, string elem, int value) { data[ComponentNames.Vector3NameToIndex[elem]]=value; }
}
public class TraitsVector3 : ITraits<Vector3, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(Vector3 lhs, Vector3 rhs) { EqualityComparer<Vector3> ec = EqualityComparer<Vector3>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector3 lhs, Vector3 rhs) { lhs = rhs; }
    public Vector3 Zero { get { return Vector3.zero; } }
    public Vector3 Zeroes(int nElems=1) { return Vector3.zero; }
    public bool HasInfinity { get=>true; }
    public Vector3 PositiveInfinity { get { return Vector3.positiveInfinity; } }
    public Vector3 PositiveInfinities(int nElems=1) { return Vector3.positiveInfinity; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Vector3 data, int index) { return data[index]; }
    public float GetComponent(Vector3 data, string elem) { return data[ComponentNames.Vector3NameToIndex[elem]]; }
    public void SetComponent(ref Vector3 data, int index, float value) { data[index]=value; }
    public void SetComponent(ref Vector3 data, string elem, float value) { data[ComponentNames.Vector3NameToIndex[elem]]=value; }
}
public class TraitsVector4 : ITraits<Vector4, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(Vector4 lhs, Vector4 rhs) { EqualityComparer<Vector4> ec = EqualityComparer<Vector4>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector4 lhs, Vector4 rhs) { lhs = rhs; }
    public Vector4 Zero { get { return Vector4.zero; } }
    public Vector4 Zeroes(int nElems=1) { return Vector4.zero; }
    public bool HasInfinity { get=>true; }
    public Vector4 PositiveInfinity { get { return Vector4.positiveInfinity; } }
    public Vector4 PositiveInfinities(int nElems=1) { return Vector4.positiveInfinity; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(Vector4 data, int index) { return data[index]; }
    public float GetComponent(Vector4 data, string elem) { return data[ComponentNames.Vector4NameToIndex[elem]]; }
    public void SetComponent(ref Vector4 data, int index, float value) { data[index]=value; }
    public void SetComponent(ref Vector4 data, string elem, float value) { data[ComponentNames.Vector4NameToIndex[elem]]=value; }
}
public class TraitsQuaternion : ITraits<Quaternion, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(Quaternion lhs, Quaternion rhs) { EqualityComparer<Quaternion> ec = EqualityComparer<Quaternion>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Quaternion lhs, Quaternion rhs) { lhs = rhs; }
    public Quaternion Zero { get { return Quaternion.identity; } }
    public Quaternion Zeroes(int nElems=1) { return Quaternion.identity; }
    public bool HasInfinity { get=>false; }
    public Quaternion PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Quaternion.identity;
    #endif
    } }
    public Quaternion PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return Quaternion.identity;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>false; }
    public bool ElementAccessByString { get=>false; }
    public float GetComponent(Quaternion data, int index) { return data[index]; }
    public float GetComponent(Quaternion data, string elem) { return data[ComponentNames.Vector4NameToIndex[elem]]; }
    public void SetComponent(ref Quaternion data, int index, float value) { data[index]=value; }
    public void SetComponent(ref Quaternion data, string elem, float value) { data[ComponentNames.Vector4NameToIndex[elem]]=value; }
}
public class TraitsTriggerList : ITraits<List<Trigger>, Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Trigger; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public bool TestEquals(List<Trigger> lhs, List<Trigger> rhs) { EqualityComparer<List<Trigger>> ec = EqualityComparer<List<Trigger>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Trigger lhs, Trigger rhs) { EqualityComparer<Trigger> ec = EqualityComparer<Trigger>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Trigger> lhs, List<Trigger> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Trigger> Zero { get { return null; } }
    public List<Trigger> Zeroes(int nElems=1) { List<Trigger> lst = new List<Trigger>(nElems); for(int i=0; i<nElems;++i){lst.Add(new Trigger());} return lst; }
    public bool HasInfinity { get=>false; }
    public List<Trigger> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<Trigger> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Trigger GetComponent(List<Trigger> data, int index) { return data[index]; }
    public Trigger GetComponent(List<Trigger> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Trigger> data, int index, Trigger value) { data[index]=value; }
    public void SetComponent(ref List<Trigger> data, string elem, Trigger value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsBoolList : ITraits<List<bool>, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public bool TestEquals(List<bool> lhs, List<bool> rhs) { EqualityComparer<List<bool>> ec = EqualityComparer<List<bool>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(bool lhs, bool rhs) { EqualityComparer<bool> ec = EqualityComparer<bool>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<bool> lhs, List<bool> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<bool> Zero { get { return null; } }
    public List<bool> Zeroes(int nElems=1) { List<bool> lst = new List<bool>(nElems); for(int i=0; i<nElems;++i){lst.Add(false);} return lst; }
    public bool HasInfinity { get=>false; }
    public List<bool> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null
    #endif
    } }
    public List<bool> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public bool GetComponent(List<bool> data, int index) { return data[index]; }
    public bool GetComponent(List<bool> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<bool> data, int index, bool value) { data[index]=value; }
    public void SetComponent(ref List<bool> data, string elem, bool value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsCharList : ITraits<List<char>, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public bool TestEquals(List<char> lhs, List<char> rhs) { EqualityComparer<List<char>> ec = EqualityComparer<List<char>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(char lhs, char rhs) { EqualityComparer<char> ec = EqualityComparer<char>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<char> lhs, List<char> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<char> Zero { get { return null; } }
    public List<char> Zeroes(int nElems=1) { List<char> lst = new List<char>(nElems); for(int i=0; i<nElems;++i){lst.Add(default(char));} return lst; }
    public bool HasInfinity { get=>false; }
    public List<char> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<char> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public char GetComponent(List<char> data, int index) { return data[index]; }
    public char GetComponent(List<char> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<char> data, int index, char value) { data[index]=value; }
    public void SetComponent(ref List<char> data, string elem, char value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsStringList : ITraits<List<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public bool TestEquals(List<string> lhs, List<string> rhs) { EqualityComparer<List<string>> ec = EqualityComparer<List<string>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(string lhs, string rhs) { EqualityComparer<string> ec = EqualityComparer<string>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<string> lhs, List<string> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<string> Zero { get { return null; } }
    public List<string> Zeroes(int nElems=1) { List<string> lst = new List<string>(nElems); for(int i=0; i<nElems;++i){lst.Add(default(string));} return lst; }
    public bool HasInfinity { get=>false; }
    public List<string> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<string> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public string GetComponent(List<string> data, int index) { return data[index]; }
    public string GetComponent(List<string> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<string> data, int index, string value) { data[index]=value; }
    public void SetComponent(ref List<string> data, string elem, string value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsIntList : ITraits<List<int>, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public bool TestEquals(List<int> lhs, List<int> rhs) { EqualityComparer<List<int>> ec = EqualityComparer<List<int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<int> lhs, List<int> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<int> Zero { get { return null; } }
    public List<int> Zeroes(int nElems=1) { List<int> lst = new List<int>(nElems); for(int i=0; i<nElems;++i){lst.Add(0);} return lst; }
    public bool HasInfinity { get=>false; }
    public List<int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public int GetComponent(List<int> data, int index) { return data[index]; }
    public int GetComponent(List<int> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<int> data, int index, int value) { data[index]=value; }
    public void SetComponent(ref List<int> data, string elem, int value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsFloatList : ITraits<List<float>, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(List<float> lhs, List<float> rhs) { EqualityComparer<List<float>> ec = EqualityComparer<List<float>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<float> lhs, List<float> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<float> Zero { get { return null; } }
    public List<float> Zeroes(int nElems=1) { List<float> lst = new List<float>(nElems); for(int i=0; i<nElems;++i){lst.Add(0f);} return lst; }
    public bool HasInfinity { get=>true; }
    public List<float> PositiveInfinity { get { return null; } }
    public List<float> PositiveInfinities(int nElems=1) { List<float> lst = new List<float>(nElems); for(int i=0;i<nElems;++i){lst.Add(float.PositiveInfinity);} return lst; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public float GetComponent(List<float> data, int index) { return data[index]; }
    public float GetComponent(List<float> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<float> data, int index, float value) { data[index]=value; }
    public void SetComponent(ref List<float> data, string elem, float value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsVector2IntList : ITraits<List<Vector2Int>, Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public bool TestEquals(List<Vector2Int> lhs, List<Vector2Int> rhs) { EqualityComparer<List<Vector2Int>> ec = EqualityComparer<List<Vector2Int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector2Int lhs, Vector2Int rhs) { EqualityComparer<Vector2Int> ec = EqualityComparer<Vector2Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector2Int> lhs, List<Vector2Int> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Vector2Int> Zero { get { return null; } }
    public List<Vector2Int> Zeroes(int nElems=1) { List<Vector2Int> lst = new List<Vector2Int>(nElems); for(int i=0; i<nElems;++i){lst.Add(Vector2Int.zero);} return lst; }
    public bool HasInfinity { get=>false; }
    public List<Vector2Int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<Vector2Int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector2Int GetComponent(List<Vector2Int> data, int index) { return data[index]; }
    public Vector2Int GetComponent(List<Vector2Int> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Vector2Int> data, int index, Vector2Int value) { data[index]=value; }
    public void SetComponent(ref List<Vector2Int> data, string elem, Vector2Int value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsVector2List : ITraits<List<Vector2>, Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public bool TestEquals(List<Vector2> lhs, List<Vector2> rhs) { EqualityComparer<List<Vector2>> ec = EqualityComparer<List<Vector2>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector2 lhs, Vector2 rhs) { EqualityComparer<Vector2> ec = EqualityComparer<Vector2>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector2> lhs, List<Vector2> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Vector2> Zero { get { return null; } }
    public List<Vector2> Zeroes(int nElems=1) { List<Vector2> lst = new List<Vector2>(nElems); for(int i=0; i<nElems;++i){lst.Add(Vector2.zero);} return lst; }
    public bool HasInfinity { get=>true; }
    public List<Vector2> PositiveInfinity { get { return null; } }
    public List<Vector2> PositiveInfinities(int nElems=1) { List<Vector2> lst = new List<Vector2>(nElems); for(int i=0;i<nElems;++i){lst.Add(Vector2.positiveInfinity);} return lst; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector2 GetComponent(List<Vector2> data, int index) { return data[index]; }
    public Vector2 GetComponent(List<Vector2> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Vector2> data, int index, Vector2 value) { data[index]=value; }
    public void SetComponent(ref List<Vector2> data, string elem, Vector2 value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsVector3IntList : ITraits<List<Vector3Int>, Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public bool TestEquals(List<Vector3Int> lhs, List<Vector3Int> rhs) { EqualityComparer<List<Vector3Int>> ec = EqualityComparer<List<Vector3Int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector3Int lhs, Vector3Int rhs) { EqualityComparer<Vector3Int> ec = EqualityComparer<Vector3Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector3Int> lhs, List<Vector3Int> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Vector3Int> Zero { get { return null; } }
    public List<Vector3Int> Zeroes(int nElems=1) { List<Vector3Int> lst = new List<Vector3Int>(nElems); for(int i=0; i<nElems;++i){lst.Add(Vector3Int.zero);} return lst; }
    public bool HasInfinity { get=>false; }
    public List<Vector3Int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<Vector3Int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector3Int GetComponent(List<Vector3Int> data, int index) { return data[index]; }
    public Vector3Int GetComponent(List<Vector3Int> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Vector3Int> data, int index, Vector3Int value) { data[index]=value; }
    public void SetComponent(ref List<Vector3Int> data, string elem, Vector3Int value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsVector3List : ITraits<List<Vector3>, Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public bool TestEquals(List<Vector3> lhs, List<Vector3> rhs) { EqualityComparer<List<Vector3>> ec = EqualityComparer<List<Vector3>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector3 lhs, Vector3 rhs) { EqualityComparer<Vector3> ec = EqualityComparer<Vector3>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector3> lhs, List<Vector3> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Vector3> Zero { get { return null; } }
    public List<Vector3> Zeroes(int nElems=1) { List<Vector3> lst = new List<Vector3>(nElems); for(int i=0; i<nElems;++i){lst.Add(Vector3.zero);} return lst; }
    public bool HasInfinity { get=>true; }
    public List<Vector3> PositiveInfinity { get { return null; } }
    public List<Vector3> PositiveInfinities(int nElems=1) { List<Vector3> lst = new List<Vector3>(nElems); for(int i=0;i<nElems;++i){lst.Add(Vector3.positiveInfinity);} return lst; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector3 GetComponent(List<Vector3> data, int index) { return data[index]; }
    public Vector3 GetComponent(List<Vector3> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Vector3> data, int index, Vector3 value) { data[index]=value; }
    public void SetComponent(ref List<Vector3> data, string elem, Vector3 value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsVector4List : ITraits<List<Vector4>, Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public bool TestEquals(List<Vector4> lhs, List<Vector4> rhs) { EqualityComparer<List<Vector4>> ec = EqualityComparer<List<Vector4>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector4 lhs, Vector4 rhs) { EqualityComparer<Vector4> ec = EqualityComparer<Vector4>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector4> lhs, List<Vector4> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Vector4> Zero { get { return null; } }
    public List<Vector4> Zeroes(int nElems=1) { List<Vector4> lst = new List<Vector4>(nElems); for(int i=0; i<nElems;++i){lst.Add(Vector4.zero);} return lst; }
    public bool HasInfinity { get=>true; }
    public List<Vector4> PositiveInfinity { get { return null; } }
    public List<Vector4> PositiveInfinities(int nElems=1) { List<Vector4> lst = new List<Vector4>(nElems); for(int i=0;i<nElems;++i){lst.Add(Vector4.positiveInfinity);} return lst; }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Vector4 GetComponent(List<Vector4> data, int index) { return data[index]; }
    public Vector4 GetComponent(List<Vector4> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Vector4> data, int index, Vector4 value) { data[index]=value; }
    public void SetComponent(ref List<Vector4> data, string elem, Vector4 value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsQuaternionList : ITraits<List<Quaternion>, Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Quaternion; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public bool TestEquals(List<Quaternion> lhs, List<Quaternion> rhs) { EqualityComparer<List<Quaternion>> ec = EqualityComparer<List<Quaternion>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Quaternion lhs, Quaternion rhs) { EqualityComparer<Quaternion> ec = EqualityComparer<Quaternion>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Quaternion> lhs, List<Quaternion> rhs) { lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];} }
    public List<Quaternion> Zero { get { return null; } }
    public List<Quaternion> Zeroes(int nElems=1) { List<Quaternion> lst = new List<Quaternion>(nElems); for(int i=0; i<nElems;++i){lst.Add(Quaternion.identity);} return lst; }
    public bool HasInfinity { get=>false; }
    public List<Quaternion> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    } }
    public List<Quaternion> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>false; }
    public Quaternion GetComponent(List<Quaternion> data, int index) { return data[index]; }
    public Quaternion GetComponent(List<Quaternion> data, string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
    public void SetComponent(ref List<Quaternion> data, int index, Quaternion value) { data[index]=value; }
    public void SetComponent(ref List<Quaternion> data, string elem, Quaternion value) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
    }
}
public class TraitsKVariablesTrigger : ITraits<KVariables<Trigger>, Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Trigger; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public bool TestEquals(KVariables<Trigger> lhs, KVariables<Trigger> rhs) { EqualityComparer<KVariables<Trigger>> ec = EqualityComparer<KVariables<Trigger>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Trigger lhs, Trigger rhs) { EqualityComparer<Trigger> ec = EqualityComparer<Trigger>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Trigger> lhs, KVariables<Trigger> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Trigger> Zero { get { return new KVariables<Trigger>(Trigger.Default); } }
    public KVariables<Trigger> Zeroes(int nElems=1) { return new KVariables<Trigger>(new Trigger()); }
    public bool HasInfinity { get=>false; }
    public KVariables<Trigger> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Trigger>(Trigger.Default);
    #endif
    } }
    public KVariables<Trigger> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Trigger>(Trigger.Default);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Trigger GetComponent(KVariables<Trigger> data, int index) { Trigger value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Trigger GetComponent(KVariables<Trigger> data, string elem) { Trigger value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Trigger> data, int index, Trigger value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Trigger> data, string elem, Trigger value) { data.Set(elem,value); }
}
public class TraitsKVariablesBool : ITraits<KVariables<bool>, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public bool TestEquals(KVariables<bool> lhs, KVariables<bool> rhs) { EqualityComparer<KVariables<bool>> ec = EqualityComparer<KVariables<bool>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(bool lhs, bool rhs) { EqualityComparer<bool> ec = EqualityComparer<bool>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<bool> lhs, KVariables<bool> rhs) { lhs.SetEqual(rhs); }
    public KVariables<bool> Zero { get { return new KVariables<bool>(false); } }
    public KVariables<bool> Zeroes(int nElems=1) { return new KVariables<bool>(false); }
    public bool HasInfinity { get=>false; }
    public KVariables<bool> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<bool>(false);
    #endif
    } }
    public KVariables<bool> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<bool>(false);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public bool GetComponent(KVariables<bool> data, int index) { bool value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public bool GetComponent(KVariables<bool> data, string elem) { bool value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<bool> data, int index, bool value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<bool> data, string elem, bool value) { data.Set(elem,value); }
}
public class TraitsKVariablesChar : ITraits<KVariables<char>, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public bool TestEquals(KVariables<char> lhs, KVariables<char> rhs) { EqualityComparer<KVariables<char>> ec = EqualityComparer<KVariables<char>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(char lhs, char rhs) { EqualityComparer<char> ec = EqualityComparer<char>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<char> lhs, KVariables<char> rhs) { lhs.SetEqual(rhs); }
    public KVariables<char> Zero { get { return new KVariables<char>(default(char)); } }
    public KVariables<char> Zeroes(int nElems=1) { return new KVariables<char>(default(char)); }
    public bool HasInfinity { get=>false; }
    public KVariables<char> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<char>(default(char));
    #endif
    } }
    public KVariables<char> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<char>(default(char));
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public char GetComponent(KVariables<char> data, int index) { char value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public char GetComponent(KVariables<char> data, string elem) { char value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<char> data, int index, char value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<char> data, string elem, char value) { data.Set(elem,value); }
}
public class TraitsKVariablesString : ITraits<KVariables<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public bool TestEquals(KVariables<string> lhs, KVariables<string> rhs) { EqualityComparer<KVariables<string>> ec = EqualityComparer<KVariables<string>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(string lhs, string rhs) { EqualityComparer<string> ec = EqualityComparer<string>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<string> lhs, KVariables<string> rhs) { lhs.SetEqual(rhs); }
    public KVariables<string> Zero { get { return new KVariables<string>(default(string)); } }
    public KVariables<string> Zeroes(int nElems=1) { return new KVariables<string>(default(string)); }
    public bool HasInfinity { get=>false; }
    public KVariables<string> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<string>(default(string));
    #endif
    } }
    public KVariables<string> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<string>(default(string));
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public string GetComponent(KVariables<string> data, int index) { string value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public string GetComponent(KVariables<string> data, string elem) { string value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<string> data, int index, string value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<string> data, string elem, string value) { data.Set(elem,value); }
}
public class TraitsKVariablesInt : ITraits<KVariables<int>, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public bool TestEquals(KVariables<int> lhs, KVariables<int> rhs) { EqualityComparer<KVariables<int>> ec = EqualityComparer<KVariables<int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<int> lhs, KVariables<int> rhs) { lhs.SetEqual(rhs); }
    public KVariables<int> Zero { get { return new KVariables<int>(0); } }
    public KVariables<int> Zeroes(int nElems=1) { return new KVariables<int>(0); }
    public bool HasInfinity { get=>false; }
    public KVariables<int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<int>(0);
    #endif
    } }
    public KVariables<int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<int>(0);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(KVariables<int> data, int index) { int value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public int GetComponent(KVariables<int> data, string elem) { int value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<int> data, int index, int value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<int> data, string elem, int value) { data.Set(elem,value); }
}
public class TraitsKVariablesFloat : ITraits<KVariables<float>, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(KVariables<float> lhs, KVariables<float> rhs) { EqualityComparer<KVariables<float>> ec = EqualityComparer<KVariables<float>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<float> lhs, KVariables<float> rhs) { lhs.SetEqual(rhs); }
    public KVariables<float> Zero { get { return new KVariables<float>(0f); } }
    public KVariables<float> Zeroes(int nElems=1) { return new KVariables<float>(0f); }
    public bool HasInfinity { get=>true; }
    public KVariables<float> PositiveInfinity { get { return new KVariables<float>(float.PositiveInfinity); } }
    public KVariables<float> PositiveInfinities(int nElems=1) { return new KVariables<float>(float.PositiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(KVariables<float> data, int index) { float value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public float GetComponent(KVariables<float> data, string elem) { float value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<float> data, int index, float value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<float> data, string elem, float value) { data.Set(elem,value); }
}
public class TraitsKVariablesVector2Int : ITraits<KVariables<Vector2Int>, Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public bool TestEquals(KVariables<Vector2Int> lhs, KVariables<Vector2Int> rhs) { EqualityComparer<KVariables<Vector2Int>> ec = EqualityComparer<KVariables<Vector2Int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector2Int lhs, Vector2Int rhs) { EqualityComparer<Vector2Int> ec = EqualityComparer<Vector2Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector2Int> lhs, KVariables<Vector2Int> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector2Int> Zero { get { return new KVariables<Vector2Int>(Vector2Int.zero); } }
    public KVariables<Vector2Int> Zeroes(int nElems=1) { return new KVariables<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariables<Vector2Int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Vector2Int>(Vector2Int.zero);
    #endif
    } }
    public KVariables<Vector2Int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Vector2Int>(Vector2Int.zero);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2Int GetComponent(KVariables<Vector2Int> data, int index) { Vector2Int value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector2Int GetComponent(KVariables<Vector2Int> data, string elem) { Vector2Int value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Vector2Int> data, int index, Vector2Int value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Vector2Int> data, string elem, Vector2Int value) { data.Set(elem,value); }
}
public class TraitsKVariablesVector2 : ITraits<KVariables<Vector2>, Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public bool TestEquals(KVariables<Vector2> lhs, KVariables<Vector2> rhs) { EqualityComparer<KVariables<Vector2>> ec = EqualityComparer<KVariables<Vector2>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector2 lhs, Vector2 rhs) { EqualityComparer<Vector2> ec = EqualityComparer<Vector2>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector2> lhs, KVariables<Vector2> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector2> Zero { get { return new KVariables<Vector2>(Vector2.zero); } }
    public KVariables<Vector2> Zeroes(int nElems=1) { return new KVariables<Vector2>(Vector2.zero); }
    public bool HasInfinity { get=>true; }
    public KVariables<Vector2> PositiveInfinity { get { return new KVariables<Vector2>(Vector2.positiveInfinity); } }
    public KVariables<Vector2> PositiveInfinities(int nElems=1) { return new KVariables<Vector2>(Vector2.positiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2 GetComponent(KVariables<Vector2> data, int index) { Vector2 value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector2 GetComponent(KVariables<Vector2> data, string elem) { Vector2 value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Vector2> data, int index, Vector2 value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Vector2> data, string elem, Vector2 value) { data.Set(elem,value); }
}
public class TraitsKVariablesVector3Int : ITraits<KVariables<Vector3Int>, Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public bool TestEquals(KVariables<Vector3Int> lhs, KVariables<Vector3Int> rhs) { EqualityComparer<KVariables<Vector3Int>> ec = EqualityComparer<KVariables<Vector3Int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector3Int lhs, Vector3Int rhs) { EqualityComparer<Vector3Int> ec = EqualityComparer<Vector3Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector3Int> lhs, KVariables<Vector3Int> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector3Int> Zero { get { return new KVariables<Vector3Int>(Vector3Int.zero); } }
    public KVariables<Vector3Int> Zeroes(int nElems=1) { return new KVariables<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariables<Vector3Int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Vector3Int>(Vector3Int.zero);
    #endif
    } }
    public KVariables<Vector3Int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Vector3Int>(Vector3Int.zero);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3Int GetComponent(KVariables<Vector3Int> data, int index) { Vector3Int value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector3Int GetComponent(KVariables<Vector3Int> data, string elem) { Vector3Int value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Vector3Int> data, int index, Vector3Int value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Vector3Int> data, string elem, Vector3Int value) { data.Set(elem,value); }
}
public class TraitsKVariablesVector3 : ITraits<KVariables<Vector3>, Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public bool TestEquals(KVariables<Vector3> lhs, KVariables<Vector3> rhs) { EqualityComparer<KVariables<Vector3>> ec = EqualityComparer<KVariables<Vector3>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector3 lhs, Vector3 rhs) { EqualityComparer<Vector3> ec = EqualityComparer<Vector3>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector3> lhs, KVariables<Vector3> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector3> Zero { get { return new KVariables<Vector3>(Vector3.zero); } }
    public KVariables<Vector3> Zeroes(int nElems=1) { return new KVariables<Vector3>(Vector3.zero); }
    public bool HasInfinity { get=>true; }
    public KVariables<Vector3> PositiveInfinity { get { return new KVariables<Vector3>(Vector3.positiveInfinity); } }
    public KVariables<Vector3> PositiveInfinities(int nElems=1) { return new KVariables<Vector3>(Vector3.positiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3 GetComponent(KVariables<Vector3> data, int index) { Vector3 value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector3 GetComponent(KVariables<Vector3> data, string elem) { Vector3 value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Vector3> data, int index, Vector3 value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Vector3> data, string elem, Vector3 value) { data.Set(elem,value); }
}
public class TraitsKVariablesVector4 : ITraits<KVariables<Vector4>, Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public bool TestEquals(KVariables<Vector4> lhs, KVariables<Vector4> rhs) { EqualityComparer<KVariables<Vector4>> ec = EqualityComparer<KVariables<Vector4>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector4 lhs, Vector4 rhs) { EqualityComparer<Vector4> ec = EqualityComparer<Vector4>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector4> lhs, KVariables<Vector4> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector4> Zero { get { return new KVariables<Vector4>(Vector4.zero); } }
    public KVariables<Vector4> Zeroes(int nElems=1) { return new KVariables<Vector4>(Vector4.zero); }
    public bool HasInfinity { get=>true; }
    public KVariables<Vector4> PositiveInfinity { get { return new KVariables<Vector4>(Vector4.positiveInfinity); } }
    public KVariables<Vector4> PositiveInfinities(int nElems=1) { return new KVariables<Vector4>(Vector4.positiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector4 GetComponent(KVariables<Vector4> data, int index) { Vector4 value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector4 GetComponent(KVariables<Vector4> data, string elem) { Vector4 value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Vector4> data, int index, Vector4 value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Vector4> data, string elem, Vector4 value) { data.Set(elem,value); }
}
public class TraitsKVariablesQuaternion : ITraits<KVariables<Quaternion>, Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public bool TestEquals(KVariables<Quaternion> lhs, KVariables<Quaternion> rhs) { EqualityComparer<KVariables<Quaternion>> ec = EqualityComparer<KVariables<Quaternion>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Quaternion lhs, Quaternion rhs) { EqualityComparer<Quaternion> ec = EqualityComparer<Quaternion>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Quaternion> lhs, KVariables<Quaternion> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Quaternion> Zero { get { return new KVariables<Quaternion>(Quaternion.identity); } }
    public KVariables<Quaternion> Zeroes(int nElems=1) { return new KVariables<Quaternion>(Quaternion.identity); }
    public bool HasInfinity { get=>false; }
    public KVariables<Quaternion> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Quaternion>(Quaternion.identity);
    #endif
    } }
    public KVariables<Quaternion> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariables<Quaternion>(Quaternion.identity);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Quaternion GetComponent(KVariables<Quaternion> data, int index) { Quaternion value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Quaternion GetComponent(KVariables<Quaternion> data, string elem) { Quaternion value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariables<Quaternion> data, int index, Quaternion value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariables<Quaternion> data, string elem, Quaternion value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtTrigger : ITraits<KVariablesExt<Trigger>, Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Trigger; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public bool TestEquals(KVariablesExt<Trigger> lhs, KVariablesExt<Trigger> rhs) { EqualityComparer<KVariablesExt<Trigger>> ec = EqualityComparer<KVariablesExt<Trigger>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Trigger lhs, Trigger rhs) { EqualityComparer<Trigger> ec = EqualityComparer<Trigger>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Trigger> lhs, KVariablesExt<Trigger> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Trigger> Zero { get { return new KVariablesExt<Trigger>(Trigger.Default); } }
    public KVariablesExt<Trigger> Zeroes(int nElems=1) { return new KVariablesExt<Trigger>(new Trigger()); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Trigger> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Trigger>(Trigger.Default);
    #endif
    } }
    public KVariablesExt<Trigger> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Trigger>(Trigger.Default);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Trigger GetComponent(KVariablesExt<Trigger> data, int index) { Trigger value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Trigger GetComponent(KVariablesExt<Trigger> data, string elem) { Trigger value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Trigger> data, int index, Trigger value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Trigger> data, string elem, Trigger value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtBool : ITraits<KVariablesExt<bool>, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public bool TestEquals(KVariablesExt<bool> lhs, KVariablesExt<bool> rhs) { EqualityComparer<KVariablesExt<bool>> ec = EqualityComparer<KVariablesExt<bool>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(bool lhs, bool rhs) { EqualityComparer<bool> ec = EqualityComparer<bool>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<bool> lhs, KVariablesExt<bool> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<bool> Zero { get { return new KVariablesExt<bool>(false); } }
    public KVariablesExt<bool> Zeroes(int nElems=1) { return new KVariablesExt<bool>(false); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<bool> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<bool>(false);
    #endif
    } }
    public KVariablesExt<bool> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<bool>(false);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public bool GetComponent(KVariablesExt<bool> data, int index) { bool value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public bool GetComponent(KVariablesExt<bool> data, string elem) { bool value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<bool> data, int index, bool value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<bool> data, string elem, bool value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtChar : ITraits<KVariablesExt<char>, char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Char; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public bool TestEquals(KVariablesExt<char> lhs, KVariablesExt<char> rhs) { EqualityComparer<KVariablesExt<char>> ec = EqualityComparer<KVariablesExt<char>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(char lhs, char rhs) { EqualityComparer<char> ec = EqualityComparer<char>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<char> lhs, KVariablesExt<char> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<char> Zero { get { return new KVariablesExt<char>(default(char)); } }
    public KVariablesExt<char> Zeroes(int nElems=1) { return new KVariablesExt<char>(default(char)); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<char> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<char>(default(char));
    #endif
    } }
    public KVariablesExt<char> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<char>(default(char));
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public char GetComponent(KVariablesExt<char> data, int index) { char value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public char GetComponent(KVariablesExt<char> data, string elem) { char value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<char> data, int index, char value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<char> data, string elem, char value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtString : ITraits<KVariablesExt<string>, string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public bool TestEquals(KVariablesExt<string> lhs, KVariablesExt<string> rhs) { EqualityComparer<KVariablesExt<string>> ec = EqualityComparer<KVariablesExt<string>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(string lhs, string rhs) { EqualityComparer<string> ec = EqualityComparer<string>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<string> lhs, KVariablesExt<string> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<string> Zero { get { return new KVariablesExt<string>(default(string)); } }
    public KVariablesExt<string> Zeroes(int nElems=1) { return new KVariablesExt<string>(default(string)); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<string> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<string>(default(string));
    #endif
    } }
    public KVariablesExt<string> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<string>(default(string));
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public string GetComponent(KVariablesExt<string> data, int index) { string value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public string GetComponent(KVariablesExt<string> data, string elem) { string value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<string> data, int index, string value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<string> data, string elem, string value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtInt : ITraits<KVariablesExt<int>, int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public bool TestEquals(KVariablesExt<int> lhs, KVariablesExt<int> rhs) { EqualityComparer<KVariablesExt<int>> ec = EqualityComparer<KVariablesExt<int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<int> lhs, KVariablesExt<int> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<int> Zero { get { return new KVariablesExt<int>(0); } }
    public KVariablesExt<int> Zeroes(int nElems=1) { return new KVariablesExt<int>(0); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<int>(0);
    #endif
    } }
    public KVariablesExt<int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<int>(0);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public int GetComponent(KVariablesExt<int> data, int index) { int value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public int GetComponent(KVariablesExt<int> data, string elem) { int value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<int> data, int index, int value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<int> data, string elem, int value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtFloat : ITraits<KVariablesExt<float>, float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public bool TestEquals(KVariablesExt<float> lhs, KVariablesExt<float> rhs) { EqualityComparer<KVariablesExt<float>> ec = EqualityComparer<KVariablesExt<float>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<float> lhs, KVariablesExt<float> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<float> Zero { get { return new KVariablesExt<float>(0f); } }
    public KVariablesExt<float> Zeroes(int nElems=1) { return new KVariablesExt<float>(0f); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<float> PositiveInfinity { get { return new KVariablesExt<float>(float.PositiveInfinity); } }
    public KVariablesExt<float> PositiveInfinities(int nElems=1) { return new KVariablesExt<float>(float.PositiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public float GetComponent(KVariablesExt<float> data, int index) { float value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public float GetComponent(KVariablesExt<float> data, string elem) { float value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<float> data, int index, float value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<float> data, string elem, float value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtVector2Int : ITraits<KVariablesExt<Vector2Int>, Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public bool TestEquals(KVariablesExt<Vector2Int> lhs, KVariablesExt<Vector2Int> rhs) { EqualityComparer<KVariablesExt<Vector2Int>> ec = EqualityComparer<KVariablesExt<Vector2Int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector2Int lhs, Vector2Int rhs) { EqualityComparer<Vector2Int> ec = EqualityComparer<Vector2Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector2Int> lhs, KVariablesExt<Vector2Int> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector2Int> Zero { get { return new KVariablesExt<Vector2Int>(Vector2Int.zero); } }
    public KVariablesExt<Vector2Int> Zeroes(int nElems=1) { return new KVariablesExt<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Vector2Int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Vector2Int>(Vector2Int.zero);
    #endif
    } }
    public KVariablesExt<Vector2Int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Vector2Int>(Vector2Int.zero);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2Int GetComponent(KVariablesExt<Vector2Int> data, int index) { Vector2Int value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector2Int GetComponent(KVariablesExt<Vector2Int> data, string elem) { Vector2Int value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Vector2Int> data, int index, Vector2Int value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Vector2Int> data, string elem, Vector2Int value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtVector2 : ITraits<KVariablesExt<Vector2>, Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public bool TestEquals(KVariablesExt<Vector2> lhs, KVariablesExt<Vector2> rhs) { EqualityComparer<KVariablesExt<Vector2>> ec = EqualityComparer<KVariablesExt<Vector2>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector2 lhs, Vector2 rhs) { EqualityComparer<Vector2> ec = EqualityComparer<Vector2>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector2> lhs, KVariablesExt<Vector2> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector2> Zero { get { return new KVariablesExt<Vector2>(Vector2.zero); } }
    public KVariablesExt<Vector2> Zeroes(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.zero); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<Vector2> PositiveInfinity { get { return new KVariablesExt<Vector2>(Vector2.positiveInfinity); } }
    public KVariablesExt<Vector2> PositiveInfinities(int nElems=1) { return new KVariablesExt<Vector2>(Vector2.positiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector2 GetComponent(KVariablesExt<Vector2> data, int index) { Vector2 value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector2 GetComponent(KVariablesExt<Vector2> data, string elem) { Vector2 value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Vector2> data, int index, Vector2 value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Vector2> data, string elem, Vector2 value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtVector3Int : ITraits<KVariablesExt<Vector3Int>, Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public bool TestEquals(KVariablesExt<Vector3Int> lhs, KVariablesExt<Vector3Int> rhs) { EqualityComparer<KVariablesExt<Vector3Int>> ec = EqualityComparer<KVariablesExt<Vector3Int>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector3Int lhs, Vector3Int rhs) { EqualityComparer<Vector3Int> ec = EqualityComparer<Vector3Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector3Int> lhs, KVariablesExt<Vector3Int> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector3Int> Zero { get { return new KVariablesExt<Vector3Int>(Vector3Int.zero); } }
    public KVariablesExt<Vector3Int> Zeroes(int nElems=1) { return new KVariablesExt<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Vector3Int> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Vector3Int>(Vector3Int.zero);
    #endif
    } }
    public KVariablesExt<Vector3Int> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Vector3Int>(Vector3Int.zero);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3Int GetComponent(KVariablesExt<Vector3Int> data, int index) { Vector3Int value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector3Int GetComponent(KVariablesExt<Vector3Int> data, string elem) { Vector3Int value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Vector3Int> data, int index, Vector3Int value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Vector3Int> data, string elem, Vector3Int value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtVector3 : ITraits<KVariablesExt<Vector3>, Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public bool TestEquals(KVariablesExt<Vector3> lhs, KVariablesExt<Vector3> rhs) { EqualityComparer<KVariablesExt<Vector3>> ec = EqualityComparer<KVariablesExt<Vector3>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector3 lhs, Vector3 rhs) { EqualityComparer<Vector3> ec = EqualityComparer<Vector3>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector3> lhs, KVariablesExt<Vector3> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector3> Zero { get { return new KVariablesExt<Vector3>(Vector3.zero); } }
    public KVariablesExt<Vector3> Zeroes(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.zero); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<Vector3> PositiveInfinity { get { return new KVariablesExt<Vector3>(Vector3.positiveInfinity); } }
    public KVariablesExt<Vector3> PositiveInfinities(int nElems=1) { return new KVariablesExt<Vector3>(Vector3.positiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector3 GetComponent(KVariablesExt<Vector3> data, int index) { Vector3 value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector3 GetComponent(KVariablesExt<Vector3> data, string elem) { Vector3 value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Vector3> data, int index, Vector3 value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Vector3> data, string elem, Vector3 value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtVector4 : ITraits<KVariablesExt<Vector4>, Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector4; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public bool TestEquals(KVariablesExt<Vector4> lhs, KVariablesExt<Vector4> rhs) { EqualityComparer<KVariablesExt<Vector4>> ec = EqualityComparer<KVariablesExt<Vector4>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Vector4 lhs, Vector4 rhs) { EqualityComparer<Vector4> ec = EqualityComparer<Vector4>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector4> lhs, KVariablesExt<Vector4> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector4> Zero { get { return new KVariablesExt<Vector4>(Vector4.zero); } }
    public KVariablesExt<Vector4> Zeroes(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.zero); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<Vector4> PositiveInfinity { get { return new KVariablesExt<Vector4>(Vector4.positiveInfinity); } }
    public KVariablesExt<Vector4> PositiveInfinities(int nElems=1) { return new KVariablesExt<Vector4>(Vector4.positiveInfinity); }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Vector4 GetComponent(KVariablesExt<Vector4> data, int index) { Vector4 value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Vector4 GetComponent(KVariablesExt<Vector4> data, string elem) { Vector4 value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Vector4> data, int index, Vector4 value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Vector4> data, string elem, Vector4 value) { data.Set(elem,value); }
}
public class TraitsKVariablesExtQuaternion : ITraits<KVariablesExt<Quaternion>, Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public bool TestEquals(KVariablesExt<Quaternion> lhs, KVariablesExt<Quaternion> rhs) { EqualityComparer<KVariablesExt<Quaternion>> ec = EqualityComparer<KVariablesExt<Quaternion>>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(Quaternion lhs, Quaternion rhs) { EqualityComparer<Quaternion> ec = EqualityComparer<Quaternion>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Quaternion> lhs, KVariablesExt<Quaternion> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Quaternion> Zero { get { return new KVariablesExt<Quaternion>(Quaternion.identity); } }
    public KVariablesExt<Quaternion> Zeroes(int nElems=1) { return new KVariablesExt<Quaternion>(Quaternion.identity); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Quaternion> PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Quaternion>(Quaternion.identity);
    #endif
    } }
    public KVariablesExt<Quaternion> PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return new KVariablesExt<Quaternion>(Quaternion.identity);
    #endif
    }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public Quaternion GetComponent(KVariablesExt<Quaternion> data, int index) { Quaternion value; data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public Quaternion GetComponent(KVariablesExt<Quaternion> data, string elem) { Quaternion value; data.Get(elem, out value); return value; }
    public void SetComponent(ref KVariablesExt<Quaternion> data, int index, Quaternion value) { data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); }
    public void SetComponent(ref KVariablesExt<Quaternion> data, string elem, Quaternion value) { data.Set(elem,value); }
}
public class TraitsKVariableTypeSet : ITraits<KVariableTypeSet, bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariableTypeSetType; }
    public DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public bool TestEquals(KVariableTypeSet lhs, KVariableTypeSet rhs) { EqualityComparer<KVariableTypeSet> ec = EqualityComparer<KVariableTypeSet>.Default; return ec.Equals(lhs, rhs); }
    public bool TestEqualsComponent(bool lhs, bool rhs) { EqualityComparer<bool> ec = EqualityComparer<bool>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariableTypeSet lhs, KVariableTypeSet rhs) { lhs.SetEqual(rhs); }
    public KVariableTypeSet Zero { get { return KVariableTypeInfo.None; } }
    public KVariableTypeSet Zeroes(int nElems=1) { return KVariableTypeInfo.None; }
    public bool HasInfinity { get=>false; }
    public KVariableTypeSet PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return KVariableTypeInfo.None;
    #endif
     } }
    public KVariableTypeSet PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return KVariableTypeInfo.None;
    #endif
     }
    public ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public bool ElementAccessByIndex { get=>true; }
    public bool ElementAccessByString { get=>true; }
    public bool GetComponent(KVariableTypeSet data, int index) { return data.Contains(KVariableTypeInfo.IndexToKVariableEnum(index)); }
    public bool GetComponent(KVariableTypeSet data, string elem) { return data.Contains(elem); }
    public void SetComponent(ref KVariableTypeSet data, int index, bool value) { if (value){data.Add(KVariableTypeInfo.IndexToKVariableEnum(index));} else {data.Remove(KVariableTypeInfo.IndexToKVariableEnum(index));} }
    public void SetComponent(ref KVariableTypeSet data, string elem, bool value) { if (value){data.Add(elem);}else{data.Remove(elem);} }
}
