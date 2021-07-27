using System.Text.RegularExpressions;

/// <summary>
/// Used to select a single or multiple DataObjs
/// </summary>

public struct ObjFilter {

    public enum NameCriterionEnum {
        None,
        Exact,
        Contains,
        Regex,
        RegexCaseInsensitive
    }
    public enum TypeNameCriterionEnum {
        None,
        Exact,
        Contains,
        Regex,
        RegexCaseInsensitive
    }
    public enum IdCriterion {
        None,
        Exact,
        GreaterThan,
        LessThan
    }

    // Name fields
    private string m_name;
    private Regex m_nameRegex;
    private NameCriterionEnum m_nameCriterion;

    // TypeName fields
    private string m_typeName;
    private Regex m_typeNameRegex;
    private TypeNameCriterionEnum m_typeNameCriterion;

    // Id fields
    // Internally: id longs - we use 0 == filter not applied (because I'm a struct)
    // all negatives (and zero) are shifted -= 1 because GlobalRegistrar.IdAnonymous is long.MinValue
    private long m_id;
    private IdCriterion m_idCriterion;

    // General constraint
    IObjPass<IObj> m_constraint;

    // *** Access
    public string Name {
        get => m_name;
        set {
            m_name=value;
            UpdateNameRegex();
        }
    }
    public NameCriterionEnum NameCriterion {
        get => m_nameCriterion;
        set {
            m_nameCriterion = value;
            UpdateNameRegex();
        }
    }
    public string TypeName {
        get => m_typeName;
        set {
            m_typeName=value;
            UpdateTypeNameRegex();
        }
    }
    public TypeNameCriterionEnum TypeNameCriterion {
        get => m_typeNameCriterion;
        set {
            m_typeNameCriterion = value;
            UpdateTypeNameRegex();
        }
    }
    public long Id {
        get { return m_id < 0 ? m_id + 1 : m_id; }
        set { m_id = value < 1 ? value - 1 : value; }
    }
    public IdCriterion IdType { get => m_idCriterion; set => m_idCriterion = value; }

    public IObjPass<IObj> Constraint { get => m_constraint; set => m_constraint = value;}

    // *** Query
    public bool Pass(IObj obj) {
        // Check name
        switch (m_nameCriterion) {
            case NameCriterionEnum.None:
                // Do nothing
                break;
            case NameCriterionEnum.Exact:
                if (obj.Name != m_name) {
                    return false;
                }
                break;
            case NameCriterionEnum.Contains:
                if (!obj.Name.Contains(m_name)) {
                    return false;
                }
                break;
            case NameCriterionEnum.Regex:
            case NameCriterionEnum.RegexCaseInsensitive:
                if (!m_nameRegex.Match(obj.Name).Success) {
                    return false;
                }
                break;
        }

        // Check typeName
        switch (m_typeNameCriterion) {
            case TypeNameCriterionEnum.None:
                // Do nothing
                break;
            case TypeNameCriterionEnum.Exact:
                if (obj.GetType().Name != m_typeName) {
                    return false;
                }
                break;
            case TypeNameCriterionEnum.Contains:
                if (!obj.GetType().Name.Contains(m_typeName)) {
                    return false;
                }
                break;
            case TypeNameCriterionEnum.Regex:
            case TypeNameCriterionEnum.RegexCaseInsensitive:
                if (!m_typeNameRegex.Match(obj.GetType().Name).Success) {
                    return false;
                }
                break;
        }

        // Check id
        switch (m_idCriterion) {
            case IdCriterion.None:
                // Do nothing
                break;
            case IdCriterion.Exact:
                if (obj.Id != Id) {
                    return false;
                }
                break;
            case IdCriterion.GreaterThan:
                if (obj.Id <= Id) {
                    return false;
                }
                break;
            case IdCriterion.LessThan:
                if (obj.Id >= Id) {
                    return false;
                }
                break;
        }

        // Passed all tests
        if (m_constraint != null) {
            return m_constraint.Pass(obj);
        }
        return true;
    }

