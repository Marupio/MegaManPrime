using System.Collections.Generic;
using UnityEngine;

//    1D,2D,3D,3R
// Q = f,v2,v3, q
// V = f,v2,v3,v3
public enum KinematicVariableEnum {
    None,
    Variable,
    Derivative,
    SecondDerivative,
    AppliedForce,
    ImpulseForce,
    Drag
}

public enum KinematicVariableExtendedEnum {
    None,
    ThirdDerivative,
    AppliedForceDerivative,
    ImpulseForceDerivative
}

//public class Dictionary<string, KinematicVariableEnum>;


public class KinematicVariableSet<Q,V> {
    protected Q variable;
    protected V derivative;
    protected V secondDerivative;
    protected V appliedForce;
    protected V impulseForce;
    protected float drag;

    // *** Aliases for setting and getting by name
    public static Dictionary<string, KinematicVariableEnum> VariableAliases = new Dictionary<string, KinematicVariableEnum> {
        {"Position", KinematicVariableEnum.Variable},
        {"Distance", KinematicVariableEnum.Variable},
        {"Rotation", KinematicVariableEnum.Variable},
        {"Speed", KinematicVariableEnum.Derivative},
        {"Velocity", KinematicVariableEnum.Derivative},
        {"AngularVelocity", KinematicVariableEnum.Derivative},
        {"Omega", KinematicVariableEnum.Derivative},
        {"Acceleration", KinematicVariableEnum.SecondDerivative},
        {"AngularAcceleration", KinematicVariableEnum.SecondDerivative},
        {"OmegaDot", KinematicVariableEnum.SecondDerivative},
        {"AppliedForce", KinematicVariableEnum.AppliedForce},
        {"AppliedTorque", KinematicVariableEnum.AppliedForce},
        {"Force", KinematicVariableEnum.AppliedForce},
        {"Torque", KinematicVariableEnum.AppliedForce},
        {"ImpulseForce", KinematicVariableEnum.ImpulseForce},
        {"ImpulseTorque", KinematicVariableEnum.ImpulseForce}
    };

    // *** Access
    public Q Variable {get=>variable; set=>variable=value;}
    public V Derivative {get=>derivative; set=>derivative=value;}
    public V SecondDerivative {get=>secondDerivative; set=>secondDerivative=value;}
    public V AppliedForce {get=>appliedForce; set=>appliedForce=value;}
    public V ImpulseForce {get=>impulseForce; set=>impulseForce=value;}
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
    public V Force_alias          {get=>appliedForce; set=>appliedForce=value;}
    public V AppliedTorque_alias  {get=>appliedForce; set=>appliedForce=value;}
    public V Torque_alias         {get=>appliedForce; set=>appliedForce=value;}
    public V ImpulseTorque_alias  {get=>impulseForce; set=>impulseForce=value;}

    public float AngularDrag_alias {get=>drag; set=>drag=value;}


