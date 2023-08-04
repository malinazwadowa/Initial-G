using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected EnemyData enemyData;

    protected float startTime;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
    }

    public virtual void Enter()
    {
        DoChecks();
    }

    public virtual void Exit() 
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
