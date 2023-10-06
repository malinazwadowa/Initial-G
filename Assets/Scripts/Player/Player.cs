using UnityEngine;

public class Player : MonoBehaviour, IWeaponWielder
{
    [SerializeField] public PlayerData playerData;

    //private Transform weapon;
    private Transform center;
    private CombatStats combatStats;

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
        combatStats = new CombatStats(playerData.baseDamageModifier, playerData.baseWeaponSpeed, playerData.baseCooldownModifier);
        
        inputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        healthController = GetComponent<HealthController>();
        weaponController = GetComponent<WeaponController>();

        movementController = GetComponent<PlayerMovementController>();
        animationController = GetComponent<PlayerAnimationController>();
        inputController = GetComponent<PlayerInputController>();

        healthController.Init(playerData.maxHealth);
        movementController.Init(playerData, rigidBody);
        animationController.Init(animator, movementController, playerData, spriteRenderers);
        inputController.Init(movementController, inputActions);
        weaponController.Init(this, combatStats);
    }

    private void Start()
    {
        center = transform.Find("Center");
    }

    private void Update()
    {
        animationController.SetAnimationVelocity(inputActions.PlayerMovement.Movement.ReadValue<Vector2>());
        
        //Testing purposes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestCombatStats();
        }
    }

    private void FixedUpdate()
    {

    }

    public Vector3 GetCenterPosition()
    {
        return center.position;
    }
    public Transform GetCenterTransform()
    {
        return center;
    }

    public Vector2 GetFacingDirection()
    {
        return movementController.LastNonZeroVelocity.normalized;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {/*
        Debug.Log("Kope wroga");
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log("Kope wroga");
            Vector3 direction = enemy.transform.position - weapon.transform.position;
            enemy.GetKnockbacked(3, direction);
        }*/
    }

    public void TestCombatStats()
    {
        Debug.Log("Upping the base damage");


        combatStats.UpdateCombatStat(StatType.DamageModifier, 1);

    }
}
