using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Test : MonoBehaviour
{
    public Transform player;
    public Transform obstacle;

    Vector3 toTarget;
    Vector3 awayFromObstacle;
    Vector3 sumDirection;
    // Start is called before the first frame update
    void Start()
    {
     
        Collider2D collider = obstacle.GetComponent<Collider2D>();
        //collider.bounds.
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the normalized direction from the object to the target
            toTarget = (player.position - transform.position).normalized;

            // Draw the arrow in the scene view
            DrawArrow(transform.position, toTarget, Color.red);
        }

        if (obstacle != null)
        {
            // Calculate the normalized direction away from the obstacle
            awayFromObstacle = (transform.position - obstacle.position).normalized * 1.1f;

            // Draw the arrow pointing away from the obstacle
            DrawArrow(transform.position, awayFromObstacle, Color.green);
        }
        // Calculate the normalized direction of the sum of the two vectors
        sumDirection = (toTarget + awayFromObstacle).normalized;
        //Debug.Log(sumDirection);
        float angle = Mathf.Atan2(sumDirection.y, sumDirection.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        // Draw the arrow pointing in the direction of the sum of the vectors
        DrawArrow(transform.position, sumDirection, Color.blue);

        //Vector3 dup = sumDirection + awayFromObstacle;


        //DrawArrow(transform.position, dup, Color.magenta);
    }

    void DrawArrow(Vector3 start, Vector3 direction, Color color)
    {
        Debug.DrawRay(start, direction * 3, color);
    }
}
