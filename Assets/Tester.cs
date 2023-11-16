using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public Transform player;
    public Transform obstacle;

    Vector3 directionTowardsPlayer;
    Vector3 dirToObstacle;
    Collider2D collideer;
    // Start is called before the first frame update//
    // dodaj x i y modifiery jak w movementcontroljuz, czek przy wygryciu obiektu az do scenriusza a bez obiektu wtedy reset modfifierow

    void Start()
    {
        collideer = obstacle.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        directionTowardsPlayer = (player.transform.position - transform.position).normalized;
        AvoidObstacles2(collideer);
    }

    private void AvoidObstacles2( Collider2D collider)
    {
        Collider2D obstacle = collider;
        //Vector3 dirToObstacle = Vector3.zero;

        dirToObstacle = obstacle.transform.position - transform.position;
        
        //Debug.Log($"Dir to obstacle is: {dirToObstacle}");
        //Debug.Log($"Dir to player is: {directionTowardsPlayer}");

        float xDif = Mathf.Abs(dirToObstacle.x) - obstacle.bounds.extents.x;
        float yDif = Mathf.Abs(dirToObstacle.y) - obstacle.bounds.extents.y;
       // Debug.Log($"(xdif to: {xDif}, ydifto: {yDif})");
        dirToObstacle.Normalize();
        if (xDif > yDif)
        {
            if (dirToObstacle.x > 0)
            {
                //on left
                if (directionTowardsPlayer.x < 0) { return; }

                if (directionTowardsPlayer.y > dirToObstacle.y)
                {
                    //go up 
                    Debug.Log("ide w gore");
                }
                else
                {
                    //go down
                    Debug.Log("ide w dol");
                }
            }
            else
            {
                //on right
                if (directionTowardsPlayer.x > 0) { return; }

                if (directionTowardsPlayer.y > dirToObstacle.y)
                {
                    //go up 
                    Debug.Log("ide w gore");
                }
                else
                {
                    //go down
                    Debug.Log("ide w dol");
                }
            }
        }
        else
        {
            if (dirToObstacle.y > 0)
            {
                //under
                Debug.Log("under");
                if (directionTowardsPlayer.y < 0) { return; }

                if (directionTowardsPlayer.x > dirToObstacle.x)
                {
                    //goright
                    Debug.Log("ide w prawo");
                }
                else
                {
                    //goleft
                    Debug.Log("ide w lewo");
                }
            }
            else
            {
                //above
                if (directionTowardsPlayer.y > 0) { return; }

                if (directionTowardsPlayer.x > dirToObstacle.x)
                {
                    //goright
                    Debug.Log("ide w prawo");
                }
                else
                {
                    //goleft
                    Debug.Log("ide w lewo");
                }
            }
        }
    }
}
