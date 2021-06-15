using System.Collections.Generic;
using UnityEngine;

public enum Axis {None, X, Y, Z}
public enum Plane {None, XY, XZ, YZ}

// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float

public class WorldSpace<Q, V, T> {
    IRigidbody<Q, V, T> m_rigidBody;
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

    Dictionary<string, AxisMovement<float>> m_axes1D;
    Dictionary<string, AxisMovement<Vector2>> m_axes2D;
    Dictionary<string, AxisMovement<Vector3>> m_axes3D;
    List<AxisMovement<float>> m_activeAxes1D;
    List<AxisMovement<Vector2>> m_activeAxes2D;
    List<AxisMovement<Vector3>> m_activeAxes3D;

    bool CheckSetup() {
        int nFreedoms = NSpatialFreedoms + NRotationalFreedoms;
        int nControls = m_activeAxes1D.Count + 2*m_activeAxes2D.Count + 3*m_activeAxes3D.Count;
        if (nFreedoms - nControls < 0) {
            Debug.LogError("Overconstrained system: " + nFreedoms + " freedoms, " + nControls + " controls.");
            return false;
        }
        return true;
    }
}

//2D:
//  XY, rotation
//  X, Y, rotation
//  1d+rotation
//  1d
//  rotation
//
// 3D
//  0s : XYZ | XY,Z | XZ,Y | YZ,X | 2d,1d | 1d,1d,1d
//  1s : XY | XZ | YZ | 2d | X,Y | X,Z | Y,Z | 1d,1d
//  2s : X | Y | Z | 1d
//  XYZ