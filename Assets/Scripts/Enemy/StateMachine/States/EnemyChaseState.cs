using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyChaseState : EnemyState
{
    private float timer;
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData) : base(enemy, stateMachine, enemyData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.radius = enemyData.chaseObsRadius;
        enemy.agent.stoppingDistance = enemyData.chaseStopDistance;
        enemy.agent.ResetPath();
        enemy.animator.SetBool(EnemyAnimatorParameters.IsChasing, true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animator.SetBool(EnemyAnimatorParameters.IsChasing, false);
        enemy.animator.SetBool(EnemyAnimatorParameters.IsLooking, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer += Time.deltaTime;

        ChasePlayer();
        
        if (enemy.GetDistanceToPlayer() > 5 && timer > 5)
        {
            timer = 0;
            stateMachine.ChangeState(enemy.WanderState);
        }
        if (enemy.GetDistanceToPlayer() < 1)
        {
            timer = 0;
            stateMachine.ChangeState(enemy.CombatState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void ChasePlayer()
    {
        enemy.agent.SetDestination(enemy.player.transform.position);
        //Animator logic when moving.
        if (enemy.agent.velocity != Vector3.zero)
        {
            enemy.animator.SetBool(EnemyAnimatorParameters.IsChasing, true);
            enemy.animator.SetBool(EnemyAnimatorParameters.IsLooking, false);
            enemy.animator.SetInteger(EnemyAnimatorParameters.DirectionID, enemy.GetFacingDirection(enemy.agent.desiredVelocity));
        }

        //Animator logic when not moving.
        if (enemy.agent.velocity == Vector3.zero)
        {
            enemy.animator.SetBool(EnemyAnimatorParameters.IsLooking, true);
            enemy.animator.SetBool(EnemyAnimatorParameters.IsChasing, false);
            enemy.animator.SetInteger(EnemyAnimatorParameters.DirectionID, enemy.GetFacingDirection(enemy.GetDirectionTowardsPlayer()));
        }
        //Chases player by choosing closes avialble spot(within maximum specified distance) in case player is not on walkable area.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(enemy.player.transform.position, out hit, 2.0f, 1))
        {
            enemy.agent.SetDestination(hit.position);
        }
        else
        {
            Debug.Log("No avilable spot");
        }

    }
}
