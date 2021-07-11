public static class DataTypeStaticData {
    public const int nDataTypeEnums = 50;
    public const int nDataTypeEnum_Singles = 13;
    public const int nDataTypeEnum_Lists = 13;
    public const int nDataTypeEnum_KVariables = 13;
    public const int nDataTypeEnum_KvariableExts = 13;
}

public enum DataTypeEnum {
// *** SINGLE TYPES
    // ENUM_NAME        // PREFIX_NAME      // TYPE_NAME         // COMPONENT     // ZERO
    None,               // Object           // object            // object        // null
    TriggerType,        // Trigger          // Trigger           // object        // new Trigger()`
    Bool,               // Bool             // bool              // object        // false
    Char,               // Char             // char              // object        // '\0'
    String,             // String           // string            // char          // ""
    Int,                // Int              // int               // object        // 0
    Float,              // Float            // float             // object        // 0f
    Vector2IntType,     // Vector2Int       // Vector2Int        // int           // Vector2Int.zero
    Vector2Type,        // Vector2          // Vector2           // float         // Vector2.zero
    Vector3IntType,     // Vector3Int       // Vector3Int        // int           // Vector3Int.zero
    Vector3Type,        // Vector3          // Vector3           // float         // Vector3.zero
    Vector4Type,        // Vector4          // Vector4           // float         // Vector4.zero
    QuaternionType,     // Quaternion       // Quaternion        // float         // Quaternion.identity
// *** LIST TYPES
    List_Trigger,       // TriggerList      // List<Trigger>     // Trigger       // new List<Trigger>()
    List_Bool,          // BoolList         // List<Bool>        // Bool          // new List<Bool>()
    List_Char,          // CharList         // List<Char>        // Char          // new List<Char>()
    List_String,        // StringList       // List<String>      // String        // new List<String>()
    List_Int,           // IntList          // List<Int>         // Int           // new List<Int>()
    List_Float,         // FloatList        // List<Float>       // Float         // new List<Float>()
    List_Vector2Int,    // Vector2IntList   // List<Vector2Int>  // Vector2Int    // new List<Vector2Int>()
    List_Vector2,       // Vector2List      // List<Vector2>     // Vector2       // new List<Vector2>()
    List_Vector3Int,    // Vector3IntList   // List<Vector3Int>  // Vector3Int    // new List<Vector3Int>()
    List_Vector3,       // Vector3List      // List<Vector3>     // Vector3       // new List<Vector3>()
    List_Vector4,       // Vector4List      // List<Vector4>     // Vector4       // new List<Vector4>()
    List_Quaternion,    // QuaternionList   // List<Quaternion>  // Quaternion    // new List<Quaternion>()
// *** KVARIABLES TYPES
    KVariables_Trigger,         // Trigger
    KVariables_Bool,            // Bool
    KVariables_Char,            // Char
    KVariables_String,          // String
    KVariables_Int,             // Int
    KVariables_Float,           // Float
    KVariables_Vector2Int,      // Vector2Int
    KVariables_Vector2,         // Vector2
    KVariables_Vector3Int,      // Vector3Int
    KVariables_Vector3,         // Vector3
    KVariables_Vector4,         // Vector4
    KVariables_Quaternion,      // Quaternion
// *** KVARIABLESEXT TYPES
    KVariablesExt_Trigger,      // Trigger
    KVariablesExt_Bool,         // Bool
    KVariablesExt_Char,         // Char
    KVariablesExt_String,       // String
    KVariablesExt_Int,          // Int
    KVariablesExt_Float,        // Float
    KVariablesExt_Vector2Int,   // Vector2Int
    KVariablesExt_Vector2,      // Vector2
    KVariablesExt_Vector3Int,   // Vector3Int
    KVariablesExt_Vector3,      // Vector3
    KVariablesExt_Vector4,      // Vector4
    KVariablesExt_Quaternion,   // Quaternion
// *** OTHER SET TYPES
    KVariableTypeSetType        // Bool
    //KVariableLimitsType         // KVariableExt_Float
}

// registry.FindObjects<KVariableLimitsDataObject>(entityName + ".limits");

public enum DataTypeEnum_Single {
    None,
    Trigger_Type,
    Bool,
    Char,
    String,
    Int,
    Float,
    Vector2IntType,
    Vector2Type,
    Vector3IntType,
    Vector3Type,
    Vector4Type,
    QuaternionType
}

public enum DataTypeEnum_List {
    None,
    List_Trigger = 13,
    List_Bool,
    List_Char,
    List_String,
    List_Int,
    List_Float,
    List_Vector2Int,
    List_Vector2,
    List_Vector3Int,
    List_Vector3,
    List_Vector4,
    List_Quaternion
}

public enum DataTypeEnum_KVariables {
    None,
    KVariables_Trigger = 25,
    KVariables_Bool,
    KVariables_Char,
    KVariables_String,
    KVariables_Int,
    KVariables_Float,
    KVariables_Vector2Int,
    KVariables_Vector2,
    KVariables_Vector3Int,
    KVariables_Vector3,
    KVariables_Vector4,
    KVariables_Quaternion
}

public enum DataTypeEnum_KVariablesExt {
    None,
    KVariablesExt_Trigger = 37,
    KVariablesExt_Bool,
    KVariablesExt_Char,
    KVariablesExt_String,
    KVariablesExt_Int,
    KVariablesExt_Float,
    KVariablesExt_Vector2Int,
    KVariablesExt_Vector2,
    KVariablesExt_Vector3Int,
    KVariablesExt_Vector3,
    KVariablesExt_Vector4,
    KVariablesExt_Quaternion
}
