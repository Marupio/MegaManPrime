using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ClipSelector : MonoBehaviour
{
    private Animator animator;
    private AnimatorOverrideController aoc;
    public string clipName;

    void Awake()
    {
        animator = GetComponent<Animator>();
        // AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(gameObject.layer);
        // List<string> clipNames = new List<string>(clips.Length);
        // foreach (AnimatorClipInfo clip in clips)
        // {
        //     clipNames.Add(clip.clip.name);
        // }
        animator.SetTrigger(clipName);
    }
}
