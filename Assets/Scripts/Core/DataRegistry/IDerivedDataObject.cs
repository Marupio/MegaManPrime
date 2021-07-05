using System.Collections.Generic;

/// <summary>
/// Additional interface components for DataObjects that depend on other data objects.
/// I.e. these objects have 'derived' data.
/// </summary>
public interface IDerivedObject : IObject {
    /// <summary>
    /// Other data objects on which this one depends
    /// </summary>
    public List<IObject> DependsOn { get; }
    /// <summary>
    /// True if I am up to date
    /// </summary>
    public bool UpToDate();
    /// <summary>
    /// Bring all my derived data up to date
    /// </summary>
    public void Update();
}

public abstract class SimpleDerivedObject : ObjectHeader, IDerivedObject {
    protected List<IObject> m_dependsOn;

    public virtual List<IObject> DependsOn { get => m_dependsOn; }
    public virtual bool UpToDate() {
        foreach (IObject ido in m_dependsOn) {
            if (ido.MTag > m_mtag) { return false; }
        }
        return true;
    }
    // Implement the method to update this object's derived data
    public abstract void Update();

    public override bool HasDerivedData() { return true; }
    public override IDerivedObject DerivedObject() { return this; }

    // *** Constructors
    public SimpleDerivedObject(string name, IObjectRegistry parent = null)
    : base(name, parent) {
        m_dependsOn = new List<IObject>();
    }
    public SimpleDerivedObject(SimpleDerivedObject ddo)
    : base(ddo) {
        m_dependsOn = ddo.m_dependsOn;
    }
    public SimpleDerivedObject(IDerivedObject ido)
    : base(ido) {
        m_dependsOn = ido.DependsOn;
    }
    public SimpleDerivedObject() {
        m_dependsOn = new List<IObject>();
    }
}