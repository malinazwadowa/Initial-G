using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWalkingState : PlayerState
{
    public PlayerWalkingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.audioSource.loop = true;
        AudioManager.Instance.PlaySound(AudioManagerClips.Walk, player.audioSource);
        player.animator.SetBool(PlayerAnimatorParameters.IsWalking, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.audioSource.loop = false;
        player.audioSource.Stop();
        player.animator.SetBool(PlayerAnimatorParameters.IsWalking, false);
       // player.audioSource.Stop();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        

        //Changes state based on input.

        if (!player.playerInputActions.Player.Movement.IsPressed())
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (player.playerInputActions.Player.Run.IsPressed() && player.playerInputActions.Player.Movement.IsPressed() && !player.playerInputActions.Player.Stealth.IsPressed())
        {
            stateMachine.ChangeState(player.RunningState);
        }

        if (player.playerInputActions.Player.Stealth.IsPressed() && player.playerInputActions.Player.Movement.IsPressed() && !player.playerInputActions.Player.Run.IsPressed())
        {
            stateMachine.ChangeState(player.StealthState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        PlayerDirectionController.Instance.MovePlayer(playerData.walkSpeed);
    }
}
