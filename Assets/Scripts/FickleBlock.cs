using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class FickleBlock : SnapToTileGrid
{
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    private Collider2D collider;

    public float on;
    public float off;
    public float cycle;

    public Sprite solidSprite;
    public AnimationClip appearingAnimation;
    public AnimationClip hereAnimation;
    public AnimationClip disappearingAnimation;
    public AnimationClip goneAnimation;

    /// <summary>
    /// When true, the block is visible when the cycle loops
    /// </summary>
    private bool startsVisible;
    /// <summary>
    /// Block exists after appearanceAnimation is done playing, and before disappearanceAnimation begins
    /// </summary>
    private float appearanceAnimationStartTime;
    private float appearanceDuration;
    private float disappearanceDuration;
    private float disappearanceAnimationFinishTime;
    private bool exists;

    public enum BlockState
    {
        Appearing,
        Here,
        Disappearing,
        Gone
    }

    BlockState state = BlockState.Gone;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["Appear"] = appearingAnimation;
        animatorOverrideController["Here"] = hereAnimation;
        animatorOverrideController["Disappear"] = disappearingAnimation;
        animatorOverrideController["Gone"] = goneAnimation;
        appearanceDuration = appearingAnimation.length;
        disappearanceDuration = disappearingAnimation.length;
        appearanceAnimationStartTime = on - appearanceDuration;
        if (appearanceAnimationStartTime < 0)
        {
            appearanceAnimationStartTime += cycle;
        }
        disappearanceAnimationFinishTime = off + disappearanceDuration;
        if (disappearanceAnimationFinishTime > cycle)
        {
            disappearanceAnimationFinishTime -= cycle;
        }

        startsVisible = false;
        exists = false;
        state = BlockState.Gone;
        if (on > off)
        {
            startsVisible = true;
            exists = true;
            state = BlockState.Here;
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
        
        // Disable / enable rigidBody if needed
        if (existsWas && ! exists)
        {
            collider.enabled = false;
        }
        else if (!existsWas && exists)
        {
            collider.enabled = true;
        }

        // Check for animator state change
        switch (state)
        {
            case BlockState.Appearing:
                if (exists)
                {
                    state = BlockState.Here;
                    animator.SetTrigger("Here");
                }
                break;
            case BlockState.Here:
                if (!exists)
                {
                    state = BlockState.Disappearing;
                    animator.SetTrigger("Disappear");
                }
                break;
            case BlockState.Disappearing:
                if (cycleTime >= disappearanceAnimationFinishTime)
                {
                    state = BlockState.Gone;
                    animator.SetTrigger("Gone");
                }
                break;
            case BlockState.Gone:
                if (cycleTime >= appearanceAnimationStartTime)
                {
                    state = BlockState.Appearing;
                    animator.SetTrigger("Appear");
                }
                break;
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
