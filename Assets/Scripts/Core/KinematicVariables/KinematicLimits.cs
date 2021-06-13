using System.Collections.Generic;
using UnityEngine;

public class KinematicLimits {
    KinematicVariableSetExtended1D m_maxVars;
    KinematicVariableSetExtended1D m_minVars;

    // *** Access
    public KinematicVariableSetExtended1D Max { get => m_maxVars; set => m_maxVars = value; }
    public KinematicVariableSetExtended1D Min { get => m_minVars; set => m_minVars = value; }

    // *** Constructors
    public KinematicLimits() {
        m_maxVars = new KinematicVariableSetExtended1D(Mathf.Infinity);
        m_minVars = new KinematicVariableSetExtended1D(Mathf.NegativeInfinity);
    }
}

public struct KinmaticVariableLimit {
    public baseType 
}

// RUBBISH BELOW
// ************************************************************************************************

// public enum KinematicVariableEnum {
//     None,
//     Position,
//     Speed,
//     Acceleration,
//     Force,
//     Jerk
// }


// public class KinematicLimit {
//     public KinematicVariableTypes Type;
//     public float Max;
//     public float Min;
//     public KinematicLimit() {
//         Type = KinematicVariableTypes.None;
//         Max = float.PositiveInfinity;
//         Min = float.NegativeInfinity;
//     }
//     public KinematicLimit(KinematicVariableTypes typeIn, float maxIn, float minIn) {
//         Type = typeIn;
//         Max = maxIn;
//         Min = minIn;
//     }
// }


// public class KinematicLimits {
//     // *** Private member fields
//     private float m_forceMax = float.PositiveInfinity;
//     private float m_forceMin = float.NegativeInfinity;
//     private float m_positionMax = float.PositiveInfinity;
//     private float m_positionMin = float.NegativeInfinity;
//     private float m_speedMax = float.PositiveInfinity;
//     private float m_speedMin = float.NegativeInfinity;
//     private float m_accelerationMax = float.PositiveInfinity;
//     private float m_accelerationMin = float.NegativeInfinity;
//     private float m_jerkMax = float.PositiveInfinity;
//     private float m_jerkMin = float.NegativeInfinity;

//     // *** Access
//     public virtual float ForceMax { get => m_forceMax; set => m_forceMax = value; }
//     public virtual float ForceMin { get => m_forceMin; set => m_forceMin = value; }
//     public virtual float PositionMax { get => m_positionMax; set => m_positionMax = value; }
//     public virtual float PositionMin { get => m_positionMin; set => m_positionMin = value; }
//     public virtual float SpeedMax { get => m_speedMax; set => m_speedMax = value; }
//     public virtual float SpeedMin { get => m_speedMin; set => m_speedMin = value; }
//     public virtual float AccelerationMax { get => m_accelerationMax; set => m_accelerationMax = value; }
//     public virtual float AccelerationMin { get => m_accelerationMin; set => m_accelerationMin = value; }
//     public virtual float JerkMax { get => m_jerkMax; set => m_jerkMax = value; }
//     public virtual float JerkMin { get => m_jerkMin; set => m_jerkMin = value; }

