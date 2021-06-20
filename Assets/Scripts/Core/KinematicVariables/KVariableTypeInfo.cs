using System.Collections.Generic;
using UnityEngine;

public enum KVariableEnum {
    None                   = 0b_0000_0000_0000_0000,  //   0
    Variable               = 0b_0000_0000_0000_0001,  //   1
    Derivative             = 0b_0000_0000_0000_0010,  //   2
    SecondDerivative       = 0b_0000_0000_0000_0100,  //   4
    ThirdDerivative        = 0b_0000_0000_0000_1000,  //   8
    AppliedForce           = 0b_0000_0000_0001_0000,  //  16
    ImpulseForce           = 0b_0000_0000_0010_0000,  //  32
    AppliedForceDerivative = 0b_0000_0000_0100_0000,  //  64
    ImpulseForceDerivative = 0b_0000_0000_1000_0000,  // 128
    Drag                   = 0b_0000_0001_0000_0000   // 256
}

public enum KVariableControllableEnum{
    None                   = 0b_0000_0000_0000_0000,  //   0
    Variable               = 0b_0000_0000_0000_0001,  //   1
    Derivative             = 0b_0000_0000_0000_0010,  //   2
    AppliedForce           = 0b_0000_0000_0001_0000,  //  16
    ImpulseForce           = 0b_0000_0000_0010_0000   //  32
}

public enum KVariableRestriction {
    None                 = 0b_0000_0000,
    Singular             = 0b_0000_0001,
    Controllable         = 0b_0000_0010,
    SingularControllable = 0b_0000_0011
}

public class KVariableTypeInfo {
    // None
    public static KVariableTypeSet None                   = new KVariableTypeSet(KVariableEnum.None);
    // Base types
    public static KVariableTypeSet Variable               = new KVariableTypeSet(KVariableEnum.Variable);
    public static KVariableTypeSet Derivative             = new KVariableTypeSet(KVariableEnum.Derivative);
    public static KVariableTypeSet SecondDerivative       = new KVariableTypeSet(KVariableEnum.SecondDerivative);
    public static KVariableTypeSet ThirdDerivative        = new KVariableTypeSet(KVariableEnum.ThirdDerivative);
    public static KVariableTypeSet AppliedForce           = new KVariableTypeSet(KVariableEnum.AppliedForce);
    public static KVariableTypeSet ImpulseForce           = new KVariableTypeSet(KVariableEnum.ImpulseForce);
    public static KVariableTypeSet AppliedForceDerivative = new KVariableTypeSet(KVariableEnum.AppliedForceDerivative);
    public static KVariableTypeSet ImpulseForceDerivative = new KVariableTypeSet(KVariableEnum.ImpulseForceDerivative);
    public static KVariableTypeSet Drag                   = new KVariableTypeSet(KVariableEnum.Drag);
    // Mixed types
    public static KVariableTypeSet AllForceTypes = AppliedForce|ImpulseForce|AppliedForceDerivative|ImpulseForceDerivative;
    public static KVariableTypeSet AllStateSetterTypes = ~AllForceTypes;
    public static KVariableTypeSet AllBaseTypes =
        new KVariableTypeSet(
            None | Variable | Derivative | SecondDerivative | AppliedForce | ImpulseForce | Drag
        );
    public static KVariableTypeSet AllExtendedTypes =
        new KVariableTypeSet(
            
        );
    public static KVariableTypeSet ExcludedFromControl = 
        new KVariableTypeSet(
            SecondDerivative | ThirdDerivative | AppliedForceDerivative | ImpulseForceDerivative | Drag
        );
    public static KVariableTypeSet AllControllableTypes =
        new KVariableTypeSet(
            Variable | Derivative | AppliedForce | ImpulseForce
        );
    public static KVariableTypeSet AllTypes = new KVariableTypeSet(
        Variable |
        Derivative |
        SecondDerivative |
        ThirdDerivative |
        AppliedForce |
        ImpulseForce |
        AppliedForceDerivative |
        ImpulseForceDerivative |
        Drag
    );

    public static int MaxValue = 511;
    public static int NBaseEnums = 7;
    public static int NExtendedEnums = 4;

    // c# switch statement hack, look away
    public const System.Int32 NoneEnum = (System.Int32)KVariableEnum.None;
    public const System.Int32 VariableEnum = (System.Int32)KVariableEnum.Variable;
    public const System.Int32 DerivativeEnum = (System.Int32)KVariableEnum.Derivative;
    public const System.Int32 SecondDerivativeEnum = (System.Int32)KVariableEnum.SecondDerivative;
    public const System.Int32 ThirdDerivativeEnum = (System.Int32)KVariableEnum.ThirdDerivative;
    public const System.Int32 AppliedForceEnum = (System.Int32)KVariableEnum.AppliedForce;
    public const System.Int32 ImpulseForceEnum = (System.Int32)KVariableEnum.ImpulseForce;
    public const System.Int32 AppliedForceDerivativeEnum = (System.Int32)KVariableEnum.AppliedForceDerivative;
    public const System.Int32 ImpulseForceDerivativeEnum = (System.Int32)KVariableEnum.ImpulseForceDerivative;
    public const System.Int32 DragEnum = (System.Int32)KVariableEnum.Drag;

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
    public static KVariableTypeSet AngularDrag_alias         { get=>Drag; }

    // *** String aliases
    public static Dictionary<string, KVariableEnum> Aliases = new Dictionary<string, KVariableEnum> {
        {"Position", KVariableEnum.Variable},
        {"Distance", KVariableEnum.Variable},
        {"Rotation", KVariableEnum.Variable},
        {"Speed", KVariableEnum.Derivative},
        {"Velocity", KVariableEnum.Derivative},
        {"AngularVelocity", KVariableEnum.Derivative},
        {"Omega", KVariableEnum.Derivative},
        {"Acceleration", KVariableEnum.SecondDerivative},
        {"AngularAcceleration", KVariableEnum.SecondDerivative},
        {"OmegaDot", KVariableEnum.SecondDerivative},
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
        {"AppliedForceRate", KVariableEnum.AppliedForceDerivative},
        {"AppliedTorqueRate", KVariableEnum.AppliedForceDerivative},
        {"ForceRate", KVariableEnum.AppliedForceDerivative},
        {"TorqueRate", KVariableEnum.AppliedForceDerivative},
        {"ImpulseRate", KVariableEnum.ImpulseForceDerivative},
        {"ImpulseForceRate", KVariableEnum.ImpulseForceDerivative},
        {"ImpulseTorqueRate", KVariableEnum.ImpulseForceDerivative},
        {"Drag", KVariableEnum.Drag},
        {"AngularDrag", KVariableEnum.Drag}
    };
    public static KVariableEnum EnumFromName(string name) {
        KVariableEnum baseEnum = KVariableEnum.None;
        Aliases.TryGetValue(name, out baseEnum);
        return baseEnum;
    }
    public static System.Int32 EnumValueFromName(string name) {
        KVariableEnum baseEnum;
        if (Aliases.TryGetValue(name, out baseEnum)) {
            return (System.Int32)baseEnum;
        }
        return -1;
    }
}
