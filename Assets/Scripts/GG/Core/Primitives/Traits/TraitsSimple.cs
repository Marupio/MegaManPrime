using System.Collections.Generic;
using UnityEngine;

public class TraitsSimpleNone : ITraitsSimple<object> {
    public DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public bool TestEquals(object lhs, object rhs) { EqualityComparer<object> ec = EqualityComparer<object>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref object lhs, object rhs) { /* do nothing */; }
    public object Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public object PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleBool : ITraitsSimple<bool> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public bool TestEquals(bool lhs, bool rhs) { EqualityComparer<bool> ec = EqualityComparer<bool>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref bool lhs, bool rhs) { lhs = rhs; }
    public bool Zero { get=>false; }
    public bool HasInfinity { get=>false; }
    public bool PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleTrigger : ITraitsSimple<Trigger> {
    public DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public bool TestEquals(Trigger lhs, Trigger rhs) { EqualityComparer<Trigger> ec = EqualityComparer<Trigger>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Trigger lhs, Trigger rhs) { lhs = rhs; }
    public Trigger Zero { get=>Trigger.Default; }
    public bool HasInfinity { get=>false; }
    public Trigger PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleChar : ITraitsSimple<char> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public bool TestEquals(char lhs, char rhs) { EqualityComparer<char> ec = EqualityComparer<char>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref char lhs, char rhs) { lhs = rhs; }
    public char Zero { get=>default(char); }
    public bool HasInfinity { get=>false; }
    public char PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleString : ITraitsSimple<string> {
    public DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public bool TestEquals(string lhs, string rhs) { EqualityComparer<string> ec = EqualityComparer<string>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref string lhs, string rhs) { lhs = rhs; }
    public string Zero { get=>default(string); }
    public bool HasInfinity { get=>false; }
    public string PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleInt : ITraitsSimple<int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public bool TestEquals(int lhs, int rhs) { EqualityComparer<int> ec = EqualityComparer<int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref int lhs, int rhs) { lhs = rhs; }
    public int Zero { get=>0; }
    public bool HasInfinity { get=>false; }
    public int PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleFloat : ITraitsSimple<float> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public bool TestEquals(float lhs, float rhs) { EqualityComparer<float> ec = EqualityComparer<float>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref float lhs, float rhs) { lhs = rhs; }
    public float Zero { get=>0f; }
    public bool HasInfinity { get=>true; }
    public float PositiveInfinity { get=>float.PositiveInfinity; }
}
public class TraitsSimpleVector2Int : ITraitsSimple<Vector2Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public bool TestEquals(Vector2Int lhs, Vector2Int rhs) { EqualityComparer<Vector2Int> ec = EqualityComparer<Vector2Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector2Int lhs, Vector2Int rhs) { lhs = rhs; }
    public Vector2Int Zero { get=>Vector2Int.zero; }
    public bool HasInfinity { get=>false; }
    public Vector2Int PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleVector2 : ITraitsSimple<Vector2> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public bool TestEquals(Vector2 lhs, Vector2 rhs) { EqualityComparer<Vector2> ec = EqualityComparer<Vector2>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector2 lhs, Vector2 rhs) { lhs = rhs; }
    public Vector2 Zero { get=>Vector2.zero; }
    public bool HasInfinity { get=>true; }
    public Vector2 PositiveInfinity { get=>Vector2.positiveInfinity; }
}
public class TraitsSimpleVector3Int : ITraitsSimple<Vector3Int> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public bool TestEquals(Vector3Int lhs, Vector3Int rhs) { EqualityComparer<Vector3Int> ec = EqualityComparer<Vector3Int>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector3Int lhs, Vector3Int rhs) { lhs = rhs; }
    public Vector3Int Zero { get=>Vector3Int.zero; }
    public bool HasInfinity { get=>false; }
    public Vector3Int PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleVector3 : ITraitsSimple<Vector3> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public bool TestEquals(Vector3 lhs, Vector3 rhs) { EqualityComparer<Vector3> ec = EqualityComparer<Vector3>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector3 lhs, Vector3 rhs) { lhs = rhs; }
    public Vector3 Zero { get=>Vector3.zero; }
    public bool HasInfinity { get=>true; }
    public Vector3 PositiveInfinity { get=>Vector3.positiveInfinity; }
}
public class TraitsSimpleVector4 : ITraitsSimple<Vector4> {
    public DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public bool TestEquals(Vector4 lhs, Vector4 rhs) { EqualityComparer<Vector4> ec = EqualityComparer<Vector4>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Vector4 lhs, Vector4 rhs) { lhs = rhs; }
    public Vector4 Zero { get=>Vector4.zero; }
    public bool HasInfinity { get=>true; }
    public Vector4 PositiveInfinity { get=>Vector4.positiveInfinity; }
}
public class TraitsSimpleQuaternion : ITraitsSimple<Quaternion> {
    public DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public bool TestEquals(Quaternion lhs, Quaternion rhs) { EqualityComparer<Quaternion> ec = EqualityComparer<Quaternion>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref Quaternion lhs, Quaternion rhs) { lhs = rhs; }
    public Quaternion Zero { get=>Quaternion.identity; }
    public bool HasInfinity { get=>false; }
    public Quaternion PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleTriggerList : ITraitsSimple<List<Trigger>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Trigger; }
    public bool TestEquals(List<Trigger> lhs, List<Trigger> rhs) { EqualityComparer<List<Trigger>> ec = EqualityComparer<List<Trigger>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Trigger> lhs, List<Trigger> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Trigger> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<Trigger> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleBoolList : ITraitsSimple<List<bool>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Bool; }
    public bool TestEquals(List<bool> lhs, List<bool> rhs) { EqualityComparer<List<bool>> ec = EqualityComparer<List<bool>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<bool> lhs, List<bool> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<bool> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<bool> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleCharList : ITraitsSimple<List<char>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Char; }
    public bool TestEquals(List<char> lhs, List<char> rhs) { EqualityComparer<List<char>> ec = EqualityComparer<List<char>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<char> lhs, List<char> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<char> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<char> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleStringList : ITraitsSimple<List<string>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_String; }
    public bool TestEquals(List<string> lhs, List<string> rhs) { EqualityComparer<List<string>> ec = EqualityComparer<List<string>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<string> lhs, List<string> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<string> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<string> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleIntList : ITraitsSimple<List<int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Int; }
    public bool TestEquals(List<int> lhs, List<int> rhs) { EqualityComparer<List<int>> ec = EqualityComparer<List<int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<int> lhs, List<int> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<int> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleFloatList : ITraitsSimple<List<float>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Float; }
    public bool TestEquals(List<float> lhs, List<float> rhs) { EqualityComparer<List<float>> ec = EqualityComparer<List<float>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<float> lhs, List<float> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<float> Zero { get=>null; }
    public bool HasInfinity { get=>true; }
    public List<float> PositiveInfinity { get=>null; }
}
public class TraitsSimpleVector2IntList : ITraitsSimple<List<Vector2Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2Int; }
    public bool TestEquals(List<Vector2Int> lhs, List<Vector2Int> rhs) { EqualityComparer<List<Vector2Int>> ec = EqualityComparer<List<Vector2Int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector2Int> lhs, List<Vector2Int> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Vector2Int> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<Vector2Int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleVector2List : ITraitsSimple<List<Vector2>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector2; }
    public bool TestEquals(List<Vector2> lhs, List<Vector2> rhs) { EqualityComparer<List<Vector2>> ec = EqualityComparer<List<Vector2>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector2> lhs, List<Vector2> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Vector2> Zero { get=>null; }
    public bool HasInfinity { get=>true; }
    public List<Vector2> PositiveInfinity { get=>null; }
}
public class TraitsSimpleVector3IntList : ITraitsSimple<List<Vector3Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3Int; }
    public bool TestEquals(List<Vector3Int> lhs, List<Vector3Int> rhs) { EqualityComparer<List<Vector3Int>> ec = EqualityComparer<List<Vector3Int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector3Int> lhs, List<Vector3Int> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Vector3Int> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<Vector3Int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleVector3List : ITraitsSimple<List<Vector3>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector3; }
    public bool TestEquals(List<Vector3> lhs, List<Vector3> rhs) { EqualityComparer<List<Vector3>> ec = EqualityComparer<List<Vector3>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector3> lhs, List<Vector3> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Vector3> Zero { get=>null; }
    public bool HasInfinity { get=>true; }
    public List<Vector3> PositiveInfinity { get=>null; }
}
public class TraitsSimpleVector4List : ITraitsSimple<List<Vector4>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Vector4; }
    public bool TestEquals(List<Vector4> lhs, List<Vector4> rhs) { EqualityComparer<List<Vector4>> ec = EqualityComparer<List<Vector4>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Vector4> lhs, List<Vector4> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Vector4> Zero { get=>null; }
    public bool HasInfinity { get=>true; }
    public List<Vector4> PositiveInfinity { get=>null; }
}
public class TraitsSimpleQuaternionList : ITraitsSimple<List<Quaternion>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.List_Quaternion; }
    public bool TestEquals(List<Quaternion> lhs, List<Quaternion> rhs) { EqualityComparer<List<Quaternion>> ec = EqualityComparer<List<Quaternion>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref List<Quaternion> lhs, List<Quaternion> rhs) {lhs.Capacity=rhs.Capacity; for(int i=0;i<rhs.Count;++i){lhs[i]=rhs[i];}}
    public List<Quaternion> Zero { get=>null; }
    public bool HasInfinity { get=>false; }
    public List<Quaternion> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesTrigger : ITraitsSimple<KVariables<Trigger>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Trigger; }
    public bool TestEquals(KVariables<Trigger> lhs, KVariables<Trigger> rhs) { EqualityComparer<KVariables<Trigger>> ec = EqualityComparer<KVariables<Trigger>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Trigger> lhs, KVariables<Trigger> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Trigger> Zero { get=>new KVariables<Trigger>(Trigger.Default); }
    public bool HasInfinity { get=>false; }
    public KVariables<Trigger> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesBool : ITraitsSimple<KVariables<bool>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Bool; }
    public bool TestEquals(KVariables<bool> lhs, KVariables<bool> rhs) { EqualityComparer<KVariables<bool>> ec = EqualityComparer<KVariables<bool>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<bool> lhs, KVariables<bool> rhs) { lhs.SetEqual(rhs); }
    public KVariables<bool> Zero { get=>new KVariables<bool>(false); }
    public bool HasInfinity { get=>false; }
    public KVariables<bool> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesChar : ITraitsSimple<KVariables<char>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Char; }
    public bool TestEquals(KVariables<char> lhs, KVariables<char> rhs) { EqualityComparer<KVariables<char>> ec = EqualityComparer<KVariables<char>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<char> lhs, KVariables<char> rhs) { lhs.SetEqual(rhs); }
    public KVariables<char> Zero { get=>new KVariables<char>(default(char)); }
    public bool HasInfinity { get=>false; }
    public KVariables<char> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesString : ITraitsSimple<KVariables<string>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_String; }
    public bool TestEquals(KVariables<string> lhs, KVariables<string> rhs) { EqualityComparer<KVariables<string>> ec = EqualityComparer<KVariables<string>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<string> lhs, KVariables<string> rhs) { lhs.SetEqual(rhs); }
    public KVariables<string> Zero { get=>new KVariables<string>(default(string)); }
    public bool HasInfinity { get=>false; }
    public KVariables<string> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesInt : ITraitsSimple<KVariables<int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Int; }
    public bool TestEquals(KVariables<int> lhs, KVariables<int> rhs) { EqualityComparer<KVariables<int>> ec = EqualityComparer<KVariables<int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<int> lhs, KVariables<int> rhs) { lhs.SetEqual(rhs); }
    public KVariables<int> Zero { get=>new KVariables<int>(0); }
    public bool HasInfinity { get=>false; }
    public KVariables<int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesFloat : ITraitsSimple<KVariables<float>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Float; }
    public bool TestEquals(KVariables<float> lhs, KVariables<float> rhs) { EqualityComparer<KVariables<float>> ec = EqualityComparer<KVariables<float>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<float> lhs, KVariables<float> rhs) { lhs.SetEqual(rhs); }
    public KVariables<float> Zero { get=>new KVariables<float>(0f); }
    public bool HasInfinity { get=>true; }
    public KVariables<float> PositiveInfinity { get=>new KVariables<float>(float.PositiveInfinity); }
}
public class TraitsSimpleKVariablesVector2Int : ITraitsSimple<KVariables<Vector2Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2Int; }
    public bool TestEquals(KVariables<Vector2Int> lhs, KVariables<Vector2Int> rhs) { EqualityComparer<KVariables<Vector2Int>> ec = EqualityComparer<KVariables<Vector2Int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector2Int> lhs, KVariables<Vector2Int> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector2Int> Zero { get=>new KVariables<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariables<Vector2Int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesVector2 : ITraitsSimple<KVariables<Vector2>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector2; }
    public bool TestEquals(KVariables<Vector2> lhs, KVariables<Vector2> rhs) { EqualityComparer<KVariables<Vector2>> ec = EqualityComparer<KVariables<Vector2>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector2> lhs, KVariables<Vector2> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector2> Zero { get=>new KVariables<Vector2>(Vector2.zero); }
    public bool HasInfinity { get=>true; }
    public KVariables<Vector2> PositiveInfinity { get=>new KVariables<Vector2>(Vector2.positiveInfinity); }
}
public class TraitsSimpleKVariablesVector3Int : ITraitsSimple<KVariables<Vector3Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3Int; }
    public bool TestEquals(KVariables<Vector3Int> lhs, KVariables<Vector3Int> rhs) { EqualityComparer<KVariables<Vector3Int>> ec = EqualityComparer<KVariables<Vector3Int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector3Int> lhs, KVariables<Vector3Int> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector3Int> Zero { get=>new KVariables<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariables<Vector3Int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesVector3 : ITraitsSimple<KVariables<Vector3>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector3; }
    public bool TestEquals(KVariables<Vector3> lhs, KVariables<Vector3> rhs) { EqualityComparer<KVariables<Vector3>> ec = EqualityComparer<KVariables<Vector3>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector3> lhs, KVariables<Vector3> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector3> Zero { get=>new KVariables<Vector3>(Vector3.zero); }
    public bool HasInfinity { get=>true; }
    public KVariables<Vector3> PositiveInfinity { get=>new KVariables<Vector3>(Vector3.positiveInfinity); }
}
public class TraitsSimpleKVariablesVector4 : ITraitsSimple<KVariables<Vector4>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Vector4; }
    public bool TestEquals(KVariables<Vector4> lhs, KVariables<Vector4> rhs) { EqualityComparer<KVariables<Vector4>> ec = EqualityComparer<KVariables<Vector4>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Vector4> lhs, KVariables<Vector4> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Vector4> Zero { get=>new KVariables<Vector4>(Vector4.zero); }
    public bool HasInfinity { get=>true; }
    public KVariables<Vector4> PositiveInfinity { get=>new KVariables<Vector4>(Vector4.positiveInfinity); }
}
public class TraitsSimpleKVariablesQuaternion : ITraitsSimple<KVariables<Quaternion>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariables_Quaternion; }
    public bool TestEquals(KVariables<Quaternion> lhs, KVariables<Quaternion> rhs) { EqualityComparer<KVariables<Quaternion>> ec = EqualityComparer<KVariables<Quaternion>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariables<Quaternion> lhs, KVariables<Quaternion> rhs) { lhs.SetEqual(rhs); }
    public KVariables<Quaternion> Zero { get=>new KVariables<Quaternion>(Quaternion.identity); }
    public bool HasInfinity { get=>false; }
    public KVariables<Quaternion> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtTrigger : ITraitsSimple<KVariablesExt<Trigger>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Trigger; }
    public bool TestEquals(KVariablesExt<Trigger> lhs, KVariablesExt<Trigger> rhs) { EqualityComparer<KVariablesExt<Trigger>> ec = EqualityComparer<KVariablesExt<Trigger>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Trigger> lhs, KVariablesExt<Trigger> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Trigger> Zero { get=>new KVariablesExt<Trigger>(Trigger.Default); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Trigger> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtBool : ITraitsSimple<KVariablesExt<bool>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Bool; }
    public bool TestEquals(KVariablesExt<bool> lhs, KVariablesExt<bool> rhs) { EqualityComparer<KVariablesExt<bool>> ec = EqualityComparer<KVariablesExt<bool>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<bool> lhs, KVariablesExt<bool> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<bool> Zero { get=>new KVariablesExt<bool>(false); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<bool> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtChar : ITraitsSimple<KVariablesExt<char>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Char; }
    public bool TestEquals(KVariablesExt<char> lhs, KVariablesExt<char> rhs) { EqualityComparer<KVariablesExt<char>> ec = EqualityComparer<KVariablesExt<char>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<char> lhs, KVariablesExt<char> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<char> Zero { get=>new KVariablesExt<char>(default(char)); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<char> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtString : ITraitsSimple<KVariablesExt<string>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_String; }
    public bool TestEquals(KVariablesExt<string> lhs, KVariablesExt<string> rhs) { EqualityComparer<KVariablesExt<string>> ec = EqualityComparer<KVariablesExt<string>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<string> lhs, KVariablesExt<string> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<string> Zero { get=>new KVariablesExt<string>(default(string)); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<string> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtInt : ITraitsSimple<KVariablesExt<int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Int; }
    public bool TestEquals(KVariablesExt<int> lhs, KVariablesExt<int> rhs) { EqualityComparer<KVariablesExt<int>> ec = EqualityComparer<KVariablesExt<int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<int> lhs, KVariablesExt<int> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<int> Zero { get=>new KVariablesExt<int>(0); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtFloat : ITraitsSimple<KVariablesExt<float>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Float; }
    public bool TestEquals(KVariablesExt<float> lhs, KVariablesExt<float> rhs) { EqualityComparer<KVariablesExt<float>> ec = EqualityComparer<KVariablesExt<float>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<float> lhs, KVariablesExt<float> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<float> Zero { get=>new KVariablesExt<float>(0f); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<float> PositiveInfinity { get=>new KVariablesExt<float>(float.PositiveInfinity); }
}
public class TraitsSimpleKVariablesExtVector2Int : ITraitsSimple<KVariablesExt<Vector2Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2Int; }
    public bool TestEquals(KVariablesExt<Vector2Int> lhs, KVariablesExt<Vector2Int> rhs) { EqualityComparer<KVariablesExt<Vector2Int>> ec = EqualityComparer<KVariablesExt<Vector2Int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector2Int> lhs, KVariablesExt<Vector2Int> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector2Int> Zero { get=>new KVariablesExt<Vector2Int>(Vector2Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Vector2Int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtVector2 : ITraitsSimple<KVariablesExt<Vector2>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector2; }
    public bool TestEquals(KVariablesExt<Vector2> lhs, KVariablesExt<Vector2> rhs) { EqualityComparer<KVariablesExt<Vector2>> ec = EqualityComparer<KVariablesExt<Vector2>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector2> lhs, KVariablesExt<Vector2> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector2> Zero { get=>new KVariablesExt<Vector2>(Vector2.zero); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<Vector2> PositiveInfinity { get=>new KVariablesExt<Vector2>(Vector2.positiveInfinity); }
}
public class TraitsSimpleKVariablesExtVector3Int : ITraitsSimple<KVariablesExt<Vector3Int>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3Int; }
    public bool TestEquals(KVariablesExt<Vector3Int> lhs, KVariablesExt<Vector3Int> rhs) { EqualityComparer<KVariablesExt<Vector3Int>> ec = EqualityComparer<KVariablesExt<Vector3Int>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector3Int> lhs, KVariablesExt<Vector3Int> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector3Int> Zero { get=>new KVariablesExt<Vector3Int>(Vector3Int.zero); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Vector3Int> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariablesExtVector3 : ITraitsSimple<KVariablesExt<Vector3>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector3; }
    public bool TestEquals(KVariablesExt<Vector3> lhs, KVariablesExt<Vector3> rhs) { EqualityComparer<KVariablesExt<Vector3>> ec = EqualityComparer<KVariablesExt<Vector3>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector3> lhs, KVariablesExt<Vector3> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector3> Zero { get=>new KVariablesExt<Vector3>(Vector3.zero); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<Vector3> PositiveInfinity { get=>new KVariablesExt<Vector3>(Vector3.positiveInfinity); }
}
public class TraitsSimpleKVariablesExtVector4 : ITraitsSimple<KVariablesExt<Vector4>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Vector4; }
    public bool TestEquals(KVariablesExt<Vector4> lhs, KVariablesExt<Vector4> rhs) { EqualityComparer<KVariablesExt<Vector4>> ec = EqualityComparer<KVariablesExt<Vector4>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Vector4> lhs, KVariablesExt<Vector4> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Vector4> Zero { get=>new KVariablesExt<Vector4>(Vector4.zero); }
    public bool HasInfinity { get=>true; }
    public KVariablesExt<Vector4> PositiveInfinity { get=>new KVariablesExt<Vector4>(Vector4.positiveInfinity); }
}
public class TraitsSimpleKVariablesExtQuaternion : ITraitsSimple<KVariablesExt<Quaternion>> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariablesExt_Quaternion; }
    public bool TestEquals(KVariablesExt<Quaternion> lhs, KVariablesExt<Quaternion> rhs) { EqualityComparer<KVariablesExt<Quaternion>> ec = EqualityComparer<KVariablesExt<Quaternion>>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariablesExt<Quaternion> lhs, KVariablesExt<Quaternion> rhs) { lhs.SetEqual(rhs); }
    public KVariablesExt<Quaternion> Zero { get=>new KVariablesExt<Quaternion>(Quaternion.identity); }
    public bool HasInfinity { get=>false; }
    public KVariablesExt<Quaternion> PositiveInfinity { get=>throw new System.InvalidOperationException(); }
}
public class TraitsSimpleKVariableTypeSet : ITraitsSimple<KVariableTypeSet> {
    public DataTypeEnum DataType { get=>DataTypeEnum.KVariableTypeSetType; }
    public bool TestEquals(KVariableTypeSet lhs, KVariableTypeSet rhs) { EqualityComparer<KVariableTypeSet> ec = EqualityComparer<KVariableTypeSet>.Default; return ec.Equals(lhs, rhs); }
    public void SetEqual(ref KVariableTypeSet lhs, KVariableTypeSet rhs) { lhs.SetEqual(rhs); }
    public KVariableTypeSet Zero { get { return KVariableTypeInfo.None; } }
    public bool HasInfinity { get=>false; }
    public KVariableTypeSet PositiveInfinity { get { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return KVariableTypeInfo.None;
    #endif
     } }
    public KVariableTypeSet PositiveInfinities(int nElems=1) { 
    #if DEBUG
    throw new System.InvalidOperationException();
    #else
    return KVariableTypeInfo.None;
    #endif
     }
}
