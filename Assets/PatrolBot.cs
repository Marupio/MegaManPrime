using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ILive))]
[RequireComponent(typeof(ISelfDestruct))]
[RequireComponent(typeof(Collider2D))]
public class PatrolBot : MonoBehaviour, ILoyalty, IDie, IGetHurt, ICanHit
{
    ILive m_health;
    ISelfDestruct m_reaper;
    Collider2D m_collider;

    [Tooltip("How much damage does MegaMan get when he touches me")]
    [SerializeField] private int m_touchDamage = 5;

    [Tooltip("Explosion death scene, if any")]
    [SerializeField] private GameObject m_explosion;
    private bool m_exploded;

    // The graphics face left, so PatrolBot's default direction is left
    bool m_facingLeft;

    // Smug expression (when shielded and he hit MegaMan)
    bool m_smug;
    float m_smugStart;
    [Tooltip("When he hits MegaMan, he can become smug.  This is how long.")]
    [SerializeField] [Range(0, 2)] private float m_smugDuration = 1;


    // *** MonoBehaviour interface

    void Awake()
    {
        m_health = GetComponent<ILive>();
        m_reaper = GetComponent<ISelfDestruct>();
        m_collider = GetComponent<Collider2D>();
        m_exploded = false;
        m_facingLeft = true;
        m_smug = false;
        m_smugStart = -1;
        side = Team.BadGuys;
    }


    void FixedUpdate()
    {
        if (m_smug && Time.time - m_smugStart > m_smugDuration)
        {
            m_smug = false;
        }
    }

    // *** ILoyalty interface
    public Team side { get; set; }


    // *** IDie interface
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


    // *** IGetHurt interface
    public bool TakeDamage(Collision2D collision, int damage, ICanHit attacker)
    {

        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);
        List<Vector2> points = (from contact in contacts select contact.point).ToList();
        return InternalHit(points, damage, attacker);
    }
    public bool TakeDamage(Collider2D otherCollider, int damage, ICanHit attacker)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(otherCollider.bounds.center);
        return InternalHit(points, damage, attacker);
    }
    // *** IGetHurt interface internal helpers
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
                    if (!attacker.ScatterHit())
                    {
                        break;
                    }
                }
            }
        }
        if (nHits > 0)
        {
            if (!attacker.ScatterHit())
            {
                nHits = 1;
            }
            m_health.TakeDamage(nHits * damage);
            return true;
        }
        else
        {
            if (attacker.Deflectable())
            {
                attacker.Deflect();
            }
            return false;
        }
    }


    // *** ICanHit interface

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.otherCollider;
        IGetHurt other = GeneralTools.ApplyRulesOfEngagement(otherCollider, m_collider, side, "collision.otherCollider");
        if (other == null)
        {
            otherCollider = collision.collider;
            other = GeneralTools.ApplyRulesOfEngagement(otherCollider, m_collider, side, "collision.collider");
        }
        if (other == null)
        {
            return;
        }
        if (other.TakeDamage(collision, m_touchDamage, this))
        {
            // Other has accepted the hit, do our Hit reaction
            m_smugStart = Time.time;
            m_smug = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IGetHurt other = GeneralTools.ApplyRulesOfEngagement(hitInfo, m_collider, side, "collision.otherCollider");
        if (other == null)
        {
            return;
        }
        if (other.TakeDamage(hitInfo, m_touchDamage, this))
        {
            // Other has accepted the hit, do our Hit reaction
            m_smugStart = Time.time;
            m_smug = true;
        }
    }
    public bool Deflectable()
    {
        return false;
    }
    public void Deflect()
    {
        // Do nothing
    }
    public bool ScatterHit()
    {
        return false;
    }
}
