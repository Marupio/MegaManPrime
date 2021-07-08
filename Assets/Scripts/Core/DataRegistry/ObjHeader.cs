public class ObjHeader : IObj {
    protected string m_name;
    protected long m_id;
    protected ModTag m_mtag;
    protected IObjRegistry m_parent;

    // *** IObj interface
    public virtual string Name { get=>m_name; set=>m_name=value; }
    public virtual long Id { get=>m_id; }
    public virtual void SetId(long id) { m_id = id; }
    public virtual IObjRegistry Parent { get=>m_parent; }
    public void RegisterToParent(IObjRegistry newParent) {
        if (m_parent != null) {
            m_parent.UnregisterChild(this);
        }
        newParent.RegisterChild(this);
        m_parent = newParent;
    }
    public void UnregisterFromParent() {
        if (m_parent != null) {
            m_parent.UnregisterChild(this);
            m_parent = null;
        }
    }
    public void InternalSetOrphan() { m_parent = null; }
    public virtual ModTag MTag { get=>m_mtag; } // set
    public virtual void SetModified() { GlobalRegistrar.UpdateModTag(m_mtag); }
    /// <summary>
    /// DeepCopy on everything except parent
    /// </summary>
    public virtual IObj Clone(IObjRegistry parent) {
        ObjHeader newObj = new ObjHeader(this);
        return (IObj)newObj;
    }
    public IObjRegistry ObjRegistry() { return this as IObjRegistry; }
    public virtual IDataObjMeta DataObjMeta() { return this as IDataObjMeta; }

    // *** Constructors
    // Stream (future), Components, Copy, Null
    /// <summary>
    /// Contsruct from components
    /// </summary>
    public ObjHeader(string name, IObjRegistry parent = null) {
        m_name = name;
        m_id = GlobalRegistrar.GetNextId();
        m_mtag = GlobalRegistrar.GetNextModTag();
        m_parent = parent;
        if (parent != null) {
            parent.RegisterChild(this);
        }
    }
    public ObjHeader(ObjHeader obj) {
        m_name = obj.Name;
        m_id = GlobalRegistrar.GetNextId();
        m_mtag = obj.MTag;
        m_parent = obj.m_parent;
        if (m_parent != null) {
            m_parent.RegisterChild(this);
        }
    }
    /// <summary>
    /// Null constructor has no parent, no mod tag, but an ID is generated for it
    /// </summary>
    public ObjHeader() {
        m_id = GlobalRegistrar.GetNextId();
        m_mtag = new ModTag(GlobalRegistrar.ModTagUntagged);
    }
    ~ObjHeader() {
        if (m_parent != null) {
            m_parent.UnregisterChild(this);
        }
    }
}
