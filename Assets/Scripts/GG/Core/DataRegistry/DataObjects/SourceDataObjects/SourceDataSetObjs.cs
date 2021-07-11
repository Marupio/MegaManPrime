using UnityEngine;
using System.Collections.Generic;

// THIS FILE WAS CREATED AUTOMATICALLY (THE MANUAL KIND OF AUTOMATIC)
// DO NOT EDIT

public class TriggerListDataObj : SourceDataSetObj<List<Trigger>, Trigger> {
    public static readonly TraitsSimpleTriggerList m_traitsSimple = new TraitsSimpleTriggerList();
    public static readonly TraitsTriggerList m_traits = new TraitsTriggerList();
    public override ITraitsSimple<List<Trigger>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Trigger>, Trigger> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Trigger this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Trigger this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        TriggerListDataObj obj = new TriggerListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    TriggerListDataObj(string name, IObjRegistry parent = null, List<Trigger> m_data = default(List<Trigger>)) : base(name, parent, m_data) {}
    public TriggerListDataObj(TriggerListDataObj obj) : base(obj) {}
    public TriggerListDataObj() {}
}
public class BoolListDataObj : SourceDataSetObj<List<bool>, bool> {
    public static readonly TraitsSimpleBoolList m_traitsSimple = new TraitsSimpleBoolList();
    public static readonly TraitsBoolList m_traits = new TraitsBoolList();
    public override ITraitsSimple<List<bool>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<bool>, bool> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override bool this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override bool this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        BoolListDataObj obj = new BoolListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    BoolListDataObj(string name, IObjRegistry parent = null, List<bool> m_data = default(List<bool>)) : base(name, parent, m_data) {}
    public BoolListDataObj(BoolListDataObj obj) : base(obj) {}
    public BoolListDataObj() {}
}
public class CharListDataObj : SourceDataSetObj<List<char>, char> {
    public static readonly TraitsSimpleCharList m_traitsSimple = new TraitsSimpleCharList();
    public static readonly TraitsCharList m_traits = new TraitsCharList();
    public override ITraitsSimple<List<char>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<char>, char> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override char this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override char this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        CharListDataObj obj = new CharListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    CharListDataObj(string name, IObjRegistry parent = null, List<char> m_data = default(List<char>)) : base(name, parent, m_data) {}
    public CharListDataObj(CharListDataObj obj) : base(obj) {}
    public CharListDataObj() {}
}
public class StringListDataObj : SourceDataSetObj<List<string>, string> {
    public static readonly TraitsSimpleStringList m_traitsSimple = new TraitsSimpleStringList();
    public static readonly TraitsStringList m_traits = new TraitsStringList();
    public override ITraitsSimple<List<string>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<string>, string> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override string this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override string this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        StringListDataObj obj = new StringListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    StringListDataObj(string name, IObjRegistry parent = null, List<string> m_data = default(List<string>)) : base(name, parent, m_data) {}
    public StringListDataObj(StringListDataObj obj) : base(obj) {}
    public StringListDataObj() {}
}
public class IntListDataObj : SourceDataSetObj<List<int>, int> {
    public static readonly TraitsSimpleIntList m_traitsSimple = new TraitsSimpleIntList();
    public static readonly TraitsIntList m_traits = new TraitsIntList();
    public override ITraitsSimple<List<int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<int>, int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override int this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        IntListDataObj obj = new IntListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    IntListDataObj(string name, IObjRegistry parent = null, List<int> m_data = default(List<int>)) : base(name, parent, m_data) {}
    public IntListDataObj(IntListDataObj obj) : base(obj) {}
    public IntListDataObj() {}
}
public class FloatListDataObj : SourceDataSetObj<List<float>, float> {
    public static readonly TraitsSimpleFloatList m_traitsSimple = new TraitsSimpleFloatList();
    public static readonly TraitsFloatList m_traits = new TraitsFloatList();
    public override ITraitsSimple<List<float>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<float>, float> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override float this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override float this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        FloatListDataObj obj = new FloatListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    FloatListDataObj(string name, IObjRegistry parent = null, List<float> m_data = default(List<float>)) : base(name, parent, m_data) {}
    public FloatListDataObj(FloatListDataObj obj) : base(obj) {}
    public FloatListDataObj() {}
}
public class Vector2IntListDataObj : SourceDataSetObj<List<Vector2Int>, Vector2Int> {
    public static readonly TraitsSimpleVector2IntList m_traitsSimple = new TraitsSimpleVector2IntList();
    public static readonly TraitsVector2IntList m_traits = new TraitsVector2IntList();
    public override ITraitsSimple<List<Vector2Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector2Int>, Vector2Int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Vector2Int this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Vector2Int this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        Vector2IntListDataObj obj = new Vector2IntListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    Vector2IntListDataObj(string name, IObjRegistry parent = null, List<Vector2Int> m_data = default(List<Vector2Int>)) : base(name, parent, m_data) {}
    public Vector2IntListDataObj(Vector2IntListDataObj obj) : base(obj) {}
    public Vector2IntListDataObj() {}
}
public class Vector2ListDataObj : SourceDataSetObj<List<Vector2>, Vector2> {
    public static readonly TraitsSimpleVector2List m_traitsSimple = new TraitsSimpleVector2List();
    public static readonly TraitsVector2List m_traits = new TraitsVector2List();
    public override ITraitsSimple<List<Vector2>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector2>, Vector2> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Vector2 this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Vector2 this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        Vector2ListDataObj obj = new Vector2ListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    Vector2ListDataObj(string name, IObjRegistry parent = null, List<Vector2> m_data = default(List<Vector2>)) : base(name, parent, m_data) {}
    public Vector2ListDataObj(Vector2ListDataObj obj) : base(obj) {}
    public Vector2ListDataObj() {}
}
public class Vector3IntListDataObj : SourceDataSetObj<List<Vector3Int>, Vector3Int> {
    public static readonly TraitsSimpleVector3IntList m_traitsSimple = new TraitsSimpleVector3IntList();
    public static readonly TraitsVector3IntList m_traits = new TraitsVector3IntList();
    public override ITraitsSimple<List<Vector3Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector3Int>, Vector3Int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Vector3Int this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Vector3Int this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        Vector3IntListDataObj obj = new Vector3IntListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    Vector3IntListDataObj(string name, IObjRegistry parent = null, List<Vector3Int> m_data = default(List<Vector3Int>)) : base(name, parent, m_data) {}
    public Vector3IntListDataObj(Vector3IntListDataObj obj) : base(obj) {}
    public Vector3IntListDataObj() {}
}
public class Vector3ListDataObj : SourceDataSetObj<List<Vector3>, Vector3> {
    public static readonly TraitsSimpleVector3List m_traitsSimple = new TraitsSimpleVector3List();
    public static readonly TraitsVector3List m_traits = new TraitsVector3List();
    public override ITraitsSimple<List<Vector3>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector3>, Vector3> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Vector3 this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Vector3 this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        Vector3ListDataObj obj = new Vector3ListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    Vector3ListDataObj(string name, IObjRegistry parent = null, List<Vector3> m_data = default(List<Vector3>)) : base(name, parent, m_data) {}
    public Vector3ListDataObj(Vector3ListDataObj obj) : base(obj) {}
    public Vector3ListDataObj() {}
}
public class Vector4ListDataObj : SourceDataSetObj<List<Vector4>, Vector4> {
    public static readonly TraitsSimpleVector4List m_traitsSimple = new TraitsSimpleVector4List();
    public static readonly TraitsVector4List m_traits = new TraitsVector4List();
    public override ITraitsSimple<List<Vector4>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector4>, Vector4> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Vector4 this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Vector4 this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        Vector4ListDataObj obj = new Vector4ListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    Vector4ListDataObj(string name, IObjRegistry parent = null, List<Vector4> m_data = default(List<Vector4>)) : base(name, parent, m_data) {}
    public Vector4ListDataObj(Vector4ListDataObj obj) : base(obj) {}
    public Vector4ListDataObj() {}
}
public class QuaternionListDataObj : SourceDataSetObj<List<Quaternion>, Quaternion> {
    public static readonly TraitsSimpleQuaternionList m_traitsSimple = new TraitsSimpleQuaternionList();
    public static readonly TraitsQuaternionList m_traits = new TraitsQuaternionList();
    public override ITraitsSimple<List<Quaternion>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Quaternion>, Quaternion> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override int NComponents { get=>Data.Count; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override Quaternion this[int index] { get { return m_data[index]; } set { m_data[index]=value; } }
    public override Quaternion this[string elem] { get {
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } set{
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override IObj Clone(IObjRegistry parent) {
        QuaternionListDataObj obj = new QuaternionListDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    QuaternionListDataObj(string name, IObjRegistry parent = null, List<Quaternion> m_data = default(List<Quaternion>)) : base(name, parent, m_data) {}
    public QuaternionListDataObj(QuaternionListDataObj obj) : base(obj) {}
    public QuaternionListDataObj() {}
}
public class KVariablesTriggerDataObj : SourceDataSetObj<KVariables<Trigger>, Trigger> {
    public static readonly TraitsSimpleKVariablesTrigger m_traitsSimple = new TraitsSimpleKVariablesTrigger();
    public static readonly TraitsKVariablesTrigger m_traits = new TraitsKVariablesTrigger();
    public override ITraitsSimple<KVariables<Trigger>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Trigger>, Trigger> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Trigger this[int index] { get { Trigger value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Trigger this[string elem] { get { Trigger value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesTriggerDataObj obj = new KVariablesTriggerDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesTriggerDataObj(string name, IObjRegistry parent = null, KVariables<Trigger> m_data = default(KVariables<Trigger>)) : base(name, parent, m_data) {}
    public KVariablesTriggerDataObj(KVariablesTriggerDataObj obj) : base(obj) {}
    public KVariablesTriggerDataObj() {}
}
public class KVariablesBoolDataObj : SourceDataSetObj<KVariables<bool>, bool> {
    public static readonly TraitsSimpleKVariablesBool m_traitsSimple = new TraitsSimpleKVariablesBool();
    public static readonly TraitsKVariablesBool m_traits = new TraitsKVariablesBool();
    public override ITraitsSimple<KVariables<bool>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<bool>, bool> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override bool this[int index] { get { bool value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override bool this[string elem] { get { bool value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesBoolDataObj obj = new KVariablesBoolDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesBoolDataObj(string name, IObjRegistry parent = null, KVariables<bool> m_data = default(KVariables<bool>)) : base(name, parent, m_data) {}
    public KVariablesBoolDataObj(KVariablesBoolDataObj obj) : base(obj) {}
    public KVariablesBoolDataObj() {}
}
public class KVariablesCharDataObj : SourceDataSetObj<KVariables<char>, char> {
    public static readonly TraitsSimpleKVariablesChar m_traitsSimple = new TraitsSimpleKVariablesChar();
    public static readonly TraitsKVariablesChar m_traits = new TraitsKVariablesChar();
    public override ITraitsSimple<KVariables<char>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<char>, char> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override char this[int index] { get { char value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override char this[string elem] { get { char value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesCharDataObj obj = new KVariablesCharDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesCharDataObj(string name, IObjRegistry parent = null, KVariables<char> m_data = default(KVariables<char>)) : base(name, parent, m_data) {}
    public KVariablesCharDataObj(KVariablesCharDataObj obj) : base(obj) {}
    public KVariablesCharDataObj() {}
}
public class KVariablesStringDataObj : SourceDataSetObj<KVariables<string>, string> {
    public static readonly TraitsSimpleKVariablesString m_traitsSimple = new TraitsSimpleKVariablesString();
    public static readonly TraitsKVariablesString m_traits = new TraitsKVariablesString();
    public override ITraitsSimple<KVariables<string>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<string>, string> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override string this[int index] { get { string value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override string this[string elem] { get { string value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesStringDataObj obj = new KVariablesStringDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesStringDataObj(string name, IObjRegistry parent = null, KVariables<string> m_data = default(KVariables<string>)) : base(name, parent, m_data) {}
    public KVariablesStringDataObj(KVariablesStringDataObj obj) : base(obj) {}
    public KVariablesStringDataObj() {}
}
public class KVariablesIntDataObj : SourceDataSetObj<KVariables<int>, int> {
    public static readonly TraitsSimpleKVariablesInt m_traitsSimple = new TraitsSimpleKVariablesInt();
    public static readonly TraitsKVariablesInt m_traits = new TraitsKVariablesInt();
    public override ITraitsSimple<KVariables<int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<int>, int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int this[int index] { get { int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override int this[string elem] { get { int value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesIntDataObj obj = new KVariablesIntDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesIntDataObj(string name, IObjRegistry parent = null, KVariables<int> m_data = default(KVariables<int>)) : base(name, parent, m_data) {}
    public KVariablesIntDataObj(KVariablesIntDataObj obj) : base(obj) {}
    public KVariablesIntDataObj() {}
}
public class KVariablesFloatDataObj : SourceDataSetObj<KVariables<float>, float> {
    public static readonly TraitsSimpleKVariablesFloat m_traitsSimple = new TraitsSimpleKVariablesFloat();
    public static readonly TraitsKVariablesFloat m_traits = new TraitsKVariablesFloat();
    public override ITraitsSimple<KVariables<float>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<float>, float> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override float this[int index] { get { float value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override float this[string elem] { get { float value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesFloatDataObj obj = new KVariablesFloatDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesFloatDataObj(string name, IObjRegistry parent = null, KVariables<float> m_data = default(KVariables<float>)) : base(name, parent, m_data) {}
    public KVariablesFloatDataObj(KVariablesFloatDataObj obj) : base(obj) {}
    public KVariablesFloatDataObj() {}
}
public class KVariablesVector2IntDataObj : SourceDataSetObj<KVariables<Vector2Int>, Vector2Int> {
    public static readonly TraitsSimpleKVariablesVector2Int m_traitsSimple = new TraitsSimpleKVariablesVector2Int();
    public static readonly TraitsKVariablesVector2Int m_traits = new TraitsKVariablesVector2Int();
    public override ITraitsSimple<KVariables<Vector2Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector2Int>, Vector2Int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector2Int this[int index] { get { Vector2Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector2Int this[string elem] { get { Vector2Int value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesVector2IntDataObj obj = new KVariablesVector2IntDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesVector2IntDataObj(string name, IObjRegistry parent = null, KVariables<Vector2Int> m_data = default(KVariables<Vector2Int>)) : base(name, parent, m_data) {}
    public KVariablesVector2IntDataObj(KVariablesVector2IntDataObj obj) : base(obj) {}
    public KVariablesVector2IntDataObj() {}
}
public class KVariablesVector2DataObj : SourceDataSetObj<KVariables<Vector2>, Vector2> {
    public static readonly TraitsSimpleKVariablesVector2 m_traitsSimple = new TraitsSimpleKVariablesVector2();
    public static readonly TraitsKVariablesVector2 m_traits = new TraitsKVariablesVector2();
    public override ITraitsSimple<KVariables<Vector2>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector2>, Vector2> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector2 this[int index] { get { Vector2 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector2 this[string elem] { get { Vector2 value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesVector2DataObj obj = new KVariablesVector2DataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesVector2DataObj(string name, IObjRegistry parent = null, KVariables<Vector2> m_data = default(KVariables<Vector2>)) : base(name, parent, m_data) {}
    public KVariablesVector2DataObj(KVariablesVector2DataObj obj) : base(obj) {}
    public KVariablesVector2DataObj() {}
}
public class KVariablesVector3IntDataObj : SourceDataSetObj<KVariables<Vector3Int>, Vector3Int> {
    public static readonly TraitsSimpleKVariablesVector3Int m_traitsSimple = new TraitsSimpleKVariablesVector3Int();
    public static readonly TraitsKVariablesVector3Int m_traits = new TraitsKVariablesVector3Int();
    public override ITraitsSimple<KVariables<Vector3Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector3Int>, Vector3Int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector3Int this[int index] { get { Vector3Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector3Int this[string elem] { get { Vector3Int value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesVector3IntDataObj obj = new KVariablesVector3IntDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesVector3IntDataObj(string name, IObjRegistry parent = null, KVariables<Vector3Int> m_data = default(KVariables<Vector3Int>)) : base(name, parent, m_data) {}
    public KVariablesVector3IntDataObj(KVariablesVector3IntDataObj obj) : base(obj) {}
    public KVariablesVector3IntDataObj() {}
}
public class KVariablesVector3DataObj : SourceDataSetObj<KVariables<Vector3>, Vector3> {
    public static readonly TraitsSimpleKVariablesVector3 m_traitsSimple = new TraitsSimpleKVariablesVector3();
    public static readonly TraitsKVariablesVector3 m_traits = new TraitsKVariablesVector3();
    public override ITraitsSimple<KVariables<Vector3>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector3>, Vector3> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector3 this[int index] { get { Vector3 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector3 this[string elem] { get { Vector3 value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesVector3DataObj obj = new KVariablesVector3DataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesVector3DataObj(string name, IObjRegistry parent = null, KVariables<Vector3> m_data = default(KVariables<Vector3>)) : base(name, parent, m_data) {}
    public KVariablesVector3DataObj(KVariablesVector3DataObj obj) : base(obj) {}
    public KVariablesVector3DataObj() {}
}
public class KVariablesVector4DataObj : SourceDataSetObj<KVariables<Vector4>, Vector4> {
    public static readonly TraitsSimpleKVariablesVector4 m_traitsSimple = new TraitsSimpleKVariablesVector4();
    public static readonly TraitsKVariablesVector4 m_traits = new TraitsKVariablesVector4();
    public override ITraitsSimple<KVariables<Vector4>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector4>, Vector4> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector4 this[int index] { get { Vector4 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector4 this[string elem] { get { Vector4 value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesVector4DataObj obj = new KVariablesVector4DataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesVector4DataObj(string name, IObjRegistry parent = null, KVariables<Vector4> m_data = default(KVariables<Vector4>)) : base(name, parent, m_data) {}
    public KVariablesVector4DataObj(KVariablesVector4DataObj obj) : base(obj) {}
    public KVariablesVector4DataObj() {}
}
public class KVariablesQuaternionDataObj : SourceDataSetObj<KVariables<Quaternion>, Quaternion> {
    public static readonly TraitsSimpleKVariablesQuaternion m_traitsSimple = new TraitsSimpleKVariablesQuaternion();
    public static readonly TraitsKVariablesQuaternion m_traits = new TraitsKVariablesQuaternion();
    public override ITraitsSimple<KVariables<Quaternion>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Quaternion>, Quaternion> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>5; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Quaternion this[int index] { get { Quaternion value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Quaternion this[string elem] { get { Quaternion value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesQuaternionDataObj obj = new KVariablesQuaternionDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesQuaternionDataObj(string name, IObjRegistry parent = null, KVariables<Quaternion> m_data = default(KVariables<Quaternion>)) : base(name, parent, m_data) {}
    public KVariablesQuaternionDataObj(KVariablesQuaternionDataObj obj) : base(obj) {}
    public KVariablesQuaternionDataObj() {}
}
public class KVariablesExtTriggerDataObj : SourceDataSetObj<KVariablesExt<Trigger>, Trigger> {
    public static readonly TraitsSimpleKVariablesExtTrigger m_traitsSimple = new TraitsSimpleKVariablesExtTrigger();
    public static readonly TraitsKVariablesExtTrigger m_traits = new TraitsKVariablesExtTrigger();
    public override ITraitsSimple<KVariablesExt<Trigger>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Trigger>, Trigger> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Trigger this[int index] { get { Trigger value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Trigger this[string elem] { get { Trigger value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtTriggerDataObj obj = new KVariablesExtTriggerDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtTriggerDataObj(string name, IObjRegistry parent = null, KVariablesExt<Trigger> m_data = default(KVariablesExt<Trigger>)) : base(name, parent, m_data) {}
    public KVariablesExtTriggerDataObj(KVariablesExtTriggerDataObj obj) : base(obj) {}
    public KVariablesExtTriggerDataObj() {}
}
public class KVariablesExtBoolDataObj : SourceDataSetObj<KVariablesExt<bool>, bool> {
    public static readonly TraitsSimpleKVariablesExtBool m_traitsSimple = new TraitsSimpleKVariablesExtBool();
    public static readonly TraitsKVariablesExtBool m_traits = new TraitsKVariablesExtBool();
    public override ITraitsSimple<KVariablesExt<bool>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<bool>, bool> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override bool this[int index] { get { bool value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override bool this[string elem] { get { bool value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtBoolDataObj obj = new KVariablesExtBoolDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtBoolDataObj(string name, IObjRegistry parent = null, KVariablesExt<bool> m_data = default(KVariablesExt<bool>)) : base(name, parent, m_data) {}
    public KVariablesExtBoolDataObj(KVariablesExtBoolDataObj obj) : base(obj) {}
    public KVariablesExtBoolDataObj() {}
}
public class KVariablesExtCharDataObj : SourceDataSetObj<KVariablesExt<char>, char> {
    public static readonly TraitsSimpleKVariablesExtChar m_traitsSimple = new TraitsSimpleKVariablesExtChar();
    public static readonly TraitsKVariablesExtChar m_traits = new TraitsKVariablesExtChar();
    public override ITraitsSimple<KVariablesExt<char>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<char>, char> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override char this[int index] { get { char value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override char this[string elem] { get { char value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtCharDataObj obj = new KVariablesExtCharDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtCharDataObj(string name, IObjRegistry parent = null, KVariablesExt<char> m_data = default(KVariablesExt<char>)) : base(name, parent, m_data) {}
    public KVariablesExtCharDataObj(KVariablesExtCharDataObj obj) : base(obj) {}
    public KVariablesExtCharDataObj() {}
}
public class KVariablesExtStringDataObj : SourceDataSetObj<KVariablesExt<string>, string> {
    public static readonly TraitsSimpleKVariablesExtString m_traitsSimple = new TraitsSimpleKVariablesExtString();
    public static readonly TraitsKVariablesExtString m_traits = new TraitsKVariablesExtString();
    public override ITraitsSimple<KVariablesExt<string>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<string>, string> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override string this[int index] { get { string value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override string this[string elem] { get { string value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtStringDataObj obj = new KVariablesExtStringDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtStringDataObj(string name, IObjRegistry parent = null, KVariablesExt<string> m_data = default(KVariablesExt<string>)) : base(name, parent, m_data) {}
    public KVariablesExtStringDataObj(KVariablesExtStringDataObj obj) : base(obj) {}
    public KVariablesExtStringDataObj() {}
}
public class KVariablesExtIntDataObj : SourceDataSetObj<KVariablesExt<int>, int> {
    public static readonly TraitsSimpleKVariablesExtInt m_traitsSimple = new TraitsSimpleKVariablesExtInt();
    public static readonly TraitsKVariablesExtInt m_traits = new TraitsKVariablesExtInt();
    public override ITraitsSimple<KVariablesExt<int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<int>, int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int this[int index] { get { int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override int this[string elem] { get { int value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtIntDataObj obj = new KVariablesExtIntDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtIntDataObj(string name, IObjRegistry parent = null, KVariablesExt<int> m_data = default(KVariablesExt<int>)) : base(name, parent, m_data) {}
    public KVariablesExtIntDataObj(KVariablesExtIntDataObj obj) : base(obj) {}
    public KVariablesExtIntDataObj() {}
}
public class KVariablesExtFloatDataObj : SourceDataSetObj<KVariablesExt<float>, float> {
    public static readonly TraitsSimpleKVariablesExtFloat m_traitsSimple = new TraitsSimpleKVariablesExtFloat();
    public static readonly TraitsKVariablesExtFloat m_traits = new TraitsKVariablesExtFloat();
    public override ITraitsSimple<KVariablesExt<float>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<float>, float> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override float this[int index] { get { float value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override float this[string elem] { get { float value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtFloatDataObj obj = new KVariablesExtFloatDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtFloatDataObj(string name, IObjRegistry parent = null, KVariablesExt<float> m_data = default(KVariablesExt<float>)) : base(name, parent, m_data) {}
    public KVariablesExtFloatDataObj(KVariablesExtFloatDataObj obj) : base(obj) {}
    public KVariablesExtFloatDataObj() {}
}
public class KVariablesExtVector2IntDataObj : SourceDataSetObj<KVariablesExt<Vector2Int>, Vector2Int> {
    public static readonly TraitsSimpleKVariablesExtVector2Int m_traitsSimple = new TraitsSimpleKVariablesExtVector2Int();
    public static readonly TraitsKVariablesExtVector2Int m_traits = new TraitsKVariablesExtVector2Int();
    public override ITraitsSimple<KVariablesExt<Vector2Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector2Int>, Vector2Int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector2Int this[int index] { get { Vector2Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector2Int this[string elem] { get { Vector2Int value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtVector2IntDataObj obj = new KVariablesExtVector2IntDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtVector2IntDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector2Int> m_data = default(KVariablesExt<Vector2Int>)) : base(name, parent, m_data) {}
    public KVariablesExtVector2IntDataObj(KVariablesExtVector2IntDataObj obj) : base(obj) {}
    public KVariablesExtVector2IntDataObj() {}
}
public class KVariablesExtVector2DataObj : SourceDataSetObj<KVariablesExt<Vector2>, Vector2> {
    public static readonly TraitsSimpleKVariablesExtVector2 m_traitsSimple = new TraitsSimpleKVariablesExtVector2();
    public static readonly TraitsKVariablesExtVector2 m_traits = new TraitsKVariablesExtVector2();
    public override ITraitsSimple<KVariablesExt<Vector2>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector2>, Vector2> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector2 this[int index] { get { Vector2 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector2 this[string elem] { get { Vector2 value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtVector2DataObj obj = new KVariablesExtVector2DataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtVector2DataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector2> m_data = default(KVariablesExt<Vector2>)) : base(name, parent, m_data) {}
    public KVariablesExtVector2DataObj(KVariablesExtVector2DataObj obj) : base(obj) {}
    public KVariablesExtVector2DataObj() {}
}
public class KVariablesExtVector3IntDataObj : SourceDataSetObj<KVariablesExt<Vector3Int>, Vector3Int> {
    public static readonly TraitsSimpleKVariablesExtVector3Int m_traitsSimple = new TraitsSimpleKVariablesExtVector3Int();
    public static readonly TraitsKVariablesExtVector3Int m_traits = new TraitsKVariablesExtVector3Int();
    public override ITraitsSimple<KVariablesExt<Vector3Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector3Int>, Vector3Int> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector3Int this[int index] { get { Vector3Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector3Int this[string elem] { get { Vector3Int value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtVector3IntDataObj obj = new KVariablesExtVector3IntDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtVector3IntDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector3Int> m_data = default(KVariablesExt<Vector3Int>)) : base(name, parent, m_data) {}
    public KVariablesExtVector3IntDataObj(KVariablesExtVector3IntDataObj obj) : base(obj) {}
    public KVariablesExtVector3IntDataObj() {}
}
public class KVariablesExtVector3DataObj : SourceDataSetObj<KVariablesExt<Vector3>, Vector3> {
    public static readonly TraitsSimpleKVariablesExtVector3 m_traitsSimple = new TraitsSimpleKVariablesExtVector3();
    public static readonly TraitsKVariablesExtVector3 m_traits = new TraitsKVariablesExtVector3();
    public override ITraitsSimple<KVariablesExt<Vector3>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector3>, Vector3> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector3 this[int index] { get { Vector3 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector3 this[string elem] { get { Vector3 value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtVector3DataObj obj = new KVariablesExtVector3DataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtVector3DataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector3> m_data = default(KVariablesExt<Vector3>)) : base(name, parent, m_data) {}
    public KVariablesExtVector3DataObj(KVariablesExtVector3DataObj obj) : base(obj) {}
    public KVariablesExtVector3DataObj() {}
}
public class KVariablesExtVector4DataObj : SourceDataSetObj<KVariablesExt<Vector4>, Vector4> {
    public static readonly TraitsSimpleKVariablesExtVector4 m_traitsSimple = new TraitsSimpleKVariablesExtVector4();
    public static readonly TraitsKVariablesExtVector4 m_traits = new TraitsKVariablesExtVector4();
    public override ITraitsSimple<KVariablesExt<Vector4>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector4>, Vector4> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Vector4 this[int index] { get { Vector4 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Vector4 this[string elem] { get { Vector4 value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtVector4DataObj obj = new KVariablesExtVector4DataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtVector4DataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector4> m_data = default(KVariablesExt<Vector4>)) : base(name, parent, m_data) {}
    public KVariablesExtVector4DataObj(KVariablesExtVector4DataObj obj) : base(obj) {}
    public KVariablesExtVector4DataObj() {}
}
public class KVariablesExtQuaternionDataObj : SourceDataSetObj<KVariablesExt<Quaternion>, Quaternion> {
    public static readonly TraitsSimpleKVariablesExtQuaternion m_traitsSimple = new TraitsSimpleKVariablesExtQuaternion();
    public static readonly TraitsKVariablesExtQuaternion m_traits = new TraitsKVariablesExtQuaternion();
    public override ITraitsSimple<KVariablesExt<Quaternion>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Quaternion>, Quaternion> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override Quaternion this[int index] { get { Quaternion value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } set { m_data.Set(KVariableTypeInfo.IndexToKVariableEnum(index), value); } }
    public override Quaternion this[string elem] { get { Quaternion value; m_data.Get(elem, out value); return value; } set{ m_data.Set(elem,value); } }
    public override IObj Clone(IObjRegistry parent) {
        KVariablesExtQuaternionDataObj obj = new KVariablesExtQuaternionDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    KVariablesExtQuaternionDataObj(string name, IObjRegistry parent = null, KVariablesExt<Quaternion> m_data = default(KVariablesExt<Quaternion>)) : base(name, parent, m_data) {}
    public KVariablesExtQuaternionDataObj(KVariablesExtQuaternionDataObj obj) : base(obj) {}
    public KVariablesExtQuaternionDataObj() {}
}
