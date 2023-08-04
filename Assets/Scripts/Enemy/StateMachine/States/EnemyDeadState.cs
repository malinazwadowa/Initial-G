using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{

    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData) : base(enemy, stateMachine, enemyData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySound(AudioManagerClips.Death, enemy.audioSource);
        GameObject.Destroy(enemy.gameObject, 10);
        enemy.animator.SetBool(EnemyAnimatorParameters.IsDead, true);
        enemy.enabled = false;
        enemy.agent.enabled = false;
        enemy.GetComponent<CapsuleCollider2D>().enabled = false;
        enemy.GetComponent<Transform>().localScale = new Vector3(1, 0.7f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
