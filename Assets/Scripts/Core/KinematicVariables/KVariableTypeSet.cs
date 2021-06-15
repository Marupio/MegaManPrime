using UnityEngine;

public class KVariableTypeSet {
    bool m_extendedAllowed = true;
    bool m_singularOnly = false;
    System.UInt32 m_value;

    public System.UInt32 Value {
        get => m_value;
        set {
            if (!m_extendedAllowed) {
                KVariableTypeSet kv = new KVariableTypeSet(value);
                if (kv.HasExtendedVariables()) {
                    Debug.LogError("Attempting to set value for extended variable types when this is disallowed, ignoring");
                    return;
                }
            }
            if (m_singularOnly) {
                KVariableTypeSet kv = new KVariableTypeSet(value);
                if (kv.Count + Count > 1 && kv != this) {
                    Debug.LogError("Attempting to set value to multiple variable types for a single-only type, ignoring");
                    return;
                }
            }
            m_value = value;
        }
    }

    // *** Special properties
    public bool ExtendedAllowed {
        get=>m_extendedAllowed;
        set {
            if (HasExtendedVariables() && !value) {
                Debug.LogError("Attempting to disallow extended when Extended exists already");
            }
            m_extendedAllowed = value;
        }
    }
    public bool HasExtendedVariables() {
        return this >= KVariableTypeInfo.ThirdDerivative;
    }
    public bool HasBaseVariables() {
        return this == KVariableTypeInfo.Variable || this == KVariableTypeInfo.Derivative || this == KVariableTypeInfo.SecondDerivative ||
            this == KVariableTypeInfo.AppliedForce || this == KVariableTypeInfo.ImpulseForce || this == KVariableTypeInfo.Drag;
    }
    public bool SingularOnly {
        get => m_singularOnly;
        set {
            if (Count > 1 && value)  {
                Debug.LogError("Attempting to enforce Singular when multiple variables already exist");
            }
            m_singularOnly = value;
        }
    }
    public bool IsSingular() { return Count == 1; }

