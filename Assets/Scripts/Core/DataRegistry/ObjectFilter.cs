/// <summary>
/// Used to select a single or multiple DataObjects
/// </summary>
public struct ObjectFilter {
    // // Not sure if we need this functionality
    // public enum HierarchicalQualifier {
    //     None,
    //     LocalChildren,
    //     GlobalChildren,
    //     Parent,
    //     AllParents
    // }
    // private HierarchicalQualifier m_hierarchicalQualifier; // once object[s] found, use this to decide what to return

    // strings - we use null == filter not applied
    private string m_nameRegex;
    private string m_nameExact;
    private string m_nameContains;
    private string m_typeNameRegex;
    private string m_typeNameExact;
    private string m_typeNameContains;
    // Internally: id longs - we use 0 == filter not applied (because I'm a struct)
    // all negatives (and zero) are shifted -= 1 because GlobalRegistrar.IdAnonymous is long.MinValue
    private long m_id;
    private long m_idGreaterThan;
    private long m_idLessThan;

    // *** Access
    public string NameRegex { get=> m_nameRegex; set => m_nameRegex=value; }
    public string NameExact { get=> m_nameExact; set => m_nameExact=value; }
    public string NameContains { get=> m_nameContains; set => m_nameContains=value; }
    public string TypeNameRegex { get=> m_typeNameRegex; set => m_typeNameRegex=value; }
    public string TypeNameExact { get=> m_typeNameExact; set => m_typeNameExact=value; }
    public string TypeNameContains { get=> m_typeNameContains; set => m_typeNameContains=value; }
    public long Id {
        get { return m_id < 0 ? m_id + 1 : m_id; }
        set { m_id = value < 1 ? value - 1 : value; }
    }
    public long IdGreaterThan {
        get { return m_idGreaterThan < 0 ? m_idGreaterThan + 1 : m_idGreaterThan; }
        set { m_idGreaterThan = value < 1 ? value - 1 : value; }
    }
    public long IdLessThan {
        get { return m_idLessThan < 0 ? m_idLessThan + 1 : m_idLessThan; }
        set { m_idLessThan = value < 1 ? value - 1 : value; }
    }

    // *** Query
    public bool Apply(IObject ido) {
        // TODO
        throw new System.NotImplementedException();
    }

    // TODO Contstructors
}
