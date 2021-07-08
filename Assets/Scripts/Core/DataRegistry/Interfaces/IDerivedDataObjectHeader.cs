using System.Collections.Generic;

/// <summary>
/// Additional interface components for DataObjects that depend on other data objects.
/// I.e. these objects have 'derived' data.
/// </summary>
public interface IDerivedDataObjectHeader : IDataObjectHeader {
    /// <summary>
    /// Other data objects on which this one depends
    /// </summary>
    public List<IDataObjectHeader> DependsOn { get; }
    /// <summary>
    /// True if I am up to date
    /// </summary>
    public bool UpToDate();
}

public class DerivedDataObjectHeader : DataObjectHeader, IDerivedDataObjectHeader {
    protected List<IDataObjectHeader> m_dependsOn;
    public List<IDataObjectHeader> DependsOn { get=>m_dependsOn; }
    public virtual bool UpToDate() {
        foreach (IObject dataObject in m_dependsOn) {
            if (dataObject.MTag > m_mtag) { return false; }
        }
        return true;
    }

    public DerivedDataObjectHeader(
        string name,
        IObjectRegistry parent = null,
        List<IDataObjectHeader> dependsOn = null
    ) : base (name, parent) { m_dependsOn = dependsOn; }
    public DerivedDataObjectHeader(IDerivedDataObjectHeader obj) : base (obj) { m_dependsOn = obj.DependsOn; }
    public DerivedDataObjectHeader(DerivedDataObjectHeader obj) : base (obj) { m_dependsOn = obj.m_dependsOn; }
    public DerivedDataObjectHeader() {}
}
