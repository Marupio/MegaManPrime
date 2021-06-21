using System;
using System.Collections.Generic;
using UnityEngine;

public class THolder<T> {
    T data;
    public T Data {get=>data; set=>data = value;}
    // public void DoSomething<S>() {
    //     S otherType;
    //     GenericOperation(out otherType, data);
    //     // Debug.Log("Return we have " + otherType);
    // }
    public void GenericOperation<S>(out S otherType, T templateType) {
        //throw new System.NotImplementedException();
        otherType = default(S);
        Second(templateType);
        Type t = typeof(T);
        Type s = typeof(S);
        Debug.Log("NOT IMPLEMENTED:" + s + "," + t);
    }
    public void GenericOperation(out int otherType, T templateType) {
        otherType = 5;
        Second(templateType);
        Debug.Log("GO int, T");
    }
    public void GenericOperation(out Vector2 otherType, T templateType) {
        otherType = Vector2.left;
        Second(templateType);
        Debug.Log("GO Vector2, T");
    }
    public void GenericOperation(out Vector2 otherType, float templateType) {
        otherType = Vector2.down;
        Second(templateType);
        Debug.Log("GO Vector2, float");
    }
    public void Second<S>(S sIn) {
        Type t = typeof(S);
        Debug.Log("SecondNotImplemented " + t);
    }
    public void Second(int sIn) {
        Debug.Log("Second int");
    }
    public void Second(float sIn) {
        Debug.Log("Second float");
    }
    public void Second(Vector3 sIn) {
        Debug.Log("Second Vector3");
    }
}

public class TestTraits : MonoBehaviour
{
    void Start()
    {
        int intType = -7;
        float floatType = 5.6f;
        Vector2 v2Type = Vector2.down;
        Vector3 v3Type = Vector3.zero;

        THolder<Vector3> thVec3 = new THolder<Vector3>();
        thVec3.Data = Vector3.forward;
        // thVec3.GenericOperation(out intType, v3Type);

        Debug.Log("GenericOperation");
        thVec3.GenericOperation(out v2Type, v3Type);
        thVec3.GenericOperation(out floatType, v3Type);
        // thVec3.GenericOperation(out v2Type, floatType);

        Debug.Log("Now manually going Second...");
        thVec3.Second(floatType);
        thVec3.Second(v2Type);
        thVec3.Second(v3Type);

        // THolder<float> thFloat = new THolder<float>();
        // thFloat.Data = -4.8f;
        // thFloat.GenericOperation(); // I expect GO int, T
        // thFloat.GenericOperation(); // I expect GO Vector2, float




        // float tf;
        // Vector2 tv2;
        // Vector3 tv3;
        // Traits traits = new Traits();
        // tv2 = traits.Zero(default(Vector2));
        // tv3 = Vector3.up;
        // tv3 = traits.Zero(default(Vector3));
        // tf = 3;
        // tf = traits.Zero(default(float));

        // THolder<Vector2> th = new THolder<Vector2>();
        // th.Data = Vector2.up;
        // Debug.Log("tf = " + tf + ", tv2 = " + tv2 + ", tv3 = " + tv3 + ", th.Data = " + th.Data);


        // Traits<Vector2> traitsVector2 = new Traits<Vector2>();
        // Traits<Vector3> traitsVector3 = new Traits<Vector3>();
    }

    void Update()
    {
        
    }
}
