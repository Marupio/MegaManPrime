using System.Collections.Generic;

public interface IDerivedDataObject<L> : IDerivedDataObjectHeader, IDataObject<L> {
    public IDerivedDataUpdaterOld<L> Updater { get; set; }
    public void UpdateDerived();
}

public interface IDerivedDataUpdaterOld<L> {
    public void UpdateDerivedOn(IDerivedDataObject<L> target, ref L data);
}

// Now this might work, but I think I could possibly attach even lower in the inheritence tree
// public abstract class DerivedDataObjectBase<L> : DerivedDataObjectHeader, IDerivedDataObject<L> {
//     protected L m_derivedData;
//     protected IDerivedDataUpdater<L> m_updater;

//     public virtual L Data { get; set; }
//     public virtual IDerivedDataUpdater<L> Updater { get=>m_updater; set=>m_updater=value; }
//     public virtual void UpdateDerived() {
//         m_updater.UpdateDerivedOn(this, m_derivedData);
//         SetModified();
//     }

//     // *** Constructors
//     public DerivedDataObjectBase(
//         string name,
//         IObjectRegistry parent = null,
//         List<IDataObjectHeader> dependsOn = null,
//         IDerivedDataUpdater<L> updater = null
//     ) : base (name, parent, dependsOn) {
//         m_updater = updater;
//     }
//     public DerivedDataObjectBase(IDerivedDataObject<L> obj) : base(obj) { m_updater = obj.Updater; }
//     public DerivedDataObjectBase(DerivedDataObjectBase<L> obj) : base(obj) { m_updater = obj.m_updater; }
//     public DerivedDataObjectBase() {}
// }
