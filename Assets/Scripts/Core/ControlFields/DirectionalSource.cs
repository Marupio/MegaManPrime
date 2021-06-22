using System.Collections.Generic;
using UnityEngine;

// External sources that affect movement
public enum DirectionalSourceType
{
    None,                   // Billiards
    ConstantSpeed,          // Conveyor (includes constant rotation)
    ConstantAcceleration,   // Gravity (includes constant angular acceleration)
    ConstantForce           // Rocket (includes constant torque)
}

//public interface IDirectionalSourceToolset<

public class DirectionalSource
{
    private string m_name;
    private DirectionalSourceType m_sourceType;
    private float m_value;
    private Vector3 m_direction;
    private bool m_rotating;
    private bool m_fixedToWorldSpace;

    public string Name { get => m_name; set => m_name = value; }
    public DirectionalSourceType SourceType { get => m_sourceType; set => m_sourceType = value; }
    public float Value { get => m_value; set => m_value = value; }
    public Vector3 Direction { get => m_direction; set => m_direction = value; }
    public bool Rotating { get => m_rotating; set => m_rotating = value; }
    public bool FixedToWorldSpace { get => m_fixedToWorldSpace; set => m_fixedToWorldSpace = value; }

    // AddSource<V, T> based on RigidBody dimensions, will only ever see:
    //  * WorldSpace3D (Rigidbody)   : V = Vector3, T = Vector3
    //  * WorldSpace2D (Rigidbody2D) : V = Vector2, T = float
    // AddSource<Vector3, Vector3> and AddSource<Vector2, float>
    public void AddSource(Transform owner, ref KVariables<Vector3> spatialSource, ref KVariables<Vector3> rotationalSource) {
        Vector3 globalDirection;
        if (m_fixedToWorldSpace) {
            globalDirection = m_direction;
        } else {
            globalDirection = owner.TransformDirection(m_direction);
        }
        Vector3 sourceDelta = globalDirection * m_value;
        if (m_rotating) {
            switch(m_sourceType) {
                case DirectionalSourceType.None:
                    break;
                case DirectionalSourceType.ConstantSpeed:
                    rotationalSource.Variable += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantAcceleration:
                    rotationalSource.Derivative += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantForce:
                    rotationalSource.AppliedForce += sourceDelta;                    
                    break;
                default:
                    Debug.LogError("Unhandled case");
                    break;
            }
        } else {
            switch(m_sourceType) {
                case DirectionalSourceType.None:
                    break;
                case DirectionalSourceType.ConstantSpeed:
                    spatialSource.Variable += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantAcceleration:
                    spatialSource.Derivative += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantForce:
                    spatialSource.AppliedForce += sourceDelta;                    
                    break;
                default:
                    Debug.LogError("Unhandled case");
                    break;
            }
        }
    }
    public void AddSource(Transform owner, ref KVariables<Vector2> spatialSource, ref KVariables<float> rotationalSource) {
        if (m_rotating) {
            float sourceDelta = m_direction.z > 0 ? m_value : -m_value;
            switch(m_sourceType) {
                case DirectionalSourceType.None:
                    break;
                case DirectionalSourceType.ConstantSpeed:
                    rotationalSource.Variable += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantAcceleration:
                    rotationalSource.Derivative += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantForce:
                    rotationalSource.AppliedForce += sourceDelta;                    
                    break;
                default:
                    Debug.LogError("Unhandled case");
                    break;
            }
        } else {
            Vector3 globalDirection;
            if (m_fixedToWorldSpace) {
                globalDirection = m_direction;
            } else {
                globalDirection = owner.TransformDirection(m_direction);
            }
            // Ignore Z direction
            Vector2 sourceDelta = (Vector2)(globalDirection * m_value);
            switch(m_sourceType) {
                case DirectionalSourceType.None:
                    break;
                case DirectionalSourceType.ConstantSpeed:
                    spatialSource.Variable += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantAcceleration:
                    spatialSource.Derivative += sourceDelta;
                    break;
                case DirectionalSourceType.ConstantForce:
                    spatialSource.AppliedForce += sourceDelta;                    
                    break;
                default:
                    Debug.LogError("Unhandled case");
                    break;
            }
        }
    }

    public DirectionalSource(string name, DirectionalSourceType sourceType, float value, Vector3 direction, bool rotating, bool fixToWorld) {
        m_name = name;
        m_sourceType = sourceType;
        m_value = value;
        m_direction = direction;
        m_rotating = rotating;
        m_fixedToWorldSpace = fixToWorld;
    }
    public DirectionalSource(DirectionalSource asIn) {
        m_sourceType = asIn.m_sourceType;
        m_value = asIn.m_value;
        m_direction = asIn.m_direction;
        m_rotating = asIn.m_rotating;
        m_fixedToWorldSpace = asIn.m_fixedToWorldSpace;
    }
    public DirectionalSource() {
        m_sourceType = DirectionalSourceType.None;
        m_value = 0f;
        m_direction = Vector3.zero;
        m_rotating = false;
        m_fixedToWorldSpace = true;
    }
}
