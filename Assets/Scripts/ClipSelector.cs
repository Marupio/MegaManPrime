using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClipSelector : MonoBehaviour
{
    private Animator animator;
    public string clipName;
    HashSet<string> clipNames;

    public ClipSelector(Animator animatorIn, string clipNameIn)
    {
        animator = animatorIn;
        clipName = clipNameIn;
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetTrigger(clipName);
        // AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(gameObject.layer);
        // List<string> clipNames = new List<string>(clips.Length);
        // foreach (AnimatorClipInfo clip in clips)
        // {
        //     clipNames.Add(clip.clip.name);
        // }
        // if (clipNames.Contains(clipName))
        // {
        //     animator.SetTrigger(clipName);
        // }
        // else
        // {
        //     Debug.LogError("ClipSelect '" + gameObject + "' wants to play '" + clipName + "' but this clipName doesn't exist.");
        // }
    }
}
