using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <subject>
/// State, which affects the current laws of physics to which MegaMan is subject.  Associated integer value is for the animator.
/// The tens column indicates a different animator state.
/// </subject>
public enum MegaManStates
{
    // When editting these, animator may need to be modified
    Normal = 0,         // Grounded, standing or running
    Jumping = 10,       // Going up, jump button held down
    Falling = 11,       // Going downwards - out of jump steam, or jump button released, or fell off something
    Dashing = 20,       // Grounded dash movement, left or right
    DashJumping = 21,   // Jumping while dashing, jump button held down
    DashFalling = 22,   // Falling while dashing, no more going up
    Climbing = 30,      // On a ladder
    Sliding = 40,       // Sliding movement, left or right
    Hurt = 50           // Recoil from an attack
}


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ILive))]
[RequireComponent(typeof(ISelfDestruct))]
public class MegaManController: MonoBehaviour, ILoyalty, IGetHurt, IDie
{
    // *** Private references

    private Rigidbody2D m_rigidBodySelf;
    private ILive m_health;
    private ISelfDestruct m_reaper;
    private MegaManAnimationDirector m_animationDirector;
    private EnergyBuster m_energyBuster;
    private LadderHandler m_ladderHandler;


    // *** Inspector settings

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

    [SerializeField] private float m_runSpeed = 5.0f;
    [SerializeField] private float m_dashSpeed = 12.0f;
    [SerializeField] private float m_dashDistance = 4.0f;
    [SerializeField] private float m_slideSpeed = 12.0f;
    [SerializeField] private float m_slideDistance = 4.0f;
    [SerializeField] private float m_jumpSpeed = 15.0f;
    [SerializeField] private float m_jumpHeight = 4.0f;
    [SerializeField] private float m_ladderClimbSpeed = 5.0f;
    [Tooltip("When mounting a ladder below his feet, MegaMan needs to adjust down this many y units")]
    [SerializeField] private float m_climbingDownYAdjust = 0.4f;
    [Tooltip("When mounting a ladder above his head, MegaMan needs to adjust up this many y units")]
    [SerializeField] private float m_climbingUpYAdjust = 0.4f;
    [Tooltip("How much time does MegaMan keep his weapon out after shooting")]
    [SerializeField] [Range(0, 1)] private float m_jumpAirSteerAccelerationFactor = 0.8f;
    [Tooltip("How much control can MegaMan have in the air? 0=none, 1=full")]
    [SerializeField] [Range(0, 1)] private float m_dashJumpAirSteerAccelerationFactor = 0.1f;
    [Tooltip("Smooth out MegaMan's movement - 0=jerky")]
    [SerializeField] [Range(0,0.3f)] public float m_movementSmoothing = 0.1f;
    [Tooltip("How long does MegaMan recoil from being hurt")]
    [SerializeField] [Range(0.1f, 5)] private float m_hurtDuration = 0.75f;
    [Tooltip("How fast does MegaMan recoil when hurt")]
    [SerializeField] private float m_hurtSpeed = 2;
    [Tooltip("How long is MegaMan invincible after recoiling from a hit?")]
    [SerializeField] [Range(0.1f, 5)] private float m_invincibleDuration = 0.75f;

    public MegaManStates state = MegaManStates.Normal;


    // *** Physics

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
    private float m_invincibleStartTime = -1;
    private bool m_invincible = false;
    private bool m_visible = true; // Toggles on and off when invincible
    private bool m_dead = false;
    private bool m_facingRight = true;
    private bool m_grounded = true;
    private ISurfaceModel m_surfaceModel;
    private float m_ladderXPosition = 0;
    

    // *** Inputs

    private bool m_jumpButtonPressed = false;           // True when a jumpButtonPress event occured
    private bool m_jumpButtonReleased = false;          // True when a jumpButtonRelease event occured
    private bool m_jumpButton = false;                  // Actual position of jumpButton
    //private float m_gravity = 1;                        // Gravity multiplier in y axis - positive 1 = gravity pointing down
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


    // *** MonoBehaviour interface

