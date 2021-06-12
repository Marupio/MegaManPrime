using UnityEngine;

// Rigidbody2D : T = float, V = Vector2, Q = float
// Rigidbody   : T = vector3, V = Vector3, Q = Quaternion

public interface IRigidbody<T, V, Q>
{
    public V position { get; set; }
    public Q rotation { get; set; }
    public V velocity { get; set; }
    public T angularVelocity { get; set; }
    public float mass { get; set; }
    public void ApplyForce(V force);   // => AddForce(force, 0)
    public void ImpulseForce(V force); // => AddForce(force, 1)
    public void ApplyTorque(T torque); // As above
    public void ImpulseTorque(T torque); // As above
    public float drag { get; set; }
    public float angularDrag { get; set; }
    public bool useGravity { get; set; } // 3d = use gravity, 2d = gravityScale (need to store value)
    public bool freezeRotation { get; set; }
}

public class WrappedRigidbody : IRigidbody<Vector3, Vector3, Quaternion> {
    public Rigidbody m_rigidBody;
    public WrappedRigidbody(Rigidbody rb) {m_rigidBody = rb;}

    public Vector3 position { get=>m_rigidBody.position; set=>m_rigidBody.position = value; }
    public Quaternion rotation { get=>m_rigidBody.rotation; set=>m_rigidBody.rotation = value; }
    public Vector3 velocity { get=>m_rigidBody.velocity; set=>m_rigidBody.velocity = value; }
    public Vector3 angularVelocity { get=>m_rigidBody.angularVelocity; set=>m_rigidBody.angularVelocity = value; }
    public float mass { get=>m_rigidBody.mass; set=>m_rigidBody.mass = value; }
    public void ApplyForce(Vector3 force) { m_rigidBody.AddForce(force, ForceMode.Force); }
    public void ImpulseForce(Vector3 force)  { m_rigidBody.AddForce(force, ForceMode.Impulse); }
    public void ApplyTorque(Vector3 torque)  { m_rigidBody.AddTorque(torque, ForceMode.Force); }
    public void ImpulseTorque(Vector3 torque)  { m_rigidBody.AddTorque(torque, ForceMode.Impulse); }
    public float drag { get=>m_rigidBody.drag; set=>m_rigidBody.drag = value; }
    public float angularDrag { get=>m_rigidBody.angularDrag; set=>m_rigidBody.angularDrag = value; }
    public bool useGravity { get=>m_rigidBody.useGravity; set=>m_rigidBody.useGravity = value; }
    public bool freezeRotation { get=>m_rigidBody.freezeRotation; set=>m_rigidBody.freezeRotation = value; }
}


public class WrappedRigidbody2D : IRigidbody<float, Vector2, float> {
    public float m_origGravity;
    public Rigidbody2D m_rigidBody;
    public WrappedRigidbody2D(Rigidbody2D rb) {
        m_rigidBody = rb;
        m_origGravity = m_rigidBody.gravityScale;
    }

    public Vector2 position { get=>m_rigidBody.position; set=>m_rigidBody.position = value; }
    public float rotation { get=>m_rigidBody.rotation; set=>m_rigidBody.rotation = value; }
    public Vector2 velocity { get=>m_rigidBody.velocity; set=>m_rigidBody.velocity = value; }
    public float angularVelocity { get=>m_rigidBody.angularVelocity; set=>m_rigidBody.angularVelocity = value; }
    public float mass { get=>m_rigidBody.mass; set=>m_rigidBody.mass = value; }
    public void ApplyForce(Vector2 force) { m_rigidBody.AddForce(force, ForceMode2D.Force); }
    public void ImpulseForce(Vector2 force)  { m_rigidBody.AddForce(force, ForceMode2D.Impulse); }
    public void ApplyTorque(float torque)  { m_rigidBody.AddTorque(torque, ForceMode2D.Force); }
    public void ImpulseTorque(float torque)  { m_rigidBody.AddTorque(torque, ForceMode2D.Impulse); }
    public float drag { get=>m_rigidBody.drag; set=>m_rigidBody.drag = value; }
    public float angularDrag { get=>m_rigidBody.angularDrag; set=>m_rigidBody.angularDrag = value; }
    public bool useGravity {
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
    public bool freezeRotation { get=>m_rigidBody.freezeRotation; set=>m_rigidBody.freezeRotation = value; }
}