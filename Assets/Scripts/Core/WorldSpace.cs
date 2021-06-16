using System;
using System.Collections.Generic;
using UnityEngine;

public enum Axis {None, X, Y, Z}
public enum ControlAxis {None, U, V, W}
public enum Plane {None, XY, XZ, YZ}
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

    Dictionary<string, AxisProfile<float, V>> m_axes1D;
    Dictionary<string, AxisProfile<Vector2, V>> m_axes2D;
    Dictionary<string, AxisProfile<Vector3, V>> m_axes3D;
    List<AxisProfile<float, V>> m_activeAxes1D;
    List<AxisProfile<Vector2, V>> m_activeAxes2D;
    List<AxisProfile<Vector3, V>> m_activeAxes3D;
    
    // TODO Set , Query , Swap axes functionality needed

    public void Move() {
        foreach(AxisProfile<float, V> axis in m_activeAxes1D) {
            // Project if needed
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
        Vector3Int fixedSpatial = Vector3Int.zero;
        Vector3Int fixedRotational = Vector3Int.zero;
        int nSpatial = NSpatialFreedoms;
        int nRotational = NRotationalFreedoms;
        foreach (AxisProfile<float, V> axis in m_activeAxes1D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Type == AxisType.Spatial) {
                nSpatial -= axis.NControlledDimensions;
            } else if (axis.Type == AxisType.Rotational) {
                nRotational -= axis.NControlledDimensions;
            }
        }
        foreach (AxisProfile<Vector2, V> axis in m_activeAxes2D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Type == AxisType.Spatial) {
                nSpatial -= axis.NControlledDimensions;
            } else if (axis.Type == AxisType.Rotational) {
                nRotational -= axis.NControlledDimensions;
            }
        }
        foreach (AxisProfile<Vector3, V> axis in m_activeAxes3D) {
            fixedSpatial += axis.CheckUsedSpatialAxes;
            fixedRotational += axis.CheckUsedRotationalAxes;
            if (axis.Type == AxisType.Spatial) {
                nSpatial -= axis.NControlledDimensions;
            } else if (axis.Type == AxisType.Rotational) {
                nRotational -= axis.NControlledDimensions;
            }
        }
        // Now check results of summations
        bool pass = true;
        if (nSpatial < 0) {
            Debug.LogError("Spatially overconstrained with " + nSpatial + " too many spatial axes controlled.");
            pass = false;
        }
        if (nRotational < 0) {
            Debug.LogError("Rotationally overconstrained with " + nRotational + " too many rotational axes controlled.");
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
        Debug.Log("Number of axis controllers aligned to each axis cannot exceed 1." + spaceFail + rotateFail);
        return false;
    }
}

public enum AxisType{Unknown, Spatial, Rotational}

// T is based on what we control : 1 axis = float, 2 axes = Vector2, 3 axes = Vector3
// D is based on Rigidbody model : 2D = Vector2, 3D = Vector3
public abstract class AxisProfile<T, D> {
    protected Transform m_owner;
    protected string m_name;
    protected AxisType m_type;
    /// <summary>
    /// Axis alignment - how is this control axes aligned with the entity (player/enemy)'s position
    /// 3D World Space
    ///     1D Spatial|Rotational   - None  = free, axis aligns with m_direction
    ///                             - X|Y|Z = aligned with entity's X,Y,Z
    ///     2D Spatial|Rotational   - None  = free, normal aligns with m_direction
    ///                             - X|Y|Z = plane normal aligned with axes
    ///     3D Spatial|Rotational   - None  = aligned with entity's X,Y,Z   *** AUTOMATIC
    ///                             - X|Y|Z = not allowed                   *** IGNORED
    /// 2D World  Space
    ///     1D Spatial      - None      = free, axis aligns with m_direction
    ///                     - X|Y       = aligned with entity's X,Y
    ///                     - Z         = not allowed
    ///     1D Rotational   - Z         = aligned with entity's Z
    ///                     - X|Y|None  = not allowed
    ///     2D Spatial      - None|Z    = normal aligned with Z     *** AUTOMATIC
    ///                     - X,Y       = not allowed               *** IGNORED
    /// </summary>
    protected Axis m_alignment;
    protected AxisMovement<T> m_control;
    protected D m_direction;
    protected bool m_projecting; // true if kvars need to be projected to axial directions
    protected Vector3Int m_usedRotationalAxes;
    protected Vector3Int m_usedSpatialAxes;

    public string Name { get => m_name; set => m_name = value; }
    public bool ThreeD { get => GeneralTools.ThreeD<D>(); }
    public bool TwoD { get => GeneralTools.TwoD<D>(); }
    public abstract int NControlledDimensions { get; }
    public AxisType Type { get => m_type; }
    public Axis Alignment { get => m_alignment; }
    public AxisMovement<T> Control { get => m_control; set => m_control = value; }
    public D Direction { get => m_direction; set => m_direction = value; }
    public bool Projecting { get => m_projecting; }
    public Vector3Int CheckUsedRotationalAxes { get => m_usedRotationalAxes; }
    public Vector3Int CheckUsedSpatialAxes { get => m_usedSpatialAxes; }

