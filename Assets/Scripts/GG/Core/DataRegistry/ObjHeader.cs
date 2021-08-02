using System;

public class ObjHeader : IObj, IEquatable<IObj> {
    protected string m_name;
    protected long m_id;
    protected ModTag m_mtag;
    protected IObjRegistry m_parent;
    protected bool m_clonable;

    // *** IObj interface
    // TODO remove virtual wherever possible
    public virtual string Name { get=>m_name; set=>m_name=value; }
    public virtual long Id { get=>m_id; }
    public virtual void SetId(long id) { m_id = id; }
    public override int GetHashCode() { return m_id.GetHashCode(); }
    public virtual bool Equals(IObj obj) { return m_id != GlobalRegistrar.IdAnonymous && m_id == obj.Id; }
    public override bool Equals(object obj) {
        if (obj is IObj) {
            return m_id != GlobalRegistrar.IdAnonymous && m_id == ((IObj)obj).Id;
        }
        return false;
    }
    public static bool operator==(IObj lhs, ObjHeader rhs) { return lhs.Id != GlobalRegistrar.IdAnonymous && lhs.Id == rhs.Id; }
    public static bool operator!=(IObj lhs, ObjHeader rhs) { return lhs.Id == GlobalRegistrar.IdAnonymous || lhs.Id != rhs.Id; }
    public static bool operator==(ObjHeader lhs, IObj rhs) { return lhs.Id != GlobalRegistrar.IdAnonymous && lhs.Id == rhs.Id; }
    public static bool operator!=(ObjHeader lhs, IObj rhs) { return lhs.Id == GlobalRegistrar.IdAnonymous || lhs.Id != rhs.Id; }
    public static bool operator==(ObjHeader lhs, ObjHeader rhs) { return lhs.Id != GlobalRegistrar.IdAnonymous && lhs.Id == rhs.Id; }
    public static bool operator!=(ObjHeader lhs, ObjHeader rhs) { return lhs.Id == GlobalRegistrar.IdAnonymous || lhs.Id != rhs.Id; }
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
    public virtual void SetModified() { GlobalRegistrar.UpdateModTag(ref m_mtag); }
    public virtual bool Clonable { get=>m_clonable; set=>m_clonable=value; }
    /// <summary>
    /// DeepCopy on everything except parent
    /// </summary>
    public virtual IObj Clone(IObjRegistry parent) {
        if (m_clonable) { // TODO propagate clonable throughout
            ObjHeader newObj = new ObjHeader(this);
            newObj.RegisterToParent(parent);
            return (IObj)newObj;
        } else {
            return this;
        }
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