//     // *** Constructors
//     public KinematicLimits() {}
//     public KinematicLimits(KinematicLimits limits) {
//         m_forceMax = limits.m_forceMax;
//         m_forceMin = limits.m_forceMin;
//         m_positionMax = limits.m_positionMax;
//         m_positionMin = limits.m_positionMin;
//         m_speedMax = limits.m_speedMax;
//         m_speedMin = limits.m_speedMin;
//         m_accelerationMax = limits.m_accelerationMax;
//         m_accelerationMin = limits.m_accelerationMin;
//         m_jerkMax = limits.m_jerkMax;
//         m_jerkMin = limits.m_jerkMin;
//     }
//     public KinematicLimits(KinematicLimit limit) {
//         SetLimit(limit);
//     }
//     public KinematicLimits(KinematicLimit[] limits) {
//         if (limits.Length > 5) {
//             Debug.LogWarning("Constructing kinematic limits from too many kinematic variables");
//         }
//         foreach (KinematicLimit limit in limits) {
//             SetLimit(limit);
//         }
//     }
//     public KinematicLimits(List<KinematicLimit> limits) {
//         if (limits.Count > 5) {
//             Debug.LogWarning("Constructing kinematic limits from too many kinematic variables");
//         }
//         foreach (KinematicLimit limit in limits) {
//             SetLimit(limit);
//         }
//     }
//     public KinematicLimits(KinematicVariableTypes varType, float max, float min) {
//         SetLimit(varType, max, min);
//     }
//     public KinematicLimits(KinematicVariableTypes varType, float max, float min, KinematicVariableTypes varType1, float max1, float min1) {
//         if (varType == varType1) {
//             Debug.LogWarning("Setting limits to same variable type more than once");
//         }
//         SetLimit(varType, max, min);
//         SetLimit(varType1, max1, min1);
//     }
//     public KinematicLimits(
//         KinematicVariableTypes varType, float max, float min,
//         KinematicVariableTypes varType1, float max1, float min1,
//         KinematicVariableTypes varType2, float max2, float min2
//     ){
//         if (varType == varType1 || varType == varType2 || varType1 == varType2) {
//             Debug.LogWarning("Setting limits to same variable type more than once");
//         }
//         SetLimit(varType, max, min);
//         SetLimit(varType1, max1, min1);
//         SetLimit(varType2, max2, min2);
//     }
//     public KinematicLimits(
//         KinematicVariableTypes varType, float max, float min,
//         KinematicVariableTypes varType1, float max1, float min1,
//         KinematicVariableTypes varType2, float max2, float min2,
//         KinematicVariableTypes varType3, float max3, float min3
//     ){
//         if (
//             varType == varType1 || varType == varType2 || varType == varType3 ||
//             varType1 == varType2 || varType1 == varType3 || varType2 == varType3
//         ) {
//             Debug.LogWarning("Setting limits to same variable type more than once");
//         }
//         SetLimit(varType, max, min);
//         SetLimit(varType1, max1, min1);
//         SetLimit(varType2, max2, min2);
//         SetLimit(varType3, max3, min3);
//     }
//     public KinematicLimits(
//         float forceMin, float forceMax,
//         float positionMin, float positionMax,
//         float speedMin, float speedMax,
//         float accelerationMin, float accelerationMax,
//         float jerkMin, float jerkMax
//     ) {
//         m_forceMax = forceMax;
//         m_forceMin = forceMin;
//         m_positionMax = positionMax;
//         m_positionMin = positionMin;
//         m_speedMax = speedMax;
//         m_speedMin = speedMin;
//         m_accelerationMax = accelerationMax;
//         m_accelerationMin = accelerationMin;
//         m_jerkMax = jerkMax;
//         m_jerkMin = jerkMin;
//     }

