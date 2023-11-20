using System;
using UnityEngine;

public class Player : MonoBehaviour, IWeaponWielder, IDamagable
{
    [SerializeField] public PlayerData playerData;

    private Transform center;
    private CombatStats combatStats;

    [HideInInspector] public PlayerInputActions InputActions { get; private set; }
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private Rigidbody2D rigidBody;

    private HealthController healthController;
    [HideInInspector] public WeaponController weaponController;
    private ExperienceController experienceController;

    private PlayerMovementController movementController;
    private PlayerAnimationController animationController;
    [HideInInspector] public PlayerInputController inputController { get; private set; }

    private void Awake()
    {
        combatStats = new CombatStats(playerData.baseDamageModifier, playerData.baseWeaponSpeed, playerData.baseCooldownModifier);
        
        InputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        
        healthController = GetComponent<HealthController>();
        weaponController = GetComponent<WeaponController>();

        movementController = GetComponent<PlayerMovementController>();
        animationController = GetComponent<PlayerAnimationController>();
        inputController = GetComponent<PlayerInputController>();
        experienceController = GetComponent<ExperienceController>();



        experienceController.Init(FindAnyObjectByType<ExpBarUI>());
        healthController.Init(playerData.maxHealth);


        movementController.Init(playerData, rigidBody);
        animationController.Init(animator, movementController, playerData, spriteRenderers);
        inputController.Init(movementController, InputActions);
        weaponController.Init(this, combatStats);
    }

    private void Start()
    {
        center = transform.Find("Center");
    }

    private void Update()
    {
        animationController.SetAnimationVelocity(InputActions.GameplayActions.Movement.ReadValue<Vector2>());
        
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
        float current = Time.timeScale;
        Debug.Log(current);
        combatStats.UpdateCombatStat(StatType.DamageModifier, 1);

    }

    public void GetDamaged(float amount)
    {
        healthController.SubstractCurrentHealth(amount);
        if(healthController.GetCurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        EventManager.OnPlayerDeath?.Invoke();
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        //throw new System.NotImplementedException();
    }
}
