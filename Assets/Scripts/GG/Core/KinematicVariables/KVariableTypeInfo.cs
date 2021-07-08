using System.Collections.Generic;
using UnityEngine;

public enum KVariableEnum : int {
    None                   = 0b_0000_0000_0000_0000,  //   0
    Variable               = 0b_0000_0000_0000_0001,  //   1
    Derivative             = 0b_0000_0000_0000_0010,  //   2
    SecondDerivative       = 0b_0000_0000_0000_0100,  //   4
    ThirdDerivative        = 0b_0000_0000_0000_1000,  //   8
    AppliedForce           = 0b_0000_0000_0001_0000,  //  16
    ImpulseForce           = 0b_0000_0000_0010_0000,  //  32
    AppliedForceDerivative = 0b_0000_0000_0100_0000,  //  64
    ImpulseForceDerivative = 0b_0000_0000_1000_0000   // 128
}

public enum KVariableEnum_Controllable : int {
    None                   = 0b_0000_0000_0000_0000,  //   0
    Variable               = 0b_0000_0000_0000_0001,  //   1
    Derivative             = 0b_0000_0000_0000_0010,  //   2
    SecondDerivative       = 0b_0000_0000_0000_0100,  //   4
    AppliedForce           = 0b_0000_0000_0001_0000,  //  16
    ImpulseForce           = 0b_0000_0000_0010_0000   //  32
}

// For use as array indices
public enum KVIndex : int {
    None                   = 0,
    Variable               = 1,
    Derivative             = 2,
    SecondDerivative       = 3,
    AppliedForce           = 4,
    ImpulseForce           = 5,
    ThirdDerivative        = 6,
    AppliedForceDerivative = 7,
    ImpulseForceDerivative = 8
}

public enum KVIndex_Controllable : int {
    None                   = 0,
    Variable               = 1,
    Derivative             = 2,
    SecondDerivative       = 3,
    AppliedForce           = 4,
    ImpulseForce           = 5
}


public static class KVariableTypeInfo {
    // *** Statically constructed types
    // *** None
    public static readonly KVariableTypeSet None                    = new KVariableTypeSet(KVariableEnum.None);
    // *** SingleTypes
    public static readonly KVariableTypeSet Variable                = new KVariableTypeSet(KVariableEnum.Variable);
    public static readonly KVariableTypeSet Derivative              = new KVariableTypeSet(KVariableEnum.Derivative);
    public static readonly KVariableTypeSet SecondDerivative        = new KVariableTypeSet(KVariableEnum.SecondDerivative);
    public static readonly KVariableTypeSet ThirdDerivative         = new KVariableTypeSet(KVariableEnum.ThirdDerivative);
    public static readonly KVariableTypeSet AppliedForce            = new KVariableTypeSet(KVariableEnum.AppliedForce);
    public static readonly KVariableTypeSet ImpulseForce            = new KVariableTypeSet(KVariableEnum.ImpulseForce);
    public static readonly KVariableTypeSet AppliedForceDerivative  = new KVariableTypeSet(KVariableEnum.AppliedForceDerivative);
    public static readonly KVariableTypeSet ImpulseForceDerivative  = new KVariableTypeSet(KVariableEnum.ImpulseForceDerivative);
    // *** Mixed types
    public static readonly KVariableTypeSet AllTypes                = Variable | Derivative | SecondDerivative | ThirdDerivative | AppliedForce | ImpulseForce | AppliedForceDerivative | ImpulseForceDerivative;
    public static readonly KVariableTypeSet AllControllableTypes    = Variable | Derivative | SecondDerivative | /***************/ AppliedForce | ImpulseForce /************************|**********************/;
    public static readonly KVariableTypeSet AllForceTypes           = /********|************|******************|*****************/ AppliedForce | ImpulseForce | AppliedForceDerivative | ImpulseForceDerivative;
    public static readonly KVariableTypeSet AllRigidBodyVarSetters  = Variable | Derivative /******************|*****************|**************|**************|************************|***********************/;
    public static readonly KVariableTypeSet AllRigidBodyVarAdders   = /********|************/ SecondDerivative | /***************/ AppliedForce | ImpulseForce /************************|***********************/;
    public static readonly KVariableTypeSet AllNonControllableTypes = /********|************|******************/ ThirdDerivative | /************|**************/ AppliedForceDerivative | ImpulseForceDerivative;
    public static readonly KVariableTypeSet AllNonForceTypes        = Variable | Derivative | SecondDerivative /*****************|**************|**************|************************|***********************/;
    // *** Enum limits
    public const int MaxValueKVariableEnum = 255;
    public const int NKVariableEnums = 9;
    public const int NKVIndex = 9;
    public const int MaxValueKVariableEnum_Controllable = 63;
    public const int NKVariableEnum_Controllable = 6;
    public const int NKVIndex_Controllable = 6;

