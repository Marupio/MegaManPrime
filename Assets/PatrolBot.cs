using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ILive))]
[RequireComponent(typeof(IDestroy))]
[RequireComponent(typeof(Collider2D))]
public class PatrolBot : MonoBehaviour, ILoyalty, IDie, IGetHurt, ICanHit
{
    ILive m_health;
    IDestroy m_reaper;
    Collider2D m_collider;

    [Tooltip("How much damage does MegaMan get when he touches me")]
    [SerializeField] private int m_touchDamage = 5;

    [Tooltip("Explosion death scene, if any")]
    [SerializeField] private GameObject m_explosion;
    private bool m_exploded;

    // *** ILoyalty interface
    public Team side { get; set; }


    // The graphics face left, so PatrolBot's default direction is left
    bool m_facingLeft;


    void Awake()
    {
        m_health = GetComponent<ILive>();
        m_reaper = GetComponent<IDestroy>();
        m_collider = GetComponent<Collider2D>();
        m_facingLeft = true;
        side = Team.BadGuys;
        m_exploded = false;
    }


    // *** IDie interface ***

    public void Die()
    {
        if (m_explosion != null)
        {
            Instantiate(m_explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        m_exploded = true;
    }
    public bool Dying()
    {
        return m_exploded;
    }
    public bool ReadyToDie()
    {
        return m_exploded;
    }


    // *** IGetHurt interface ***

    public bool TakeDamage(Collision2D collision, int damage, ICanHit attacker)
    {
        List<Vector2> points = new List<Vector2>();

        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);
        // TODO - Google this and fix it with LINQ, .ToList or something like that
        foreach (ContactPoint2D contact in contacts)
        {
            points.Add(contact.point);
        }
        return InternalHit(points, damage, attacker);
    }

    public bool TakeDamage(Collider2D otherCollider, int damage, ICanHit attacker)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(otherCollider.bounds.center);
        return InternalHit(points, damage, attacker);
    }


    // *** ICanHit interface ***

    public void OnCollisionEnter2D(Collision2D collision);
    public void OnTriggerEnter2D(Collider2D hitInfo);
    public bool Deflectable();
    public void Deflect();
    public bool ScatterHit();


    // *** IProjectile interface ***
    /// <summary>
    /// ScatterShot flag
    /// </summary>
    /// <returns>True if the projectile can hit at multiple points, causing multple damage</returns>
    public bool ScatterShot()
    {
        return false;
    }
    public bool Deflect()
    {
        return false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Not implemented
        Debug.LogError("PatrolBot.cs - OnCollisionEnter2D not implemented");
    }

    /// <summary>
    /// I can hurt MegaMan by running into him
    /// </summary>
    /// <param name="hitInfo">The thing I ran into</param>
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        ILive other = hitInfo.gameObject.GetComponentInParent<ILive>();
        if (other != null && other.side != side)
        {
            other.TakeHit(m_collider, m_touchDamage, this);
        }
    }



    private bool InternalHit(List<Vector2> points, int damage, ICanHit attacker)
    {
        int nHits = 0;
        if (m_facingLeft)
        {
            foreach (Vector2 point in points)
            {
                if (point.x > gameObject.transform.position.x)
                {
                    ++nHits;
                    if (!attacker.ScatterHit())
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (Vector2 point in points)
            {
                if (point.x < gameObject.transform.position.x)
                {
                    ++nHits;
                    if (!projectile.ScatterShot())
                    {
                        break;
                    }
                }
            }
        }
        if (nHits > 0)
        {
            if (!projectile.ScatterShot())
            {
                nHits = 1;
            }
            m_health.TakeDamage(nHits * damage);
            return true;
        }
        else
        {
            projectile.Deflect();
            return false;
        }
    }
}
