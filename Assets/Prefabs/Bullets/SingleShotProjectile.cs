using UnityEngine;

/// <summary>
/// A projectile that initiates its flight path as a straight line to the right, from where it spawns
/// </summary>
public class SingleShotProjectile : Projectile
{
    void Start()
    {
        m_rigidBodySelf.velocity = transform.right * m_speed;
    }
}
