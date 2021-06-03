using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy base class with interfaces used by other classes that interact with it
/// </summary>
abstract public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Component can take over death by calling LastGasp
    /// </summary>
    public Enemy reaper;

    /// <summary>
    /// Tells the enemy a bullet has hit it
    /// </summary>
    /// <param name="hitPoint">Location of hit</param>
    /// <param name="damage">Normal amount of damage</param>
    /// <param name="bullet">Type of bullet that hit</param>
    /// <returns>True if bullet was absorbed, false if deflected</returns>
    abstract public bool Hit(Transform hitPoint, int damage, GameObject bullet);

    /// <summary>
    /// Returns maximum health
    /// </summary>
    abstract public int GetMaxHealth();

    /// <summary>
    /// Returns current health
    /// </summary>
    abstract public int GetCurrentHealth();


    public virtual void LastGasp(Enemy angel)
    {
        if (reaper == null)
        {
            reaper = angel;
        }
        else
        {
            reaper.LastGasp(angel);
        }
    }


    public virtual void Die()
    {
        if (reaper == null)
        {
            Destroy(gameObject);
        }
    }
}
