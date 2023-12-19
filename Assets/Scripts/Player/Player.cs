using System;
using UnityEngine;

public class Player : MonoBehaviour, IWeaponWielder, IDamagable
{
    [SerializeField] public SO_PlayerParameters playerData;

    private Transform center;
    

    [HideInInspector] public PlayerInputActions InputActions { get; private set; }
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private Rigidbody2D rigidBody;

    private HealthController healthController;
    private ExperienceController experienceController;
    private PlayerMovementController movementController;
    private PlayerAnimationController animationController;
    private LootCollisionHandler lootCollisionHandler;

    private CharacterStatsController characterStatsController;
    private AccessoryController accessoryController;

    [HideInInspector] public WeaponController weaponController;
    [HideInInspector] public PlayerInputController InputController { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        
        healthController = GetComponent<HealthController>();
        weaponController = GetComponent<WeaponController>();

        lootCollisionHandler = GetComponentInChildren<LootCollisionHandler>();
        movementController = GetComponent<PlayerMovementController>();
        animationController = GetComponent<PlayerAnimationController>();
        InputController = GetComponent<PlayerInputController>();
        experienceController = GetComponent<ExperienceController>();

        characterStatsController = GetComponent<CharacterStatsController>();
        accessoryController = GetComponent<AccessoryController>();

        characterStatsController.Initialize();
        experienceController.Initialize(FindAnyObjectByType<ExpBarUI>());
        healthController.Initialize(playerData.maxHealth);

        lootCollisionHandler.Initialize(playerData.lootingRadius);
        movementController.Initialize(playerData, rigidBody);
        animationController.Initialize(animator, movementController, playerData, spriteRenderers);
        InputController.Initialize(movementController, InputActions);


        accessoryController.Initialize(characterStatsController);
        weaponController.Initalize(this, characterStatsController.GetStats());
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
        //AudioManager.Instance.StopAllClips();
        Debug.Log("Upping the base damage");
        float current = Time.timeScale;
        Debug.Log(current);
        //combatStats.UpdateCombatStat(StatType.DamageModifier, 1);
        AudioManager.Instance.PauseAllSounds();
    }

    public void Test2()
    {
        //AudioManager.Instance.ResumeAllSounds();
    }

    public void Test3()
    {
        AudioManager.Instance.PlaySound(AudioClipID.PlayerDeath);
    }

    public void GetDamaged(float amount)
    {
        healthController.SubstractCurrentHealth(amount);
        animationController.ChangeColorOnDamage();
        AudioManager.Instance.PlaySound(AudioClipID.PlayerHit);

        if(healthController.GetCurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        //AudioManager.Instance.PlaySound(AudioClipID.Something);
        EventManager.OnPlayerDeath?.Invoke();
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        //throw new System.NotImplementedException();
    }
}
