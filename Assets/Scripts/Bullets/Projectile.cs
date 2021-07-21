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
public class Projectile : MonoBehaviour, ICanHit, ILoyalty, IDie, ISelfDestruct
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
    [Tooltip("What splat does it do when it hits something?")]
    [SerializeField] protected GameObject m_splat;
    protected bool m_splatted;
    [Tooltip("How many frames does the projectile keep going after hitting something, to see if it hits other things, e.g. at a grid edge")]
    [SerializeField] [Range(0, 10)] int m_endurance;

    // List of components that have death scenes
    private List<IDie> m_overActors;
    private bool m_swanSong; // When true, this bullet has hit something, and is running out its m_endurance frames before disintegrating
    private List<IGetHurt> m_objectsHit;
    protected float m_duration;

    // *** ILoyalty interface
    public Team side { get; set; }

    // *** MonoBehaviour interface

    void Awake()
    {
        m_rigidBodySelf = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
        m_swanSong = false;
        m_splatted = false;
        side = Team.Neutral;
        m_duration = m_range / m_speed;
        m_createdTime = Time.time;
        m_objectsHit = new List<IGetHurt>();
        m_overActors = new List<IDie>();
    }

    void Start()
    {
        IHaveFinalWords(this);
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
                FinalRites();
            }
        }
    }


    // *** ICanHit interface
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D targetCollider = collision.collider;
        IGetHurt target = GeneralGameTools.ApplyRulesOfEngagement(targetCollider, m_collider, side, "collision.collider");
        if (target == null || m_objectsHit.Contains(target))
        {
            return;
        }
        if (target.TakeDamage(collision, m_damage, this))
        {
            // Target has accepted the hit, do our Hit reaction
            Hit(targetCollider, target);
        }
    }
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        CheckForColliderHits(hitInfo);
    }
    public void OnTriggerStay2D(Collider2D hitInfo)
    {
        CheckForColliderHits(hitInfo);
    }
    // *** ICanHit interface internal helpers
    private void CheckForColliderHits(Collider2D hitInfo)
    {
        IGetHurt target = GeneralGameTools.ApplyRulesOfEngagement(hitInfo, m_collider, side, "OnTriggerEnter2D");
        // If it isn't null and isn't an entity we already hit, proceed with the hit
        if (target != null && !m_objectsHit.Contains(target))
        {
            if (target.TakeDamage(m_collider, m_damage, this))
            {
                // Entity has accepted the hit, do our Hit reaction
                Hit(hitInfo, target);
            }
        }
    }

    public bool Deflectable()
    {
        return true;
    }
    /// <summary>
    /// Deflect the projectile, sending it backwards, 45 degrees upwards, and renders it unable to cause any further damage
    /// </summary>
    /// <returns>true if deflected</returns>
    public void Deflect()
    {
        transform.Rotate(0f, 180f, 0f);
        Vector2 velocity = m_rigidBodySelf.velocity;
        velocity.x *= -0.7071067812f;
        velocity.y = 0.7071067812f*m_speed;
        m_rigidBodySelf.velocity = velocity;

        // A deflected bullet can't hit anything anymore
        GetComponent<Collider2D>().enabled = false;
    }
    public bool ScatterHit()
    {
        return false;
    }


    // *** IDie interface

    public void Die()
    {
        if (!m_splatted)
        {
            if (m_splat != null)
            {
                Instantiate(m_splat, gameObject.transform.position, gameObject.transform.rotation);
            }
            m_splatted = true;
        }
    }
    public bool Dying()
    {
        return m_splatted;
    }
    public bool ReadyToDie()
    {
        return m_endurance <= 0;
    }


    // *** IDestroy interface

    public void FinalRites()
    {
        bool readyToDie = true;
        foreach(IDie overActor in m_overActors)
        {
            if (!overActor.Dying())
            {
                overActor.Die();
            }
            if (!overActor.ReadyToDie())
            {
                readyToDie = false;
            }
        }
        if (readyToDie)
        {
            Destroy(gameObject);
        }
    }
    public void IHaveFinalWords(IDie overActor)
    {
        m_overActors.Add(overActor);
    }
    public void NeverMind(IDie overActor)
    {
        m_overActors.Remove(overActor);
    }


    // *** Internal member functions

    protected bool Hit(Collider2D collision, IGetHurt entity)
    {
        m_objectsHit.Add(entity);
        if (!m_swanSong)
        {
            Die();
            m_swanSong = true;
        }
        return true;
    }
}
