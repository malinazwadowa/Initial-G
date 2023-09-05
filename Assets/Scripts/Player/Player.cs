using UnityEngine;

/// <summary>
/// TODO
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

    private HealthController healthController;

    private WeaponController weaponController;

    private PlayerMovementController movementController;
    private PlayerAnimationController animationController;
    private PlayerInputController inputController;
   

    
    
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        healthController = GetComponent<HealthController>();
        weaponController = GetComponentInChildren<WeaponController>();

        movementController = GetComponent<PlayerMovementController>();
        animationController = GetComponent<PlayerAnimationController>();
        inputController = GetComponent<PlayerInputController>();

        healthController.Init(playerData.maxHealth);
        movementController.Init(playerData, rigidBody);
        animationController.Init(animator, movementController, playerData, spriteRenderers);
        inputController.Init(movementController, inputActions);

        
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        animationController.SetAnimationVelocity(inputActions.PlayerMovement.Movement.ReadValue<Vector2>());
        if (Input.GetKeyDown(KeyCode.F)) { Debug.Log(MathUtility.GetClosestEnemy(transform.position).transform.position); }

        //Debug.Log(EnemyManager.Instance.GetRandomSpawnPositionOutsideCameraView());
        
    }

    private void FixedUpdate()
    {
        
    }
}
