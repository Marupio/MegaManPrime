using System.Collections.Generic;
using UnityEngine;

public enum Axis {None, X, Y, Z}
public enum ControlAxis {None, U, V, W}
public enum Plane {None, XY, XZ, YZ}
// public enum AlignmentType {None, X, Y, Z, XY, XZ, YZ}

// WorldSpace3D (Rigidbody)   : Q = Quaternion, V = Vector3, T = vector3
//      Axis alignment Vector3
// WorldSpace2D (Rigidbody2D) : Q = float, V = Vector2, T = float
//      Axis alignment Vector2


public class WorldSpace<Q, V, T> where V : class
{
    IRigidbody<Q, V, T> m_rigidBody;
    Transform m_transform;

    public bool ThreeD { get => V is Vector3; }
    public bool TwoD { get => V is Vector2; }
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

    Dictionary<string, AxisProfile<float>> m_axes1D;
    Dictionary<string, AxisProfile<Vector2>> m_axes2D;
    Dictionary<string, AxisProfile<Vector3>> m_axes3D;
    List<AxisProfile<float>> m_activeAxes1D;
    List<AxisProfile<Vector2>> m_activeAxes2D;
    List<AxisProfile<Vector3>> m_activeAxes3D;
    
    // TODO Set , Query , Swap axes functionality needed

    public void Move() {
        foreach(AxisProfile<float> axis in m_activeAxes1D) {
            // Get scalars from direction
        }
    }

