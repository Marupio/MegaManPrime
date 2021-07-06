public class KVariableTypeSetDataObject : DataSetObjectHeader , IDataSetObject<KVariableTypeSet, bool> {
    protected ITraits<KVariables<T>, T> m_traits;
    public KVariables<T> m_data;
    public ITraits<KVariables<T>, T> Traits { get=>m_traits; }
    public KVariables<T> Data { get=>m_data; set { m_data=value; SetModified(); } }
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
    public T AppliedForce { get => m_data.m_appliedForce; set { m_data.m_appliedForce = value; SetModified(); } }
    public T ImpulseForce { get => m_data.m_impulseForce; set { m_data.m_impulseForce = value; SetModified(); } }

    public KVariableTypeSetDataObject(string name, IObjectRegistry parent = null, KVariables<T> data = null) : base(name, parent) { m_data = data; }
    public KVariableTypeSetDataObject(IDataSetObjectHeader obj) : base(obj) {}
    public KVariableTypeSetDataObject(KVariableTypeSetDataObject obj) : base(obj) {}
    public KVariableTypeSetDataObject() : base() {}
}
