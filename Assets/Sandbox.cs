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
    public Vector3 inputAxisDirection;

    [Header("Outputs")]
    public float velocityMagnitude;
    public Quaternion qForward;
    public Quaternion qReverse;
    public Vector3 reverseAngles;
    public float projectedMagnitude;
    public Vector3 unprojectedVelocity;
    public float outputMagnitude;

    // Start is called before the first frame update
    void Update() {
        velocityMagnitude = inputVelocity.magnitude;
        qForward = Quaternion.Euler(inputAngles);
        projectedMagnitude = Vector3.Dot(qForward*inputAxisDirection, inputVelocity);
        qReverse = Quaternion.Inverse(qForward);
        reverseAngles = qReverse.eulerAngles;
        Vector3 xAxis = qReverse*Vector3.right;
        Vector3 yAxis = qReverse*Vector3.up;
        Vector3 zAxis = qReverse*Vector3.forward;
        Vector3 vec = projectedMagnitude*inputAxisDirection;
        unprojectedVelocity = qReverse*vec;
        outputMagnitude = unprojectedVelocity.magnitude;
        // unprojectedVelocity = qReverse*inputAxisDirection*projectedMagnitude;
    }

    // Figuring out references / value types in C#
    // void Start() {
    //     List<SimpleClass> theList = new List<SimpleClass>();
    //     theList.Add(new SimpleClass(1, "first"));
    //     theList.Add(new SimpleClass(2, "second"));
    //     theList.Add(new SimpleClass(3, "third"));
    //     theList.Add(new SimpleClass(4, "fourth"));
    //     Dictionary<string, SimpleClass> index = new Dictionary<string, SimpleClass>();
    //     foreach(SimpleClass sc in theList) {
    //         index.Add(sc.name, sc);
    //     }

    //     string testStr = "Initial state, theList:\n";
    //     foreach(SimpleClass sc in theList) {
    //         testStr += sc.intValue + "," + sc.name + "\n";
    //     }
    //     testStr += "index:\n";
    //     foreach(KeyValuePair<string, SimpleClass> entry in index) {
    //         testStr += entry.Key + " : " + entry.Value.intValue + "," + entry.Value.name + "\n";
    //     }

    //     theList[0].intValue = 7;
    //     testStr += "\nAfter change 0's intValue to 7, theList:\n";
    //     foreach(SimpleClass sc in theList) {
    //         testStr += sc.intValue + "," + sc.name + "\n";
    //     }
    //     testStr += "index:\n";
    //     foreach(KeyValuePair<string, SimpleClass> entry in index) {
    //         testStr += entry.Key + " : " + entry.Value.intValue + "," + entry.Value.name + "\n";
    //     }

    //     theList[2].name = "newName!";
    //     testStr += "\nAfter change 0's intValue to 7, theList:\n";
    //     foreach(SimpleClass sc in theList) {
    //         testStr += sc.intValue + "," + sc.name + "\n";
    //     }
    //     testStr += "index:\n";
    //     foreach(KeyValuePair<string, SimpleClass> entry in index) {
    //         testStr += entry.Key + " : " + entry.Value.intValue + "," + entry.Value.name + "\n";
    //     }
    //     Debug.Log(testStr);
    // }
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
}
