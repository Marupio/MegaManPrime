using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlAxis {None, U, V, W} public enum Plane {None, XY, XZ, YZ}

public enum AxisNames2D {X, Y, Yaw}
public enum AxisNames3D {X, Y, Z, Roll, Pitch, Yaw}

// Ways to interact with RigidBody
// 0 - Do nothing - lets its own physics model handle things
// 1 - Set Position | Rotation
// 2 - Set Velocity | AngularVelocity
// 3 - Set Acceleration | AngularAcceleration
// 4 - Apply Force | Torque

// MovementController3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = Vector3
//      Axis alignment Vector3
// MovementController2D (Rigidbody2D) : Q = float, V = Vector2, T = float
//      Axis alignment Vector2
public interface IMovementControllerToolset<Q, V, T> {
    public V ZeroV { get; }
    public T ZeroT { get; }
    public V InfiniteV { get; }
    public T InfiniteT { get; }
    public void UpdateAccelerations(
        IRigidbody<Q, V, T> rigidbody,
        float invDeltaT,
        V velocity0,
        ref V acceleration0,
        ref V accelerationActual,
        T rotationComponents0,
        ref T angularAcceleration0,
        ref T angularAccelerationActual
    );
    public void SumDirectionalSource(
        DirectionalSourceManager sources,
        Transform owner,
        out KVariables<V> spatialSource,
        out KVariables<T> rotationalSource
    );
}

public class MovementControllerToolset3D : IMovementControllerToolset<Quaternion, Vector3, Vector3> {
    public Vector3 ZeroV { get=>Vector3.zero; }
    public Vector3 ZeroT { get=>Vector3.zero; }
    public Vector3 InfiniteV { get=>Vector3.positiveInfinity; }
    public Vector3 InfiniteT { get=>Vector3.positiveInfinity; }
    public void SumDirectionalSource(
        DirectionalSourceManager sources,
        Transform owner,
        out KVariables<Vector3> spatialSource,
        out KVariables<Vector3> rotationalSource
    ) {
        spatialSource = new KVariables<Vector3>(Vector3.zero);
        rotationalSource = new KVariables<Vector3>(Vector3.zero);
        foreach(KeyValuePair<string, DirectionalSource> sourceEntry in sources.Sources) {
            sourceEntry.Value.AddSource(owner, ref spatialSource, ref rotationalSource);
        }
    }
    public void UpdateAccelerations(
        IRigidbody<Quaternion, Vector3, Vector3> rigidbody,
        float invDeltaT,
        Vector3 velocity0,
        ref Vector3 acceleration0,
        ref Vector3 accelerationActual,
        Vector3 rotationComponents0,
        ref Vector3 angularAcceleration0,
        ref Vector3 angularAccelerationActual
    ) {
        // Linear scheme
        acceleration0 = accelerationActual;
        accelerationActual = (rigidbody.Velocity - velocity0)*invDeltaT;

        angularAcceleration0 = angularAccelerationActual;
        angularAccelerationActual = (rigidbody.AngularVelocity - angularAcceleration0)*invDeltaT;
    }
}

public class MovementControllerToolset2D : IMovementControllerToolset<float, Vector2, float> {
    public Vector2 ZeroV { get=>Vector2.zero; }
    public float ZeroT { get=>0f; }
    public Vector2 InfiniteV { get=>Vector2.positiveInfinity; }
    public float InfiniteT { get=>float.PositiveInfinity; }
    public void SumDirectionalSource(
        DirectionalSourceManager sources,
        Transform owner,
        out KVariables<Vector2> spatialSource,
        out KVariables<float> rotationalSource
    ) {
        spatialSource = new KVariables<Vector2>(Vector2.zero);
        rotationalSource = new KVariables<float>(0f);
        foreach(KeyValuePair<string, DirectionalSource> sourceEntry in sources.Sources) {
            sourceEntry.Value.AddSource(owner, ref spatialSource, ref rotationalSource);
        }
    }
    public void UpdateAccelerations(
        IRigidbody<float, Vector2, float> rigidbody,
        float invDeltaT,
        Vector2 velocity0,
        ref Vector2 acceleration0,
        ref Vector2 accelerationActual,
        float rotationComponents0,
        ref float angularAcceleration0,
        ref float angularAccelerationActual
    ) {
        // Linear scheme
        acceleration0 = accelerationActual;
        angularAcceleration0 = angularAccelerationActual;

        accelerationActual = (rigidbody.Velocity - velocity0)*invDeltaT;
        angularAccelerationActual = (rigidbody.AngularVelocity - angularAcceleration0)*invDeltaT;
    }
}

