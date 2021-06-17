using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlAxis {None, U, V, W} public enum Plane {None, XY, XZ, YZ}
// public enum AlignmentType {None, X, Y, Z, XY, XZ, YZ}

// Ways to interact with RigidBody
// 0 - Do nothing - let's its own physics model handle things
// 1 - Set Position
// 2 - Set velocity
// 3 - Apply force

// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = vector3
//      Axis alignment Vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float
//      Axis alignment Vector2
public class WorldSpace<Q, V, T>
{
    IRigidbody<Q, V, T> m_rigidBody;
    Transform m_owner;

    // Extra kinematic variables that rigidBody doesn't track
    V m_accelerationActual;
    V m_accelerationDesired;
    // V m_jerkActual;
    // V m_jerkDesired;
    V m_appliedForceActual;
    V m_appliedForceDesired;
    V m_appliedForceRateActual;
    V m_appliedForceRateDesired;
    T m_angularAccelerationActual;
    T m_angularAccelerationDesired;
    T m_angularForceActual;
    T m_angularForceDesired;
    T m_angularForceRateActual;
    T m_angularForceRateDesired;

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

    ControlledAxisManager<V> m_axes;


    // TODO Set , Query , Swap axes functionality needed - when changing axes, we have to reset any metadata

    public void Move() {
        Traits<V> traitsV = new Traits<V>();
        // Global
        float mass = m_rigidBody.Mass;

        // Linear
        float drag = m_rigidBody.Drag;
        V position = m_rigidBody.Position;
        V velocity = m_rigidBody.Velocity;
        //V acceleration = traitsV.Zero;
        //V jerk = traitsV.Zero;
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



        foreach(AxisProfile<float, V> axis in m_axes.ActiveAxes1D) {
            if (axis.Projecting) {

            }
        }
    }

    // *** Internal functions
    bool CheckSetup() {
        int nFreedoms = NSpatialFreedoms + NRotationalFreedoms;
        int nControls = m_axes.ActiveAxes1D.Count + 2*m_axes.ActiveAxes2D.Count + 3*m_axes.ActiveAxes3D.Count;
        // This check is not informative, because some control axes may be ForceUsers, which can overlap with other ForceUsers and one StateSetters
        // if (nFreedoms - nControls < 0) {
        //     Debug.LogError("Overconstrained system: " + nFreedoms + " freedoms, " + nControls + " controls.");
        //     return false;
        // }
        Vector3Int fixedSpatial = Vector3Int.zero;
        Vector3Int fixedRotational = Vector3Int.zero;
        int nSpatial = NSpatialFreedoms;
        int nRotational = NRotationalFreedoms;
        foreach (AxisProfile<float, V> axis in m_axes.ActiveAxes1D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Control.StateSetter()) {
                if (axis.Type == AxisType.Spatial) {
                    nSpatial -= axis.NControlledDimensions;
                } else if (axis.Type == AxisType.Rotational) {
                    nRotational -= axis.NControlledDimensions;
                }
            }
        }
        foreach (AxisProfile<Vector2, V> axis in m_axes.ActiveAxes2D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Control.StateSetter()) {
                if (axis.Type == AxisType.Spatial) {
                    nSpatial -= axis.NControlledDimensions;
                } else if (axis.Type == AxisType.Rotational) {
                    nRotational -= axis.NControlledDimensions;
                }
            }
        }
        foreach (AxisProfile<Vector3, V> axis in m_axes.ActiveAxes3D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Control.StateSetter()) {
                if (axis.Type == AxisType.Spatial) {
                    nSpatial -= axis.NControlledDimensions;
                } else if (axis.Type == AxisType.Rotational) {
                    nRotational -= axis.NControlledDimensions;
                }
            }
        }
        // Now check results of summations
        bool pass = true;
        if (nSpatial < 0) {
            Debug.LogError("Spatially overconstrained with " + nSpatial + " too many spatial axes controlled by StateSetter types.");
            pass = false;
        }
        if (nRotational < 0) {
            Debug.LogError("Rotationally overconstrained with " + nRotational + " too many rotational axes controlled by StateSetter types.");
            pass = false;
        }
        bool passSpace = true;
        bool passRotate = true;
        for (int i = 0; i < 3; ++i) {
            if (fixedSpatial[i] > 1) { passSpace = false; }
            if (fixedRotational[i] > 1) { passRotate = false;}
        }
        if (passSpace && passRotate) {
            return pass;
        }
        string spaceFail = "";
        string rotateFail = "";
        if (!passSpace) {
            spaceFail = " Spatial assignment = " + fixedSpatial;
        }
        if (!passRotate) {
            rotateFail = " Rotation assignment = " + fixedRotational;
        }
        Debug.Log("Number of StateSetter type axis controllers aligned to each axis cannot exceed 1." + spaceFail + rotateFail);
        return false;
    }
}

