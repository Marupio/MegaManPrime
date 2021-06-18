using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlAxis {None, U, V, W} public enum Plane {None, XY, XZ, YZ}
// public enum AlignmentType {None, X, Y, Z, XY, XZ, YZ}

// Ways to interact with RigidBody
// 0 - Do nothing - lets its own physics model handle things
// 1 - Set Position | Rotation
// 2 - Set Velocity | AngularVelocity
// 3 - Set Acceleration | AngularAcceleration
// 4 - Apply Force | Torque

// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = Vector3
//      Axis alignment Vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float
//      Axis alignment Vector2
public class WorldSpace<Q, V, T>
{
    protected IRigidbody<Q, V, T> m_rigidBody;
    protected ITime m_time;
    protected Transform m_owner;


    // Convenience
    protected float m_invDeltaT;

    // Previous timestep kinematic variables
    protected V m_position0;
    protected V m_velocity0;
    protected V m_acceleration0;
    protected Q m_rotation0;
    protected T m_angularVelocity0;
    protected T m_angularAcceleration0;

    // Extra kinematic variables that rigidBody doesn't track
    protected V m_accelerationActual;
    protected V m_accelerationDesired;
    protected V m_appliedForceActual;
    protected V m_appliedForceDesired;

    protected T m_angularAccelerationActual;
    protected T m_angularAccelerationDesired;
    protected T m_appliedTorqueActual;
    protected T m_appliedTorqueDesired;

    public bool ThreeD { get => GeneralTools.ThreeD<V>(); }
    public bool TwoD { get => GeneralTools.TwoD<V>(); }
    public int NSpatialFreedoms {
        get {
            int dof = 3;
            dof -= m_rigidBody.SpatialConstraint(Axis.X) == true ? 1 : 0;
            dof -= m_rigidBody.SpatialConstraint(Axis.Y) == true ? 1 : 0;
            dof -= m_rigidBody.SpatialConstraint(Axis.Z) == true ? 1 : 0;
            return dof;
        }
    }
    public int NRotationalFreedoms {
        get {
            int dof = 3;
            dof -= m_rigidBody.RotationalConstraint(Axis.X) == true ? 1 : 0;
            dof -= m_rigidBody.RotationalConstraint(Axis.Y) == true ? 1 : 0;
            dof -= m_rigidBody.RotationalConstraint(Axis.Z) == true ? 1 : 0;
            return dof;
        }
    }

    // Axes - controls and sources
    AxisProfileManager<Q,V,T> m_controlledAxes;
    AxisSourceManager m_sources;

    public AxisProfileManager<Q,V,T> Axes { get => m_controlledAxes; set => m_controlledAxes = value; }
    public AxisSourceManager Sources { get=> m_sources; set => m_sources = value; }


    public void Move() {
        // PseudoCode:
        //  - Update locally tracked fields (accelerations)
        //  - Create local working copies of kinematic variables
        //  - Sum all sources
        //  - For each active axis:
        //      - Assemble kvarSet, project variables if needed
        //      - Call update on axis
        //      - Add changes back to local working variables
        //  - Apply variable limits
        //  - Modify RigidBody kinematic variables
        //  - Apply forces to RigidBody
        //  - Save variables into old variables

        // Update locally tracked fields (accelerations)
        UpdateLocalFields();

        // Create local working copies of kinematic variables
        //      We need current value to feed SmoothDamp functions, but need to apply limits before changing the real things
        Traits<V> traitsV = new Traits<V>();
        float mass = m_rigidBody.Mass;
        
        // Current values to feed axes
        float drag = m_rigidBody.Drag;
        V position = m_rigidBody.Position;
        V velocity = m_rigidBody.Velocity;
        V acceleration = m_acceleration0;
        V appliedForce = m_appliedForceActual;

        float angularDrag = m_rigidBody.AngularDrag;
        Q rotation = m_rigidBody.Rotation;
        T angularVelocity = m_rigidBody.AngularVelocity;
        T appliedTorque = m_appliedTorqueActual;

        foreach(AxisProfile<float, V> axis in m_controlledAxes.ActiveAxes1D) {
            if (axis.Projecting) {

            }
        }
    }

    // *** Internal functions

    protected virtual void UpdateLocalFields() {
        float dt;
        if (m_time.inFixedTimeStep) {
            dt = m_time.fixedDeltaTime;
        } else {
            dt = m_time.deltaTime;
        }
        if (Mathf.Abs(dt) > float.Epsilon) {
            m_invDeltaT = 1f/dt;
        } else {
            m_invDeltaT = float.PositiveInfinity;
        }
    }

    // *** Constructors

}

// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = Vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float
public class WorldSpace2D : WorldSpace<float, Vector2, float> {
    protected override void UpdateLocalFields() {
        base.UpdateLocalFields();
        // Linear scheme
        m_acceleration0 = m_accelerationActual;
        m_angularAcceleration0 = m_angularAccelerationActual;
        m_accelerationActual = (m_rigidBody.Velocity - m_velocity0)*m_invDeltaT;
        m_angularAccelerationActual = (m_rigidBody.AngularVelocity - m_angularAcceleration0)*m_invDeltaT;
    }
}
public class WorldSpace3D : WorldSpace<Quaternion, Vector3, Vector3> {
    protected override void UpdateLocalFields() {
        base.UpdateLocalFields();
        // Linear scheme
        m_acceleration0 = m_accelerationActual;
        m_angularAcceleration0 = m_angularAccelerationActual;
        m_accelerationActual = (m_rigidBody.Velocity - m_velocity0)*m_invDeltaT;
        m_angularAccelerationActual = (m_rigidBody.AngularVelocity - m_angularAcceleration0)*m_invDeltaT;
    }
}