public class MovementController<Q, V, T>
{
    protected IMovementControllerToolset<Q, V, T> m_toolset;
    // Projection toolsets
    protected IProjections<float, V> m_subspaceFloatV;     // Project between 1D axis and spatial space
    protected IProjections<Vector2, V> m_subspaceVector2V; // Project between 2D axis and spatial space
    protected IProjections<Vector3, V> m_subspaceVector3V; // Project between 3D axis and spatial space (3D only)
    protected IProjections<float, T> m_subspaceFloatT;     // Project between 1D axis and rotational space
    protected IProjections<Vector2, T> m_subspaceVector2T; // Project between 2D axis and rotational space (3D only)
    protected IProjections<Vector3, T> m_subspaceVector3T; // Project between 3D axis and rotational space (3D only)

    protected ITime m_time;
    protected IRigidbody<Q, V, T> m_rigidbody;
    protected Transform m_owner;
    ControlFieldProfileManager<Q, V, T> m_controlFieldManager;
    DirectionalSourceManager m_sources;
    KVariableLimits m_limits_entityLevel; // Top level of variable limits

    // Convenience
    protected float m_invDeltaT;

    // Previous timestep kinematic variables
    protected V m_position0;
    protected V m_velocity0;
    protected V m_acceleration0;
    protected V m_appliedForce0;
    protected Q m_rotation0;
    protected T m_rotationComponents0;
    protected T m_angularVelocity0;
    protected T m_angularAcceleration0;
    protected T m_appliedTorque0;

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
    float m_localMass;            // Update with m_rigidBody.Mass;
    float m_localDrag;            // Update with m_rigidBody.Drag;
    float m_localAngularDrag;     // Update with m_rigidBody.AngularDrag;

    public IMovementControllerToolset<Q, V, T> Toolset { get=>m_toolset; set=>m_toolset=value; }
    public IProjections<float, V> SubspaceFloatV { get=>m_subspaceFloatV; set=>m_subspaceFloatV=value; }
    public IProjections<Vector2, V> SubspaceVector2V { get=>m_subspaceVector2V; set=>m_subspaceVector2V=value; }
    public IProjections<Vector3, V> SubspaceVector3V { get=>m_subspaceVector3V; set=>m_subspaceVector3V=value; }
    public IProjections<float, T> SubspaceFloatT { get=>m_subspaceFloatT; set=>m_subspaceFloatT=value; }
    public IProjections<Vector2, T> SubspaceVector2T { get=>m_subspaceVector2T; set=>m_subspaceVector2T=value; }
    public IProjections<Vector3, T> SubspaceVector3T { get=>m_subspaceVector3T; set=>m_subspaceVector3T=value; }

    public bool ThreeD { get => GeneralTools.ThreeD<V>(); }
    public bool TwoD { get => GeneralTools.TwoD<V>(); }
    public int NSpatialFreedoms {
        get {
            int dof = 3;
            dof -= m_rigidbody.SpatialConstraint(Axis.X) == true ? 1 : 0;
            dof -= m_rigidbody.SpatialConstraint(Axis.Y) == true ? 1 : 0;
            dof -= m_rigidbody.SpatialConstraint(Axis.Z) == true ? 1 : 0;
            return dof;
        }
    }
    public int NRotationalFreedoms {
        get {
            int dof = 3;
            dof -= m_rigidbody.RotationalConstraint(Axis.X) == true ? 1 : 0;
            dof -= m_rigidbody.RotationalConstraint(Axis.Y) == true ? 1 : 0;
            dof -= m_rigidbody.RotationalConstraint(Axis.Z) == true ? 1 : 0;
            return dof;
        }
    }
    public virtual int NSpatialDimensions() {
        return GeneralTools.NDimensions<V>();
    }
    public virtual int NRotationalDimensions() {
        return GeneralTools.NDimensions<T>();
    }

    // Controls, sources, limits (i.e. kinematic variable-related)
    public ControlFieldProfileManager<Q, V, T> ControlFieldManager { get => m_controlFieldManager; set => m_controlFieldManager = value; }
    public DirectionalSourceManager Sources { get=> m_sources; set => m_sources = value; }
    KVariableLimits Limits_entityLevel { get => m_limits_entityLevel; set => m_limits_entityLevel = value; }

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

