/// <summary>
/// Simple data object, can be inherited or encapsulated to build an IObject conforming class
/// </summary>
public class ObjectHeader : IObject {
    protected string m_name;
    protected long m_id;
    protected ModTag m_mtag;
    protected IObjectRegistry m_parent;

    // *** IObject interface
    public virtual string Name { get => m_name; set => m_name=value; }
    public virtual long Id { get => m_id; }
    public virtual void SetId(long id) { m_id = id; }
    public virtual IObjectRegistry Parent { get=>m_parent; }
    public void InternalSetOrphan() { m_parent = null; }
    public virtual ModTag MTag { get => m_mtag; } // set
    public virtual void SetModified() { GlobalRegistrar.UpdateModTag(m_mtag); }
    public IObjectRegistry ObjectRegistry() {
        if (this is IObjectRegistry) {
            return (IObjectRegistry)this;
        } else {
            return null;
        }
    }
    public virtual IDataObjectHeader DataObjectHeader() {
        if (this is IDataObjectHeader) {
            return (IDataObjectHeader)this;
        } else {
            return null;
        }
    }

    // *** Constructors
    /// <summary>
    /// Contsruct from components, generating a new id and modTag, if parent is provided, register with parent
    /// </summary>
    public ObjectHeader(string name, IObjectRegistry parent = null) {
        m_name = name;
        m_id = GlobalRegistrar.GetNextId();
        m_mtag = GlobalRegistrar.GetNextModTag();
        m_parent = parent;
        if (parent != null) {
            parent.Register(this);
        }
    }
    /// <summary>
    /// Creates a ObjectHeader from an IObject interface - basically clones a header from another object
    /// TODO - This will mess up my data registry.  Either it is the actual data object, or it is an entirely different object
    /// </summary>
    public ObjectHeader(IObject obj) {
        m_name = obj.Name;
        m_id = obj.Id;
        m_mtag = obj.MTag;
        m_parent = obj.Parent;
    }
    public ObjectHeader(ObjectHeader obj) {
        m_name = obj.Name;
        m_id = obj.Id;
        m_mtag = obj.MTag;
        m_parent = obj.Parent;
    }
    public ObjectHeader() {
        m_id = GlobalRegistrar.IdAnonymous;
        m_mtag = new ModTag(GlobalRegistrar.ModTagUntagged);
    }
    ~ObjectHeader() {
        if (m_parent != null) {
            m_parent.Unregister(this);
        }
    }
}
