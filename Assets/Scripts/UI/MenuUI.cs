using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button firstSelected;

    [SerializeField] private UnityEvent OnOpenEvent;
    [SerializeField] private UnityEvent OnCloseEvent;
   
    [SerializeField] public UnityEvent OnAcceptEvent;
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

        if(firstSelected != null)
        {
            firstSelected.Select();
        }
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
            inputActions.UI.Enable();
        }
    }

    void Update()
    {
        HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        if(inputActions == null) { return; }
        else if (!inputActions.UI.enabled){ return; }

        if (inputActions.UI.CancelAction.WasPerformedThisFrame())
        {
            OnCancelEvent?.Invoke();
        }

        if (inputActions.UI.AcceptAction.WasPerformedThisFrame())
        {
            OnAcceptEvent?.Invoke();
        }
    }

    public void SetFirstSelected(Button button) //might need to be gameobject for toggles and sliders 
    {
        firstSelected = button;
        firstSelected.Select();
    }
}
