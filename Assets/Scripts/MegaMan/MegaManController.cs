using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MegaManController: MonoBehaviour
{
    private Rigidbody2D m_rigidBodySelf;
    private Animator m_animator;
    private EnergyBuster m_energyBuster;
    private LadderHandler m_ladderHandler;


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


    [Header("Ladder Detectors")]
    [Tooltip("Detector for grabbing a ladder that is under MegaMan's feet")]
    [SerializeField] private Collider2D m_groundLadderDetector;
    [Tooltip("Detector for grabbing a ladder when MegaMan is standing / moving upright")]
    [SerializeField] private Collider2D m_uprightLadderDetector;
    [Tooltip("Detector for grabbing a ladder when MegaMan is standing / moving upright")]
    [SerializeField] private Collider2D m_slidingLadderDetector;
    [Tooltip("Detector climbing up - when to start transition animation at the top")]
    [SerializeField] private Collider2D m_ladderTransitionStartDetector;
    [Tooltip("Detector climbing up - when to complete transition animation at the top")]
    [SerializeField] private Collider2D m_ladderTransitionEndDetector;


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
    [SerializeField] private float m_dashDistance = 4.0f;
    [SerializeField] private float m_slideSpeed = 15.0f;
    [SerializeField] private float m_slideDistance = 4.0f;
    [SerializeField] private float m_jumpSpeed = 15.0f;
    [SerializeField] private float m_jumpHeight = 7.0f;
    [SerializeField] private float m_ladderClimbSpeed = 7.0f;
    [Tooltip("How much time does MegaMan keep his weapon out after shooting")]
    [SerializeField] [Range(0, 1)] private float m_shootFollowThroughTime = 0.5f;
    [Tooltip("How much control can MegaMan have in the air? 0=none, 1=full")]
    [SerializeField] [Range(0, 1)] private float m_jumpAirSteerAccelerationFactor = 0.8f;
    [Tooltip("How much control can MegaMan have in the air? 0=none, 1=full")]
    [SerializeField] [Range(0, 1)] private float m_dashJumpAirSteerAccelerationFactor = 0.2f;
    [Tooltip("Smooth out MegaMan's movement - 0=jerky")]
    [SerializeField] [Range(0,0.3f)] public float m_movementSmoothing = 0.05f;
    [Tooltip("How long does MegaMan recoil from being hurt")]
    [SerializeField] [Range(0.1f, 5)] private float m_hurtDuration;
    [Tooltip("How fast does MegaMan recoil when hurt")]
    [SerializeField] private float m_hurtSpeed;


    /// <subject>
    /// State, which affects the current laws of physics to which MegaMan is subject.  Associated integer value is for the animator.
    /// The tens column indicates a different animator state.
    /// </subject>
    public enum MegaManStates
    {
        // When editting these, animator may need to be modified
        Normal      = 0,    // Grounded, standing or running
        Jumping     = 10,   // Going up, jump button held down
        Falling     = 11,   // Going downwards - out of jump steam, or jump button released, or fell off something
        Dashing     = 20,   // Grounded dash movement, left or right
        DashJumping = 21,   // Jumping while dashing, jump button held down
        DashFalling = 22,   // Falling while dashing, no more going up
        Climbing    = 30,   // On a ladder
        Sliding     = 40,   // Sliding movement, left or right
        Hurt        = 50    // Recoil from an attack
    }
    public MegaManStates state = MegaManStates.Normal;


    // Animator Values

    private int m_animatorLastState = -1;
    private int m_animatorLastSpeed = -1;
    private bool m_animatorRunning = false;
    private bool m_animatorRunningPrev = false;
    private bool m_animatorShooting = false;
    private bool m_animatorShootingPrev = false;
    private bool m_animatorLadderTopTransitioning = false;  // When at the top of a ladder, halfway to transition to standing
    private bool m_animatorLadderTopTransitioningPrev = false;  // When at the top of a ladder, halfway to transition to standing


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

    private Vector2 m_acceleration = Vector2.zero;
    private bool m_canMove = true;
    private bool m_canShoot = true;
    private bool m_canFlip = true;
    private float m_maxJumpTime;
    private float m_maxSlideTime;
    private float m_slideStartTime = -1; // Different from m_jumpButtonPressTime because we can press jump button multiple times during a slide
    private float m_maxDashTime;
    private float m_dashStartTime = -1;
    private float m_hurtStartTime = -1;
    private bool m_facingRight = true;
    private bool m_grounded = true;
    

    // Inputs

    private bool m_jumpButtonPressed = false;           // True when a jumpButtonPress event occured
    private bool m_jumpButtonReleased = false;          // True when a jumpButtonRelease event occured
    private bool m_jumpButton = false;                  // Actual position of jumpButton
    private float m_gravity = 1;                        // Gravity multiplier in y axis - positive 1 = gravity pointing down
    private float m_gravityDefault;
    private float m_jumpButtonPressTime = -1;           // Time jump button was last pressed, negative when button is not down
    private bool m_shootButtonPressed = false;          // True when a shootButtonPress event occured
    private bool m_shootButtonReleased = false;         // True when a shootButtonRelease event occured
    private bool m_shootButton = false;                 // Actual position of shootButton
    private float m_shootButtonPressTime = -1;          // Time shoot button was last pressed, negative when button is not down
    private float m_shootButtonReleaseTime = -1;        // Time shoot button was last released
    private bool m_dashButtonPressed = false;           // True when a dashButtonPress event occured
    private float m_dashButtonPressTime = -1;           // Time dash button was last pressed
    private Vector2 m_controlVector = Vector2.zero;     // User-input movement vector, converted into -1 | 0 | 1 on both axes

    /// <summary>
    /// Actual position of input stick used in part to determine character movement
    /// Note - Diagonal positions give values like (0.7,0.7), but this should be taken to mean (right, up)
    /// Analogue stick inputs can give things like (0.3, 0.1), so we need a threshold (dead space)
    /// </summary>
    private Vector2 m_stickPosition = Vector2.zero;
    private ContactFilter2D m_ladderFilter;
    private ContactFilter2D m_groundFilter;

    Vector3 midPt;
    Vector3 closePt;
    Vector3 box0;
    Vector3 box1;
    Vector3 box2;
    Vector3 box3;


    // Mono behaviour interface

    private void Awake()
    {
        m_rigidBodySelf = GetComponent<Rigidbody2D>();
        m_ladderHandler = GameObject.FindObjectOfType<LadderHandler>();
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
        m_groundFilter = new ContactFilter2D();
        m_groundFilter.SetLayerMask(m_whatIsGround);
        m_maxJumpTime = m_jumpSpeed <= 0 ? 0 : m_jumpHeight / m_jumpSpeed;
        m_maxSlideTime = m_slideSpeed <= 0 ? 0 : m_slideDistance / m_slideSpeed;
        m_maxDashTime = m_dashSpeed <= 0 ? 0 : m_dashDistance / m_dashSpeed;
        m_gravityDefault = m_rigidBodySelf.gravityScale;
    }

    private void FixedUpdate()
    {
        UpdateControlVector();
        CollisionAndStateChecking();
        Move();
        UpdateAnimator();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(box0, box1);
        Gizmos.DrawLine(box1, box2);
        Gizmos.DrawLine(box2, box3);
        Gizmos.DrawLine(box3, box0);
        Gizmos.DrawLine(midPt, closePt);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("Entered " + other.name + " at " + other.transform.position);
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
        if (m_canShoot)
        {
            m_shootButtonPressed = true;
            m_shootButton = true;
            m_shootButtonPressTime = Time.time;
        }
    }

    public void ShootButtonReleased()
    {
        // Ignore shootButton release events if not pressed (maybe !m_canShoot)
        if (m_shootButton)
        {
            m_shootButtonReleased = true;
            m_shootButton = false;
            m_shootButtonReleaseTime = Time.time;
            float chargeTime = m_shootButtonReleaseTime - m_shootButtonPressTime;
            m_shootButtonPressTime = -1;
            m_energyBuster.Shoot(chargeTime);
            m_animatorShooting = true;
        }
    }

    public void DashButtonPressed()
    {
        m_dashButtonPressed = true;
        m_dashButtonPressTime = Time.time;
    }

    /// <summary>
    /// Performs all collider checks, many state transitions:
    /// * Sets m_grounded flag
    /// * Jump / slide / dash start
    /// * Jump / slide / dash end
    /// * Ladder grabbing, ladder topping, falling from ladders
    /// * End of recoil from hurt
    /// </summary>
    private void CollisionAndStateChecking()
    {
        // All states check for grounded
        m_grounded = CollisionCheck(m_groundCheckCollider, m_groundFilter);

        // Check if starting a jump, starting a slide, starting a dash
        if (m_jumpButtonPressed)
        {
            if (state == MegaManStates.Climbing)
            {
                // Jump off a ladder
                Debug.Log("Jumped off ladder");
                state = MegaManStates.Falling;
            }
            else if (m_grounded && m_canMove && state != MegaManStates.Sliding)
            {
                if (state == MegaManStates.Dashing)
                {
                    // Switch to DashJumping
                    state = MegaManStates.DashJumping;
                }
                else if (m_controlVector.y < 0)
                {
                    // Slide
                    state = MegaManStates.Sliding;
                    m_slideStartTime = m_jumpButtonPressTime;
                }
                else
                {
                    // Jump
                    state = MegaManStates.Jumping;
                }
            }
            m_jumpButtonPressed = false;
        }
        else if (m_dashButtonPressed && m_grounded & m_canMove && state != MegaManStates.Sliding)
        {
            state = MegaManStates.Dashing;
            m_dashStartTime = m_dashButtonPressTime;
        }
        m_dashButtonPressed = false;
        m_dashButtonPressTime = -1;

        // State-specific checks
        switch (state)
        {
            case MegaManStates.Normal:
                if (!m_grounded)
                {
                    state = MegaManStates.Falling;
                }
                LadderCheck(m_uprightLadderDetector);
                break;
            case MegaManStates.Jumping:
                if (m_jumpButtonReleased || Time.time - m_jumpButtonPressTime >= m_maxJumpTime || CollisionCheck(m_uprightCeilingCheckCollider, m_groundFilter))
                {
                    // Hit the ceiling or jumping ran out of steam
                    state = MegaManStates.Falling;
                }
                LadderCheck(m_uprightLadderDetector);
                break;
            case MegaManStates.Falling:
                if (m_grounded)
                {
                    // landed
                    state = MegaManStates.Normal;
                }
                LadderCheck(m_uprightLadderDetector);
                break;
            case MegaManStates.Dashing:
                if (!m_grounded)
                {
                    state = MegaManStates.DashFalling;
                }
                else
                {
                    if (Time.time - m_dashStartTime >= m_maxDashTime)
                    {
                        // Dashing is done
                        m_dashStartTime = -1;
                        state = MegaManStates.Normal;
                    }
                }
                LadderCheck(m_uprightLadderDetector);
                break;
            case MegaManStates.DashJumping:
                if (m_jumpButtonReleased || Time.time - m_jumpButtonPressTime >= m_maxJumpTime || CollisionCheck(m_uprightCeilingCheckCollider, m_groundFilter))
                {
                    // Hit the ceiling or jumping ran out of steam
                    state = MegaManStates.DashFalling;
                }
                LadderCheck(m_uprightLadderDetector);
                break;
            case MegaManStates.DashFalling:
                // Upright ladder check
                if (m_grounded)
                {
                    state = MegaManStates.Dashing;
                }
                LadderCheck(m_uprightLadderDetector);
                break;
            case MegaManStates.Climbing:
                m_animatorLadderTopTransitioning = false;
                if (m_controlVector.y < 0)
                {
                    // Climbing down
                    if (m_grounded)
                    {
                        // Step off ladder
                        Debug.Log("Stepped off ladder");
                        state = MegaManStates.Normal;
                    }
                }
                if (!m_ladderHandler.OnLadder(m_uprightLadderDetector))
                {
                    // Not holding on to a ladder anymore
                    Debug.Log("Climbed off ladder");
                    state = MegaManStates.Falling;
                }
                else
                {
                    bool transitionStart = m_ladderHandler.OnLadder(m_ladderTransitionStartDetector);
                    bool transitionEnd = m_ladderHandler.OnLadder(m_ladderTransitionEndDetector);
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
                            m_animatorLadderTopTransitioning = true;
                        }
                    }
                }
                break;
            case MegaManStates.Sliding:
                // Check if MegaMan wants to stop sliding
                float xDir = m_facingRight ? 1 : -1;
                if (xDir * m_controlVector.x < 0)
                {
                    // Abort slide if we can, or reverse direction
                    if (!CollisionCheck(m_slidingCeilingCheckCollider, m_groundFilter))
                    {
                        // It is clear, stand up
                        state = MegaManStates.Normal;
                        m_slideStartTime = -1;
                    }
                    else
                    {
                        BounceX();
                    }
                }
                // Sliding bounce check
                if (CollisionCheck(m_slidingBounceCollider, m_groundFilter))
                {
                    BounceX();
                }
                // Sliding ladder check
                if (LadderCheck(m_slidingLadderDetector))
                {
                    // Grabbed a ladder, now in a different state
                    m_slideStartTime = -1;
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
                Debug.LogError("Unhandled MegaMan state: " + state);
                break;
        }
        m_jumpButtonReleased = false;
    }


    /// <summary>
    /// Performs all x & y movement of MegaMan
    /// </summary>
    void Move()
    {
        // Reset to default
        m_slidingCollider.enabled = false;
        m_uprightCollider.enabled = true;
        m_canShoot = true;
        m_canMove = true;
        m_canFlip = true;
        m_rigidBodySelf.gravityScale = m_gravityDefault;
        switch (state)
        {
            case MegaManStates.Normal:
            {
                float xTargetSpeed = m_controlVector.x * m_runSpeed;
                if (Mathf.Abs(xTargetSpeed) > 0)
                {
                    m_animatorRunning = true;
                }
                else
                {
                    m_animatorRunning = false;
                }
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                break;
            }
            case MegaManStates.Jumping:
            {
                float xTargetSpeed = m_controlVector.x * m_runSpeed;
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_jumpAirSteerAccelerationFactor) + xTargetSpeed * m_jumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_jumpSpeed);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                break;
            }
            case MegaManStates.Falling:
            {
                float xTargetSpeed = m_controlVector.x * m_runSpeed;
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_jumpAirSteerAccelerationFactor) + xTargetSpeed * m_jumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                break;
            }
            case MegaManStates.Dashing:
            {
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = xDir * m_dashSpeed;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                m_canFlip = false;
                break;
            }
            case MegaManStates.DashJumping:
            {
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = xDir * m_dashSpeed;
                if (m_controlVector.x * xDir < 0)
                {
                        // MegaMan is trying to slow down
                        xTargetSpeed = m_controlVector.x * m_runSpeed;
                }
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_dashJumpAirSteerAccelerationFactor) + xTargetSpeed * m_dashJumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_jumpSpeed);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                m_canFlip = false;
                break;
            }
            case MegaManStates.DashFalling:
            {
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = xDir * m_dashSpeed;
                if (m_controlVector.x * xDir < 0)
                {
                        // MegaMan is trying to slow down
                        xTargetSpeed = m_controlVector.x * m_runSpeed;
                }
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_dashJumpAirSteerAccelerationFactor) + xTargetSpeed * m_dashJumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                m_canFlip = false;
                break;
            }
            case MegaManStates.Climbing:
            {
                float yTargetSpeed = m_controlVector.y * m_ladderClimbSpeed;
                Vector2 targetVelocity = new Vector2(0, yTargetSpeed);
                m_rigidBodySelf.gravityScale = 0;
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                m_canFlip = false;
                break;
            }
            case MegaManStates.Sliding:
            {
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = xDir * m_slideSpeed;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);

                m_canShoot = false;
                m_slidingCollider.enabled = true;
                m_uprightCollider.enabled = false;
                m_canFlip = false;
                break;
            }
            case MegaManStates.Hurt:
            {
                // Recoil in opposite direction from the way we are facing
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = -xDir * m_hurtSpeed;
                m_rigidBodySelf.gravityScale = 0;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, 0);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);

                m_canMove = false;
                m_canShoot = false;
                m_canFlip = false;
                break;
            }
            default:
            {
                Debug.LogError("Unhandled MegaMan state: " + state);
                break;
            }
        }
        if (m_canMove && m_canFlip)
        {
            if (m_controlVector.x < 0 && m_facingRight)
            {
                Flip();
            }
            else if (m_controlVector.x > 0 && !m_facingRight)
            {
                Flip();
            }
        }
    }


    /// <summary>
    /// MegaMan bounces in the x-direction
    /// </summary>
    void BounceX()
    {
        m_acceleration.x = -m_acceleration.x;
        Flip();
    }

    /// <summary>
    /// MegaMan bounces in the y-direction
    /// </summary>
    void BounceY()
    {
        m_acceleration.y = -m_acceleration.y;
    }


    private void UpdateAnimator()
    {
        // Handle shooting follow through
        if (m_animatorShooting && Time.time - m_shootButtonReleaseTime > m_shootFollowThroughTime)
        {
            m_animatorShooting = false;
        }

        bool animatorChanged = false;

        // Animator speed adjustment when on a ladder - if he stops climbing, he should stop animating
        float animatorSpeed = 1;
        if (state == MegaManStates.Climbing)
        {
            // Manipulate animation speed based on MegaMan's actual speed
            if (Mathf.Abs(m_rigidBodySelf.velocity.y) > 0)
            {
                animatorSpeed = Mathf.Min(Mathf.Abs(m_rigidBodySelf.velocity.y) / m_ladderClimbSpeed, 1);
            }
            else
            {
                animatorSpeed = 0;
            }
        }
        if (m_animatorLastSpeed != animatorSpeed)
        {
            m_animator.speed = animatorSpeed;
            // Animator speed change does not constitute 'animatorChanged'
        }

        int currentState = (int)state / 10;
        if (m_animatorLastState != currentState)
        {
            m_animator.SetInteger("MegaManState", currentState);
            m_animatorLastState = currentState;
            animatorChanged = true;
        }
        if (m_animatorRunningPrev != m_animatorRunning)
        {
            m_animator.SetBool("Running", m_animatorRunning);
            m_animatorRunningPrev = m_animatorRunning;
            animatorChanged = true;
        }
        if (m_animatorLadderTopTransitioningPrev != m_animatorLadderTopTransitioning)
        {
            m_animator.SetBool("ClimbTransition", m_animatorLadderTopTransitioning);
            m_animatorLadderTopTransitioningPrev = m_animatorLadderTopTransitioning;
            animatorChanged = true;
        }
        if (m_animatorShootingPrev != m_animatorShooting)
        {
            m_animator.SetBool("Shooting", m_animatorShooting);
            m_animatorShootingPrev = m_animatorShooting;
            animatorChanged = true;
        }
        if (animatorChanged)
        {
            m_animator.SetTrigger("Changed");
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
    bool LadderCheck(Collider2D ladderDetector)
    {
        if (Mathf.Abs(m_controlVector.y) > Mathf.Epsilon)
        {
            bool foundLadder = MountLadder(ladderDetector);
            if (foundLadder)
            {
                return true;
            }
            if (m_controlVector.y < 0)
            {
                // Check for ladder underneath if pressing down
                return MountLadder(m_groundLadderDetector);
            }
        }
        return false;
    }

    bool MountLadder(Collider2D ladderDetector)
    {
        Vector2 ladderCentre = new Vector2();
        bool foundLadder = m_ladderHandler.ClosestLadder(ladderDetector, ref ladderCentre);
        if (!foundLadder)
        {
            return false;
        }
        Vector2 newPosition = new Vector2(ladderCentre.x, gameObject.transform.position.y);
        Debug.Log("Mounting ladder at " + ladderCentre);
        gameObject.transform.position = newPosition;
        m_rigidBodySelf.velocity = Vector2.zero;
        state = MegaManStates.Climbing;
        return true;
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;
    
        transform.Rotate(0f, 180f, 0f);
    }
}
