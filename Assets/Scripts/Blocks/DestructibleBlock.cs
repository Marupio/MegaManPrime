using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(ILive))]
[RequireComponent(typeof(ISelfDestruct))]
public class DestructibleBlock : SnapToTileGrid, ILoyalty, IGetHurt, IDie
{
    private ILive health;
    private ISelfDestruct reaper;
    private SpriteRenderer spriterer;

    public List<Sprite> sprites;
    public List<GameObject> explosions;
    private bool exploded = false;
    private int currentState;
    private float deltaH;
    public List<int> stateTransitions;


    // *** ILoyalty interface
    public Team side { get; set; }


    // *** MonoBehaviour interface

    void Awake()
    {
        side = Team.Neutral;
        health = GetComponent<ILive>();
        reaper = GetComponent<ISelfDestruct>();
        spriterer = GetComponent<SpriteRenderer>();
        int maxHealth = health.MaxHealth;
        deltaH = (float)maxHealth / (float)sprites.Count;
        stateTransitions = new List<int>(sprites.Count);
        for (int i = 1; i <= sprites.Count; ++i)
        {
            int transition = Mathf.RoundToInt(deltaH * (float)i);
            stateTransitions.Add(transition);
        }
        int currentHealth = health.Health;
        SetCurrentState(currentHealth);
        UpdateState();
    }


    void Start()
    {
        if (explosions.Count > 0)
        {
            reaper.IHaveFinalWords(this);
        }
    }


    void FixedUpdate()
    {
        if (SetCurrentState(health.Health))
        {
            UpdateState();
        }
    }


    // *** IDie interface
    public void Die()
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
    public bool Dying()
    {
        return exploded;
    }
    public bool ReadyToDie()
    {
        return exploded;
    }


    // *** ILive interface

    public bool Hit(Transform hitPoint, int damage, GameObject bullet)
    {
        health.TakeDamage(damage);
        return true;
    }

    public bool TakeDamage(Collision2D collision, int damage, ICanHit attacker)
    {
        health.TakeDamage(damage);
        return true;
    }
    public bool TakeDamage(Collider2D targetCollider, int damage, ICanHit attacker)
    {
        health.TakeDamage(damage);
        return true;
    }


    // *** Private member functions

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
                if (delta <= 0)
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
