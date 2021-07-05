using UnityEngine;

// Header information
public class DataObjectHeader : ObjectHeader, IDataObjectHeader {
    public virtual DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public DataObjectHeader(string name, IObjectRegistry parent = null) : base (name, parent) {}
    public DataObjectHeader(IDataObjectHeader obj) : base (obj) {}
    public DataObjectHeader(DataObjectHeader obj) : base (obj) {}
    public DataObjectHeader() {}
}

public class GenericDataObject : DataObjectHeader, IDataObject<object> {
    public object m_data;
    public object Data { get=>m_data; set=>m_data=value; }
    public GenericDataObject(string name, IObjectRegistry parent = null, object data = null) : base(name, parent) { m_data = data; }
    public GenericDataObject(IDataObject<object> obj) : base(obj) {}
    public GenericDataObject(GenericDataObject obj) : base(obj) {}
    public GenericDataObject() {}
}
public class TriggerDataObject : DataObjectHeader, IDataObject<Trigger> {
    public Trigger m_data;
    public Trigger Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public TriggerDataObject(string name, IObjectRegistry parent = null, Trigger data = new Trigger()) : base(name, parent) { m_data = data; }
    public TriggerDataObject(IDataObject<Trigger> obj) : base(obj) {}
    public TriggerDataObject(TriggerDataObject obj) : base(obj) {}
    public TriggerDataObject() {}
}
public class BoolDataObject : DataObjectHeader, IDataObject<bool> {
    public bool m_data;
    public bool Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public BoolDataObject(string name, IObjectRegistry parent = null, bool data = new bool()) : base(name, parent) { m_data = data; }
    public BoolDataObject(IDataObject<bool> obj) : base(obj) {}
    public BoolDataObject(BoolDataObject obj) : base(obj) {}
    public BoolDataObject() {}
}
public class CharDataObject : DataObjectHeader, IDataObject<char> {
    public char m_data;
    public char Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public CharDataObject(string name, IObjectRegistry parent = null, char data = new char()) : base(name, parent) { m_data = data; }
    public CharDataObject(IDataObject<char> obj) : base(obj) {}
    public CharDataObject(CharDataObject obj) : base(obj) {}
    public CharDataObject() {}
}
public class StringDataObject : DataObjectHeader, IDataObject<string> {
    public string m_data;
    public string Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public StringDataObject(string name, IObjectRegistry parent = null, string data = null) : base(name, parent) { m_data = data; }
    public StringDataObject(IDataObject<string> obj) : base(obj) {}
    public StringDataObject(StringDataObject obj) : base(obj) {}
    public StringDataObject() {}
}
public class IntDataObject : DataObjectHeader, IDataObject<int> {
    public int m_data;
    public int Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public IntDataObject(string name, IObjectRegistry parent = null, int data = 0) : base(name, parent) { m_data = data; }
    public IntDataObject(IDataObject<int> obj) : base(obj) {}
    public IntDataObject(IntDataObject obj) : base(obj) {}
    public IntDataObject() {}
}
public class FloatDataObject : DataObjectHeader, IDataObject<float> {
    public float m_data;
    public float Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public FloatDataObject(string name, IObjectRegistry parent = null, float data = 0f) : base(name, parent) { m_data = data; }
    public FloatDataObject(IDataObject<float> obj) : base(obj) {}
    public FloatDataObject(FloatDataObject obj) : base(obj) {}
    public FloatDataObject() {}
}
public class Vector2IntDataObject : DataObjectHeader, IDataObject<Vector2Int> {
    public Vector2Int m_data;
    public Vector2Int Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public Vector2IntDataObject(string name, IObjectRegistry parent = null, Vector2Int data = new Vector2Int()) : base(name, parent) { m_data = data; }
    public Vector2IntDataObject(IDataObject<Vector2Int> obj) : base(obj) {}
    public Vector2IntDataObject(Vector2IntDataObject obj) : base(obj) {}
    public Vector2IntDataObject() {}
}
public class Vector2DataObject : DataObjectHeader, IDataObject<Vector2> {
    public Vector2 m_data;
    public Vector2 Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public Vector2DataObject(string name, IObjectRegistry parent = null, Vector2 data = new Vector2()) : base(name, parent) { m_data = data; }
    public Vector2DataObject(IDataObject<Vector2> obj) : base(obj) {}
    public Vector2DataObject(Vector2DataObject obj) : base(obj) {}
    public Vector2DataObject() {}
}
public class Vector3IntDataObject : DataObjectHeader, IDataObject<Vector3Int> {
    public Vector3Int m_data;
    public Vector3Int Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public Vector3IntDataObject(string name, IObjectRegistry parent = null, Vector3Int data = new Vector3Int()) : base(name, parent) { m_data = data; }
    public Vector3IntDataObject(IDataObject<Vector3Int> obj) : base(obj) {}
    public Vector3IntDataObject(Vector3IntDataObject obj) : base(obj) {}
    public Vector3IntDataObject() {}
}
public class Vector3DataObject : DataObjectHeader, IDataObject<Vector3> {
    public Vector3 m_data;
    public Vector3 Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public Vector3DataObject(string name, IObjectRegistry parent = null, Vector3 data = new Vector3()) : base(name, parent) { m_data = data; }
    public Vector3DataObject(IDataObject<Vector3> obj) : base(obj) {}
    public Vector3DataObject(Vector3DataObject obj) : base(obj) {}
    public Vector3DataObject() {}
}
public class Vector4DataObject : DataObjectHeader, IDataObject<Vector4> {
    public Vector4 m_data;
    public Vector4 Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public Vector4DataObject(string name, IObjectRegistry parent = null, Vector4 data = new Vector4()) : base(name, parent) { m_data = data; }
    public Vector4DataObject(IDataObject<Vector4> obj) : base(obj) {}
    public Vector4DataObject(Vector4DataObject obj) : base(obj) {}
    public Vector4DataObject() {}
}
public class QuaternionDataObject : DataObjectHeader, IDataObject<Quaternion> {
    public Quaternion m_data;
    public Quaternion Data { get=>m_data; set=>m_data=value; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public QuaternionDataObject(string name, IObjectRegistry parent = null, Quaternion data = new Quaternion()) : base(name, parent) { m_data = data; }
    public QuaternionDataObject(IDataObject<Quaternion> obj) : base(obj) {}
    public QuaternionDataObject(QuaternionDataObject obj) : base(obj) {}
    public QuaternionDataObject() {}
}
