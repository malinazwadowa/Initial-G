using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyWanderState : EnemyState
{
    public EnemyWanderState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData) : base(enemy, stateMachine, enemyData)
    {
    }
    private float patrolRange = 4;
    private float lookAroundTime = 2;

    private bool isLookingAround = false;
    private float timer;

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.ResetPath();
        enemy.agent.radius = enemyData.wanderObsRadius;
        enemy.agent.stoppingDistance = enemyData.wanderStopDistance;
        enemy.animator.SetBool(EnemyAnimatorParameters.IsWandering, true);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.animator.SetBool(EnemyAnimatorParameters.IsWandering, false);
        enemy.animator.SetBool(EnemyAnimatorParameters.IsLooking, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Update animation while moving.
        if (enemy.agent.desiredVelocity != Vector3.zero)
        {
            enemy.animator.SetInteger(EnemyAnimatorParameters.DirectionID, enemy.GetFacingDirection(enemy.agent.desiredVelocity));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Wander();
    }
    //Generates random destination on walkable area mask, within desired range of specified point.
    private bool GenerateRandomViableDestination(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint;
        NavMeshHit hit;

        //Generates any random point, only checking if minimal distance to travel rule is met.
        do
        {
            randomPoint = center + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * range;
        }
        //Minimal distance check.
        while (Vector2.Distance(enemy.transform.position, randomPoint) < patrolRange - 1);

        //Provides point closest to generated one but on the walkable area mask.
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, 1))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    //Makes enemy wander in random directions around the spawn point, stopping and looking around.
    private void Wander()
    {
        //Checks if destination was reached and if enemy is no longer looking around
        if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance || (enemy.agent.velocity == Vector3.zero && isLookingAround == false))
        {
            Vector3 point;
            timer += Time.deltaTime;
            isLookingAround = true;

            enemy.animator.SetBool(EnemyAnimatorParameters.IsLooking, true);
            enemy.animator.SetBool(EnemyAnimatorParameters.IsWandering, false);

            //Generates another viable destination and moves towards it.
            if (GenerateRandomViableDestination(enemyData.initalSpawnPositon, patrolRange, out point) && timer >= lookAroundTime)
            {
                timer = 0;
                isLookingAround = false;

                enemy.animator.SetBool(EnemyAnimatorParameters.IsLooking, false);
                enemy.animator.SetBool(EnemyAnimatorParameters.IsWandering, true);

                Debug.DrawRay(point, Vector3.up, Color.blue, 10.0f); 

                enemy.agent.SetDestination(point);                
            }
        }
    }
}
