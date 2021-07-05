using System.Collections.Generic;

/// <summary>
/// Additional interface components for DataObjects that depend on other data objects.
/// I.e. these objects have 'derived' data.
/// </summary>
public interface IDerivedDataObject : IDataObjectHeader {
    /// <summary>
    /// Other data objects on which this one depends
    /// </summary>
    public List<IDataObjectHeader> DependsOn { get; }
    /// <summary>
    /// True if I am up to date
    /// </summary>
    public bool UpToDate();
    /// <summary>
    /// Bring all my derived data up to date
    /// </summary>
    public void Update();
}

public abstract class SimpleDerivedDataObject : DataObjectHeader, IDerivedDataObject {
    protected List<IDataObjectHeader> m_dependsOn;

    public virtual List<IDataObjectHeader> DependsOn { get => m_dependsOn; }
    public virtual bool UpToDate() {
        foreach (IObject ido in m_dependsOn) {
            if (ido.MTag > m_mtag) { return false; }
        }
        return true;
    }
    // Implement the method to update this object's derived data
    public abstract void Update();

    // *** Constructors
    public SimpleDerivedDataObject(string name, IObjectRegistry parent = null)
    : base(name, parent) {
        m_dependsOn = new List<IDataObjectHeader>();
    }
    public SimpleDerivedDataObject(SimpleDerivedDataObject derivedDataObject)
    : base(derivedDataObject) {
        m_dependsOn = derivedDataObject.m_dependsOn;
    }
    public SimpleDerivedDataObject(IDerivedDataObject derivedDataObject)
    : base(derivedDataObject) {
        m_dependsOn = derivedDataObject.DependsOn;
    }
    public SimpleDerivedDataObject() {
        m_dependsOn = new List<IDataObjectHeader>();
    }
}
