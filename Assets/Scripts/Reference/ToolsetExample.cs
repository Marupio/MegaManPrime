using System;
using System.Collections.Generic;
using UnityEngine;



public interface IDblOps<T, S> {
    public T SmoothDamp(T variable, ref T refVar, out T outVar, float fixedType);
    public void GenericFn(S gen, T spec);
}

public class GenericDblOps<T, S> : IDblOps<T, S> {
    public static readonly IDblOps<T, S> P = GenericDblOps<T,S>.P as IDblOps<T, S> ?? new GenericDblOps<T, S>();
    public T SmoothDamp(T variable, ref T refVar, out T outVar, float fixedType) {
        // throw new System.NotImplementedException();
        outVar = default(T);
        Debug.Log("SmoothDamp not implemented");
        return outVar;
    }
    public void GenericFn(S gen, T spec) {
        //throw new System.NotImplementedException();
        Debug.Log("GenericFn not implemented");
    }
}

public class GenericDblOps :
    IDblOps<float, float>, IDblOps<float, Vector2>, IDblOps<float, Vector3>,
    IDblOps<Vector2, float>, IDblOps<Vector2, Vector2>, IDblOps<Vector2, Vector3> {
    public static GenericDblOps P = new GenericDblOps(); 
    public float SmoothDamp(float variable, ref float refVar, out float outVar, float fixedType) {
        outVar = 0;
        Debug.Log("SmoothDamp <float>");
        return 0f;
    }
    public void GenericFn(float gen, float spec) {
        Debug.Log("GenericFn <float>, <float>, gen = " + gen + ", spec = " + spec);
    }
    public void GenericFn(Vector2 gen, float spec) {
        Debug.Log("GenericFn <Vector2>, <float>, gen = " + gen + ", spec = " + spec);
    }
    public void GenericFn(Vector3 gen, float spec) {
        Debug.Log("GenericFn <Vector3>, <float>, gen = " + gen + ", spec = " + spec);
    }
    public Vector2 SmoothDamp(Vector2 variable, ref Vector2 refVar, out Vector2 outVar, float fixedType) {
        outVar = Vector2.zero;
        Debug.Log("SmoothDamp <Vector2>");
        return Vector2.zero;
    }
    public void GenericFn(float gen, Vector2 spec) {
        Debug.Log("GenericFn <float>, <Vector2>, gen = " + gen + ", spec = " + spec);
    }
    public void GenericFn(Vector2 gen, Vector2 spec) {
        Debug.Log("GenericFn <Vector2>, <Vector2>, gen = " + gen + ", spec = " + spec);
    }
    public void GenericFn(Vector3 gen, Vector2 spec) {
        Debug.Log("GenericFn <Vector3>, <Vector2>, gen = " + gen + ", spec = " + spec);
    }
}
public class HasDblOps<T> {
    GenericDblOps m_genOps;
    public GenericDblOps GenOps {get=>m_genOps; set=>m_genOps=value;}

    T tVal;
    public T TVal {get=>tVal; set=>tVal=value;}

    public void runTests() {
        float fl = 1;
        Vector2 v2 = Vector2.up;
        Vector3 v3 = Vector3.forward;

        IDblOps<T,float> gfl = GenericDblOps<T, float>.P;
        IDblOps<T,Vector2> gv2 = GenericDblOps<T, Vector2>.P;
        IDblOps<T,Vector3> gv3 = GenericDblOps<T, Vector3>.P;

        // Generic calls
        gfl.GenericFn(fl, tVal);
        gv2.GenericFn(v2, tVal);
        gv3.GenericFn(v3, tVal);

        // Spec'd calls
        if (GenOps == null) {
            GenOps = GenericDblOps.P;
        }
        GenOps.GenericFn(fl, fl);
        GenOps.GenericFn(fl, v2);
        GenOps.GenericFn(v2, fl);
        GenOps.GenericFn(v2, v2);
        GenOps.GenericFn(v3, v3);
    }
}

public class TestTraits : MonoBehaviour
{
    void Start()
    {
        // Simple single variable example stuff commented out
        // IMoveOps<float> opFloat = new MoveOpsFloat();
        // HasMoveOps<float> floaty = new HasMoveOps<float>();
        // floaty.MoveOps = opFloat;
        
        // IMoveOps<Vector2> opVecty = new MoveOpsVector2();
        // HasMoveOps<Vector2> vecty = new HasMoveOps<Vector2>();
        // vecty.MoveOps = opVecty;

        // float fixedType = 6.28f;

        // float fv = 0.4f;
        // float fref = 0.8f;

        // Vector2 vv = new Vector2(0.4f, 0.4f);
        // Vector2 vref = new Vector2(0.8f, 0.8f);

        // Debug.Log("floaty.Function...");
        // floaty.Function(fv, ref fref, fixedType);

        // Debug.Log("vecty.Function...");
        // vecty.Function(vv, ref vref, fixedType);

        HasDblOps<float> flDbl = new HasDblOps<float>();
        flDbl.TVal = 3.14f;

        HasDblOps<Vector2> v2Dbl = new HasDblOps<Vector2>();
        v2Dbl.TVal = Vector2.right;
        HasDblOps<Vector3> v3Dbl = new HasDblOps<Vector3>();
        v3Dbl.TVal = Vector3.back;

        Debug.Log("flDbl run tests");
        flDbl.runTests();
        Debug.Log("v2Dbl run tests");
        v2Dbl.runTests();
        Debug.Log("v3Dbl run tests");
        v3Dbl.runTests();
    }

    void Update()
    {
        
    }
}


// Simpler single variable example

public interface IMoveOps<T> {
    public T SmoothDamp(T variable, ref T refVar, out T outVar, float fixedType);
}
public class MoveOpsFloat : IMoveOps<float> {
    public float SmoothDamp(float variable, ref float refVar, out float outVar, float fixedType) {
        Debug.Log("SmoothDamp Float");
        outVar = 3.14f;
        return 0.5f;
    }    
}
public class MoveOpsVector2 : IMoveOps<Vector2> {
    public Vector2 SmoothDamp(Vector2 variable, ref Vector2 refVar, out Vector2 outVar, float fixedType) {
        Debug.Log("SmoothDamp Vector2");
        outVar = new Vector2(3.14f, 6.28f);
        return Vector2.left;
    }    
}
public class HasMoveOps<T> {
    IMoveOps<T> moveOps;
    public IMoveOps<T> MoveOps {get => moveOps; set => moveOps = value;}
    public void Function(T variable, ref T refVar, float fixedType) {
        T outVar;
        variable = moveOps.SmoothDamp(variable, ref refVar, out outVar, fixedType);
        Debug.Log("Output : " + outVar + ", refVar = " + refVar + ", outVar = " + outVar + ", fixedType = " + fixedType );
    }
}

