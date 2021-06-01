using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class FickleBlock : SnapToTileGrid
{
    private Rigidbody2D self;
    private Animator animator;
    private Animation animation;

    public float on;
    public float off;
    public float cycle;

    public Sprite solidSprite;
    public AnimationClip appearingAnimation;
    public AnimationClip disappearingAnimation;

    private bool exists;

    /// <summary>
    /// When true, the block is visible when the cycle loops
    /// </summary>
    private bool startsVisible;

    // public AnimatedTile appearingAnimation;
    // public AnimatedTile disappearingAnimation;

    void Awake()
    {
        self = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startsVisible = false;
        exists = false;
        if (on > off)
        {
            startsVisible = true;
            exists = true;
        }
    }


    void FixedUpdate()
    {
        float currentTime = Time.time;
        int nCycles = (int)(currentTime / cycle);
        float cycleTime = currentTime - nCycles*cycle;

        bool existsWas = exists;
        exists = false;
        if (startsVisible)
        {
            if (cycleTime >= on || cycleTime < off)
            {
                exists = true;
            }
        }
        else if (cycleTime >= on && cycleTime < off)
        {
            exists = true;
        }

    }
}



// public float attackTime;
// public float damageTime;
// public float deathTime;
// public float idleTime;

// private Animator anim;
// private AnimationClip clip;

// // Use this for initialization
// void Start()
// {
//     anim = GetComponent<Animator>();
//     if (anim == null)
//     {
//         Debug.Log("Error: Did not find anim!");
//     }
//     else
//     {
//         //Debug.Log("Got anim");
//     }

//     UpdateAnimClipTimes();
// }
// public void UpdateAnimClipTimes()
// {
//     AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
//     foreach (AnimationClip clip in clips)
//     {
//         switch (clip.name)
//         {
//             case "Attacking":
//                 attackTime = clip.length;
//                 break;
//             case "Damage":
//                 damageTime = clip.length;
//                 break;
//             case "Dead":
//                 deathTime = clip.length;
//                 break;
//             case "Idle":
//                 idleTime = clip.length;
//                 break;
//         }
//     }
// }
