/// <summary>
/// Modification tag, assigned incrementally.
/// Basically just a wrapped long now.  Was more.  Can be more.  But just a long right now.
/// </summary>
public struct ModTag {
    long m_tag;
    public long Tag { get => m_tag; set => m_tag=value; }
    public ModTag(long tag) {
        m_tag = tag;
    }
    // *** Static operators
    public override bool Equals(object obj) {
        if (!(obj is ModTag))
           return false;

        ModTag mt = (ModTag)obj;
        if (mt.m_tag == m_tag) {
            return true;
        }
        return false;
    }
    public override int GetHashCode() { return m_tag.GetHashCode(); }
    public static bool operator== (ModTag lhs, ModTag rhs) { return lhs.m_tag == rhs.m_tag; }
    public static bool operator!= (ModTag lhs, ModTag rhs) { return lhs.m_tag != rhs.m_tag; }
    public static bool operator>= (ModTag lhs, ModTag rhs) { return lhs.m_tag >= rhs.m_tag; }
    public static bool operator<= (ModTag lhs, ModTag rhs) { return lhs.m_tag <= rhs.m_tag; }
    public static bool operator> (ModTag lhs, ModTag rhs) { return lhs.m_tag > rhs.m_tag; }
    public static bool operator< (ModTag lhs, ModTag rhs) { return lhs.m_tag < rhs.m_tag; }
    public static ModTag operator+ (ModTag lhs, ModTag rhs) { return new ModTag(lhs.m_tag + lhs.m_tag);}
    public static ModTag operator- (ModTag lhs, ModTag rhs) { return new ModTag(lhs.m_tag - lhs.m_tag);}
    public static ModTag operator++(ModTag mt) { return new ModTag(mt.m_tag + 1);}
    public static ModTag operator--(ModTag mt) { return new ModTag(mt.m_tag - 1);}
}