    // *** Query
    public int Count {
        get {
            int nFound = 0;
            nFound += Contains(KVariableTypeInfo.Variable) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.Derivative) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.SecondDerivative) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.AppliedForce) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.ImpulseForce) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.Drag) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.ThirdDerivative) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.AppliedForceDerivative) ? 1 : 0;
            nFound += Contains(KVariableTypeInfo.ImpulseForceDerivative) ? 1 : 0;
            return nFound;
        }
    }
    public bool Contains(KVariableTypeSet kv) {
        return (this & kv) == kv;
    }

    // *** Edit
    public void Add(KVariableTypeSet kv) {
        if (m_singularOnly && Count > 0) {
            Debug.LogError("Attempting to add variable type to singular variable type set. Ignoring.");
            return;
        }
        if (!m_extendedAllowed && kv.HasExtendedVariables()) {
            Debug.LogError("Attempting to add extended variable types to a base-only variable type set. Ignoring.");
            return;
        }
        m_value = kv.m_value | m_value;
    }
    public void Remove(KVariableTypeSet kv) {
        m_value = (this & ~kv).Value;
    }

    // *** Operators
    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        KVariableTypeSet kvo = obj as KVariableTypeSet;
        if (kvo == null) {
            return false;
        }
        return kvo.m_value == m_value;
    }
    public override int GetHashCode() {
        return m_value.GetHashCode();
    }
    public static bool operator==(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return kvA.m_value == kvB.m_value;
    }
    public static bool operator!=(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return kvA.m_value != kvB.m_value;
    }
    public static bool operator<=(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return kvA.m_value <= kvB.m_value;
    }
    public static bool operator>=(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return kvA.m_value >= kvB.m_value;
    }
    public static bool operator<(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return kvA.m_value < kvB.m_value;
    }
    public static bool operator>(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return kvA.m_value > kvB.m_value;
    }
    public static KVariableTypeSet operator|(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return new KVariableTypeSet(kvA.m_value | kvB.m_value);
    }
    public static KVariableTypeSet operator&(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return new KVariableTypeSet(kvA.m_value & kvB.m_value);
    }
    public static KVariableTypeSet operator^(KVariableTypeSet kvA, KVariableTypeSet kvB) {
        return new KVariableTypeSet(kvA.m_value ^ kvB.m_value);
    }
    public static KVariableTypeSet operator~(KVariableTypeSet kv) {
        return kv^KVariableTypeInfo.AllTypes;
    }

    // *** Cast operators
    public static implicit operator KVariableEnum(KVariableTypeSet kv) {
        if (kv.Count > 1) {
            Debug.LogError("Attempting to cast multi-typed KinematicVariableTypeSet to base enumeration");
        }
        if (kv.HasExtendedVariables()) {
            Debug.LogError("Attempting to cast extended KinematicVariableTypeSet to base enumeration");
        }
        return (KVariableEnum)kv.Value;
    }
    public static implicit operator KVariableExtendedEnum(KVariableTypeSet kv) {
        if (kv.Count > 1) {
            Debug.LogError("Attempting to cast multi-typed KinematicVariableTypeSet to base enumeration");
        }
        if (kv.HasBaseVariables()) {
            Debug.LogError("Attempting to cast base KinematicVariableTypeSet to extended enumeration");
        }
        return (KVariableExtendedEnum)kv.Value;
    }
    // TODO - I cannot get this one to work
    //public static explicit operator KinematicVariableTypeSet(KinematicVariableEnum enum) => new KinematicVariableTypeSet(enum);

    // *** Constructors
    public KVariableTypeSet(KVariableTypeSet kv) {
        m_extendedAllowed = kv.m_extendedAllowed;
        m_singularOnly = kv.m_singularOnly;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(KVariableTypeSet kv, bool singularOnly) {
        if (singularOnly) {
            if (kv.Count > 1) {
                Debug.LogError("Attempting to construct multiple variable set as a singular variable type set");
            }
        }
        m_extendedAllowed = kv.m_extendedAllowed;
        m_singularOnly = singularOnly;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(KVariableTypeSet kv, bool singularOnly, bool extendedAllowed) {
        if (singularOnly && kv.Count > 1) {
            Debug.LogError("Attempting to construct multiple variable set as a singular variable type set");
        }
        if (!extendedAllowed && kv.HasExtendedVariables()) {
            Debug.LogError("Attempting to construct variable set with extended types as a base variable type set");
        }
        m_extendedAllowed = extendedAllowed;
        m_singularOnly = singularOnly;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(System.UInt32 value, bool singularOnly = false, bool extendedAllowed = true) {
        KVariableTypeSet kv = new KVariableTypeSet(value);
        if (singularOnly && kv.Count > 1) {
            Debug.LogError("Attempting to construct multiple variable set as a singular variable type set");
        }
        if (!extendedAllowed && kv.HasExtendedVariables()) {
            Debug.LogError("Attempting to construct variable set with extended types as a base variable type set");
        }
        m_extendedAllowed = extendedAllowed;
        m_singularOnly = singularOnly;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(KVariableEnum enumValue, bool singularOnly = false, bool extendedAllowed = true) {
        // No need to check flags, as this is definetely a singular base type
        m_value = (System.UInt32)enumValue;
        m_singularOnly = singularOnly;
        m_extendedAllowed = extendedAllowed;
    }
    public KVariableTypeSet(KVariableExtendedEnum enumValue, bool singularOnly = false, bool extendedAllowed = true) {
        if (!extendedAllowed) {
            Debug.LogError("Attempting to construct a base variable type set from an extended type set enum");
        }
        m_value = (System.UInt32)enumValue;
        m_singularOnly = singularOnly;
        m_extendedAllowed = extendedAllowed;
    }
    public KVariableTypeSet(string name, bool singularOnly = false, bool extendedAllowed = true) {
        m_singularOnly = singularOnly;
        m_extendedAllowed = extendedAllowed;
        KVariableEnum baseEnum;
        if (KVariableTypeInfo.BaseAliases.TryGetValue(name, out baseEnum)) {
            m_value = (System.UInt32)baseEnum;
        } else {
            KVariableExtendedEnum extEnum;
            if (KVariableTypeInfo.ExtendedAliases.TryGetValue(name, out extEnum)) {
                if (!extendedAllowed) {
                    Debug.LogError("Attempting to construct a baseVariable type set from an extended type set alias");
                }
                m_value = (System.UInt32)baseEnum;
            } else {
                Debug.LogError("Name does not match a kinematic variable alias");
                m_value = (System.UInt32)KVariableEnum.None;
            }
        }
    }
    public KVariableTypeSet() {
        m_value = (System.UInt32)KVariableEnum.None;
        m_extendedAllowed = true;
        m_singularOnly = false;
    }
}