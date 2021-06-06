using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    /// <summary>
    /// ScatterShot flag
    /// </summary>
    /// <returns>True if the projectile can hit at multiple points, causing multple damage</returns>
    public bool ScatterShot();

    /// <summary>
    /// Deflect the projectile
    /// </summary>
    public void Deflect();

    /// <summary>
    /// A 2D physics-based collision has occurred
    /// We support these for objects that MegaMan cannot pass through, such as destructible blocks
    /// This function must tell the IEnemy class involved that it got hit
    /// The advantage of this one is it tells the enemy exactly where it got hit
    /// </summary>
    /// <param name="collision">Informamtion about the collision</param>
    public void OnCollisionEnter2D(Collision2D collision);

    /// <summary>
    /// A trigger-type Collider2D collision has occurred
    /// We support these for objects that MegaMan can pass through, such as enemies
    /// This function must tell the IEnemy class involved that it got hit
    /// </summary>
    /// <param name="hitInfo">The Collider of the other object involved in the collision</param>
    public void OnTriggerEnter2D(Collider2D hitInfo);

    /// <summary>
    /// Tell the enemy that it has been hit - Collision2D version
    /// </summary>
    /// <param name="collision">Information about the collision</param>
    /// <param name="enemy">Enemy that has been hit</param>
    /// <returns>True if hit was accepted, false causes projectile to ignore me</returns>
    public bool Hit(Collision2D collision, ILive enemy);
    /// <summary>
    /// Tells the enemy that it has been hit - Collider2D version
    /// </summary>
    /// <param name="otherCollider">The enemy collider that has been hit</param>
    /// <param name="enemy">The enemy that has been hit</param>
    /// <returns></returns>
    public bool Hit(Collider2D otherCollider, ILive enemy);
}
