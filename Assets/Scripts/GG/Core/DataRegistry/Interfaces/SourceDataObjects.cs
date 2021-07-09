using UnityEngine;

public abstract class SourceDataObj<L> : DataObjHeader, IDataObj<L> {
    public L m_data;
    public virtual ITraitsSimple<L> TraitsSimple { get; }
    public L Data { get=>m_data; set { TraitsSimple.SetEqual(ref m_data, value); SetModified(); }  }
    public SourceDataObj(
        string name,
        IObjRegistry parent = null,
        L data = default(L)
    ) : base (name, parent) { TraitsSimple.SetEqual(ref m_data, data); }
    public SourceDataObj(SourceDataObj<L> obj) : base(obj) {
        TraitsSimple.SetEqual(ref m_data, obj.m_data);
    }
    public SourceDataObj() {}
}
// TEMPLATE
// public class $PREFIX_1SourceDataObj : SourceDataObj<$TYPE_1> {
//     public static readonly TraitsSimple$PREFIX_2 m_traitsSimple = new TraitsSimple$PREFIX_3();
//     public override ITraitsSimple<$TYPE_2> TraitsSimple { get=>m_traitsSimple; }
//     public override DataTypeEnum DataType { get=>$DATA_TYPE_ENUM; }
//     public override IObj Clone(IObjRegistry parent = null) {
//         $PREFIX_4SourceDataObj obj = new $PREFIX_5SourceDataObj(this);
//         if (parent != null) {
//             obj.UnregisterFromParent();
//             obj.RegisterToParent(parent);
//         }
//         return (IObj)obj;
//     }
//     public $PREFIX_6SourceDataObj(string name, IObjRegistry parent = null, $TYPE_3 value = false)
//     : base (name, parent) {}
//     public $PREFIX_7SourceDataObj($PREFIX_8SourceDataObj obj) : base(obj) {}
//     public $PREFIX_9SourceDataObj() {}
// }

