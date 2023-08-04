using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        playerData.attackingAllowed = false;
        player.animator.SetBool(PlayerAnimatorParameters.IsHit, true);
        //Temporary randomizer for sounds.
        int randomNumber = UnityEngine.Random.Range(1, 5);
        switch (randomNumber)
        {
            case 1:
                AudioManager.Instance.PlaySound(AudioManagerClips.GetHit1, player.audioSource);
                break;
            case 2:
                AudioManager.Instance.PlaySound(AudioManagerClips.GetHit2, player.audioSource);
                break;
            case 3:
                AudioManager.Instance.PlaySound(AudioManagerClips.GetHit3, player.audioSource);
                break;
            case 4:
                AudioManager.Instance.PlaySound(AudioManagerClips.GetHit4, player.audioSource);
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();

        playerData.attackingAllowed = true;
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
