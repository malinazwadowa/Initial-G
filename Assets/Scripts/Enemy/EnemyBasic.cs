using UnityEngine;

public class EnemyBasic : Enemy
{
    private EnemyMovementController movementController;
    private float attackTimer;
    private bool canAttack = true;

    public override void Update()
    {
        base.Update();

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer > enemyData.attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
    }

    public override void Init()
    {
        base.Init();

        movementController = GetComponent<EnemyMovementController>();
        movementController.Init(enemyData.speed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();

        if (target != null)
        {
            Attack(target);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // collision.gameObject.GetComponent<IDamagable>()?.GetDamaged(enemyData.damage);
    }

    private void Attack(IDamagable target)
    {
        if (!canAttack) { return; }

        target.GetDamaged(enemyData.damage);
        canAttack = false;
    }
}
