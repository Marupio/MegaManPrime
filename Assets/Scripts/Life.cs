using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    private bool alive;

    public int Health { get => health; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    /// <summary>
    /// Gasps are components that want to do something right before this object is destroyed
    /// </summary>
    private List<IGasp> gasps;

    public void Awake()
    {
        health = MaxHealth;
        alive = true;
        GetGasps();
    }


    public void FixedUpdate()
    {
        if (!alive)
        {
            // Keep trying to die
            Die();
        }
    }


    /// <summary>
    /// Something hurt me, take damage, maybe even die
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (!alive)
        {
            return;
        }
        health -= damage;
        if (health <= 0)
        {
            alive = false;
            Die();
        }
    }


    /// <summary>
    /// Something is healing me
    /// </summary>
    /// <param name="healing">Amount of healing</param>
    /// <param name="okayToExceedMax">When true, health can go above maxHealth</param>
    public void Heal(int healing, bool okayToExceedMax = false)
    {
        if (!alive)
        {
            return;
        }
        if (okayToExceedMax)
        {
            health += healing;
            return;
        }
        if (health >= MaxHealth)
        {
            return;
        }
        health = Mathf.Max(MaxHealth, health + healing);
    }


    /// <summary>
    /// 
    /// </summary>
    public void Die()
    {
        bool readyToDie = true;
        foreach(IGasp gasper in gasps)
        {
            if (!gasper.Gasped())
            {
                gasper.Gasp();
            }
            if (!gasper.ReadyToDie())
            {
                readyToDie = false;
            }
        }
        if (readyToDie)
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Register a gasp to the list
    /// </summary>
    /// <param name="gasper">Component that wants to do something before being Destroyed</param>
    public void IHaveFinalWords(IGasp gasper)
    {
        gasps.Add(gasper);
    }


    /// <summary>
    /// For whatever reason, a gasper is now gone, so it nolonger needs to do anything before Destroy
    /// </summary>
    /// <param name="gasper">Component that lelft</param>
    public void NeverMind(IGasp gasper)
    {
        gasps.Remove(gasper);
    }


    /// <summary>
    /// Initiate list of components that need to do something before Destroy
    /// </summary>
    private void GetGasps()
    {
        IGasp[] gaspers = GetComponentsInChildren<IGasp>();
        gasps = new List<IGasp>(gaspers);
    }
}
