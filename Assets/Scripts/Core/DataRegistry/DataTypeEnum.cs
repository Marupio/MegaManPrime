public enum DataTypeEnum {
 // MAIN TYPE (L)               // COMPONENT TYPE (C)
    None,                       // None                 // TraitsNone
    Bool,                       // None                 // TraitsNone
    Char,                       // None                 // TraitsNone
    String,                     // Char                 // TraitsNone
    Int,                        // None                 // TraitsNone
    Float,                      // None                 // TraitsNone
    Vector2IntType,             // Int                  // TraitsInt
    Vector2Type,                // Float                // TraitsFloat
    Vector3IntType,             // Int                  // TraitsInt
    Vector3Type,                // Float                // TraitsFloat
    Vector4Type,                // Int                  // TraitsInt
    QuaternionType,             // Float                // TraitsFloat
    List_Bool,                  // Bool                 // TraitsBool
    List_String,                // String               // TraitsString
    List_Int,                   // Int                  // TraitsInt
    List_Float,                 // Float                // TraitsFloat
    List_Vector2Int,            // Vector2Int           // TraitsVector2Int
    List_Vector2,               // Vector2              // TraitsVector2
    List_Vector3Int,            // Vector3Int           // TraitsVector3Int
    List_Vector3,               // Vector3              // TraitsVector3
    List_Vector4,               // Vector4              // TraitsVector4
    List_Quaternion,            // Quaternion           // TraitsQuaternion
    KVariables_Bool,            // Bool                 // TraitsBool
    KVariables_String,          // String               // TraitsString
    KVariables_Int,             // Int                  // TraitsInt
    KVariables_Float,           // Float                // TraitsFloat
    KVariables_Vector2Int,      // Vector2Int           // TraitsVector2Int
    KVariables_Vector2,         // Vector2              // TraitsVector2
    KVariables_Vector3Int,      // Vector3Int           // TraitsVector3Int
    KVariables_Vector3,         // Vector3              // TraitsVector3
    KVariables_Vector4,         // Vector4              // TraitsVector4
    KVariables_Quaternion,      // Quaternion           // TraitsQuaternion
    KVariablesExt_Bool,         // Bool                 // TraitsBool
    KVariablesExt_String,       // String               // TraitsString
    KVariablesExt_Int,          // Int                  // TraitsInt
    KVariablesExt_Float,        // Float                // TraitsFloat
    KVariablesExt_Vector2Int,   // Vector2Int           // TraitsVector2Int
    KVariablesExt_Vector2,      // Vector2              // TraitsVector2
    KVariablesExt_Vector3Int,   // Vector3Int           // TraitsVector3Int
    KVariablesExt_Vector3,      // Vector3              // TraitsVector3
    KVariablesExt_Vector4,      // Vector4              // TraitsVector4
    KVariablesExt_Quaternion    // Quaternion           // TraitsQuaternion
}

public enum DataTypeEnum_Single {
    None,
    Bool,
    String,
    Int,
    Float,
    Vector2IntType,
    Vector2Type,
    Vector3IntType,
    Vector3Type,
    Vector4Type,
    QuaternionType,
    List_Bool,
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

public enum DataTypeEnum_List {
    None,
    List_Bool = 12,
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
    KVariables_Bool = 22,
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
    KVariablesExt_Bool = 32,
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
