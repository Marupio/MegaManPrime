// using UnityEngine;

// public interface IProjectile
// {
//     /// <summary>
//     /// ScatterShot flag
//     /// </summary>
//     /// <returns>True if the projectile can hit at multiple points, causing multple damage</returns>
//     public bool ScatterShot();

//     /// <summary>
//     /// Deflect the projectile
//     /// </summary>
//     public bool Deflect();

//     /// <summary>
//     /// A 2D physics-based collision has occurred
//     /// We support these for objects that MegaMan cannot pass through, such as destructible blocks
//     /// This function must tell the IEnemy class involved that it got hit
//     /// The advantage of this one is it tells the enemy exactly where it got hit
//     /// </summary>
//     /// <param name="collision">Informamtion about the collision</param>
//     public void OnCollisionEnter2D(Collision2D collision);

//     /// <summary>
//     /// A trigger-type Collider2D collision has occurred
//     /// We support these for objects that MegaMan can pass through, such as enemies
//     /// This function must tell the IEnemy class involved that it got hit
//     /// </summary>
//     /// <param name="hitInfo">The Collider of the other object involved in the collision</param>
//     public void OnTriggerEnter2D(Collider2D hitInfo);
// }