    // *** Edit
    public virtual int ReadScalarDict(Dictionary<string, float> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, float> entry in dict) {
            KinematicVariableEnum type;
            if (VariableAliases.TryGetValue(entry.Key, out type)) {
                if (type == KinematicVariableEnum.Drag) {
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
            KinematicVariableEnum type;
            if (VariableAliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
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
    public virtual int ReadQuaternionDict(Dictionary<string, Q> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, Q> entry in dict) {
            KinematicVariableEnum type;
            if (VariableAliases.TryGetValue(entry.Key, out type)) {
                if (type == KinematicVariableEnum.Variable) {
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
        KinematicVariableEnum type;
        if (VariableAliases.TryGetValue(name, out type) && type == KinematicVariableEnum.Variable) {
            variable = value;
            return true;
        }
        return false;
    }
    public virtual bool Set(string name, V value) {
        KinematicVariableEnum type;
        if (VariableAliases.TryGetValue(name, out type)) {
            switch (type) {
                case KinematicVariableEnum.Derivative:
                    derivative = value;
                    return true;
                case KinematicVariableEnum.SecondDerivative:
                    secondDerivative = value;
                    return true;
                case KinematicVariableEnum.AppliedForce:
                    appliedForce = value;
                    return true;
                case KinematicVariableEnum.ImpulseForce:
                    impulseForce = value;
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
    public virtual bool Set(string name, float value) {
        KinematicVariableEnum type;
        if (VariableAliases.TryGetValue(name, out type) && type == KinematicVariableEnum.Drag) {
            drag = value;
            return true;
        }
        return false;
    }

    // *** Get by name
    public virtual bool Get(string name, ref Q value) {
        KinematicVariableEnum type;
        if (VariableAliases.TryGetValue(name, out type) && type == KinematicVariableEnum.Variable) {
            variable = value;
            return true;
        }
        return false;
    }
    public virtual bool Get(string name, ref V value) {
        KinematicVariableEnum type;
        if (VariableAliases.TryGetValue(name, out type)) {
            switch (type) {
                case KinematicVariableEnum.Derivative:
                    value = derivative;
                    return true;
                case KinematicVariableEnum.SecondDerivative:
                    value = secondDerivative;
                    return true;
                case KinematicVariableEnum.AppliedForce:
                    value = appliedForce;
                    return true;
                case KinematicVariableEnum.ImpulseForce:
                    value = impulseForce;
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }
    public virtual bool Get(string name, ref float value) {
        KinematicVariableEnum type;
        if (VariableAliases.TryGetValue(name, out type) && type == KinematicVariableEnum.Drag) {
            drag = value;
            return true;
        }
        return false;
    }
   
   // *** Constructors
    public KinematicVariableSet(
        Q variableIn,
        V derivativeIn,
        V secondDerivativeIn,
        V appliedForceIn,
        V impulseForceIn,
        float dragIn = 0
    ) {
        variable = variableIn;
        derivative = derivativeIn;
        secondDerivative = secondDerivativeIn;
        appliedForce = appliedForceIn;
        impulseForce = impulseForceIn;
        drag = dragIn;
    }
    public KinematicVariableSet(Dictionary<string, Q> qDict, Dictionary<string, V> vDict, Dictionary<string, float> fDict) {
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
    protected KinematicVariableSet(Dictionary<string, Q> qDict, Dictionary<string, V> vDict, Dictionary<string, float> fDict, bool dontReadYet) { }
    public KinematicVariableSet() { }
}


public class KinematicVariableSetExtended<Q,V> : KinematicVariableSet<Q,V> {
    protected V thirdDerivative;
    protected V appliedForceDerivative;
    protected V impulseForceDerivative;

    // *** Keys for setting and getting by name
    // *** Aliases for setting and getting by name
    public static Dictionary<string, KinematicVariableExtendedEnum> ExtendedVariableAliases = new Dictionary<string, KinematicVariableExtendedEnum> {
        {"Jerk", KinematicVariableExtendedEnum.ThirdDerivative},
        {"AngularJerk", KinematicVariableExtendedEnum.ThirdDerivative},
        {"OmegaDotDot", KinematicVariableExtendedEnum.ThirdDerivative},
        {"AppliedForceRate", KinematicVariableExtendedEnum.AppliedForceDerivative},
        {"AppliedTorqueRate", KinematicVariableExtendedEnum.AppliedForceDerivative},
        {"ForceRate", KinematicVariableExtendedEnum.AppliedForceDerivative},
        {"TorqueRate", KinematicVariableExtendedEnum.AppliedForceDerivative},
        {"ImpulseForceRate", KinematicVariableExtendedEnum.ImpulseForceDerivative},
        {"ImpulseTorqueRate", KinematicVariableExtendedEnum.ImpulseForceDerivative}
    };

    // *** Access
    public V ThirdDerivative {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V ForceDerivative {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V ImpulseDerivative {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}

    // *** Shameful aliases (for readability / clarity)
    public V Jerk_alias        {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V AngularJerk_alias {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V OmegaDotDot_alias {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V AppliedForceRate_alias   {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V AppliedTorqueRate_alias  {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V ForceRate_alias          {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V TorqueRate_alias         {get=>appliedForceDerivative; set=>appliedForceDerivative=value;}
    public V ImpulseForceRate_alias  {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}
    public V ImpulseTorqueRate_alias {get=>impulseForceDerivative; set=>impulseForceDerivative=value;}

    // *** Edit
    public override int ReadVectorDict(Dictionary<string, V> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, V> entry in dict) {
            KinematicVariableExtendedEnum type;
            if (ExtendedVariableAliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KinematicVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.ImpulseForceDerivative:
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
        return nFound + base.ReadVectorDict(dict);
    }

    // *** Set by name
    public override bool Set(string name, V value) {
        KinematicVariableExtendedEnum type;
        if (ExtendedVariableAliases.TryGetValue(name, out type)) {
            switch (type) {
                case KinematicVariableExtendedEnum.ThirdDerivative:
                    thirdDerivative = value;
                    return true;
                case KinematicVariableExtendedEnum.AppliedForceDerivative:
                    appliedForceDerivative = value;
                    return true;
                case KinematicVariableExtendedEnum.ImpulseForceDerivative:
                    impulseForceDerivative = value;
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }

    // *** Get by name
    public override bool Get(string name, ref V value) {
        KinematicVariableExtendedEnum type;
        if (ExtendedVariableAliases.TryGetValue(name, out type)) {
            switch (type) {
                case KinematicVariableExtendedEnum.ThirdDerivative:
                    value = thirdDerivative;
                    return true;
                case KinematicVariableExtendedEnum.AppliedForceDerivative:
                    value = appliedForceDerivative;
                    return true;
                case KinematicVariableExtendedEnum.ImpulseForceDerivative:
                    value = impulseForceDerivative;
                    return true;
                default:
                    return false;
            }
        }
        return false;
    }

    // *** Constructors
    public KinematicVariableSetExtended(
        Q variableIn,
        V derivativeIn,
        V secondDerivativeIn,
        V thirdDerivativeIn,
        V appliedForceIn,
        V forceDerivativeIn,
        V impulseForceIn,
        V impulseDerivativeIn,
        float dragIn = 0
    ) :
    base(
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        appliedForceIn,
        impulseForceIn,
        dragIn
    ){
        thirdDerivative = thirdDerivativeIn;
        appliedForceDerivative = forceDerivativeIn;
        impulseForceDerivative = impulseDerivativeIn;
    }
    public KinematicVariableSetExtended(Dictionary<string, Q> qDict, Dictionary<string, V> vDict, Dictionary<string, float> fDict)
    :
        base(qDict, vDict, fDict, true)
    {
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
    public KinematicVariableSetExtended() { }
}

// My best take at typedefs
public class KinematicVariableSet1D : KinematicVariableSet<float, float> {
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
        KinematicVariableEnum baseType;
        foreach (KeyValuePair<string, float> entry in dict) {
            if (VariableAliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KinematicVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Drag:
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
    public KinematicVariableSet1D(float values, float drag = 0)
    : base(values, values, values, values, values, drag) { }
    // *** Pass on constructors from base
    public KinematicVariableSet1D(
        float variableIn,
        float derivativeIn,
        float secondDerivativeIn,
        float appliedForceIn,
        float impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn) { }
    public KinematicVariableSet1D(Dictionary<string, float> allDicts) {
        ReadScalarDict(allDicts);
    }
    public KinematicVariableSet1D() { }
}
public class KinematicVariableSet2D : KinematicVariableSet<Vector2, Vector2> {
    public override int ReadQuaternionDict(Dictionary<string, Vector2> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector2> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, Vector2> entry in dict) {
            KinematicVariableEnum type;
            if (VariableAliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KinematicVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
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
    public KinematicVariableSet2D(Vector2 values, float drag = 0)
    : base(values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KinematicVariableSet2D(
        Vector2 variableIn,
        Vector2 derivativeIn,
        Vector2 secondDerivativeIn,
        Vector2 appliedForceIn,
        Vector2 impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn)
    { }
    public KinematicVariableSet2D(Dictionary<string, Vector2> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KinematicVariableSet2D() { }
}
public class KinematicVariableSet3D : KinematicVariableSet<Vector3, Vector3> {
    public override int ReadQuaternionDict(Dictionary<string, Vector3> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector3> dict) {
        int nFound = 0;
        foreach(KeyValuePair<string, Vector3> entry in dict) {
            KinematicVariableEnum type;
            if (VariableAliases.TryGetValue(entry.Key, out type)) {
                switch (type) {
                    case KinematicVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
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
    public KinematicVariableSet3D(Vector3 values, float drag = 0)
    : base(values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KinematicVariableSet3D(
        Vector3 variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn)
    { }
    public KinematicVariableSet3D(Dictionary<string, Vector3> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KinematicVariableSet3D() { }
}
public class KinematicVariableSet3dRotation : KinematicVariableSet<Quaternion, Vector3> {
    // *** New constructors
    public KinematicVariableSet3dRotation(Quaternion rotation, Vector3 values, float drag = 0)
    : base(rotation, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KinematicVariableSet3dRotation(
        Quaternion variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 impulseForceIn,
        float dragIn = 0
    ) : base (variableIn, derivativeIn, secondDerivativeIn, appliedForceIn, impulseForceIn, dragIn)
    { }
    public KinematicVariableSet3dRotation(Dictionary<string, Quaternion> qDict, Dictionary<string, Vector3> vDict, Dictionary<string, float> fDict) {
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
    public KinematicVariableSet3dRotation() { }
}

public class KinematicVariableSetExtended1D : KinematicVariableSetExtended<float, float> {
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
        KinematicVariableEnum baseType;
        KinematicVariableExtendedEnum extType;
        foreach (KeyValuePair<string, float> entry in dict) {
            if (VariableAliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KinematicVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Drag:
                        drag = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'float': " + entry);
                        break;
                }
            } else if (ExtendedVariableAliases.TryGetValue(entry.Key, out extType)) {
                switch (extType) {
                    case KinematicVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.ImpulseForceDerivative:
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
    public KinematicVariableSetExtended1D(float values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // Pass on base constructors
    public KinematicVariableSetExtended1D(Dictionary<string, float> allDicts)
    : base() {
        ReadScalarDict(allDicts);
    }
    public KinematicVariableSetExtended1D(
        float variableIn,
        float derivativeIn,
        float secondDerivativeIn,
        float thirdDerivativeIn,
        float appliedForceIn,
        float forceDerivativeIn,
        float impulseForceIn,
        float impulseDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        forceDerivativeIn,
        impulseForceIn,
        impulseDerivativeIn,
        dragIn
    ) { }
    public KinematicVariableSetExtended1D() { }
}
public class KinematicVariableSetExtended2D : KinematicVariableSetExtended<Vector2, Vector2> {
    public override int ReadQuaternionDict(Dictionary<string, Vector2> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector2> dict) {
        int nFound = 0;
        KinematicVariableEnum baseType;
        KinematicVariableExtendedEnum extType;
        foreach(KeyValuePair<string, Vector2> entry in dict) {
            if (VariableAliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KinematicVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else if (ExtendedVariableAliases.TryGetValue(entry.Key, out extType)) {
                switch (extType) {
                    case KinematicVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.ImpulseForceDerivative:
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
    public KinematicVariableSetExtended2D(Vector2 values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KinematicVariableSetExtended2D(
        Vector2 variableIn,
        Vector2 derivativeIn,
        Vector2 secondDerivativeIn,
        Vector2 thirdDerivativeIn,
        Vector2 appliedForceIn,
        Vector2 forceDerivativeIn,
        Vector2 impulseForceIn,
        Vector2 impulseDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        forceDerivativeIn,
        impulseForceIn,
        impulseDerivativeIn,
        dragIn
    ) { }
    public KinematicVariableSetExtended2D(Dictionary<string, Vector2> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KinematicVariableSetExtended2D() { }
}
public class KinematicVariableSetExtended3D : KinematicVariableSetExtended<Vector3, Vector3> {
    public override int ReadQuaternionDict(Dictionary<string, Vector3> dict) {
        Debug.LogWarning("Wrong ReadDict function called");
        return 0;
    }
    public override int ReadVectorDict(Dictionary<string, Vector3> dict) {
        int nFound = 0;
        KinematicVariableEnum baseType;
        KinematicVariableExtendedEnum extType;
        foreach(KeyValuePair<string, Vector3> entry in dict) {
            if (VariableAliases.TryGetValue(entry.Key, out baseType)) {
                switch (baseType) {
                    case KinematicVariableEnum.Variable:
                        variable = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.Derivative:
                        derivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.SecondDerivative:
                        secondDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.AppliedForce:
                        appliedForce = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableEnum.ImpulseForce:
                        impulseForce = entry.Value;
                        ++nFound;
                        break;
                    default:
                        Debug.LogWarning("Bad entry for type 'V': " + entry);
                        break;
                }
            } else if (ExtendedVariableAliases.TryGetValue(entry.Key, out extType)) {
                switch (extType) {
                    case KinematicVariableExtendedEnum.ThirdDerivative:
                        thirdDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.AppliedForceDerivative:
                        appliedForceDerivative = entry.Value;
                        ++nFound;
                        break;
                    case KinematicVariableExtendedEnum.ImpulseForceDerivative:
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
    public KinematicVariableSetExtended3D(Vector3 values, float drag = 0)
    : base(values, values, values, values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KinematicVariableSetExtended3D(
        Vector3 variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 thirdDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 forceDerivativeIn,
        Vector3 impulseForceIn,
        Vector3 impulseDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        forceDerivativeIn,
        impulseForceIn,
        impulseDerivativeIn,
        dragIn
    ) { }
    public KinematicVariableSetExtended3D(Dictionary<string, Vector3> vDict, Dictionary<string, float> fDict) {
        if (vDict != null) {
            ReadVectorDict(vDict);
        }
        if (fDict != null) {
            ReadScalarDict(fDict);
        }
    }
    public KinematicVariableSetExtended3D() { }
}
public class KinematicVariableSetExtended3dRotation : KinematicVariableSetExtended<Quaternion, Vector3> {
    // *** New constructors
    public KinematicVariableSetExtended3dRotation(Quaternion rotation, Vector3 values, float drag = 0)
    : base(rotation, values, values, values, values, values, values, values, drag) { }
    // *** Pass on base constructors
    public KinematicVariableSetExtended3dRotation(
        Quaternion variableIn,
        Vector3 derivativeIn,
        Vector3 secondDerivativeIn,
        Vector3 thirdDerivativeIn,
        Vector3 appliedForceIn,
        Vector3 forceDerivativeIn,
        Vector3 impulseForceIn,
        Vector3 impulseDerivativeIn,
        float dragIn = 0
    ) : base (
        variableIn,
        derivativeIn,
        secondDerivativeIn,
        thirdDerivativeIn,
        appliedForceIn,
        forceDerivativeIn,
        impulseForceIn,
        impulseDerivativeIn,
        dragIn
    ) { }
    public KinematicVariableSetExtended3dRotation(
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
    public KinematicVariableSetExtended3dRotation() { }
}
