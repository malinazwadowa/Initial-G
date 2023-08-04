using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerRunningState : PlayerState
{
    public PlayerRunningState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
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
        
        AudioManager.Instance.PlaySound(AudioManagerClips.Run, player.audioSource);
        player.animator.SetBool(PlayerAnimatorParameters.IsRunning, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.audioSource.loop = false;
        player.audioSource.Stop();
        player.animator.SetBool(PlayerAnimatorParameters.IsRunning, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Changes state based on input.
        if (!player.playerInputActions.Player.Run.IsPressed() || !player.playerInputActions.Player.Movement.IsPressed())
        {
            stateMachine.ChangeState(player.WalkingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        PlayerDirectionController.Instance.MovePlayer(playerData.runSpeed);
    }
}