    // *** Private methods
    private void UpdateNameRegex() {
        if (m_nameCriterion == NameCriterionEnum.Regex) {
            m_nameRegex = new Regex(m_name, RegexOptions.Compiled);
        } else if (m_nameCriterion == NameCriterionEnum.RegexCaseInsensitive) {
            m_nameRegex = new Regex(m_name, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        } else {
            m_nameRegex = null;
        }
    }
    private void UpdateTypeNameRegex () {
        if (m_typeNameCriterion == TypeNameCriterionEnum.Regex) {
            m_typeNameRegex = new Regex(m_typeName, RegexOptions.Compiled);
        } else if (m_typeNameCriterion == TypeNameCriterionEnum.RegexCaseInsensitive) {
            m_typeNameRegex = new Regex(m_typeName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        } else {
            m_typeNameRegex = null;
        }
    }


    // *** Contstructors
    // Constructors have 3 pairs of arguments: Name+NameCriterion, TypeName+TypeNameCriterion, Id+IdCriterion
    // List them in that order, omit any pair you don't need, and you have a valid constructor
    // If you want to apply a constraint (IObjPass<IObj>), you can add at the end of the arguments
    ObjFilter(
        string name,
        NameCriterionEnum nameCriterion,
        string typeName=null,
        TypeNameCriterionEnum typeNameCriterion=TypeNameCriterionEnum.None,
        long id = 0,
        IdCriterion idCriterion = IdCriterion.None,
        IObjPass<IObj> constraint = null
    ) {
        m_name = name;
        m_nameCriterion = nameCriterion;
        m_typeName = typeName;
        m_typeNameCriterion = typeNameCriterion;
        m_id = id;
        m_idCriterion = idCriterion;
        m_nameRegex = null;
        m_typeNameRegex = null;
        m_constraint = constraint;
        UpdateNameRegex();
        UpdateTypeNameRegex();
    }
    ObjFilter(
        string typeName,
        TypeNameCriterionEnum typeNameCriterion,
        long id = 0,
        IdCriterion idCriterion = IdCriterion.None,
        IObjPass<IObj> constraint = null
    ) {
        m_name = null;
        m_nameCriterion = NameCriterionEnum.None;
        m_typeName = typeName;
        m_typeNameCriterion = typeNameCriterion;
        m_id = id;
        m_idCriterion = idCriterion;
        m_nameRegex = null;
        m_typeNameRegex = null;
        m_constraint = constraint;
        UpdateTypeNameRegex();
    }
    ObjFilter(
        string name,
        NameCriterionEnum nameCriterion,
        long id,
        IdCriterion idCriterion,
        IObjPass<IObj> constraint = null
    ) {
        m_name = name;
        m_nameCriterion = nameCriterion;
        m_typeName = null;
        m_typeNameCriterion = TypeNameCriterionEnum.None;
        m_id = id;
        m_idCriterion = idCriterion;
        m_nameRegex = null;
        m_typeNameRegex = null;
        m_constraint = constraint;
        UpdateNameRegex();
    }
    ObjFilter(
        long id,
        IdCriterion idCriterion,
        IObjPass<IObj> constraint = null
    ) {
        m_name = null;
        m_nameCriterion = NameCriterionEnum.None;
        m_typeName = null;
        m_typeNameCriterion = TypeNameCriterionEnum.None;
        m_id = id;
        m_idCriterion = idCriterion;
        m_nameRegex = null;
        m_typeNameRegex = null;
        m_constraint = constraint;
    }
    ObjFilter(
        IObjPass<IObj> constraint
    ) {
        m_name = null;
        m_nameCriterion = NameCriterionEnum.None;
        m_typeName = null;
        m_typeNameCriterion = TypeNameCriterionEnum.None;
        m_id = 0;
        m_idCriterion = IdCriterion.None;
        m_nameRegex = null;
        m_typeNameRegex = null;
        m_constraint = constraint;
    }
}
