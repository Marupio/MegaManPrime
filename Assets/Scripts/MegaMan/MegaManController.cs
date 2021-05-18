using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MegaManController: MonoBehaviour
{
    private Rigidbody2D m_rigidBodySelf;
    private Animator m_animator;
    private EnergyBuster m_energyBuster;

    [Header("Control Settings")]
    [Tooltip("Dead zone for analogue stick controllers")]
    [SerializeField][Range(0, 0.6f)] public float m_analogueXAxisDeadZone = 0.1f;
    [Tooltip("Dead zone for analogue stick controllers")]
    [SerializeField][Range(0, 0.6f)] public float m_analogueYAxisDeadZone = 0.1f;

    [Header("Environment Interactions")]
    [Tooltip("A mask determining what is ground to MegaMan")]
    [SerializeField] private LayerMask m_whatIsGround;
    [Tooltip("A position marking where to check if MegaMan is grounded")]
    [SerializeField] private Transform m_groundCheck;
    [Tooltip("Radius of the ground check circle")]
    [SerializeField] private float m_groundedRadius;
    [Tooltip("A position marking where to check for ceilings")]
    [SerializeField] private Transform m_ceilingCheck;

    [Header("Physics Settings")]
    [SerializeField] private float m_runSpeed = 10.0f;
    [SerializeField] private float m_chargeSpeed = 15.0f;
    [SerializeField] private float m_slideSpeed = 15.0f;
    [SerializeField] private float m_jumpHeight = 7.0f;
    [SerializeField] private float m_shootFollowThroughTime = 0.5f;
    [Tooltip("How much control can MegaMan have in the air? 0=none, 1=full")]
    [SerializeField] private float m_airSteerAccelerationFactor = 0.8f;
    [Tooltip("Smooth out MegaMan's movement - 0=jerky")]
    [SerializeField][Range(0,0.3f)] public float m_movementSmoothing = 0.05f;

    // Physics

    private Vector2 m_velocity = Vector2.zero;
    private bool m_facingRight = true;
    private bool m_grounded = true;
    private bool m_running = false;
    private bool m_shooting = false;


    // Inputs

    private bool m_jumpButtonPressed = false;           // True when a jumpButtonPress event occured
    private bool m_jumpButtonReleased = false;          // True when a jumpButtonRelease event occured
    private bool m_jumpButton = false;                  // Actual position of jumpButton
    private bool m_shootButtonPressed = false;          // True when a shootButtonPress event occured
    private bool m_shootButtonReleased = false;         // True when a shootButtonRelease event occured
    private bool m_shootButton = false;                 // Actual position of shootButton
    private float m_shootButtonPressTime = -1;          // Time shoot button was last pressed, negative when button is not down
    private float m_shootButtonReleaseTime = -1;

    /// <summary>
    /// Actual position of input stick used in part to determine character movement
    /// Note - Diagonal positions give values like (0.7,0.7), but this should be taken to mean (right, up)
    /// Analogue stick inputs can give things like (0.3, 0.1), so we need a threshold (dead space)
    /// </summary>
    private Vector2 m_stickPosition = Vector2.zero;


    // Mono behaviour interface

    private void Awake()
    {
        m_rigidBodySelf = GetComponent<Rigidbody2D>();
        foreach (Transform child in this.transform)
        {
            if (!m_animator)
            {
                m_animator = child.GetComponent<Animator>();
            }
            if (!m_energyBuster)
            {
                m_energyBuster = child.GetComponent<EnergyBuster>();
            }
        }

    }

    private void FixedUpdate()
    {
        CollisionChecking();
        Move();
        UpdateAnimator();
    }


    // Accepting control input

    public void SetStickPosition(Vector2 direction)
    {
        m_stickPosition = direction;
    }

    public void JumpButtonPressed()
    {
        m_jumpButtonPressed = true;
        m_jumpButton = true;
    }

    public void JumpButtonReleased()
    {
        m_jumpButtonReleased = true;
        m_jumpButton = false;
    }

    public void ShootButtonPressed()
    {
        m_shootButtonPressed = true;
        m_shootButton = true;
        m_shootButtonPressTime = Time.time;
    }

    public void ShootButtonReleased()
    {
        m_shootButtonReleased = true;
        m_shootButton = false;
        m_shootButtonReleaseTime = Time.time;
        float chargeTime = m_shootButtonReleaseTime - m_shootButtonPressTime;
        m_shootButtonPressTime = -1;
        m_energyBuster.Shoot(chargeTime);
        m_shooting = true;
    }

    private void CollisionChecking()
    {
        m_grounded = false;

        // Check for grounded
        // The player is grounded if a circle cast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, m_groundedRadius, m_whatIsGround);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_grounded = true;
                break;
            }
        }

        // Check for ceiling hit when jumping up
        // Check for sliding bounce
        // If sliding
        // Check for sliding bounce
        // If sliding is done
        // Check for clear to stop sliding
        // If up or down are pressed
        // Check for ladder
    }

    /// <summary>
    /// Handles movement of MegaMan
    /// Consumes button press/release events to determine actual button positions and trigger press/release events
    /// </summary>
    void Move()
    {
        // Convert stick movement into L/R + U/D - ones and zeroes
        Vector2 controlVector = GetMoveComponents();

        // Calculate target speed
        float targetSpeed = controlVector.x * m_runSpeed;

        if (m_grounded && targetSpeed != 0)
        {
            m_running = true;
        }
        else
        {
            m_running = false;
        }
        Vector2 targetVelocity = new Vector2(targetSpeed, m_rigidBodySelf.velocity.y);

        // And then smoothing it out and applying it to the character
        m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_velocity, m_movementSmoothing);
        if (controlVector.x < 0 && m_facingRight)
        {
            Flip();
        }
        else if (controlVector.x > 0 && !m_facingRight)
        {
            Flip();
        }
    }

    private void UpdateAnimator()
    {
        m_animator.SetBool("Running", m_running);
        m_animator.SetBool("Grounded", m_grounded);
        m_animator.SetBool("Shooting", m_shooting);
        if (m_shooting && Time.time - m_shootButtonReleaseTime > m_shootFollowThroughTime)
        {
            m_shooting = false;
        }
    }

    /// <summary>
    /// Turns analogue inputs into 1 | 0 | -1
    /// </summary>
    /// <returns>controlVec that can only contain 1s, 0s, or -1s</returns>
    Vector2 GetMoveComponents()
    {
        Vector2 controlVec = Vector2.zero;
        if (m_stickPosition.x > m_analogueXAxisDeadZone)
        {
            controlVec.x = 1;
        }
        else if (m_stickPosition.x < -m_analogueXAxisDeadZone)
        {
            controlVec.x = -1;
        }

        if (m_stickPosition.y > m_analogueYAxisDeadZone)
        {
            controlVec.y = 1;
        }
        else if (m_stickPosition.y < -m_analogueYAxisDeadZone)
        {
            controlVec.y = -1;
        }
        return controlVec;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;
    
        transform.Rotate(0f, 180f, 0f);
    }
}
