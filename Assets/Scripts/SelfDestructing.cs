using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructing : MonoBehaviour
{
    public float duration;

    private float startTime;

    void Awake()
    {
        if (duration == 0)
        {
            float maxLength = 0;
            Animator[] animators = GetComponentsInChildren<Animator>();
            foreach (Animator anim in animators)
            {
                AnimatorClipInfo[] clips = anim.GetCurrentAnimatorClipInfo(anim.gameObject.layer);
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
    }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
