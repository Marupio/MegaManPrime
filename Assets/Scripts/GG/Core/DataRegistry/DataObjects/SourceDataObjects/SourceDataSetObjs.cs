using UnityEngine;
using System.Collections.Generic;

// THIS FILE WAS CREATED AUTOMATICALLY (THE MANUAL KIND OF AUTOMATIC)
// DO NOT EDIT

public class TriggerListSourceDataObj : SourceDataSetObj<List<Trigger>, Trigger> {
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
        TriggerListSourceDataObj obj = new TriggerListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public TriggerListSourceDataObj(string name, IObjRegistry parent = null, List<Trigger> m_data = default(List<Trigger>)) : base(name, parent, m_data) {}
    public TriggerListSourceDataObj(TriggerListSourceDataObj obj) : base(obj) {}
    public TriggerListSourceDataObj() {}
}
public class BoolListSourceDataObj : SourceDataSetObj<List<bool>, bool> {
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
        BoolListSourceDataObj obj = new BoolListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public BoolListSourceDataObj(string name, IObjRegistry parent = null, List<bool> m_data = default(List<bool>)) : base(name, parent, m_data) {}
    public BoolListSourceDataObj(BoolListSourceDataObj obj) : base(obj) {}
    public BoolListSourceDataObj() {}
}
public class CharListSourceDataObj : SourceDataSetObj<List<char>, char> {
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
        CharListSourceDataObj obj = new CharListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public CharListSourceDataObj(string name, IObjRegistry parent = null, List<char> m_data = default(List<char>)) : base(name, parent, m_data) {}
    public CharListSourceDataObj(CharListSourceDataObj obj) : base(obj) {}
    public CharListSourceDataObj() {}
}
public class StringListSourceDataObj : SourceDataSetObj<List<string>, string> {
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
        StringListSourceDataObj obj = new StringListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public StringListSourceDataObj(string name, IObjRegistry parent = null, List<string> m_data = default(List<string>)) : base(name, parent, m_data) {}
    public StringListSourceDataObj(StringListSourceDataObj obj) : base(obj) {}
    public StringListSourceDataObj() {}
}
public class IntListSourceDataObj : SourceDataSetObj<List<int>, int> {
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
        IntListSourceDataObj obj = new IntListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public IntListSourceDataObj(string name, IObjRegistry parent = null, List<int> m_data = default(List<int>)) : base(name, parent, m_data) {}
    public IntListSourceDataObj(IntListSourceDataObj obj) : base(obj) {}
    public IntListSourceDataObj() {}
}
public class FloatListSourceDataObj : SourceDataSetObj<List<float>, float> {
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
        FloatListSourceDataObj obj = new FloatListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public FloatListSourceDataObj(string name, IObjRegistry parent = null, List<float> m_data = default(List<float>)) : base(name, parent, m_data) {}
    public FloatListSourceDataObj(FloatListSourceDataObj obj) : base(obj) {}
    public FloatListSourceDataObj() {}
}
public class Vector2IntListSourceDataObj : SourceDataSetObj<List<Vector2Int>, Vector2Int> {
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
        Vector2IntListSourceDataObj obj = new Vector2IntListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public Vector2IntListSourceDataObj(string name, IObjRegistry parent = null, List<Vector2Int> m_data = default(List<Vector2Int>)) : base(name, parent, m_data) {}
    public Vector2IntListSourceDataObj(Vector2IntListSourceDataObj obj) : base(obj) {}
    public Vector2IntListSourceDataObj() {}
}
public class Vector2ListSourceDataObj : SourceDataSetObj<List<Vector2>, Vector2> {
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
        Vector2ListSourceDataObj obj = new Vector2ListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public Vector2ListSourceDataObj(string name, IObjRegistry parent = null, List<Vector2> m_data = default(List<Vector2>)) : base(name, parent, m_data) {}
    public Vector2ListSourceDataObj(Vector2ListSourceDataObj obj) : base(obj) {}
    public Vector2ListSourceDataObj() {}
}
public class Vector3IntListSourceDataObj : SourceDataSetObj<List<Vector3Int>, Vector3Int> {
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
        Vector3IntListSourceDataObj obj = new Vector3IntListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public Vector3IntListSourceDataObj(string name, IObjRegistry parent = null, List<Vector3Int> m_data = default(List<Vector3Int>)) : base(name, parent, m_data) {}
    public Vector3IntListSourceDataObj(Vector3IntListSourceDataObj obj) : base(obj) {}
    public Vector3IntListSourceDataObj() {}
}
public class Vector3ListSourceDataObj : SourceDataSetObj<List<Vector3>, Vector3> {
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
        Vector3ListSourceDataObj obj = new Vector3ListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public Vector3ListSourceDataObj(string name, IObjRegistry parent = null, List<Vector3> m_data = default(List<Vector3>)) : base(name, parent, m_data) {}
    public Vector3ListSourceDataObj(Vector3ListSourceDataObj obj) : base(obj) {}
    public Vector3ListSourceDataObj() {}
}
public class Vector4ListSourceDataObj : SourceDataSetObj<List<Vector4>, Vector4> {
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
        Vector4ListSourceDataObj obj = new Vector4ListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public Vector4ListSourceDataObj(string name, IObjRegistry parent = null, List<Vector4> m_data = default(List<Vector4>)) : base(name, parent, m_data) {}
    public Vector4ListSourceDataObj(Vector4ListSourceDataObj obj) : base(obj) {}
    public Vector4ListSourceDataObj() {}
}
public class QuaternionListSourceDataObj : SourceDataSetObj<List<Quaternion>, Quaternion> {
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
        QuaternionListSourceDataObj obj = new QuaternionListSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public QuaternionListSourceDataObj(string name, IObjRegistry parent = null, List<Quaternion> m_data = default(List<Quaternion>)) : base(name, parent, m_data) {}
    public QuaternionListSourceDataObj(QuaternionListSourceDataObj obj) : base(obj) {}
    public QuaternionListSourceDataObj() {}
}
public class KVariablesTriggerSourceDataObj : SourceDataSetObj<KVariables<Trigger>, Trigger> {
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
        KVariablesTriggerSourceDataObj obj = new KVariablesTriggerSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesTriggerSourceDataObj(string name, IObjRegistry parent = null, KVariables<Trigger> m_data = default(KVariables<Trigger>)) : base(name, parent, m_data) {}
    public KVariablesTriggerSourceDataObj(KVariablesTriggerSourceDataObj obj) : base(obj) {}
    public KVariablesTriggerSourceDataObj() {}
}
public class KVariablesBoolSourceDataObj : SourceDataSetObj<KVariables<bool>, bool> {
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
        KVariablesBoolSourceDataObj obj = new KVariablesBoolSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesBoolSourceDataObj(string name, IObjRegistry parent = null, KVariables<bool> m_data = default(KVariables<bool>)) : base(name, parent, m_data) {}
    public KVariablesBoolSourceDataObj(KVariablesBoolSourceDataObj obj) : base(obj) {}
    public KVariablesBoolSourceDataObj() {}
}
public class KVariablesCharSourceDataObj : SourceDataSetObj<KVariables<char>, char> {
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
        KVariablesCharSourceDataObj obj = new KVariablesCharSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesCharSourceDataObj(string name, IObjRegistry parent = null, KVariables<char> m_data = default(KVariables<char>)) : base(name, parent, m_data) {}
    public KVariablesCharSourceDataObj(KVariablesCharSourceDataObj obj) : base(obj) {}
    public KVariablesCharSourceDataObj() {}
}
public class KVariablesStringSourceDataObj : SourceDataSetObj<KVariables<string>, string> {
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
        KVariablesStringSourceDataObj obj = new KVariablesStringSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesStringSourceDataObj(string name, IObjRegistry parent = null, KVariables<string> m_data = default(KVariables<string>)) : base(name, parent, m_data) {}
    public KVariablesStringSourceDataObj(KVariablesStringSourceDataObj obj) : base(obj) {}
    public KVariablesStringSourceDataObj() {}
}
public class KVariablesIntSourceDataObj : SourceDataSetObj<KVariables<int>, int> {
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
        KVariablesIntSourceDataObj obj = new KVariablesIntSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesIntSourceDataObj(string name, IObjRegistry parent = null, KVariables<int> m_data = default(KVariables<int>)) : base(name, parent, m_data) {}
    public KVariablesIntSourceDataObj(KVariablesIntSourceDataObj obj) : base(obj) {}
    public KVariablesIntSourceDataObj() {}
}
public class KVariablesFloatSourceDataObj : SourceDataSetObj<KVariables<float>, float> {
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
        KVariablesFloatSourceDataObj obj = new KVariablesFloatSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesFloatSourceDataObj(string name, IObjRegistry parent = null, KVariables<float> m_data = default(KVariables<float>)) : base(name, parent, m_data) {}
    public KVariablesFloatSourceDataObj(KVariablesFloatSourceDataObj obj) : base(obj) {}
    public KVariablesFloatSourceDataObj() {}
}
public class KVariablesVector2IntSourceDataObj : SourceDataSetObj<KVariables<Vector2Int>, Vector2Int> {
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
        KVariablesVector2IntSourceDataObj obj = new KVariablesVector2IntSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesVector2IntSourceDataObj(string name, IObjRegistry parent = null, KVariables<Vector2Int> m_data = default(KVariables<Vector2Int>)) : base(name, parent, m_data) {}
    public KVariablesVector2IntSourceDataObj(KVariablesVector2IntSourceDataObj obj) : base(obj) {}
    public KVariablesVector2IntSourceDataObj() {}
}
public class KVariablesVector2SourceDataObj : SourceDataSetObj<KVariables<Vector2>, Vector2> {
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
        KVariablesVector2SourceDataObj obj = new KVariablesVector2SourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesVector2SourceDataObj(string name, IObjRegistry parent = null, KVariables<Vector2> m_data = default(KVariables<Vector2>)) : base(name, parent, m_data) {}
    public KVariablesVector2SourceDataObj(KVariablesVector2SourceDataObj obj) : base(obj) {}
    public KVariablesVector2SourceDataObj() {}
}
public class KVariablesVector3IntSourceDataObj : SourceDataSetObj<KVariables<Vector3Int>, Vector3Int> {
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
        KVariablesVector3IntSourceDataObj obj = new KVariablesVector3IntSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesVector3IntSourceDataObj(string name, IObjRegistry parent = null, KVariables<Vector3Int> m_data = default(KVariables<Vector3Int>)) : base(name, parent, m_data) {}
    public KVariablesVector3IntSourceDataObj(KVariablesVector3IntSourceDataObj obj) : base(obj) {}
    public KVariablesVector3IntSourceDataObj() {}
}
public class KVariablesVector3SourceDataObj : SourceDataSetObj<KVariables<Vector3>, Vector3> {
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
        KVariablesVector3SourceDataObj obj = new KVariablesVector3SourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesVector3SourceDataObj(string name, IObjRegistry parent = null, KVariables<Vector3> m_data = default(KVariables<Vector3>)) : base(name, parent, m_data) {}
    public KVariablesVector3SourceDataObj(KVariablesVector3SourceDataObj obj) : base(obj) {}
    public KVariablesVector3SourceDataObj() {}
}
public class KVariablesVector4SourceDataObj : SourceDataSetObj<KVariables<Vector4>, Vector4> {
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
        KVariablesVector4SourceDataObj obj = new KVariablesVector4SourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesVector4SourceDataObj(string name, IObjRegistry parent = null, KVariables<Vector4> m_data = default(KVariables<Vector4>)) : base(name, parent, m_data) {}
    public KVariablesVector4SourceDataObj(KVariablesVector4SourceDataObj obj) : base(obj) {}
    public KVariablesVector4SourceDataObj() {}
}
public class KVariablesQuaternionSourceDataObj : SourceDataSetObj<KVariables<Quaternion>, Quaternion> {
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
        KVariablesQuaternionSourceDataObj obj = new KVariablesQuaternionSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesQuaternionSourceDataObj(string name, IObjRegistry parent = null, KVariables<Quaternion> m_data = default(KVariables<Quaternion>)) : base(name, parent, m_data) {}
    public KVariablesQuaternionSourceDataObj(KVariablesQuaternionSourceDataObj obj) : base(obj) {}
    public KVariablesQuaternionSourceDataObj() {}
}
public class KVariablesExtTriggerSourceDataObj : SourceDataSetObj<KVariablesExt<Trigger>, Trigger> {
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
        KVariablesExtTriggerSourceDataObj obj = new KVariablesExtTriggerSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtTriggerSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Trigger> m_data = default(KVariablesExt<Trigger>)) : base(name, parent, m_data) {}
    public KVariablesExtTriggerSourceDataObj(KVariablesExtTriggerSourceDataObj obj) : base(obj) {}
    public KVariablesExtTriggerSourceDataObj() {}
}
public class KVariablesExtBoolSourceDataObj : SourceDataSetObj<KVariablesExt<bool>, bool> {
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
        KVariablesExtBoolSourceDataObj obj = new KVariablesExtBoolSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtBoolSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<bool> m_data = default(KVariablesExt<bool>)) : base(name, parent, m_data) {}
    public KVariablesExtBoolSourceDataObj(KVariablesExtBoolSourceDataObj obj) : base(obj) {}
    public KVariablesExtBoolSourceDataObj() {}
}
public class KVariablesExtCharSourceDataObj : SourceDataSetObj<KVariablesExt<char>, char> {
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
        KVariablesExtCharSourceDataObj obj = new KVariablesExtCharSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtCharSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<char> m_data = default(KVariablesExt<char>)) : base(name, parent, m_data) {}
    public KVariablesExtCharSourceDataObj(KVariablesExtCharSourceDataObj obj) : base(obj) {}
    public KVariablesExtCharSourceDataObj() {}
}
public class KVariablesExtStringSourceDataObj : SourceDataSetObj<KVariablesExt<string>, string> {
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
        KVariablesExtStringSourceDataObj obj = new KVariablesExtStringSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtStringSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<string> m_data = default(KVariablesExt<string>)) : base(name, parent, m_data) {}
    public KVariablesExtStringSourceDataObj(KVariablesExtStringSourceDataObj obj) : base(obj) {}
    public KVariablesExtStringSourceDataObj() {}
}
public class KVariablesExtIntSourceDataObj : SourceDataSetObj<KVariablesExt<int>, int> {
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
        KVariablesExtIntSourceDataObj obj = new KVariablesExtIntSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtIntSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<int> m_data = default(KVariablesExt<int>)) : base(name, parent, m_data) {}
    public KVariablesExtIntSourceDataObj(KVariablesExtIntSourceDataObj obj) : base(obj) {}
    public KVariablesExtIntSourceDataObj() {}
}
public class KVariablesExtFloatSourceDataObj : SourceDataSetObj<KVariablesExt<float>, float> {
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
        KVariablesExtFloatSourceDataObj obj = new KVariablesExtFloatSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtFloatSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<float> m_data = default(KVariablesExt<float>)) : base(name, parent, m_data) {}
    public KVariablesExtFloatSourceDataObj(KVariablesExtFloatSourceDataObj obj) : base(obj) {}
    public KVariablesExtFloatSourceDataObj() {}
}
public class KVariablesExtVector2IntSourceDataObj : SourceDataSetObj<KVariablesExt<Vector2Int>, Vector2Int> {
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
        KVariablesExtVector2IntSourceDataObj obj = new KVariablesExtVector2IntSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtVector2IntSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector2Int> m_data = default(KVariablesExt<Vector2Int>)) : base(name, parent, m_data) {}
    public KVariablesExtVector2IntSourceDataObj(KVariablesExtVector2IntSourceDataObj obj) : base(obj) {}
    public KVariablesExtVector2IntSourceDataObj() {}
}
public class KVariablesExtVector2SourceDataObj : SourceDataSetObj<KVariablesExt<Vector2>, Vector2> {
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
        KVariablesExtVector2SourceDataObj obj = new KVariablesExtVector2SourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtVector2SourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector2> m_data = default(KVariablesExt<Vector2>)) : base(name, parent, m_data) {}
    public KVariablesExtVector2SourceDataObj(KVariablesExtVector2SourceDataObj obj) : base(obj) {}
    public KVariablesExtVector2SourceDataObj() {}
}
public class KVariablesExtVector3IntSourceDataObj : SourceDataSetObj<KVariablesExt<Vector3Int>, Vector3Int> {
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
        KVariablesExtVector3IntSourceDataObj obj = new KVariablesExtVector3IntSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtVector3IntSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector3Int> m_data = default(KVariablesExt<Vector3Int>)) : base(name, parent, m_data) {}
    public KVariablesExtVector3IntSourceDataObj(KVariablesExtVector3IntSourceDataObj obj) : base(obj) {}
    public KVariablesExtVector3IntSourceDataObj() {}
}
public class KVariablesExtVector3SourceDataObj : SourceDataSetObj<KVariablesExt<Vector3>, Vector3> {
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
        KVariablesExtVector3SourceDataObj obj = new KVariablesExtVector3SourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtVector3SourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector3> m_data = default(KVariablesExt<Vector3>)) : base(name, parent, m_data) {}
    public KVariablesExtVector3SourceDataObj(KVariablesExtVector3SourceDataObj obj) : base(obj) {}
    public KVariablesExtVector3SourceDataObj() {}
}
public class KVariablesExtVector4SourceDataObj : SourceDataSetObj<KVariablesExt<Vector4>, Vector4> {
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
        KVariablesExtVector4SourceDataObj obj = new KVariablesExtVector4SourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtVector4SourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Vector4> m_data = default(KVariablesExt<Vector4>)) : base(name, parent, m_data) {}
    public KVariablesExtVector4SourceDataObj(KVariablesExtVector4SourceDataObj obj) : base(obj) {}
    public KVariablesExtVector4SourceDataObj() {}
}
public class KVariablesExtQuaternionSourceDataObj : SourceDataSetObj<KVariablesExt<Quaternion>, Quaternion> {
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
        KVariablesExtQuaternionSourceDataObj obj = new KVariablesExtQuaternionSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariablesExtQuaternionSourceDataObj(string name, IObjRegistry parent = null, KVariablesExt<Quaternion> m_data = default(KVariablesExt<Quaternion>)) : base(name, parent, m_data) {}
    public KVariablesExtQuaternionSourceDataObj(KVariablesExtQuaternionSourceDataObj obj) : base(obj) {}
    public KVariablesExtQuaternionSourceDataObj() {}
}
public class KVariableTypeSetSourceDataObj : SourceDataSetObj<KVariableTypeSet, bool> {
    public static readonly TraitsSimpleKVariableTypeSet m_traitsSimple = new TraitsSimpleKVariableTypeSet();
    public static readonly TraitsKVariableTypeSet m_traits = new TraitsKVariableTypeSet();
    public override ITraitsSimple<KVariableTypeSet> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariableTypeSet, bool> Traits { get=>m_traits; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariableTypeSetType; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override int NComponents { get=>8; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override bool this[int index] { get { return m_data.Contains(KVariableTypeInfo.IndexToKVariableEnum(index)); } set {if (value){m_data.Add(KVariableTypeInfo.IndexToKVariableEnum(index));} else {m_data.Remove(KVariableTypeInfo.IndexToKVariableEnum(index));} } }
    public override bool this[string elem] { get { return m_data.Contains(elem); } set{ if (value){m_data.Add(elem);}else{m_data.Remove(elem);} } }
    public override IObj Clone(IObjRegistry parent) {
        KVariableTypeSetSourceDataObj obj = new KVariableTypeSetSourceDataObj(m_name, parent, m_data);
        return (IObj)obj;
    }
    public KVariableTypeSetSourceDataObj(string name, IObjRegistry parent = null, KVariableTypeSet m_data = default(KVariableTypeSet)) : base(name, parent, m_data) {}
    public KVariableTypeSetSourceDataObj(KVariableTypeSetSourceDataObj obj) : base(obj) {}
    public KVariableTypeSetSourceDataObj() {}
}
