using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// enum PatrolBotState
// {
//     Scooting = 0,
//     TurnStart = 10,
//     TurnFinish = 20,
//     BareShock = 30,
//     BareScooting = 40,
//     BareTurnStart = 50,
//     BareTurnFinish = 60,
//     SmugScooting = 70,
//     SmugTurnStart = 80,
//     SmugTurnFinish = 90
// }

// Or using flags for Bare, BareShock, Smug
public enum PatrolBotState
{
    Scooting = 0,
    TurnStart = 10,
    TurnFinish = 20
}


[RequireComponent(typeof(ILive))]
[RequireComponent(typeof(ISelfDestruct))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PatrolBotAnimationDirector))]
public class PatrolBot : MonoBehaviour, ILoyalty, IDie, IGetHurt, ICanHit
{
    // *** References
    PatrolBotAnimationDirector m_animationDirector;
    ILive m_health;
    ISelfDestruct m_reaper;
    Collider2D m_collider;


    // *** User settings

    [Header("Patrol movement")]
    [SerializeField] private float m_leftwards = 2;
    [SerializeField] private float m_rightwards = 2;
    [SerializeField] private float m_patrolSpeed = 3;
    [SerializeField] private float m_acceleration = 10;

    // The graphics face left, so PatrolBot's default direction is left
    [Tooltip("Which way does he start facing?")]
    [SerializeField] private bool m_facingLeft;

    [Header("Other settings")]
    [Tooltip("How much damage does MegaMan get when he touches me")]
    [SerializeField] private int m_touchDamage = 5;
    [Tooltip("When his shield gets pulled off, he looks shocked for this duration")]
    [SerializeField] [Range(0, 2)] private float m_bareShockDuration = 1;
    [Tooltip("When he hits MegaMan, he becomes smug for this duration")]
    [SerializeField] [Range(0, 2)] private float m_smugDuration = 1;
    [Tooltip("Explosion death scene, if any")]
    [SerializeField] private GameObject m_explosion;


    // *** Private variables

    // Trajectory constants
    [Header("Calculated constants")]
    public Vector3 m_initPosition;
    public Vector3 m_positionLeft;
    public Vector3 m_positionRight;
    public Vector3 m_turningPointLeft;
    public Vector3 m_turningPointRight;
    public float m_totalDist;
    public float m_decelTime;
    public float m_decelDist;

    // Current state of things
    [Header("Calculated variables")]
    public PatrolBotState state = PatrolBotState.Scooting;
    public float m_direction; // -1 left, +1 right
    public float m_currVelocity;
    public bool m_exploded;
    public bool m_bare;        // m_bare and m_bareShock go on together, then m_bareShock goes off
    public bool m_bareShock;   // ^
    public float m_bareShockStart;
    public bool m_smug;
    public float m_smugStart;
    public float m_targetSpeed;
    public Vector2 m_controlVector;

    // *** Access
    public PatrolBotState State { get => state; }
    public bool Smug { get => m_smug; }
    public bool Bare { get => m_bare; }
    public bool BareShock { get => m_bareShock; }


    // *** MonoBehaviour interface

    void Awake()
    {
        m_animationDirector = GetComponent<PatrolBotAnimationDirector>();
        m_health = GetComponent<ILive>();
        m_reaper = GetComponent<ISelfDestruct>();
        m_collider = GetComponent<Collider2D>();
        m_exploded = false;
        m_smug = false;
        m_smugStart = -1;
        m_bare = false;
        m_bareShock = false;
        side = Team.BadGuys;
        CalculateTrajectory();
    }

    void Start()
    {
        if (m_explosion != null)
        {
            m_reaper.IHaveFinalWords(this);
        }
        Dictionary<string, double> clips = m_animationDirector.ClipData;
        string outputMe = "";
        foreach(KeyValuePair<string, double> entry in clips) {
            outputMe = " " + entry.Key + ":" + entry.Value;
        }
        Debug.Log(outputMe);
    }

    void FixedUpdate()
    {
        if (m_smug && Time.time - m_smugStart > m_smugDuration)
        {
            m_smug = false;
        }
        Move();
    }

    // *** ILoyalty interface
    public Team side { get; set; }


