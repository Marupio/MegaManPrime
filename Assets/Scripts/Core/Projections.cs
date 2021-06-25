using System.Collections.Generic;
using UnityEngine;

// Template specialisation pattern for these types:
// SUBPSACE <float,  float  > SPACE
//          <float,  Vector2>
//          <float,  Vector3>
//          <Vector2,Vector2>
//          <Vector2,Vector3>
//          <Vector3,Vector3>
// Projections/Substitutions between SubSpace <SS> and Space <S>
/// ControlSpaceToWorldSpace
///     * key = a single 'from' subspace axis,
///     * value = complete list of 'from' axes and 'to' axes in a single projection group.
///         The 'from' axes are encoded negatively: value = -1-axisId.
///         The 'to' axes are encoded positively: value = axisId.
///         AxisId is 0,1,2 for X,Y,Z
///         e.g. encoding a projection from 2D to 3D would be: (-2, -1, 0, 1, 2)
public interface IProjections<SS, S> {
    public void ProjectToSubspace(
        ControlFieldProfile<SS> controlFieldProfile,
        out KVariables<SS> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<S> srcVars
    );
    public void SubstituteToSubspace(
        ControlFieldProfile<SS> controlFieldProfile,
        out KVariables<SS> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<S> srcVars,
        AxisPlaneSpace alignment
    );
    public void ProjectFromSubspace(
        ControlFieldProfile<SS> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<SS> outputFromControlField,
        KVariables<S> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    );
    public void SubstituteFromSubspace(
        ControlFieldProfile<SS> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<SS> outputFromControlField,
        KVariables<S> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    );
}
public class ProjectionsFloatFloat : IProjections<float, float> {
    // 2D rotational only
    public void ProjectToSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        out KVariables<float> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> srcVars
    ) {
        // Projecting flag should never be set in this case
        throw new System.NotImplementedException();
    }
    public void SubstituteToSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        out KVariables<float> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> srcVars,
        AxisPlaneSpace alignment
    ) {
        varSet = new KVariables<float>(0f);
        varSet.SetEqual(srcVars);
        controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        controlSpaceToWorldSpace.Add(0, new List<int>{-1,0});
    }
    public void ProjectFromSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> outputFromControlField,
        KVariables<float> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        // The 'project' flag should never be set in this case
        throw new System.NotImplementedException();
    }
    public void SubstituteFromSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> outputFromControlField,
        KVariables<float> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        switch (controlFieldProfile.Control.ControlledVariableEnum) {
            case KVariableControllableEnum.None:
                // Nothing to do
                return;
            case KVariableControllableEnum.Variable: // StateSetter
                varsUpdate.Variable = outputFromControlField.Variable;
                ++(varsUsedAxis.m_variable[controlSpaceToWorldSpace[0][0]]);
                if (controlFieldProfile.Control.SmoothingEnabled) {
                    varsUpdate.Derivative = outputFromControlField.Derivative;
                    ++(varsUsedAxis.m_derivative[controlSpaceToWorldSpace[0][0]]);
                }
                break;
            case KVariableControllableEnum.Derivative: // StateSetter
                varsUpdate.Derivative = outputFromControlField.Derivative;
                ++(varsUsedAxis.m_variable[controlSpaceToWorldSpace[0][0]]);
                if (controlFieldProfile.Control.SmoothingEnabled) {
                    varsUpdate.SecondDerivative = outputFromControlField.SecondDerivative;
                    ++(varsUsedAxis.m_secondDerivative[controlSpaceToWorldSpace[0][0]]);
                }
                break;
            case KVariableControllableEnum.AppliedForce: // ForceType
                varsUpdate.AppliedForce += outputFromControlField.AppliedForce;
                break;
            case KVariableControllableEnum.ImpulseForce: // ForceType
                varsUpdate.ImpulseForce += outputFromControlField.ImpulseForce;
                break;
        }
    }
}
public class ProjectionsFloatVector2 : IProjections<float, Vector2> {
    // 2D spatial only
    private Dictionary<string, Vector2> m_cachedVectors = new Dictionary<string, Vector2>();
    public void ProjectToSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        out KVariables<float> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> srcVars
    ) {
        varSet = new KVariables<float>(0f);
        
        // Rotate the axis from X
        Vector2 unitVector = (Vector2)(controlFieldProfile.Direction*Vector3.right);
        varSet.Variable = Vector2.Dot(srcVars.Variable, unitVector);
        varSet.Derivative = Vector2.Dot(srcVars.Derivative, unitVector);
        varSet.SecondDerivative = Vector2.Dot(srcVars.SecondDerivative, unitVector);
        varSet.AppliedForce = Vector2.Dot(srcVars.AppliedForce, unitVector);
        controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0, 1});
    }
    public void SubstituteToSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        out KVariables<float> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> srcVars,
        AxisPlaneSpace alignment
    ) {
        switch (alignment) {
            case AxisPlaneSpace.X:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.x;
                varSet.Derivative = srcVars.Derivative.x;
                varSet.SecondDerivative = srcVars.SecondDerivative.x;
                varSet.AppliedForce = srcVars.AppliedForce.x;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0});
                break;
            case AxisPlaneSpace.Y:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.y;
                varSet.Derivative = srcVars.Derivative.y;
                varSet.SecondDerivative = srcVars.SecondDerivative.y;
                varSet.AppliedForce = srcVars.AppliedForce.y;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 1});
                break;
            case AxisPlaneSpace.Z:
            case AxisPlaneSpace.XYZ:
            case AxisPlaneSpace.YZ:
            case AxisPlaneSpace.XZ:
            case AxisPlaneSpace.XY:
            case AxisPlaneSpace.None:
                Debug.LogError("Invalid argument - alignment = " + alignment);
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
            default:
                Debug.LogError("Unhandled case");
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
        }
    }
    public void ProjectFromSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> outputFromControlField,
        KVariables<Vector2> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        Quaternion inverse = Quaternion.Inverse(controlFieldProfile.Direction);

        //Vector2 

        // Do the opposite of this:
        // varSet = new KVariables<float>(0f);
        //        
        // // Rotate the axis from X
        // Vector2 unitVector = (Vector2)(controlFieldProfile.Direction*Vector3.right);
        // varSet.Variable = Vector2.Dot(srcVars.Variable, unitVector);
        // varSet.Derivative = Vector2.Dot(srcVars.Derivative, unitVector);
        // varSet.SecondDerivative = Vector2.Dot(srcVars.SecondDerivative, unitVector);
        // varSet.AppliedForce = Vector2.Dot(srcVars.AppliedForce, unitVector);
        // controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        // controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0, 1});
    }
    public void SubstituteFromSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> outputFromControlField,
        KVariables<Vector2> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
}
public class ProjectionsFloatVector3 : IProjections<float, Vector3> {
    // 3D spatial or rotational
    public void ProjectToSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        out KVariables<float> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars
    ) {
        Vector3 unitDirection = controlFieldProfile.Direction*Vector3.right;
        varSet = new KVariables<float>(0f);
        varSet.Variable = Vector3.Dot(srcVars.Variable, unitDirection);
        varSet.Derivative = Vector3.Dot(srcVars.Derivative, unitDirection);
        varSet.SecondDerivative = Vector3.Dot(srcVars.SecondDerivative, unitDirection);
        varSet.AppliedForce = Vector3.Dot(srcVars.AppliedForce, unitDirection);
        controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0, 1, 2});
    }
    public void SubstituteToSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        out KVariables<float> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars,
        AxisPlaneSpace alignment
    ) {
        switch (alignment) {
            case AxisPlaneSpace.X:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.x;
                varSet.Derivative = srcVars.Derivative.x;
                varSet.SecondDerivative = srcVars.SecondDerivative.x;
                varSet.AppliedForce = srcVars.AppliedForce.x;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0});
                break;
            case AxisPlaneSpace.Y:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.y;
                varSet.Derivative = srcVars.Derivative.y;
                varSet.SecondDerivative = srcVars.SecondDerivative.y;
                varSet.AppliedForce = srcVars.AppliedForce.y;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 1});
                break;
            case AxisPlaneSpace.Z:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.z;
                varSet.Derivative = srcVars.Derivative.z;
                varSet.SecondDerivative = srcVars.SecondDerivative.z;
                varSet.AppliedForce = srcVars.AppliedForce.z;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 2});
                break;
            case AxisPlaneSpace.XYZ:
            case AxisPlaneSpace.YZ:
            case AxisPlaneSpace.XZ:
            case AxisPlaneSpace.XY:
            case AxisPlaneSpace.None:
                Debug.LogError("Invalid argument - alignment = " + alignment);
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
            default:
                Debug.LogError("Unhandled case");
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
        }
    }
    public void ProjectFromSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> outputFromControlField,
        KVariables<Vector3> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
    public void SubstituteFromSubspace(
        ControlFieldProfile<float> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<float> outputFromControlField,
        KVariables<Vector3> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
}
public class ProjectionsVector2Vector2 : IProjections<Vector2, Vector2> {
    // 2D spatial only
    public void ProjectToSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        out KVariables<Vector2> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> srcVars
    ) {
        varSet = new KVariables<Vector2>(Vector2.zero);
        
        // Rotate the axes
        Vector2 unitVectorX = (Vector2)(controlFieldProfile.Direction*Vector3.right);
        Vector2 unitVectorY = (Vector2)(controlFieldProfile.Direction*Vector3.up);
        varSet.Variable = new Vector2(Vector2.Dot(srcVars.Variable, unitVectorX), Vector2.Dot(srcVars.Variable, unitVectorY));
        varSet.Derivative = new Vector2(Vector2.Dot(srcVars.Derivative, unitVectorX), Vector2.Dot(srcVars.Derivative, unitVectorY));
        varSet.SecondDerivative = new Vector2(Vector2.Dot(srcVars.SecondDerivative, unitVectorX), Vector2.Dot(srcVars.SecondDerivative, unitVectorY));
        varSet.AppliedForce = new Vector2(Vector2.Dot(srcVars.AppliedForce, unitVectorX), Vector2.Dot(srcVars.AppliedForce, unitVectorY));
        controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        controlSpaceToWorldSpace.Add(0, new List<int>{-2, -1, 0, 1});
        controlSpaceToWorldSpace.Add(1, new List<int>{-2, -1, 0, 1});
    }
    public void SubstituteToSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        out KVariables<Vector2> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> srcVars,
        AxisPlaneSpace alignment
    ) {
        switch (alignment) {
            case AxisPlaneSpace.XY:
                varSet = new KVariables<Vector2>(Vector2.zero);
                varSet.Variable = srcVars.Variable;
                varSet.Derivative = srcVars.Derivative;
                varSet.SecondDerivative = srcVars.SecondDerivative;
                varSet.AppliedForce = srcVars.AppliedForce;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(1, new List<int>{-2, 1});
                break;
            case AxisPlaneSpace.YZ:
            case AxisPlaneSpace.XZ:
            case AxisPlaneSpace.X:
            case AxisPlaneSpace.Y:
            case AxisPlaneSpace.Z:
            case AxisPlaneSpace.XYZ:
            case AxisPlaneSpace.None:
                Debug.LogError("Invalid argument - alignment = " + alignment);
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
            default:
                Debug.LogError("Unhandled case");
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
        }
    }
    public void ProjectFromSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> outputFromControlField,
        KVariables<Vector2> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
    public void SubstituteFromSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> outputFromControlField,
        KVariables<Vector2> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
}
public class ProjectionsVector2Vector3 : IProjections<Vector2, Vector3> {
    // 3D spatial or rotational
    public void ProjectToSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        out KVariables<Vector2> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars
    ) {
        varSet = new KVariables<Vector2>(Vector2.zero);
        Vector3 unitX = controlFieldProfile.Direction*Vector3.right;
        Vector3 unitY = controlFieldProfile.Direction*Vector3.up;
        varSet.Variable = new Vector2(Vector3.Dot(srcVars.Variable, unitX), Vector3.Dot(srcVars.Variable, unitY));
        varSet.Derivative = new Vector2(Vector3.Dot(srcVars.Derivative, unitX), Vector3.Dot(srcVars.Derivative, unitY));
        varSet.SecondDerivative = new Vector2(Vector3.Dot(srcVars.SecondDerivative, unitX), Vector3.Dot(srcVars.SecondDerivative, unitY));
        varSet.AppliedForce = new Vector2(Vector3.Dot(srcVars.AppliedForce, unitX), Vector3.Dot(srcVars.AppliedForce, unitY));
        controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        controlSpaceToWorldSpace.Add(0, new List<int>{-2, -1, 0, 1, 2});
        controlSpaceToWorldSpace.Add(1, new List<int>{-2, -1, 0, 1, 2});
    }
    public void SubstituteToSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        out KVariables<Vector2> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars,
        AxisPlaneSpace alignment
    ) {
        switch (alignment) {
            case AxisPlaneSpace.XY:
                varSet = new KVariables<Vector2>(Vector2.zero);
                varSet.Variable = (Vector2)srcVars.Variable;
                varSet.Derivative = (Vector2)srcVars.Derivative;
                varSet.SecondDerivative = (Vector2)srcVars.SecondDerivative;
                varSet.AppliedForce = (Vector2)srcVars.AppliedForce;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(1, new List<int>{-2, 1});
                break;
            case AxisPlaneSpace.YZ: // X-->Y, Y-->Z
                varSet = new KVariables<Vector2>(Vector2.zero);
                varSet.Variable = new Vector2(srcVars.Variable.y, srcVars.Variable.z);
                varSet.Derivative = new Vector2(srcVars.Derivative.y, srcVars.Derivative.z);
                varSet.SecondDerivative = new Vector2(srcVars.SecondDerivative.y, srcVars.SecondDerivative.z);
                varSet.AppliedForce = new Vector2(srcVars.AppliedForce.y, srcVars.AppliedForce.z);
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 1});
                controlSpaceToWorldSpace.Add(1, new List<int>{-2, 2});
                break;
            case AxisPlaneSpace.XZ: // X-->X, Y-->Z
                varSet = new KVariables<Vector2>(Vector2.zero);
                varSet.Variable = new Vector2(srcVars.Variable.x, srcVars.Variable.z);
                varSet.Derivative = new Vector2(srcVars.Derivative.x, srcVars.Derivative.z);
                varSet.SecondDerivative = new Vector2(srcVars.SecondDerivative.x, srcVars.SecondDerivative.z);
                varSet.AppliedForce = new Vector2(srcVars.AppliedForce.x, srcVars.AppliedForce.z);
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(1, new List<int>{-2, 2});
                break;
            case AxisPlaneSpace.X:
            case AxisPlaneSpace.Y:
            case AxisPlaneSpace.Z:
            case AxisPlaneSpace.XYZ:
            case AxisPlaneSpace.None:
                Debug.LogError("Invalid argument - alignment = " + alignment);
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
            default:
                Debug.LogError("Unhandled case");
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
        }
    }
    public void ProjectFromSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> outputFromControlField,
        KVariables<Vector3> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
    public void SubstituteFromSubspace(
        ControlFieldProfile<Vector2> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> outputFromControlField,
        KVariables<Vector3> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
}
public class ProjectionsVector3Vector3 : IProjections<Vector3, Vector3> {
    // 3D spatial or rotational
    public void ProjectToSubspace(
        ControlFieldProfile<Vector3> controlFieldProfile,
        out KVariables<Vector3> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars
    ) {
        varSet = new KVariables<Vector3>(Vector3.zero);
        
        // Rotate the axes
        Vector3 unitVectorX = controlFieldProfile.Direction*Vector3.right;
        Vector3 unitVectorY = controlFieldProfile.Direction*Vector3.up;
        Vector3 unitVectorZ = controlFieldProfile.Direction*Vector3.forward;
        varSet.Variable = new Vector3(
            Vector3.Dot(srcVars.Variable, unitVectorX),
            Vector3.Dot(srcVars.Variable, unitVectorY),
            Vector3.Dot(srcVars.Variable, unitVectorZ)
        );
        varSet.Derivative = new Vector3(
            Vector3.Dot(srcVars.Derivative, unitVectorX),
            Vector3.Dot(srcVars.Derivative, unitVectorY),
            Vector3.Dot(srcVars.Derivative, unitVectorZ)
        );
        varSet.SecondDerivative = new Vector3(
            Vector3.Dot(srcVars.SecondDerivative, unitVectorX),
            Vector3.Dot(srcVars.SecondDerivative, unitVectorY),
            Vector3.Dot(srcVars.SecondDerivative, unitVectorZ)
        );
        varSet.AppliedForce = new Vector3(
            Vector3.Dot(srcVars.AppliedForce, unitVectorX),
            Vector3.Dot(srcVars.AppliedForce, unitVectorY),
            Vector3.Dot(srcVars.AppliedForce, unitVectorZ)
        );
        controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
        controlSpaceToWorldSpace.Add(0, new List<int>{-3, -2, -1, 0, 1});
        controlSpaceToWorldSpace.Add(1, new List<int>{-3, -2, -1, 0, 1});
        controlSpaceToWorldSpace.Add(2, new List<int>{-3, -2, -1, 0, 1});
    }
    public void SubstituteToSubspace(
        ControlFieldProfile<Vector3> controlFieldProfile,
        out KVariables<Vector3> varSet,
        out Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars,
        AxisPlaneSpace alignment
    ) {
        switch (alignment) {
            case AxisPlaneSpace.XYZ:
                varSet = new KVariables<Vector3>(Vector3.zero);
                varSet.Variable = srcVars.Variable;
                varSet.Derivative = srcVars.Derivative;
                varSet.SecondDerivative = srcVars.SecondDerivative;
                varSet.AppliedForce = srcVars.AppliedForce;
                controlSpaceToWorldSpace = new Dictionary<int, List<int>>();
                controlSpaceToWorldSpace.Add(0, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(1, new List<int>{-2, 1});
                controlSpaceToWorldSpace.Add(2, new List<int>{-3, 2});
                break;
            case AxisPlaneSpace.X:
            case AxisPlaneSpace.Y:
            case AxisPlaneSpace.Z:
            case AxisPlaneSpace.XY:
            case AxisPlaneSpace.YZ:
            case AxisPlaneSpace.XZ:
            case AxisPlaneSpace.None:
                Debug.LogError("Invalid argument - alignment = " + alignment);
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
            default:
                Debug.LogError("Unhandled case");
                varSet = null;
                controlSpaceToWorldSpace = null;
                break;
        }
    }
    public void ProjectFromSubspace(
        ControlFieldProfile<Vector3> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> outputFromControlField,
        KVariables<Vector3> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
    public void SubstituteFromSubspace(
        ControlFieldProfile<Vector3> controlFieldProfile,
        Dictionary<int, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> outputFromControlField,
        KVariables<Vector3> varsUpdate,
        KVariables<Vector3Int> varsUsedAxis
    ) {
        throw new System.NotImplementedException();
    }
}
