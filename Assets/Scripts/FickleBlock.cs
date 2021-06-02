using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class FickleBlock : SnapToTileGrid
{
    private Animator anim;
    private AnimatorOverrideController aoc;
    private Collider2D col;

    public bool active = true;
    public float on;
    public float off;
    public float cycle;

    // public Sprite solidSprite;
    public AnimationClip animationAppear;
    public AnimationClip animationHere;
    public AnimationClip animationDisappear;
    public AnimationClip animationGone;

    public enum BlockState
    {
        Appearing,
        Here,
        Disappearing,
        Gone
    }
    public BlockState loopState;

    public float tHere; // Start of Here state
    public float tDiss; // Start of Disappear state
    public float tGone; // Start of Gone state
    public float tApp;  // Start of Appear state

    public float durationHere; // Duration of Here state
    public float durationDiss; // Duration of Dissappear state
    public float durationGone; // Duration of Gone state
    public float durationApp;  // Duration of Appear state

    // Current state
    public BlockState state;
    public float cycleTime;
    private int nCycles;
    private int nCyclesLast = -1;
    private bool here;
    
    void Awake()
    {
        // Set references
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);

        // Set animation clips
        anim.runtimeAnimatorController = aoc;
        aoc["Appear"] = animationAppear;
        aoc["Here"] = animationHere;
        aoc["Disappear"] = animationDisappear;
        aoc["Gone"] = animationGone;

        // Calculate times and durations
        tHere = on;
        tDiss = off;
        durationApp = animationAppear.length;
        durationDiss = animationDisappear.length;
        tApp = tHere - durationApp;
        if (tApp < 0)
        {
            tApp += cycle;
            loopState = BlockState.Appearing;
        }
        tGone = tDiss + durationDiss;
        if (tGone > cycle)
        {
            tGone -= cycle;
            loopState = BlockState.Disappearing;
        }
        durationGone = tApp - tGone;
        if (durationGone < 0)
        {
            durationGone += cycle;
            loopState = BlockState.Gone;
        }
        durationHere = tDiss - tHere;
        if (durationHere < 0)
        {
            durationHere += cycle;
            loopState = BlockState.Here;
        }

        InitState();
    }

    void FixedUpdate()
    {
        if (!active)
        {
            return;
        }
        UpdateState();
    }


    public void Gone()
    {
        if (here)
        {
            here = false;
            col.enabled = false;
        }
    }
    public bool IsGone()
    {
        return !here;
    }


    public void Here()
    {
        if (!here)
        {
            here = true;
            col.enabled = true;
        }
    }
    public bool IsHere()
    {
        return here;
    }


    public void Disable()
    {
        active = false;
    }
    public bool Disabled()
    {
        return !active;
    }


    public void Enable()
    {
        active = true;
    }
    public bool Enabled()
    {
        return active;
    }

    /// <summary>
    /// Updates nCycles and cycleTime with current Time value
    /// </summary>
    void UpdateCurrentState()
    {
        float currentTime = Time.time;
        nCycles = (int)(currentTime / cycle);
        cycleTime = currentTime - nCycles * cycle;
    }


    /// <summary>
    /// Initializes animator, here flag and collider
    /// </summary>
    void InitState()
    {
        switch (loopState)
        {
            case BlockState.Appearing:
                if (cycleTime < tHere)
                {
                    anim.SetTrigger("Appear");
                    Gone();
                }
                else if (cycleTime < tDiss)
                {
                    anim.SetTrigger("Here");
                    Here();
                }
                else if (cycleTime < tGone)
                {
                    anim.SetTrigger("Disappear");
                    Gone();
                }
                else if (cycleTime < tApp)
                {
                    anim.SetTrigger("Gone");
                    Gone();
                }
                else
                {
                    anim.SetTrigger("Appear");
                    Gone();
                }
                break;
            case BlockState.Here:
                if (cycleTime < tDiss)
                {
                    anim.SetTrigger("Here");
                    Here();
                }
                else if (cycleTime < tGone)
                {
                    anim.SetTrigger("Disappear");
                    Gone();
                }
                else if (cycleTime < tApp)
                {
                    anim.SetTrigger("Gone");
                    Gone();
                }
                else if (cycleTime < tHere)
                {
                    anim.SetTrigger("Appear");
                    Gone();
                }
                else
                {
                    anim.SetTrigger("Here");
                    Here();
                }
                break;
            case BlockState.Disappearing:
                if (cycleTime < tGone)
                {
                    anim.SetTrigger("Disappear");
                    Gone();
                }
                else if (cycleTime < tApp)
                {
                    anim.SetTrigger("Gone");
                    Gone();
                }
                else if (cycleTime < tHere)
                {
                    anim.SetTrigger("Appear");
                    Gone();
                }
                if (cycleTime < tDiss)
                {
                    anim.SetTrigger("Here");
                    Here();
                }
                else
                {
                    anim.SetTrigger("Disappear");
                    Gone();
                }
                break;
            case BlockState.Gone:
                if (cycleTime < tApp)
                {
                    anim.SetTrigger("Gone");
                    Gone();
                }
                else if (cycleTime < tHere)
                {
                    anim.SetTrigger("Appear");
                    Gone();
                }
                if (cycleTime < tDiss)
                {
                    anim.SetTrigger("Here");
                    Here();
                }
                else if (cycleTime < tGone)
                {
                    anim.SetTrigger("Disappear");
                    Gone();
                }
                else
                {
                    anim.SetTrigger("Gone");
                    Gone();
                }
                break;
        }
    }


    void UpdateState()
    {
        // Get state variables up-to-date
        UpdateCurrentState();

        // Calculate any changes of state
        switch (state)
        {
            case BlockState.Appearing:
                if (loopState == state && cycleTime > tGone)
                {
                    break;
                }
                if (cycleTime >= tHere)
                {
                    state = BlockState.Here;
                    anim.SetTrigger("Here");
                    Here();
                }
                break;
            case BlockState.Here:
                if (loopState == state && cycleTime > tApp)
                {
                    break;
                }
                if (cycleTime >= tDiss)
                {
                    state = BlockState.Disappearing;
                    anim.SetTrigger("Disappear");
                    Gone();
                }
                break;
            case BlockState.Disappearing:
                if (loopState == state && cycleTime > tHere)
                {
                    break;
                }
                if (cycleTime >= tGone)
                {
                    state = BlockState.Gone;
                    anim.SetTrigger("Gone");
                    Gone();
                }
                break;
            case BlockState.Gone:
                if (state == loopState && cycleTime > tDiss)
                {
                    break;
                }
                if (cycleTime >= tApp)
                {
                    state = BlockState.Appearing;
                    anim.SetTrigger("Appear");
                    Gone();
                }
                break;
        }
        // Edge case detection
        if (nCyclesLast != nCycles && state != loopState)
        {
            state = loopState;
            SetAnimator();
        }
        nCyclesLast = nCycles;
    }


    /// <summary>
    /// If we get out of sync, this sends the animator trigger for the current state
    /// </summary>
    void SetAnimator()
    {
        switch (state)
        {
            case BlockState.Appearing:
                anim.SetTrigger("Appear");
                break;
            case BlockState.Here:
                anim.SetTrigger("Here");
                break;
            case BlockState.Disappearing:
                anim.SetTrigger("Disappear");
                break;
            case BlockState.Gone:
                anim.SetTrigger("Gone");
                break;
        }
    }
}
