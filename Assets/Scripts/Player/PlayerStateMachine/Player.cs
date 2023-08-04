using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    #region Properties
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkingState WalkingState { get; private set; }
    public PlayerRunningState RunningState { get; private set; }
    public PlayerAttackingState AttackingState { get; private set; }
    public PlayerStealthState StealthState { get; private set; }
    public PlayerCarryingStance CarryingStance { get; private set; }
    public PlayerHitState HitState { get; private set; }
    #endregion
    public enum CurrentState { IdleState, WalkingState, RunningState, AttackingState, StealthState }
    public CurrentState currentState;

    public PlayerInputActions playerInputActions;
    public PlayerData playerData;
    public Animator animator;
    public NavMeshAgent agent;
    public AudioSource audioSource;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        IdleState = new PlayerIdleState(this, StateMachine, playerData);
        WalkingState = new PlayerWalkingState(this, StateMachine, playerData);
        RunningState = new PlayerRunningState(this, StateMachine, playerData);
        AttackingState = new PlayerAttackingState(this, StateMachine, playerData);
        StealthState = new PlayerStealthState(this, StateMachine, playerData);
        CarryingStance = new PlayerCarryingStance(this, StateMachine, playerData);
        HitState = new PlayerHitState(this, StateMachine, playerData);

        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        //Needed due to how NavMeshPlus works.
        agent.updateRotation = false;
        agent.updateUpAxis = false;      
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    
}
