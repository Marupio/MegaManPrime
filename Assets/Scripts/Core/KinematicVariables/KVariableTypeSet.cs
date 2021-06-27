using System;
using UnityEngine;

public enum KVariableRestriction {
    None                 = 0b_0000_0000,
    Single             = 0b_0000_0001,
    Controllable         = 0b_0000_0010,
    SingleControllable = 0b_0000_0011
}

public class KVariableTypeSet {
    KVariableRestriction m_restriction;
    System.Int32 m_value;

    public System.Int32 Value {
        get => m_value;
        set {
            if ((m_restriction & KVariableRestriction.Single) == KVariableRestriction.Single) {
                KVariableTypeSet kv = new KVariableTypeSet(value);
                if (kv.Count > 1) {
                    Debug.LogError("Attempting to set value to multiple variable types for a single-only type, ignoring");
                    return;
                }
            }
            if ((m_restriction & KVariableRestriction.Controllable) == KVariableRestriction.Controllable) {
                KVariableTypeSet kv = new KVariableTypeSet(value);
                if (!kv.HasOnlyControllable()) {
                    Debug.LogError("Attempting to set a non-controllable variable type to a controllable-only variable type set, ignoring");
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
        return (KVariableTypeInfo.AllNonForceTypes & this).Count > 0;
    }

    // *** Restrictions
    // -** Query restrictions
    public bool RestrictedToSingle { get => (m_restriction & KVariableRestriction.Single) == KVariableRestriction.Single; }
    public bool RestrictedToControllable { get => (m_restriction & KVariableRestriction.Controllable) == KVariableRestriction.Controllable; }

    // -** Restriction conformity queries
    public bool IsSingle() { return Count <= 1; }
    public bool IsMultiple() { return Count > 1; }
    public bool HasControllable() {
        return (this & KVariableTypeInfo.AllControllableTypes) != KVariableTypeInfo.None;
    }
    public bool HasOnlyControllable() {
        return (this & KVariableTypeInfo.AllNonControllableTypes) == KVariableTypeInfo.None;
    }
    public bool HasNonControllable() {
        return (this & ~KVariableTypeInfo.AllControllableTypes) != KVariableTypeInfo.None;
    }
    public bool HasOnlyNonControllable() {
        return (this & KVariableTypeInfo.AllControllableTypes) == KVariableTypeInfo.None;
    }

    // -** Removing restrictions
    public void RemoveSingleRestriction() {
        m_restriction = RestrictedToControllable ? KVariableRestriction.Controllable : KVariableRestriction.None;
    }
    public void RemoveControllableRestriction() {
        m_restriction = RestrictedToSingle ? KVariableRestriction.Single : KVariableRestriction.None;
    }
    public void RemoveAllRestrictions() {
        m_restriction = KVariableRestriction.None;
    }

    // -** Applying restrictions
    public void SetRestrictionToSingle() {
        if (Count > 1)  {
            Debug.LogError("Attempting to enforce Single when multiple variables already exist");
        }
        m_restriction = (m_restriction | KVariableRestriction.Single);
    }
    public void SetRestrictionToControllable() {
        if (!HasOnlyControllable())  {
            Debug.LogWarning("Attempting to set ControllableOnly when non-controllable variables already exist - purging them");
            m_value = (this & KVariableTypeInfo.AllControllableTypes).m_value;
        }
        m_restriction = (m_restriction | KVariableRestriction.Controllable);
    }

    // *** Query
    public int Count {
        get {
            int nFound = 0;
            nFound += ContainsAll(KVariableTypeInfo.Variable) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.Derivative) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.SecondDerivative) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.AppliedForce) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.ImpulseForce) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.Drag) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.ThirdDerivative) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.AppliedForceDerivative) ? 1 : 0;
            nFound += ContainsAll(KVariableTypeInfo.ImpulseForceDerivative) ? 1 : 0;
            return nFound;
        }
    }
    public bool ContainsAny(KVariableTypeSet kv) {
        return (this & kv).Count > 0;
    }
    public bool ContainsAll(KVariableTypeSet kv) {
        return (this & kv) == kv;
    }
    public bool Contains(KVariableEnum kve) {
        KVariableTypeSet kvts = new KVariableTypeSet(kve);
        return ContainsAll(kvts);
    }
    public bool Contains(KVariableEnum_Controllable kve) {
        KVariableTypeSet kvts = new KVariableTypeSet(kve);
        return ContainsAll(kvts);
    }

    // *** Edit
    public int Add(KVariableTypeSet kv) {
        int countBefore = Count;
        if (RestrictedToSingle && Count > 0) {
            Debug.LogError("Attempting to add variable type to Single variable type set. Ignoring.");
            return 0;
        }
        if (RestrictedToControllable && kv.HasNonControllable()) {
            Debug.LogError("Attempting to add non-controllable variable type to a controllable-only variable type set. Ignoring.");
            return 0;
        }
        m_value = kv.m_value | m_value;
        return Count - countBefore;
    }
    public int Add(int value) {
        int countBefore = Count;
        if (value < 0 || value > KVariableTypeInfo.MaxValue) {
            Debug.LogError("Value " + value + " outside of range 0 .. " + KVariableTypeInfo.MaxValue);
            return 0;
        }
        KVariableTypeSet kv = new KVariableTypeSet();
        kv.m_value = value + m_value;
        if (RestrictedToSingle && kv.Count > 1) {
            Debug.LogError("Attempting to add variable to Single variable typeset and the result would not be Single, ignoring.");
            return 0;
        }
        if (RestrictedToControllable && kv.HasNonControllable()) {
            Debug.LogError("Attempting to add non-controllable type to controllable-only variable set, ignoring.");
            return 0;
        }
        m_value = m_value + value;
        return Count - countBefore;
    }
    public int Add(KVariableEnum value) {
        return Add((int)value);
    }
    public int Add(KVariableEnum_Controllable value) {
        return Add((int)value);
    }
    public int Add(string name) {
        return Add(KVariableTypeInfo.EnumFromName(name));
    }
    public int Remove(KVariableTypeSet kv) {
        int countBefore = Count;
        m_value = (this & ~kv).Value;
        return countBefore - Count;
    }
    public int Remove(int value) {
        int countBefore = Count;
        KVariableTypeSet kv = new KVariableTypeSet(value);
        m_value = (this & ~kv).m_value;
        return countBefore - Count;
    }
    public int Remove(KVariableEnum value) {
        return Remove((int)value);
    }
    public int Remove(KVariableEnum_Controllable value) {
        return Remove((int)value);
    }
    public int Remove(string name) {
        return Remove(KVariableTypeInfo.EnumFromName(name));
    }

    // TODO - add formated methods
    public override string ToString() {
        string outputString = "KVset(" + m_value + "): {";
        for (System.Int32 i = 1; i < KVariableTypeInfo.NKVariableEnum_Controllable; ++i) {
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
            Debug.LogError("Attempting to cast multi-typed KVariableTypeSet to enumeration");
        }
        return (KVariableEnum)kv.Value;
    }
    public static implicit operator KVariableEnum_Controllable(KVariableTypeSet kv) {
        if (kv.Count > 1) {
            Debug.LogError("Attempting to cast multi-typed KVariableTypeSet to controllable kvariable enumeration");
        }
        return (KVariableEnum_Controllable)kv.Value;
    }
    // TODO - I cannot get this one to work
    //public static explicit operator KinematicVariableTypeSet(KinematicVariableEnum enum) => new KinematicVariableTypeSet(enum);

    // *** Constructors
    // TODO - add restriction enum throughout
    public KVariableTypeSet(KVariableTypeSet kv) {
        m_restriction = kv.m_restriction;
        m_value = kv.m_value;
    }
    public KVariableTypeSet(KVariableTypeSet kv, KVariableRestriction restriction) {
        m_restriction = restriction;
        Add(kv);
    }
    public KVariableTypeSet(int value, KVariableRestriction restriction = KVariableRestriction.None) {
        m_restriction = restriction;
        Add(value);
    }
    public KVariableTypeSet(KVariableEnum enumValue, KVariableRestriction restriction = KVariableRestriction.None) {
        m_restriction = restriction;
        Add(enumValue);
    }
    public KVariableTypeSet(KVariableEnum_Controllable enumValue, KVariableRestriction restriction = KVariableRestriction.None) {
        m_restriction = restriction;
        Add(enumValue);
    }
    public KVariableTypeSet(string name, KVariableRestriction restriction = KVariableRestriction.None) {
        m_restriction = restriction;
        Add(name);
    }
    public KVariableTypeSet(KVariableRestriction restriction = KVariableRestriction.None) {
        m_value = (System.Int32)KVariableEnum.None;
        m_restriction = restriction;
    }
}
