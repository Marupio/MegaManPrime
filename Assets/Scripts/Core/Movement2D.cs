using UnityEngine;
using System.Collections.Generic;

public class Movement2D {
    ITime m_time;
    Rigidbody2D m_rigidBody;
    Dictionary<string, AxisProfile> m_axisProfiles;
    AxisProfile m_xAxis;
    AxisProfile m_yAxis;
    AxisProfile m_zAxis; // rotation
    /// <summary>
    /// Treat XY axes as 2D space with vector math instead of individual scalars. Ignores m_yAxis.
    /// </summary>
    bool m_linkXY;

    // Extra state variables
    Vector2 m_velocity0 = Vector2.zero;
    Vector2 m_accelerationActual = Vector2.zero;
    Vector2 m_accelerationDesired = Vector2.zero;
    float m_rotation0 = 0;
    float m_angularAccelerationActual = 0;
    float m_angularAccelerationDesired = 0;
    
    Vector2 m_appliedForce = Vector2.zero;
    float m_appliedTorque = 0;

    // *** Axes setup
    public void AddAxis(string name, AxisProfile axis) {
        m_axisProfiles.Add(name, axis);
    }
    public void RemoveAxis(string name) {
        m_axisProfiles.Remove(name);
    }
    public void ActivateXYZ(string nameX, string nameY, string nameZ) {
        m_xAxis = m_axisProfiles[nameX];
        m_yAxis = m_axisProfiles[nameY];
        m_zAxis = m_axisProfiles[nameZ];
    }
    public void ActivateX(string name) {
        m_xAxis = m_axisProfiles[name];
    }
    public void ActivateY(string name) {
        m_yAxis = m_axisProfiles[name];
    }
    public void ActivateZ(string name) {
        m_zAxis = m_axisProfiles[name];
    }
    public void ClearXYZ() {
        m_xAxis = null;
        m_yAxis = null;
        m_zAxis = null;
    }
    public void ClearX() {
        m_xAxis = null;
    }
    public void ClearY() {
        m_yAxis = null;
    }
    public void ClearZ() {
        m_zAxis = null;
    }
    /// <summary>
    /// LinkXY allows the X and Y axes to be treated as a 2D plane with vector math instead of individual scalars
    /// When enabled, ignores Y-Axis settings
    /// </summary>
    public void LinkXY() {
        m_linkXY = true;
    }
    public void UnlinkXY() {
        m_linkXY = false;
    }
    public bool AreXYLinked() {
        return m_linkXY;
    }

    // *** Query
    // Relative quantities - how fast the entity is moving relative to applied sources
    // Useful for quantities such as walking speed
    float RelativeSpeedX();
    float RelativeSpeedY();
    float RelativeRotation();
    Vector2 RelativeVelocityXY();
    // Felt quantities - the external applied forces (excluding controlled inputs)
    // Captures magnitude of effects such as wind
    float FeltForceX();
    float FeltForceY();
    float FeltTorque();
    Vector2 FeltForceXY();

    // *** Operate
    public void Move(float deltaT) {
        UpdateLocalStateVariable(deltaT);
        Vector2 curPosition = m_rigidBody.position;
        Vector2 curVelocity = m_rigidBody.velocity;
        Vector2 instantaneousForce = Vector2.zero;
        float curRotation = m_rigidBody.rotation;
        float curOmega = m_rigidBody.angularVelocity;
        float instantaneousTorque = 0;
        KVariableTypeSet xInvolvedVariables = new KinematicVariableTypes();
        KVariableTypeSet yInvolvedVariables = new KinematicVariableTypes();
        KVariableTypeSet zInvolvedVariables = new KinematicVariableTypes();

        MoveAxis(
            m_xAxis,
            ref curPosition.x,
            ref curVelocity.x,
            ref m_accelerationDesired.x,
            ref m_appliedForce.x,
            ref instantaneousForce.x,
            ref xInvolvedVariables
        );
        MoveAxis(
            m_yAxis,
            ref curPosition.y,
            ref curVelocity.y,
            ref m_accelerationDesired.y,
            ref m_appliedForce.y,
            ref instantaneousForce.y,
            ref yInvolvedVariables
        );
        MoveAxis(
            m_zAxis,
            ref curRotation,
            ref curOmega,
            ref m_angularAccelerationDesired,
            ref m_appliedTorque,
            ref instantaneousTorque,
            ref zInvolvedVariables
        );


        // Do accelerations first
        ApplyAccelerations(xInvolvedVariables, ref curPosition.x, ref curVelocity.x, m_accelerationDesired.x, deltaT);
        ApplyAccelerations(yInvolvedVariables, ref curPosition.y, ref curVelocity.y, m_accelerationDesired.y, deltaT);
        ApplyAccelerations(zInvolvedVariables, ref curRotation, ref curOmega, m_angularAccelerationDesired, deltaT);

        // Now all changes have been loaded into the local variables, apply limits
        m_xAxis.AxisMovement.ApplyLimits(ref curPosition.x, ref curVelocity.x, ref m_accelerationDesired.x, ref m_appliedForce.x);
        m_xAxis.AxisMovement.ApplyLimit(KVariableTypeSet.Force, ref instantaneousForce.x);
        m_yAxis.AxisMovement.ApplyLimits(ref curPosition.y, ref curVelocity.y, ref m_accelerationDesired.y, ref m_appliedForce.y);
        m_yAxis.AxisMovement.ApplyLimit(KVariableTypeSet.Force, ref instantaneousForce.y);
        m_zAxis.AxisMovement.ApplyLimits(ref curRotation, ref curOmega, ref m_angularAccelerationDesired, ref m_appliedTorque);
        m_zAxis.AxisMovement.ApplyLimit(KVariableTypeSet.Force, ref instantaneousTorque);

        // Transfer local variables to m_rigidBody variables
        m_rigidBody.position = curPosition;
        m_rigidBody.velocity = curVelocity;
        m_rigidBody.rotation = curRotation;
        m_rigidBody.angularVelocity = curOmega;

        // Finally, apply forces
        if (m_appliedForce != Vector2.zero)
        {
            m_rigidBody.AddForce(m_appliedForce, ForceMode2D.Force);
        }
        if (instantaneousForce != Vector2.zero)
        {
            m_rigidBody.AddForce(instantaneousForce, ForceMode2D.Impulse);
        }
        if (m_appliedTorque != 0)
        {
            m_rigidBody.AddTorque(m_appliedTorque, ForceMode2D.Force);
        }
        if (instantaneousTorque != 0)
        {
            m_rigidBody.AddTorque(instantaneousTorque, ForceMode2D.Impulse);
        }
    }

