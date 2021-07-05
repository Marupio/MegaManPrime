public static class DataTypeStaticData {
    public const int nDataTypeEnums = 50;
}

public enum DataTypeEnum {
 // MAIN TYPE (L)               // COMPONENT TYPE (C)
    None,                       // None
    TriggerType,                // None
    Bool,                       // None
    Char,                       // None
    String,                     // Char
    Int,                        // None
    Float,                      // None
    Vector2IntType,             // Int
    Vector2Type,                // Float
    Vector3IntType,             // Int
    Vector3Type,                // Float
    Vector4Type,                // Int
    QuaternionType,             // Float
    List_Trigger,               // Trigger
    List_Bool,                  // Bool
    List_Char,                  // Char
    List_String,                // String
    List_Int,                   // Int
    List_Float,                 // Float
    List_Vector2Int,            // Vector2Int
    List_Vector2,               // Vector2
    List_Vector3Int,            // Vector3Int
    List_Vector3,               // Vector3
    List_Vector4,               // Vector4
    List_Quaternion,            // Quaternion
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
    KVariablesExt_Quaternion    // Quaternion
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
    QuaternionType,
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
