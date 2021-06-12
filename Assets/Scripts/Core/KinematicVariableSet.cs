using System.Collections.Generic;
using UnityEngine;

//    1D,2D,3D,3R
// Q = f,v2,v3, q
// V = f,v2,v3,v3
public class KinematicVariableSet<Q,V> {
    private Q variable;
    private V derivative;
    private V secondDerivative;
    private V appliedForce;
    private V impulseForce;
    private float drag;

    // Keys for dictionary reading / writing
    private static HashSet<string> variableKeys = new HashSet<string> {
        "Position", "Distance", "Rotation"
    };
    private static HashSet<string> derivativeKeys = new HashSet<string> {
        "Speed", "Velocity", "AngularVelocity", "Omega"
    };
    private static HashSet<string> secondDerivativeKeys = new HashSet<string> {
        "Acceleration", "AngularAcceleration", "OmegaDot"
    };
    private static HashSet<string> appliedForceKeys = new HashSet<string> {
        "AppliedForce", "AppliedTorque", "Force", "Torque"
    };
    private static HashSet<string> impulseForceKeys = new HashSet<string> {
        "ImpulseForce", "ImpulseTorque"
    };
    private static HashSet<string> dragKeys = new HashSet<string> {
        "Drag", "AngularDrag"
    };

    // *** Access

    public Q Variable {get=>variable; set=>variable=value;}
    public V Derivative {get=>derivative; set=>derivative=value;}
    public V SecondDerivative {get=>secondDerivative; set=>secondDerivative=value;}
    public V AppliedForce {get=>appliedForce; set=>appliedForce=value;}
    public V ImpulseForce {get=>impulseForce; set=>impulseForce=value;}
    public float Drag {get=>drag; set=>drag=value;}

    // *** Edit
    virtual public int ReadDict(Dictionary<string, float> dict) {
        foreach (string key in dragKeys) {
            float newVal;
            bool found = dict.TryGetValue(key, out newVal);
            if (found) {
                drag = newVal;
                return 1;
            }
        }
        return 0;
    }
    virtual public int ReadDict(Dictionary<string, V> dict) {
        bool foundDerivative = false;
        bool foundSecondDerivative = false;
        bool foundAppliedForce = false;
        bool foundImpulseForce = false;
        int nFound = 0;
        foreach (KeyValuePair<string, V> entry in dict) {
            if (!foundDerivative && derivativeKeys.Contains(entry.Key))
            {
                derivative = entry.Value;
                foundDerivative = true;
                ++nFound;
                continue;
            }
            if (!foundSecondDerivative && secondDerivativeKeys.Contains(entry.Key))
            {
                secondDerivative = entry.Value;
                foundSecondDerivative = true;
                ++nFound;
                continue;
            }
            if (!foundAppliedForce && appliedForceKeys.Contains(entry.Key))
            {
                appliedForce = entry.Value;
                foundAppliedForce = true;
                ++nFound;
                continue;
            }
            if (!foundImpulseForce && impulseForceKeys.Contains(entry.Key))
            {
                impulseForce = entry.Value;
                foundImpulseForce = true;
                ++nFound;
                continue;
            }
            return nFound;
        }
        return 0;
    }
    virtual public int ReadDict(Dictionary<string, Q> dict) {
        foreach (string key in variableKeys) {
            Q newVal;
            bool found = dict.TryGetValue(key, out newVal);
            if (found) {
                variable = newVal;
                return 1;
            }
        }
        return 0;
    }

    // *** Set by name
    virtual bool Set(string name, Q value) {
        if (variableKeys.Contains(name)) {
            variable = value;
            return true;
        }
        return false;
    }
    virtual bool Set(string name, V value) {
        if (derivativeKeys.Contains(name)) {
            derivative = value;
            return true;
        }
        if (secondDerivativeKeys.Contains(name)) {
            secondDerivative = value;
            return true;
        }
        if (appliedForceKeys.Contains(name)) {
            appliedForce = value;
            return true;
        }
        if (impulseForceKeys.Contains(name)) {
            impulseForce = value;
            return true;
        }
        return false;
    }
    virtual bool Set(string name, float value) {
        if (dragKeys.Contains(name)) {
            drag = value;
            return true;
        }
        return false;
    }

    // *** Get by name
    virtual bool Get(string name, ref Q value) {
        if (variableKeys.Contains(name)) {
            Q = variable;
            return true;
        }
        return false;
    }
    virtual bool Get(string name, ref V value) {
        if (derivativeKeys.Contains(name)) {
            value = derivative;
            return true;
        }
        if (secondDerivativeKeys.Contains(name)) {
            value = secondDerivative;
            return true;
        }
        if (appliedForceKeys.Contains(name)) {
            value = appliedForce;
            return true;
        }
        if (impulseForceKeys.Contains(name)) {
            value = impulseForce;
            return true;
        }
        return false;
    }
    virtual bool Get(string name, ref float value) {
        if (dragKeys.Contains(name)) {
            value = drag;
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
            ReadDict(qDict);
        }
        if (vDict != null) {
            ReadDict(vDict);
        }
        if (fDict != null) {
            ReadDict(fDict);
        }
    }
    public KinematicVariableSet() { }
}


public class KinematicVariableSetExtended<Q,V> : KinematicVariableSet<Q,V> {
    private V thirdDerivative;
    private V forceDerivative;
    private V impulseDerivative;

    private static HashSet<string> thirdDerivativeKeys = new HashSet<string> {
        "Jerk", "AngularJerk", "OmegaDotDot"
    };
    private static HashSet<string> forceDerivativeKeys = new HashSet<string> {
        "ForceRate", "TorqueRate"
    };
    private static HashSet<string> impulseDerivativeKeys = new HashSet<string> {
        "ImpulseForceRate", "ImpulseTorqueRate"
    };


    public V ThirdDerivative {get=>thirdDerivative; set=>thirdDerivative=value;}
    public V ForceDerivative {get=>forceDerivative; set=>forceDerivative=value;}
    public V ImpulseDerivative {get=>impulseDerivative; set=>impulseDerivative=value;}
}

public class KinematicVariableSet1D : KinematicVariableSet<float, float> { }
public class KinematicVariableSet2D : KinematicVariableSet<Vector2, Vector2> { }
public class KinematicVariableSet3D : KinematicVariableSet<Vector3, Vector3> { }
public class KinematicVariableSet3dRotation : KinematicVariableSet<Quaternion, Vector3> { }

public class KinematicVariableSetExtended1D : KinematicVariableSetExtended<float, float> { }
public class KinematicVariableSetExtended2D : KinematicVariableSetExtended<Vector2, Vector2> { }
public class KinematicVariableSetExtended3D : KinematicVariableSetExtended<Vector3, Vector3> { }
public class KinematicVariableSetExtended3dRotation : KinematicVariableSetExtended<Quaternion, Vector3> { }
