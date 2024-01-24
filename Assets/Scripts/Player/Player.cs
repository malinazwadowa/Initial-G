using System;
using UnityEngine;

public class Player : MonoBehaviour, IItemWielder, IDamagable
{
    [SerializeField] public SO_PlayerParameters playerData;

    private Transform center;
    public bool IsAlive {get; private set;}

    [HideInInspector] public PlayerInputActions InputActions { get; private set; }
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private Rigidbody2D rigidBody;

    private HealthController healthController;
    private PlayerMovementController movementController;
    private PlayerAnimationController animationController;
    private LootCollisionHandler lootCollisionHandler;

    private CharacterStatsController characterStatsController;

    [HideInInspector] public  ExperienceController ExperienceController { get; private set; }
    [HideInInspector] public ItemController ItemController { get; private set; }
    [HideInInspector] public PlayerInputController InputController { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        IsAlive = true;
        healthController = GetComponent<HealthController>();
        ItemController = GetComponent<ItemController>();
        lootCollisionHandler = GetComponentInChildren<LootCollisionHandler>();
        movementController = GetComponent<PlayerMovementController>();
        animationController = GetComponent<PlayerAnimationController>();
        InputController = GetComponent<PlayerInputController>();
        ExperienceController = GetComponent<ExperienceController>();

        characterStatsController = GetComponent<CharacterStatsController>();

        characterStatsController.Initialize();
        ExperienceController.Initialize(FindAnyObjectByType<ExpBarUI>());
        healthController.Initialize(playerData.maxHealth);

        lootCollisionHandler.Initialize(playerData.lootingRadius, characterStatsController.characterStats);
        movementController.Initialize(playerData, rigidBody, characterStatsController.characterStats);
        animationController.Initialize(animator, movementController, spriteRenderers);
        InputController.Initialize(movementController, InputActions);

        ItemController.Initialize(this, characterStatsController);
    }

    private void Start()
    {
        center = transform.Find("Center");
    }

    private void Update()
    {
        animationController.SetAnimationVelocity(InputActions.GameplayActions.Movement.ReadValue<Vector2>());
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
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Vector3 direction = enemy.transform.position - weapon.transform.position;
            enemy.GetKnockbacked(3, direction);
        }*/
    }

    public void GetDamaged(float amount, string damageSource)
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
        if (!IsAlive) { return; }
        //AudioManager.Instance.PlaySound(AudioClipID.Something);
        IsAlive = false;
        EventManager.OnPlayerDeath?.Invoke();
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        //throw new System.NotImplementedException();
    }
}
