using UnityEngine;

public class KVariableTypeSetDataObject : DataSetObjectHeader , IDataSetObject<KVariableTypeSet, bool> {
    protected ITraits<KVariableTypeSet, bool> m_traits;
    public KVariableTypeSet m_data;
    public ITraits<KVariableTypeSet, bool> Traits { get=>m_traits; }
    public KVariableTypeSet Data { get=>m_data; set { m_data=value; SetModified(); } }
    public override DataTypeEnum DataType { get=>Traits.DataType; }
    public override DataTypeEnum ComponentType { get=>Traits.ComponentType; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return (int)KVariableTypeInfo.Aliases[elem]; }
    public override bool ElementAccessByString { get=>true; }
    public bool this[int index] {
        get=>m_data.Contains(KVariableTypeInfo.IndexToKVariableEnum(index));
        set {
            KVariableEnum kvType = KVariableTypeInfo.IndexToKVariableEnum(index);
            if (value) {
                m_data.Add(kvType);
            } else {
                m_data.Remove(kvType);
            }
        }
    }
    public bool GetComponent(int index) { return m_data.Contains(KVariableTypeInfo.IndexToKVariableEnum(index)); }
    public bool GetComponent(string elem) { return m_data.Contains(elem); }
    public void SetComponent(int index, bool value) {
        KVariableEnum kvType = KVariableTypeInfo.IndexToKVariableEnum(index);
        if (value) {
            m_data.Add(kvType);
        } else {
            m_data.Remove(kvType);
        }
        SetModified();
    }
    public void SetComponent(string elem, bool value) {
        KVariableEnum kvType = KVariableTypeInfo.StringToKVariableEnum(elem);
        if (value) {
            m_data.Add(kvType);
        } else {
            m_data.Remove(kvType);
        }
        SetModified();
    }

    // This makes it behave a bit like a KVariable... we want a KVariableTypeSet... too much to pass through, so just get the DataType and work with it.
    // public bool Variable { get => m_data.Contains(KVariableEnum.Variable); set { if (value) { m_data.Add(KVariableEnum.Variable) } else { m_data.Remove(KVariableEnum.Variable); } SetModified(); } }
    // public bool Derivative { get => m_data.Contains(KVariableEnum.Derivative); set { if (value) { m_data.Add(KVariableEnum.Derivative) } else { m_data.Remove(KVariableEnum.Derivative); } SetModified(); } }
    // public bool SecondDerivative { get => m_data.Contains(KVariableEnum.SecondDerivative); set { if (value) { m_data.Add(KVariableEnum.SecondDerivative) } else { m_data.Remove(KVariableEnum.SecondDerivative); } SetModified(); } }
    // public bool AppliedForce { get => m_data.Contains(KVariableEnum.AppliedForce); set { if (value) { m_data.Add(KVariableEnum.AppliedForce) } else { m_data.Remove(KVariableEnum.AppliedForce); } SetModified(); } }
    // public bool ImpulseForce { get => m_data.Contains(KVariableEnum.ImpulseForce); set { if (value) { m_data.Add(KVariableEnum.ImpulseForce) } else { m_data.Remove(KVariableEnum.ImpulseForce); } SetModified(); } }

    public KVariableTypeSetDataObject(string name, IObjectRegistry parent = null, KVariableTypeSet data = null) : base(name, parent) { m_data = data; }
    public KVariableTypeSetDataObject(IDataSetObjectHeader obj) : base(obj) {}
    public KVariableTypeSetDataObject(KVariableTypeSetDataObject obj) : base(obj) {}
    public KVariableTypeSetDataObject() : base() {}
}
