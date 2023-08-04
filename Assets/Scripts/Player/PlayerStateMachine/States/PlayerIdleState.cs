using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.animator.SetBool(PlayerAnimatorParameters.IsIdling, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.animator.SetBool(PlayerAnimatorParameters.IsIdling, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.playerInputActions.Player.Movement.IsPressed())
        {
            stateMachine.ChangeState(player.WalkingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