    // *** Internal helper functions
    protected void MoveAxis(
        AxisProfile axis,
        ref float position,
        ref float speed,
        ref float accelerationDesired,
        ref float appliedForce,
        ref float instantaneousForce,
        ref KVariableTypeSet involvedVariables
    ) {
        switch (axis.AxisMovement.IndependentVariable.Enum) {
            case KVariableTypeSet.NoneEnum: {
                // Uncontrolled axis, 
                break;
            }
            case KVariableTypeSet.PositionEnum: {
                UpdateKinematicVariableSet (
                    axis,
                    ref position,
                    axis.AxisMovement.ValueTarget,
                    ref speed,
                    axis.AxisMovement.SpeedMax,
                    axis.AxisMovement.SpeedMin
                );
                involvedVariables.Add(KVariableTypeSet.Position);
                break;
            }
            case KVariableTypeSet.SpeedEnum: {
                UpdateKinematicVariableSet(
                    axis,
                    ref speed,
                    axis.AxisMovement.DerivativeTarget,
                    ref accelerationDesired,
                    axis.AxisMovement.AccelerationMax,
                    axis.AxisMovement.AccelerationMin
                );
                involvedVariables.Add(KVariableTypeSet.Speed);
                break;
            }
            case KVariableTypeSet.AccelerationEnum: {
                float jerk = 0;
                UpdateKinematicVariableSet(
                    axis,
                    ref accelerationDesired,
                    axis.AxisMovement.SecondDerivativeTarget,
                    ref jerk,
                    axis.AxisMovement.JerkMax,
                    axis.AxisMovement.JerkMin
                );
                involvedVariables.Add(KVariableTypeSet.Acceleration);
                break;
            }
            case KVariableTypeSet.ForceEnum: {
                // Force is handled differently than the rest because rigidBody2D has AddForce functionality
                ImpulseMovement impulse = axis.AxisMovement.ImpulseType();
                if (impulse != null && impulse.Instantaneous) {
                    instantaneousForce = axis.AxisMovement.ForceTarget;
                } else {
                    if (axis.SmoothingEnabled) {
                        float forceTarget = axis.AxisMovement.ForceTarget;
                        // We don't have derivative limits for force application, but we do have jerk limits
                        float jerkLimit = forceTarget > appliedForce ? axis.AxisMovement.JerkMax : axis.AxisMovement.JerkMin;
                        float forceDerivativeLimit = jerkLimit;
                        if (jerkLimit != float.PositiveInfinity && jerkLimit != float.NegativeInfinity) {
                            forceDerivativeLimit *= m_rigidBody.mass;
                        }
                        float forceDerivative = 0;
                        appliedForce = Mathf.SmoothDamp(appliedForce, forceTarget, ref forceDerivative, axis.SmoothingTime, forceDerivativeLimit);
                    } else {
                        appliedForce = axis.AxisMovement.ForceTarget;
                    }
                    involvedVariables.Add(KVariableTypeSet.Force);
                }
                break;
            }
            case KVariableTypeSet.JerkEnum:
            default: {
                Debug.LogError("Unhandled case");
                break;
            }
        } // end switch (axis.AxisMovement.IndependentVariable)
        // Apply sources
        Dictionary<string, AxisSource> sources = axis.Sources;
        foreach (KeyValuePair<string, AxisSource> entry in sources) {
            switch (entry.Value.Type) {
                case AxisSourceType.None:
                    // Do nothing
                    break;
                case AxisSourceType.ConstantSpeed:
                    speed += entry.Value.Value;
                    involvedVariables.Add(KVariableTypeSet.Speed);
                    break;
                case AxisSourceType.ConstantAcceleration:
                    accelerationDesired += entry.Value.Value;
                    involvedVariables.Add(KVariableTypeSet.Acceleration);
                    break;
                case AxisSourceType.ConstantForce:
                    appliedForce += entry.Value.Value;
                    involvedVariables.Add(KVariableTypeSet.Force);
                    break;
                default:
                    Debug.LogError("Unhandled case");
                    break;
            }
        }
    }
    /// <summary>
    /// Updates a kinematic variable, applying smoothing if necessary
    /// </summary>
    /// <returns>True if the derivative was used / changed</returns>
    protected bool UpdateKinematicVariableSet(
        AxisProfile axis,
        ref float var,
        float varTarget,
        ref float derivative,
        float derivativeMax,
        float derivativeMin
    ) {
        if (axis.SmoothingEnabled) {
            float derivativeLimit = varTarget > var ? derivativeMax : derivativeMin;
            var = Mathf.SmoothDamp(var, varTarget, ref derivative, axis.SmoothingTime, derivativeLimit);
            return true;
        } else {
            var = varTarget;
            return false;
        }
    }
    /// <summary>
    /// Update the extra state variables that we are tracking based on the current state of m_rigidBody
    /// Uses a linear scheme
    /// </summary>
    protected void UpdateLocalStateVariable(float deltaT) {
        m_accelerationActual = (m_rigidBody.velocity - m_velocity0) / deltaT;
        m_angularAccelerationActual = (m_rigidBody.rotation - m_rotation0) / deltaT;
        m_accelerationDesired = m_accelerationActual;
        m_angularAccelerationDesired = m_angularAccelerationActual;
    }
    /// <summary>
    /// If any axes involve acceleration, this is where we apply it
    /// </summary>
    public void ApplyAccelerations(
        KVariableTypeSet involvedVariables,
        ref float curPosition,
        ref float curSpeed,
        float accelerationDesired,
        float deltaT
    ){
        if (involvedVariables.Contains(KVariableTypeSet.Acceleration)) {
            // Calculate new speed and position
            float newSpeed = curSpeed + accelerationDesired * deltaT;
            float newPosition = curPosition + curSpeed * deltaT + 0.5f * accelerationDesired * deltaT * deltaT;

            GeneralTools.Assert
            (
                !involvedVariables.Contains(KVariableTypeSet.Position) && !involvedVariables.Contains(KVariableTypeSet.Speed)
            );

            curPosition = newPosition;
            curSpeed = newSpeed;
        }
    }
}

