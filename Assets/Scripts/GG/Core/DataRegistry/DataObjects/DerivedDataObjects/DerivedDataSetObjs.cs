using System.Collections.Generic;
using UnityEngine;

// THIS IS AN AUTOMATICALLY GENERATED FILE (THE MANUAL KIND OF AUTOMATIC), DO NOT EDIT IT OR BAD THINGS
// WILL HAPPEN, THINGS THAT YOU WOULDN'T EXPECT.  YOU MIGHT FIX SOMETHING.

public class TriggerListDerivedDataObj : DerivedDataSetObj<List<Trigger>, Trigger> {
    public static readonly TraitsSimpleTriggerList m_traitsSimple = new TraitsSimpleTriggerList();
    public static readonly TraitsTriggerList m_traits = new TraitsTriggerList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Trigger>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Trigger>, Trigger> Traits { get=>m_traits; }
    public override Trigger this[int index] { get { return m_data[index]; } }
    public override Trigger this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Trigger GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Trigger GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public TriggerListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Trigger> data = default(List<Trigger>)
    ) : base(name, parent, updater, data) {}
    public TriggerListDerivedDataObj(TriggerListDerivedDataObj obj) : base(obj) {}
    public TriggerListDerivedDataObj() {}
}
public class BoolListDerivedDataObj : DerivedDataSetObj<List<bool>, bool> {
    public static readonly TraitsSimpleBoolList m_traitsSimple = new TraitsSimpleBoolList();
    public static readonly TraitsBoolList m_traits = new TraitsBoolList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<bool>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<bool>, bool> Traits { get=>m_traits; }
    public override bool this[int index] { get { return m_data[index]; } }
    public override bool this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override bool GetComponentNoUpdate(int index) { return m_data[index]; }
    public override bool GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public BoolListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<bool> data = default(List<bool>)
    ) : base(name, parent, updater, data) {}
    public BoolListDerivedDataObj(BoolListDerivedDataObj obj) : base(obj) {}
    public BoolListDerivedDataObj() {}
}
public class CharListDerivedDataObj : DerivedDataSetObj<List<char>, char> {
    public static readonly TraitsSimpleCharList m_traitsSimple = new TraitsSimpleCharList();
    public static readonly TraitsCharList m_traits = new TraitsCharList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<char>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<char>, char> Traits { get=>m_traits; }
    public override char this[int index] { get { return m_data[index]; } }
    public override char this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override char GetComponentNoUpdate(int index) { return m_data[index]; }
    public override char GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public CharListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<char> data = default(List<char>)
    ) : base(name, parent, updater, data) {}
    public CharListDerivedDataObj(CharListDerivedDataObj obj) : base(obj) {}
    public CharListDerivedDataObj() {}
}
public class StringListDerivedDataObj : DerivedDataSetObj<List<string>, string> {
    public static readonly TraitsSimpleStringList m_traitsSimple = new TraitsSimpleStringList();
    public static readonly TraitsStringList m_traits = new TraitsStringList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<string>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<string>, string> Traits { get=>m_traits; }
    public override string this[int index] { get { return m_data[index]; } }
    public override string this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override string GetComponentNoUpdate(int index) { return m_data[index]; }
    public override string GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public StringListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<string> data = default(List<string>)
    ) : base(name, parent, updater, data) {}
    public StringListDerivedDataObj(StringListDerivedDataObj obj) : base(obj) {}
    public StringListDerivedDataObj() {}
}
public class IntListDerivedDataObj : DerivedDataSetObj<List<int>, int> {
    public static readonly TraitsSimpleIntList m_traitsSimple = new TraitsSimpleIntList();
    public static readonly TraitsIntList m_traits = new TraitsIntList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<int>, int> Traits { get=>m_traits; }
    public override int this[int index] { get { return m_data[index]; } }
    public override int this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override int GetComponentNoUpdate(int index) { return m_data[index]; }
    public override int GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public IntListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<int> data = default(List<int>)
    ) : base(name, parent, updater, data) {}
    public IntListDerivedDataObj(IntListDerivedDataObj obj) : base(obj) {}
    public IntListDerivedDataObj() {}
}
public class FloatListDerivedDataObj : DerivedDataSetObj<List<float>, float> {
    public static readonly TraitsSimpleFloatList m_traitsSimple = new TraitsSimpleFloatList();
    public static readonly TraitsFloatList m_traits = new TraitsFloatList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<float>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<float>, float> Traits { get=>m_traits; }
    public override float this[int index] { get { return m_data[index]; } }
    public override float this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override float GetComponentNoUpdate(int index) { return m_data[index]; }
    public override float GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public FloatListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<float> data = default(List<float>)
    ) : base(name, parent, updater, data) {}
    public FloatListDerivedDataObj(FloatListDerivedDataObj obj) : base(obj) {}
    public FloatListDerivedDataObj() {}
}
public class Vector2IntListDerivedDataObj : DerivedDataSetObj<List<Vector2Int>, Vector2Int> {
    public static readonly TraitsSimpleVector2IntList m_traitsSimple = new TraitsSimpleVector2IntList();
    public static readonly TraitsVector2IntList m_traits = new TraitsVector2IntList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Vector2Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector2Int>, Vector2Int> Traits { get=>m_traits; }
    public override Vector2Int this[int index] { get { return m_data[index]; } }
    public override Vector2Int this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Vector2Int GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Vector2Int GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public Vector2IntListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Vector2Int> data = default(List<Vector2Int>)
    ) : base(name, parent, updater, data) {}
    public Vector2IntListDerivedDataObj(Vector2IntListDerivedDataObj obj) : base(obj) {}
    public Vector2IntListDerivedDataObj() {}
}
public class Vector2ListDerivedDataObj : DerivedDataSetObj<List<Vector2>, Vector2> {
    public static readonly TraitsSimpleVector2List m_traitsSimple = new TraitsSimpleVector2List();
    public static readonly TraitsVector2List m_traits = new TraitsVector2List();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Vector2>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector2>, Vector2> Traits { get=>m_traits; }
    public override Vector2 this[int index] { get { return m_data[index]; } }
    public override Vector2 this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Vector2 GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Vector2 GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public Vector2ListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Vector2> data = default(List<Vector2>)
    ) : base(name, parent, updater, data) {}
    public Vector2ListDerivedDataObj(Vector2ListDerivedDataObj obj) : base(obj) {}
    public Vector2ListDerivedDataObj() {}
}
public class Vector3IntListDerivedDataObj : DerivedDataSetObj<List<Vector3Int>, Vector3Int> {
    public static readonly TraitsSimpleVector3IntList m_traitsSimple = new TraitsSimpleVector3IntList();
    public static readonly TraitsVector3IntList m_traits = new TraitsVector3IntList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Vector3Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector3Int>, Vector3Int> Traits { get=>m_traits; }
    public override Vector3Int this[int index] { get { return m_data[index]; } }
    public override Vector3Int this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Vector3Int GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Vector3Int GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public Vector3IntListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Vector3Int> data = default(List<Vector3Int>)
    ) : base(name, parent, updater, data) {}
    public Vector3IntListDerivedDataObj(Vector3IntListDerivedDataObj obj) : base(obj) {}
    public Vector3IntListDerivedDataObj() {}
}
public class Vector3ListDerivedDataObj : DerivedDataSetObj<List<Vector3>, Vector3> {
    public static readonly TraitsSimpleVector3List m_traitsSimple = new TraitsSimpleVector3List();
    public static readonly TraitsVector3List m_traits = new TraitsVector3List();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Vector3>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector3>, Vector3> Traits { get=>m_traits; }
    public override Vector3 this[int index] { get { return m_data[index]; } }
    public override Vector3 this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Vector3 GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Vector3 GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public Vector3ListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Vector3> data = default(List<Vector3>)
    ) : base(name, parent, updater, data) {}
    public Vector3ListDerivedDataObj(Vector3ListDerivedDataObj obj) : base(obj) {}
    public Vector3ListDerivedDataObj() {}
}
public class Vector4ListDerivedDataObj : DerivedDataSetObj<List<Vector4>, Vector4> {
    public static readonly TraitsSimpleVector4List m_traitsSimple = new TraitsSimpleVector4List();
    public static readonly TraitsVector4List m_traits = new TraitsVector4List();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Vector4>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Vector4>, Vector4> Traits { get=>m_traits; }
    public override Vector4 this[int index] { get { return m_data[index]; } }
    public override Vector4 this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Vector4 GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Vector4 GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public Vector4ListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Vector4> data = default(List<Vector4>)
    ) : base(name, parent, updater, data) {}
    public Vector4ListDerivedDataObj(Vector4ListDerivedDataObj obj) : base(obj) {}
    public Vector4ListDerivedDataObj() {}
}
public class QuaternionListDerivedDataObj : DerivedDataSetObj<List<Quaternion>, Quaternion> {
    public static readonly TraitsSimpleQuaternionList m_traitsSimple = new TraitsSimpleQuaternionList();
    public static readonly TraitsQuaternionList m_traits = new TraitsQuaternionList();
    public override DataTypeEnum DataType { get=>DataTypeEnum.List_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.Index; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return false; }
    public override string GetComponentName(int index) { return index.ToString(); }
    public override int GetComponentIndex(string elem) { int index; if (int.TryParse(elem, out index)){return index;} return -1; }
    public override int NComponents { get=>Data.Count; }
    public override ITraitsSimple<List<Quaternion>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<List<Quaternion>, Quaternion> Traits { get=>m_traits; }
    public override Quaternion this[int index] { get { return m_data[index]; } }
    public override Quaternion this[string elem] { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     } }
    public override Quaternion GetComponentNoUpdate(int index) { return m_data[index]; }
    public override Quaternion GetComponentNoUpdate(string elem) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return null;
    #endif
     }
    public QuaternionListDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        List<Quaternion> data = default(List<Quaternion>)
    ) : base(name, parent, updater, data) {}
    public QuaternionListDerivedDataObj(QuaternionListDerivedDataObj obj) : base(obj) {}
    public QuaternionListDerivedDataObj() {}
}
public class KVariablesTriggerDerivedDataObj : DerivedDataSetObj<KVariables<Trigger>, Trigger> {
    public static readonly TraitsSimpleKVariablesTrigger m_traitsSimple = new TraitsSimpleKVariablesTrigger();
    public static readonly TraitsKVariablesTrigger m_traits = new TraitsKVariablesTrigger();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Trigger>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Trigger>, Trigger> Traits { get=>m_traits; }
    public override Trigger this[int index] { get { Trigger value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Trigger this[string elem] { get { Trigger value; m_data.Get(elem, out value); return value; } }
    public override Trigger GetComponentNoUpdate(int index) { Trigger value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Trigger GetComponentNoUpdate(string elem) { Trigger value; m_data.Get(elem, out value); return value; }
    public KVariablesTriggerDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Trigger> data = default(KVariables<Trigger>)
    ) : base(name, parent, updater, data) {}
    public KVariablesTriggerDerivedDataObj(KVariablesTriggerDerivedDataObj obj) : base(obj) {}
    public KVariablesTriggerDerivedDataObj() {}
}
public class KVariablesBoolDerivedDataObj : DerivedDataSetObj<KVariables<bool>, bool> {
    public static readonly TraitsSimpleKVariablesBool m_traitsSimple = new TraitsSimpleKVariablesBool();
    public static readonly TraitsKVariablesBool m_traits = new TraitsKVariablesBool();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<bool>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<bool>, bool> Traits { get=>m_traits; }
    public override bool this[int index] { get { bool value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override bool this[string elem] { get { bool value; m_data.Get(elem, out value); return value; } }
    public override bool GetComponentNoUpdate(int index) { bool value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override bool GetComponentNoUpdate(string elem) { bool value; m_data.Get(elem, out value); return value; }
    public KVariablesBoolDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<bool> data = default(KVariables<bool>)
    ) : base(name, parent, updater, data) {}
    public KVariablesBoolDerivedDataObj(KVariablesBoolDerivedDataObj obj) : base(obj) {}
    public KVariablesBoolDerivedDataObj() {}
}
public class KVariablesCharDerivedDataObj : DerivedDataSetObj<KVariables<char>, char> {
    public static readonly TraitsSimpleKVariablesChar m_traitsSimple = new TraitsSimpleKVariablesChar();
    public static readonly TraitsKVariablesChar m_traits = new TraitsKVariablesChar();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<char>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<char>, char> Traits { get=>m_traits; }
    public override char this[int index] { get { char value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override char this[string elem] { get { char value; m_data.Get(elem, out value); return value; } }
    public override char GetComponentNoUpdate(int index) { char value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override char GetComponentNoUpdate(string elem) { char value; m_data.Get(elem, out value); return value; }
    public KVariablesCharDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<char> data = default(KVariables<char>)
    ) : base(name, parent, updater, data) {}
    public KVariablesCharDerivedDataObj(KVariablesCharDerivedDataObj obj) : base(obj) {}
    public KVariablesCharDerivedDataObj() {}
}
public class KVariablesStringDerivedDataObj : DerivedDataSetObj<KVariables<string>, string> {
    public static readonly TraitsSimpleKVariablesString m_traitsSimple = new TraitsSimpleKVariablesString();
    public static readonly TraitsKVariablesString m_traits = new TraitsKVariablesString();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<string>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<string>, string> Traits { get=>m_traits; }
    public override string this[int index] { get { string value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override string this[string elem] { get { string value; m_data.Get(elem, out value); return value; } }
    public override string GetComponentNoUpdate(int index) { string value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override string GetComponentNoUpdate(string elem) { string value; m_data.Get(elem, out value); return value; }
    public KVariablesStringDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<string> data = default(KVariables<string>)
    ) : base(name, parent, updater, data) {}
    public KVariablesStringDerivedDataObj(KVariablesStringDerivedDataObj obj) : base(obj) {}
    public KVariablesStringDerivedDataObj() {}
}
public class KVariablesIntDerivedDataObj : DerivedDataSetObj<KVariables<int>, int> {
    public static readonly TraitsSimpleKVariablesInt m_traitsSimple = new TraitsSimpleKVariablesInt();
    public static readonly TraitsKVariablesInt m_traits = new TraitsKVariablesInt();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<int>, int> Traits { get=>m_traits; }
    public override int this[int index] { get { int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override int this[string elem] { get { int value; m_data.Get(elem, out value); return value; } }
    public override int GetComponentNoUpdate(int index) { int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override int GetComponentNoUpdate(string elem) { int value; m_data.Get(elem, out value); return value; }
    public KVariablesIntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<int> data = default(KVariables<int>)
    ) : base(name, parent, updater, data) {}
    public KVariablesIntDerivedDataObj(KVariablesIntDerivedDataObj obj) : base(obj) {}
    public KVariablesIntDerivedDataObj() {}
}
public class KVariablesFloatDerivedDataObj : DerivedDataSetObj<KVariables<float>, float> {
    public static readonly TraitsSimpleKVariablesFloat m_traitsSimple = new TraitsSimpleKVariablesFloat();
    public static readonly TraitsKVariablesFloat m_traits = new TraitsKVariablesFloat();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<float>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<float>, float> Traits { get=>m_traits; }
    public override float this[int index] { get { float value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override float this[string elem] { get { float value; m_data.Get(elem, out value); return value; } }
    public override float GetComponentNoUpdate(int index) { float value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override float GetComponentNoUpdate(string elem) { float value; m_data.Get(elem, out value); return value; }
    public KVariablesFloatDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<float> data = default(KVariables<float>)
    ) : base(name, parent, updater, data) {}
    public KVariablesFloatDerivedDataObj(KVariablesFloatDerivedDataObj obj) : base(obj) {}
    public KVariablesFloatDerivedDataObj() {}
}
public class KVariablesVector2IntDerivedDataObj : DerivedDataSetObj<KVariables<Vector2Int>, Vector2Int> {
    public static readonly TraitsSimpleKVariablesVector2Int m_traitsSimple = new TraitsSimpleKVariablesVector2Int();
    public static readonly TraitsKVariablesVector2Int m_traits = new TraitsKVariablesVector2Int();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Vector2Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector2Int>, Vector2Int> Traits { get=>m_traits; }
    public override Vector2Int this[int index] { get { Vector2Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector2Int this[string elem] { get { Vector2Int value; m_data.Get(elem, out value); return value; } }
    public override Vector2Int GetComponentNoUpdate(int index) { Vector2Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector2Int GetComponentNoUpdate(string elem) { Vector2Int value; m_data.Get(elem, out value); return value; }
    public KVariablesVector2IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Vector2Int> data = default(KVariables<Vector2Int>)
    ) : base(name, parent, updater, data) {}
    public KVariablesVector2IntDerivedDataObj(KVariablesVector2IntDerivedDataObj obj) : base(obj) {}
    public KVariablesVector2IntDerivedDataObj() {}
}
public class KVariablesVector2DerivedDataObj : DerivedDataSetObj<KVariables<Vector2>, Vector2> {
    public static readonly TraitsSimpleKVariablesVector2 m_traitsSimple = new TraitsSimpleKVariablesVector2();
    public static readonly TraitsKVariablesVector2 m_traits = new TraitsKVariablesVector2();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Vector2>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector2>, Vector2> Traits { get=>m_traits; }
    public override Vector2 this[int index] { get { Vector2 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector2 this[string elem] { get { Vector2 value; m_data.Get(elem, out value); return value; } }
    public override Vector2 GetComponentNoUpdate(int index) { Vector2 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector2 GetComponentNoUpdate(string elem) { Vector2 value; m_data.Get(elem, out value); return value; }
    public KVariablesVector2DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Vector2> data = default(KVariables<Vector2>)
    ) : base(name, parent, updater, data) {}
    public KVariablesVector2DerivedDataObj(KVariablesVector2DerivedDataObj obj) : base(obj) {}
    public KVariablesVector2DerivedDataObj() {}
}
public class KVariablesVector3IntDerivedDataObj : DerivedDataSetObj<KVariables<Vector3Int>, Vector3Int> {
    public static readonly TraitsSimpleKVariablesVector3Int m_traitsSimple = new TraitsSimpleKVariablesVector3Int();
    public static readonly TraitsKVariablesVector3Int m_traits = new TraitsKVariablesVector3Int();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Vector3Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector3Int>, Vector3Int> Traits { get=>m_traits; }
    public override Vector3Int this[int index] { get { Vector3Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector3Int this[string elem] { get { Vector3Int value; m_data.Get(elem, out value); return value; } }
    public override Vector3Int GetComponentNoUpdate(int index) { Vector3Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector3Int GetComponentNoUpdate(string elem) { Vector3Int value; m_data.Get(elem, out value); return value; }
    public KVariablesVector3IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Vector3Int> data = default(KVariables<Vector3Int>)
    ) : base(name, parent, updater, data) {}
    public KVariablesVector3IntDerivedDataObj(KVariablesVector3IntDerivedDataObj obj) : base(obj) {}
    public KVariablesVector3IntDerivedDataObj() {}
}
public class KVariablesVector3DerivedDataObj : DerivedDataSetObj<KVariables<Vector3>, Vector3> {
    public static readonly TraitsSimpleKVariablesVector3 m_traitsSimple = new TraitsSimpleKVariablesVector3();
    public static readonly TraitsKVariablesVector3 m_traits = new TraitsKVariablesVector3();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Vector3>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector3>, Vector3> Traits { get=>m_traits; }
    public override Vector3 this[int index] { get { Vector3 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector3 this[string elem] { get { Vector3 value; m_data.Get(elem, out value); return value; } }
    public override Vector3 GetComponentNoUpdate(int index) { Vector3 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector3 GetComponentNoUpdate(string elem) { Vector3 value; m_data.Get(elem, out value); return value; }
    public KVariablesVector3DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Vector3> data = default(KVariables<Vector3>)
    ) : base(name, parent, updater, data) {}
    public KVariablesVector3DerivedDataObj(KVariablesVector3DerivedDataObj obj) : base(obj) {}
    public KVariablesVector3DerivedDataObj() {}
}
public class KVariablesVector4DerivedDataObj : DerivedDataSetObj<KVariables<Vector4>, Vector4> {
    public static readonly TraitsSimpleKVariablesVector4 m_traitsSimple = new TraitsSimpleKVariablesVector4();
    public static readonly TraitsKVariablesVector4 m_traits = new TraitsKVariablesVector4();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Vector4>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Vector4>, Vector4> Traits { get=>m_traits; }
    public override Vector4 this[int index] { get { Vector4 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector4 this[string elem] { get { Vector4 value; m_data.Get(elem, out value); return value; } }
    public override Vector4 GetComponentNoUpdate(int index) { Vector4 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector4 GetComponentNoUpdate(string elem) { Vector4 value; m_data.Get(elem, out value); return value; }
    public KVariablesVector4DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Vector4> data = default(KVariables<Vector4>)
    ) : base(name, parent, updater, data) {}
    public KVariablesVector4DerivedDataObj(KVariablesVector4DerivedDataObj obj) : base(obj) {}
    public KVariablesVector4DerivedDataObj() {}
}
public class KVariablesQuaternionDerivedDataObj : DerivedDataSetObj<KVariables<Quaternion>, Quaternion> {
    public static readonly TraitsSimpleKVariablesQuaternion m_traitsSimple = new TraitsSimpleKVariablesQuaternion();
    public static readonly TraitsKVariablesQuaternion m_traits = new TraitsKVariablesQuaternion();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>5; }
    public override ITraitsSimple<KVariables<Quaternion>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariables<Quaternion>, Quaternion> Traits { get=>m_traits; }
    public override Quaternion this[int index] { get { Quaternion value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Quaternion this[string elem] { get { Quaternion value; m_data.Get(elem, out value); return value; } }
    public override Quaternion GetComponentNoUpdate(int index) { Quaternion value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Quaternion GetComponentNoUpdate(string elem) { Quaternion value; m_data.Get(elem, out value); return value; }
    public KVariablesQuaternionDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariables<Quaternion> data = default(KVariables<Quaternion>)
    ) : base(name, parent, updater, data) {}
    public KVariablesQuaternionDerivedDataObj(KVariablesQuaternionDerivedDataObj obj) : base(obj) {}
    public KVariablesQuaternionDerivedDataObj() {}
}
public class KVariablesExtTriggerDerivedDataObj : DerivedDataSetObj<KVariablesExt<Trigger>, Trigger> {
    public static readonly TraitsSimpleKVariablesExtTrigger m_traitsSimple = new TraitsSimpleKVariablesExtTrigger();
    public static readonly TraitsKVariablesExtTrigger m_traits = new TraitsKVariablesExtTrigger();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Trigger; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.TriggerType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Trigger>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Trigger>, Trigger> Traits { get=>m_traits; }
    public override Trigger this[int index] { get { Trigger value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Trigger this[string elem] { get { Trigger value; m_data.Get(elem, out value); return value; } }
    public override Trigger GetComponentNoUpdate(int index) { Trigger value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Trigger GetComponentNoUpdate(string elem) { Trigger value; m_data.Get(elem, out value); return value; }
    public KVariablesExtTriggerDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Trigger> data = default(KVariablesExt<Trigger>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtTriggerDerivedDataObj(KVariablesExtTriggerDerivedDataObj obj) : base(obj) {}
    public KVariablesExtTriggerDerivedDataObj() {}
}
public class KVariablesExtBoolDerivedDataObj : DerivedDataSetObj<KVariablesExt<bool>, bool> {
    public static readonly TraitsSimpleKVariablesExtBool m_traitsSimple = new TraitsSimpleKVariablesExtBool();
    public static readonly TraitsKVariablesExtBool m_traits = new TraitsKVariablesExtBool();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<bool>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<bool>, bool> Traits { get=>m_traits; }
    public override bool this[int index] { get { bool value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override bool this[string elem] { get { bool value; m_data.Get(elem, out value); return value; } }
    public override bool GetComponentNoUpdate(int index) { bool value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override bool GetComponentNoUpdate(string elem) { bool value; m_data.Get(elem, out value); return value; }
    public KVariablesExtBoolDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<bool> data = default(KVariablesExt<bool>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtBoolDerivedDataObj(KVariablesExtBoolDerivedDataObj obj) : base(obj) {}
    public KVariablesExtBoolDerivedDataObj() {}
}
public class KVariablesExtCharDerivedDataObj : DerivedDataSetObj<KVariablesExt<char>, char> {
    public static readonly TraitsSimpleKVariablesExtChar m_traitsSimple = new TraitsSimpleKVariablesExtChar();
    public static readonly TraitsKVariablesExtChar m_traits = new TraitsKVariablesExtChar();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Char; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Char; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<char>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<char>, char> Traits { get=>m_traits; }
    public override char this[int index] { get { char value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override char this[string elem] { get { char value; m_data.Get(elem, out value); return value; } }
    public override char GetComponentNoUpdate(int index) { char value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override char GetComponentNoUpdate(string elem) { char value; m_data.Get(elem, out value); return value; }
    public KVariablesExtCharDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<char> data = default(KVariablesExt<char>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtCharDerivedDataObj(KVariablesExtCharDerivedDataObj obj) : base(obj) {}
    public KVariablesExtCharDerivedDataObj() {}
}
public class KVariablesExtStringDerivedDataObj : DerivedDataSetObj<KVariablesExt<string>, string> {
    public static readonly TraitsSimpleKVariablesExtString m_traitsSimple = new TraitsSimpleKVariablesExtString();
    public static readonly TraitsKVariablesExtString m_traits = new TraitsKVariablesExtString();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.String; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<string>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<string>, string> Traits { get=>m_traits; }
    public override string this[int index] { get { string value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override string this[string elem] { get { string value; m_data.Get(elem, out value); return value; } }
    public override string GetComponentNoUpdate(int index) { string value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override string GetComponentNoUpdate(string elem) { string value; m_data.Get(elem, out value); return value; }
    public KVariablesExtStringDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<string> data = default(KVariablesExt<string>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtStringDerivedDataObj(KVariablesExtStringDerivedDataObj obj) : base(obj) {}
    public KVariablesExtStringDerivedDataObj() {}
}
public class KVariablesExtIntDerivedDataObj : DerivedDataSetObj<KVariablesExt<int>, int> {
    public static readonly TraitsSimpleKVariablesExtInt m_traitsSimple = new TraitsSimpleKVariablesExtInt();
    public static readonly TraitsKVariablesExtInt m_traits = new TraitsKVariablesExtInt();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Int; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<int>, int> Traits { get=>m_traits; }
    public override int this[int index] { get { int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override int this[string elem] { get { int value; m_data.Get(elem, out value); return value; } }
    public override int GetComponentNoUpdate(int index) { int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override int GetComponentNoUpdate(string elem) { int value; m_data.Get(elem, out value); return value; }
    public KVariablesExtIntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<int> data = default(KVariablesExt<int>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtIntDerivedDataObj(KVariablesExtIntDerivedDataObj obj) : base(obj) {}
    public KVariablesExtIntDerivedDataObj() {}
}
public class KVariablesExtFloatDerivedDataObj : DerivedDataSetObj<KVariablesExt<float>, float> {
    public static readonly TraitsSimpleKVariablesExtFloat m_traitsSimple = new TraitsSimpleKVariablesExtFloat();
    public static readonly TraitsKVariablesExtFloat m_traits = new TraitsKVariablesExtFloat();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Float; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<float>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<float>, float> Traits { get=>m_traits; }
    public override float this[int index] { get { float value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override float this[string elem] { get { float value; m_data.Get(elem, out value); return value; } }
    public override float GetComponentNoUpdate(int index) { float value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override float GetComponentNoUpdate(string elem) { float value; m_data.Get(elem, out value); return value; }
    public KVariablesExtFloatDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<float> data = default(KVariablesExt<float>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtFloatDerivedDataObj(KVariablesExtFloatDerivedDataObj obj) : base(obj) {}
    public KVariablesExtFloatDerivedDataObj() {}
}
public class KVariablesExtVector2IntDerivedDataObj : DerivedDataSetObj<KVariablesExt<Vector2Int>, Vector2Int> {
    public static readonly TraitsSimpleKVariablesExtVector2Int m_traitsSimple = new TraitsSimpleKVariablesExtVector2Int();
    public static readonly TraitsKVariablesExtVector2Int m_traits = new TraitsKVariablesExtVector2Int();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Vector2Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector2Int>, Vector2Int> Traits { get=>m_traits; }
    public override Vector2Int this[int index] { get { Vector2Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector2Int this[string elem] { get { Vector2Int value; m_data.Get(elem, out value); return value; } }
    public override Vector2Int GetComponentNoUpdate(int index) { Vector2Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector2Int GetComponentNoUpdate(string elem) { Vector2Int value; m_data.Get(elem, out value); return value; }
    public KVariablesExtVector2IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Vector2Int> data = default(KVariablesExt<Vector2Int>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtVector2IntDerivedDataObj(KVariablesExtVector2IntDerivedDataObj obj) : base(obj) {}
    public KVariablesExtVector2IntDerivedDataObj() {}
}
public class KVariablesExtVector2DerivedDataObj : DerivedDataSetObj<KVariablesExt<Vector2>, Vector2> {
    public static readonly TraitsSimpleKVariablesExtVector2 m_traitsSimple = new TraitsSimpleKVariablesExtVector2();
    public static readonly TraitsKVariablesExtVector2 m_traits = new TraitsKVariablesExtVector2();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector2Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Vector2>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector2>, Vector2> Traits { get=>m_traits; }
    public override Vector2 this[int index] { get { Vector2 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector2 this[string elem] { get { Vector2 value; m_data.Get(elem, out value); return value; } }
    public override Vector2 GetComponentNoUpdate(int index) { Vector2 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector2 GetComponentNoUpdate(string elem) { Vector2 value; m_data.Get(elem, out value); return value; }
    public KVariablesExtVector2DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Vector2> data = default(KVariablesExt<Vector2>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtVector2DerivedDataObj(KVariablesExtVector2DerivedDataObj obj) : base(obj) {}
    public KVariablesExtVector2DerivedDataObj() {}
}
public class KVariablesExtVector3IntDerivedDataObj : DerivedDataSetObj<KVariablesExt<Vector3Int>, Vector3Int> {
    public static readonly TraitsSimpleKVariablesExtVector3Int m_traitsSimple = new TraitsSimpleKVariablesExtVector3Int();
    public static readonly TraitsKVariablesExtVector3Int m_traits = new TraitsKVariablesExtVector3Int();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3IntType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Vector3Int>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector3Int>, Vector3Int> Traits { get=>m_traits; }
    public override Vector3Int this[int index] { get { Vector3Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector3Int this[string elem] { get { Vector3Int value; m_data.Get(elem, out value); return value; } }
    public override Vector3Int GetComponentNoUpdate(int index) { Vector3Int value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector3Int GetComponentNoUpdate(string elem) { Vector3Int value; m_data.Get(elem, out value); return value; }
    public KVariablesExtVector3IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Vector3Int> data = default(KVariablesExt<Vector3Int>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtVector3IntDerivedDataObj(KVariablesExtVector3IntDerivedDataObj obj) : base(obj) {}
    public KVariablesExtVector3IntDerivedDataObj() {}
}
public class KVariablesExtVector3DerivedDataObj : DerivedDataSetObj<KVariablesExt<Vector3>, Vector3> {
    public static readonly TraitsSimpleKVariablesExtVector3 m_traitsSimple = new TraitsSimpleKVariablesExtVector3();
    public static readonly TraitsKVariablesExtVector3 m_traits = new TraitsKVariablesExtVector3();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector3Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Vector3>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector3>, Vector3> Traits { get=>m_traits; }
    public override Vector3 this[int index] { get { Vector3 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector3 this[string elem] { get { Vector3 value; m_data.Get(elem, out value); return value; } }
    public override Vector3 GetComponentNoUpdate(int index) { Vector3 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector3 GetComponentNoUpdate(string elem) { Vector3 value; m_data.Get(elem, out value); return value; }
    public KVariablesExtVector3DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Vector3> data = default(KVariablesExt<Vector3>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtVector3DerivedDataObj(KVariablesExtVector3DerivedDataObj obj) : base(obj) {}
    public KVariablesExtVector3DerivedDataObj() {}
}
public class KVariablesExtVector4DerivedDataObj : DerivedDataSetObj<KVariablesExt<Vector4>, Vector4> {
    public static readonly TraitsSimpleKVariablesExtVector4 m_traitsSimple = new TraitsSimpleKVariablesExtVector4();
    public static readonly TraitsKVariablesExtVector4 m_traits = new TraitsKVariablesExtVector4();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector4; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Vector4Type; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Vector4>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Vector4>, Vector4> Traits { get=>m_traits; }
    public override Vector4 this[int index] { get { Vector4 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Vector4 this[string elem] { get { Vector4 value; m_data.Get(elem, out value); return value; } }
    public override Vector4 GetComponentNoUpdate(int index) { Vector4 value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Vector4 GetComponentNoUpdate(string elem) { Vector4 value; m_data.Get(elem, out value); return value; }
    public KVariablesExtVector4DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Vector4> data = default(KVariablesExt<Vector4>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtVector4DerivedDataObj(KVariablesExtVector4DerivedDataObj obj) : base(obj) {}
    public KVariablesExtVector4DerivedDataObj() {}
}
public class KVariablesExtQuaternionDerivedDataObj : DerivedDataSetObj<KVariablesExt<Quaternion>, Quaternion> {
    public static readonly TraitsSimpleKVariablesExtQuaternion m_traitsSimple = new TraitsSimpleKVariablesExtQuaternion();
    public static readonly TraitsKVariablesExtQuaternion m_traits = new TraitsKVariablesExtQuaternion();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.QuaternionType; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariablesExt<Quaternion>> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariablesExt<Quaternion>, Quaternion> Traits { get=>m_traits; }
    public override Quaternion this[int index] { get { Quaternion value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; } }
    public override Quaternion this[string elem] { get { Quaternion value; m_data.Get(elem, out value); return value; } }
    public override Quaternion GetComponentNoUpdate(int index) { Quaternion value; m_data.Get(KVariableTypeInfo.IndexToKVariableEnum(index), out value);return value; }
    public override Quaternion GetComponentNoUpdate(string elem) { Quaternion value; m_data.Get(elem, out value); return value; }
    public KVariablesExtQuaternionDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariablesExt<Quaternion> data = default(KVariablesExt<Quaternion>)
    ) : base(name, parent, updater, data) {}
    public KVariablesExtQuaternionDerivedDataObj(KVariablesExtQuaternionDerivedDataObj obj) : base(obj) {}
    public KVariablesExtQuaternionDerivedDataObj() {}
}
public class KVariableTypeSetDerivedDataObj : DerivedDataSetObj<KVariableTypeSet, bool> {
    public static readonly TraitsSimpleKVariableTypeSet m_traitsSimple = new TraitsSimpleKVariableTypeSet();
    public static readonly TraitsKVariableTypeSet m_traits = new TraitsKVariableTypeSet();
    public override DataTypeEnum DataType { get=>DataTypeEnum.KVariableTypeSetType; }
    public override DataTypeEnum ComponentType { get=>DataTypeEnum.Bool; }
    public override ComponentAccessType PreferredAccessType { get=>ComponentAccessType.String; }
    public override bool ElementAccessByIndex() { return true; }
    public override bool ElementAccessByString() { return true; }
    public override string GetComponentName(int index) { return KVariableTypeInfo.IndexToKVariableEnum(index).ToString(); }
    public override int GetComponentIndex(string elem) { return KVariableTypeInfo.KVariableEnumToIndex(KVariableTypeInfo.Aliases[elem]); }
    public override int NComponents { get=>8; }
    public override ITraitsSimple<KVariableTypeSet> TraitsSimple { get=>m_traitsSimple; }
    public override ITraits<KVariableTypeSet, bool> Traits { get=>m_traits; }
    public override bool this[int index] { get { return m_data.Contains(KVariableTypeInfo.IndexToKVariableEnum(index)); } }
    public override bool this[string elem] { get { return m_data.Contains(elem); } }
    public override bool GetComponentNoUpdate(int index) { return m_data.Contains(KVariableTypeInfo.IndexToKVariableEnum(index)); }
    public override bool GetComponentNoUpdate(string elem) { return m_data.Contains(elem); }
    public KVariableTypeSetDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        KVariableTypeSet data = default(KVariableTypeSet)
    ) : base(name, parent, updater, data) {}
    public KVariableTypeSetDerivedDataObj(KVariableTypeSetDerivedDataObj obj) : base(obj) {}
    public KVariableTypeSetDerivedDataObj() {}
}
