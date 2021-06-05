using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBuster: MonoBehaviour
{
    private Transform m_firePoint;
    
    public GameObject m_busterShotSmall;
    public GameObject m_busterShotMedium;
    public GameObject m_busterShotLarge;
    [Tooltip("Minimum charge time required to generate a medium shot")]
    [SerializeField] private float m_mediumChargeTime;
    [Tooltip("Minimum charge time required to generate a large shot")]
    [SerializeField] private float m_largeChargeTime;

    private void Awake()
    {
        m_firePoint = GetComponent<Transform>();
    }

    public void Shoot(float chargeTime)
    {
        if (chargeTime >= m_largeChargeTime)
        {
            Instantiate(m_busterShotLarge, m_firePoint.position, m_firePoint.rotation);
        }
        else if (chargeTime >= m_mediumChargeTime)
        {
            Instantiate(m_busterShotMedium, m_firePoint.position, m_firePoint.rotation);
        }
        else
        {
            Instantiate(m_busterShotSmall, m_firePoint.position, m_firePoint.rotation);
        }
    }
}
