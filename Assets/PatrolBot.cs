using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Life))]
public class PatrolBot : MonoBehaviour, IEnemy
{
    Life life;

    // The graphics face left, so PatrolBot's default direction is left
    bool facingLeft;


    void Awake()
    {
        life = GetComponent<Life>();
        facingLeft = true;
    }


    // *** IEnemy interface ***

    public bool Hit(Collision2D collision, int damage, IProjectile projectile)
    {
        List<Vector2> points = new List<Vector2>();

        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);
        // TODO - Google this and fix it with LINQ, .ToList or something like that
        foreach (ContactPoint2D contact in contacts)
        {
            points.Add(contact.point);
        }
        return InternalHit(points, damage, projectile);
    }

    public bool Hit(Collider2D otherCollider, int damage, IProjectile projectile)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(otherCollider.bounds.center);
        return InternalHit(points, damage, projectile);
    }

    private bool InternalHit(List<Vector2> points, int damage, IProjectile projectile)
    {
        int nHits = 0;
        if (facingLeft)
        {
            foreach (Vector2 point in points)
            {
                if (point.x > gameObject.transform.position.x)
                {
                    ++nHits;
                    if (!projectile.ScatterShot())
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
            life.TakeDamage(nHits * damage);
            return true;
        }
        else
        {
            projectile.Deflect();
            return false;
        }
    }


    public void Die()
    {
        // Do death animation
    }
}
