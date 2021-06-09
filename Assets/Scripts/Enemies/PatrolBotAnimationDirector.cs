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

    struct ClipData
    {
        public ClipData(string nameIn, float lengthIn)
        {
            name = nameIn;
            length = lengthIn;
        }
        public string name;
        public float length;
    }
    List<ClipData> m_clipData;


    void Awake()
    {
        m_controller = GetComponent<PatrolBot>();
        m_animator = GetComponent<Animator>();
        GetClipData();
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
    void GetClipData()
    {
        // TODO - Use animator layers properly
        AnimatorClipInfo[] clips = m_animator.GetCurrentAnimatorClipInfo(0);
        m_clipData = clips.Select(clip => new ClipData(clip.clip.name, clip.clip.length)).ToList();
    }
}
