using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{
    public UnityAction onAttackInputPressed;


    private PlayerInputActions playerInputActions;
    private PlayerMovementController playerMovementController;
    //private PlayerCombatController playerCombatController;

    private bool inputEnabled = true;

    public void Init(PlayerMovementController playerMovementController, PlayerInputActions playerInputActions)
    {
        //this.playerCombatController = playerCombatController;
        this.playerMovementController = playerMovementController;
        this.playerInputActions = playerInputActions;
    }

    private void Update()
    {
        EnableMovementInput(true);
        //HandleInput();
    }
    
    public void EnableMovementInput(bool active)
    {
        inputEnabled = active;

        if (active)
        {
            playerInputActions.PlayerMovement.Enable();
        }
        else
        {
            playerInputActions.PlayerMovement.Disable();
        }
        
        
    }
    public void HandleInput()
    {
        if (!inputEnabled) { return; }

        playerMovementController.SetRun(playerInputActions.PlayerMovement.Run.IsPressed());
    }
    public Vector2 GetMovementInput()
    {
        return playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>();
    }





    private void HandleAttackInput()
    {
        if (playerInputActions.PlayerMovement.Attack.WasPerformedThisFrame())
        {
            //playerCombatController.Attack();
            onAttackInputPressed?.Invoke();
        }
    }
}
