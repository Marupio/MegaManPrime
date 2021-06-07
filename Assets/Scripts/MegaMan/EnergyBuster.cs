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
            GameObject bullet = Instantiate(m_busterShotLarge, m_firePoint.position, m_firePoint.rotation);
            ILoyalty bulletLive = bullet.GetComponent<ILoyalty>();
            bulletLive.side = Team.GoodGuys;
        }
        else if (chargeTime >= m_mediumChargeTime)
        {
            GameObject bullet = Instantiate(m_busterShotMedium, m_firePoint.position, m_firePoint.rotation);
            ILoyalty bulletLive = bullet.GetComponent<ILoyalty>();
            bulletLive.side = Team.GoodGuys;
        }
        else
        {
            GameObject bullet = Instantiate(m_busterShotSmall, m_firePoint.position, m_firePoint.rotation);
            ILoyalty bulletLive = bullet.GetComponent<ILoyalty>();
            bulletLive.side = Team.GoodGuys;
        }
    }
}
