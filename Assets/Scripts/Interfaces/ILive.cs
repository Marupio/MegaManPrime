using UnityEngine;

/// <summary>
/// Base class for entities with health that can be hurt / healed
/// </summary>
public interface ILive
{
    public int Health { get; }
    public int MaxHealth { get; set; }

    public bool Alive();
    public bool Dead();
    /// <summary>
    /// Something hurt me, take damage, maybe even die
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage);
    /// <summary>
    /// Something is healing me
    /// </summary>
    /// <param name="healing">Amount of healing</param>
    /// <param name="okayToExceedMax">When true, health can go above maxHealth</param>
    public void Heal(int healing, bool okayToExceedMax = false);
}


public class DefaultILive : ILive
{
    private int m_maxHealth;
    private int m_health;
    private bool alive;

    public DefaultILive(int maxHealth)
    {
        m_maxHealth = maxHealth;
        m_health = m_maxHealth;
    }

    public void Awake()
    {
        m_health = MaxHealth;
        alive = true;
    }


    // *** ILive interface
    public int Health { get => m_health; }
    public int MaxHealth { get => m_maxHealth; set => m_maxHealth = value; }

    public bool Alive() { return alive; }
    public bool Dead() { return !alive; }
    public void TakeDamage(int damage)
    {
        if (!alive)
        {
            return;
        }
        m_health -= damage;
        if (m_health <= 0)
        {
            alive = false;
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
            m_health += healing;
            return;
        }
        if (m_health >= MaxHealth)
        {
            return;
        }
        m_health = Mathf.Max(MaxHealth, m_health + healing);
    }
}