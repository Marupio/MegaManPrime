using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaManRunTimer : MonoBehaviour
{

    [SerializeField] private Collider2D m_megaMan;
    [SerializeField] private Collider2D m_start;
    [SerializeField] private Collider2D m_end;
    float startTime = -1;

    private void FixedUpdate()
    {
        if (startTime < 0 && m_megaMan.IsTouching(m_start))
        {
            startTime = Time.time;
        }
        if (startTime > 0 && m_megaMan.IsTouching(m_end))
        {
            Debug.Log("Time is " + (Time.time - startTime));
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (startTime < 0)
    //     {
    //         startTime = Time.time;
    //     }
    //     else
    //     {
    //         Debug.Log("Time is " + (Time.time - startTime));
    //     }
    // }
}
