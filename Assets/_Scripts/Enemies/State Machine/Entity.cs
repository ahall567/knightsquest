using System.Diagnostics.Contracts;
using UnityEngine;
using KnightsQuest.CoreSystem;

public class Entity : MonoBehaviour
{
    protected Movement Movement { get => movement ??= Core.GetCoreComponent<Movement>(); }

    private Movement movement;

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public Animator anim {  get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core Core { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;

    protected Stats stats;
    

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        // Get references to Core Components
        stats = Core.GetCoreComponent<Stats>();

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Movement.RB.linearVelocity.y);

        if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    #region Check Functions

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAggroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    #endregion

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.linearVelocity.x, velocity);
        Movement.RB.linearVelocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void OnDrawGizmos()
    {
        if(Core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAggroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAggroDistance), 0.2f);
        }
        
    }
}