    // c# switch statement hack, look away
    public const int NoneEnum = (int)KVariableEnum.None;
    public const int VariableEnum = (int)KVariableEnum.Variable;
    public const int DerivativeEnum = (int)KVariableEnum.Derivative;
    public const int SecondDerivativeEnum = (int)KVariableEnum.SecondDerivative;
    public const int ThirdDerivativeEnum = (int)KVariableEnum.ThirdDerivative;
    public const int AppliedForceEnum = (int)KVariableEnum.AppliedForce;
    public const int ImpulseForceEnum = (int)KVariableEnum.ImpulseForce;
    public const int AppliedForceDerivativeEnum = (int)KVariableEnum.AppliedForceDerivative;
    public const int ImpulseForceDerivativeEnum = (int)KVariableEnum.ImpulseForceDerivative;

    // *** Aliases
    public static KVariableTypeSet Position_alias            { get=>Variable; }
    public static KVariableTypeSet Distance_alias            { get=>Variable; }
    public static KVariableTypeSet Rotation_alias            { get=>Variable; }
    public static KVariableTypeSet Speed_alias               { get=>Derivative; }
    public static KVariableTypeSet Velocity_alias            { get=>Derivative; }
    public static KVariableTypeSet AngularVelocity_alias     { get=>Derivative; }
    public static KVariableTypeSet Omega_alias               { get=>Derivative; }
    public static KVariableTypeSet Acceleration_alias        { get=>SecondDerivative; }
    public static KVariableTypeSet AngularAcceleration_alias { get=>SecondDerivative; }
    public static KVariableTypeSet OmegaDot_alias            { get=>SecondDerivative; }
    public static KVariableTypeSet Jerk_alias                { get=>ThirdDerivative; }
    public static KVariableTypeSet AngularJerk_alias         { get=>ThirdDerivative; }
    public static KVariableTypeSet OmegaDotDot_alias         { get=>ThirdDerivative; }
    public static KVariableTypeSet Force_alias               { get=>AppliedForce; }
    public static KVariableTypeSet Torque_alias              { get=>AppliedForce; }
    public static KVariableTypeSet AppliedTorque_alias       { get=>AppliedForce; }
    public static KVariableTypeSet Impulse_alias             { get=>ImpulseForce; }
    public static KVariableTypeSet ImpulseTorque_alias       { get=>ImpulseForce; }
    public static KVariableTypeSet AppliedForceRate_alias    { get=>AppliedForceDerivative; }
    public static KVariableTypeSet TorqueRate_alias          { get=>AppliedForceDerivative; }
    public static KVariableTypeSet AppliedTorqueRate_alias   { get=>AppliedForceDerivative; }
    public static KVariableTypeSet ImpulseRate_alias         { get=>ImpulseForceDerivative; }
    public static KVariableTypeSet ImpulseForceRate_alias    { get=>ImpulseForceDerivative; }
    public static KVariableTypeSet ImpulseTorqueRate_alias   { get=>ImpulseForceDerivative; }
    // *** Mixed type aliases
    public static KVariableTypeSet AllBaseTypes_alias        { get=>AllControllableTypes; }
    public static KVariableTypeSet AllExtendedTypes_alias    { get=>AllNonControllableTypes; }

