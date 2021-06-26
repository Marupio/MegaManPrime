using UnityEngine;

// Rigidbody   : Q = Quaternion, V = Vector3, T = vector3
// Rigidbody2D : Q = float, V = Vector2, T = float

// Name       Exclusive, RB Action
// Variable       Yes     Setter
// Derivative     Yes     Setter
// SecondDer      Yes     Additive
// Force          No      Additive
// Impulse        No      Additive


public enum RigidBodyActorType {
    None,
    ForceUser,      // These actors interact by applying a force / forceDerivative
    StateSetter,    // These actors interact by setting a state variable such as position or velocity
    Both
}

public interface IRigidbody<Q, V, T>
{
    public bool TwoD { get; }
    public bool ThreeD { get; }
    public V Position { get; set; }
    public Q Rotation { get; set; }
    public T RotationComponents { get; set; }
    public V Velocity { get; set; }
    public T AngularVelocity { get; set; }
    public float Mass { get; set; }
    public void ApplyForce(V force);   // => AddForce(force, 0)
    public void ImpulseForce(V force); // => AddForce(force, 1)
    public void ApplyTorque(T torque); // As above
    public void ImpulseTorque(T torque); // As above
    public float Drag { get; set; }
    public float AngularDrag { get; set; }
    public bool UseGravity { get; set; } // 3d = use gravity, 2d = gravityScale (need to store value)
    public bool FreezeRotation { get; set; }
    public bool SpatialConstraint(Axis axis);
    public void SetSpatialConstraint(Axis axis, bool constrain);
    public bool RotationalConstraint(Axis axis);
    public void SetRotationalConstraint(Axis axis, bool constrain);
}

public class WrappedRigidbody : IRigidbody<Quaternion, Vector3, Vector3> {
    public Rigidbody m_rigidBody;
    public WrappedRigidbody(Rigidbody rb) {m_rigidBody = rb;}

    public bool TwoD { get=>false; }
    public bool ThreeD { get=>true; }
    public Vector3 Position { get=>m_rigidBody.position; set=>m_rigidBody.position = value; }
    public Quaternion Rotation { get=>m_rigidBody.rotation; set=>m_rigidBody.rotation = value; }
    public Vector3 RotationComponents { get=>Rotation.eulerAngles; set {m_rigidBody.rotation = Quaternion.Euler(value);} }
    public Vector3 Velocity { get=>m_rigidBody.velocity; set=>m_rigidBody.velocity = value; }
    public Vector3 AngularVelocity { get=>m_rigidBody.angularVelocity; set=>m_rigidBody.angularVelocity = value; }
    public float Mass { get=>m_rigidBody.mass; set=>m_rigidBody.mass = value; }
    public void ApplyForce(Vector3 force) { m_rigidBody.AddForce(force, ForceMode.Force); }
    public void ImpulseForce(Vector3 force)  { m_rigidBody.AddForce(force, ForceMode.Impulse); }
    public void ApplyTorque(Vector3 torque)  { m_rigidBody.AddTorque(torque, ForceMode.Force); }
    public void ImpulseTorque(Vector3 torque)  { m_rigidBody.AddTorque(torque, ForceMode.Impulse); }
    public float Drag { get=>m_rigidBody.drag; set=>m_rigidBody.drag = value; }
    public float AngularDrag { get=>m_rigidBody.angularDrag; set=>m_rigidBody.angularDrag = value; }
    public bool UseGravity { get=>m_rigidBody.useGravity; set=>m_rigidBody.useGravity = value; }
    public bool FreezeRotation { get=>m_rigidBody.freezeRotation; set=>m_rigidBody.freezeRotation = value; }
    public bool SpatialConstraint(Axis axis) {
        RigidbodyConstraints constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                return false;
            case Axis.X:
                return (constraints & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX;
            case Axis.Y:
                return (constraints & RigidbodyConstraints.FreezePositionY) == RigidbodyConstraints.FreezePositionY;
            case Axis.Z:
                return (constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ;
            default:
                Debug.LogError("Unhandled case");
                return false;
        }
    }
    public void SetSpatialConstraint(Axis axis, bool constrain) {
        RigidbodyConstraints constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                break;
            case Axis.X:
                m_rigidBody.constraints = constraints | RigidbodyConstraints.FreezePositionX;
                break;
            case Axis.Y:
                m_rigidBody.constraints = constraints | RigidbodyConstraints.FreezePositionY;
                break;
            case Axis.Z:
                m_rigidBody.constraints = constraints | RigidbodyConstraints.FreezePositionZ;
                break;
            default:
                Debug.LogError("Unhandled case");
                return;
        }
    }
    public bool RotationalConstraint(Axis axis) {
        RigidbodyConstraints constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                return false;
            case Axis.X:
                return (constraints & RigidbodyConstraints.FreezeRotationX) == RigidbodyConstraints.FreezeRotationX;
            case Axis.Y:
                return (constraints & RigidbodyConstraints.FreezeRotationY) == RigidbodyConstraints.FreezeRotationY;
            case Axis.Z:
                return (constraints & RigidbodyConstraints.FreezeRotationZ) == RigidbodyConstraints.FreezeRotationZ;
            default:
                Debug.LogError("Unhandled case");
                return false;
        }
    }
    public void SetRotationalConstraint(Axis axis, bool constrain) {
        RigidbodyConstraints constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                break;
            case Axis.X:
                m_rigidBody.constraints = constraints | RigidbodyConstraints.FreezeRotationX;
                break;
            case Axis.Y:
                m_rigidBody.constraints = constraints | RigidbodyConstraints.FreezeRotationY;
                break;
            case Axis.Z:
                m_rigidBody.constraints = constraints | RigidbodyConstraints.FreezeRotationZ;
                break;
            default:
                Debug.LogError("Unhandled case");
                break;
        }
    }
}


