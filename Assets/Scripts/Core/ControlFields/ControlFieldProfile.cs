using System.Collections;
using UnityEngine;

public enum Axis {None, X, Y, Z}
public enum AxisType{Unknown, Spatial, Rotational}
public enum AxisPlaneSpace {
    None    = 0b_0000_0000,
    X       = 0b_0000_0001,
    Y       = 0b_0000_0010,
    Z       = 0b_0000_0100,
    XY      = 0b_0000_0011,
    XZ      = 0b_0000_0101,
    YZ      = 0b_0000_0110,
    XYZ     = 0b_0000_0111
}

// AxisPlaneSpace switch
// switch (m_alignment) {
//     case AxisPlaneSpace.None:
//     case AxisPlaneSpace.X:
//     case AxisPlaneSpace.Y:
//     case AxisPlaneSpace.Z:
//     case AxisPlaneSpace.XY:
//     case AxisPlaneSpace.XZ:
//     case AxisPlaneSpace.YZ:
//     case AxisPlaneSpace.XYZ:
//         break;
// }


// T is based on what we control : 1 axis = float, 2 axes = Vector2, 3 axes = Vector3
// D is based on Rigidbody model : 2D = Vector2, 3D = Vector3
public abstract class ControlFieldProfile<T, D> {
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
    protected AxisPlaneSpace m_alignment;
    protected ControlField<T> m_control;
    protected D m_direction;
    protected bool m_projecting; // true if kvars need to be projected to axial directions
    protected Vector3Int m_usedRotationalAxes;
    protected Vector3Int m_usedSpatialAxes;

    public string Name { get => m_name; set => m_name = value; }
    public bool ThreeD { get => GeneralTools.ThreeD<D>(); }
    public bool TwoD { get => GeneralTools.TwoD<D>(); }
    public abstract int NControlledDimensions { get; }
    public AxisType Type { get => m_type; }
    public AxisPlaneSpace Alignment { get => m_alignment; }
    public ControlField<T> Control { get => m_control; set => m_control = value; }
    public D Direction { get => m_direction; set => m_direction = value; }
    public bool Projecting { get => m_projecting; }
    public Vector3Int CheckUsedRotationalAxes { get => m_usedRotationalAxes; }
    public Vector3Int CheckUsedSpatialAxes { get => m_usedSpatialAxes; }

    // *** Constructors
    public ControlFieldProfile(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<T> control) {
        m_name = name;
        m_owner = owner;
        m_type = type;
        if ((int)alignment > 7) {
            Debug.LogError("Out-of-range setting for axis alignment: " + alignment + ", setting to " + AxisPlaneSpace.None);
            alignment = AxisPlaneSpace.None;
        }
        m_alignment = alignment;
        m_control = control;
        m_direction = default(D);
        m_projecting = false;
        m_usedRotationalAxes = Vector3Int.zero;
        m_usedSpatialAxes = Vector3Int.zero;
        CheckAxes();
    }

