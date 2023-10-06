using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    //public bool isInitialized = false;
    private Player player;
    private float speed;

    private Transform playersTransform;

    public LayerMask enemyLayer;

    public float detectionRadius;
    public float offsetDistanceFromPlayersPosition;

    private Vector3 avoidanceOffset;

    public void Init(float speed)
    {
        this.speed = speed;
        //isInitialized = true;
        playersTransform = PlayerManager.Instance.GetPlayersCenterTransform();
    }
    private void Update()
    {
        AvoidOtherEnemies();
        Vector3 directionTowardsPlayer = (playersTransform.position - transform.position).normalized;
        Vector3 targetPosition = playersTransform.position - (directionTowardsPlayer * offsetDistanceFromPlayersPosition);
        transform.position += Time.deltaTime * speed * ((targetPosition - transform.position).normalized + avoidanceOffset);
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
