using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple timed self-destructing script
/// GameObject gets destroyed after a user-supplied duration
/// If no duration is supplied, it attempts to lookup the longest animation clip in a component animator, untested.
/// Supports ISelfDestruct interface, so other components can have their own death scenes
/// </summary>
public class SelfDestructTimer : MonoBehaviour, ISelfDestruct
{
    public float duration;

    private float startTime;

    bool alive;
    /// <summary>
    /// OverActors are components that want to do something right before this object is destroyed
    /// </summary>
    private List<IDie> overActors;

    void Awake()
    {
        if (duration == 0)
        {
            float maxLength = 0;
            Animator[] animators = GetComponentsInChildren<Animator>();
            foreach (Animator anim in animators)
            {
                // TODO - Use animator layers properly
                AnimatorClipInfo[] clips = anim.GetCurrentAnimatorClipInfo(0);
                foreach (AnimatorClipInfo clip in clips)
                {
                    maxLength = Mathf.Max(maxLength, clip.clip.length);
                }
            }
            if (maxLength > 0)
            {
                duration = maxLength;
            }
        }
        alive = true;
        overActors = new List<IDie>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        if (!alive)
        {
            FinalRites();
            return;
        }
        if (Time.time - startTime >= duration)
        {
            FinalRites();
            alive = false;
        }
    }


    // *** ISelfDestruct interface
    public void FinalRites()
    {
        bool readyToDie = true;
        foreach (IDie overActor in overActors)
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
        if (!overActors.Contains(overActor))
        {
            overActors.Add(overActor);
        }
    }
    public void NeverMind(IDie overActor)
    {
        if (overActors.Contains(overActor))
        {
            overActors.Remove(overActor);
        }
    }
}
