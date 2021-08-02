using System.Collections.Generic;

public abstract class DerivedDataObjHeader : DataObjHeader, IDerivedDataObjMeta {
    // Removed from design, now Updater holds them all
    // // Encapsulation - updater is responsible for initialising and populating these data
    // DataObjList_AllPass m_directDependsOn;
    // DataObjList_SourcePass m_sourceDependsOn;
    IDerivedUpdater m_updater;

    public IDerivedUpdater Updater { get=>m_updater; set=>m_updater=value; }
    // public DataObjList_AllPass DirectDependsOn { get=>m_directDependsOn; set=>m_directDependsOn=value; }
    // public DataObjList_SourcePass SourceDependsOn { get=>m_sourceDependsOn; set=>m_sourceDependsOn=value; }
    public bool Stale() { return m_updater == null; }
    public bool UpToDate() {
        if (m_updater == null) { return false; }
        return m_updater.UpToDateFor(this);
    }
    public bool UpdateDerived() {
        if (m_updater == null) {
            return false;
        }
        return m_updater.PerformUpdatesFor(this);
    }

    // *** Factory methods
    public static DerivedDataObjHeader Spawn(
        DataTypeEnum type,
        string name,
        IObjRegistry parent,
        IDerivedUpdater updater
        // no need to provide initial value - that only matters if making a stale Derived
    ) {
        switch (type) {
            case DataTypeEnum.None:
                return new NoneDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Bool:
                return new BoolDerivedDataObj(name, parent, updater);
            case DataTypeEnum.TriggerType:
                return new TriggerDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Char:
                return new CharDerivedDataObj(name, parent, updater);
            case DataTypeEnum.String:
                return new StringDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Int:
                return new IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Float:
                return new FloatDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Vector2IntType:
                return new Vector2IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Vector2Type:
                return new Vector2DerivedDataObj(name, parent, updater);
            case DataTypeEnum.Vector3IntType:
                return new Vector3IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.Vector3Type:
                return new Vector3DerivedDataObj(name, parent, updater);
            case DataTypeEnum.Vector4Type:
                return new Vector4DerivedDataObj(name, parent, updater);
            case DataTypeEnum.QuaternionType:
                return new QuaternionDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Trigger:
                return new TriggerListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Bool:
                return new BoolListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Char:
                return new CharListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_String:
                return new StringListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Int:
                return new IntListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Float:
                return new FloatListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Vector2Int:
                return new Vector2IntListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Vector2:
                return new Vector2ListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Vector3Int:
                return new Vector3IntListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Vector3:
                return new Vector3ListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Vector4:
                return new Vector4ListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.List_Quaternion:
                return new QuaternionListDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Trigger:
                return new KVariablesTriggerDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Bool:
                return new KVariablesBoolDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Char:
                return new KVariablesCharDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_String:
                return new KVariablesStringDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Int:
                return new KVariablesIntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Float:
                return new KVariablesFloatDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Vector2Int:
                return new KVariablesVector2IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Vector2:
                return new KVariablesVector2DerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Vector3Int:
                return new KVariablesVector3IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Vector3:
                return new KVariablesVector3DerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Vector4:
                return new KVariablesVector4DerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariables_Quaternion:
                return new KVariablesQuaternionDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Trigger:
                return new KVariablesExtTriggerDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Bool:
                return new KVariablesExtBoolDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Char:
                return new KVariablesExtCharDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_String:
                return new KVariablesExtStringDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Int:
                return new KVariablesExtIntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Float:
                return new KVariablesExtFloatDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Vector2Int:
                return new KVariablesExtVector2IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Vector2:
                return new KVariablesExtVector2DerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Vector3Int:
                return new KVariablesExtVector3IntDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Vector3:
                return new KVariablesExtVector3DerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Vector4:
                return new KVariablesExtVector4DerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariablesExt_Quaternion:
                return new KVariablesExtQuaternionDerivedDataObj(name, parent, updater);
            case DataTypeEnum.KVariableTypeSetType:
                return new KVariableTypeSetDerivedDataObj(name, parent, updater);
            default:
                #if DEBUG
                    throw new System.ArgumentException("Unhandled case: DataTypeEnum = " + type);
                #else
                    return null;
                #endif
        }
    }

    public DerivedDataObjHeader(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null
    ) : base(name, parent) {
        m_updater = updater;
    }
    public DerivedDataObjHeader(DerivedDataObjHeader obj) : base(obj) {}
    public DerivedDataObjHeader() : base() {}
}
