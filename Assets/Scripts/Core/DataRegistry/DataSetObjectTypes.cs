using System.Collections.Generic;
using UnityEngine;

// Header information
public class DataSetObjectHeader : DataObjectHeader, IDataSetObjectId {
    public virtual DataTypeEnum ComponentType { get=>DataTypeEnum.None; }
    public virtual string GetComponentName(int elem) { return "GenericComponent"; }
    public virtual string GetComponentName(string elem) { return "GenericComponent"; }
    public virtual bool ElementAccessByIndex { get=>true; }
    public virtual bool ElementAccessByString { get=>false; }
    public DataSetObjectHeader(string name, IObjectRegistry parent = null) : base(name, parent) {}
    public DataSetObjectHeader(IDataSetObjectId obj) : base(obj) {}
    public DataSetObjectHeader(DataSetObjectHeader obj) : base(obj) {}
    public DataSetObjectHeader() : base() {}
}

public class TriggerListDataObject : DataSetObjectHeader, IDataSetObject<List<Trigger>, Trigger> {
    public List<Trigger> m_data;
    public List<Trigger> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Trigger this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Trigger GetComponent(int elem) { return m_data[elem]; }
    public Trigger GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Trigger value) { m_data[elem] = value; }
    public void SetComponent(string elem, Trigger value) { throw new System.InvalidOperationException(); }
    public TriggerListDataObject(string name, IObjectRegistry parent = null, List<Trigger> data = null) : base(name, parent) { m_data = data; }
    public TriggerListDataObject(IDataSetObjectId obj) : base(obj) {}
    public TriggerListDataObject(TriggerListDataObject obj) : base(obj) {}
    public TriggerListDataObject() : base() {}
}
public class BoolListDataObject : DataSetObjectHeader, IDataSetObject<List<bool>, bool> {
    public List<bool> m_data;
    public List<bool> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public bool this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public bool GetComponent(int elem) { return m_data[elem]; }
    public bool GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, bool value) { m_data[elem] = value; }
    public void SetComponent(string elem, bool value) { throw new System.InvalidOperationException(); }
    public BoolListDataObject(string name, IObjectRegistry parent = null, List<bool> data = null) : base(name, parent) { m_data = data; }
    public BoolListDataObject(IDataSetObjectId obj) : base(obj) {}
    public BoolListDataObject(BoolListDataObject obj) : base(obj) {}
    public BoolListDataObject() : base() {}
}
public class CharListDataObject : DataSetObjectHeader, IDataSetObject<List<char>, char> {
    public List<char> m_data;
    public List<char> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public char this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public char GetComponent(int elem) { return m_data[elem]; }
    public char GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, char value) { m_data[elem] = value; }
    public void SetComponent(string elem, char value) { throw new System.InvalidOperationException(); }
    public CharListDataObject(string name, IObjectRegistry parent = null, List<char> data = null) : base(name, parent) { m_data = data; }
    public CharListDataObject(IDataSetObjectId obj) : base(obj) {}
    public CharListDataObject(CharListDataObject obj) : base(obj) {}
    public CharListDataObject() : base() {}
}
public class StringListDataObject : DataSetObjectHeader, IDataSetObject<List<string>, string> {
    public List<string> m_data;
    public List<string> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public string this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public string GetComponent(int elem) { return m_data[elem]; }
    public string GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, string value) { m_data[elem] = value; }
    public void SetComponent(string elem, string value) { throw new System.InvalidOperationException(); }
    public StringListDataObject(string name, IObjectRegistry parent = null, List<string> data = null) : base(name, parent) { m_data = data; }
    public StringListDataObject(IDataSetObjectId obj) : base(obj) {}
    public StringListDataObject(StringListDataObject obj) : base(obj) {}
    public StringListDataObject() : base() {}
}
public class IntListDataObject : DataSetObjectHeader, IDataSetObject<List<int>, int> {
    public List<int> m_data;
    public List<int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public int GetComponent(int elem) { return m_data[elem]; }
    public int GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, int value) { m_data[elem] = value; }
    public void SetComponent(string elem, int value) { throw new System.InvalidOperationException(); }
    public IntListDataObject(string name, IObjectRegistry parent = null, List<int> data = null) : base(name, parent) { m_data = data; }
    public IntListDataObject(IDataSetObjectId obj) : base(obj) {}
    public IntListDataObject(IntListDataObject obj) : base(obj) {}
    public IntListDataObject() : base() {}
}
public class FloatListDataObject : DataSetObjectHeader, IDataSetObject<List<float>, float> {
    public List<float> m_data;
    public List<float> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public float this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public float GetComponent(int elem) { return m_data[elem]; }
    public float GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, float value) { m_data[elem] = value; }
    public void SetComponent(string elem, float value) { throw new System.InvalidOperationException(); }
    public FloatListDataObject(string name, IObjectRegistry parent = null, List<float> data = null) : base(name, parent) { m_data = data; }
    public FloatListDataObject(IDataSetObjectId obj) : base(obj) {}
    public FloatListDataObject(FloatListDataObject obj) : base(obj) {}
    public FloatListDataObject() : base() {}
}
public class Vector2IntListDataObject : DataSetObjectHeader, IDataSetObject<List<Vector2Int>, Vector2Int> {
    public List<Vector2Int> m_data;
    public List<Vector2Int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Vector2Int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector2Int GetComponent(int elem) { return m_data[elem]; }
    public Vector2Int GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Vector2Int value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector2Int value) { throw new System.InvalidOperationException(); }
    public Vector2IntListDataObject(string name, IObjectRegistry parent = null, List<Vector2Int> data = null) : base(name, parent) { m_data = data; }
    public Vector2IntListDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector2IntListDataObject(Vector2IntListDataObject obj) : base(obj) {}
    public Vector2IntListDataObject() : base() {}
}
public class Vector2ListDataObject : DataSetObjectHeader, IDataSetObject<List<Vector2>, Vector2> {
    public List<Vector2> m_data;
    public List<Vector2> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Vector2 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector2 GetComponent(int elem) { return m_data[elem]; }
    public Vector2 GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Vector2 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector2 value) { throw new System.InvalidOperationException(); }
    public Vector2ListDataObject(string name, IObjectRegistry parent = null, List<Vector2> data = null) : base(name, parent) { m_data = data; }
    public Vector2ListDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector2ListDataObject(Vector2ListDataObject obj) : base(obj) {}
    public Vector2ListDataObject() : base() {}
}
public class Vector3IntListDataObject : DataSetObjectHeader, IDataSetObject<List<Vector3Int>, Vector3Int> {
    public List<Vector3Int> m_data;
    public List<Vector3Int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Vector3Int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector3Int GetComponent(int elem) { return m_data[elem]; }
    public Vector3Int GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Vector3Int value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector3Int value) { throw new System.InvalidOperationException(); }
    public Vector3IntListDataObject(string name, IObjectRegistry parent = null, List<Vector3Int> data = null) : base(name, parent) { m_data = data; }
    public Vector3IntListDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector3IntListDataObject(Vector3IntListDataObject obj) : base(obj) {}
    public Vector3IntListDataObject() : base() {}
}
public class Vector3ListDataObject : DataSetObjectHeader, IDataSetObject<List<Vector3>, Vector3> {
    public List<Vector3> m_data;
    public List<Vector3> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Vector3 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector3 GetComponent(int elem) { return m_data[elem]; }
    public Vector3 GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Vector3 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector3 value) { throw new System.InvalidOperationException(); }
    public Vector3ListDataObject(string name, IObjectRegistry parent = null, List<Vector3> data = null) : base(name, parent) { m_data = data; }
    public Vector3ListDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector3ListDataObject(Vector3ListDataObject obj) : base(obj) {}
    public Vector3ListDataObject() : base() {}
}
public class Vector4ListDataObject : DataSetObjectHeader, IDataSetObject<List<Vector4>, Vector4> {
    public List<Vector4> m_data;
    public List<Vector4> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Vector4 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector4 GetComponent(int elem) { return m_data[elem]; }
    public Vector4 GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Vector4 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector4 value) { throw new System.InvalidOperationException(); }
    public Vector4ListDataObject(string name, IObjectRegistry parent = null, List<Vector4> data = null) : base(name, parent) { m_data = data; }
    public Vector4ListDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector4ListDataObject(Vector4ListDataObject obj) : base(obj) {}
    public Vector4ListDataObject() : base() {}
}
public class QuaternionListDataObject : DataSetObjectHeader, IDataSetObject<List<Quaternion>, Quaternion> {
    public List<Quaternion> m_data;
    public List<Quaternion> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    //public override bool ElementAccessByString { get=>true; } // default is false - uncomment if true
    public Quaternion this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Quaternion GetComponent(int elem) { return m_data[elem]; }
    public Quaternion GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, Quaternion value) { m_data[elem] = value; }
    public void SetComponent(string elem, Quaternion value) { throw new System.InvalidOperationException(); }
    public QuaternionListDataObject(string name, IObjectRegistry parent = null, List<Quaternion> data = null) : base(name, parent) { m_data = data; }
    public QuaternionListDataObject(IDataSetObjectId obj) : base(obj) {}
    public QuaternionListDataObject(QuaternionListDataObject obj) : base(obj) {}
    public QuaternionListDataObject() : base() {}
}
public class TriggerKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<Trigger>, Trigger> {
    public KVariables<Trigger> m_data;
    public KVariables<Trigger> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Trigger this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Trigger GetComponent(int elem) { return m_data[elem]; }
    public Trigger GetComponent(string elem) { Trigger value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Trigger value) { m_data[elem] = value; }
    public void SetComponent(string elem, Trigger value) { m_data.Set(elem, value); }
    public TriggerKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Trigger> data = null) : base(name, parent) { m_data = data; }
    public TriggerKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public TriggerKVariablesDataObject(TriggerKVariablesDataObject obj) : base(obj) {}
    public TriggerKVariablesDataObject() : base() {}
}
public class BoolKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<bool>, bool> {
    public KVariables<bool> m_data;
    public KVariables<bool> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public bool this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public bool GetComponent(int elem) { return m_data[elem]; }
    public bool GetComponent(string elem) { bool value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, bool value) { m_data[elem] = value; }
    public void SetComponent(string elem, bool value) { m_data.Set(elem, value); }
    public BoolKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<bool> data = null) : base(name, parent) { m_data = data; }
    public BoolKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public BoolKVariablesDataObject(BoolKVariablesDataObject obj) : base(obj) {}
    public BoolKVariablesDataObject() : base() {}
}
public class CharKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<char>, char> {
    public KVariables<char> m_data;
    public KVariables<char> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public char this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public char GetComponent(int elem) { return m_data[elem]; }
    public char GetComponent(string elem) { char value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, char value) { m_data[elem] = value; }
    public void SetComponent(string elem, char value) { m_data.Set(elem, value); }
    public CharKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<char> data = null) : base(name, parent) { m_data = data; }
    public CharKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public CharKVariablesDataObject(CharKVariablesDataObject obj) : base(obj) {}
    public CharKVariablesDataObject() : base() {}
}
public class StringKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<string>, string> {
    public KVariables<string> m_data;
    public KVariables<string> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public string this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public string GetComponent(int elem) { return m_data[elem]; }
    public string GetComponent(string elem) { string value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, string value) { m_data[elem] = value; }
    public void SetComponent(string elem, string value) { m_data.Set(elem, value); }
    public StringKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<string> data = null) : base(name, parent) { m_data = data; }
    public StringKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public StringKVariablesDataObject(StringKVariablesDataObject obj) : base(obj) {}
    public StringKVariablesDataObject() : base() {}
}
public class IntKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<int>, int> {
    public KVariables<int> m_data;
    public KVariables<int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public int GetComponent(int elem) { return m_data[elem]; }
    public int GetComponent(string elem) { int value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, int value) { m_data[elem] = value; }
    public void SetComponent(string elem, int value) { m_data.Set(elem, value); }
    public IntKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<int> data = null) : base(name, parent) { m_data = data; }
    public IntKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public IntKVariablesDataObject(IntKVariablesDataObject obj) : base(obj) {}
    public IntKVariablesDataObject() : base() {}
}
public class FloatKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<float>, float> {
    public KVariables<float> m_data;
    public KVariables<float> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public float this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public float GetComponent(int elem) { return m_data[elem]; }
    public float GetComponent(string elem) { float value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, float value) { m_data[elem] = value; }
    public void SetComponent(string elem, float value) { m_data.Set(elem, value); }
    public FloatKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<float> data = null) : base(name, parent) { m_data = data; }
    public FloatKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public FloatKVariablesDataObject(FloatKVariablesDataObject obj) : base(obj) {}
    public FloatKVariablesDataObject() : base() {}
}
public class Vector2IntKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<Vector2Int>, Vector2Int> {
    public KVariables<Vector2Int> m_data;
    public KVariables<Vector2Int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector2Int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector2Int GetComponent(int elem) { return m_data[elem]; }
    public Vector2Int GetComponent(string elem) { Vector2Int value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector2Int value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector2Int value) { m_data.Set(elem, value); }
    public Vector2IntKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector2Int> data = null) : base(name, parent) { m_data = data; }
    public Vector2IntKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector2IntKVariablesDataObject(Vector2IntKVariablesDataObject obj) : base(obj) {}
    public Vector2IntKVariablesDataObject() : base() {}
}
public class Vector2KVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<Vector2>, Vector2> {
    public KVariables<Vector2> m_data;
    public KVariables<Vector2> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector2 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector2 GetComponent(int elem) { return m_data[elem]; }
    public Vector2 GetComponent(string elem) { Vector2 value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector2 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector2 value) { m_data.Set(elem, value); }
    public Vector2KVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector2> data = null) : base(name, parent) { m_data = data; }
    public Vector2KVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector2KVariablesDataObject(Vector2KVariablesDataObject obj) : base(obj) {}
    public Vector2KVariablesDataObject() : base() {}
}
public class Vector3IntKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<Vector3Int>, Vector3Int> {
    public KVariables<Vector3Int> m_data;
    public KVariables<Vector3Int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector3Int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector3Int GetComponent(int elem) { return m_data[elem]; }
    public Vector3Int GetComponent(string elem) { Vector3Int value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector3Int value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector3Int value) { m_data.Set(elem, value); }
    public Vector3IntKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector3Int> data = null) : base(name, parent) { m_data = data; }
    public Vector3IntKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector3IntKVariablesDataObject(Vector3IntKVariablesDataObject obj) : base(obj) {}
    public Vector3IntKVariablesDataObject() : base() {}
}
public class Vector3KVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<Vector3>, Vector3> {
    public KVariables<Vector3> m_data;
    public KVariables<Vector3> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector3 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector3 GetComponent(int elem) { return m_data[elem]; }
    public Vector3 GetComponent(string elem) { Vector3 value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector3 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector3 value) { m_data.Set(elem, value); }
    public Vector3KVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector3> data = null) : base(name, parent) { m_data = data; }
    public Vector3KVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector3KVariablesDataObject(Vector3KVariablesDataObject obj) : base(obj) {}
    public Vector3KVariablesDataObject() : base() {}
}
public class QuaternionKVariablesDataObject : DataSetObjectHeader, IDataSetObject<KVariables<Quaternion>, Quaternion> {
    public KVariables<Quaternion> m_data;
    public KVariables<Quaternion> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Quaternion this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Quaternion GetComponent(int elem) { return m_data[elem]; }
    public Quaternion GetComponent(string elem) { Quaternion value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Quaternion value) { m_data[elem] = value; }
    public void SetComponent(string elem, Quaternion value) { m_data.Set(elem, value); }
    public QuaternionKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Quaternion> data = null) : base(name, parent) { m_data = data; }
    public QuaternionKVariablesDataObject(IDataSetObjectId obj) : base(obj) {}
    public QuaternionKVariablesDataObject(QuaternionKVariablesDataObject obj) : base(obj) {}
    public QuaternionKVariablesDataObject() : base() {}
}
public class TriggerKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<Trigger>, Trigger> {
    public KVariablesExt<Trigger> m_data;
    public KVariablesExt<Trigger> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Trigger this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Trigger GetComponent(int elem) { return m_data[elem]; }
    public Trigger GetComponent(string elem) { Trigger value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Trigger value) { m_data[elem] = value; }
    public void SetComponent(string elem, Trigger value) { m_data.Set(elem, value); }
    public TriggerKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Trigger> data = null) : base(name, parent) { m_data = data; }
    public TriggerKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public TriggerKVariablesExtDataObject(TriggerKVariablesExtDataObject obj) : base(obj) {}
    public TriggerKVariablesExtDataObject() : base() {}
}
public class BoolKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<bool>, bool> {
    public KVariablesExt<bool> m_data;
    public KVariablesExt<bool> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public bool this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public bool GetComponent(int elem) { return m_data[elem]; }
    public bool GetComponent(string elem) { bool value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, bool value) { m_data[elem] = value; }
    public void SetComponent(string elem, bool value) { m_data.Set(elem, value); }
    public BoolKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<bool> data = null) : base(name, parent) { m_data = data; }
    public BoolKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public BoolKVariablesExtDataObject(BoolKVariablesExtDataObject obj) : base(obj) {}
    public BoolKVariablesExtDataObject() : base() {}
}
public class CharKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<char>, char> {
    public KVariablesExt<char> m_data;
    public KVariablesExt<char> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public char this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public char GetComponent(int elem) { return m_data[elem]; }
    public char GetComponent(string elem) { char value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, char value) { m_data[elem] = value; }
    public void SetComponent(string elem, char value) { m_data.Set(elem, value); }
    public CharKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<char> data = null) : base(name, parent) { m_data = data; }
    public CharKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public CharKVariablesExtDataObject(CharKVariablesExtDataObject obj) : base(obj) {}
    public CharKVariablesExtDataObject() : base() {}
}
public class StringKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<string>, string> {
    public KVariablesExt<string> m_data;
    public KVariablesExt<string> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public string this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public string GetComponent(int elem) { return m_data[elem]; }
    public string GetComponent(string elem) { string value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, string value) { m_data[elem] = value; }
    public void SetComponent(string elem, string value) { m_data.Set(elem, value); }
    public StringKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<string> data = null) : base(name, parent) { m_data = data; }
    public StringKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public StringKVariablesExtDataObject(StringKVariablesExtDataObject obj) : base(obj) {}
    public StringKVariablesExtDataObject() : base() {}
}
public class IntKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<int>, int> {
    public KVariablesExt<int> m_data;
    public KVariablesExt<int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public int GetComponent(int elem) { return m_data[elem]; }
    public int GetComponent(string elem) { int value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, int value) { m_data[elem] = value; }
    public void SetComponent(string elem, int value) { m_data.Set(elem, value); }
    public IntKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<int> data = null) : base(name, parent) { m_data = data; }
    public IntKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public IntKVariablesExtDataObject(IntKVariablesExtDataObject obj) : base(obj) {}
    public IntKVariablesExtDataObject() : base() {}
}
public class FloatKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<float>, float> {
    public KVariablesExt<float> m_data;
    public KVariablesExt<float> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public float this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public float GetComponent(int elem) { return m_data[elem]; }
    public float GetComponent(string elem) { float value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, float value) { m_data[elem] = value; }
    public void SetComponent(string elem, float value) { m_data.Set(elem, value); }
    public FloatKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<float> data = null) : base(name, parent) { m_data = data; }
    public FloatKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public FloatKVariablesExtDataObject(FloatKVariablesExtDataObject obj) : base(obj) {}
    public FloatKVariablesExtDataObject() : base() {}
}
public class Vector2IntKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<Vector2Int>, Vector2Int> {
    public KVariablesExt<Vector2Int> m_data;
    public KVariablesExt<Vector2Int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector2Int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector2Int GetComponent(int elem) { return m_data[elem]; }
    public Vector2Int GetComponent(string elem) { Vector2Int value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector2Int value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector2Int value) { m_data.Set(elem, value); }
    public Vector2IntKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector2Int> data = null) : base(name, parent) { m_data = data; }
    public Vector2IntKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector2IntKVariablesExtDataObject(Vector2IntKVariablesExtDataObject obj) : base(obj) {}
    public Vector2IntKVariablesExtDataObject() : base() {}
}
public class Vector2KVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<Vector2>, Vector2> {
    public KVariablesExt<Vector2> m_data;
    public KVariablesExt<Vector2> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector2 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector2 GetComponent(int elem) { return m_data[elem]; }
    public Vector2 GetComponent(string elem) { Vector2 value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector2 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector2 value) { m_data.Set(elem, value); }
    public Vector2KVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector2> data = null) : base(name, parent) { m_data = data; }
    public Vector2KVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector2KVariablesExtDataObject(Vector2KVariablesExtDataObject obj) : base(obj) {}
    public Vector2KVariablesExtDataObject() : base() {}
}
public class Vector3IntKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<Vector3Int>, Vector3Int> {
    public KVariablesExt<Vector3Int> m_data;
    public KVariablesExt<Vector3Int> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector3Int this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector3Int GetComponent(int elem) { return m_data[elem]; }
    public Vector3Int GetComponent(string elem) { Vector3Int value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector3Int value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector3Int value) { m_data.Set(elem, value); }
    public Vector3IntKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector3Int> data = null) : base(name, parent) { m_data = data; }
    public Vector3IntKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector3IntKVariablesExtDataObject(Vector3IntKVariablesExtDataObject obj) : base(obj) {}
    public Vector3IntKVariablesExtDataObject() : base() {}
}
public class Vector3KVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<Vector3>, Vector3> {
    public KVariablesExt<Vector3> m_data;
    public KVariablesExt<Vector3> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Vector3 this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Vector3 GetComponent(int elem) { return m_data[elem]; }
    public Vector3 GetComponent(string elem) { Vector3 value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Vector3 value) { m_data[elem] = value; }
    public void SetComponent(string elem, Vector3 value) { m_data.Set(elem, value); }
    public Vector3KVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector3> data = null) : base(name, parent) { m_data = data; }
    public Vector3KVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public Vector3KVariablesExtDataObject(Vector3KVariablesExtDataObject obj) : base(obj) {}
    public Vector3KVariablesExtDataObject() : base() {}
}
public class QuaternionKVariablesExtDataObject : DataSetObjectHeader, IDataSetObject<KVariablesExt<Quaternion>, Quaternion> {
    public KVariablesExt<Quaternion> m_data;
    public KVariablesExt<Quaternion> Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    //public override bool ElementAccessByIndex { get=>false; } // default is true - uncomment if false
    public override bool ElementAccessByString { get=>true; }
    public Quaternion this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public Quaternion GetComponent(int elem) { return m_data[elem]; }
    public Quaternion GetComponent(string elem) { Quaternion value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, Quaternion value) { m_data[elem] = value; }
    public void SetComponent(string elem, Quaternion value) { m_data.Set(elem, value); }
    public QuaternionKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Quaternion> data = null) : base(name, parent) { m_data = data; }
    public QuaternionKVariablesExtDataObject(IDataSetObjectId obj) : base(obj) {}
    public QuaternionKVariablesExtDataObject(QuaternionKVariablesExtDataObject obj) : base(obj) {}
    public QuaternionKVariablesExtDataObject() : base() {}
}