public class NoneSourceDataObj : SourceDataObj<object> {
    public static readonly TraitsSimpleNone m_traitsSimple = new TraitsSimpleNone();
    public override ITraitsSimple<object> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public override IObj Clone(IObjRegistry parent = null) {
        NoneSourceDataObj obj = new NoneSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public NoneSourceDataObj(string name, IObjRegistry parent = null, object value = false)
    : base (name, parent) {}
    public NoneSourceDataObj(NoneSourceDataObj obj) : base(obj) {}
    public NoneSourceDataObj() {}
}
public class TriggerSourceDataObj : SourceDataObj<Trigger> {
    public static readonly TraitsSimpleTrigger m_traitsSimple = new TraitsSimpleTrigger();
    public override ITraitsSimple<Trigger> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.TriggerType; }
    public override IObj Clone(IObjRegistry parent = null) {
        TriggerSourceDataObj obj = new TriggerSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public TriggerSourceDataObj(string name, IObjRegistry parent = null, Trigger value = new Trigger())
    : base (name, parent) {}
    public TriggerSourceDataObj(TriggerSourceDataObj obj) : base(obj) {}
    public TriggerSourceDataObj() {}
}
public class BoolSourceDataObj : SourceDataObj<bool> {
    public static readonly TraitsSimpleBool m_traitsSimple = new TraitsSimpleBool();
    public override ITraitsSimple<bool> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
    public override IObj Clone(IObjRegistry parent = null) {
        BoolSourceDataObj obj = new BoolSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public BoolSourceDataObj(string name, IObjRegistry parent = null, bool value = false)
    : base (name, parent) {}
    public BoolSourceDataObj(BoolSourceDataObj obj) : base(obj) {}
    public BoolSourceDataObj() {}
}
public class CharSourceDataObj : SourceDataObj<char> {
    public static readonly TraitsSimpleChar m_traitsSimple = new TraitsSimpleChar();
    public override ITraitsSimple<char> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Char; }
    public override IObj Clone(IObjRegistry parent = null) {
        CharSourceDataObj obj = new CharSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public CharSourceDataObj(string name, IObjRegistry parent = null, char value = '\0')
    : base (name, parent) {}
    public CharSourceDataObj(CharSourceDataObj obj) : base(obj) {}
    public CharSourceDataObj() {}
}
public class StringSourceDataObj : SourceDataObj<string> {
    public static readonly TraitsSimpleString m_traitsSimple = new TraitsSimpleString();
    public override ITraitsSimple<string> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.String; }
    public override IObj Clone(IObjRegistry parent = null) {
        StringSourceDataObj obj = new StringSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public StringSourceDataObj(string name, IObjRegistry parent = null, string value = "")
    : base (name, parent) {}
    public StringSourceDataObj(StringSourceDataObj obj) : base(obj) {}
    public StringSourceDataObj() {}
}
public class IntSourceDataObj : SourceDataObj<int> {
    public static readonly TraitsSimpleInt m_traitsSimple = new TraitsSimpleInt();
    public override ITraitsSimple<int> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Int; }
    public override IObj Clone(IObjRegistry parent = null) {
        IntSourceDataObj obj = new IntSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public IntSourceDataObj(string name, IObjRegistry parent = null, int value = 0)
    : base (name, parent) {}
    public IntSourceDataObj(IntSourceDataObj obj) : base(obj) {}
    public IntSourceDataObj() {}
}
public class FloatSourceDataObj : SourceDataObj<float> {
    public static readonly TraitsSimpleFloat m_traitsSimple = new TraitsSimpleFloat();
    public override ITraitsSimple<float> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Float; }
    public override IObj Clone(IObjRegistry parent = null) {
        FloatSourceDataObj obj = new FloatSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public FloatSourceDataObj(string name, IObjRegistry parent = null, float value = 0f)
    : base (name, parent) {}
    public FloatSourceDataObj(FloatSourceDataObj obj) : base(obj) {}
    public FloatSourceDataObj() {}
}
public class Vector2IntSourceDataObj : SourceDataObj<Vector2Int> {
    public static readonly TraitsSimpleVector2Int m_traitsSimple = new TraitsSimpleVector2Int();
    public override ITraitsSimple<Vector2Int> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector2IntType; }
    public override IObj Clone(IObjRegistry parent = null) {
        Vector2IntSourceDataObj obj = new Vector2IntSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector2IntSourceDataObj(string name, IObjRegistry parent = null, Vector2Int value = default(Vector2Int))
    : base (name, parent) {}
    public Vector2IntSourceDataObj(Vector2IntSourceDataObj obj) : base(obj) {}
    public Vector2IntSourceDataObj() {}
}
public class Vector2SourceDataObj : SourceDataObj<Vector2> {
    public static readonly TraitsSimpleVector2 m_traitsSimple = new TraitsSimpleVector2();
    public override ITraitsSimple<Vector2> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector2Type; }
    public override IObj Clone(IObjRegistry parent = null) {
        Vector2SourceDataObj obj = new Vector2SourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector2SourceDataObj(string name, IObjRegistry parent = null, Vector2 value = default(Vector2))
    : base (name, parent) {}
    public Vector2SourceDataObj(Vector2SourceDataObj obj) : base(obj) {}
    public Vector2SourceDataObj() {}
}
public class Vector3IntSourceDataObj : SourceDataObj<Vector3Int> {
    public static readonly TraitsSimpleVector3Int m_traitsSimple = new TraitsSimpleVector3Int();
    public override ITraitsSimple<Vector3Int> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector3IntType; }
    public override IObj Clone(IObjRegistry parent = null) {
        Vector3IntSourceDataObj obj = new Vector3IntSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector3IntSourceDataObj(string name, IObjRegistry parent = null, Vector3Int value = default(Vector3Int))
    : base (name, parent) {}
    public Vector3IntSourceDataObj(Vector3IntSourceDataObj obj) : base(obj) {}
    public Vector3IntSourceDataObj() {}
}
public class Vector3SourceDataObj : SourceDataObj<Vector3> {
    public static readonly TraitsSimpleVector3 m_traitsSimple = new TraitsSimpleVector3();
    public override ITraitsSimple<Vector3> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector3Type; }
    public override IObj Clone(IObjRegistry parent = null) {
        Vector3SourceDataObj obj = new Vector3SourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector3SourceDataObj(string name, IObjRegistry parent = null, Vector3 value = default(Vector3))
    : base (name, parent) {}
    public Vector3SourceDataObj(Vector3SourceDataObj obj) : base(obj) {}
    public Vector3SourceDataObj() {}
}
public class Vector4SourceDataObj : SourceDataObj<Vector4> {
    public static readonly TraitsSimpleVector4 m_traitsSimple = new TraitsSimpleVector4();
    public override ITraitsSimple<Vector4> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.Vector4Type; }
    public override IObj Clone(IObjRegistry parent = null) {
        Vector4SourceDataObj obj = new Vector4SourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public Vector4SourceDataObj(string name, IObjRegistry parent = null, Vector4 value = default(Vector4))
    : base (name, parent) {}
    public Vector4SourceDataObj(Vector4SourceDataObj obj) : base(obj) {}
    public Vector4SourceDataObj() {}
}
public class QuaternionSourceDataObj : SourceDataObj<Quaternion> {
    public static readonly TraitsSimpleQuaternion m_traitsSimple = new TraitsSimpleQuaternion();
    public override ITraitsSimple<Quaternion> TraitsSimple { get=>m_traitsSimple; }
    public override DataTypeEnum DataType { get=>DataTypeEnum.QuaternionType; }
    public override IObj Clone(IObjRegistry parent = null) {
        QuaternionSourceDataObj obj = new QuaternionSourceDataObj(this);
        if (parent != null) {
            obj.UnregisterFromParent();
            obj.RegisterToParent(parent);
        }
        return (IObj)obj;
    }
    public QuaternionSourceDataObj(string name, IObjRegistry parent = null, Quaternion value = default(Quaternion))
    : base (name, parent) {}
    public QuaternionSourceDataObj(QuaternionSourceDataObj obj) : base(obj) {}
    public QuaternionSourceDataObj() {}
}
