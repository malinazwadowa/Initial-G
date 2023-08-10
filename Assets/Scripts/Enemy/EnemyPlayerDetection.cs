using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for detecting the player based on their state and updating the state of the enemy accordingly. 
public class EnemyPlayerDetection : MonoBehaviour
{
    private Player player;
    private Enemy enemy;

    [SerializeField] private float generalDetectionRange = 5;
    [SerializeField] private float stealthDetectionRange = 3;
    [SerializeField] private float runDetectionRange = 6;
    [SerializeField] private float maxDistanceToSpawn = 15;
    void Awake()
    {
        player = FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();


    }

    void Update()
    { /*
        if (enemy.GetDistanceToSpawn() < maxDistanceToSpawn || enemy.GetDistanceToPlayer() < 3)
        {
            if (enemy.StateMachine.CurrentState == enemy.WanderState)
            {
                if (player.StateMachine.CurrentState == player.StealthState)
                {
                    if (enemy.GetDistanceToPlayer() < stealthDetectionRange)
                    {
                        enemy.StateMachine.ChangeState(enemy.ChaseState);
                    }
                }

                else if (player.StateMachine.CurrentState == player.RunningState)
                {
                    if (enemy.GetDistanceToPlayer() < runDetectionRange)
                    {
                        enemy.StateMachine.ChangeState(enemy.ChaseState);
                    }
                }

                else if (enemy.GetDistanceToPlayer() < generalDetectionRange)
                {
                    enemy.StateMachine.ChangeState(enemy.ChaseState);
                }
            }
        }*/
    } 

}
