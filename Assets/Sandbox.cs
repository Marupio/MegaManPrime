using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    [Header("Inputs")]
    public Vector3 inputAngles;
    public Vector3 inputVelocity;
    public float inputThetaDegrees;
    public Vector3 inputAxisDirection;

    [Header("Outputs")]
    public Vector3 rotatedVelocity;
    public Vector3 thetaUnitVector;
    public float projectedMagnitude;
    public Vector3 rotatedAxisDirection;
    public float rotatedAxisDirectionDotInputVelocity;

    // Start is called before the first frame update
    void Update()
    {
        Quaternion q = Quaternion.Euler(inputAngles);
        rotatedVelocity = q*inputVelocity;
        float inputThetaRadians = inputThetaDegrees*Mathf.Deg2Rad;
        thetaUnitVector = new Vector3(Mathf.Cos(inputThetaRadians), Mathf.Sin(inputThetaRadians), 0);
        projectedMagnitude = Vector3.Dot(thetaUnitVector, inputVelocity);
        rotatedAxisDirection = q*inputAxisDirection;
        rotatedAxisDirectionDotInputVelocity = Vector3.Dot(rotatedAxisDirection, inputVelocity);
    }
}
