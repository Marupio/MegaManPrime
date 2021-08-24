using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolBot))]
[RequireComponent(typeof(Animator))]
public class PatrolBotAnimationDirector : MonoBehaviour
{
    // *** References
    PatrolBot m_controller;
    Animator m_animator;

    // *** Member variables
    string m_clipName;
    string m_clipNamePrev;
    float m_timeCurrentClipStarted;
    float m_timeCurrentClipEnds;
    string m_clipNameNext;

    public Dictionary<string, double> m_clipData;
    // public struct ClipMetaData
    // {
    //     public ClipMetaData(string nameIn, float lengthIn)
    //     {
    //         name = nameIn;
    //         length = lengthIn;
    //     }
    //     public string name;
    //     public float length;
    // }
    // List<ClipMetaData> m_clipData;

    public Dictionary<string, double> ClipData {get=>m_clipData; set=>m_clipData=value;}

    void Awake()
    {
        m_controller = GetComponent<PatrolBot>();
        m_animator = GetComponent<Animator>();
        MakeClipData();
    }

    void FixedUpdate()
    {
        switch (m_controller.State)
        {
            case PatrolBotState.Scooting:
                if (m_controller.BareShock)
                {
                    m_clipName = "BareShock";
                }
                else if (m_controller.Bare)
                {
                    m_clipName = "BareScooting";
                }
                else if (m_controller.Smug)
                {
                    m_clipName = "SmugScooting";
                }
                else
                {
                    m_clipName = "Scooting";
                }
                break;
            case PatrolBotState.TurnStart:
                if (m_controller.Bare)
                {
                    m_clipName = "BareTurnStart";
                }
                else if (m_controller.Smug)
                {
                    m_clipName = "SmugTurnStart";
                }
                else
                {
                    m_clipName = "TurnStart";
                }
                break;
            case PatrolBotState.TurnFinish:
                if (m_controller.Bare)
                {
                    m_clipName = "BareTurnFinish";
                }
                else if (m_controller.Smug)
                {
                    m_clipName = "SmugTurnFinish";
                }
                else
                {
                    m_clipName = "TurnFinish";
                }
                break;
            default:
                Debug.LogError("Unhandled PatrolBotState: " + m_controller.State);
                break;
        }
        if (m_clipName != m_clipNamePrev)
        {
            m_animator.SetTrigger(m_clipName);
            m_clipNamePrev = m_clipName;
        }
    }


    // *** Private member functions
    void MakeClipData()
    {
        m_clipData = new Dictionary<string, double>();
        // TODO - Use animator layers properly
        AnimatorClipInfo[] clips = m_animator.GetCurrentAnimatorClipInfo(0);
        foreach(AnimatorClipInfo clip in clips) {
            m_clipData.Add(clip.clip.name, clip.clip.length);
        }
        // m_clipData = clips.ToDictionary<string, double>(clips.Select(clip=>clip.clip.name).ToList(), clip => clip.clip.length);
        // m_clipData = clips.Select(clip => new ClipMetaData(clip.clip.name, clip.clip.length)).ToList();
    }
}
