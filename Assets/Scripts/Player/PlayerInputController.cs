using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{
    public UnityAction onAttackInputPressed;

    private PlayerInputActions playerInputActions;
    private PlayerCombatController playerCombatController;

    public void Init(PlayerInputActions playerInputActions, PlayerCombatController playerCombatController)
    {
        this.playerCombatController = playerCombatController;
        this.playerInputActions = playerInputActions;
    }

    private void Update()
    {
    }

    public Vector2 GetMovementInput()
    {
        return playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    private void HandleAttackInput()
    {
        if (playerInputActions.Player.Attack.WasPerformedThisFrame())
        {
            //playerCombatController.Attack();
            onAttackInputPressed?.Invoke();
        }
    }
}
