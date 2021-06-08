using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple health-managing class
/// Components that have death scenes register with this to play their death scenes before being destroyed.
/// </summary>
public class HealthManager : MonoBehaviour, ILive, ISelfDestruct
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    private bool alive;


    /// <summary>
    /// OverActors are components that want to do something right before this object is destroyed
    /// </summary>
    private List<IDie> overActors;

    public void Awake()
    {
        health = MaxHealth;
        alive = true;
        GetOverActors();
    }


    public void FixedUpdate()
    {
        if (!alive)
        {
            // Keep trying to die
            FinalRites();
        }
    }


    // *** ILive interface

    public int Health { get => health; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public bool Alive() { return alive; }
    public bool Dead() { return !alive; }
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
            FinalRites();
        }
    }
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


    // *** ISelfDestruct interface
    public void FinalRites()
    {
        bool readyToDie = true;
        foreach(IDie overActor in overActors)
        {
            if (!overActor.Dying())
            {
                overActor.Die();
            }
            if (!overActor.ReadyToDie())
            {
                readyToDie = false;
            }
        }
        if (readyToDie)
        {
            Destroy(gameObject);
        }
    }
    public void IHaveFinalWords(IDie overActor)
    {
        overActors.Add(overActor);
    }
    public void NeverMind(IDie overActor)
    {
        overActors.Remove(overActor);
    }


    // *** Internal functions
    private void GetOverActors()
    {
        IDie[] overActorArray = GetComponentsInChildren<IDie>();
        overActors = new List<IDie>(overActorArray);
    }
}


// Attempted 'default' implementation below
//{
//     DefaultISelfDestruct<HealthManager> m_selfDestruct;
//     DefaultILive m_live;

//     [SerializeField] private int m_maxHealth;

//     void Awake()
//     {
//         m_selfDestruct = new DefaultISelfDestruct<HealthManager>(this);
//         m_live = new DefaultILive(m_maxHealth);
//     }


//     void FixedUpdate()
//     {
//         if (m_live.Dead())
//         {
//             m_selfDestruct.FinalRites();
//         }
//     }

//     // *** ILive interface
//     public int Health { get => m_live.Health; }
//     public int MaxHealth { get => m_live.MaxHealth; set => m_live.MaxHealth = value; }

//     public bool Alive() { return m_live.Alive(); }
//     public bool Dead() { return m_live.Dead(); }
//     public void TakeDamage(int damage) { m_live.TakeDamage(damage); }
//     public void Heal(int healing, bool okayToExceedMax = false) { m_live.Heal(healing, okayToExceedMax); }


//     public void FinalRites() { m_selfDestruct.FinalRites(); }
//     public void IHaveFinalWords(IDie overActor) { m_selfDestruct.IHaveFinalWords(overActor); }
//     public void NeverMind(IDie overActor) { m_selfDestruct.NeverMind(overActor); }

// }
