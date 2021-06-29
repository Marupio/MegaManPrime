using UnityEngine;

// Template specialisation pattern for SUBSPACE to SPACE conversions, back and forth:
// SUBPSACE <float,  float  > SPACE
//          <float,  Vector2>
//          <float,  Vector3>
//          <Vector2,Vector2>
//          <Vector2,Vector3>
//          <Vector3,Vector3>
// Projections/Substitutions between SubSpace <SS> and Space <S>
public interface IProjections<SS, S> {
    public void ExecuteControlField(
        ControlFieldProfile<SS> controlFieldProfile,
        KVariables<S> varsInit,
        KVariables<S> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    );
}
public class ProjectionsFloatFloat : IProjections<float, float> {
    // 2D rotational only
    public void ExecuteControlField(
        ControlFieldProfile<float> controlFieldProfile,
        KVariables<float> varsInit,
        KVariables<float> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    ) {
        // 2D rotation
        // No need to check for projection, not possible
        ControlField<float> control = controlFieldProfile.Control;
        switch (control.ControlledVariableEnum) {
            case KVariableEnum_Controllable.None: {
                return;
            }
            case KVariableEnum_Controllable.Variable: {
                varsUpdate.m_variable = varsInit.Variable;
                varsUpdate.m_derivative = varsInit.Derivative;
                control.Update(ref varsUpdate.m_variable, ref varsUpdate.m_derivative, deltaTime);
                dofsUsed.Add(2);
                return;
            }
            case KVariableEnum_Controllable.Derivative: {
                varsUpdate.m_derivative = varsInit.Derivative;
                varsUpdate.m_secondDerivative = varsInit.SecondDerivative;
                control.Update(ref varsUpdate.m_derivative, ref varsUpdate.m_secondDerivative, deltaTime);
                break;
            }
            case KVariableEnum_Controllable.SecondDerivative: {
                varsUpdate.m_secondDerivative = varsInit.SecondDerivative;
                float jerk_unused = 0;
                control.Update(ref varsUpdate.m_secondDerivative, ref jerk_unused, deltaTime);
                break;
            }
            case KVariableEnum_Controllable.AppliedForce: {
                varsUpdate.m_appliedForce = varsInit.AppliedForce;
                float forceRate_unused = 0;
                control.Update(ref varsUpdate.m_appliedForce, ref forceRate_unused, deltaTime);
                break;
            }
            case KVariableEnum_Controllable.ImpulseForce: {
                varsUpdate.m_impulseForce = varsInit.ImpulseForce;
                float forceRate_unused = 0;
                control.Update(ref varsUpdate.m_impulseForce, ref forceRate_unused, deltaTime);
                break;
            }
        }
        dofsUsed.Add(2);
        return;
    }
}
public class ProjectionsFloatVector2 : IProjections<float, Vector2> {
    public void ExecuteControlField(
        ControlFieldProfile<float> controlFieldProfile,
        KVariables<Vector2> varsInit,
        KVariables<Vector2> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    ) {
        // 2D spatial
        ControlField<float> control = controlFieldProfile.Control;
        if (controlFieldProfile.Projecting) {
            // Perform projections
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.Variable;
                    Vector3 rotatedDer = controlFieldProfile.Direction*varsInit.Derivative;
                    control.Update(ref rotatedVar.x, ref rotatedDer.x, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_variable = inverseQ*rotatedVar;
                    varsUpdate.m_derivative = inverseQ*rotatedDer;
                    break;
                }
                case KVariableEnum_Controllable.Derivative: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.Derivative;
                    Vector3 rotatedDer = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    control.Update(ref rotatedVar.x, ref rotatedDer.x, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_derivative = inverseQ*rotatedVar;
                    varsUpdate.m_secondDerivative = inverseQ*rotatedDer;
                    break;
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    float jerk_unused = 0f;
                    control.Update(ref rotatedVar.x, ref jerk_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_secondDerivative = inverseQ*rotatedVar;
                    break;
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.AppliedForce;
                    float forceRate_unused = 0f;
                    control.Update(ref rotatedVar.x, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_appliedForce = inverseQ*rotatedVar;
                    break;
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    Vector3 rotatedImpulseForce = controlFieldProfile.Direction*varsInit.ImpulseForce;
                    float forceRate_unused = 0f;
                    control.Update(ref rotatedImpulseForce.x, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_impulseForce = inverseQ*rotatedImpulseForce;
                    break;
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
            dofsUsed.Add(0, 1);
            return;
        } else {
            // Perform axis substitutions
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_variable.x = varsInit.Variable.x;
                            varsUpdate.m_derivative.x = varsInit.Derivative.x;
                            control.Update(ref varsUpdate.m_variable.x, ref varsUpdate.m_derivative.x, deltaTime);
                            dofsUsed.Add(0);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_variable.y = varsInit.Variable.y;
                            varsUpdate.m_derivative.y = varsInit.Derivative.y;
                            control.Update(ref varsUpdate.m_variable.y, ref varsUpdate.m_derivative.y, deltaTime);
                            dofsUsed.Add(1);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 2D space."
                            );
                            return;
                        }
                    }
                } // end switch (controlFieldProfile.Alignment)
                case KVariableEnum_Controllable.Derivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_derivative.x = varsInit.Derivative.x;
                            varsUpdate.m_secondDerivative.x = varsInit.SecondDerivative.x;
                            control.Update(ref varsUpdate.m_derivative.x, ref varsUpdate.m_secondDerivative.x, deltaTime);
                            dofsUsed.Add(0);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_derivative.y = varsInit.Derivative.y;
                            varsUpdate.m_secondDerivative.y = varsInit.SecondDerivative.y;
                            control.Update(ref varsUpdate.m_derivative.y, ref varsUpdate.m_secondDerivative.y, deltaTime);
                            dofsUsed.Add(1);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 2D space."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_secondDerivative.x = varsInit.SecondDerivative.x;
                            float jerk_unused = 0;
                            control.Update(ref varsUpdate.m_secondDerivative.x, ref jerk_unused, deltaTime);
                            dofsUsed.Add(0);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_secondDerivative.y = varsInit.SecondDerivative.y;
                            float jerk_unused = 0;
                            control.Update(ref varsUpdate.m_secondDerivative.y, ref jerk_unused, deltaTime);
                            dofsUsed.Add(1);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 2D space."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_appliedForce.x = varsInit.AppliedForce.x;
                            float forceRate_unused = 0;
                            control.Update(ref varsUpdate.m_appliedForce.x, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(0);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_appliedForce.y = varsInit.AppliedForce.y;
                            float forceRate_unused = 0;
                            control.Update(ref varsUpdate.m_appliedForce.y, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(1);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 2D space."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_impulseForce.x = varsInit.ImpulseForce.x;
                            float forceRate_unused = 0;
                            control.Update(ref varsUpdate.m_impulseForce.x, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(0);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_impulseForce.y = varsInit.ImpulseForce.y;
                            float forceRate_unused = 0;
                            control.Update(ref varsUpdate.m_impulseForce.y, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(1);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 2D space."
                            );
                            return;
                        }
                    }
                }
            }
        }
    }
}
public class ProjectionsFloatVector3 : IProjections<float, Vector3> {
    public void ExecuteControlField(
        ControlFieldProfile<float> controlFieldProfile,
        KVariables<Vector3> varsInit,
        KVariables<Vector3> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    ) {
        // 3 dimensionsal spatial or rotational
        ControlField<float> control = controlFieldProfile.Control;
        if (controlFieldProfile.Projecting) {
            // Perform projections
            switch (control.ControlledVariableEnum)  {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.Variable;
                    Vector3 rotatedDer = controlFieldProfile.Direction*varsInit.Derivative;
                    control.Update(ref rotatedVar.x, ref rotatedDer.x, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_variable = inverseQ*rotatedVar;
                    varsUpdate.m_derivative = inverseQ*rotatedDer;
                    break;
                }
                case KVariableEnum_Controllable.Derivative: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.Derivative;
                    Vector3 rotatedDer = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    control.Update(ref rotatedVar.x, ref rotatedDer.x, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_derivative = inverseQ*rotatedVar;
                    varsUpdate.m_secondDerivative = inverseQ*rotatedDer;
                    break;
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    float jerk_unused = 0f;
                    control.Update(ref rotatedVar.x, ref jerk_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_secondDerivative = inverseQ*rotatedVar;
                    break;
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.AppliedForce;
                    float forceRate_unused = 0f;
                    control.Update(ref rotatedVar.x, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_appliedForce = inverseQ*rotatedVar;
                    break;
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.ImpulseForce;
                    float forceRate_unused = 0f;
                    control.Update(ref rotatedVar.x, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_impulseForce = inverseQ*rotatedVar;
                    break;
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
            return;
        } else {
            // Perform axis substitutions
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_variable.x = varsInit.Variable.x;
                            varsUpdate.m_derivative.x = varsInit.Derivative.x;
                            control.Update(ref varsUpdate.m_variable.x, ref varsUpdate.m_derivative.x, deltaTime);
                            dofsUsed.Add(dofStart);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_variable.y = varsInit.Variable.y;
                            varsUpdate.m_derivative.y = varsInit.Derivative.y;
                            control.Update(ref varsUpdate.m_variable.y, ref varsUpdate.m_derivative.y, deltaTime);
                            dofsUsed.Add(dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.Z: {
                            varsUpdate.m_variable.z = varsInit.Variable.z;
                            varsUpdate.m_derivative.z = varsInit.Derivative.z;
                            control.Update(ref varsUpdate.m_variable.z, ref varsUpdate.m_derivative.z, deltaTime);
                            dofsUsed.Add(dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 3D space or rotation."
                            );
                            return;
                        }
                    } // end switch (controlFieldProfile.Alignment)
                }
                case KVariableEnum_Controllable.Derivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_derivative.x = varsInit.Derivative.x;
                            varsUpdate.m_secondDerivative.x = varsInit.SecondDerivative.x;
                            control.Update(ref varsUpdate.m_derivative.x, ref varsUpdate.m_secondDerivative.x, deltaTime);
                            dofsUsed.Add(dofStart);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_derivative.y = varsInit.Derivative.y;
                            varsUpdate.m_secondDerivative.y = varsInit.SecondDerivative.y;
                            control.Update(ref varsUpdate.m_derivative.y, ref varsUpdate.m_secondDerivative.y, deltaTime);
                            dofsUsed.Add(dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.Z: {
                            varsUpdate.m_derivative.z = varsInit.Derivative.z;
                            varsUpdate.m_secondDerivative.z = varsInit.SecondDerivative.z;
                            control.Update(ref varsUpdate.m_derivative.z, ref varsUpdate.m_secondDerivative.z, deltaTime);
                            dofsUsed.Add(dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 3D space or rotation."
                            );
                            return;
                        }
                    } // end switch (controlFieldProfile.Alignment)
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_secondDerivative.x = varsInit.SecondDerivative.x;
                            float jerk_unused = 0f;
                            control.Update(ref varsUpdate.m_secondDerivative.x, ref jerk_unused, deltaTime);
                            dofsUsed.Add(dofStart);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_secondDerivative.y = varsInit.SecondDerivative.y;
                            float jerk_unused = 0f;
                            control.Update(ref varsUpdate.m_secondDerivative.y, ref jerk_unused, deltaTime);
                            dofsUsed.Add(dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.Z: {
                            varsUpdate.m_secondDerivative.z = varsInit.SecondDerivative.z;
                            float jerk_unused = 0f;
                            control.Update(ref varsUpdate.m_secondDerivative.z, ref jerk_unused, deltaTime);
                            dofsUsed.Add(dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 3D space or rotation."
                            );
                            return;
                        }
                    } // end switch (controlFieldProfile.Alignment)
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_appliedForce.x = varsInit.AppliedForce.x;
                            float forceRate_unused = 0f;
                            control.Update(ref varsUpdate.m_appliedForce.x, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_appliedForce.y = varsInit.AppliedForce.y;
                            float forceRate_unused = 0f;
                            control.Update(ref varsUpdate.m_appliedForce.y, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.Z: {
                            varsUpdate.m_appliedForce.z = varsInit.AppliedForce.z;
                            float forceRate_unused = 0f;
                            control.Update(ref varsUpdate.m_appliedForce.z, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 3D space or rotation."
                            );
                            return;
                        }
                    } // end switch (controlFieldProfile.Alignment)
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.X: {
                            varsUpdate.m_impulseForce.x = varsInit.ImpulseForce.x;
                            float forceRate_unused = 0f;
                            control.Update(ref varsUpdate.m_impulseForce.x, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart);
                            return;
                        }
                        case AxisPlaneSpace.Y: {
                            varsUpdate.m_impulseForce.y = varsInit.ImpulseForce.y;
                            float forceRate_unused = 0f;
                            control.Update(ref varsUpdate.m_impulseForce.y, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.Z: {
                            varsUpdate.m_impulseForce.z = varsInit.ImpulseForce.z;
                            float forceRate_unused = 0f;
                            control.Update(ref varsUpdate.m_impulseForce.z, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 1D control in 3D space or rotation."
                            );
                            return;
                        }
                    } // end switch (controlFieldProfile.Alignment)
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            } // end switch (control.ControlledVariableEnum)
        }
    }
}
public class ProjectionsVector2Vector2 : IProjections<Vector2, Vector2> {
    public void ExecuteControlField(
        ControlFieldProfile<Vector2> controlFieldProfile,
        KVariables<Vector2> varsInit,
        KVariables<Vector2> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    ) {
        // 2D spatial
        ControlField<Vector2> control = controlFieldProfile.Control;
        if (controlFieldProfile.Projecting) {
            // Perform projections
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    Vector2 rotatedVariable = (Vector2)(controlFieldProfile.Direction*varsInit.Variable);
                    Vector2 rotatedDerivative = (Vector2)(controlFieldProfile.Direction*varsInit.Derivative);
                    control.Update(ref rotatedVariable, ref rotatedDerivative, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_variable = inverseQ*rotatedVariable;
                    varsUpdate.m_derivative = inverseQ*rotatedDerivative;
                    break;
                }
                case KVariableEnum_Controllable.Derivative: {
                    Vector2 rotatedDerivative = (Vector2)(controlFieldProfile.Direction*varsInit.Derivative);
                    Vector2 rotatedSecondDerivative = (Vector2)(controlFieldProfile.Direction*varsInit.SecondDerivative);
                    control.Update(ref rotatedDerivative, ref rotatedSecondDerivative, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_derivative = inverseQ*rotatedDerivative;
                    varsUpdate.m_secondDerivative = inverseQ*rotatedSecondDerivative;
                    break;
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    Vector2 rotatedSecondDerivative = (Vector2)(controlFieldProfile.Direction*varsInit.SecondDerivative);
                    Vector2 jerk_unused = Vector2.zero;
                    control.Update(ref rotatedSecondDerivative, ref jerk_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_secondDerivative = inverseQ*rotatedSecondDerivative;
                    break;
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    Vector2 rotatedAppliedForce = (Vector2)(controlFieldProfile.Direction*varsInit.AppliedForce);
                    Vector2 forceRate_unused = Vector2.zero;
                    control.Update(ref rotatedAppliedForce, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_appliedForce = inverseQ*rotatedAppliedForce;
                    break;
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    Vector2 rotatedImpulseForce = (Vector2)(controlFieldProfile.Direction*varsInit.ImpulseForce);
                    Vector2 forceRate_unused = Vector2.zero;
                    control.Update(ref rotatedImpulseForce, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_impulseForce = inverseQ*rotatedImpulseForce;
                    break;
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
            dofsUsed.Add(0, 1);
            return;
        } else {
            // Perform axis substitutions
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XY: { // X->X, Y->Y
                            varsUpdate.m_variable.x = varsInit.Variable.x;
                            varsUpdate.m_variable.y = varsInit.Variable.y;
                            Vector2 workingVec = (Vector2)(varsInit.Variable);
                            Vector2 workingDer = (Vector2)(varsInit.Derivative);
                            control.Update(ref workingVec, ref workingDer, deltaTime);
                            varsUpdate.m_variable.x = workingVec.x;
                            varsUpdate.m_variable.y = workingVec.y;
                            varsUpdate.m_derivative.x = workingDer.x;
                            varsUpdate.m_derivative.y = workingDer.y;
                            dofsUsed.Add(0, 1);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 2D control in 2D space."
                            );
                            return;
                        }
                    }
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            } // end switch (control.ControlledVariableEnum)
        }
    }
}
public class ProjectionsVector2Vector3 : IProjections<Vector2, Vector3> {
    public void ExecuteControlField(
        ControlFieldProfile<Vector2> controlFieldProfile,
        KVariables<Vector3> varsInit,
        KVariables<Vector3> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    ) {
        // 2D control plan in 3D space / rotation
        ControlField<Vector2> control = controlFieldProfile.Control;
        if (controlFieldProfile.Projecting) {
            // Perform projections
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    Vector3 rotatedVariable = controlFieldProfile.Direction*varsInit.Variable;
                    Vector3 rotatedDerivative = controlFieldProfile.Direction*varsInit.Derivative;
                    Vector2 workingVec = (Vector2)rotatedVariable;
                    Vector2 workingDer = (Vector2)rotatedDerivative;
                    control.Update(ref workingVec, ref workingDer, deltaTime);
                    rotatedVariable.x = workingVec.x;
                    rotatedVariable.y = workingVec.y;
                    rotatedDerivative.x = workingDer.x;
                    rotatedDerivative.y = workingDer.y;
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_variable = inverseQ*rotatedVariable;
                    varsUpdate.m_derivative = inverseQ*rotatedDerivative;
                    break;
                }
                case KVariableEnum_Controllable.Derivative: {
                    Vector3 rotatedVariable = controlFieldProfile.Direction*varsInit.Derivative;
                    Vector3 rotatedDerivative = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    Vector2 workingVec = (Vector2)rotatedVariable;
                    Vector2 workingDer = (Vector2)rotatedDerivative;
                    control.Update(ref workingVec, ref workingDer, deltaTime);
                    rotatedVariable.x = workingVec.x;
                    rotatedVariable.y = workingVec.y;
                    rotatedDerivative.x = workingDer.x;
                    rotatedDerivative.y = workingDer.y;
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_derivative = inverseQ*rotatedVariable;
                    varsUpdate.m_secondDerivative = inverseQ*rotatedDerivative;
                    break;
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    Vector3 rotatedVariable = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    Vector2 workingVec = (Vector2)rotatedVariable;
                    Vector2 workingDer = Vector2.zero;
                    control.Update(ref workingVec, ref workingDer, deltaTime);
                    rotatedVariable.x = workingVec.x;
                    rotatedVariable.y = workingVec.y;
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_secondDerivative = inverseQ*rotatedVariable;
                    break;
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    Vector3 rotatedVariable = controlFieldProfile.Direction*varsInit.AppliedForce;
                    Vector2 workingVec = (Vector2)rotatedVariable;
                    Vector2 workingDer = Vector2.zero;
                    control.Update(ref workingVec, ref workingDer, deltaTime);
                    rotatedVariable.x = workingVec.x;
                    rotatedVariable.y = workingVec.y;
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_appliedForce = inverseQ*rotatedVariable;
                    break;
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    Vector3 rotatedVariable = controlFieldProfile.Direction*varsInit.ImpulseForce;
                    Vector2 workingVec = (Vector2)rotatedVariable;
                    Vector2 workingDer = Vector2.zero;
                    control.Update(ref workingVec, ref workingDer, deltaTime);
                    rotatedVariable.x = workingVec.x;
                    rotatedVariable.y = workingVec.y;
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_impulseForce = inverseQ*rotatedVariable;
                    break;
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
            return;
        } else {
            // Perform axis substitutions
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XY: { // X->X, Y->Y
                            Vector2 workingVar = new Vector2(varsInit.Variable.x, varsInit.Variable.y);
                            Vector2 workingDer = new Vector2(varsInit.Derivative.x, varsInit.Derivative.y);
                            control.Update(ref workingVar, ref workingDer, deltaTime);
                            varsUpdate.m_variable.x = workingVar.x;
                            varsUpdate.m_variable.y = workingVar.y;
                            varsUpdate.m_derivative.x = workingDer.x;
                            varsUpdate.m_derivative.y = workingDer.y;
                            dofsUsed.Add(dofStart, dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.XZ: { // X->X, Y->Z
                            Vector2 workingVar = new Vector2(varsInit.Variable.x, varsInit.Variable.z);
                            Vector2 workingDer = new Vector2(varsInit.Derivative.x, varsInit.Derivative.z);
                            control.Update(ref workingVar, ref workingDer, deltaTime);
                            varsUpdate.m_variable.x = workingVar.x;
                            varsUpdate.m_variable.z = workingVar.y;
                            varsUpdate.m_derivative.x = workingDer.x;
                            varsUpdate.m_derivative.z = workingDer.y;
                            dofsUsed.Add(dofStart, dofStart+2);
                            return;
                        }
                        case AxisPlaneSpace.YZ: { // X->Y, Y->Z
                            Vector2 workingVar = new Vector2(varsInit.Variable.y, varsInit.Variable.z);
                            Vector2 workingDer = new Vector2(varsInit.Derivative.y, varsInit.Derivative.z);
                            control.Update(ref workingVar, ref workingDer, deltaTime);
                            varsUpdate.m_variable.y = workingVar.x;
                            varsUpdate.m_variable.z = workingVar.y;
                            varsUpdate.m_derivative.y = workingDer.x;
                            varsUpdate.m_derivative.z = workingDer.y;
                            dofsUsed.Add(dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 2D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.Derivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XY: { // X->X, Y->Y
                            Vector2 workingVar = new Vector2(varsInit.Derivative.x, varsInit.Derivative.y);
                            Vector2 workingDer = new Vector2(varsInit.SecondDerivative.x, varsInit.SecondDerivative.y);
                            control.Update(ref workingVar, ref workingDer, deltaTime);
                            varsUpdate.m_derivative.x = workingVar.x;
                            varsUpdate.m_derivative.y = workingVar.y;
                            varsUpdate.m_secondDerivative.x = workingDer.x;
                            varsUpdate.m_secondDerivative.y = workingDer.y;
                            dofsUsed.Add(dofStart, dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.XZ: { // X->X, Y->Z
                            Vector2 workingVar = new Vector2(varsInit.Derivative.x, varsInit.Derivative.z);
                            Vector2 workingDer = new Vector2(varsInit.SecondDerivative.x, varsInit.SecondDerivative.z);
                            control.Update(ref workingVar, ref workingDer, deltaTime);
                            varsUpdate.m_derivative.x = workingVar.x;
                            varsUpdate.m_derivative.z = workingVar.y;
                            varsUpdate.m_secondDerivative.x = workingDer.x;
                            varsUpdate.m_secondDerivative.z = workingDer.y;
                            dofsUsed.Add(dofStart, dofStart+2);
                            return;
                        }
                        case AxisPlaneSpace.YZ: { // X->Y, Y->Z
                            Vector2 workingVar = new Vector2(varsInit.Derivative.y, varsInit.Derivative.z);
                            Vector2 workingDer = new Vector2(varsInit.SecondDerivative.y, varsInit.SecondDerivative.z);
                            control.Update(ref workingVar, ref workingDer, deltaTime);
                            varsUpdate.m_derivative.y = workingVar.x;
                            varsUpdate.m_derivative.z = workingVar.y;
                            varsUpdate.m_secondDerivative.y = workingDer.x;
                            varsUpdate.m_secondDerivative.z = workingDer.y;
                            dofsUsed.Add(dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 2D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XY: { // X->X, Y->Y
                            Vector2 workingVar = new Vector2(varsInit.SecondDerivative.x, varsInit.SecondDerivative.y);
                            Vector2 jerk_unused = Vector2.zero;
                            control.Update(ref workingVar, ref jerk_unused, deltaTime);
                            varsUpdate.m_secondDerivative.x = workingVar.x;
                            varsUpdate.m_secondDerivative.y = workingVar.y;
                            dofsUsed.Add(dofStart, dofStart+1);
                            return;
                        }
                        case AxisPlaneSpace.XZ: { // X->X, Y->Z
                            Vector2 workingVar = new Vector2(varsInit.Derivative.x, varsInit.Derivative.z);
                            Vector2 jerk_unused = Vector2.zero;
                            control.Update(ref workingVar, ref jerk_unused, deltaTime);
                            varsUpdate.m_secondDerivative.x = workingVar.x;
                            varsUpdate.m_secondDerivative.z = workingVar.y;
                            dofsUsed.Add(dofStart, dofStart+2);
                            return;
                        }
                        case AxisPlaneSpace.YZ: { // X->Y, Y->Z
                            Vector2 workingVar = new Vector2(varsInit.Derivative.y, varsInit.Derivative.z);
                            Vector2 jerk_unused = Vector2.zero;
                            control.Update(ref workingVar, ref jerk_unused, deltaTime);
                            varsUpdate.m_secondDerivative.y = workingVar.x;
                            varsUpdate.m_secondDerivative.z = workingVar.y;
                            dofsUsed.Add(dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 2D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
        }
    }
}
public class ProjectionsVector3Vector3 : IProjections<Vector3, Vector3> {
    public void ExecuteControlField(
        ControlFieldProfile<Vector3> controlFieldProfile,
        KVariables<Vector3> varsInit,
        KVariables<Vector3> varsUpdate,
        DegreesOfFreedomChecker dofsUsed,
        int dofStart,
        float deltaTime
    ) {
        ControlField<Vector3> control = controlFieldProfile.Control;
        if (controlFieldProfile.Projecting) {
            // Perform projections
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.Variable;
                    Vector3 rotatedDer = controlFieldProfile.Direction*varsInit.Derivative;
                    control.Update(ref rotatedVar, ref rotatedDer, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_variable = inverseQ*rotatedVar;
                    varsUpdate.m_derivative = inverseQ*rotatedDer;
                    break;
                }
                case KVariableEnum_Controllable.Derivative: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.Derivative;
                    Vector3 rotatedDer = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    control.Update(ref rotatedVar, ref rotatedDer, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_derivative = inverseQ*rotatedVar;
                    varsUpdate.m_secondDerivative = inverseQ*rotatedDer;
                    break;
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.SecondDerivative;
                    Vector3 jerk_unused = Vector3.zero;
                    control.Update(ref rotatedVar, ref jerk_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_secondDerivative = inverseQ*rotatedVar;
                    break;
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.AppliedForce;
                    Vector3 forceRate_unused = Vector3.zero;
                    control.Update(ref rotatedVar, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_appliedForce = inverseQ*rotatedVar;
                    break;
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    Vector3 rotatedVar = controlFieldProfile.Direction*varsInit.ImpulseForce;
                    Vector3 forceRate_unused = Vector3.zero;
                    control.Update(ref rotatedVar, ref forceRate_unused, deltaTime);
                    Quaternion inverseQ = Quaternion.Inverse(controlFieldProfile.Direction);
                    varsUpdate.m_impulseForce = inverseQ*rotatedVar;
                    break;
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
        } else {
            // Perform axis substitutions
            switch (control.ControlledVariableEnum) {
                case KVariableEnum_Controllable.None: {
                    return;
                }
                case KVariableEnum_Controllable.Variable: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XYZ: {
                            varsUpdate.m_variable = varsInit.m_variable;
                            varsUpdate.m_derivative = varsInit.m_derivative;
                            control.Update(ref varsUpdate.m_variable, ref varsUpdate.m_derivative, deltaTime);
                            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 3D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.Derivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XYZ: {
                            varsUpdate.m_derivative = varsInit.m_derivative;
                            varsUpdate.m_secondDerivative = varsInit.m_secondDerivative;
                            control.Update(ref varsUpdate.m_derivative, ref varsUpdate.m_secondDerivative, deltaTime);
                            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 3D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.SecondDerivative: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XYZ: {
                            varsUpdate.m_secondDerivative = varsInit.m_secondDerivative;
                            Vector3 jerk_unused = Vector3.zero;
                            control.Update(ref varsUpdate.m_secondDerivative, ref jerk_unused, deltaTime);
                            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 3D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.AppliedForce: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XYZ: {
                            varsUpdate.m_appliedForce = varsInit.m_appliedForce;
                            Vector3 forceRate_unused = Vector3.zero;
                            control.Update(ref varsUpdate.m_appliedForce, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 3D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                case KVariableEnum_Controllable.ImpulseForce: {
                    switch (controlFieldProfile.Alignment) {
                        case AxisPlaneSpace.XYZ: {
                            varsUpdate.m_impulseForce = varsInit.m_impulseForce;
                            Vector3 forceRate_unused = Vector3.zero;
                            control.Update(ref varsUpdate.m_impulseForce, ref forceRate_unused, deltaTime);
                            dofsUsed.Add(dofStart, dofStart+1, dofStart+2);
                            return;
                        }
                        default: {
                            Debug.LogError(
                                "ControlFieldProfile: " + controlFieldProfile.Name + " has an invalid alignment: " + controlFieldProfile.Alignment +
                                " for 3D control in 3D space / rotation."
                            );
                            return;
                        }
                    }
                }
                default: {
                    Debug.LogError("Unhandled case");
                    return;
                }
            }
        }
    }
}
