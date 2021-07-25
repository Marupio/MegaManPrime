using System;
using System.Collections.Generic;

public abstract class ObjConstraintBase<T> where T : class, IObj {
    public abstract bool Test(T obj);
}

// For use with ObjList
public abstract class ObjConstraint : ObjConstraintBase<IObj> {}
public class ObjConstraintList : ObjConstraint {
    public List<ObjConstraint> constraints;
    public override bool Test(IObj obj) {
        foreach(ObjConstraint constraint in constraints) {
            if (!constraint.Test(obj)) { return false; }
        }
        return true;
    }
}
public class ObjConstraintMustBeClass<T> : ObjConstraint where T : class, IObj {
    public override bool Test(IObj obj) {
        return obj is T;
    }
}

// For use with DataObjList    
public abstract class DataObjConstraint : ObjConstraintBase<IDataObjMeta> {}
public class DataObjConstraintList : DataObjConstraint {
    public List<DataObjConstraint> constraints;
    public override bool Test(IDataObjMeta obj) {
        foreach(DataObjConstraint constraint in constraints) {
            if (!constraint.Test(obj)) { return false; }
        }
        return true;
    }
}
public class DataObjConstraintMustBeClass<T> : DataObjConstraint where T : class, IDataObjMeta {
    public override bool Test(IDataObjMeta obj) {
        return obj is T;
    }
}
public class DataObjConstraintMustBeDataType : DataObjConstraint {
    public DataTypeEnum dataType;
    public override bool Test(IDataObjMeta obj) {
        return obj.DataType == dataType;
    }
}
public class DataObjConstraintMustBeComponentType : DataObjConstraint {
    public DataTypeEnum componentType;
    public override bool Test(IDataObjMeta obj) {
        if (obj is IDataSetObjMeta) {
            return ((IDataSetObjMeta)obj).ComponentType == componentType;
        }
        return false;
    }
}
