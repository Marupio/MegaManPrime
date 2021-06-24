using System.Collections;
using UnityEngine;

public enum Axis {None, X, Y, Z}
public enum ControlFieldType{Unknown, Spatial, Rotational}
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

public interface IControlFieldProfile {
    
}

// T is based on what we control : 1 axis = float, 2 axes = Vector2, 3 axes = Vector3
public abstract class ControlFieldProfile<T> {
    protected Transform m_owner;
    protected IControlFieldProfileManager m_manager;
    protected string m_name;
    protected int m_nAvailableDimensions;

    protected ControlFieldType m_type;
    /// <summary>
    /// Axis alignment - how is this control axes aligned with the entity (player/enemy)'s position
    /// 3D World Space
    ///     1D Spatial|Rotational   - None  = free, X axis orients with m_direction, expressed relative to entity
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
    protected bool m_active; // true when contained in an 'activeControlFields' list in a ControFieldProfileManager
    protected Quaternion m_direction; // direction X axis is facing within its subspace, relative to entity's axes
    protected bool m_projecting; // true if kvars need to be projected to axial directions
    protected Vector3Int m_usedAxes;

    public IControlFieldProfileManager Manager { get=>m_manager; }
    public string Name { get => m_name; set => m_name = value; }
    public int NAvailableDimensions { get => m_nAvailableDimensions; }
    public abstract int NControlledDimensions { get; }
    public ControlFieldType Type { get => m_type; }
    public AxisPlaneSpace Alignment { get => m_alignment; }
    public ControlField<T> Control { get => m_control; set => m_control = value; }
    public bool Active { get => m_active; }
    public Quaternion Direction { get => m_direction; set => m_direction = value; }
    public bool Projecting { get => m_projecting; }

    // Functions intended to be used internally by controlField machinery
    public Vector3Int InternalCheckUsedAxes { get => m_usedAxes; }
    public void InternalSetManager(IControlFieldProfileManager newManager) { m_manager = newManager; }
    public void InternalActivate() { m_active = true;}
    public void InternalDeactivate() { m_active = false;}

    // *** Constructors
    public ControlFieldProfile(
        string name,
        Transform owner,
        ControlFieldType type,
        int nAvailableDimensions,
        AxisPlaneSpace alignment,
        ControlField<T> control
    ) {
        m_name = name;
        m_nAvailableDimensions = nAvailableDimensions;
        m_owner = owner;
        m_type = type;
        if ((int)alignment > 7) {
            Debug.LogError("Out-of-range setting for axis alignment: " + alignment + ", setting to " + AxisPlaneSpace.None);
            alignment = AxisPlaneSpace.None;
        }
        m_alignment = alignment;
        m_control = control;
        m_active = false;
        m_direction = Quaternion.identity;
        m_projecting = false;
        m_usedAxes = Vector3Int.zero;
        CheckAxes();
    }

    // *** Protected member functions
    protected bool CheckAxes() {
        if (m_nAvailableDimensions < NControlledDimensions) {
            Debug.LogError(
                "Attempting to control " + NControlledDimensions + " " + m_type + " dimensions when only " + m_nAvailableDimensions +
                " are available."
            );
            return false;
        }
        switch (m_nAvailableDimensions) {
            case 1: {
                // 2D rotation
                m_alignment = AxisPlaneSpace.Z;
                if (m_control.StateSetter()) { ++m_usedAxes[0]; }
                return true;
            }
            case 2: {
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
                            if (m_control.StateSetter()) { ++m_usedAxes[(int)m_alignment]; }
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
                                ++m_usedAxes[0];
                                ++m_usedAxes[1];
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
                    default: {
                        Debug.LogError("Unhandled case");
                        return false;
                    }
                } // end switch (NControlledDimensions)
            } // end case 2:
            case 3: {
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
                            return true;
                        } else if (nAxes == 1) {
                            if (m_control.StateSetter()) {
                                ++m_usedAxes[(int)m_alignment];
                            }
                            return true;
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
                                return true;
                            case AxisPlaneSpace.X:
                            case AxisPlaneSpace.Y:
                            case AxisPlaneSpace.Z:
                                Debug.LogError("Attempting to align 2D control plane with 1D axis");
                                return false;
                            case AxisPlaneSpace.XY:
                                if (m_control.StateSetter()) {
                                    ++m_usedAxes[0];
                                    ++m_usedAxes[1];
                                }
                                return true;
                            case AxisPlaneSpace.XZ:
                                if (m_control.StateSetter()) {
                                    ++m_usedAxes[0];
                                    ++m_usedAxes[2];
                                }
                                return true;
                            case AxisPlaneSpace.YZ:
                                if (m_control.StateSetter()) {
                                    ++m_usedAxes[1];
                                    ++m_usedAxes[2];
                                }
                                return true;
                            case AxisPlaneSpace.XYZ:
                                Debug.LogError("Attempting to align 2D control plane to 3D world space.");
                                return false;
                            default:
                                Debug.LogError("Unhandled case");
                                return false;
                        }
                    }
                    case 3: {
                        // Can be floating or fixed
                        if (m_alignment == AxisPlaneSpace.XYZ) {
                            if (m_control.StateSetter()) {
                                m_usedAxes += new Vector3Int(1, 1, 1);
                            }
                            return true;
                        } else if (m_alignment == AxisPlaneSpace.None) {
                            // m_direction points along X axis
                            m_projecting = true;
                            return true;
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
            } // end case 3:
            default: {
                Debug.LogError("Unhandled number of dimensions");
                return false;
            }
        }
    }
}

// *** Concrete types - ControlField
public class ControlField1D : ControlFieldProfile<float> {
    public override int NControlledDimensions { get => 1; }
    public ControlField1D(
        string name,
        Transform owner,
        ControlFieldType type,
        int nAvailableDimensions,
        AxisPlaneSpace alignment,
        ControlField<float> control
    ) : base (name, owner, type, nAvailableDimensions, alignment, control)
    {}
}
public class ControlField2D : ControlFieldProfile<Vector2> {
    public override int NControlledDimensions { get => 2; }
    public ControlField2D(
        string name,
        Transform owner,
        ControlFieldType type,
        int nAvailableDimensions,
        AxisPlaneSpace alignment,
        ControlField<Vector2> control
    ) : base (name, owner, type, nAvailableDimensions, alignment, control)
    {}
}
public class ControlField3D : ControlFieldProfile<Vector3> {
    public override int NControlledDimensions { get => 3; }
    public ControlField3D(
        string name,
        Transform owner,
        ControlFieldType type,
        int nAvailableDimensions,
        AxisPlaneSpace alignment,
        ControlField<Vector3> control
    ) : base (name, owner, type, nAvailableDimensions, alignment, control)
    {}
}
