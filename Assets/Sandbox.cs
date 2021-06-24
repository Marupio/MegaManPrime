using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClass {
    public int intValue;
    public string name;
    public SimpleClass(int i, string n) {
        intValue = i;
        name = n;
    }
}

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

    void Start() {
        List<SimpleClass> theList = new List<SimpleClass>();
        theList.Add(new SimpleClass(1, "first"));
        theList.Add(new SimpleClass(2, "second"));
        theList.Add(new SimpleClass(3, "third"));
        theList.Add(new SimpleClass(4, "fourth"));
        Dictionary<string, SimpleClass> index = new Dictionary<string, SimpleClass>();
        foreach(SimpleClass sc in theList) {
            index.Add(sc.name, sc);
        }

        string testStr = "Initial state, theList:\n";
        foreach(SimpleClass sc in theList) {
            testStr += sc.intValue + "," + sc.name + "\n";
        }
        testStr += "index:\n";
        foreach(KeyValuePair<string, SimpleClass> entry in index) {
            testStr += entry.Key + " : " + entry.Value.intValue + "," + entry.Value.name + "\n";
        }

        theList[0].intValue = 7;
        testStr += "\nAfter change 0's intValue to 7, theList:\n";
        foreach(SimpleClass sc in theList) {
            testStr += sc.intValue + "," + sc.name + "\n";
        }
        testStr += "index:\n";
        foreach(KeyValuePair<string, SimpleClass> entry in index) {
            testStr += entry.Key + " : " + entry.Value.intValue + "," + entry.Value.name + "\n";
        }

        theList[2].name = "newName!";
        testStr += "\nAfter change 0's intValue to 7, theList:\n";
        foreach(SimpleClass sc in theList) {
            testStr += sc.intValue + "," + sc.name + "\n";
        }
        testStr += "index:\n";
        foreach(KeyValuePair<string, SimpleClass> entry in index) {
            testStr += entry.Key + " : " + entry.Value.intValue + "," + entry.Value.name + "\n";
        }
        Debug.Log(testStr);
    }
// Initial state, theList:
// 1,first
// 2,second
// 3,third
// 4,fourth
// index:
// first : 1,first
// second : 2,second
// third : 3,third
// fourth : 4,fourth

// After change 0's intValue to 7, theList:
// 7,first
// 2,second
// 3,third
// 4,fourth
// index:
// first : 7,first
// second : 2,second
// third : 3,third
// fourth : 4,fourth

// After change 0's intValue to 7, theList:
// 7,first
// 2,second
// 3,newName!
// 4,fourth
// index:
// first : 7,first
// second : 2,second
// third : 3,newName!
// fourth : 4,fourth

// UnityEngine.Debug:Log (object)
// Sandbox:Start () (at Assets/Sandbox.cs:68)


    // Start is called before the first frame update
    void Update() {
        Quaternion q = Quaternion.Euler(inputAngles);
        rotatedVelocity = q*inputVelocity;
        float inputThetaRadians = inputThetaDegrees*Mathf.Deg2Rad;
        thetaUnitVector = new Vector3(Mathf.Cos(inputThetaRadians), Mathf.Sin(inputThetaRadians), 0);
        projectedMagnitude = Vector3.Dot(thetaUnitVector, inputVelocity);
        rotatedAxisDirection = q*inputAxisDirection;
        rotatedAxisDirectionDotInputVelocity = Vector3.Dot(rotatedAxisDirection, inputVelocity);
    }
}
