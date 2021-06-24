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
        out KVariables<SS> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<S> srcVars,
        Quaternion direction
    );
    public void SubstituteToSubspace(
        out KVariables<SS> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<S> srcVars,
        AxisPlaneSpace alignment
    );
    // public void ProjectFromSubspace(
    //     ControlFieldProfile<SS, S> controlFieldProfile,
    //     Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
    //     KVariables<SS> outputFromControlField,
    //     KVariables<S> varsUpdate,
    //     KVariables<Vector3Int> varsUsedAxis
    // );
    // public void SubstituteFromSubspace(
    //     ControlFieldProfile<SS, S> controlFieldProfile,
    //     Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
    //     KVariables<SS> outputFromControlField,
    //     KVariables<S> varsUpdate,
    //     KVariables<Vector3Int> varsUsedAxis
    // );
}
public class ProjectionsFloatFloat : IProjections<float, float> {
    // 2D rotational only
    public void ProjectToSubspace(
        out KVariables<float> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<float> srcVars,
        Quaternion direction
    ) {
        // Projecting flag should never be set in this case
        throw new System.NotImplementedException();
    }
    public void SubstituteToSubspace(
        out KVariables<float> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<float> srcVars,
        AxisPlaneSpace alignment
    ) {
        varSet = new KVariables<float>(0f);
        varSet.SetEqual(srcVars);
        controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
        controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1,0});
    }
}
public class ProjectionsFloatVector2 : IProjections<float, Vector2> {
    // 2D spatial only
    public void ProjectToSubspace(
        out KVariables<float> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> srcVars,
        Quaternion direction
    ) {
        varSet = new KVariables<float>(0f);
        
        // Rotate the axis from X
        Vector2 unitVector = (Vector2)(direction*Vector3.right);
        varSet.Variable = Vector2.Dot(srcVars.Variable, unitVector);
        varSet.Derivative = Vector2.Dot(srcVars.Derivative, unitVector);
        varSet.SecondDerivative = Vector2.Dot(srcVars.SecondDerivative, unitVector);
        varSet.AppliedForce = Vector2.Dot(srcVars.AppliedForce, unitVector);
        controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
        controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0, 1});
    }
    public void SubstituteToSubspace(
        out KVariables<float> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
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
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0});
                break;
            case AxisPlaneSpace.Y:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.y;
                varSet.Derivative = srcVars.Derivative.y;
                varSet.SecondDerivative = srcVars.SecondDerivative.y;
                varSet.AppliedForce = srcVars.AppliedForce.y;
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 1});
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
}
public class ProjectionsFloatVector3 : IProjections<float, Vector3> {
    // 3D spatial or rotational
    public void ProjectToSubspace(
        out KVariables<float> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars,
        Quaternion direction
    ) {
        Vector3 unitDirection = direction*Vector3.right;
        varSet = new KVariables<float>(0f);
        varSet.Variable = Vector3.Dot(srcVars.Variable, unitDirection);
        varSet.Derivative = Vector3.Dot(srcVars.Derivative, unitDirection);
        varSet.SecondDerivative = Vector3.Dot(srcVars.SecondDerivative, unitDirection);
        varSet.AppliedForce = Vector3.Dot(srcVars.AppliedForce, unitDirection);
        controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
        controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0, 1, 2});
    }
    public void SubstituteToSubspace(
        out KVariables<float> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
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
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0});
                break;
            case AxisPlaneSpace.Y:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.y;
                varSet.Derivative = srcVars.Derivative.y;
                varSet.SecondDerivative = srcVars.SecondDerivative.y;
                varSet.AppliedForce = srcVars.AppliedForce.y;
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 1});
                break;
            case AxisPlaneSpace.Z:
                varSet = new KVariables<float>(0f);
                varSet.Variable = srcVars.Variable.z;
                varSet.Derivative = srcVars.Derivative.z;
                varSet.SecondDerivative = srcVars.SecondDerivative.z;
                varSet.AppliedForce = srcVars.AppliedForce.z;
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 2});
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
}
public class ProjectionsVector2Vector2 : IProjections<Vector2, Vector2> {
    // 2D spatial only
    public void ProjectToSubspace(
        out KVariables<Vector2> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector2> srcVars,
        Quaternion direction
    ) {
        varSet = new KVariables<Vector2>(Vector2.zero);
        
        // Rotate the axis from X
        Vector2 unitVectorX = (Vector2)(direction*Vector3.right);
        Vector2 unitVectorY = (Vector2)(direction*Vector3.up);
        varSet.Variable = new Vector2(Vector2.Dot(srcVars.Variable, unitVectorX), Vector2.Dot(srcVars.Variable, unitVectorY));
        varSet.Derivative = new Vector2(Vector2.Dot(srcVars.Derivative, unitVectorX), Vector2.Dot(srcVars.Derivative, unitVectorY));
        varSet.SecondDerivative = new Vector2(Vector2.Dot(srcVars.SecondDerivative, unitVectorX), Vector2.Dot(srcVars.SecondDerivative, unitVectorY));
        varSet.AppliedForce = new Vector2(Vector2.Dot(srcVars.AppliedForce, unitVectorX), Vector2.Dot(srcVars.AppliedForce, unitVectorY));
        controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
        controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-2, -1, 0, 1});
        controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, -1, 0, 1});
    }
    public void SubstituteToSubspace(
        out KVariables<Vector2> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
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
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, 1});
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
}
public class ProjectionsVector2Vector3 : IProjections<Vector2, Vector3> {
    // 3D spatial or rotational
    public void ProjectToSubspace(
        out KVariables<Vector2> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars,
        Quaternion Direction
    ) {
        varSet = new KVariables<Vector2>(Vector2.zero);
        Vector3 unitX = Direction*Vector3.right;
        Vector3 unitY = Direction*Vector3.up;
        varSet.Variable = new Vector2(Vector3.Dot(srcVars.Variable, unitX), Vector3.Dot(srcVars.Variable, unitY));
        varSet.Derivative = new Vector2(Vector3.Dot(srcVars.Derivative, unitX), Vector3.Dot(srcVars.Derivative, unitY));
        varSet.SecondDerivative = new Vector2(Vector3.Dot(srcVars.SecondDerivative, unitX), Vector3.Dot(srcVars.SecondDerivative, unitY));
        varSet.AppliedForce = new Vector2(Vector3.Dot(srcVars.AppliedForce, unitX), Vector3.Dot(srcVars.AppliedForce, unitY));
        controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
        controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-2, -1, 0, 1, 2});
        controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, -1, 0, 1, 2});
    }
    public void SubstituteToSubspace(
        out KVariables<Vector2> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
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
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, 1});
                break;
            case AxisPlaneSpace.YZ: // X-->Y, Y-->Z
                varSet = new KVariables<Vector2>(Vector2.zero);
                varSet.Variable = new Vector2(srcVars.Variable.y, srcVars.Variable.z);
                varSet.Derivative = new Vector2(srcVars.Derivative.y, srcVars.Derivative.z);
                varSet.SecondDerivative = new Vector2(srcVars.SecondDerivative.y, srcVars.SecondDerivative.z);
                varSet.AppliedForce = new Vector2(srcVars.AppliedForce.y, srcVars.AppliedForce.z);
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 1});
                controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, 2});
                break;
            case AxisPlaneSpace.XZ: // X-->X, Y-->Z
                varSet = new KVariables<Vector2>(Vector2.zero);
                varSet.Variable = new Vector2(srcVars.Variable.x, srcVars.Variable.z);
                varSet.Derivative = new Vector2(srcVars.Derivative.x, srcVars.Derivative.z);
                varSet.SecondDerivative = new Vector2(srcVars.SecondDerivative.x, srcVars.SecondDerivative.z);
                varSet.AppliedForce = new Vector2(srcVars.AppliedForce.x, srcVars.AppliedForce.z);
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, 2});
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
}
public class ProjectionsVector3Vector3 : IProjections<Vector3, Vector3> {
    // 3D spatial or rotational
    public void ProjectToSubspace(
        out KVariables<Vector3> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
        KVariables<Vector3> srcVars,
        Quaternion Direction
    ) {
        // TODO - this is an encounterable case
        // Projecting flag should never be set when space and subspace have the same number of dimensions
        throw new System.NotImplementedException();
    }
    public void SubstituteToSubspace(
        out KVariables<Vector3> varSet,
        out Dictionary<List<int>, List<int>> controlSpaceToWorldSpace,
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
                controlSpaceToWorldSpace = new Dictionary<List<int>, List<int>>();
                controlSpaceToWorldSpace.Add(new List<int>{0}, new List<int>{-1, 0});
                controlSpaceToWorldSpace.Add(new List<int>{1}, new List<int>{-2, 1});
                controlSpaceToWorldSpace.Add(new List<int>{2}, new List<int>{-3, 2});
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
}
