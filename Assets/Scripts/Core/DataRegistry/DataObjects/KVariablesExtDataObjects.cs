using UnityEngine;

public class KVariablesExtDataObject<T> : DataSetObjectHeader , IDataSetObject<KVariablesExt<T>, T> {
    protected ITraits<KVariablesExt<T>, T> m_traits;
    public KVariablesExt<T> m_data;
    public ITraits<KVariablesExt<T>, T> Traits { get=>m_traits; }
    public KVariablesExt<T> Data { get=>m_data; set { m_data=value; SetModified(); } }
    public override DataTypeEnum DataType { get=>Traits.DataType; }
    public override DataTypeEnum ComponentType { get=>Traits.ComponentType; }
    public override string GetComponentName(int elem) { return ((KVariableEnum)elem).ToString(); }
    public override int GetComponentIndex(string elem) { return (int)KVariableTypeInfo.Aliases[elem]; }
    public override bool ElementAccessByString { get=>true; }
    public T this[int elem] { get=>m_data[elem]; set=>m_data[elem]=value; }
    public T GetComponent(int elem) { return m_data[elem]; }
    public T GetComponent(string elem) { T value; m_data.Get(elem, out value); return value; }
    public void SetComponent(int elem, T value) { m_data[elem] = value; SetModified(); }
    public void SetComponent(string elem, T value) { m_data.Set(elem, value); SetModified(); }

    public T Variable { get => m_data.m_variable; set { m_data.m_variable = value; SetModified(); } }
    public T Derivative { get => m_data.m_derivative; set { m_data.m_derivative = value; SetModified(); } }
    public T SecondDerivative { get => m_data.m_secondDerivative; set { m_data.m_secondDerivative = value; SetModified(); } }
    public T ThirdDerivative { get => m_data.m_thirdDerivative; set { m_data.m_thirdDerivative = value; SetModified(); } }
    public T AppliedForce { get => m_data.m_appliedForce; set { m_data.m_appliedForce = value; SetModified(); } }
    public T AppliedForceDerivative { get => m_data.m_appliedForceDerivative; set { m_data.m_appliedForceDerivative = value; SetModified(); } }
    public T ImpulseForce { get => m_data.m_impulseForce; set { m_data.m_impulseForce = value; SetModified(); } }
    public T ImpulseForceDerivative { get => m_data.m_impulseForceDerivative; set { m_data.m_impulseForceDerivative = value; SetModified(); } }

    public KVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<T> data = null) : base(name, parent) { m_data = data; }
    public KVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) {}
    public KVariablesExtDataObject(KVariablesExtDataObject<T> obj) : base(obj) {}
    public KVariablesExtDataObject() : base() {}
}

public class TriggerKVariablesExtDataObject : KVariablesExtDataObject<Trigger> {
    public TriggerKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Trigger> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtTrigger(); }
    public TriggerKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtTrigger(); }
    public TriggerKVariablesExtDataObject(TriggerKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtTrigger(); }
    public TriggerKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtTrigger(); }
}
public class BoolKVariablesExtDataObject : KVariablesExtDataObject<bool> {
    public BoolKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<bool> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtBool(); }
    public BoolKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtBool(); }
    public BoolKVariablesExtDataObject(BoolKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtBool(); }
    public BoolKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtBool(); }
}
public class CharKVariablesExtDataObject : KVariablesExtDataObject<char> {
    public CharKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<char> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtChar(); }
    public CharKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtChar(); }
    public CharKVariablesExtDataObject(CharKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtChar(); }
    public CharKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtChar(); }
}
public class StringKVariablesExtDataObject : KVariablesExtDataObject<string> {
    public StringKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<string> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtString(); }
    public StringKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtString(); }
    public StringKVariablesExtDataObject(StringKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtString(); }
    public StringKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtString(); }
}
public class IntKVariablesExtDataObject : KVariablesExtDataObject<int> {
    public IntKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<int> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtInt(); }
    public IntKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtInt(); }
    public IntKVariablesExtDataObject(IntKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtInt(); }
    public IntKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtInt(); }
}
public class FloatKVariablesExtDataObject : KVariablesExtDataObject<float> {
    public FloatKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<float> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtFloat(); }
    public FloatKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtFloat(); }
    public FloatKVariablesExtDataObject(FloatKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtFloat(); }
    public FloatKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtFloat(); }
}
public class Vector2IntKVariablesExtDataObject : KVariablesExtDataObject<Vector2Int> {
    public Vector2IntKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector2Int> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtVector2Int(); }
    public Vector2IntKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtVector2Int(); }
    public Vector2IntKVariablesExtDataObject(Vector2IntKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtVector2Int(); }
    public Vector2IntKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtVector2Int(); }
}
public class Vector2KVariablesExtDataObject : KVariablesExtDataObject<Vector2> {
    public Vector2KVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector2> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtVector2(); }
    public Vector2KVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtVector2(); }
    public Vector2KVariablesExtDataObject(Vector2KVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtVector2(); }
    public Vector2KVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtVector2(); }
}
public class Vector3IntKVariablesExtDataObject : KVariablesExtDataObject<Vector3Int> {
    public Vector3IntKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector3Int> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtVector3Int(); }
    public Vector3IntKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtVector3Int(); }
    public Vector3IntKVariablesExtDataObject(Vector3IntKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtVector3Int(); }
    public Vector3IntKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtVector3Int(); }
}
public class Vector3KVariablesExtDataObject : KVariablesExtDataObject<Vector3> {
    public Vector3KVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector3> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtVector3(); }
    public Vector3KVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtVector3(); }
    public Vector3KVariablesExtDataObject(Vector3KVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtVector3(); }
    public Vector3KVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtVector3(); }
}
public class Vector4KVariablesExtDataObject : KVariablesExtDataObject<Vector4> {
    public Vector4KVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Vector4> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtVector4(); }
    public Vector4KVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtVector4(); }
    public Vector4KVariablesExtDataObject(Vector4KVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtVector4(); }
    public Vector4KVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtVector4(); }
}
public class QuaternionKVariablesExtDataObject : KVariablesExtDataObject<Quaternion> {
    public QuaternionKVariablesExtDataObject(string name, IObjectRegistry parent = null, KVariablesExt<Quaternion> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesExtQuaternion(); }
    public QuaternionKVariablesExtDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesExtQuaternion(); }
    public QuaternionKVariablesExtDataObject(QuaternionKVariablesExtDataObject obj) : base(obj) { m_traits = new TraitsKVariablesExtQuaternion(); }
    public QuaternionKVariablesExtDataObject() : base() { m_traits = new TraitsKVariablesExtQuaternion(); }
}