        // Local working variables, initialised to hold the actual current state at the start of this iteration
        KVariables<V> spatialVarsInit = new KVariables<V>(
            m_rigidbody.Position,
            m_rigidbody.Velocity,
            m_accelerationActual,
            m_appliedForceActual,
            default(V)
        );
        KVariables<T> rotationalVarsInit = new KVariables<T>(
            m_rigidbody.RotationComponents,
            m_rigidbody.AngularVelocity,
            m_angularAccelerationActual,
            m_appliedTorqueActual,
            default(T)
        );

        // Non-forces start at negative infinite --> acts as a flag 'unused', forces start at zero
        KVariables<V> spatialVarsUpdate = new KVariables<V>(m_toolset.InfiniteV, m_toolset.ZeroV);
        KVariables<T> rotationalVarsUpdate = new KVariables<T>(m_toolset.InfiniteT, m_toolset.ZeroT);
        
        // Always: 2D => spatial dofsUsed[0,1], rotational dofsUsed[2] | 3D => spatial dofsUsed[0,1,2], rotational dofsUsed[3,4,5]
        int nDims = NSpatialDimensions() + NRotationalDimensions();
        DegreesOfFreedomChecker dofsUsed = new DegreesOfFreedomChecker(NSpatialDimensions() + NRotationalDimensions(), ThreeD);

        // Sum all sources
        KVariables<V> spatialSource;
        KVariables<T> rotationalSource;
        m_toolset.SumDirectionalSource(m_sources, m_owner, out spatialSource, out rotationalSource);

        //  - For each active axis:
        //      - Assemble kvarSet, project variables if needed
        //      - Call update on axis
        //      - Add changes back to local working variables
        foreach(ControlFieldProfile<float> controlFieldProfile in m_controlFieldManager.ActiveControlFields1D) {
            ExecuteControlField(
                controlFieldProfile,
                m_subspaceFloatV,
                m_subspaceFloatT,
                spatialVarsInit,
                rotationalVarsInit,
                spatialVarsUpdate,
                rotationalVarsUpdate,
                dofsUsed,
                m_time.fixedDeltaTime
            );
        }
        foreach(ControlFieldProfile<Vector2> controlFieldProfile in m_controlFieldManager.ActiveControlFields2D) {
            ExecuteControlField(
                controlFieldProfile,
                m_subspaceVector2V,
                m_subspaceVector2T,
                spatialVarsInit,
                rotationalVarsInit,
                spatialVarsUpdate,
                rotationalVarsUpdate,
                dofsUsed,
                m_time.fixedDeltaTime
            );
        }
        foreach(ControlFieldProfile<Vector3> controlFieldProfile in m_controlFieldManager.ActiveControlFields3D) {
            ExecuteControlField(
                controlFieldProfile,
                m_subspaceVector3V,
                m_subspaceVector3T,
                spatialVarsInit,
                rotationalVarsInit,
                spatialVarsUpdate,
                rotationalVarsUpdate,
                dofsUsed,
                m_time.fixedDeltaTime
            );
        }
        dofsUsed.Check();

