using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryingStance : PlayerState
{
    private float chargeStartTime;
    private float chargeTime;
    private bool isThrowing;
    private bool isCarrying;
    private bool hasThrown;
    public PlayerCarryingStance(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
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
        isCarrying = true;
        hasThrown = false;
    }

    public override void Exit()
    {
        base.Exit();
        //Checks if player was carrying a bomb and drops it.
        if(isCarrying || isThrowing && !hasThrown)
        {
            PlayerCombatController.Instance.DropBomb();
        }
        player.animator.SetBool(PlayerAnimatorParameters.IsCarrying, false);
        player.animator.SetBool(PlayerAnimatorParameters.IsHolding, false);
        player.animator.SetBool(PlayerAnimatorParameters.IsThrowing, false);
        isThrowing = false;
        hasThrown = false;
        playerData.attackingAllowed = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Checks if player is carrying a bomb and walking.
        if (player.playerInputActions.Player.Movement.IsPressed() && !isThrowing && !hasThrown)
        {
            player.animator.SetBool(PlayerAnimatorParameters.IsHolding, false);
            player.animator.SetBool(PlayerAnimatorParameters.IsCarrying, true);
        }
        //Checks if player is stationary with a bomb.
        else if (!hasThrown)
        {
            player.animator.SetBool(PlayerAnimatorParameters.IsHolding, true);
            player.animator.SetBool(PlayerAnimatorParameters.IsCarrying, false);
        }
        //Checks if player started charging throw and measures time spent charging.
        if (player.playerInputActions.Player.Throw.WasPressedThisFrame() && !hasThrown && !isThrowing)
        {
            isThrowing = true;
            isCarrying = false;
            chargeStartTime = Time.time;
        }
        //Checks if player stopped charging the throw and throws the bomb based on charged time.
        if (player.playerInputActions.Player.Throw.WasReleasedThisFrame() && isThrowing && !hasThrown)
        {
            hasThrown = true;
            chargeTime = Time.time - chargeStartTime;
            player.animator.SetBool(PlayerAnimatorParameters.IsHolding, false);
            player.animator.SetBool(PlayerAnimatorParameters.IsThrowing, true);
            PlayerCombatController.Instance.ThrowBomb(1.5f * chargeTime);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //Allows for movement of player while carrying the bomb.
        if(!isThrowing && !hasThrown)
        {
            PlayerDirectionController.Instance.MovePlayer(playerData.walkSpeed);
        }
        //Minimal movement to trigger direction change when charging shot.
        else if (!hasThrown)
        {
            PlayerDirectionController.Instance.MovePlayer(playerData.walkSpeed /1000);
        }
    }
}