    // *** Internal functions
    bool CheckSetup() {
        int nFreedoms = NSpatialFreedoms + NRotationalFreedoms;
        int nControls = m_activeAxes1D.Count + 2*m_activeAxes2D.Count + 3*m_activeAxes3D.Count;
        if (nFreedoms - nControls < 0) {
            Debug.LogError("Overconstrained system: " + nFreedoms + " freedoms, " + nControls + " controls.");
            return false;
        }
        bool[] fixedSpatial = new bool[NSpatialFreedoms];
        bool[] fixedRotational = new bool[NRotationalFreedoms];
        Array.Fill(fixedSpatial, false);
        Array.Fill(fixedRotational, false);
        int nSpatial = NSpatialFreedoms;
        int nRotational = NRotationalFreedoms;
        foreach (AxisProfile<float> axis in m_activeAxes1D) {
            if (axis.Type == AxisType.Spatial) {
                nSpatial -= axis.NDimensions;
                if (!CheckAxisAlignemnt(1, axis, fixedSpatial)) {
                    // Failed axis alignment
                    return false;
                }
            } else if (axis.Type == AxisType.Rotational) {
                if (!CheckAxisAlignemnt(1, axis, fixedRotational)) {
                    // Failed axis alignment
                    return false;
                }
            }
        }
        foreach (AxisProfile<Vector2> axis in m_activeAxes2D) {
            if (axis.Type == AxisType.Spatial) {
                nSpatial -= axis.NDimensions;
                if (!CheckAxisAlignemnt(2, axis, fixedSpatial)) {
                    // Failed axis alignment
                    return false;
                }
            } else if (axis.Type == AxisType.Rotational) {
                if (!CheckAxisAlignemnt(2, axis, fixedRotational)) {
                    // Failed axis alignment
                    return false;
                }
            }
        }
        foreach (AxisProfile<Vector3> axis in m_activeAxes3D) {
            if (axis.Type == AxisType.Spatial) {
                nSpatial -= axis.NDimensions;
                if (!CheckAxisAlignemnt(3, axis, fixedSpatial)) {
                    // Failed axis alignment
                    return false;
                }
            } else if (axis.Type == AxisType.Rotational) {
                if (!CheckAxisAlignemnt(3, axis, fixedRotational)) {
                    // Failed axis alignment
                    return false;
                }
            }
        }
        return true;
    }
    bool CheckAxisAlignemnt<S>(int nDimsExpected, AxisProfile<S> axis, bool[] fixedArray) {
        if (axis.NDimensions != nDimsExpected) {
            Debug.LogError("Incorrectly typed AxisProfile " + axis.Name);
        }
        int[] addresses = new int[3];
        Array.Fill(addresses, -1);
        if (axis.Type == AxisType.Rotational && TwoD) {
            // 2D rotational - exceptional circumstances
            if (axis.NDimensions > 1 || axis.Alignment != Axis.Z) {
                Debug.LogError("2D rotational axis requires 1D axis with Axis.Z alignment, incorrect in AxisProfile " + axis.Name);
                return false;
            } else {
                addresses[0] = 0;
            }
        }
        // Fill in fixedArray based on axis settings
        switch (axis.NDimensions) {
            case 1:
                switch (axis.Alignment) {
                    case Axis.None:
                        // does not fill fixedArray
                        break;
                    case Axis.X:
                        addresses[0] = 0;
                        break;
                    case Axis.Y:
                        addresses[0] = 1;
                        break;
                    case Axis.Z:
                        if (TwoD) {
                            Debug.LogError("Z axis constrained in 2D in AxisProfile " + axis.Name);
                            return false;
                        }
                        addresses[0] = 2;
                        break;
                    default:
                        Debug.LogError("Unhandled case");
                        return false;
                }
                break;
            case 2:
                switch (axis.Alignment) {
                    case Axis.None:
                        if (TwoD) {
                            // Assume it fills entire array
                            addresses[0] = 0;
                            addresses[1] = 1;
                        }
                        // Otherwise, does not fill fixedArray
                        break;
                    case Axis.X:
                        if (TwoD) {
                            Debug.LogError("2D AxisProfile can only be aligned with Axis.Z in 2D WorldSpace, in AxisProfile " + axis.Name);
                            return false;
                        }
                        addresses[0] = 1;
                        addresses[1] = 2;
                        break;
                    case Axis.Y:
                        if (TwoD) {
                            Debug.LogError("2D AxisProfile can only be aligned with Axis.Z in 2D WorldSpace, in AxisProfile " + axis.Name);
                            return false;
                        }
                        addresses[0] = 0;
                        addresses[1] = 2;
                        break;
                    case Axis.Z:
                        addresses[0] = 0;
                        addresses[1] = 1;
                        break;
                    default:
                        Debug.LogError("Unhandled case");
                        return false;
                }
                break;
            case 3:
                if (TwoD) {
                    Debug.Log("Cannot have 3D axis in 2D world space, in AxisProfile " + axis.Name);
                    return false;
                }
                switch (axis.Alignment) {
                    case Axis.None:
                        // Assume fills the entire array
                        addresses[0] = 0;
                        addresses[1] = 1;
                        addresses[2] = 2;
                        // Does not fill fixedArray
                        break;
                    case Axis.X:
                    case Axis.Y:
                    case Axis.Z:
                        Debug.LogError("Cannot have 3D axis aligned with X,Y or Z.  Can only be None.  Problem with " + axis.Name);
                        break;
                    default:
                        Debug.LogError("Unhandled case");
                        return false;
                }
                break;
        }
        foreach(int i in addresses) {
            if (i >= 0) {
                if (fixedArray[i])
                {
                    Debug.LogError("Too many axes aligned to " + (Axis)i + " axis, including " + axis.Name);
                    return false;
                }
                fixedArray[i] = true;
            }
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

public enum AxisType{Unknown, Spatial, Rotational}

public class AxisProfile<T> {
    private string m_name;
    private int m_nDimensions;
    private AxisType m_type;
    private Axis m_alignment;
    private AxisMovement<T> m_controlledAxis;
    /// <summary>
    /// The direction this axis is aligned with currently
    /// </summary>
    private Vector3 m_direction;

    public string Name { get => m_name; set => m_name = value; }
    public int NDimensions { get => m_nDimensions; }
    public AxisType Type { get => m_type; set => m_type = value; }
    public Axis Alignment { get => m_alignment; set => m_alignment; }
}
