using System;
using UnityEngine;

public class Player : MonoBehaviour, IWeaponWielder, IDamagable
{
    [SerializeField] public PlayerParameters playerData;

    private Transform center;
    private CombatStats combatStats;

    [HideInInspector] public PlayerInputActions InputActions { get; private set; }
    private Animator animator;
    private SpriteRenderer[] spriteRenderers;
    private Rigidbody2D rigidBody;

    private HealthController healthController;
    private ExperienceController experienceController;
    private PlayerMovementController movementController;
    private PlayerAnimationController animationController;
    private LootCollisionHandler lootCollisionHandler;

    [HideInInspector] public WeaponController weaponController;
    [HideInInspector] public PlayerInputController InputController { get; private set; }

    private void Awake()
    {
        combatStats = new CombatStats(playerData.baseDamageModifier, playerData.baseWeaponSpeed, playerData.baseCooldownModifier);
        
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



        experienceController.Init(FindAnyObjectByType<ExpBarUI>());
        healthController.Init(playerData.maxHealth);

        lootCollisionHandler.Init(playerData.lootingRadius);
        movementController.Init(playerData, rigidBody);
        animationController.Init(animator, movementController, playerData, spriteRenderers);
        InputController.Init(movementController, InputActions);
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

        if (Input.GetKeyDown(KeyCode.N))
        {
            Test2();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Test3();
            GameManager.Instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.Load();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(GameManager.Instance.loadedData.settingsData.masterVolume);
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
        combatStats.UpdateCombatStat(StatType.DamageModifier, 1);
        AudioManager.Instance.PauseAllSounds();
    }

    public void Test2()
    {
        AudioManager.Instance.ResumeAllSounds();
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
