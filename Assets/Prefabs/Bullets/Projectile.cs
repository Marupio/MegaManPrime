using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a projectile
/// A physical projectile with a speed, range, damage, impact effects.
/// Does not initiate flight path
/// </summary>
public class Projectile : MonoBehaviour
{
    protected float m_createdTime;
    protected Rigidbody2D m_rigidBodySelf;
    [SerializeField] protected LayerMask m_ignoreHitsOnTheseLayer;
    [Tooltip("Base damage dealt when the projectile hits")]
    [SerializeField] [Range(1, 100)] protected int m_damage;
    [Tooltip("Speed of the projectile")]
    [SerializeField] [Range(0.1f, 100.0f)] protected float m_speed;
    [Tooltip("Distance the projectile will travel before disappearing")]
    [SerializeField] [Range(1, 500)] protected int m_range;
    protected float m_duration;

    private void Awake()
    {
        m_rigidBodySelf = GetComponent<Rigidbody2D>();
        m_duration = m_range / m_speed;
        m_createdTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (Time.time - m_createdTime >= m_duration)
        {
            // Out of range
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            return;
        }
        if (hitInfo.tag.Contains("Deflective"))
        {
            Deflect();
            return;
        }
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(hitInfo.transform, m_damage, gameObject);
            Destroy(gameObject);
        }
        Debug.Log("Hit " + hitInfo.name + ", with tag[" + hitInfo.tag + "]");
    }

    /// <summary>
    /// Deflect the projectile, sending it backwards, 45 degrees upwards
    /// Renders it unable to cause any further damage
    /// </summary>
    public void Deflect()
    {
        m_rigidBodySelf.velocity = new Vector2(-1, 1).normalized * m_speed;

        // A deflected bullet can't hit anything anymore
        GetComponent<Collider2D>().enabled = false;
    }
}
