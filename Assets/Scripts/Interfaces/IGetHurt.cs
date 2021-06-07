using UnityEngine;

/// <summary>
/// Interface for a class that can TakeDamage
/// </summary>
public interface IGetHurt
{
    /// <summary>
    /// Tells the entity the projectile has hit it
    /// This is the Physics2D collision version
    /// </summary>
    /// <param name="collision">Information about the hit</param>
    /// <param name="damage">Normal amount of damage</param>
    /// <param name="projectile">The projectile object that has hit</param>
    /// <returns>True if the hit was accepted, false if ignored</returns>
    public bool TakeDamage(Collision2D collision, int damage, ICanHit attacker);
    /// <summary>
    /// Tells the entity it was hit
    /// This is the Collider2D Trigger version
    /// </summary>
    /// <param name="otherCollider"></param>
    /// <param name="damage"></param>
    /// <param name="projectile"></param>
    /// <returns></returns>
    public bool TakeDamage(Collider2D otherCollider, int damage, ICanHit attacker);
}