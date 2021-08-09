using System.Collections.Generic;
using UnityEngine;

public class NoneDerivedDataObj : DerivedDataObj<object> {
    public static readonly TraitsSimpleNone m_traitsSimple = new TraitsSimpleNone();
    public override ITraitsSimple<object> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        NoneDerivedDataObj obj = new NoneDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public NoneDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        object data = default(object))
    : base (name, parent, updater, data) {}
    public NoneDerivedDataObj(NoneDerivedDataObj obj) : base(obj) {}
    public NoneDerivedDataObj() {}
}
public class BoolDerivedDataObj : DerivedDataObj<bool> {
    public static readonly TraitsSimpleBool m_traitsSimple = new TraitsSimpleBool();
    public override ITraitsSimple<bool> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        BoolDerivedDataObj obj = new BoolDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public BoolDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        bool data = default(bool))
    : base (name, parent, updater, data) {}
    public BoolDerivedDataObj(BoolDerivedDataObj obj) : base(obj) {}
    public BoolDerivedDataObj() {}
}
public class TriggerDerivedDataObj : DerivedDataObj<Trigger> {
    public static readonly TraitsSimpleTrigger m_traitsSimple = new TraitsSimpleTrigger();
    public override ITraitsSimple<Trigger> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        TriggerDerivedDataObj obj = new TriggerDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public TriggerDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Trigger data = default(Trigger))
    : base (name, parent, updater, data) {}
    public TriggerDerivedDataObj(TriggerDerivedDataObj obj) : base(obj) {}
    public TriggerDerivedDataObj() {}
}
public class CharDerivedDataObj : DerivedDataObj<char> {
    public static readonly TraitsSimpleChar m_traitsSimple = new TraitsSimpleChar();
    public override ITraitsSimple<char> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        CharDerivedDataObj obj = new CharDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public CharDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        char data = default(char))
    : base (name, parent, updater, data) {}
    public CharDerivedDataObj(CharDerivedDataObj obj) : base(obj) {}
    public CharDerivedDataObj() {}
}
public class StringDerivedDataObj : DerivedDataObj<string> {
    public static readonly TraitsSimpleString m_traitsSimple = new TraitsSimpleString();
    public override ITraitsSimple<string> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        StringDerivedDataObj obj = new StringDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public StringDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        string data = default(string))
    : base (name, parent, updater, data) {}
    public StringDerivedDataObj(StringDerivedDataObj obj) : base(obj) {}
    public StringDerivedDataObj() {}
}
public class IntDerivedDataObj : DerivedDataObj<int> {
    public static readonly TraitsSimpleInt m_traitsSimple = new TraitsSimpleInt();
    public override ITraitsSimple<int> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        IntDerivedDataObj obj = new IntDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        int data = default(int))
    : base (name, parent, updater, data) {}
    public IntDerivedDataObj(IntDerivedDataObj obj) : base(obj) {}
    public IntDerivedDataObj() {}
}
public class FloatDerivedDataObj : DerivedDataObj<float> {
    public static readonly TraitsSimpleFloat m_traitsSimple = new TraitsSimpleFloat();
    public override ITraitsSimple<float> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        FloatDerivedDataObj obj = new FloatDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public FloatDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        float data = default(float))
    : base (name, parent, updater, data) {}
    public FloatDerivedDataObj(FloatDerivedDataObj obj) : base(obj) {}
    public FloatDerivedDataObj() {}
}
public class Vector2IntDerivedDataObj : DerivedDataObj<Vector2Int> {
    public static readonly TraitsSimpleVector2Int m_traitsSimple = new TraitsSimpleVector2Int();
    public override ITraitsSimple<Vector2Int> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        Vector2IntDerivedDataObj obj = new Vector2IntDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector2IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Vector2Int data = default(Vector2Int))
    : base (name, parent, updater, data) {}
    public Vector2IntDerivedDataObj(Vector2IntDerivedDataObj obj) : base(obj) {}
    public Vector2IntDerivedDataObj() {}
}
public class Vector2DerivedDataObj : DerivedDataObj<Vector2> {
    public static readonly TraitsSimpleVector2 m_traitsSimple = new TraitsSimpleVector2();
    public override ITraitsSimple<Vector2> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        Vector2DerivedDataObj obj = new Vector2DerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector2DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Vector2 data = default(Vector2))
    : base (name, parent, updater, data) {}
    public Vector2DerivedDataObj(Vector2DerivedDataObj obj) : base(obj) {}
    public Vector2DerivedDataObj() {}
}
public class Vector3IntDerivedDataObj : DerivedDataObj<Vector3Int> {
    public static readonly TraitsSimpleVector3Int m_traitsSimple = new TraitsSimpleVector3Int();
    public override ITraitsSimple<Vector3Int> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        Vector3IntDerivedDataObj obj = new Vector3IntDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector3IntDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Vector3Int data = default(Vector3Int))
    : base (name, parent, updater, data) {}
    public Vector3IntDerivedDataObj(Vector3IntDerivedDataObj obj) : base(obj) {}
    public Vector3IntDerivedDataObj() {}
}
public class Vector3DerivedDataObj : DerivedDataObj<Vector3> {
    public static readonly TraitsSimpleVector3 m_traitsSimple = new TraitsSimpleVector3();
    public override ITraitsSimple<Vector3> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        Vector3DerivedDataObj obj = new Vector3DerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector3DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Vector3 data = default(Vector3))
    : base (name, parent, updater, data) {}
    public Vector3DerivedDataObj(Vector3DerivedDataObj obj) : base(obj) {}
    public Vector3DerivedDataObj() {}
}
public class Vector4DerivedDataObj : DerivedDataObj<Vector4> {
    public static readonly TraitsSimpleVector4 m_traitsSimple = new TraitsSimpleVector4();
    public override ITraitsSimple<Vector4> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        Vector4DerivedDataObj obj = new Vector4DerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector4DerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Vector4 data = default(Vector4))
    : base (name, parent, updater, data) {}
    public Vector4DerivedDataObj(Vector4DerivedDataObj obj) : base(obj) {}
    public Vector4DerivedDataObj() {}
}
public class QuaternionDerivedDataObj : DerivedDataObj<Quaternion> {
    public static readonly TraitsSimpleQuaternion m_traitsSimple = new TraitsSimpleQuaternion();
    public override ITraitsSimple<Quaternion> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public override IObj Clone(IObjRegistry parent = null) {
        // Clones a stale DerivedDataObj
        QuaternionDerivedDataObj obj = new QuaternionDerivedDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public QuaternionDerivedDataObj(
        string name,
        IObjRegistry parent = null,
        IDerivedUpdater updater = null,
        Quaternion data = default(Quaternion))
    : base (name, parent, updater, data) {}
    public QuaternionDerivedDataObj(QuaternionDerivedDataObj obj) : base(obj) {}
    public QuaternionDerivedDataObj() {}
}
