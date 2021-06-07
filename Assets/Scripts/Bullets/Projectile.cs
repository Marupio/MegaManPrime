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
public class Projectile : MonoBehaviour, ICanHit, ILoyalty, IDie, IDestroy
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

    // *** ILoyalty interface ***
    public Team side { get; set; }

    // *** MonoBehaviour interface ***

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
        if (m_splat != null)
        {
            IHaveFinalWords(this);
        }
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


    // *** ICanHit interface ***

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.otherCollider;
        IGetHurt other = CheckCollider(otherCollider, "collision.otherCollider");
        if (other == null)
        {
            otherCollider = collision.collider;
            other = CheckCollider(otherCollider, "collision.collider");
        }
        if (other == null)
        {
            return;
        }
        if (other.TakeDamage(collision, m_damage, this))
        {
            // Other has accepted the hit, do our Hit reaction
            Hit(otherCollider, other);
        }
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IGetHurt other = CheckCollider(hitInfo, "OnTriggerEnter2D");
        if (other != null)
        {
            if (other.TakeDamage(m_collider, m_damage, this))
            {
                // Entity has accepted the hit, do our Hit reaction
                Hit(hitInfo, other);
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
        m_rigidBodySelf.velocity = new Vector2(-1, 1).normalized * m_speed;

        // A deflected bullet can't hit anything anymore
        GetComponent<Collider2D>().enabled = false;
    }
    public bool ScatterHit()
    {
        return false;
    }


    // *** IDie interface ***

    public void Die()
    {
        if (!m_splatted)
        {
            Instantiate(m_splat, gameObject.transform.position, gameObject.transform.rotation);
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


    // *** IDestroy interface ***

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


    // *** Internal member functions ***
    
    /// <summary>
    /// Check if this is an actual hit
    /// </summary>
    /// <param name="hitInfo">The other collider involved</param>
    /// <param name="origin">The function calling this function, debugging purposes</param>
    /// <returns>The entity that we just hit</returns>
    protected IGetHurt CheckCollider(Collider2D hitInfo, string origin)
    {
        if (hitInfo == m_collider)
        {
            Debug.Log("Projectile - I found my own collider from " + origin);
        }
        ILoyalty team = hitInfo.gameObject.GetComponent<ILoyalty>();
        GeneralTools.AssertNotNull(team, "Projectile CheckCollider " + origin);
        if (team.side != Team.Neutral && team.side == side)
        {
            // No friendly fire
            return null;
        }
        IGetHurt entity = hitInfo.GetComponent<IGetHurt>();
        GeneralTools.AssertNotNull(entity, "Projectile CheckCollider " + origin);
        // Don't hit the same entity twice
        if (m_objectsHit.Contains(entity))
        {
            return null;
        }
        return entity;
    }

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
