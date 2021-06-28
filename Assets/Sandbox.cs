using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sandbox : MonoBehaviour {

    [Header("Inputs")]
    public Vector3 inputVelocity;
    public Vector3 inputAngles;

    [Header("Outputs")]
    public float oneAxisRepresentation;
    // public Vector2 twoAxisRepresentation;
    public float rotatedXComponent;
    public float rotatedYComponent;
    public float rotatedZComponent;
    public Vector3 rotatedAll;
    public Vector3 unrotatedAll;
    public Vector3 withCrossProduct;
    public Vector3 reversified;
    // public Vector3 twoAxisUndo;

    public void Awake() {
#if DEBUG
        Debug.Log("Debug is set");
#else
        Debug.Log("Debug is not set");
#endif
    }

    // Start is called before the first frame update
    public void Update() {
        Quaternion qForward = Quaternion.Euler(inputAngles);
        Quaternion qReverse = Quaternion.Inverse(qForward);
        Vector3 xAxisUnitVector = qForward*Vector3.right;
        Vector3 yAxisUnitVector = qForward*Vector3.up;
        Vector3 zAxisUnitVector = qForward*Vector3.forward;
        rotatedXComponent = Vector3.Dot(xAxisUnitVector, inputVelocity);
        rotatedYComponent = Vector3.Dot(yAxisUnitVector, inputVelocity);
        rotatedZComponent = Vector3.Dot(zAxisUnitVector, inputVelocity);
        rotatedAll = qForward*inputVelocity;
        unrotatedAll = qReverse*rotatedAll;

        // oneAxisRepresentation = Vector3.Dot(xAxisUnitVector, inputVelocity);
        // Vector3 cross = Vector3.Cross(xAxisUnitVector, inputVelocity);
        // twoAxisRepresentation = new Vector2(Vector3.Dot(xAxisUnitVector, inputVelocity), Vector3.Dot(yAxisUnitVector, inputVelocity));
        // oneAxisUndo = oneAxisRepresentation*xAxisUnitVector;
        // withCrossProduct = oneAxisUndo + cross;
        // reversified = qReverse*withCrossProduct;
        // // twoAxisUndo = 

        // velocityMagnitude = inputVelocity.magnitude;
        // qForward = Quaternion.Euler(inputAngles);
        // projectedMagnitude = Vector3.Dot(qForward*inputAxisDirection, inputVelocity);
        // qReverse = Quaternion.Inverse(qForward);


        // reverseAngles = qReverse.eulerAngles;
        // Vector3 xAxis = qReverse*Vector3.right;
        // Vector3 yAxis = qReverse*Vector3.up;
        // Vector3 zAxis = qReverse*Vector3.forward;
        // Vector3 vec = projectedMagnitude*inputAxisDirection;
        // unprojectedVelocity = qReverse*vec;
        // outputMagnitude = unprojectedVelocity.magnitude;
        // // unprojectedVelocity = qReverse*inputAxisDirection*projectedMagnitude;
    }
}

// public class SimpleClass {
//     public int intValue;
//     public string name;
//     public SimpleClass(int i, string n) {
//         intValue = i;
//         name = n;
//     }
// }
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



// public class HasVector3 {
//     Vector3 m_vecA;
//     Vector3 m_vecB;
//     Vector3 m_vecC;
//     Vector3 m_vecD;
//     public Vector3 VecA { get=>m_vecA; set=>m_vecA=value;}
//     public Vector3 VecB { get=>m_vecB; set=>m_vecB=value;}
//     public Vector3 VecC { get=>m_vecC; set=>m_vecC=value;}
//     public Vector3 VecD { get=>m_vecD; set=>m_vecD=value;}
// }

// public class HasMethod {
//     public static void DoSomethingWithVector3(Vector3 vec) { vec = Vector3.right; }
//     public static void DoSomethingWithHasVector3(HasVector3 h, in HasVector3 inH) {
//         Vector3 va = h.VecA;
//         va += Vector3.back;
//         Vector3 vb = h.VecB;
//         vb += Vector3.back;
//         h.VecB = vb;
//         Vector3 vc = inH.VecC;
//         vc.y = 5;
//         inH.VecC = vc;
//     }
// }

    // public void Awake() {
    //     HasVector3 hv = new HasVector3();
    //     hv.VecA = Vector3.zero;
    //     hv.VecB = Vector3.zero;
    //     hv.VecC = Vector3.zero;
    //     HasMethod.DoSomethingWithVector3(hv.VecA);
    //     HasMethod.DoSomethingWithHasVector3(hv, in hv);
    //     Debug.Log("end A="+hv.VecA+" B="+hv.VecB+" C="+hv.VecC+" D="+hv.VecD);
    // }
// end A=(0.0, 0.0, 0.0) B=(0.0, 0.0, -1.0) C=(0.0, 0.0, 0.0) D=(0.0, 0.0, 0.0)
// UnityEngine.Debug:Log (object)
// Sandbox:Awake () (at Assets/Sandbox.cs:37)
