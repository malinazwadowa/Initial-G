using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    public bool isInitialized = false;
    private float delay;

    public void Init(NavMeshAgent agent, Player player)
    {
        this.agent = agent;
        this.player = player;
        //isInitialized = true;
        //delay = 0;
        
    }
    private void Update()
    {
        //delay += Time.deltaTime;
        if (gameObject.activeSelf && agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }
}