    private void Awake()
    {
        m_rigidBodySelf = GetComponent<Rigidbody2D>();
        m_health = GetComponent<ILive>();
        m_reaper = GetComponent<ISelfDestruct>();
        side = Team.GoodGuys;
        m_ladderHandler = GameObject.FindObjectOfType<LadderHandler>();
        foreach (Transform child in this.transform)
        {
            if (!m_animationDirector)
            {
                m_animationDirector = child.GetComponent<MegaManAnimationDirector>();
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

    private void Start()
    {
        m_reaper.IHaveFinalWords(this);
    }

    private void FixedUpdate()
    {
        UpdateControlVector();
        CollisionAndStateChecking();
        Move();
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


    // *** ILoyalty interface
    public Team side { get; set; }

    // *** IGetHurt interface
    public bool TakeDamage(Collision2D collision, int damage, ICanHit attacker)
    {
        return InternalTakeDamage(damage, attacker);
    }
    public bool TakeDamage(Collider2D otherCollider, int damage, ICanHit attacker)
    {
        return InternalTakeDamage(damage, attacker);
    }
    // *** IGetHurt interface internal helpers
    private bool InternalTakeDamage(int damage, ICanHit attacker)
    {
        if (m_invincible)
        {
            return false;
        }
        if (state == MegaManStates.Hurt)
        {
            // Cannot get hurt while hurt
            return false;
        }
        m_health.TakeDamage(damage);
        m_hurtStartTime = Time.time;
        state = MegaManStates.Hurt;
        return true;
    }


    // *** IDie interface
    public void Die()
    {
        m_dead = true;
        Debug.Log("MegaMan has died");
    }
    public bool Dying()
    {
        return m_dead;
    }
    public bool ReadyToDie()
    {
        return m_dead;
    }


    // *** Other public member functions

    // *** Access
    public float ShootButtonReleaseTime { get => m_shootButtonReleaseTime; set => m_shootButtonReleaseTime = value; }
    public float LadderClimbSpeed { get => m_ladderClimbSpeed; set => m_ladderClimbSpeed = value; }
    public float RunSpeed { get => m_runSpeed; set => m_runSpeed = value; }
    public float InvincibleDuration { get => m_invincibleDuration; set => m_invincibleDuration = value; }
    public bool Visible { get => m_visible; set => m_visible = value; }

    // public float GroundSpeed
    // {
    //     get
    //     {
    //         if (m_surfaceModel != null)
    //         {
    //             return m_rigidBodySelf.velocity.x - m_surfaceModel.WallVelocity.x;
    //         }
    //         else
    //         {
    //             return m_rigidBodySelf.velocity.x;
    //         }
    //     }
    // }


    public Collider2D GetUprightLadderDetector()
    {
        return m_uprightLadderDetector;
    }
    public Collider2D GetGroundLadderDetector()
    {
        return m_groundLadderDetector;
    }


    // *** Accepting control input
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
            ShootButtonReleaseTime = Time.time;
            float chargeTime = ShootButtonReleaseTime - m_shootButtonPressTime;
            m_shootButtonPressTime = -1;
            m_animationDirector.Shoot();
            m_energyBuster.Shoot(chargeTime);
        }
    }
    public void DashButtonPressed()
    {
        m_dashButtonPressed = true;
        m_dashButtonPressTime = Time.time;
    }


    // *** Other miscellaneous public functions

    /// <summary>
    /// MegaMan bounces in the x-direction
    /// </summary>
    public void BounceX()
    {
        m_acceleration.x = -m_acceleration.x;
        Flip();
    }

    /// <summary>
    /// MegaMan bounces in the y-direction
    /// </summary>
    public void BounceY()
    {
        m_acceleration.y = -m_acceleration.y;
    }


    // *** Private member functions

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
        m_surfaceModel = GroundCheck(m_groundCheckCollider, m_groundFilter);
        m_grounded = m_surfaceModel != null;

        // Check if starting a jump, starting a slide, starting a dash
        if (m_jumpButtonPressed)
        {
            if (state == MegaManStates.Climbing)
            {
                if (m_controlVector.y == 0)
                {
                    // Jump off a ladder
                    state = MegaManStates.Falling;
                }
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
                    if (m_surfaceModel == null || m_surfaceModel.Slidable)
                    {
                        // Slide
                        state = MegaManStates.Sliding;
                        m_slideStartTime = m_jumpButtonPressTime;
                    }
                    else
                    {
                        // MegaMan is trying to slide, but can't
                        // Don't do anything
                    }
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
                LadderCheck(m_uprightLadderDetector, true);
                break;
            case MegaManStates.Falling:
                if (m_grounded)
                {
                    // landed
                    state = MegaManStates.Normal;
                }
                LadderCheck(m_uprightLadderDetector, true);
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
                LadderCheck(m_uprightLadderDetector, true);
                break;
            case MegaManStates.DashFalling:
                // Upright ladder check
                if (m_grounded)
                {
                    state = MegaManStates.Dashing;
                }
                LadderCheck(m_uprightLadderDetector, true);
                break;
            case MegaManStates.Climbing:
                m_animationDirector.LadderTopTransitioning = false;
                if (m_controlVector.y < 0)
                {
                    // MegaMan is climbing down and grounded, but is there still a ladder below him?
                    if (m_ladderHandler.OnLadder(m_groundLadderDetector))
                    {
                        m_ladderHandler.OpenTrapDoors(m_groundLadderDetector);
                        // Keep trying to climb down
                        break;
                    }
                    if (m_grounded)
                    {
                        // No ladder below, step off ladder
                        state = MegaManStates.Normal;
                        break;
                    }
                }
                // Is he still holding a ladder?
                if (!m_ladderHandler.OnLadder(m_uprightLadderDetector))
                {
                    // Not holding on to a ladder anymore
                    state = MegaManStates.Falling;
                    break;
                }
                else
                {
                    if (m_controlVector.y > 0)
                    {
                        m_ladderHandler.OpenTrapDoors(m_uprightCeilingCheckCollider);
                    }
                    bool transitionStart = m_ladderHandler.OnLadder(m_ladderTransitionStartDetector);
                    bool transitionEnd = m_ladderHandler.OnLadder(m_ladderTransitionEndDetector);
                    if (!transitionStart)
                    {
                        if (!transitionEnd)
                        {
                            // Reached top, stand up (must adjust collider to ensure standing has correct y value)
                            state = MegaManStates.Normal;
                            // Tell the ladderHandler that we made it to the top, in case it needs to close a trapdoor
                            m_ladderHandler.ToppedLadder(m_groundLadderDetector);
                        }
                        else
                        {
                            // Almost at top
                            m_animationDirector.LadderTopTransitioning = true;
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
                    m_invincible = true;
                    m_invincibleStartTime = Time.time;
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
        if (m_invincible)
        {
            if (Time.time - m_invincibleStartTime > m_invincibleDuration)
            {
                // Invincible is over
                m_invincible = false;
                m_invincibleStartTime = -1;
                m_visible = true;
            }
            else
            {
                m_visible = !m_visible;
            }
        }
    }


    /// <summary>
    /// Performs all x & y movement of MegaMan
    /// </summary>
    private void Move()
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
                float speedLimitFactor = 1;
                float maxAccel = 100;
                float xGroundSpeed = 0;
                if (m_surfaceModel != null)
                {
                    speedLimitFactor = 1 - m_surfaceModel.Resistance;
                    maxAccel = m_surfaceModel.Mu * 100;
                    xGroundSpeed = m_surfaceModel.WallVelocity.x;
                }
                if (maxAccel == 100)
                {
                    maxAccel = Mathf.Infinity;
                }
                float xTargetSpeed = m_controlVector.x * RunSpeed * speedLimitFactor + xGroundSpeed;
                if (Mathf.Abs(xTargetSpeed - xGroundSpeed) > 0)
                {
                    m_animationDirector.Running = true;
                }
                else
                {
                    m_animationDirector.Running = false;
                }
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing, maxAccel);
                break;
            }
            case MegaManStates.Jumping:
            {
                float xTargetSpeed = m_controlVector.x * RunSpeed;
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_jumpAirSteerAccelerationFactor) + xTargetSpeed * m_jumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_jumpSpeed);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                break;
            }
            case MegaManStates.Falling:
            {
                float xTargetSpeed = m_controlVector.x * RunSpeed;
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_jumpAirSteerAccelerationFactor) + xTargetSpeed * m_jumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                break;
            }
            case MegaManStates.Dashing:
            {
                float speedLimitFactor = 1;
                float xGroundSpeed = 0;
                if (m_surfaceModel != null)
                {
                    speedLimitFactor = 1 - m_surfaceModel.Resistance;
                    xGroundSpeed = m_surfaceModel.WallVelocity.x;
                }
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = xDir * m_dashSpeed * speedLimitFactor + xGroundSpeed;
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
                        xTargetSpeed = m_controlVector.x * RunSpeed;
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
                        xTargetSpeed = m_controlVector.x * RunSpeed;
                }
                xTargetSpeed = m_rigidBodySelf.velocity.x * (1-m_dashJumpAirSteerAccelerationFactor) + xTargetSpeed * m_dashJumpAirSteerAccelerationFactor;
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                m_canFlip = false;
                break;
            }
            case MegaManStates.Climbing:
            {
                float yTargetSpeed = m_controlVector.y * LadderClimbSpeed;
                Vector2 targetVelocity = new Vector2(0, yTargetSpeed);
                m_rigidBodySelf.gravityScale = 0;
                m_rigidBodySelf.velocity = Vector2.SmoothDamp(m_rigidBodySelf.velocity, targetVelocity, ref m_acceleration, m_movementSmoothing);
                // Now ensure rigidBodyPhysics hasn't pushed MegaMan off the ladder
                Vector3 newPosition = m_rigidBodySelf.transform.position;
                newPosition.x = m_ladderXPosition;
                m_rigidBodySelf.transform.position = newPosition;
                break;
            }
            case MegaManStates.Sliding:
            {
                float speedLimitFactor = 1;
                float xGroundSpeed = 0;
                if (m_surfaceModel != null)
                {
                    speedLimitFactor = 1 - m_surfaceModel.Resistance;
                    xGroundSpeed = m_surfaceModel.WallVelocity.x;
                    if (!m_surfaceModel.Slidable)
                    {
                        // Full stop, halt slide
                        speedLimitFactor = 0;
                        m_slideStartTime = -1;
                        state = MegaManStates.Normal;
                        m_rigidBodySelf.velocity = Vector2.zero;
                    }
                }
                float xDir = m_facingRight ? 1 : -1;
                float xTargetSpeed = xDir * m_slideSpeed * speedLimitFactor + xGroundSpeed;
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
                Vector2 targetVelocity = new Vector2(xTargetSpeed, m_rigidBodySelf.velocity.y);
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
    /// Turns analogue inputs into 1 | 0 | -1
    /// </summary>
    /// <returns>controlVec that can only contain 1s, 0s, or -1s</returns>
    private void UpdateControlVector()
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


    /// <summary>
    /// Checks for the presence of a surface model under MegaMan's feet
    /// </summary>
    /// <param name="checkCollider">Which collider to use to check for a surface model</param>
    /// <param name="filter">Apply this filter to the results</param>
    /// <returns>Any surface model that may have been found, or null for none</returns>
    private ISurfaceModel GroundCheck(Collider2D checkCollider, ContactFilter2D filter)
    {
        List<Collider2D> candidates = new List<Collider2D>(0);
        Physics2D.OverlapCollider(checkCollider, filter, candidates);
        foreach(Collider2D col in candidates)
        {
            ISurfaceModel surf = col.GetComponent<ISurfaceModel>();
            if (surf != null)
            {
                return surf;
            }
        }
        return null;
    }


    /// <summary>
    /// Collider check helper function
    /// </summary>
    /// <param name="checkCollider">What collider to check with</param>
    /// <param name="filter">Filter the results with</param>
    /// <returns>True if something was found</returns>
    private bool CollisionCheck(Collider2D checkCollider, ContactFilter2D filter)
    {
        List<Collider2D> candidates = new List<Collider2D>(0);
        Physics2D.OverlapCollider(checkCollider, filter, candidates);
        return candidates.Count > 0;
    }


    /// <summary>
    /// Checks if MegaMan grabs a ladder, if so, changes state, adjusts position as required
    /// </summary>
    /// <param name="ladderDetector">Collider to check under</param>
    /// <param name="checkAbove">Check also the uprightCeiling collider for a ladder above his head</param>
    /// <returns>True if MegaMan grabbed a ladder</returns>
    private bool LadderCheck(Collider2D ladderDetector, bool checkAbove = false)
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
                return MountLadder(m_groundLadderDetector, -m_climbingDownYAdjust);
            }
            else
            {
                // Pressing 'up'
                return MountLadder(m_uprightCeilingCheckCollider, m_climbingUpYAdjust);
            }
        }
        return false;
    }

    /// <summary>
    /// MegaMan gets on the supplied ladder
    /// </summary>
    /// <param name="ladderDetector">The ladder detector that found the ladder</param>
    /// <param name="topEntry">When true, we are checking below MegaMan's feet, so he might need to be shoved downwards a little</param>
    /// <returns></returns>
    private bool MountLadder(Collider2D ladderDetector, float yAdjust = 0)
    {
        Vector2 ladderCentre;
        Vector3Int dummy;
        bool foundLadder = m_ladderHandler.ClosestLadder(ladderDetector, out ladderCentre, out dummy);
        if (!foundLadder)
        {
            return false;
        }
        if (yAdjust < 0)
        {
            m_ladderHandler.OpenTrapDoors(m_groundLadderDetector);
        }
        else if (yAdjust > 0)
        {
            m_ladderHandler.OpenTrapDoors(m_uprightCeilingCheckCollider);
        }
        Vector2 newPosition = new Vector2(ladderCentre.x, gameObject.transform.position.y + yAdjust);
        gameObject.transform.position = newPosition;
        m_ladderXPosition = newPosition.x;
        m_rigidBodySelf.velocity = Vector2.zero;
        state = MegaManStates.Climbing;
        return true;
    }


    /// <summary>
    /// Everything is rightward facing for MegaMan, when turning left, just use this
    /// </summary>
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;
    
        transform.Rotate(0f, 180f, 0f);
    }
}
