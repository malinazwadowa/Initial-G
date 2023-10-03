using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player player;
    public bool isInitialized = false;
    private float speed;
    private float delay;
    //private Rigidbody2D myRigidbody;



    public LayerMask enemyLayer;
    public float detectionRadius;
    public void Init(Player player, float speed)
    {
        this.player = player;
        this.speed = speed;
        //isInitialized = true;
        //delay = 0;
        //myRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //delay += Time.deltaTime;



        //Debug.Log(player.transform.position);
        transform.position += Time.deltaTime * speed * (player.transform.position - transform.position).normalized;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //myRigidbody.velocity = speed * (player.transform.position - transform.position).normalized;
        /*
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        List<Vector2> directions = new List<Vector2>();
        foreach(Collider2D enemy in nearbyEnemies)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            directions.Add(direction);
        } */

    }

}
