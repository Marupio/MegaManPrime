/// <summary>
/// A basic header for objects that can be stored in the DataRegistry.  Other objects may depend on data in this object.
/// This can be attached to the actual object, or a seperate header used to refer to that object
/// </summary>
public interface IDataObject {
    /// <summary>
    /// Name assigned to this object, can be duplicated
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// A unique ID number, assigned upon creation, and never changes
    /// </summary>
    public long Id { get; }
    /// <summary>
    /// A unique tag indicating when it was last modified, relative to other tags
    /// </summary>
    /// <value></value>
    public IDataRegistry Parent { get; }
    /// <summary>
    /// Return a header-only IDataObject copy of this object
    /// </summary>
    public ModTag MTag { get; }
    /// <summary>
    /// Indicates this data object has changed, updates MTag to latest
    /// </summary>
    public void SetModified();
    // public List<string> [Type]DataTableOfContents
    // public List<Type> [Type]Data
    /// <summary>
    /// The DataRegistry that I'm registered in, or null if orphan
    /// </summary>
    public IDataObject CloneHeader();
    /// <summary>
    /// Returns true if DerivedDataObject will return non-null
    /// </summary>
    public bool HasDerivedData();
    /// <summary>
    /// Convert this to an IDerivedDataObject, or null if it is not one.
    /// </summary>
    public IDerivedDataObject DerivedDataObject();
}

/// <summary>
/// Header-only data object, can be inherited to build an IDataObject conforming class
/// </summary>
public class DataObjectHeader : IDataObject {
    protected string m_name;
    protected long m_id;
    protected ModTag m_mtag;
    protected IDataRegistry m_parent;

    // *** IDataObject interface
    public virtual string Name { get => m_name; set => m_name=value; }
    public virtual long Id { get => m_id; set => m_id = value; }
    public virtual IDataRegistry Parent { get=>m_parent; }
    public virtual ModTag MTag { get => m_mtag; } // set
    public virtual void SetModified() { GlobalRegistrar.UpdateModTag(m_mtag); }
    public virtual IDataObject CloneHeader() {
        return new DataObjectHeader(this);
    }
    public virtual bool HasDerivedData() {
        IDerivedDataObject ido = DerivedDataObject();
        return ido != null;
    }
    public virtual IDerivedDataObject DerivedDataObject() {
        if (this is IDerivedDataObject) {
            return (IDerivedDataObject)this;
        } else {
            return null;
        }
    }

    // *** Constructors
    /// <summary>
    /// Contsruct from components, generating a new id and modTag, if parent is provided, register with parent
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parent"></param>
    public DataObjectHeader(string name, IDataRegistry parent = null) {
        m_name = name;
        m_id = GlobalRegistrar.GetNextId();
        m_mtag = GlobalRegistrar.GetNextModTag();
        m_parent = parent;
        if (parent != null) {
            parent.Register(this);
        }
    }
    /// <summary>
    /// Creates a DataObjectHeader from a IDataObject interface - basically clones a header from another object
    /// </summary>
    /// <param name="ido"></param>
    public DataObjectHeader(IDataObject ido) {
        m_name = ido.Name;
        m_id = ido.Id;
        m_mtag = ido.MTag;
        m_parent = ido.Parent;
    }
    public DataObjectHeader(DataObjectHeader doh) {
        m_name = doh.Name;
        m_id = doh.Id;
        m_mtag = doh.MTag;
        m_parent = doh.Parent;
    }
    public DataObjectHeader() {
        m_id = GlobalRegistrar.IdAnonymous;
        m_mtag = new ModTag(GlobalRegistrar.ModTagUntagged);
    }
}