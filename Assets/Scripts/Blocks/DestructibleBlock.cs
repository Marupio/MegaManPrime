using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Life))]
public class DestructibleBlock : SnapToTileGrid, IEnemy, IGasp
{
    private Life life;
    private SpriteRenderer spriterer;

    public List<Sprite> sprites;
    public List<GameObject> explosions;
    private bool exploded = false;
    private int currentState;
    private float deltaH;
    private List<int> stateTransitions;

    void Awake()
    {
        life = GetComponent<Life>();
        spriterer = GetComponent<SpriteRenderer>();
        int maxHealth = life.MaxHealth;
        deltaH = (float)maxHealth / (float)sprites.Count;
        stateTransitions = new List<int>(sprites.Count);
        for (int i = 1; i <= sprites.Count; ++i)
        {
            int transition = Mathf.RoundToInt(deltaH * (float)i);
            stateTransitions.Add(transition);
        }
        int currentHealth = life.Health;
        SetCurrentState(currentHealth);
        UpdateState();
    }


    void FixedUpdate()
    {
        if (SetCurrentState(life.Health))
        {
            UpdateState();
        }
    }


    // *** IGasp interface ***

    /// <summary>
    /// Do death scene
    /// </summary>
    public void Gasp()
    {
        if (explosions.Count > 0)
        {
            int zOffset = 0;
            // List<GameObject> reverseExplosions = explosions;
            // reverseExplosions.Reverse();
            foreach (GameObject explosion in explosions)
            {
                Vector3 pos = gameObject.transform.position;
                pos.z += zOffset;
                Instantiate(explosion, pos, gameObject.transform.rotation);
                ++zOffset;
            }
        }
        exploded = true;
    }


    /// <summary>
    /// Gasp has already been called on me, check if I'm ready to die
    /// </summary>
    /// <returns></returns>
    public bool Gasped()
    {
        return exploded;
    }


    /// <summary>
    /// Returns true when I'm ready to be Destroyed
    /// </summary>
    public bool ReadyToDie()
    {
        return exploded;
    }


    // *** IEnemy interface ***

    /// <summary>
    /// Tells the enemy a bullet has hit it
    /// </summary>
    /// <param name="hitPoint">Location of hit</param>
    /// <param name="damage">Normal amount of damage</param>
    /// <param name="bullet">Type of bullet that hit</param>
    /// <returns>True if bullet was absorbed, false if deflected</returns>
    public bool Hit(Transform hitPoint, int damage, GameObject bullet)
    {
        life.TakeDamage(damage);
        return true;
    }

    /// <summary>
    /// Play death scene, if any.
    /// </summary>
    public void Die()
    {
        Gasp();
    }


    // *** Private member functions ***

    /// <summary>
    /// Sets the currentState to the correct value
    /// </summary>
    /// <param name="health">Current health</param>
    /// <returns>True if state has changed</returns>
    private bool SetCurrentState(int health)
    {
        int newState = stateTransitions.Count - 1;
        if (health < 0)
        {
            newState = 0;
        }
        else
        {
            for (int i = 0; i < stateTransitions.Count; ++i)
            {
                int t = stateTransitions[i];
                int delta = health - t;
                if (delta < 0)
                {
                    newState = i;
                    break;
                }
            }
        }
        // Reverse it, because I want it sorted this way in the Inspector and can't be bothered to change the code
        newState = sprites.Count - newState - 1;
        if (newState != currentState)
        {
            currentState = newState;
            return true;
        }
        return false;
    }

    private void UpdateState()
    {
        spriterer.sprite = sprites[currentState];
    }
}