public class WrappedRigidbody2D : IRigidbody<float, Vector2, float> {
    public float m_origGravity;
    public Rigidbody2D m_rigidBody;
    public WrappedRigidbody2D(Rigidbody2D rb) {
        m_rigidBody = rb;
        m_origGravity = m_rigidBody.gravityScale;
    }

    public bool TwoD { get=>true; }
    public bool ThreeD { get=>false; }
    public Vector2 Position { get=>m_rigidBody.position; set=>m_rigidBody.position = value; }
    public float Rotation { get=>m_rigidBody.rotation; set=>m_rigidBody.rotation = value; }
    public float RotationComponents { get=>m_rigidBody.rotation; set=>m_rigidBody.rotation = value; }
    public Vector2 Velocity { get=>m_rigidBody.velocity; set=>m_rigidBody.velocity = value; }
    public float AngularVelocity { get=>m_rigidBody.angularVelocity; set=>m_rigidBody.angularVelocity = value; }
    public float Mass { get=>m_rigidBody.mass; set=>m_rigidBody.mass = value; }
    public void ApplyForce(Vector2 force) { m_rigidBody.AddForce(force, ForceMode2D.Force); }
    public void ImpulseForce(Vector2 force)  { m_rigidBody.AddForce(force, ForceMode2D.Impulse); }
    public void ApplyTorque(float torque)  { m_rigidBody.AddTorque(torque, ForceMode2D.Force); }
    public void ImpulseTorque(float torque)  { m_rigidBody.AddTorque(torque, ForceMode2D.Impulse); }
    public float Drag { get=>m_rigidBody.drag; set=>m_rigidBody.drag = value; }
    public float AngularDrag { get=>m_rigidBody.angularDrag; set=>m_rigidBody.angularDrag = value; }
    public bool UseGravity {
        get=>m_rigidBody.gravityScale != 0;
        set
        {
            if (value) {
                m_rigidBody.gravityScale = m_origGravity;
            } else {
                m_rigidBody.gravityScale = 0;
            }
        }
    }
    public bool FreezeRotation { get=>m_rigidBody.freezeRotation; set=>m_rigidBody.freezeRotation = value; }
    public bool SpatialConstraint(Axis axis) {
        RigidbodyConstraints2D constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                return false;
            case Axis.X:
                return (constraints & RigidbodyConstraints2D.FreezePositionX) == RigidbodyConstraints2D.FreezePositionX;
            case Axis.Y:
                return (constraints & RigidbodyConstraints2D.FreezePositionY) == RigidbodyConstraints2D.FreezePositionY;
            case Axis.Z:
                //Debug.LogError("Querying constraint on Z-axis in 2D");
                // In 2D, Z axis is always constrained
                return true;
            default:
                Debug.LogError("Unhandled case");
                return false;
        }
    }
    public void SetSpatialConstraint(Axis axis, bool constrain) {
        RigidbodyConstraints2D constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                break;
            case Axis.X:
                m_rigidBody.constraints = constraints | RigidbodyConstraints2D.FreezePositionX;
                break;
            case Axis.Y:
                m_rigidBody.constraints = constraints | RigidbodyConstraints2D.FreezePositionY;
                break;
            case Axis.Z:
                Debug.LogError("ATtempting to set Z axis constraint in 2D");
                break;
            default:
                Debug.LogError("Unhandled case");
                break;;
        }
    }
    public bool RotationalConstraint(Axis axis) {
        RigidbodyConstraints2D constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                return false;
            case Axis.X:
                //Debug.LogError("Attempting to query rotational constraint on X axis in 2D");
                // In 2D X & Y rotations are always constrained
                return true;
            case Axis.Y:
                // Debug.LogError("Attempting to query rotational constraint on Y axis in 2D");
                // In 2D X & Y rotations are always constrained
                return true;
            case Axis.Z:
                return (constraints & RigidbodyConstraints2D.FreezeRotation) == RigidbodyConstraints2D.FreezeRotation;
            default:
                Debug.LogError("Unhandled case");
                return false;
        }
    }
    public void SetRotationalConstraint(Axis axis, bool constrain) {
        RigidbodyConstraints2D constraints = m_rigidBody.constraints;
        switch (axis) {
            case Axis.None:
                break;
            case Axis.X:
                Debug.LogError("Attempting to set rotational constraint on X axis in 2D");
                break;
            case Axis.Y:
                Debug.LogError("Attempting to set rotational constraint on Y axis in 2D");
                break;
            case Axis.Z:
                m_rigidBody.constraints = constraints | RigidbodyConstraints2D.FreezeRotation;
                break;
            default:
                Debug.LogError("Unhandled case");
                break;
        }
    }
}