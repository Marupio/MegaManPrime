using UnityEngine;

public class KVariableTypeSet {
    bool m_singularOnly = false;
    System.Int32 m_value;

    public System.Int32 Value {
        get => m_value;
        set {
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
    public bool ForceUser() {
        return (KVariableTypeInfo.AllForceTypes & this).Count > 0;
    }
    public bool StateSetter() {
        return (KVariableTypeInfo.AllStateSetterTypes & this).Count > 0;
    }
    public bool IsSingular() { return Count <= 1; }
    public bool SingularOnly { get => m_singularOnly; }
    // Once set, cannot be unset
    public void SetSingularOnly() {
        if (Count > 1)  {
            Debug.LogError("Attempting to enforce Singular when multiple variables already exist");
        }
        m_singularOnly = true;
    }

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
    public bool Contains(KVariableEnum kve) {
        KVariableTypeSet kvts = new KVariableTypeSet(kve);
        return Contains(kvts);
    }

    // *** Edit
    public void Add(KVariableTypeSet kv) {
        if (m_singularOnly && Count > 0) {
            Debug.LogError("Attempting to add variable type to singular variable type set. Ignoring.");
            return;
        }
        m_value = kv.m_value | m_value;
    }
    public void Remove(KVariableTypeSet kv) {
        m_value = (this & ~kv).Value;
    }


    public override string ToString() {
        string outputString = "KVset(" + m_value + "): {";
        for (System.Int32 i = 1; i < KVariableTypeInfo.NBaseEnums; ++i) {
            KVariableEnum curEnum = (KVariableEnum)i;
            if (Contains(curEnum)) {outputString += curEnum + " ";}
        }
        outputString += "}";
        return outputString;
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
            Debug.LogError("Attempting to cast multi-typed KinematicVariableTypeSet to enumeration");
        }
        return (KVariableEnum)kv.Value;
    }
    // TODO - I cannot get this one to work
    //public static explicit operator KinematicVariableTypeSet(KinematicVariableEnum enum) => new KinematicVariableTypeSet(enum);

    // *** Constructors
    public KVariableTypeSet(KVariableTypeSet kv) {
        m_singularOnly = kv.m_singularOnly;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(KVariableTypeSet kv, bool singularOnly) {
        if (singularOnly) {
            if (kv.Count > 1) {
                Debug.LogError("Attempting to construct multiple variable set as a singular variable type set");
            }
        }
        m_singularOnly = singularOnly;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(System.Int32 value, bool singularOnly = false) {
        if (value < 0 || value > KVariableTypeInfo.MaxValue) {
            Debug.LogError("value " + value + " out of range : 0 .. " + KVariableTypeInfo.MaxValue + ". Setting to zero.");
            value = 0;
        }
        m_value = value;
        if (singularOnly && Count > 1) {
            Debug.LogError("Attempting to construct multiple variable set as a singular variable type set");
        }
        m_singularOnly = singularOnly;
    }
    public KVariableTypeSet(KVariableEnum enumValue, bool singularOnly = false, bool extendedAllowed = true) {
        // No need to check flags, as this is definetely a singular type
        m_value = (System.Int32)enumValue;
        m_singularOnly = singularOnly;
    }
    public KVariableTypeSet(string name, bool singularOnly = false, bool extendedAllowed = true) {
        m_singularOnly = singularOnly;
        KVariableEnum baseEnum;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out baseEnum)) {
            m_value = (System.Int32)baseEnum;
        } else {
            Debug.LogError("Name does not match a kinematic variable alias");
            m_value = (System.Int32)KVariableEnum.None;
        }
    }
    public KVariableTypeSet() {
        m_value = (System.Int32)KVariableEnum.None;
        m_singularOnly = false;
    }
}
