using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumBulletSplat : MonoBehaviour
{
    private float m_createdTime;
    [SerializeField] private float m_duration = 1;

    private void Awake()
    {
        m_createdTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - m_createdTime >= m_duration)
        {
            Destroy(gameObject);
        }
    }
}
