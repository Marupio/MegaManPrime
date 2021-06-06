using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for entities with health that can be hurt / healed
/// </summary>
public interface ILive
{
    // /// <summary>
    // /// Tells the entity a bullet has hit it
    // /// </summary>
    // /// <param name="hitPoint">Location of hit</param>
    // /// <param name="damage">Normal amount of damage</param>
    // /// <param name="bullet">Type of bullet that hit</param>
    // /// <returns>True if hit was accepted, false if ignored</returns>
    // public bool Hit(Transform hitPoint, int damage, GameObject bullet);

    /// <summary>
    /// Tells the entity the projectile has hit it
    /// This is the Physics2D collision version
    /// </summary>
    /// <param name="collision">Information about the hit</param>
    /// <param name="damage">Normal amount of damage</param>
    /// <param name="projectile">The projectile object that has hit</param>
    /// <returns>True if the hit was accepted, false if ignored</returns>
    public bool Hit(Collision2D collision, int damage, IProjectile projectile);
    /// <summary>
    /// Tells the entity it was hit
    /// This is the Collider2D Trigger version
    /// </summary>
    /// <param name="otherCollider"></param>
    /// <param name="damage"></param>
    /// <param name="projectile"></param>
    /// <returns></returns>
    public bool Hit(Collider2D otherCollider, int damage, IProjectile projectile);
}
