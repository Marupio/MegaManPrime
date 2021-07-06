using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClass {
    float m_data;
    public float Data {
        get {
            Debug.Log("Get called");
            return m_data;
        }
        set {
            Debug.Log("Set called with " + value);
            m_data = value;
        }
    }
    public override string ToString() {
        return "Data = " + m_data;
    }
}

public class UsesDataClass {
    DataClass m_alpha;
    DataClass m_beta;
    public DataClass Alpha {
        get {
            Debug.Log("UsesDataClass:Get called");
            return m_alpha;
        }
        set {
            Debug.Log("UsesDataClass:Set called with " + value);
            m_alpha = value;
        }
    }
}

public class SandBox2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UsesDataClass udc = new UsesDataClass();
        DataClass dc = new DataClass();
        Debug.Log("dc.Data = 0.4");
        dc.Data = 0.4f;
        Debug.Log("udc.Alpha = dc");
        udc.Alpha = dc;
        DataClass dcOther;
        Debug.Log("dcOther = udc.Alpha");
        dcOther = udc.Alpha;
        Debug.Log("dcOther.Data = 0.7");
        dcOther.Data = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
