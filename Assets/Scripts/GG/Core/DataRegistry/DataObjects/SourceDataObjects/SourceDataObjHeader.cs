public abstract class SourceDataObjHeader : DataObjHeader, ISourceDataObjMeta {
    // Currently ISourceDataObjMeta is empty, but if it had stuff, it would be here

    // *** Factory methods
    public static SourceDataObjHeader Spawn(
        DataTypeEnum type,
        string name,
        IObjRegistry parent
    ) {
        switch (type) {
            case DataTypeEnum.None:
                return new NoneSourceDataObj(name, parent);
            case DataTypeEnum.Bool:
                return new BoolSourceDataObj(name, parent);
            case DataTypeEnum.TriggerType:
                return new TriggerSourceDataObj(name, parent);
            case DataTypeEnum.Char:
                return new CharSourceDataObj(name, parent);
            case DataTypeEnum.String:
                return new StringSourceDataObj(name, parent);
            case DataTypeEnum.Int:
                return new IntSourceDataObj(name, parent);
            case DataTypeEnum.Float:
                return new FloatSourceDataObj(name, parent);
            case DataTypeEnum.Vector2IntType:
                return new Vector2IntSourceDataObj(name, parent);
            case DataTypeEnum.Vector2Type:
                return new Vector2SourceDataObj(name, parent);
            case DataTypeEnum.Vector3IntType:
                return new Vector3IntSourceDataObj(name, parent);
            case DataTypeEnum.Vector3Type:
                return new Vector3SourceDataObj(name, parent);
            case DataTypeEnum.Vector4Type:
                return new Vector4SourceDataObj(name, parent);
            case DataTypeEnum.QuaternionType:
                return new QuaternionSourceDataObj(name, parent);
            case DataTypeEnum.List_Trigger:
                return new TriggerListSourceDataObj(name, parent);
            case DataTypeEnum.List_Bool:
                return new BoolListSourceDataObj(name, parent);
            case DataTypeEnum.List_Char:
                return new CharListSourceDataObj(name, parent);
            case DataTypeEnum.List_String:
                return new StringListSourceDataObj(name, parent);
            case DataTypeEnum.List_Int:
                return new IntListSourceDataObj(name, parent);
            case DataTypeEnum.List_Float:
                return new FloatListSourceDataObj(name, parent);
            case DataTypeEnum.List_Vector2Int:
                return new Vector2IntListSourceDataObj(name, parent);
            case DataTypeEnum.List_Vector2:
                return new Vector2ListSourceDataObj(name, parent);
            case DataTypeEnum.List_Vector3Int:
                return new Vector3IntListSourceDataObj(name, parent);
            case DataTypeEnum.List_Vector3:
                return new Vector3ListSourceDataObj(name, parent);
            case DataTypeEnum.List_Vector4:
                return new Vector4ListSourceDataObj(name, parent);
            case DataTypeEnum.List_Quaternion:
                return new QuaternionListSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Trigger:
                return new KVariablesTriggerSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Bool:
                return new KVariablesBoolSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Char:
                return new KVariablesCharSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_String:
                return new KVariablesStringSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Int:
                return new KVariablesIntSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Float:
                return new KVariablesFloatSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Vector2Int:
                return new KVariablesVector2IntSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Vector2:
                return new KVariablesVector2SourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Vector3Int:
                return new KVariablesVector3IntSourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Vector3:
                return new KVariablesVector3SourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Vector4:
                return new KVariablesVector4SourceDataObj(name, parent);
            case DataTypeEnum.KVariables_Quaternion:
                return new KVariablesQuaternionSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Trigger:
                return new KVariablesExtTriggerSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Bool:
                return new KVariablesExtBoolSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Char:
                return new KVariablesExtCharSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_String:
                return new KVariablesExtStringSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Int:
                return new KVariablesExtIntSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Float:
                return new KVariablesExtFloatSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Vector2Int:
                return new KVariablesExtVector2IntSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Vector2:
                return new KVariablesExtVector2SourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Vector3Int:
                return new KVariablesExtVector3IntSourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Vector3:
                return new KVariablesExtVector3SourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Vector4:
                return new KVariablesExtVector4SourceDataObj(name, parent);
            case DataTypeEnum.KVariablesExt_Quaternion:
                return new KVariablesExtQuaternionSourceDataObj(name, parent);
            case DataTypeEnum.KVariableTypeSetType:
                return new KVariableTypeSetSourceDataObj(name, parent);
            default:
                #if DEBUG
                    throw new System.ArgumentException("Unhandled case: DataTypeEnum = " + type);
                #else
                    return null;
                #endif
        }
    }

    public SourceDataObjHeader(string name, IObjRegistry parent = null) : base(name, parent) {}
    public SourceDataObjHeader(SourceDataObjHeader obj) : base(obj) {}
    public SourceDataObjHeader() {}
}
