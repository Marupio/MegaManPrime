using UnityEngine;

public interface ICanHit
{
    /// <summary>
    /// A 2D physics-based collision has occurred
    /// We support these for objects that MegaMan cannot pass through, such as destructible blocks
    /// This function must tell the IEnemy class involved that it got hit
    /// </summary>
    /// <param name="collision">Informamtion about the collision</param>
    /// <note>collision.otherCollider (etc.) seems to refer to the attacker, and collision.collider refers to the entity getting hit</note>
    public void OnCollisionEnter2D(Collision2D collision);

    /// <summary>
    /// A trigger-type Collider2D collision has occurred
    /// We support these for objects that MegaMan can pass through, such as enemies.  Implement both Enter and Stay variations.
    /// This function must tell the IEnemy class involved that it got hit
    /// </summary>
    /// <param name="hitInfo">The Collider of the other object involved in the collision</param>
    public void OnTriggerEnter2D(Collider2D hitInfo);
    public void OnTriggerStay2D(Collider2D hitInfo);

    /// <summary>
    /// Am I deflectable?
    /// </summary>
    /// <returns>True if I can be deflected</returns>
    public bool Deflectable();

    /// <summary>
    /// Deflect this attacking entity, if possible
    /// </summary>
    public void Deflect();

    /// <summary>
    /// If I have multiple contact points, can I produce multiple hits?
    /// </summary>
    /// <returns>True if multiple hits are allowed</returns>
    public bool ScatterHit();
}
