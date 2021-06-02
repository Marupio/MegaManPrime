using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemy : Enemy
{
    public GameObject deathExplosion;
    public int maxHealth;

    private int currentHealth;

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


    override public bool Hit(Transform hitPoint, int damage, GameObject bullet)
    {
        currentHealth -= damage;
        return true;
    }


    override public int GetMaxHealth()
    {
        return maxHealth;
    }


    public override int GetCurrentHealth()
    {
        return currentHealth;
    }


    void Die()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
