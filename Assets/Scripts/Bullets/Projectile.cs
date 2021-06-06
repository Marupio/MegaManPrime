using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a projectile
/// A physical projectile with a speed, range, damage, impact effects.
/// Does not initiate flight path
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour, IProjectile
{
    protected float m_createdTime;
    protected Rigidbody2D m_rigidBodySelf;
    protected Collider2D m_collider;
    [SerializeField] protected LayerMask m_ignoreHitsOnTheseLayers;
    [Tooltip("Base damage dealt when the projectile hits")]
    [SerializeField] [Range(1, 100)] protected int m_damage;
    [Tooltip("Speed of the projectile")]
    [SerializeField] [Range(0.1f, 100.0f)] protected float m_speed;
    [Tooltip("Distance the projectile will travel before disappearing")]
    [SerializeField] [Range(1, 500)] protected int m_range;
    [Tooltip("How many frames does the projectile keep going after hitting something, to see if it hits other things, e.g. at a grid edge")]
    [SerializeField] [Range(0, 10)] int m_endurance;
    private bool m_swanSong; // When true, this bullet has hit something, and is running out its m_endurance frames before disintegrating
    private List<ILive> m_objectsHit;
    protected float m_duration;

    void Awake()
    {
        m_rigidBodySelf = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_duration = m_range / m_speed;
        m_createdTime = Time.time;
        m_objectsHit = new List<ILive>();
    }

    void FixedUpdate()
    {
        if (Time.time - m_createdTime >= m_duration)
        {
            // Out of range
            Destroy(gameObject);
        }
        if (m_swanSong)
        {
            --m_endurance;
            if (m_endurance <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


    // *** IProjectile interface ***

    public bool ScatterShot()
    {
        return false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            return;
        }
        ILive enemy = collision.gameObject.GetComponent<ILive>();
        if (enemy != null)
        {
            // Don't hit the same enemy twice
            if (m_objectsHit.Contains(enemy))
            {
                return;
            }
            if (enemy.Hit(collision, m_damage, this))
            {
                // Enemy has accepted the hit, do our Hit reaction
                Hit(collision, enemy);
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            return;
        }
        ILive enemy = hitInfo.GetComponent<ILive>();
        if (enemy != null)
        {
            // Don't hit the same enemy twice
            if (m_objectsHit.Contains(enemy))
            {
                return;
            }
            if (enemy.Hit(m_collider, m_damage, this))
            {
                // Enemy has accepted the hit, do our Hit reaction
                Hit(hitInfo, enemy);
            }
        }
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

    public bool Hit(Collision2D collision, ILive enemy)
    {
        m_objectsHit.Add(enemy);
        m_swanSong = true;
        return true;
    }

    public bool Hit(Collider2D collision, ILive enemy)
    {
        m_objectsHit.Add(enemy);
        m_swanSong = true;
        return true;
    }
}
