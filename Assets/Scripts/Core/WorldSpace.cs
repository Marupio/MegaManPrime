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
    IRigidbody<Q, V, T> m_rigidBody;
    Transform m_owner;

    // Previous timestep kinematic variables
    V m_position0;
    V m_velocity0;
    V m_acceleration0;
    Q m_rotation0;
    V m_angularVelocity0;
    V m_angularAcceleration0;

    // Extra kinematic variables that rigidBody doesn't track
    V m_accelerationActual;
    V m_accelerationDesired;
    V m_appliedForceActual;
    V m_appliedForceDesired;
    T m_angularAccelerationActual;
    T m_angularAccelerationDesired;
    T m_angularForceActual;
    T m_angularForceDesired;
    // T m_angularForceRateActual;
    // T m_angularForceRateDesired;

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
        Traits<V> traitsV = new Traits<V>();
        // Global
        float mass = m_rigidBody.Mass;

        // Linear
        float drag = m_rigidBody.Drag;
        V position = m_rigidBody.Position;
        V velocity = m_rigidBody.Velocity;
        // V m_accelerationActual;
        // V m_accelerationDesired;
        // V m_appliedForceActual;
        // V m_appliedForceDesired;
        // V m_appliedForceRateActual;
        // V m_appliedForceRateDesired;
        // T m_angularAccelerationActual;
        // T m_angularAccelerationDesired;
        // T m_angularForceActual;
        // T m_angularForceDesired;
        // T m_angularForceRateActual;
        // T m_angularForceRateDesired;

        V appliedForce = traitsV.Zero;
        V impulseForce = traitsV.Zero;
        V appliedForceRate = traitsV.Zero;
        V impulseForceRate = traitsV.Zero;

        // Rotational
        float angularDrag = 0f;
        Q rotation = (new Traits<Q>()).Zero;
        V angularVelocity = traitsV.Zero;
        V angularAcceleration = traitsV.Zero;
        V angularJerk = traitsV.Zero;
        V appliedTorque = traitsV.Zero;
        V impulseTorque = traitsV.Zero;
        V appliedTorqueRate = traitsV.Zero;
        V impulseTorqueRate = traitsV.Zero;



        foreach(AxisProfile<float, V> axis in m_controlledAxes.ActiveAxes1D) {
            if (axis.Projecting) {

            }
        }
    }
}