    // *** Protected member functions
    protected bool CheckAxes() {
        if (TwoD) {
            if (NControlledDimensions > 2) {
                Debug.LogError("Too many controlled dimensions in 2D space on ControlField " + m_name);
                return false;
            }
            if (m_type == AxisType.Rotational) {
                // Automatic, ignore user input
                m_alignment = AxisPlaneSpace.Z;
                if (m_control.StateSetter()) { ++m_usedRotationalAxes[2]; }
                return true;
            }
            switch (NControlledDimensions) {
                case 0: {
                    // Uncontrolled
                    m_alignment = AxisPlaneSpace.None;
                    return true;
                }
                case 1: {
                    int nAxes = 0;
                    nAxes += (m_alignment & AxisPlaneSpace.X) == AxisPlaneSpace.X ? 1 : 0;
                    nAxes += (m_alignment & AxisPlaneSpace.Y) == AxisPlaneSpace.Y ? 1 : 0;
                    nAxes += (m_alignment & AxisPlaneSpace.Z) == AxisPlaneSpace.Z ? 1 : 0;
                    if (nAxes == 0) {
                        m_projecting = true;
                        return true;
                    } else if (nAxes == 1) {
                        if (m_control.StateSetter()) { ++m_usedSpatialAxes[(int)m_alignment]; }
                        return true;
                    } else {
                        Debug.LogError("Attempting to align 1 dimensional control axis with " + nAxes + " world axes");
                        return false;
                    }
                }
                case 2: {
                    // It can still be free or fixed
                    if (m_alignment == AxisPlaneSpace.XY) {
                        if (m_control.StateSetter()) {
                            ++m_usedSpatialAxes[0];
                            ++m_usedSpatialAxes[1];
                        }
                        return true;
                    } else if (m_alignment == AxisPlaneSpace.None) {
                        // m_direction points along x
                        m_projecting = true;
                        return true;
                    } else {
                        Debug.LogError("Unhandled alignment setting - 2D control plane in 2D space can either be XY or None");
                        return false;
                    }
                }
                case 3:
                    Debug.LogError("Attempting to control more dimensions than world space");
                    return false;
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
                    m_alignment = AxisPlaneSpace.None;
                    return true;
                }
                case 1: {
                    int nAxes = 0;
                    nAxes += (m_alignment & AxisPlaneSpace.X) == AxisPlaneSpace.X ? 1 : 0;
                    nAxes += (m_alignment & AxisPlaneSpace.Y) == AxisPlaneSpace.Y ? 1 : 0;
                    nAxes += (m_alignment & AxisPlaneSpace.Z) == AxisPlaneSpace.Z ? 1 : 0;
                    if (nAxes == 0) {
                        m_projecting = true;
                        break;
                    } else if (nAxes == 1) {
                        if (m_control.StateSetter()) {
                            ++usedAxisDelta[(int)m_alignment];
                        }
                        break;
                    } else {
                        Debug.LogError("Attempting to align a control axis with more than one dimension");
                        return false;
                    }
                }
                case 2: {
                    // We only allow fully fixed or fully floating
                    switch (m_alignment) {
                        case AxisPlaneSpace.None:
                            m_projecting = true;
                            break;
                        case AxisPlaneSpace.X:
                        case AxisPlaneSpace.Y:
                        case AxisPlaneSpace.Z:
                            Debug.LogError("Attempting to align 2D control plane with 1D axis");
                            return false;
                        case AxisPlaneSpace.XY:
                            if (m_control.StateSetter()) {
                                ++usedAxisDelta[0];
                                ++usedAxisDelta[1];
                            }
                            break;
                        case AxisPlaneSpace.XZ:
                            if (m_control.StateSetter()) {
                                ++usedAxisDelta[0];
                                ++usedAxisDelta[2];
                            }
                            break;
                        case AxisPlaneSpace.YZ:
                            if (m_control.StateSetter()) {
                                ++usedAxisDelta[1];
                                ++usedAxisDelta[2];
                            }
                            break;
                        case AxisPlaneSpace.XYZ:
                            Debug.LogError("Attempting to align 2D control plane to 3D world space.");
                            break;
                    }
                    break;
                }
                case 3: {
                    // Can be floating or fixed
                    if (m_alignment == AxisPlaneSpace.XYZ) {
                        if (m_control.StateSetter()) {
                            usedAxisDelta += new Vector3Int(1, 1, 1);
                        }
                        break;
                    } else if (m_alignment == AxisPlaneSpace.None) {
                        // m_direction points along X axis
                        m_projecting = true;
                        break;
                    } else {
                        Debug.LogError("Unhandled alignment setting - 3D control space in 3D world space can align to either XYZ or None");
                        return false;
                    }
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
                Debug.LogError("Unknown axis type - expecting rotational / spatial, problem in ControlField " + m_name);
                return false;
            }
        } else {
            Debug.LogError("Unhandled number of spatial dimensions");
            return false;
        }
    }
}

// *** Concrete types - ControlField
public class ControlField1D_2 : ControlFieldProfile<float, Vector2> {
    public override int NControlledDimensions { get => 1; }
    public ControlField1D_2(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class ControlField1D_3 : ControlFieldProfile<float, Vector3> {
    public override int NControlledDimensions { get => 1; }
    public ControlField1D_3(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class ControlField2D_2 : ControlFieldProfile<float, Vector2> {
    public override int NControlledDimensions { get => 2; }
    public ControlField2D_2(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class ControlField2D_3 : ControlFieldProfile<float, Vector3> {
    public override int NControlledDimensions { get => 2; }
    public ControlField2D_3(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class ControlField3D_2 : ControlFieldProfile<float, Vector2> {
    public override int NControlledDimensions { get => 3; }
    public ControlField3D_2(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
public class ControlField3D_3 : ControlFieldProfile<float, Vector3> {
    public override int NControlledDimensions { get => 3; }
    public ControlField3D_3(string name, Transform owner, AxisType type, AxisPlaneSpace alignment, ControlField<float> control)
    : base (name, owner, type, alignment, control)
    {}
}
