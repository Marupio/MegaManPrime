using System.Collections.Generic;

// *** Two interfaces to choose from
public interface IObjPass<T> where T : class, IObj { bool Null {get;} bool Pass(T obj); }

public interface IDataObjPass<T> : IObjPass<T> where T : class, IDataObjMeta {}

// *** Base classes
public abstract class ObjPassBase : IObjPass<IObj> {
    public virtual bool Null { get=>false; }
    public abstract bool Pass(IObj obj);
}
public abstract class DataObjPassBase : IDataObjPass<IDataObjMeta> {
    public virtual bool Null { get=>false; }
    public abstract bool Pass(IDataObjMeta obj);
}


// *** Comile time classes (parameterless)
public class ObjPassNull : ObjPassBase {
    public override bool Null { get=>true; }
    public override bool Pass(IObj obj) { return true; }
}
public class DataObjPassNull : DataObjPassBase {
    public override bool Null { get=>true; }
    public override bool Pass(IDataObjMeta obj) { return true; }
}

// *** Class Type passes
public class ObjPassClassType<T> : ObjPassBase where T : class, IObj {
    public override bool Pass(IObj obj) { return obj is T; }
}
public class DataObjPassClassType<T> : DataObjPassBase where T : class, IDataObjMeta {
    public override bool Pass(IDataObjMeta obj) { return obj is T; }
}

// *** Boolean AND List Passes
public class ObjPassList_and : ObjPassBase {
    public List<IObjPass<IObj>> passes;
    public override bool Pass(IObj obj) {
        foreach(IObjPass<IObj> pass in passes) { if (!pass.Pass(obj)) return false; }
        return true;
    }
    public ObjPassList_and(IEnumerable<IObjPass<IObj>> collection) { passes = new List<IObjPass<IObj>>(collection); }
    public ObjPassList_and() { passes = new List<IObjPass<IObj>>(); }
}
public class DataObjPassList_and : DataObjPassBase {
    public List<IObjPass<IDataObjMeta>> passes;
    public override bool Pass(IDataObjMeta obj) {
        foreach(IObjPass<IDataObjMeta> pass in passes) { if (!pass.Pass(obj)) return false; }
        return true;
    }
    public DataObjPassList_and(IEnumerable<IObjPass<IDataObjMeta>> collection) { passes = new List<IObjPass<IDataObjMeta>>(collection); }
    public DataObjPassList_and() { passes = new List<IObjPass<IDataObjMeta>>(); }
}

// *** Boolean OR List Passes
public class ObjPassList_or : ObjPassBase {
    public List<IObjPass<IObj>> passes;
    public override bool Pass(IObj obj) {
        foreach(IObjPass<IObj> pass in passes) { if (!pass.Pass(obj)) return false; }
        return true;
    }
    public ObjPassList_or(IEnumerable<IObjPass<IObj>> collection) { passes = new List<IObjPass<IObj>>(collection); }
    public ObjPassList_or() { passes = new List<IObjPass<IObj>>(); }
}
public class DataObjPassList_or : DataObjPassBase {
    public List<IObjPass<IDataObjMeta>> passes;
    public override bool Pass(IDataObjMeta obj) {
        foreach(IObjPass<IDataObjMeta> pass in passes) { if (pass.Pass(obj)) return true; }
        return false;
    }
    public DataObjPassList_or(IEnumerable<IObjPass<IDataObjMeta>> collection) { passes = new List<IObjPass<IDataObjMeta>>(collection); }
    public DataObjPassList_or() { passes = new List<IObjPass<IDataObjMeta>>(); }
}

// *** Data DataType passes - currently resolved at runtime
// TODO - Expand these to compile-time classes
public class ObjPassDataType : ObjPassBase {
    public DataTypeEnum dataType;
    public override bool Pass(IObj obj) {
        if (obj is IDataObjMeta) { return ((IDataObjMeta)obj).DataType == dataType; }
        return false;
    }
    public ObjPassDataType(DataTypeEnum dataTypeIn) { dataType = dataTypeIn; }
    public ObjPassDataType() { dataType = DataTypeEnum.None; }
}
public class DataObjPassDataType : DataObjPassBase {
    public DataTypeEnum dataType;
    public override bool Pass(IDataObjMeta obj) { return obj.DataType == dataType; }
    public DataObjPassDataType(DataTypeEnum dataTypeIn) { dataType = dataTypeIn; }
    public DataObjPassDataType() { dataType = DataTypeEnum.None; }
}

// *** Component DataType passes - currently resolved at runtime
// TODO - Expand these to compile-time classes
public class ObjPassComponentType : ObjPassBase {
    public DataTypeEnum componentType;
    public override bool Pass(IObj obj) {
        if (obj is IDataSetObjMeta) { return ((IDataSetObjMeta)obj).ComponentType == componentType; }
        return false;
    }
    public ObjPassComponentType(DataTypeEnum componentTypeIn) { componentType = componentTypeIn; }
    public ObjPassComponentType() { componentType = DataTypeEnum.None; }
}
public class DataObjPassComponentType<T> : DataObjPassBase {
    public DataTypeEnum componentType;
    public override bool Pass(IDataObjMeta obj) {
        if (obj is IDataSetObjMeta) { return ((IDataSetObjMeta)obj).ComponentType == componentType; }
        return false;
    }
    public DataObjPassComponentType(DataTypeEnum componentTypeIn) { componentType = componentTypeIn; }
    public DataObjPassComponentType() { componentType = DataTypeEnum.None; }
}

// *** Typedefs, but not really
public class ObjPassSourceData : ObjPassClassType<ISourceDataObjMeta> {}
public class DataObjPassSourceData : DataObjPassClassType<ISourceDataObjMeta> {}
public class ObjPassDerivedData : ObjPassClassType<IDerivedDataObjMeta> {}
public class DataObjPassDerivedData : DataObjPassClassType<IDerivedDataObjMeta> {}
