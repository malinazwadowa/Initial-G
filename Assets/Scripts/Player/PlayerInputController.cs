using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputController : MonoBehaviour
{
    public UnityAction onAttackInputPressed;

    private PlayerInputActions inputActions;
    private PlayerMovementController playerMovementController;

    private bool movementInputEnabled = true;

    public static event Action OnPausePressed;

    public void Init(PlayerMovementController playerMovementController, PlayerInputActions playerInputActions)
    {
        this.playerMovementController = playerMovementController;
        this.inputActions = playerInputActions;
        this.inputActions.Enable();
    }
    private void Update()
    {
        HandleRunning();
        HandlePause();
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
            inputActions.PlayerMovement.Enable();
        }
        else
        {
            inputActions.PlayerMovement.Disable();
        } 
    }

    public void HandleMovement()
    {
        if (!movementInputEnabled) { return; }

        playerMovementController.MovePlayer(inputActions.PlayerMovement.Movement.ReadValue<Vector2>());
    }

    public void HandleRunning()
    {
        if (!movementInputEnabled) { return; }

        playerMovementController.SetRun(inputActions.PlayerMovement.Run.IsPressed());
    }

    public void HandlePause()
    {
        if (inputActions.PlayerMovement.Pause.WasPerformedThisFrame())
        {
            OnPausePressed?.Invoke();
        }
    }

    private void HandleAttackInput()
    {
        if (inputActions.PlayerMovement.Attack.WasPerformedThisFrame())
        {
            onAttackInputPressed?.Invoke();
        }
    }
}