        // TODO I don't think we ApplyLimits with the controlFieldManager... I think it's right here
        // m_controlFieldManager.ApplyLimits(m_limits_entityLevel);
        // TODO Apply limits to local working variables
        // TODO Apply sources
        // TODO Make changes to m_rigidBody variables
        // TODO Apply forces
        // Save current variables to previous time step variables (variables0)
        ArchiveVariables();
    }

    // *** Internal functions
    protected void ExecuteControlField<S>(
        ControlFieldProfile<S> controlFieldProfile,
        IProjections<S, V> projectionToolsetV,
        IProjections<S, T> projectionToolsetT,
        KVariables<V> spatialVarsInit,
        KVariables<T> rotationalVarsInit,
        KVariables<V> spatialVarsUpdate,
        KVariables<T> rotationalVarsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        float deltaTime
    ) {
        if (controlFieldProfile.Type == ControlFieldType.Spatial) {
            projectionToolsetV.ExecuteControlField(
                controlFieldProfile,
                spatialVarsInit,
                spatialVarsUpdate,
                dofsUsed,
                0,
                deltaTime
            );
        } else {
            projectionToolsetT.ExecuteControlField(
                controlFieldProfile,
                rotationalVarsInit,
                rotationalVarsUpdate,
                dofsUsed,
#if DEBUG
                NSpatialDimensions(),
#else
                0,
#endif
                deltaTime
            );
        }
    }

    /// <summary>
    /// Update locally carried kinematic variables, always call base first
    /// </summary>
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
        // Update working variables
        m_localMass = m_rigidbody.Mass;
        m_localDrag = m_rigidbody.Drag;
        m_localAngularDrag = m_rigidbody.AngularDrag;

        // Update accelerations using linear scheme
        m_toolset.UpdateAccelerations(
            m_rigidbody,
            m_invDeltaT,
            m_velocity0,
            ref m_acceleration0,
            ref m_accelerationActual,
            m_rotationComponents0,
            ref m_angularAcceleration0,
            ref m_angularAccelerationActual
        );
    }
    protected void ArchiveVariables() {
        m_position0 = m_rigidbody.Position;
        m_velocity0 = m_rigidbody.Velocity;
        m_acceleration0 = m_accelerationActual;
        m_appliedForce0 = m_appliedForceActual;

        m_rotationComponents0 = m_rigidbody.RotationComponents;
        m_angularVelocity0 = m_rigidbody.AngularVelocity;
        m_angularAcceleration0 = m_angularAccelerationActual;
        m_appliedTorque0 = m_appliedTorqueActual;
    }

    // *** Constructors
    // Need to initialise previous timestep variables with appropriate values
}

// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = Vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float
public class MovementController2D : MovementController<float, Vector2, float> {
    public override int NSpatialDimensions() {
        return 2;
    }
    public override int NRotationalDimensions() {
        return 1;
    }
    protected override void UpdateLocalFields() {
        base.UpdateLocalFields();
        // Linear scheme
        m_acceleration0 = m_accelerationActual;
        m_angularAcceleration0 = m_angularAccelerationActual;
        m_accelerationActual = (m_rigidbody.Velocity - m_velocity0)*m_invDeltaT;
        m_angularAccelerationActual = (m_rigidbody.AngularVelocity - m_angularAcceleration0)*m_invDeltaT;
        KVariables<Vector2> testVars = new KVariables<Vector2>();
    }
}
public class MovementController3D : MovementController<Quaternion, Vector3, Vector3> {
    public override int NSpatialDimensions() {
        return 3;
    }
    public override int NRotationalDimensions() {
        return 3;
    }
    protected override void UpdateLocalFields() {
        base.UpdateLocalFields();
        // Linear scheme
        m_rotationComponents0 = m_rotationComponentsActual;
        m_acceleration0 = m_accelerationActual;
        m_angularAcceleration0 = m_angularAccelerationActual;
        m_accelerationActual = (m_rigidbody.Velocity - m_velocity0)*m_invDeltaT;
        m_angularAccelerationActual = (m_rigidbody.AngularVelocity - m_angularAcceleration0)*m_invDeltaT;
        
    }
}


// Micro-optimization made me stick this into a class
public class DegreesOfFreedomChecker {
#if DEBUG
    private int[] m_dofsUsed;
    private bool m_threeD;
    public void Add(int a) { ++m_dofsUsed[a]; }
    public void Add(int a, int b) { ++m_dofsUsed[a]; ++m_dofsUsed[b]; }
    public void Add(int a, int b, int c) { ++m_dofsUsed[a]; ++m_dofsUsed[b]; ++m_dofsUsed[c]; }
    public void Check() {
        for (int i = 0; i < m_dofsUsed.Length; ++i) {
            if (m_threeD) {
                Debug.LogError(m_dofsUsed[i] + " controlFields attempting to set kinematic variables along " + (AxisNames3D)i + " axis");
            } else {
                Debug.LogError(m_dofsUsed[i] + " controlFields attempting to set kinematic variables along " + (AxisNames2D)i + " axis");
            }
        }
    }
    public DegreesOfFreedomChecker(int size, bool threeD) {
        m_dofsUsed = new int[size];
        m_threeD = threeD;
    }
#else
    public void Add(int a) {}
    public void Add(int a, int b) {}
    public void Add(int a, int b, int c) {}
    public void Check() {}
    public DegreesOfFreedomChecker(int size, bool threeD) {}
#endif
}