using System.Collections.Generic;
using UnityEngine;

public enum KVariableEnum {
    None             = 0b_0000_0000_0000_0000,  //   0
    Variable         = 0b_0000_0000_0000_0001,  //   1
    Derivative       = 0b_0000_0000_0000_0010,  //   2
    SecondDerivative = 0b_0000_0000_0000_0100,  //   4
    AppliedForce     = 0b_0000_0000_0000_1000,  //   8
    ImpulseForce     = 0b_0000_0000_0001_0000,  //  16
    Drag             = 0b_0000_0000_0010_0000   //  32
}

public enum KVariableExtendedEnum {
    None                   = 0b_0000_0000_0000_0000,  //  64
    ThirdDerivative        = 0b_0000_0000_0100_0000,  // 128
    AppliedForceDerivative = 0b_0000_0000_1000_0000,  // 256
    ImpulseForceDerivative = 0b_0000_0001_0000_0000   // 512
}

public class KVariableTypeInfo {
    // None
    public static KVariableTypeSet None                   = new KVariableTypeSet(KVariableEnum.None);
    // Base types
    public static KVariableTypeSet Variable               = new KVariableTypeSet(KVariableEnum.Variable);
    public static KVariableTypeSet Derivative             = new KVariableTypeSet(KVariableEnum.Derivative);
    public static KVariableTypeSet SecondDerivative       = new KVariableTypeSet(KVariableEnum.SecondDerivative);
    public static KVariableTypeSet AppliedForce           = new KVariableTypeSet(KVariableEnum.AppliedForce);
    public static KVariableTypeSet ImpulseForce           = new KVariableTypeSet(KVariableEnum.ImpulseForce);
    public static KVariableTypeSet Drag                   = new KVariableTypeSet(KVariableEnum.Drag);
    // Extended types
    public static KVariableTypeSet ThirdDerivative        = new KVariableTypeSet(KVariableExtendedEnum.ThirdDerivative);
    public static KVariableTypeSet AppliedForceDerivative = new KVariableTypeSet(KVariableExtendedEnum.AppliedForceDerivative);
    public static KVariableTypeSet ImpulseForceDerivative = new KVariableTypeSet(KVariableExtendedEnum.ImpulseForceDerivative);
    // Mixed types
    public static KVariableTypeSet AllForceTypes = AppliedForce|ImpulseForce|AppliedForceDerivative|ImpulseForceDerivative;
    public static KVariableTypeSet AllStateSetterTypes = ~AllForceTypes;
    public static KVariableTypeSet AllBaseTypes =
        new KVariableTypeSet(
            None | Variable | Derivative | SecondDerivative | AppliedForce | ImpulseForce | Drag
        );
    public static KVariableTypeSet AllExtendedTypes =
        new KVariableTypeSet(
            None | ThirdDerivative | AppliedForceDerivative | ImpulseForceDerivative
        );
    public static KVariableTypeSet ExcludedFromControl = 
        new KVariableTypeSet(
            ThirdDerivative | AppliedForceDerivative | ImpulseForceDerivative | Drag
        );
    public static KVariableTypeSet AllTypes = new KVariableTypeSet(AllBaseTypes | AllExtendedTypes);

    public static int NBaseEnums = 7;
    public static int NExtendedEnums = 4;

    // c# switch statement hack, look away
    public const System.UInt32 NoneEnum = (System.UInt32)KVariableEnum.None;
    public const System.UInt32 VariableEnum = (System.UInt32)KVariableEnum.Variable;
    public const System.UInt32 DerivativeEnum = (System.UInt32)KVariableEnum.Derivative;
    public const System.UInt32 SecondDerivativeEnum = (System.UInt32)KVariableEnum.SecondDerivative;
    public const System.UInt32 AppliedForceEnum = (System.UInt32)KVariableEnum.AppliedForce;
    public const System.UInt32 ImpulseForceEnum = (System.UInt32)KVariableEnum.ImpulseForce;
    public const System.UInt32 DragEnum = (System.UInt32)KVariableEnum.Drag;
    public const System.UInt32 ThirdDerivativeEnum = (System.UInt32)KVariableExtendedEnum.ThirdDerivative;
    public const System.UInt32 AppliedForceDerivativeEnum = (System.UInt32)KVariableExtendedEnum.AppliedForceDerivative;
    public const System.UInt32 ImpulseForceDerivativeEnum = (System.UInt32)KVariableExtendedEnum.ImpulseForceDerivative;

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
    public static Dictionary<string, KVariableEnum> BaseAliases = new Dictionary<string, KVariableEnum> {
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
        {"AppliedForce", KVariableEnum.AppliedForce},
        {"AppliedTorque", KVariableEnum.AppliedForce},
        {"Force", KVariableEnum.AppliedForce},
        {"Torque", KVariableEnum.AppliedForce},
        {"Impulse", KVariableEnum.ImpulseForce},
        {"ImpulseForce", KVariableEnum.ImpulseForce},
        {"ImpulseTorque", KVariableEnum.ImpulseForce},
        {"Drag", KVariableEnum.Drag},
        {"AngularDrag", KVariableEnum.Drag}
    };
    public static Dictionary<string, KVariableExtendedEnum> ExtendedAliases = new Dictionary<string, KVariableExtendedEnum> {
        {"Jerk", KVariableExtendedEnum.ThirdDerivative},
        {"AngularJerk", KVariableExtendedEnum.ThirdDerivative},
        {"OmegaDotDot", KVariableExtendedEnum.ThirdDerivative},
        {"AppliedForceRate", KVariableExtendedEnum.AppliedForceDerivative},
        {"AppliedTorqueRate", KVariableExtendedEnum.AppliedForceDerivative},
        {"ForceRate", KVariableExtendedEnum.AppliedForceDerivative},
        {"TorqueRate", KVariableExtendedEnum.AppliedForceDerivative},
        {"ImpulseRate", KVariableExtendedEnum.ImpulseForceDerivative},
        {"ImpulseForceRate", KVariableExtendedEnum.ImpulseForceDerivative},
        {"ImpulseTorqueRate", KVariableExtendedEnum.ImpulseForceDerivative}
    };
    public static KVariableEnum BaseEnumFromName(string name) {
        KVariableEnum baseEnum = KVariableEnum.None;
        BaseAliases.TryGetValue(name, out baseEnum);
        return baseEnum;
    }
    public static KVariableExtendedEnum ExtendedEnumFromName(string name) {
        KVariableExtendedEnum extEnum = KVariableExtendedEnum.None;
        ExtendedAliases.TryGetValue(name, out extEnum);
        return extEnum;
    }
    public static System.UInt32 EnumValueFromName(string name) {
        KVariableEnum baseEnum;
        if (BaseAliases.TryGetValue(name, out baseEnum)) {
            return (System.UInt32)baseEnum;
        }
        KVariableExtendedEnum extEnum;
        if (ExtendedAliases.TryGetValue(name, out extEnum)) {
            return (System.UInt32)extEnum;
        }
        return System.UInt32.MaxValue;
    }
}