// ********************************* RUBBISH BELOW *********************************

/*
    AxisMovement (required variables / settings)
        Constrained
        ControlledSpeed
            Speed (inputRange)
            MaxAcceleration
            Smoothing
        ControlledAcceleration
            Acceleration (inputRange)
            MaxSpeed
            JerkSmoothing
            TaperOffSmoothing
        ControlledForce
            Force (inputRange)
            MaxSpeed
            JerkSmoothing
            TaperOffSmoothing
        ImpulseInitialSpeed
            InitialSpeed (inputRange)
        ImpulseSpeedAndDuration
            Speed
            Duration (inputRange)
        ImpulseAccelerationAndDuration
        ImpulseForceAndDuration
*/


// public class Movement2D
// {


//     [Header("General movement")]
//     public float m_speed;
//     public float m_acceleration;
//     public float m_gravity;

//     [Header("Jumping")]
//     public bool m_canJump;
//     public float m_jumpSpeed;
//     public float m_jumpHeight;

//     [Header("Constraints")]
//     public RigidbodyConstraints2D m_constraints;

// }


// The mechanism by which the entity has control over movement on an axis
public enum EnumAxisMovement
{
    Uncontrolled,

    // Normal AxisMovement classes (use base class AxisMovement)
    ControlledPosition,
    ControlledSpeed,
    ControlledAcceleration,
    ControlledForce,

    // Impulse AxisMovement class (use base class ImpulseMovement)
    PositionImpulse,
    SpeedImpulse,
    AccelerationImpulse,
    ForceImpulse
}



// This affects how the input value into the AxisMovement is assigned
// I take a float as an input and give the 'AxisMovement input' as my output
public enum InputRangeClasses
{
    UnsignedFixedValue, // 0, maxValue
    FixedValue,         // -maxValue, 0, maxValue
    UnsignedAnalogue,   // 0..maxValue
    Analogue            // -maxValue..0..maxValue
}

// The response to the entity from an impact along the axis
public enum AxisReaction
{
    Unaffected,
    AppliedSpeed,
    AppliedAcceleration,
    AppliedForce
}



