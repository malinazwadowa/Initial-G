using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public UnityAction onAttackInputPressed;

    private PlayerInputActions playerInputActions;
    private PlayerMovementController playerMovementController;
    //private PlayerCombatController playerCombatController;

    private bool movementInputEnabled = true;

    public void Init(PlayerMovementController playerMovementController, PlayerInputActions playerInputActions)
    {
        //this.playerCombatController = playerCombatController;
        this.playerMovementController = playerMovementController;
        this.playerInputActions = playerInputActions;

        this.playerInputActions.Enable();
    }

    private void Update()
    {
        HandleRunning();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void EnableMovementInput(bool active)
    {
        movementInputEnabled = active;

        if (active)
        {
            playerInputActions.PlayerMovement.Enable();
        }
        else
        {
            playerInputActions.PlayerMovement.Disable();
        } 
    }

    public void HandleMovement()
    {
        if (!movementInputEnabled) { return; }

        Debug.Log(playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>());

        playerMovementController.MovePlayer(playerInputActions.PlayerMovement.Movement.ReadValue<Vector2>());
    }

    public void HandleRunning()
    {
        if (!movementInputEnabled) { return; }

        playerMovementController.SetRun(playerInputActions.PlayerMovement.Run.IsPressed());
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