//     // *** Apply
//     public void ApplyLimit(KinematicVariableTypes varType, ref float value) {
//         switch (varType.Enum)
//         {
//             case KinematicVariableTypes.NoneEnum:{
//                 Debug.LogWarning("Attempting to apply None limit to a variable");
//                 break;
//             }
//             case KinematicVariableTypes.PositionEnum: {
//                 value = Mathf.Min(m_positionMax, Mathf.Max(m_positionMin, value));
//                 break;
//             }
//             case KinematicVariableTypes.SpeedEnum: {
//                 value = Mathf.Min(m_speedMax, Mathf.Max(m_speedMin, value));
//                 break;
//             }
//             case KinematicVariableTypes.AccelerationEnum: {
//                 value = Mathf.Min(m_accelerationMax, Mathf.Max(m_accelerationMin, value));
//                 break;
//             }
//             case KinematicVariableTypes.ForceEnum: {
//                 value = Mathf.Min(m_forceMax, Mathf.Max(m_forceMin, value));
//                 break;
//             }
//             case KinematicVariableTypes.JerkEnum: {
//                 value = Mathf.Min(m_jerkMax, Mathf.Max(m_jerkMin, value));
//                 break;
//             }
//             default: {
//                 Debug.LogError("Unhandled case: " + varType.Enum);
//                 break;
//             }
//         }
//     }
//     public void ApplyLimit(KinematicVariableTypes varType, ref Vector2 value) {
//         switch (varType.Enum)
//         {
//             case KinematicVariableTypes.NoneEnum:{
//                 Debug.LogWarning("Attempting to apply None limit to a variable");
//                 break;
//             }
//             case KinematicVariableTypes.PositionEnum: {
//                 value.x = Mathf.Min(m_positionMax, Mathf.Max(m_positionMin, value.x));
//                 value.y = Mathf.Min(m_positionMax, Mathf.Max(m_positionMin, value.y));
//                 break;
//             }
//             case KinematicVariableTypes.SpeedEnum: {
//                 value.x = Mathf.Min(m_speedMax, Mathf.Max(m_speedMin, value.x));
//                 value.y = Mathf.Min(m_speedMax, Mathf.Max(m_speedMin, value.y));
//                 break;
//             }
//             case KinematicVariableTypes.AccelerationEnum: {
//                 value.x = Mathf.Min(m_accelerationMax, Mathf.Max(m_accelerationMin, value.x));
//                 value.y = Mathf.Min(m_accelerationMax, Mathf.Max(m_accelerationMin, value.y));
//                 break;
//             }
//             case KinematicVariableTypes.ForceEnum: {
//                 value.x = Mathf.Min(m_forceMax, Mathf.Max(m_forceMin, value.x));
//                 value.y = Mathf.Min(m_forceMax, Mathf.Max(m_forceMin, value.y));
//                 break;
//             }
//             case KinematicVariableTypes.JerkEnum: {
//                 value.x = Mathf.Min(m_jerkMax, Mathf.Max(m_jerkMin, value.x));
//                 value.y = Mathf.Min(m_jerkMax, Mathf.Max(m_jerkMin, value.y));
//                 break;
//             }
//             default: {
//                 Debug.LogError("Unhandled case: " + varType.Enum);
//                 break;
//             }
//         }
//     }
//     // Custom use case
//     public void ApplyLimits(ref float position, ref float speed, ref float acceleration, ref float force)
//     {
//         position = Mathf.Min(m_positionMax, Mathf.Max(m_positionMin, position));
//         speed = Mathf.Min(m_speedMax, Mathf.Max(m_speedMin, speed));
//         acceleration = Mathf.Min(m_accelerationMax, Mathf.Max(m_accelerationMin, acceleration));
//         force = Mathf.Min(m_forceMax, Mathf.Max(m_forceMin, force));
//     }
//     public void ApplyLimits(ref Vector2 position, ref Vector2 velocity, ref Vector2 acceleration, ref Vector2 force)
//     {
//         position.x = Mathf.Min(m_positionMax, Mathf.Max(m_positionMin, position.x));
//         position.y = Mathf.Min(m_positionMax, Mathf.Max(m_positionMin, position.y));
//         velocity.x = Mathf.Min(m_speedMax, Mathf.Max(m_speedMin, velocity.x));
//         velocity.y = Mathf.Min(m_speedMax, Mathf.Max(m_speedMin, velocity.y));
//         acceleration.x = Mathf.Min(m_accelerationMax, Mathf.Max(m_accelerationMin, acceleration.x));
//         acceleration.y = Mathf.Min(m_accelerationMax, Mathf.Max(m_accelerationMin, acceleration.y));
//         force.x = Mathf.Min(m_forceMax, Mathf.Max(m_forceMin, force.x));
//         force.y = Mathf.Min(m_forceMax, Mathf.Max(m_forceMin, force.y));
//     }
//     // *** Edit
//     public void SetLimit(KinematicLimit limit) {
//         SetLimit(limit.Type, limit.Max, limit.Min);
//     }
//     public void SetLimit(KinematicVariableTypes varType, float max, float min) {
//         switch (varType.Enum) {
//             case KinematicVariableTypes.NoneEnum: {
//                 Debug.LogWarning("Attempting to set None to limits");
//                 break;
//             }
//             case KinematicVariableTypes.PositionEnum: {
//                 m_positionMax = max;
//                 m_positionMin = min;
//                 break;
//             }
//             case KinematicVariableTypes.SpeedEnum: {
//                 m_speedMax = max;
//                 m_speedMin = min;
//                 break;
//             }
//             case KinematicVariableTypes.AccelerationEnum: {
//                 m_accelerationMax = max;
//                 m_accelerationMin = min;
//                 break;
//             }
//             case KinematicVariableTypes.ForceEnum: {
//                 m_forceMax = max;
//                 m_forceMin = min;
//                 break;
//             }
//             case KinematicVariableTypes.JerkEnum: {
//                 m_jerkMax = max;
//                 m_jerkMin = min;
//                 break;
//             }
//             default: {
//                 Debug.LogError("Unhandled case: " + varType.Enum);
//                 break;
//             }
//         }
//     }
//     public void RemoveLimit(KinematicVariableTypes varType) {
//         switch (varType.Enum) {
//             case KinematicVariableTypes.NoneEnum: {
//                 Debug.LogWarning("Attempting to remove None from limits");
//                 break;
//             }
//             case KinematicVariableTypes.PositionEnum: {
//                 m_positionMax = float.PositiveInfinity;
//                 m_positionMin = float.NegativeInfinity;
//                 break;
//             }
//             case KinematicVariableTypes.SpeedEnum: {
//                 m_speedMax = float.PositiveInfinity;
//                 m_speedMin = float.NegativeInfinity;
//                 break;
//             }
//             case KinematicVariableTypes.AccelerationEnum: {
//                 m_accelerationMax = float.PositiveInfinity;
//                 m_accelerationMin = float.NegativeInfinity;
//                 break;
//             }
//             case KinematicVariableTypes.ForceEnum: {
//                 m_forceMax = float.PositiveInfinity;
//                 m_forceMin = float.NegativeInfinity;
//                 break;
//             }
//             case KinematicVariableTypes.JerkEnum: {
//                 m_jerkMax = float.PositiveInfinity;
//                 m_jerkMin = float.NegativeInfinity;
//                 break;
//             }
//             default: {
//                 Debug.LogError("Unhandled case");
//                 break;
//             }
//         }
//     }
//     public void RemoveAllLimits() {
//         m_positionMax = float.PositiveInfinity;
//         m_positionMin = float.NegativeInfinity;
//         m_speedMax = float.PositiveInfinity;
//         m_speedMin = float.NegativeInfinity;
//         m_accelerationMax = float.PositiveInfinity;
//         m_accelerationMin = float.NegativeInfinity;
//         m_jerkMax = float.PositiveInfinity;
//         m_jerkMin = float.NegativeInfinity;
//     }
// }
