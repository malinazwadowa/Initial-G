using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyCombatState : EnemyState
{
    float timeSinceLastAttack = 3;

    public EnemyCombatState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData) : base(enemy, stateMachine, enemyData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        enemy.animator.SetBool(EnemyAnimatorParameters.IsCombating, true);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animator.SetBool(EnemyAnimatorParameters.IsCombating, false);
        enemy.animator.SetBool(EnemyAnimatorParameters.IsAttacking, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timeSinceLastAttack += Time.deltaTime;
        //Turns enemy toward player.
        enemy.animator.SetInteger(EnemyAnimatorParameters.DirectionID, enemy.GetFacingDirection(enemy.GetDirectionTowardsPlayer()));

        if (enemy.GetDistanceToPlayer() > 1.3f)
        {
            stateMachine.ChangeState(enemy.ChaseState);
        }

        //Performs attack, actual attack is triggered by animation event.
        if(timeSinceLastAttack > 2 && enemyData.attackingAllowed)
        {
            timeSinceLastAttack = 0;
            enemy.animator.SetBool(EnemyAnimatorParameters.IsAttacking, true);
            enemy.animator.SetBool(EnemyAnimatorParameters.IsCombating, false);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}