    // *** Constructors
    public AxisProfile(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<T> control) {
        m_name = name;
        m_owner = owner;
        m_type = type;
        m_alignment = alignment;
        m_control = control;
        m_direction = (new Traits<D>()).Zero;
        m_projecting = false;
        m_usedRotationalAxes = Vector3Int.zero;
        m_usedSpatialAxes = Vector3Int.zero;
        CheckAxes();
    }

    // *** Protected member functions
    protected bool CheckAxes() {
        if (TwoD) {
            if (NControlledDimensions > 2) {
                Debug.LogError("Too many controlled dimensions in 2D space on AxisProfile " + m_name);
                return false;
            }
            if (m_type == AxisType.Rotational) {
                m_alignment = Axis.Z;
                ++m_usedRotationalAxes[2];
                return true;
            }
            switch (NControlledDimensions) {
                case 0: {
                    // Uncontrolled
                    m_alignment = Axis.None;
                    return true;
                }
                case 1: {
                    switch (m_alignment) {
                        case Axis.None:
                            m_projecting = true;
                            return true;
                        case Axis.X:
                            ++m_usedSpatialAxes[0];
                            return true;
                        case Axis.Y:
                            ++m_usedSpatialAxes[1];
                            return true;
                        case Axis.Z:
                            Debug.LogError("Spatial Z-axis alignment specified in 2D space for AxisProfile " + m_name);
                            return false;
                        default:
                            Debug.LogError("Unhandled case");
                            return false;
                    }
                }
                case 2: {
                    switch (m_alignment) {
                        case Axis.None:
                        case Axis.Z:
                            // Z and None both are okay
                            m_alignment = Axis.None;
                            ++m_usedSpatialAxes[0];
                            ++m_usedSpatialAxes[1];
                            return true;
                        case Axis.X:
                        case Axis.Y:
                            Debug.LogError("Cannot align 2D control axes with X or Y in 2D space, problem in AxisProfiel " + m_name);
                            return false;
                        default:
                            Debug.LogError("Unhandled case");
                            return false;
                    }
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return false;
                }
            }
        } else if (ThreeD) {
            Vector3Int usedAxisDelta = Vector3Int.zero;
            switch(NControlledDimensions) {
                case 0: {
                    // Uncontrolled
                    m_alignment = Axis.None;
                    return true;
                }
                case 1: {
                    switch (m_alignment) {
                        case Axis.None: {
                            m_projecting = true;
                            return true;
                        }
                        case Axis.X: {
                            ++usedAxisDelta[0];
                            break;
                        }
                        case Axis.Y: {
                            ++usedAxisDelta[1];
                            break;
                        }
                        case Axis.Z: {
                            ++usedAxisDelta[2];
                            break;
                        }
                        default: {
                            Debug.LogError("Unhandled case");
                            return false;
                        }
                    }
                    break;
                }
                case 2: {
                    switch (m_alignment) {
                        case Axis.None: {
                            m_projecting = true;
                            return true;
                        }
                        case Axis.X: {
                            ++usedAxisDelta[1];
                            ++usedAxisDelta[2];
                            break;
                        }
                        case Axis.Y: {
                            ++usedAxisDelta[0];
                            ++usedAxisDelta[2];
                            break;
                        }
                        case Axis.Z: {
                            ++usedAxisDelta[0];
                            ++usedAxisDelta[1];
                            break;
                        }
                        default: {
                            Debug.LogError("Unhandled case");
                            return false;
                        }
                    }
                    break;
                }
                case 3: {
                    m_alignment = Axis.None;
                    ++usedAxisDelta[0];
                    ++usedAxisDelta[1];
                    ++usedAxisDelta[2];
                    break;
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return false;
                }
            }
            if (m_type == AxisType.Rotational) {
                m_usedRotationalAxes += usedAxisDelta;
                return true;
            } else if (m_type == AxisType.Spatial) {
                m_usedSpatialAxes += usedAxisDelta;
                return true;
            } else {
                Debug.LogError("Unknown axis type - expecting rotational / spatial, problem in AxisProfile " + m_name);
                return false;
            }
        } else {
            Debug.LogError("Unknown number of spatial dimensions");
            return false;
        }
    }
}

// *** Concrete types - AxisProfile
public class AxisProfile1D_2 : AxisProfile<float, Vector2> {
    public override int NControlledDimensions { get => 1; }
    public AxisProfile1D_2(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class AxisProfile1D_3 : AxisProfile<float, Vector3> {
    public override int NControlledDimensions { get => 1; }
    public AxisProfile1D_3(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class AxisProfile2D_2 : AxisProfile<float, Vector2> {
    public override int NControlledDimensions { get => 2; }
    public AxisProfile2D_2(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class AxisProfile2D_3 : AxisProfile<float, Vector3> {
    public override int NControlledDimensions { get => 2; }
    public AxisProfile2D_3(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class AxisProfile3D_2 : AxisProfile<float, Vector2> {
    public override int NControlledDimensions { get => 3; }
    public AxisProfile3D_2(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class AxisProfile3D_3 : AxisProfile<float, Vector3> {
    public override int NControlledDimensions { get => 3; }
    public AxisProfile3D_3(string name, Transform owner, AxisType type, Axis alignment, AxisMovement<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
