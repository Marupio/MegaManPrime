using UnityEngine;

// External sources that affect movement
public enum AxisSource
{
    None,                   // Billiards
    ConstantSpeed,          // Conveyor
    ConstantAcceleration,   // Gravity
    ConstantForce           // Rocket
}

// The mechanism by which the entity has control over movement on an axis
public enum EnumAxisMovement
{
    Uncontrolled,                // Fixed to a position
    ControlledPosition,         // Input affects position, e.g. paddle-controlled pong
    ControlledSpeed,            // Input affects speed, e.g. walking left / right
    ControlledAcceleration,     // Input affects acceleration, e.g. tilting a marble game
    ControlledForce,            // Input affects force, e.g. asteroids

    // Impulse controls, i.e. jumping models
    ImpulsePositionChange,      // A sudden change in target position
    ImpulseInitialSpeed,        // Only a sudden jolt of initial speed is allowed
    ImpulseSpeedDuration,       // A fixed speed for a maximum duration is allowed
    ImpulseAccelerationDuration, // A fixed acceleration for a maximum duration is allowed
    ImpulseForceDuration        //  A fixed force for a maximum duration is allowed
}



// This affects how the input value into the AxisMovement is assigned
// I take a float as an input and give the 'AxisMovement input' as my output
public enum InputRangeClasses
{
    UnsignedFixedValue, // 0, maxValue
    FixedValue,         // -maxValue, 0, maxValue
    UnsignedAnalogue,
    Analogue            // -maxValue...0...maxValue
}

// The response to the entity from an impact along the axis
public enum AxisReaction
{
    Unaffected,
    AppliedSpeed,
    AppliedAcceleration,
    AppliedForce
}

public class AxisProfile
{
    string m_name;

    AxisSource m_sourceType;
    float m_sourceValue;

    // AxisMovement m_movementType;
    // InputRange m_inputRange;

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

}

public class Movement2D
{


    [Header("General movement")]
    public float m_speed;
    public float m_acceleration;
    public float m_gravity;

    [Header("Jumping")]
    public bool m_canJump;
    public float m_jumpSpeed;
    public float m_jumpHeight;

    [Header("Constraints")]
    public RigidbodyConstraints2D m_constraints;
    
}