using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Properties
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemyWanderState WanderState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyCombatState CombatState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }
    public EnemyHitState HitState { get; private set; }
    #endregion

    public Player player;
    public NavMeshAgent agent;
    public NavMeshObstacle obstacle;
    public Animator animator;
    public AudioSource audioSource;

    public EnemyData enemyData;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        WanderState = new EnemyWanderState(this, StateMachine, enemyData);
        ChaseState = new EnemyChaseState(this, StateMachine, enemyData);
        CombatState = new EnemyCombatState(this, StateMachine, enemyData);
        DeadState = new EnemyDeadState(this, StateMachine, enemyData);
        HitState = new EnemyHitState(this, StateMachine, enemyData);
        
        player = GameObject.FindObjectOfType<Player>();
        //enemyData = new EnemyData();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        enemyData.currentHealth = enemyData.maxHealth;
        enemyData.initalSpawnPositon = transform.position;

        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();

        //Needed due to how NavMeshPlus works.
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        StateMachine.Initialize(WanderState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    // **********************
    public void SubstractHealth(float amount)
    {
        enemyData.currentHealth -= amount;
    }
    public float CurrentHealth()
    {
        return enemyData.currentHealth;
    }
    /************************/

    public float GetDistanceToPlayer()
    {
        float distance;
        return distance = Vector2.Distance(transform.position, player.transform.position);
    }
    public float GetDistanceToSpawn()
    {
        float distance;
        return distance = Vector2.Distance(transform.position, enemyData.initalSpawnPositon);
    }
    public Vector2 GetDirectionTowardsPlayer()
    {
        Vector2 direction;
        return direction = player.transform.position - transform.position;
    }
    /*
     * Returns one of 4 values based on direction towards provided vector 
     * 1 - Up
     * 2 - Right
     * 3 - Down
     * 4 - Left
     * Depending on the angle, returns the corresponding direction code and flips the 'spriteRenderer' if needed.
     */
    public int GetFacingDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        int tempDir = 3;
        if (angle > 60 && angle <= 120)
        {
            spriteRenderer.flipX = false;
            tempDir = 1;
            return tempDir;
        }
        if (angle > 120 && angle <= 180)
        {
            spriteRenderer.flipX = false;
            tempDir = 4;
            return tempDir;
        }
        if (angle >= -180 && angle <= -120)
        {
            spriteRenderer.flipX = false;
            tempDir = 4;
            return tempDir;
        }
        if (angle > -120 && angle <= -60)
        {
            spriteRenderer.flipX = false;
            tempDir = 3;
            return tempDir;
        }
        if (angle > -60 && angle <= 0)
        {
            spriteRenderer.flipX = true;
            tempDir = 2;
            return tempDir;
        }
        if (angle > 0 && angle <= 60)
        {
            spriteRenderer.flipX = true;
            tempDir = 2;
            return tempDir;
        }

        spriteRenderer.flipX = false;
        return tempDir;
    }
}
