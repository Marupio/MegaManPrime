using UnityEngine;
using System.Collections.Generic;

public class Base<T> {
    protected T m_variable;
    public T Variable {get=>m_variable; set=>m_variable=value;}
    public virtual void DoSomethingWithVariable() {
        Debug.Log("Base DoSomethingWith " + m_variable);
    }
    public Base(T varIn) {
        m_variable = varIn;
    }
    public Base() {}
}

public class BaseFloat : Base<float> {
    public override void DoSomethingWithVariable() {
        Debug.Log("Derived DoSomethingWith " + m_variable);
        base.DoSomethingWithVariable();
    }
    public BaseFloat(float varIn) : base(varIn) {}
    public BaseFloat() {}
}

public class BaseVector : Base<Vector2> {
    public override void DoSomethingWithVariable() {
        Debug.Log("Derived DoSomethingWith " + m_variable);
        base.DoSomethingWithVariable();
    }
    public BaseVector(Vector2 varIn) : base(varIn) {}
    public BaseVector() {}
}

public class GenericListGuy {
    
    public List<Base<float>> floatList;
    public List<Base<Vector2>> vec2List;

    public void DoSomethingWithAllTypes() {
        foreach(Base<float> g in floatList) {
            DoSomethingSpecific(g, "floatList");
            DoSomethingGeneric(g, "floatList");
        }
        foreach(Base<Vector2> g in vec2List) {
            DoSomethingSpecific(g, "vec2List");
            DoSomethingGeneric(g, "vec2List");
        }
    }
    public void DoSomethingGeneric<T1>(Base<T1> gIn, string strIn) {
        Debug.Log(strIn + ", genericSomething with " + gIn.Variable);
        gIn.DoSomethingWithVariable();
    }
    public void DoSomethingSpecific(Base<float> gIn, string strIn) {
        Debug.Log(strIn + " doSomethingSpecific with float " + gIn.Variable);
        gIn.DoSomethingWithVariable();
    }
    public void DoSomethingSpecific(Base<Vector2> gIn, string strIn) {
        Debug.Log(strIn + " doSomethingSpecific with vector " + gIn.Variable);
        gIn.DoSomethingWithVariable();
    }

    public GenericListGuy() {
        floatList = new List<Base<float>>();
        vec2List = new List<Base<Vector2>>();
    }
}
