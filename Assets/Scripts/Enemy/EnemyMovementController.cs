using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    //public bool isInitialized = false;
    private Player player;
    private float speed;

    public LayerMask enemyLayer;
    public float detectionRadius;
    private Vector3 avoidanceOffset;

    public void Init(Player player, float speed)
    {
        this.player = player;
        this.speed = speed;
        //isInitialized = true;
    }
    private void Update()
    {
        AvoidOtherEnemies();
        
        transform.position += Time.deltaTime * speed * ((player.transform.position - transform.position).normalized + avoidanceOffset);
    }
    private void AvoidOtherEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

        if (nearbyEnemies.Length > 0)
        {
            List<Vector3> directions = new List<Vector3>();

            foreach (Collider2D enemy in nearbyEnemies)
            {
                if (enemy.gameObject != gameObject)
                {
                    Vector3 direction = (transform.position - enemy.transform.position).normalized;
                    directions.Add(direction);

                }

            }

            Vector3 sumOfDirections = Vector3.zero;

            foreach (Vector3 direction in directions)
            {
                sumOfDirections += direction;
            }

            avoidanceOffset = sumOfDirections.normalized;
        }
        else
        {
            avoidanceOffset = Vector3.zero;
        }
    } 
}
