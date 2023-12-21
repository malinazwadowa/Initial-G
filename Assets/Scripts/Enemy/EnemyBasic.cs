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

            if(attackTimer > enemyParameters.attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
    }

    public override void Initialize()
    {
        base.Initialize();

        movementController = GetComponent<EnemyMovementController>();
        movementController.Init(enemyParameters.speed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamagable target = collision.gameObject.GetComponent<IDamagable>();

        if (target != null)
        {
            Attack(target);
        }
    }

    private void Attack(IDamagable target)
    {
        if (!canAttack) { return; }

        target.GetDamaged(enemyParameters.damage);
        canAttack = false;
    }
}
