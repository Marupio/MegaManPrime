// using System;
// using System.Collections.Generic;

// // ****************************** ObjPass idea ********************************
// // No generics, class type hard-coded, but can use base for all
// // This doesn't work with ObjList, which is based on generics
// // ****************************************************************************

// public interface IObjPass {
//     bool Pass(IObj obj);
// }
// public struct ObjPassList : IObjPass {
//     public List<IObjPass> passes;
//     public bool Pass(IObj obj) {
//         foreach(IObjPass pass in passes) {
//             if (!pass.Pass(obj)) return false;
//         }
//         return true;
//     }
//     public ObjPassList(IEnumerable<IObjPass> collection) {
//         passes = new List<IObjPass>(collection);
//     }
// }
// public struct ObjPassClassType<T> : IObjPass where T : class, IObj {
//     public bool Pass(IObj obj) { return obj is T; }
// }
// public struct ObjPassDataType : IObjPass {
//     public DataTypeEnum dataType;
//     public bool Pass(IObj obj) {
//         if (obj is IDataObjMeta) return ((IDataObjMeta)obj).DataType == dataType;
//         return false;
//     }
//     public ObjPassDataType(DataTypeEnum dataTypeIn) { dataType = dataTypeIn; }
// }
// public struct ObjPassComponentType : IObjPass {
//     public DataTypeEnum componentType;
//     public bool Pass(IObj obj) {
//         if (obj is IDataSetObjMeta) return ((IDataSetObjMeta)obj).ComponentType == componentType;
//         return false;
//     }
//     public ObjPassComponentType(DataTypeEnum componentTypeIn) { componentType = componentTypeIn; }
// }

// // Convenience - these can be accomplished at runTime with ObjPassClassType
// public struct ObjPassDataOnly : IObjPass {
//     public bool Pass(IObj obj) { return obj is IDataObjMeta; }
// }
// public struct ObjPassDerivedOnly : IObjPass {
//     public bool Pass(IObj obj) { return obj is IDerivedDataObjMeta; }
// }
// public struct ObjPassSourceOnly : IObjPass {
//     public bool Pass(IObj obj) { return obj is ISourceDataObjMeta; }
// }



// public interface IDataObjPass {
//     bool Pass(IDataObjMeta obj);
// }
// public struct DataObjPassList : IDataObjPass {
//     public List<IDataObjPass> passes;
//     public bool Pass(IDataObjMeta obj) {
//         foreach(IDataObjPass pass in passes) {
//             if (!pass.Pass(obj)) return false;
//         }
//         return true;
//     }
//     public DataObjPassList(IEnumerable<IDataObjPass> collection) {
//         passes = new List<IDataObjPass>(collection);
//     }
// }
// public struct DataObjPassClassType<T> : IDataObjPass where T : class, IDataObjMeta {
//     public bool Pass(IDataObjMeta obj) { return obj is T; }
// }
// public struct DataObjPassDataType : IDataObjPass {
//     public DataTypeEnum dataType;
//     public bool Pass(IDataObjMeta obj) {
//         if (obj is IDataObjMeta) return ((IDataObjMeta)obj).DataType == dataType;
//         return false;
//     }
//     public DataObjPassDataType(DataTypeEnum dataTypeIn) { dataType = dataTypeIn; }
// }
// public struct DataObjPassComponentType : IDataObjPass {
//     public DataTypeEnum componentType;
//     public bool Pass(IDataObjMeta obj) {
//         if (obj is IDataSetObjMeta) return ((IDataSetObjMeta)obj).ComponentType == componentType;
//         return false;
//     }
//     public DataObjPassComponentType(DataTypeEnum componentTypeIn) { componentType = componentTypeIn; }
// }

// // Convenience - these can be accomplished at runTime with ObjPassClassType
// public struct DataObjPassDataOnly : IDataObjPass {
//     public bool Pass(IDataObjMeta obj) { return obj is IDataObjMeta; }
// }
// public struct DataObjPassDerivedOnly : IDataObjPass {
//     public bool Pass(IDataObjMeta obj) { return obj is IDerivedDataObjMeta; }
// }
// public struct DataObjPassSourceOnly : IDataObjPass {
//     public bool Pass(IDataObjMeta obj) { return obj is ISourceDataObjMeta; }
// }


// ******************************* FIRST PASS ********************************
// // *** Constraint Interfaces
// public interface IObjConstraint<T> where T : class, IObj {
//     bool Test(T obj);
// }
// public interface IDataObjConstraint<T> : IObjConstraint<T> where T : class, IDataObjMeta {}

// // *** Base class
// public abstract class ObjConstraintBase<T> : IObjConstraint<T> where T : class, IObj {
//     public abstract bool Test(T obj);
// }

// // *** ObjList constraints
// public abstract class ObjConstraint : ObjConstraintBase<IObj> {}
// public class ObjConstraintList : ObjConstraint {
//     public List<ObjConstraint> constraints;
//     public override bool Test(IObj obj) {
//         foreach(ObjConstraint constraint in constraints) {
//             if (!constraint.Test(obj)) { return false; }
//         }
//         return true;
//     }
// }
// public class ObjConstraintMustBeClass<T> : ObjConstraint where T : class, IObj {
//     public override bool Test(IObj obj) {
//         return obj is T;
//     }
// }

// // For use with DataObjList    
// public abstract class DataObjConstraint : ObjConstraintBase<IDataObjMeta>,  {}
// public class DataObjConstraintList : DataObjConstraint {
//     public List<DataObjConstraint> constraints;
//     public override bool Test(IDataObjMeta obj) {
//         foreach(DataObjConstraint constraint in constraints) {
//             if (!constraint.Test(obj)) { return false; }
//         }
//         return true;
//     }
// }
// public class DataObjConstraintMustBeClass<T> : DataObjConstraint where T : class, IDataObjMeta {
//     public override bool Test(IDataObjMeta obj) {
//         return obj is T;
//     }
// }
// public class DataObjConstraintDerivedOnly : DataObjConstraintMustBeClass<IDerivedDataObjMeta> {}
// public class DataObjConstraintSourceOnly : DataObjConstraintMustBeClass<ISourceDataObjMeta> {}
// public class DataObjConstraintMustBeDataType : DataObjConstraint {
//     public DataTypeEnum dataType;
//     public override bool Test(IDataObjMeta obj) {
//         return obj.DataType == dataType;
//     }
// }
// public class DataObjConstraintMustBeComponentType : DataObjConstraint {
//     public DataTypeEnum componentType;
//     public override bool Test(IDataObjMeta obj) {
//         if (obj is IDataSetObjMeta) {
//             return ((IDataSetObjMeta)obj).ComponentType == componentType;
//         }
//         return false;
//     }
// }
