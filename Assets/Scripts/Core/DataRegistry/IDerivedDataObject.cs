using System.Collections.Generic;

/// <summary>
/// Additional interface components for DataObjects that depend on other data objects.
/// I.e. these objects have 'derived' data.
/// </summary>
public interface IDerivedDataObject : IDataObject {
    /// <summary>
    /// Other data objects on which this one depends
    /// </summary>
    public List<IDataObject> DependsOn { get; }
    /// <summary>
    /// True if I am up to date
    /// </summary>
    public bool UpToDate();
    /// <summary>
    /// Bring all my derived data up to date
    /// </summary>
    public void Update();
}

public abstract class DerivedDataObjectHeader : DataObjectHeader, IDerivedDataObject {
    protected List<IDataObject> m_dependsOn;

    public virtual List<IDataObject> DependsOn { get => m_dependsOn; }
    public virtual bool UpToDate() {
        foreach (IDataObject ido in m_dependsOn) {
            if (ido.MTag > m_mtag) { return false; }
        }
        return true;
    }
    // Implement the method to update this object's derived data
    public abstract void Update();

    public override bool HasDerivedData() { return true; }
    public override IDerivedDataObject DerivedDataObject() { return this; }

    // *** Constructors
    public DerivedDataObjectHeader(string name, IDataRegistry parent = null)
    : base(name, parent) {
        m_dependsOn = new List<IDataObject>();
    }
    public DerivedDataObjectHeader(DerivedDataObjectHeader ddo)
    : base(ddo) {
        m_dependsOn = ddo.m_dependsOn;
    }
    public DerivedDataObjectHeader(IDerivedDataObject ido)
    : base(ido) {
        m_dependsOn = ido.DependsOn;
    }
    public DerivedDataObjectHeader() {
        m_dependsOn = new List<IDataObject>();
    }
}