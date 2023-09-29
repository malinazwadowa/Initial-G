using System;
using UnityEngine;

public class Player : MonoBehaviour, IWeaponWielder
{
    [SerializeField] public PlayerData playerData;

    private Transform weapon;
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
        weaponController = GetComponentInChildren<WeaponController>();

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
        weapon = GetComponentInChildren<WeaponController>().transform;
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

    public Vector3 GetWeaponsPosition()
    {
        return weapon.transform.position;
    }
    public Transform GetWeaponsTransform()
    {
        return weapon.transform;
    }

    public Vector2 GetFacingDirection()
    {
        return movementController.LastNonZeroVelocity.normalized;
    }


    public void TestCombatStats()
    {
        Debug.Log("Upping the base damage");


        combatStats.UpdateCombatStat(StatType.DamageModifier, 1);

    }
}
