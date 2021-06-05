using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy base class with interfaces used by other classes that interact with it
/// </summary>
public interface IEnemy
{
    // /// <summary>
    // /// Tells the enemy a bullet has hit it
    // /// </summary>
    // /// <param name="hitPoint">Location of hit</param>
    // /// <param name="damage">Normal amount of damage</param>
    // /// <param name="bullet">Type of bullet that hit</param>
    // /// <returns>True if hit was accepted, false if ignored</returns>
    // public bool Hit(Transform hitPoint, int damage, GameObject bullet);

    /// <summary>
    /// Tells the enemy the projectile has hit it
    /// This is the Physics2D collision version
    /// </summary>
    /// <param name="collision">Information about the hit</param>
    /// <param name="damage">Normal amount of damage</param>
    /// <param name="projectile">The projectile object that has hit</param>
    /// <returns>True if the hit was accepted, false if ignored</returns>
    public bool Hit(Collision2D collision, int damage, IProjectile projectile);
    /// <summary>
    /// Tells the enemy it was hit
    /// This is the Collider2D Trigger version
    /// </summary>
    /// <param name="otherCollider"></param>
    /// <param name="damage"></param>
    /// <param name="projectile"></param>
    /// <returns></returns>
    public bool Hit(Collider2D otherCollider, int damage, IProjectile projectile);

    /// <summary>
    /// Play death scene, if any.
    /// </summary>
    public void Die();
}
