using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemy : Enemy
{
    public GameObject deathExplosion;
    public int maxHealth;

    private int currentHealth;

    PassiveEnemy(int maxHealthIn, GameObject deathExplosionIn)
    {
        maxHealth = maxHealthIn;
        deathExplosion = deathExplosionIn;
    }


    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            Die();
        }
    }


    public override bool Hit(Transform hitPoint, int damage, GameObject bullet)
    {
        currentHealth -= damage;
        return true;
    }


    public override int GetMaxHealth()
    {
        return maxHealth;
    }


    public override int GetCurrentHealth()
    {
        return currentHealth;
    }


    public override void Die()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);
        if (reaper == null)
        {
            Destroy(gameObject);
        }
    }
}
