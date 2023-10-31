using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : Enemy
{
    private EnemyMovementController movementController;

    public override void Update()
    {
        base.Update();
    }

    public override void Init()
    {
        base.Init();

        movementController = GetComponent<EnemyMovementController>();
        movementController.Init(enemyData.speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            collision.gameObject.GetComponent<IDamagable>().GetDamaged(10);
        }
    }
}
