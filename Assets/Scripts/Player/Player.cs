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
    [SerializeField] public PlayerData playerData;

    private PlayerInputActions inputActions;
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private Rigidbody2D rigidBody;

    private PlayerMovementController movementController;
    private PlayerAnimationController animationController;
    private PlayerInputController inputController;
    
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        //inputActions.PlayerMovement.Enable();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        movementController = GetComponent<PlayerMovementController>();
        animationController = GetComponent<PlayerAnimationController>();
        inputController = GetComponent<PlayerInputController>();
       
        movementController.Init(inputActions, playerData, rigidBody, spriteRenderers );
        animationController.Init(animator, movementController, playerData);
        inputController.Init(movementController, inputActions);
    }

    private void Start()
    {
    
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    
}
