using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class to handle MegaMan's animator
/// </summary>
[RequireComponent(typeof(Animator))]
public class MegaManAnimationDirector : MonoBehaviour
{
    private Animator m_animator;
    private SpriteRenderer m_renderer;
    private MegaManController m_controller;
    private Rigidbody2D m_rigidBodySelf;
    private EnergyBuster m_energyBuster;


    // *** Inspector Settings

    [Tooltip("How much time does MegaMan keep his weapon out after shooting")]
    [SerializeField] [Range(0, 1)] private float m_shootFollowThroughTime = 0.5f;


    // *** Animator Values

    private int m_currentState = 0;
    private int m_lastState = -1;
    private int m_lastSpeed = -1;
    private bool m_running = false;
    private bool m_runningPrev = false;
    private bool m_shooting = false;
    private bool m_shootingPrev = false;
    private bool m_ladderTopTransitioning = false;  // When at the top of a ladder, halfway to transition to standing
    private bool m_ladderTopTransitioningPrev = false;  // When at the top of a ladder, halfway to transition to standing
    private bool m_visiblePrev = true;


    // *** Weapon Fire Point Values

    private Vector2 m_currentFirePoint = Vector2.zero;
    private float m_firePointZ;
    private Vector2 m_firePointStandShoot = new Vector2(1.25f, 0);
    private Vector2 m_firePointRunShoot = new Vector2(1.1875f, 0);
    private Vector2 m_firePointJumpShoot = new Vector2(1, 0.125f);
    private Vector2 m_firePointDashShoot = new Vector2(1.3125f, -0.25f);
    private Vector2 m_firePointClimbShoot = new Vector2(1, 0.1875f);


    // *** Access

    public bool Running { get => m_running; set => m_running = value; }
    public bool Shooting { get => m_shooting; set => m_shooting = value; }
    public bool LadderTopTransitioning { get => m_ladderTopTransitioning; set => m_ladderTopTransitioning = value; }


    // *** Edit

    public void Shoot()
    {
        m_shooting = true;
        // Update Weapon Fire Point
        Vector2 newFirePoint = new Vector2();
        switch (m_currentState)
        {
            case 0:
            {
                if (m_running)
                {
                    newFirePoint = m_firePointRunShoot;
                }
                else
                {
                    newFirePoint = m_firePointStandShoot;
                }
                break;
            }
            case 1:
            {
                newFirePoint = m_firePointJumpShoot;
                break;
            }
            case 2:
            {
                newFirePoint = m_firePointDashShoot;
                break;
            }
            case 3:
            {
                newFirePoint = m_firePointClimbShoot;
                break;
            }
            default:
            {
                Debug.LogError("Unhandled state for Weapon Fire Point position");
                break;
            }
        }
        if (newFirePoint != m_currentFirePoint)
        {
            m_energyBuster.transform.localPosition = new Vector3(newFirePoint.x, newFirePoint.y, m_firePointZ);
            m_currentFirePoint = newFirePoint;
        }
    }


    // *** MonoBehaviour Interface

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_renderer = GetComponent<SpriteRenderer>();
        m_controller = GetComponentInParent<MegaManController>();
        m_rigidBodySelf = GetComponentInParent<Rigidbody2D>();

        foreach (Transform child in m_controller.transform)
        {
            if (!m_energyBuster)
            {
                m_energyBuster = child.GetComponent<EnergyBuster>();
            }
        }
        m_firePointZ = m_energyBuster.transform.position.z;
    }


    private void FixedUpdate()
    {
        UpdateAnimator();
    }


    private void UpdateAnimator()
    {
        // Handle shooting follow through
        if (m_shooting && Time.time - m_controller.ShootButtonReleaseTime > m_shootFollowThroughTime)
        {
            m_shooting = false;
        }

        bool animatorChanged = false;

        // Animator speed adjustment when on a ladder - if he stops climbing, he should stop animating
        float animatorSpeed = 1;
        if (m_controller.state == MegaManStates.Climbing)
        {
            // Manipulate animation speed based on MegaMan's actual speed
            if (Mathf.Abs(m_rigidBodySelf.velocity.y) > 0)
            {
                animatorSpeed = Mathf.Min(Mathf.Abs(m_rigidBodySelf.velocity.y) / m_controller.LadderClimbSpeed, 1);
            }
            else
            {
                animatorSpeed = 0;
            }
        }
        if (m_lastSpeed != animatorSpeed)
        {
            m_animator.speed = animatorSpeed;
            // Animator speed change does not constitute 'animatorChanged'
        }

        m_currentState = (int)m_controller.state / 10;
        if (m_lastState != m_currentState)
        {
            m_animator.SetInteger("MegaManState", m_currentState);
            m_lastState = m_currentState;
            animatorChanged = true;
        }
        if (m_runningPrev != m_running)
        {
            m_animator.SetBool("Running", m_running);
            m_runningPrev = m_running;
            animatorChanged = true;
        }
        if (m_ladderTopTransitioningPrev != m_ladderTopTransitioning)
        {
            m_animator.SetBool("ClimbTransition", m_ladderTopTransitioning);
            m_ladderTopTransitioningPrev = m_ladderTopTransitioning;
            animatorChanged = true;
        }
        if (m_shootingPrev != m_shooting)
        {
            m_animator.SetBool("Shooting", m_shooting);
            m_shootingPrev = m_shooting;
            animatorChanged = true;
        }
        if (animatorChanged)
        {
            m_animator.SetTrigger("Changed");
        }
        if (m_visiblePrev != m_controller.Visible)
        {
            m_visiblePrev = !m_visiblePrev;
            m_renderer.enabled = m_visiblePrev;
        }
    }
}
