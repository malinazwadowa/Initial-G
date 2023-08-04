using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealthState : PlayerState
{
    public PlayerStealthState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.Instance.PlaySound(AudioManagerClips.Stealth, player.audioSource);
    }

    public override void Exit()
    {
        base.Exit();
        player.animator.SetBool(PlayerAnimatorParameters.IsStealthing, false);
        player.animator.SetBool(PlayerAnimatorParameters.IsStealthingIdle, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Changes state and updates animations accordingly to input.
        if (!player.playerInputActions.Player.Stealth.IsPressed())
        {
            stateMachine.ChangeState(player.WalkingState);
        }
        if (player.playerInputActions.Player.Stealth.IsPressed() && !player.playerInputActions.Player.Movement.IsPressed())
        {
            player.animator.SetBool(PlayerAnimatorParameters.IsStealthing, false);
            player.animator.SetBool(PlayerAnimatorParameters.IsStealthingIdle, true);
        }
        if (player.playerInputActions.Player.Stealth.IsPressed() && player.playerInputActions.Player.Movement.IsPressed())
        {
            player.animator.SetBool(PlayerAnimatorParameters.IsStealthingIdle, false);
            player.animator.SetBool(PlayerAnimatorParameters.IsStealthing, true);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        PlayerDirectionController.Instance.MovePlayer(playerData.stealthSpeed);
    }
}
