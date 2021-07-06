using System.Collections.Generic;
using UnityEngine;

public class ListDataObject<T> : DataSetObjectHeader, IDataSetObject<List<T>, T> {
    protected ITraits<List<T>, T> m_traits;
    public List<T> m_data;
    public ITraits<List<T>, T> Traits { get=>m_traits; }
    public List<T> Data { get=>m_data; set { m_data=value; SetModified(); } }
    public override DataTypeEnum DataType { get=>m_traits.DataType; }
    public override DataTypeEnum ComponentType { get=>m_traits.ComponentType; }
    public T this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public T GetComponent(int elem) { return m_data[elem]; }
    public T GetComponent(string elem) { throw new System.InvalidOperationException(); }
    public void SetComponent(int elem, T value) { m_data[elem] = value; SetModified(); }
    public void SetComponent(string elem, T value) { throw new System.InvalidOperationException(); }
    public ListDataObject(string name, IObjectRegistry parent = null, List<T> data = null) : base(name, parent) { m_data = data; }
    public ListDataObject(IDataSetObjectHeader obj) : base(obj) {}
    public ListDataObject(ListDataObject<T> obj) : base(obj) {}
    public ListDataObject() : base() {}
}

public class TriggerListDataObject : ListDataObject<Trigger> {
    public TriggerListDataObject(string name, IObjectRegistry parent = null, List<Trigger> data = null) : base(name, parent, data) { m_traits = new TraitsListTrigger(); }
    public TriggerListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListTrigger(); }
    public TriggerListDataObject(TriggerListDataObject obj) : base(obj) { m_traits = new TraitsListTrigger(); }
    public TriggerListDataObject() : base() { m_traits = new TraitsListTrigger(); }
}
public class BoolListDataObject : ListDataObject<bool> {
    public BoolListDataObject(string name, IObjectRegistry parent = null, List<bool> data = null) : base(name, parent, data) { m_traits = new TraitsListBool(); }
    public BoolListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListBool(); }
    public BoolListDataObject(BoolListDataObject obj) : base(obj) { m_traits = new TraitsListBool(); }
    public BoolListDataObject() : base() { m_traits = new TraitsListBool(); }
}
public class CharListDataObject : ListDataObject<char> {
    public CharListDataObject(string name, IObjectRegistry parent = null, List<char> data = null) : base(name, parent, data) { m_traits = new TraitsListChar(); }
    public CharListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListChar(); }
    public CharListDataObject(CharListDataObject obj) : base(obj) { m_traits = new TraitsListChar(); }
    public CharListDataObject() : base() { m_traits = new TraitsListChar(); }
}
public class StringListDataObject : ListDataObject<string> {
    public StringListDataObject(string name, IObjectRegistry parent = null, List<string> data = null) : base(name, parent, data) { m_traits = new TraitsListString(); }
    public StringListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListString(); }
    public StringListDataObject(StringListDataObject obj) : base(obj) { m_traits = new TraitsListString(); }
    public StringListDataObject() : base() { m_traits = new TraitsListString(); }
}
public class IntListDataObject : ListDataObject<int> {
    public IntListDataObject(string name, IObjectRegistry parent = null, List<int> data = null) : base(name, parent, data) { m_traits = new TraitsListInt(); }
    public IntListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListInt(); }
    public IntListDataObject(IntListDataObject obj) : base(obj) { m_traits = new TraitsListInt(); }
    public IntListDataObject() : base() { m_traits = new TraitsListInt(); }
}
public class FloatListDataObject : ListDataObject<float> {
    public FloatListDataObject(string name, IObjectRegistry parent = null, List<float> data = null) : base(name, parent, data) { m_traits = new TraitsListFloat(); }
    public FloatListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListFloat(); }
    public FloatListDataObject(FloatListDataObject obj) : base(obj) { m_traits = new TraitsListFloat(); }
    public FloatListDataObject() : base() { m_traits = new TraitsListFloat(); }
}
public class Vector2IntListDataObject : ListDataObject<Vector2Int> {
    public Vector2IntListDataObject(string name, IObjectRegistry parent = null, List<Vector2Int> data = null) : base(name, parent, data) { m_traits = new TraitsListVector2Int(); }
    public Vector2IntListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListVector2Int(); }
    public Vector2IntListDataObject(Vector2IntListDataObject obj) : base(obj) { m_traits = new TraitsListVector2Int(); }
    public Vector2IntListDataObject() : base() { m_traits = new TraitsListVector2Int(); }
}
public class Vector2ListDataObject : ListDataObject<Vector2> {
    public Vector2ListDataObject(string name, IObjectRegistry parent = null, List<Vector2> data = null) : base(name, parent, data) { m_traits = new TraitsListVector2(); }
    public Vector2ListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListVector2(); }
    public Vector2ListDataObject(Vector2ListDataObject obj) : base(obj) { m_traits = new TraitsListVector2(); }
    public Vector2ListDataObject() : base() { m_traits = new TraitsListVector2(); }
}
public class Vector3IntListDataObject : ListDataObject<Vector3Int> {
    public Vector3IntListDataObject(string name, IObjectRegistry parent = null, List<Vector3Int> data = null) : base(name, parent, data) { m_traits = new TraitsListVector3Int(); }
    public Vector3IntListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListVector3Int(); }
    public Vector3IntListDataObject(Vector3IntListDataObject obj) : base(obj) { m_traits = new TraitsListVector3Int(); }
    public Vector3IntListDataObject() : base() { m_traits = new TraitsListVector3Int(); }
}
public class Vector3ListDataObject : ListDataObject<Vector3> {
    public Vector3ListDataObject(string name, IObjectRegistry parent = null, List<Vector3> data = null) : base(name, parent, data) { m_traits = new TraitsListVector3(); }
    public Vector3ListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListVector3(); }
    public Vector3ListDataObject(Vector3ListDataObject obj) : base(obj) { m_traits = new TraitsListVector3(); }
    public Vector3ListDataObject() : base() { m_traits = new TraitsListVector3(); }
}
public class Vector4ListDataObject : ListDataObject<Vector4> {
    public Vector4ListDataObject(string name, IObjectRegistry parent = null, List<Vector4> data = null) : base(name, parent, data) { m_traits = new TraitsListVector4(); }
    public Vector4ListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListVector4(); }
    public Vector4ListDataObject(Vector4ListDataObject obj) : base(obj) { m_traits = new TraitsListVector4(); }
    public Vector4ListDataObject() : base() { m_traits = new TraitsListVector4(); }
}
public class QuaternionListDataObject : ListDataObject<Quaternion> {
    public QuaternionListDataObject(string name, IObjectRegistry parent = null, List<Quaternion> data = null) : base(name, parent, data) { m_traits = new TraitsListQuaternion(); }
    public QuaternionListDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsListQuaternion(); }
    public QuaternionListDataObject(QuaternionListDataObject obj) : base(obj) { m_traits = new TraitsListQuaternion(); }
    public QuaternionListDataObject() : base() { m_traits = new TraitsListQuaternion(); }
}
