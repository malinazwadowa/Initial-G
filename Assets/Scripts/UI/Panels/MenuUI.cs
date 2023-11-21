using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private UnityEvent OnOpenEvent;
    [SerializeField] private UnityEvent OnCloseEvent;
   
    [SerializeField] private UnityEvent OnAcceptEvent;
    [SerializeField] private UnityEvent OnCancelEvent;

    private PlayerInputActions inputActions;

    public void Close()
    {
        OnCloseEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        OnOpenEvent?.Invoke();
    }

    void Start()
    {
        if(PlayerManager.Instance != null)
        {
            inputActions = PlayerManager.Instance.GetPlayerInputActions();
        }
        else
        {
            inputActions = new PlayerInputActions();
            inputActions.MenuActions.Enable();
        }
    }

    void Update()
    {
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        if(inputActions == null) { return; }
        else if (!inputActions.MenuActions.enabled){ return; }

        if (inputActions.MenuActions.CancelAction.WasPerformedThisFrame())
        {
            OnCancelEvent?.Invoke();
        }
        if (inputActions.MenuActions.AcceptAction.WasPerformedThisFrame())
        {
            OnAcceptEvent?.Invoke();
        }
    }
}