    // *** IDie interface
    public void Die()
    {
        if (m_explosion != null)
        {
            Instantiate(m_explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
        m_exploded = true;
    }
    public bool Dying()
    {
        return m_exploded;
    }
    public bool ReadyToDie()
    {
        return m_exploded;
    }


    // *** IGetHurt interface
    public bool TakeDamage(Collision2D collision, int damage, ICanHit attacker)
    {
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        collision.GetContacts(contacts);
        List<Vector2> points = (from contact in contacts select contact.point).ToList();
        return InternalHit(points, damage, attacker);
    }
    public bool TakeDamage(Collider2D incomingCollider, int damage, ICanHit attacker)
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(incomingCollider.bounds.center);
        return InternalHit(points, damage, attacker);
    }
    // *** IGetHurt interface internal helpers
    private bool InternalHit(List<Vector2> points, int damage, ICanHit attacker)
    {
        int nHits = 0;
        if (m_facingLeft)
        {
            foreach (Vector2 point in points)
            {
                if (point.x > gameObject.transform.position.x)
                {
                    ++nHits;
                    if (!attacker.ScatterHit())
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (Vector2 point in points)
            {
                if (point.x < gameObject.transform.position.x)
                {
                    ++nHits;
                    if (!attacker.ScatterHit())
                    {
                        break;
                    }
                }
            }
        }
        if (nHits > 0)
        {
            if (!attacker.ScatterHit())
            {
                nHits = 1;
            }
            m_health.TakeDamage(nHits * damage);
            return true;
        }
        else
        {
            if (attacker.Deflectable())
            {
                attacker.Deflect();
            }
            return false;
        }
    }


    // *** ICanHit interface

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D targetCollider = collision.collider;
        IGetHurt target = GeneralGameTools.ApplyRulesOfEngagement(targetCollider, m_collider, side, "collision.collider");
        if (target == null)
        {
            return;
        }
        if (target.TakeDamage(collision, m_touchDamage, this))
        {
            // Target has accepted the hit, do our Hit reaction
            m_smugStart = Time.time;
            m_smug = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        CheckForColliderHits(hitInfo);
    }
    public void OnTriggerStay2D(Collider2D hitInfo)
    {
        CheckForColliderHits(hitInfo);
    }
    // *** ICanHit interface internal helpers
    private void CheckForColliderHits(Collider2D hitInfo)
    {
        IGetHurt target = GeneralGameTools.ApplyRulesOfEngagement(hitInfo, m_collider, side, "collision.otherCollider");
        if (target == null)
        {
            return;
        }
        if (target.TakeDamage(hitInfo, m_touchDamage, this))
        {
            // Target has accepted the hit, do our Hit reaction
            m_smugStart = Time.time;
            m_smug = true;
        }
    }
    public bool Deflectable()
    {
        return false;
    }
    public void Deflect()
    {
        // Do nothing
    }
    public bool ScatterHit()
    {
        return false;
    }

    // *** Public member functions

    public void InputMoveLeft()
    {
        m_controlVector = Vector2.left;
    }
    public void InputMoveRight()
    {
        m_controlVector = Vector2.right;
    }
    public void InputMoveNone()
    {
        m_controlVector = Vector2.zero;
    }


    // *** Private member functions

    private void Move()
    {
        if (m_targetSpeed > 0)
        {
            // Accelerating / moving right
            if (transform.position.x >= m_turningPointRight.x)
            {
                // Change direction
                state = PatrolBotState.TurnStart;
                m_targetSpeed = -m_patrolSpeed;
                // Debug.Log("Moving right, turning, set m_targetSpeed to " + m_targetSpeed);
            }
        }
        else
        {
            if (transform.position.x <= m_turningPointLeft.x)
            {
                // Change direction
                state = PatrolBotState.TurnStart;
                m_targetSpeed = m_patrolSpeed;
                // Debug.Log("Moving left, turning, set m_targetSpeed to " + m_targetSpeed);
            }
        }
        float delta = m_currVelocity - m_targetSpeed;
        if (delta < Mathf.Epsilon)
        {
            m_currVelocity = Mathf.Max(m_targetSpeed, m_currVelocity + m_acceleration*Time.fixedDeltaTime);
        }
        else if (delta > Mathf.Epsilon)
        {
            m_currVelocity = Mathf.Min(m_targetSpeed, m_currVelocity - m_acceleration * Time.fixedDeltaTime);
        }
        else
        {
            state = PatrolBotState.Scooting;
        }
        Vector3 newPosition = new Vector3(transform.position.x + m_currVelocity*Time.fixedDeltaTime, transform.position.y, transform.position.z);
        transform.position = newPosition;
        // Detect a zero-crossing of m_currentVelocity
        if (state == PatrolBotState.TurnStart && m_currVelocity*m_direction < 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        state = PatrolBotState.TurnFinish;
        m_facingLeft = !m_facingLeft;
        m_direction *= -1;
        transform.Rotate(0f, 180f, 0f);
        // Debug.Log("Flip : now facingLeft is " + m_facingLeft);
    }

    /// <summary>
    /// Calculate constant variables associated with patrolbot's trajectory
    /// </summary>
    private void CalculateTrajectory()
    {
        m_initPosition = gameObject.transform.position;
        m_positionLeft = m_initPosition;
        m_positionLeft.x -= m_leftwards;
        m_positionRight = m_initPosition;
        m_positionRight.x += m_rightwards;
        float m_totalDist = m_leftwards + m_rightwards;
        m_direction = m_facingLeft ? -1 : 1;
        m_targetSpeed = m_direction * m_patrolSpeed;
        // Debug.Log("Initialised m_targetSpeed to " + m_targetSpeed);
        m_currVelocity = m_targetSpeed;
        if (m_acceleration < Mathf.Epsilon)
        {
            // Disable acceleration
            m_decelTime = 0;
            m_decelDist = 0;
        }
        else
        {
            m_decelTime = m_patrolSpeed/m_acceleration;
            m_decelDist = m_patrolSpeed * m_decelTime + 0.5f * m_acceleration * m_decelTime * m_decelTime;
            if (m_decelDist >= m_totalDist*0.5f)
            {
                Vector3 midPoint = 0.5f*(m_positionLeft + m_positionRight);
                m_turningPointLeft = midPoint;
                m_turningPointRight = midPoint;
                m_currVelocity = 0;
            }
            else
            {
                m_turningPointLeft = m_positionLeft;
                m_turningPointLeft.x += m_decelDist;
                m_turningPointRight = m_positionRight;
                m_turningPointRight.x -= m_decelDist;
            }
        }
    }
}
