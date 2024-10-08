using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public UnityAction onAttackInputPressed;

    [HideInInspector] public PlayerInputActions inputActions { get; private set; }
    private PlayerMovementController playerMovementController;

    private bool movementInputEnabled = true;

    public void Initialize(PlayerMovementController playerMovementController, PlayerInputActions playerInputActions)
    {
        this.playerMovementController = playerMovementController;
        this.inputActions = playerInputActions;
        this.inputActions.GameplayActions.Enable();
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

    public void AllowInput(bool active)
    {
        if(active)
        {
            inputActions.Enable();
        }
        else
        {
            inputActions.Disable();
        }
    }

    public void SwitchActionMap(InputActionMap mapToActivate)
    {
        inputActions.Disable();
        mapToActivate.Enable();
    }

    public void EnableMovementInput(bool active)
    {
        movementInputEnabled = active;

        if (active)
        {
            inputActions.GameplayActions.Enable();
        }
        else
        {
            inputActions.GameplayActions.Disable();
        } 
    }

    public void HandleMovement()
    {
        if (!movementInputEnabled) { return; }

        playerMovementController.MovePlayer(inputActions.GameplayActions.Movement.ReadValue<Vector2>());
    }

    public void HandleRunning()
    {
        if (!movementInputEnabled) { return; }

        playerMovementController.SetRun(inputActions.GameplayActions.Run.IsPressed());
    }

    public void HandlePause()
    {
        if (inputActions.GameplayActions.CancelAction.WasPerformedThisFrame())
        {
            EventManager.OnPauseRequest?.Invoke();
        }
    }
    /*
    private void HandleAttackInput()
    {
        if (inputActions.GameplayActions.Attack.WasPerformedThisFrame())
        {
            onAttackInputPressed?.Invoke();
        }
    } */
}
