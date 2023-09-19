using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : Enemy
{
    private NavMeshAgent agent;
    private EnemyMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Init()
    {
        base.Init();
        Debug.Log("Dupala");

        agent = GetComponent<NavMeshAgent>();
        //Needed due to how NavMeshPlus works.
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        movementController = GetComponent<EnemyMovementController>();
        movementController.Init(agent, player);
    }
}
