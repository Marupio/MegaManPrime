using System;
using System.Collections.Generic;
using UnityEngine;

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

public class TestTraits : MonoBehaviour
{
    void Start()
    {
        IMoveOps<float> opFloat = new MoveOpsFloat();
        HasMoveOps<float> floaty = new HasMoveOps<float>();
        floaty.MoveOps = opFloat;
        
        IMoveOps<Vector2> opVecty = new MoveOpsVector2();
        HasMoveOps<Vector2> vecty = new HasMoveOps<Vector2>();
        vecty.MoveOps = opVecty;

        float fv = 0.4f;
        float fref = 0.8f;
        float fixedType = 6.28f;

        Vector2 vv = new Vector2(0.4f, 0.4f);
        Vector2 vref = new Vector2(0.8f, 0.8f);
        Vector2 vout;

        Debug.Log("floaty.Function...");
        floaty.Function(fv, ref fref, fixedType);

        Debug.Log("vecty.Function...");
        vecty.Function(vv, ref vref, fixedType);

    }

    void Update()
    {
        
    }
}
