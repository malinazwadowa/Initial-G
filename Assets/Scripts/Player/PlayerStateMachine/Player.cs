using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// TODO
/// 
/// Player should be a main starting point of a player MODULE
/// It should have references to all its components and controllers and initialize these by passing all necessary references to these components 
/// We should avoid circular dependencies at all costs (Player should have reference to PlayerCombatController, but PlayerCombatController should not have reference to Player directly)
/// PlayerCombatController should have Init() function which will have as arguments all other components that it needs in order to function
/// And Player class here itself should be the "Master" that passes all references around 
/// 
/// Player should be a prefab, and it should be spawned by a something like PlayerSpawner
/// 
/// There should not be any variables in the inspector that are not meant to be changed in the inspector directly.
/// 
/// There should be PlayerInputController class that gathers player input and invokes methods in player controllers accordingly
/// PlayerInputController should have reference to PlayerCombatController and it should invoke Attack() method when input conditions are met
/// 
/// 
/// </summary>
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

    //THIS ENUM SHOULD BE IN SEPARATE FILE AND SHOULD NOT BE NAMED CurrentState but EPlayerState if you want to keep it
    public enum CurrentState { IdleState, WalkingState, RunningState, AttackingState, StealthState }
    public CurrentState currentState;

    public PlayerInputActions playerInputActions;
    public PlayerData playerData;
    public Animator animator;
    public NavMeshAgent agent;
    public AudioSource audioSource;

    [SerializeField] public Vector2 vectorToTest;

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

        Debug.Log(MathUtility.RoundVector2D(playerInputActions.Player.Movement.ReadValue<Vector2>()));
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    
}
