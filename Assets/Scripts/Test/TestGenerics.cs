using UnityEngine;

public class TestGenerics : MonoBehaviour {
    GenericListGuy m_gg;

    public void Awake() {
        Debug.Log("TestGenerics Awake");
        m_gg = new GenericListGuy();
    }

    public void Start() {
        Debug.Log("TestGenerics Start");
        BaseVector bv0 = new BaseVector(Vector2.up);
        BaseVector bv1 = new BaseVector(Vector2.left);
        Base<Vector2> bv2 = new Base<Vector2>(Vector2.right);
        BaseFloat bf0 = new BaseFloat(0);
        BaseFloat bf1 = new BaseFloat(3.14f);
        Base<float> bf2 = new Base<float>(0.77f);
        m_gg.floatList.Add(bf0);
        m_gg.floatList.Add(bf1);
        m_gg.floatList.Add(bf2);
        m_gg.vec2List.Add(bv0);
        m_gg.vec2List.Add(bv1);
        m_gg.vec2List.Add(bv2);
        Debug.Log("DoSomethingWithAllTypes - begin");
        m_gg.DoSomethingWithAllTypes();
        Debug.Log("DoSomethingWithAllTypes - end");
    }
}