    // *** String aliases
    public static readonly Dictionary<string, KVariableEnum> Aliases = new Dictionary<string, KVariableEnum> {
        {"None", KVariableEnum.None},
        {"Variable", KVariableEnum.Variable},
        {"Position", KVariableEnum.Variable},
        {"Distance", KVariableEnum.Variable},
        {"Rotation", KVariableEnum.Variable},
        {"Derivative", KVariableEnum.Derivative},
        {"Speed", KVariableEnum.Derivative},
        {"Velocity", KVariableEnum.Derivative},
        {"AngularVelocity", KVariableEnum.Derivative},
        {"Omega", KVariableEnum.Derivative},
        {"SecondDerivative", KVariableEnum.SecondDerivative},
        {"Acceleration", KVariableEnum.SecondDerivative},
        {"AngularAcceleration", KVariableEnum.SecondDerivative},
        {"OmegaDot", KVariableEnum.SecondDerivative},
        {"ThirdDerivative", KVariableEnum.ThirdDerivative},
        {"Jerk", KVariableEnum.ThirdDerivative},
        {"AngularJerk", KVariableEnum.ThirdDerivative},
        {"OmegaDotDot", KVariableEnum.ThirdDerivative},
        {"AppliedForce", KVariableEnum.AppliedForce},
        {"AppliedTorque", KVariableEnum.AppliedForce},
        {"Force", KVariableEnum.AppliedForce},
        {"Torque", KVariableEnum.AppliedForce},
        {"Impulse", KVariableEnum.ImpulseForce},
        {"ImpulseForce", KVariableEnum.ImpulseForce},
        {"ImpulseTorque", KVariableEnum.ImpulseForce},
        {"AppliedForceDerivative", KVariableEnum.AppliedForceDerivative},
        {"AppliedTorqueDerivative", KVariableEnum.AppliedForceDerivative},
        {"AppliedForceRate", KVariableEnum.AppliedForceDerivative},
        {"AppliedTorqueRate", KVariableEnum.AppliedForceDerivative},
        {"ForceRate", KVariableEnum.AppliedForceDerivative},
        {"TorqueRate", KVariableEnum.AppliedForceDerivative},
        {"ImpulseRate", KVariableEnum.ImpulseForceDerivative},
        {"ImpulseForceDerivative", KVariableEnum.ImpulseForceDerivative},
        {"ImpulseTorqueDerivative", KVariableEnum.ImpulseForceDerivative},
        {"ImpulseForceRate", KVariableEnum.ImpulseForceDerivative},
        {"ImpulseTorqueRate", KVariableEnum.ImpulseForceDerivative}
    };
    public static KVariableEnum StringToKVariableEnum(string name) {
        KVariableEnum baseEnum = KVariableEnum.None;
        Aliases.TryGetValue(name, out baseEnum);
        return baseEnum;
    }
    public static int StringToKVariableEnumIntValue(string name) {
        KVariableEnum baseEnum;
        if (Aliases.TryGetValue(name, out baseEnum)) {
            return (int)baseEnum;
        }
        return -1;
    }
    /// <summary>
    /// When taking an int as an index for KVariables, rather than as a bitset mask, like KVariableTypeSet, use this conversion
    /// </summary>
    public static KVariableEnum IndexToKVariableEnum(int index) {
        int kvInt = 1;
        for (int i = 0; i < index; ++i, kvInt*=2) {}
        return (KVariableEnum)kvInt;
    }
    /// <summary>
    /// When taking an int as an index for KVariables, rather than as a bitset mask, like KVariableTypeSet, use this conversion
    /// </summary>
    public static int KVariableEnumToIndex(KVariableEnum enumIn) {
        switch(enumIn) {
            case KVariableEnum.None:
                return 0;
            case KVariableEnum.Variable:
                return 1;
            case KVariableEnum.Derivative:
                return 2;
            case KVariableEnum.SecondDerivative:
                return 3;
            case KVariableEnum.ThirdDerivative:
                return 4;
            case KVariableEnum.AppliedForce:
                return 5;
            case KVariableEnum.ImpulseForce:
                return 6;
            case KVariableEnum.AppliedForceDerivative:
                return 7;
            case KVariableEnum.ImpulseForceDerivative:
                return 8;
            default:
                return -1;
        }
    }
}
