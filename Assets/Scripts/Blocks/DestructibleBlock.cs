using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Enemy))]
public class DestructibleBlock : SnapToTileGrid
{
    private Enemy enemy;

    public List<Sprite> states;
    // public Sprite state0;
    // public Sprite state1;
    // public Sprite state2;
    // public Sprite state3;
    // public Sprite state4;
    // public Sprite state5;

    private List<int> stateTransitions;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        stateTransitions = new List<int>(states.Count);
        int maxHealth = enemy.GetMaxHealth();
        int deltaH = maxHealth / states.Count;
    }

    void FixedUpdate()
    {
        
    }
}
