using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : Enemy
{
    private EnemyMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    /*public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    } */
    public override void Init()
    {
        base.Init();

/*agent = GetComponent<NavMeshAgent>();
        //Needed due to how NavMeshPlus works.
        agent.updateRotation = false;
        agent.updateUpAxis = false; */

        movementController = GetComponent<EnemyMovementController>();
        movementController.Init(enemyData.speed);
    }
    
}
