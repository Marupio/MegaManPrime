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
    [SerializeField] [Range(0, 0.6f)] public float m_analogueXAxisDeadZone = 0.1f;
    [Tooltip("Dead zone for analogue stick controllers")]
    [SerializeField] [Range(0, 0.6f)] public float m_analogueYAxisDeadZone = 0.1f;


    [Header("Movement Colliders")]

    [Tooltip("Movement collider for MegaMan when standing / moving upright")]
    [SerializeField] private Collider2D m_uprightCollider;
    [Tooltip("Movement collider for MegaMan when sliding")]
    [SerializeField] private Collider2D m_slidingCollider;


    [Header("Ladder Colliders")]

    [Tooltip("A mask determining what is a ladder to MegaMan")]
    [SerializeField] private LayerMask m_whatAreLadders;
    [Tooltip("Collider for grabbing a ladder when MegaMan is standing / moving upright")]
    [SerializeField] private Collider2D m_uprightLadderCollider;
    [Tooltip("Collider for grabbing a ladder when MegaMan is standing / moving upright")]
    [SerializeField] private Collider2D m_slidingLadderCollider;
    [Tooltip("Collider climbing up - when to start transition animation at the top")]
    [SerializeField] private Collider2D m_ladderTransitionStartCollider;
    [Tooltip("Collider climbing up - when to complete transition animation at the top")]
    [SerializeField] private Collider2D m_ladderTransitionEndCollider;


    [Header("Collision Check Colliders")]

    [Tooltip("A mask determining what is ground to MegaMan")]
    [SerializeField] private LayerMask m_whatIsGround;
    [Tooltip("Collider for checking when MegaMan is on the ground")]
    [SerializeField] private Collider2D m_groundCheckCollider;
    [Tooltip("Collider for checking when MegaMan hits a ceiling while upright")]
    [SerializeField] private Collider2D m_uprightCeilingCheckCollider;
    [Tooltip("Collider to check if MegaMan bounces against a wall while sliding")]
    [SerializeField] private Collider2D m_slidingBounceCollider;
    [Tooltip("Collider for checking if MegaMan can stand up while sliding")]
    [SerializeField] private Collider2D m_slidingCeilingCheckCollider;


    [Header("Physics Settings")]

    [SerializeField] private float m_runSpeed = 10.0f;
    [SerializeField] private float m_dashSpeed = 15.0f;
    [SerializeField] private float m_slideSpeed = 15.0f;
    [SerializeField] private float m_slideDistance = 4.0f;
    [SerializeField] private float m_jumpSpeed = 15.0f;
    [SerializeField] private float m_jumpHeight = 7.0f;
    [Tooltip("How much time does MegaMan keep his weapon out after shooting")]
    [SerializeField] [Range(0, 1)] private float m_shootFollowThroughTime = 0.5f;
    [Tooltip("How much control can MegaMan have in the air? 0=none, 1=full")]
    [SerializeField] private float m_airSteerAccelerationFactor = 0.8f;
    [Tooltip("Smooth out MegaMan's movement - 0=jerky")]
    [SerializeField] [Range(0,0.3f)] public float m_movementSmoothing = 0.05f;
    [Tooltip("How long does MegaMan recoil from being hurt")]
    [SerializeField] [Range(0.1f, 1)] private float m_hurtDuration;
    [Tooltip("How fast does MegaMan recoil when hurt")]
    [SerializeField] private float m_hurtSpeed;

    public enum MegaManStates
    {
        Normal,         // Grounded, standing or running
        Jumping,        // Going up, jump button held down
        Falling,        // Going downwards - out of jump steam, or jump button released, or fell off something
        Dashing,        // Grounded dash movement, left or right
        DashJumping,    // Jumping while dashing, jump button held down
        DashFalling,    // Falling while dashing, no more going up
        Climbing,       // On a ladder
        Sliding,        // Sliding movement, left or right
        Hurt            // Recoil from an attack
    }
    public MegaManStates state = MegaManStates.Normal;

