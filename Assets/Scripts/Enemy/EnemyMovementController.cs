using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    private bool isInitialized = false;

    public void Init(NavMeshAgent agent, Player player)
    {
        this.agent = agent;
        this.player = player;
        isInitialized = true;
    }
    private void Update()
    {
        if (isInitialized)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }
}
