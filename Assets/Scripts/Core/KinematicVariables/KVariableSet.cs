using System.Collections.Generic;
using UnityEngine;

//    1D,2D,3D,3R
// Q = f,v2,v3, q
// V = f,v2,v3,v3
public class KVariableSet<Q,V> {
    protected Q variable;
    protected V derivative;
    protected V secondDerivative;
    protected V thirdDerivative;
    protected V appliedForce;
    protected V impulseForce;
    protected V appliedForceDerivative;
    protected V impulseForceDerivative;
    protected float drag;

    // *** Access
    public Q Variable {get=>variable; set=>variable=value;}
    public V Derivative {get=>derivative; set=>derivative=value;}
    public V SecondDerivative {get=>secondDerivative; set=>secondDerivative=value;}
    public V ThirdDerivative {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V AppliedForce {get=>appliedForce; set=>appliedForce=value;}
    public V ImpulseForce {get=>impulseForce; set=>impulseForce=value;}
    public V AppliedForceDerivative {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V ImpulseForceDerivative {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}
    public float Drag {get=>drag; set=>drag=value;}

    // *** Shameful aliases (for readability / clarity)
    public Q Position_alias {get=>variable; set=>variable=value;}
    public Q Distance_alias {get=>variable; set=>variable=value;}
    public Q Rotation_alias {get=>variable; set=>variable=value;}
    public V Speed_alias           {get=>derivative; set=>derivative=value;}
    public V Velocity_alias        {get=>derivative; set=>derivative=value;}
    public V AngularRotation_alias {get=>derivative; set=>derivative=value;}
    public V Omega_alias           {get=>derivative; set=>derivative=value;}
    public V Acceleration_alias        {get=>secondDerivative; set=>secondDerivative=value;}
    public V AngularAcceleration_alias {get=>secondDerivative; set=>secondDerivative=value;}
    public V OmegaDot_alias            {get=>secondDerivative; set=>secondDerivative=value;}
    public V Jerk_alias        {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V AngularJerk_alias {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V OmegaDotDot_alias {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V Force_alias          {get=>appliedForce; set=>appliedForce=value;}
    public V AppliedTorque_alias  {get=>appliedForce; set=>appliedForce=value;}
    public V Torque_alias         {get=>appliedForce; set=>appliedForce=value;}
    public V Impulse_alias        {get=>impulseForce; set=>impulseForce=value;}
    public V ImpulseTorque_alias  {get=>impulseForce; set=>impulseForce=value;}
    public V AppliedForceRate_alias   {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V AppliedTorqueRate_alias  {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V ForceRate_alias          {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V TorqueRate_alias         {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V ImpulseRate_alias       {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}
    public V ImpulseForceRate_alias  {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}
    public V ImpulseTorqueRate_alias {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}
    public float AngularDrag_alias {get=>drag; set=>drag=value;}

    // *** Edit
    public virtual int ReadScalarDict(Dictionary<string, float> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, float> entry in dict) {
            KVariableEnum type;
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out type)) {
                if (type == KVariableEnum.Drag) {
                    drag = entry.Value;
                    ++nFound;
                } else {
                    Debug.LogWarning("Bad entry for type 'float': " + entry);
                }
            } else {
                Debug.LogWarning("Bad entry for type 'float': " + entry);
            }
        }
        return nFound;
    }
    public virtual int ReadVectorDict(Dictionary<string, V> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, V> entry in dict) {
            KVariableEnum type;
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForceDerivative:
                        impulseForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'V': " + entry);
            }
        }
        return nFound;
    }
    public virtual int ReadQuaternionDict(Dictionary<string, Q> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, Q> entry in dict) {
            KVariableEnum type;
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out type)) {
                if (type == KVariableEnum.Variable) {
                    variable = entry.Value;
                    ++nFound;
                } else {
                    Debug.LogWarning("Bad entry for type 'Q': " + entry);
                }
            } else {
                Debug.LogWarning("Bad entry for type 'Q': " + entry);
            }
        }
        return nFound;
    }

    // *** Set by name
    public virtual bool Set(string name, Q value) {
        KVariableEnum type;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out type) && type == KVariableEnum.Variable) {
            variable = value;
            return true;
        }
        return false;
    }
    public virtual bool Set(string name, V value) {
        KVariableEnum type;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out type)) {
            switch (type) {
                case KVariableEnum.Derivative:
                    derivative = value;
                    return true;
                case KVariableEnum.SecondDerivative:
                    secondDerivative = value;
                    return true;
                case KVariableEnum.ThirdDerivative:
                    thirdDerivative = value;
                    return true;
                case KVariableEnum.AppliedForce:
                    appliedForce = value;
                    return true;
                case KVariableEnum.ImpulseForce:
                    impulseForce = value;
                    return true;
                case KVariableEnum.AppliedForceDerivative:
                    appliedForceDerivative = value;
                    return true;
                case KVariableEnum.ImpulseForceDerivative:
                    impulseForceDerivative = value;
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
    public virtual bool Set(string name, float value) {
        KVariableEnum type;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out type) && type == KVariableEnum.Drag) {
            drag = value;
            return true;
        }
        return false;
    }
    // *** Get by name
    public virtual bool Get(string name, ref Q value) {
        KVariableEnum type;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out type) && type == KVariableEnum.Variable) {
            variable = value;
            return true;
        }
        return false;
    }
    public virtual bool Get(string name, ref V value) {
        KVariableEnum type;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out type)) {
            switch (type) {
                case KVariableEnum.Derivative:
                    value = derivative;
                    return true;
                case KVariableEnum.SecondDerivative:
                    value = secondDerivative;
                    return true;
                case KVariableEnum.ThirdDerivative:
                    value = thirdDerivative;
                    return true;
                case KVariableEnum.AppliedForce:
                    value = appliedForce;
                    return true;
                case KVariableEnum.ImpulseForce:
                    value = impulseForce;
                    return true;
                case KVariableEnum.AppliedForceDerivative:
                    value = appliedForceDerivative;
                    return true;
                case KVariableEnum.ImpulseForceDerivative:
                    value = impulseForceDerivative;
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
    public virtual bool Get(string name, ref float value) {
        KVariableEnum type;
        if (KVariableTypeInfo.Aliases.TryGetValue(name, out type) && type == KVariableEnum.Drag) {
            drag = value;
            return true;
        }
        return false;
    }
   
   // *** Constructors
    public KVariableSet(
        Q variableIn,
        V derivativeIn,
        V secondDerivativeIn,
        V thirdDerivativeIn,
        V appliedForceIn,
        V appliedForceDerivativeIn,
        V impulseForceIn,
        V impulseForceDerivativeIn,
        float dragIn = 0
    ) {
        variable = variableIn;
        derivative = derivativeIn;
        secondDerivative = secondDerivativeIn;
        thirdDerivative = thirdDerivativeIn;
        appliedForce = appliedForceIn;
        appliedForceDerivative = appliedForceDerivativeIn;
        impulseForce = impulseForceIn;
        impulseForceDerivative = impulseForceDerivativeIn;
    }
    public KVariableSet(Dictionary<string, Q> qDict, Dictionary<string, V> vDict, Dictionary<string, float> fDict) {
        if (qDict != null) {
            ReadQuaternionDict(qDict);
        }
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    protected KVariableSet(Dictionary<string, Q> qDict, Dictionary<string, V> vDict, Dictionary<string, float> fDict, bool dontReadYet) { }
    public KVariableSet() { }
}

// Concrete classes
public class KVariableSet1D : KVariableSet<float, float> {
    public override int ReadQuaternionDict(Dictionary<string, float> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, float> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadScalarDict(Dictionary<string, float> dict) {
        int nFound = 0;
        KVariableEnum baseType;
        foreach (KeyValuePair<string, float> entry in dict) {
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForceDerivative:
                        impulseForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Drag:
                        drag = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'float': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'float': " + entry);
            }
        }
        return nFound;
    }
    // *** New constructors
    public KVariableSet1D(float values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // *** Pass on constructors from base
    public KVariableSet1D(
        float variableIn,
        float derivativeIn,
        float secondDerivativeIn,
        float thirdDerivativeIn,
        float appliedForceIn,
        float impulseForceIn,
        float appliedForceDerivativeIn,
        float impulseForceDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        impulseForceIn,
        appliedForceDerivativeIn,
        impulseForceDerivativeIn,
        dragIn
    ) { }
    public KVariableSet1D(Dictionary<string, float> allDicts) {
        ReadScalarDict(allDicts);
    }
    public KVariableSet1D() { }
}
public class KVariableSet2D : KVariableSet<Vector2, Vector2> {
    public override int ReadQuaternionDict(Dictionary<string, Vector2> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector2> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, Vector2> entry in dict) {
            KVariableEnum type;
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'V': " + entry);
            }
        }
        return nFound;
    }
    // *** New constructors
    public KVariableSet2D(Vector2 values, float drag = 0)
    : base(values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KVariableSet2D(
        Vector2 variableIn,
        Vector2 derivativeIn,
        Vector2 secondDerivativeIn,
        Vector2 appliedForceIn,
        Vector2 impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn)
    { }
    public KVariableSet2D(Dictionary<string, Vector2> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KVariableSet2D() { }
}
public class KVariableSet3D : KVariableSet<Vector3, Vector3> {
    public override int ReadQuaternionDict(Dictionary<string, Vector3> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector3> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, Vector3> entry in dict) {
            KVariableEnum type;
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'V': " + entry);
            }
        }
        return nFound;
    }
    // *** New constructors
    public KVariableSet3D(Vector3 values, float drag = 0)
    : base(values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KVariableSet3D(
        Vector3 variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn)
    { }
    public KVariableSet3D(Dictionary<string, Vector3> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KVariableSet3D() { }
}
public class KVariableSet3dRotation : KVariableSet<Quaternion, Vector3> {
    // *** New constructors
    public KVariableSet3dRotation(Quaternion rotation, Vector3 values, float drag = 0)
    : base(rotation, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KVariableSet3dRotation(
        Quaternion variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn)
    { }
    public KVariableSet3dRotation(Dictionary<string, Quaternion> qDict, Dictionary<string, Vector3> vDict, Dictionary<string, float> fDict) {
        if (qDict != null) {
            ReadQuaternionDict(qDict);
        }
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KVariableSet3dRotation() { }
}

public class KVariableSetExt1D : KVariableSetExt<float, float> {
    public override int ReadQuaternionDict(Dictionary<string, float> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, float> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadScalarDict(Dictionary<string, float> dict) {
        int nFound = 0;
        KVariableEnum baseType;
        KVariableExtendedEnum extType;
        foreach (KeyValuePair<string, float> entry in dict) {
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Drag:
                        drag = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'float': " + entry);
                        break;
                }
            } else if (KVariableTypeInfo.ExtendedAliases.TryGetValue(entry.Key, out extType)) {
                switch (extType) {
                    case KVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableExtendedEnum.ImpulseForceDerivative:
                        impulseForceDerivative = entry.Value;
                        ++nFound;
                        break;

                    default:
                        Debug.LogWarning("Bad entry for type 'float': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'float': " + entry);
            }
        }
        return nFound;
    }
    // New constructors
    public KVariableSetExt1D(float values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // Pass on base constructors
    public KVariableSetExt1D(Dictionary<string, float> allDicts)
    : base() {
        ReadScalarDict(allDicts);
    }
    public KVariableSetExt1D(
        float variableIn,
        float derivativeIn,
        float secondDerivativeIn,
        float thirdDerivativeIn,
        float appliedForceIn,
        float appliedForceDerivativeIn,
        float impulseForceIn,
        float impulseForceDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        appliedForceDerivativeIn,
        impulseForceIn,
        impulseForceDerivativeIn,
        dragIn
    ) { }
    public KVariableSetExt1D() { }
}
public class KVariableSetExt2D : KVariableSetExt<Vector2, Vector2> {
    public override int ReadQuaternionDict(Dictionary<string, Vector2> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector2> dict) {
        int nFound = 0;
        KVariableEnum baseType;
        KVariableExtendedEnum extType;
        foreach(KeyValuePair<string, Vector2> entry in dict) {
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else if (KVariableTypeInfo.ExtendedAliases.TryGetValue(entry.Key, out extType)) {
                switch (extType) {
                    case KVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableExtendedEnum.ImpulseForceDerivative:
                        impulseForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'V': " + entry);
            }
        }
        return nFound;
    }
    // *** New constructors
    public KVariableSetExt2D(Vector2 values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KVariableSetExt2D(
        Vector2 variableIn,
        Vector2 derivativeIn,
        Vector2 secondDerivativeIn,
        Vector2 thirdDerivativeIn,
        Vector2 appliedForceIn,
        Vector2 appliedForceDerivativeIn,
        Vector2 impulseForceIn,
        Vector2 impulseForceDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        appliedForceDerivativeIn,
        impulseForceIn,
        impulseForceDerivativeIn,
        dragIn
    ) { }
    public KVariableSetExt2D(Dictionary<string, Vector2> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KVariableSetExt2D() { }
}
public class KVariableSetExt3D : KVariableSetExt<Vector3, Vector3> {
    public override int ReadQuaternionDict(Dictionary<string, Vector3> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector3> dict) {
        int nFound = 0;
        KVariableEnum baseType;
        KVariableExtendedEnum extType;
        foreach(KeyValuePair<string, Vector3> entry in dict) {
            if (KVariableTypeInfo.Aliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else if (KVariableTypeInfo.ExtendedAliases.TryGetValue(entry.Key, out extType)) {
                switch (extType) {
                    case KVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KVariableExtendedEnum.ImpulseForceDerivative:
                        impulseForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else {
                Debug.LogWarning("Bad entry for type 'V': " + entry);
            }
        }
        return nFound;
    }
    // *** New constructors
    public KVariableSetExt3D(Vector3 values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KVariableSetExt3D(
        Vector3 variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 thirdDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 appliedForceDerivativeIn,
        Vector3 impulseForceIn,
        Vector3 impulseForceDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        appliedForceDerivativeIn,
        impulseForceIn,
        impulseForceDerivativeIn,
        dragIn
    ) { }
    public KVariableSetExt3D(Dictionary<string, Vector3> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KVariableSetExt3D() { }
}
public class KVariableSetExt3DRot : KVariableSetExt<Quaternion, Vector3> {
    // *** New constructors
    public KVariableSetExt3DRot(Quaternion rotation, Vector3 values, float drag = 0)
    : base(rotation, values, values, values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KVariableSetExt3DRot(
        Quaternion variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 thirdDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 appliedForceDerivativeIn,
        Vector3 impulseForceIn,
        Vector3 impulseForceDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        appliedForceDerivativeIn,
        impulseForceIn,
        impulseForceDerivativeIn,
        dragIn
    ) { }
    public KVariableSetExt3DRot(
        Dictionary<string, Quaternion> qDict,
        Dictionary<string, Vector3> vDict,
        Dictionary<string, float> fDict
    ) {
        if (qDict != null) {
            ReadQuaternionDict(qDict);
        }
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KVariableSetExt3DRot() { }
}
