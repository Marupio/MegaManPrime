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
    protected T m_rotationComponents0;
    protected T m_angularVelocity0;
    protected T m_angularAcceleration0;

    // Extra kinematic variables that rigidBody doesn't track
    protected V m_accelerationActual;
    protected V m_accelerationDesired;
    protected V m_appliedForceActual;
    protected V m_appliedForceDesired;

    protected T m_rotationComponentsActual;
    protected T m_rotationComponentsDesired;
    protected T m_angularAccelerationActual;
    protected T m_angularAccelerationDesired;
    protected T m_appliedTorqueActual;
    protected T m_appliedTorqueDesired;

    // Local workspace - temporary variables used during Move()
    // TODO - I want to refactor how it works to remove most of these - reduce memory footprint
    float m_localMass;            // Update with m_rigidBody.Mass;
    float m_localDrag;            // Update with m_rigidBody.Drag;
    float m_localAngularDrag;     // Update with m_rigidBody.AngularDrag;

    // V m_localPosition;            // Update with m_rigidBody.Position;
    // V m_localVelocity;            // Update with m_rigidBody.Velocity;
    // V m_localAcceleration;        // Update with m_acceleration0;
    // V m_localAppliedForce;        // Update with m_appliedForceActual;
    // Q m_localRotation;            // Update with m_rigidBody.Rotation;
    // T m_localRotationComponents;  // Update with m_rotationComponentsActual;
    // T m_localAngularVelocity;     // Update with m_rigidBody.AngularVelocity;
    // T m_localAngularAcceleration; // Update with m_angularAcceleration0;
    // T m_localAppliedTorque;       // Update with m_appliedTorqueActual;

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
    ControlFieldProfileManager<Q,V,T> m_controlFields;
    DirectionalSourceManager m_sources;

    public ControlFieldProfileManager<Q,V,T> ControlFields { get => m_controlFields; set => m_controlFields = value; }
    public DirectionalSourceManager Sources { get=> m_sources; set => m_sources = value; }


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

        // Update locally tracked accelerations and working variables
        UpdateLocalFields();

        // These hold the actual current state at the start of this iteration
        KVariables<V> spatialVarsInit = new KVariables<V>(
            m_rigidBody.Position,
            m_rigidBody.Velocity,
            m_accelerationActual,
            m_appliedForceActual,
            default(V)
        );
        KVariables<T> rotationalVarsInit = new KVariables<T>(
            m_rigidBody.RotationComponents,
            m_rigidBody.AngularVelocity,
            m_angularAccelerationActual,
            m_appliedTorqueActual,
            default(T)
        );

        // The V types hold any updated components of each kvariable type, the Vi types hold 0|1 indicating which component has been updated
        KVariables<V> spatialVarsUpdate = new KVariables<V>(new Traits<V>().Zero);
        KVariables<Vector3Int> spatialVarsUsedAxis = new KVariables<Vector3Int>(Vector3Int.zero);
        KVariables<T> rotationalVarsUpdate = new KVariables<T>(new Traits<T>().Zero);
        KVariables<Vector3Int> rotationalVarsUsedAxis = new KVariables<Vector3Int>(Vector3Int.zero);

        // Create local working copies of kinematic variables
        //  These are now member fields: 'Local workspace'

        // Sum all sources
        // TODO

        //  - For each active axis:
        //      - Assemble kvarSet, project variables if needed
        //      - Call update on axis
        //      - Add changes back to local working variables
        foreach(ControlFieldProfile<float, V> controlField in m_controlFields.ActiveAxes1D) {
            KVariables<float> varSet;
            InitialiseVarSet(out varSet, spatialVarsInit, rotationalVarsInit, controlField);
            controlField.Update(varSet);
        }
        foreach(ControlFieldProfile<Vector2, V> controlField in m_controlFields.ActiveAxes2D) {
            KVariables<Vector2> varSet;
            InitialiseVarSet(out varSet, spatialVarsInit, rotationalVarsInit, controlField);
            controlField.Update(varSet);
        }
        foreach(ControlFieldProfile<Vector3, V> controlField in m_controlFields.ActiveAxes3D) {
            KVariables<Vector3> varSet;
            InitialiseVarSet(out varSet, spatialVarsInit, rotationalVarsInit, controlField);
            controlField.Update(varSet);
        }
    }

    // *** Internal functions

    // protected virtual bool InitialiseVarSet<V1>(out KVariableSet<V1> varSet, AxisProfile<V1, V> axis) {
    //     Debug.LogError("Not implemented");
    //     varSet = new KVariableSet<V1>();
    //     return false;
    // }
// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = Vector3
//      Axis alignment Vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float
//      Axis alignment Vector2
    protected virtual bool InitialiseVarSet(
        out KVariables<float> varSet,
        KVariables<V> spatialVarsInit,
        KVariables<T> rotationalVarsInit,
        ControlFieldProfile<float, V> axis
    ) {
        if (axis.Type == AxisType.Rotational) {
            KVariables<T> srcVars = new KVariables<T>(
                rotationalVarsInit.Variable,            // m_localRotationComponents,
                rotationalVarsInit.Derivative,          // m_localAngularVelocity,
                rotationalVarsInit.SecondDerivative,    // m_localAngularAcceleration,
                rotationalVarsInit.AppliedForce,        // m_localAppliedTorque,
                new Traits<T>().Zero
            );
            if (axis.Projecting) {
                return ProjectToSubspace(out varSet, srcVars, axis.Direction);
            } else {
                return SubstituteToSubspace(out varSet, srcVars, axis.Alignment);
            }
        } else {
            KVariables<V> srcVars = new KVariables<V>(
                spatialVarsInit.Variable,           // m_localPosition,
                spatialVarsInit.Derivative,         // m_localVelocity,
                spatialVarsInit.SecondDerivative,   // m_localAcceleration,
                spatialVarsInit.AppliedForce,       // m_localAppliedForce,
                new Traits<V>().Zero
            );
            if (axis.Projecting) {
                return ProjectToSubspace(out varSet, srcVars, axis.Direction);
            } else {
                return SubstituteToSubspace(out varSet, srcVars, axis.Alignment);
            }
        }
    }

    // TODO Fill in for types
    protected bool ProjectToSubspace<V1, TorV>(out KVariables<V1> varSet, KVariables<TorV> srcVars, V direction) {
        varSet = default(KVariables<V1>);
        return false;
    }
    // TODO Fill in for types
    protected bool SubstituteToSubspace<V1, TorV>(out KVariables<V1> varSet, KVariables<TorV> srcVars, AxisPlaneSpace alignment) {
        varSet = default(KVariables<V1>);
        return false;
    }

    protected virtual void UpdateLocalFields() {
        // Update invDeltaT
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

        // Update accelerations using linear scheme --> this work is done in derived/concrete function

        // Update working variables
        m_localMass = m_rigidBody.Mass;
        m_localDrag = m_rigidBody.Drag;
        m_localAngularDrag = m_rigidBody.AngularDrag;
        // m_localPosition = m_rigidBody.Position;
        // m_localVelocity = m_rigidBody.Velocity;
        // m_localAcceleration = m_acceleration0;
        // m_localAppliedForce = m_appliedForceActual;
        // m_localRotation = m_rigidBody.Rotation;
        // m_localAngularVelocity = m_rigidBody.AngularVelocity;
        // m_localAngularAcceleration = m_angularAcceleration0;
        // m_localAppliedTorque = m_appliedTorqueActual;
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
        m_rotationComponents0 = m_rotationComponentsActual;
        m_acceleration0 = m_accelerationActual;
        m_angularAcceleration0 = m_angularAccelerationActual;
        m_accelerationActual = (m_rigidBody.Velocity - m_velocity0)*m_invDeltaT;
        m_angularAccelerationActual = (m_rigidBody.AngularVelocity - m_angularAcceleration0)*m_invDeltaT;
        
    }
}
