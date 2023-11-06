using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuUI : MonoBehaviour
{
    //[SerializeField] private List<Button> myButtons;

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
    // Start is called before the first frame update
    void Start()
    {
        inputActions = PlayerManager.Instance.GetPlayerInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerInput();
    }
    private void HandlePlayerInput()
    {
        if(inputActions == null) { return; }
        if (inputActions.PlayerMovement.CancelAction.WasPerformedThisFrame())
        {
            OnCancelEvent?.Invoke();
        }
        if (inputActions.PlayerMovement.AcceptAction.WasPerformedThisFrame())
        {
            OnAcceptEvent?.Invoke();
        }
    }
}
