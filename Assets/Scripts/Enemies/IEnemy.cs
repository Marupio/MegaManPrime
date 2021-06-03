using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy base class with interfaces used by other classes that interact with it
/// </summary>
public interface IEnemy
{
    /// <summary>
    /// Tells the enemy a bullet has hit it
    /// </summary>
    /// <param name="hitPoint">Location of hit</param>
    /// <param name="damage">Normal amount of damage</param>
    /// <param name="bullet">Type of bullet that hit</param>
    /// <returns>True if bullet was absorbed, false if deflected</returns>
    public bool Hit(Transform hitPoint, int damage, GameObject bullet);

    /// <summary>
    /// Play death scene, if any.
    /// </summary>
    public void Die();
}