// States switch template
//        switch (state)
//        {
//            case MegaManStates.Normal:
//            case MegaManStates.Jumping:
//            case MegaManStates.Falling:
//            case MegaManStates.Dashing:
//            case MegaManStates.DashJumping:
//            case MegaManStates.DashFalling:
//            case MegaManStates.Climbing:
//            case MegaManStates.Sliding:
//            case MegaManStates.Hurt:
//            default:
//        }


    // Physics

    private Vector2 m_velocity = Vector2.zero;
    private float m_maxJumpTime;
    private float m_maxSlideTime;
    private float m_slideStartTime = -1; // Different from m_jumpButtonPressTime because we can press jump button multiple times during a slide
    private float m_hurtStartTime = -1;
    private bool m_facingRight = true;
    private bool m_grounded = true;
    private bool m_running = false;
    private bool m_shooting = false;
    private bool m_ladderTopTransitioning = false;  // When at the top of a ladder, halfway to transition to standing


    // Inputs

    private bool m_jumpButtonPressed = false;           // True when a jumpButtonPress event occured
    private bool m_jumpButtonReleased = false;          // True when a jumpButtonRelease event occured
    private bool m_jumpButton = false;                  // Actual position of jumpButton
    private float m_jumpButtonPressTime = -1;           // Time jump button was last pressed, negative when button is not down
    private bool m_shootButtonPressed = false;          // True when a shootButtonPress event occured
    private bool m_shootButtonReleased = false;         // True when a shootButtonRelease event occured
    private bool m_shootButton = false;                 // Actual position of shootButton
    private float m_shootButtonPressTime = -1;          // Time shoot button was last pressed, negative when button is not down
    private float m_shootButtonReleaseTime = -1;
    private Vector2 m_controlVector;                    // User-input movement vector, converted into -1 | 0 | 1 on both axes

    /// <summary>
    /// Actual position of input stick used in part to determine character movement
    /// Note - Diagonal positions give values like (0.7,0.7), but this should be taken to mean (right, up)
    /// Analogue stick inputs can give things like (0.3, 0.1), so we need a threshold (dead space)
    /// </summary>
    private Vector2 m_stickPosition = Vector2.zero;
    private ContactFilter2D m_ladderFilter;
    private ContactFilter2D m_groundFilter;


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
        m_ladderFilter = new ContactFilter2D();
        m_ladderFilter.SetLayerMask(m_whatAreLadders);
        m_groundFilter = new ContactFilter2D();
        m_groundFilter.SetLayerMask(m_whatIsGround);
        m_maxJumpTime = m_jumpSpeed <= 0 ? 0 : m_jumpHeight / m_jumpSpeed;
        m_maxSlideTime = m_slideSpeed <= 0 ? 0 : m_slideDistance / m_slideSpeed;
    }

    private void FixedUpdate()
    {
        UpdateControlVector();
        CollisionAndStateChecking();
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
        m_jumpButtonPressTime = Time.time;
    }

    public void JumpButtonReleased()
    {
        m_jumpButtonReleased = true;
        m_jumpButton = false;
        m_jumpButtonPressTime = -1;
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

    private void CollisionAndStateChecking()
    {
        // All states check for grounded
        m_grounded = CollisionCheck(m_groundCheckCollider, m_groundFilter);

        // State-specific checks
        switch (state)
        {
            case MegaManStates.Normal:
                if (!m_grounded)
                {
                    state = MegaManStates.Falling;
                }
                LadderCheck(m_uprightLadderCollider);
                break;
            case MegaManStates.Jumping:
                if (m_grounded)
                {
                    // landed
                    state = MegaManStates.Normal;
                }
                else
                {
                    if (Time.time - m_jumpButtonPressTime >= m_maxJumpTime || CollisionCheck(m_uprightCeilingCheckCollider, m_groundFilter))
                    {
                        // Hit the ceiling or jumping ran out of steam
                        state = MegaManStates.Falling;
                    }
                }
                LadderCheck(m_uprightLadderCollider);
                break;
            case MegaManStates.Falling:
                if (m_grounded)
                {
                    // landed
                    state = MegaManStates.Normal;
                }
                LadderCheck(m_uprightLadderCollider);
                break;
            case MegaManStates.Dashing:
                if (!m_grounded)
                {
                    state = MegaManStates.DashFalling;
                }
                LadderCheck(m_uprightLadderCollider);
                break;
            case MegaManStates.DashJumping:
                if (Time.time - m_jumpButtonPressTime >= m_maxJumpTime || CollisionCheck(m_uprightCeilingCheckCollider, m_groundFilter))
                {
                    // Hit the ceiling or jumping ran out of steam
                    state = MegaManStates.DashFalling;
                }
                LadderCheck(m_uprightLadderCollider);
                break;
            case MegaManStates.DashFalling:
                // Upright ladder check
                LadderCheck(m_uprightLadderCollider);
                break;
            case MegaManStates.Climbing:
                m_ladderTopTransitioning = false;
                if (m_controlVector.y < 0)
                {
                    // Climbing down
                    if (m_grounded)
                    {
                        // Step off ladder
                        state = MegaManStates.Normal;
                    }
                }
                if (!CollisionCheck(m_uprightLadderCollider, m_ladderFilter))
                {
                    // Not holding on to a ladder anymore
                    state = MegaManStates.Falling;
                }
                else
                {
                    bool transitionStart = CollisionCheck(m_ladderTransitionStartCollider, m_ladderFilter);
                    bool transitionEnd = CollisionCheck(m_ladderTransitionEndCollider, m_ladderFilter);
                    if (!transitionStart)
                    {
                        if (!transitionEnd)
                        {
                            // Reached top, stand up (must adjust collider to ensure standing has correct y value)
                            state = MegaManStates.Normal;
                        }
                        else
                        {
                            // Almost at top
                            m_ladderTopTransitioning = true;
                        }
                    }
                }
                break;
            case MegaManStates.Sliding:
                // Sliding bounce check
                if (CollisionCheck(m_slidingBounceCollider, m_groundFilter))
                {
                    BounceX();
                }
                // Sliding ladder check
                if (LadderCheck(m_slidingLadderCollider))
                {
                    // Grabbed a ladder, now in a different state
                    break;
                }

                if (Time.time - m_slideStartTime >= m_maxSlideTime)
                {
                    // Ready to finish sliding - check if he can stand up
                    if (!CollisionCheck(m_slidingCeilingCheckCollider, m_groundFilter))
                    {
                        // It is clear, stand up
                        state = MegaManStates.Normal;
                        m_slideStartTime = -1;
                    }
                }
                break;
            case MegaManStates.Hurt:
                if (Time.time - m_hurtStartTime >= m_hurtDuration)
                {
                    // Done recoiling
                    if (m_grounded)
                    {
                        state = MegaManStates.Normal;
                    }
                    else
                    {
                        state = MegaManStates.Falling;
                    }
                }
                break;
            default:
                Debug.LogError("Unhandled MegaMan state" + state);
                break;
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
        // Calculate target speed
        float targetSpeed = m_controlVector.x * m_runSpeed;

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
        if (m_controlVector.x < 0 && m_facingRight)
        {
            Flip();
        }
        else if (m_controlVector.x > 0 && !m_facingRight)
        {
            Flip();
        }
    }

    void BounceX()
    {
        m_velocity.x = -m_velocity.x;
        Flip();
    }

    void BounceY()
    {
        m_velocity.y = -m_velocity.y;
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

// States switch template
        
        switch (state)
        {
            case MegaManStates.Normal:
                // Standing / StandShoot / StandThrow / Running / RunShoot / RunThrow
            case MegaManStates.Jumping:
            case MegaManStates.Falling:
            case MegaManStates.DashJumping:
            case MegaManStates.DashFalling:
                // Set to jumping
            case MegaManStates.Dashing:
                // Set to dashing
            case MegaManStates.Climbing:
            case MegaManStates.Sliding:
            case MegaManStates.Hurt:
            default:
                break;
        }
    }

    /// <summary>
    /// Turns analogue inputs into 1 | 0 | -1
    /// </summary>
    /// <returns>controlVec that can only contain 1s, 0s, or -1s</returns>
    void UpdateControlVector()
    {
        m_controlVector = Vector2.zero;
        if (m_stickPosition.x > m_analogueXAxisDeadZone)
        {
            m_controlVector.x = 1;
        }
        else if (m_stickPosition.x < -m_analogueXAxisDeadZone)
        {
            m_controlVector.x = -1;
        }

        if (m_stickPosition.y > m_analogueYAxisDeadZone)
        {
            m_controlVector.y = 1;
        }
        else if (m_stickPosition.y < -m_analogueYAxisDeadZone)
        {
            m_controlVector.y = -1;
        }
    }

    bool CollisionCheck(Collider2D checkCollider, ContactFilter2D filter)
    {
        List<Collider2D> candidates = new List<Collider2D>(0);
        Physics2D.OverlapCollider(checkCollider, filter, candidates);
        return candidates.Count > 0;
    }

    /// <summary>
    /// Checks if MegaMan grabs a ladder, if so, changes state, adjusts position as required
    /// </summary>
    /// <returns>true if he did</returns>
    bool LadderCheck(Collider2D ladderCheckCollider)
    {
        if (Mathf.Abs(m_controlVector.y) > Mathf.Epsilon)
        {
            List<Collider2D> candidateLadder = new List<Collider2D>(0);
            Physics2D.OverlapCollider(ladderCheckCollider, m_ladderFilter, candidateLadder);
            bool foundLadder = false;
            float closestMagY = float.MaxValue;
            Collider2D closestLadder = new Collider2D();
            foreach (Collider2D ladderI in candidateLadder)
            {
                float dist = Mathf.Abs(ladderI.transform.position.y - m_rigidBodySelf.transform.position.y);
                if (dist < closestMagY)
                {
                    closestMagY = dist;
                    closestLadder = ladderI;
                    foundLadder = true;
                }
            }
            if (foundLadder)
            {
                MountLadder(closestLadder);
            }
            return foundLadder;
        }
        return false;
    }

    void MountLadder(Collider2D ladder)
    {
        Vector2 oldPosition = m_rigidBodySelf.transform.position;
        Vector2 ladderPosition = ladder.transform.position;
        Vector2 newPosition = new Vector2(ladderPosition.x, 0.5f*(oldPosition.y + ladderPosition.y));
        state = MegaManStates.Climbing;
        // For ladders, if MegaMan's y position is above the ladder's tile, then the animation needs to be the topping style

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;
    
        transform.Rotate(0f, 180f, 0f);
    }
}
