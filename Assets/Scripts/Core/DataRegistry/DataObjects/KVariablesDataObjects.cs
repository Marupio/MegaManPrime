using UnityEngine;

public class KVariablesDataObject<T> : DataSetObjectHeader , IDataSetObject<KVariables<T>, T> {
    protected ITraits<KVariables<T>, T> m_traits;
    public KVariables<T> m_data;
    public ITraits<KVariables<T>, T> Traits { get=>m_traits; }
    public KVariables<T> Data { get=>m_data; set { m_data=value; SetModified(); } }
    public override DataTypeEnum DataType { get=>Traits.DataType; }
    public override DataTypeEnum ComponentType { get=>Traits.ComponentType; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
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
    public T AppliedForce { get => m_data.m_appliedForce; set { m_data.m_appliedForce = value; SetModified(); } }
    public T ImpulseForce { get => m_data.m_impulseForce; set { m_data.m_impulseForce = value; SetModified(); } }

    public KVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<T> data = null) : base(name, parent) { m_data = data; }
    public KVariablesDataObject(IDataSetObjectHeader obj) : base(obj) {}
    public KVariablesDataObject(KVariablesDataObject<T> obj) : base(obj) {}
    public KVariablesDataObject() : base() {}
}

public class TriggerKVariablesDataObject : KVariablesDataObject<Trigger> {
    public TriggerKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Trigger> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesTrigger(); }
    public TriggerKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesTrigger(); }
    public TriggerKVariablesDataObject(TriggerKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesTrigger(); }
    public TriggerKVariablesDataObject() : base() { m_traits = new TraitsKVariablesTrigger(); }
}
public class BoolKVariablesDataObject : KVariablesDataObject<bool> {
    public BoolKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<bool> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesBool(); }
    public BoolKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesBool(); }
    public BoolKVariablesDataObject(BoolKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesBool(); }
    public BoolKVariablesDataObject() : base() { m_traits = new TraitsKVariablesBool(); }
}
public class CharKVariablesDataObject : KVariablesDataObject<char> {
    public CharKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<char> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesChar(); }
    public CharKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesChar(); }
    public CharKVariablesDataObject(CharKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesChar(); }
    public CharKVariablesDataObject() : base() { m_traits = new TraitsKVariablesChar(); }
}
public class StringKVariablesDataObject : KVariablesDataObject<string> {
    public StringKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<string> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesString(); }
    public StringKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesString(); }
    public StringKVariablesDataObject(StringKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesString(); }
    public StringKVariablesDataObject() : base() { m_traits = new TraitsKVariablesString(); }
}
public class IntKVariablesDataObject : KVariablesDataObject<int> {
    public IntKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<int> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesInt(); }
    public IntKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesInt(); }
    public IntKVariablesDataObject(IntKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesInt(); }
    public IntKVariablesDataObject() : base() { m_traits = new TraitsKVariablesInt(); }
}
public class FloatKVariablesDataObject : KVariablesDataObject<float> {
    public FloatKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<float> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesFloat(); }
    public FloatKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesFloat(); }
    public FloatKVariablesDataObject(FloatKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesFloat(); }
    public FloatKVariablesDataObject() : base() { m_traits = new TraitsKVariablesFloat(); }
}
public class Vector2IntKVariablesDataObject : KVariablesDataObject<Vector2Int> {
    public Vector2IntKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector2Int> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesVector2Int(); }
    public Vector2IntKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesVector2Int(); }
    public Vector2IntKVariablesDataObject(Vector2IntKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesVector2Int(); }
    public Vector2IntKVariablesDataObject() : base() { m_traits = new TraitsKVariablesVector2Int(); }
}
public class Vector2KVariablesDataObject : KVariablesDataObject<Vector2> {
    public Vector2KVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector2> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesVector2(); }
    public Vector2KVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesVector2(); }
    public Vector2KVariablesDataObject(Vector2KVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesVector2(); }
    public Vector2KVariablesDataObject() : base() { m_traits = new TraitsKVariablesVector2(); }
}
public class Vector3IntKVariablesDataObject : KVariablesDataObject<Vector3Int> {
    public Vector3IntKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector3Int> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesVector3Int(); }
    public Vector3IntKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesVector3Int(); }
    public Vector3IntKVariablesDataObject(Vector3IntKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesVector3Int(); }
    public Vector3IntKVariablesDataObject() : base() { m_traits = new TraitsKVariablesVector3Int(); }
}
public class Vector3KVariablesDataObject : KVariablesDataObject<Vector3> {
    public Vector3KVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector3> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesVector3(); }
    public Vector3KVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesVector3(); }
    public Vector3KVariablesDataObject(Vector3KVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesVector3(); }
    public Vector3KVariablesDataObject() : base() { m_traits = new TraitsKVariablesVector3(); }
}
public class Vector4KVariablesDataObject : KVariablesDataObject<Vector4> {
    public Vector4KVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Vector4> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesVector4(); }
    public Vector4KVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesVector4(); }
    public Vector4KVariablesDataObject(Vector4KVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesVector4(); }
    public Vector4KVariablesDataObject() : base() { m_traits = new TraitsKVariablesVector4(); }
}
public class QuaternionKVariablesDataObject : KVariablesDataObject<Quaternion> {
    public QuaternionKVariablesDataObject(string name, IObjectRegistry parent = null, KVariables<Quaternion> data = null) : base(name, parent, data) { m_traits = new TraitsKVariablesQuaternion(); }
    public QuaternionKVariablesDataObject(IDataSetObjectHeader obj) : base(obj) { m_traits = new TraitsKVariablesQuaternion(); }
    public QuaternionKVariablesDataObject(QuaternionKVariablesDataObject obj) : base(obj) { m_traits = new TraitsKVariablesQuaternion(); }
    public QuaternionKVariablesDataObject() : base() { m_traits = new TraitsKVariablesQuaternion(); }
}
