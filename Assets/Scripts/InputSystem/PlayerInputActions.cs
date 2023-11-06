//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""GameplayActions"",
            ""id"": ""fc3a4361-da10-4593-b289-9279c6aad862"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""95f30fe4-8f69-45f7-9b01-60be94056994"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""e76da4b2-d9b6-4527-be71-8a16c37f73cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""8858e68e-58e8-46c9-ac88-8f205153cec1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Stealth"",
                    ""type"": ""Button"",
                    ""id"": ""b1655c05-3cb7-47d2-88f8-ff34dd0dc7ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""e71db7c3-d35a-4b40-a526-610097ca4ecf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""8491de4b-9977-470e-b956-c87bc15f0cc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CancelAction"",
                    ""type"": ""Button"",
                    ""id"": ""b040f5e8-f1ee-47a4-96d2-e26c32805654"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AcceptAction"",
                    ""type"": ""Button"",
                    ""id"": ""ebb3d932-ba19-4732-b81a-801759d36abd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""68f4d0ad-bd6f-4760-b340-b26cc79d1a6e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""804372bb-99e6-474f-9e3f-93acbaff2ee0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""885e1e60-4266-437a-9b65-e1eabc144301"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""3d8488a1-f576-4282-9bcb-ab0e7f182668"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""1a80409a-9c4a-492c-b9d0-2e736fb64992"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b26ad95e-7945-4825-b80f-d417802efb31"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e245fc6e-f8b2-4968-8e8d-18a37d385f0e"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fe700e48-8fe0-48b8-bea5-c136267f9603"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""67e3c9c8-bb69-44c2-84c3-6233ee8aaeec"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e6d75dbe-3d2e-4611-bdd8-384a7ac81503"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""dcf76332-4c46-4521-8832-8c899466d15a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0819d9c-7f6d-4c53-aaf7-a8e23b56c608"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad3057c3-f681-4044-b36e-daed957bc74f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9380b04-1f4c-4bf3-b429-4b1deb23bdec"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Stealth"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66fae11d-056f-44d5-a8b2-c6d6f4f24796"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c2e2f63-06b4-43e0-a675-6aa5b11b2886"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b88b3b80-93dc-4741-bf5f-b3d5a7b922f1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CancelAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31879184-0fad-4235-b557-da132e2eaa3e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AcceptAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PopUpActions"",
            ""id"": ""ca34882f-7a5d-438d-9e9d-d81ca812345f"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""MenuActions"",
            ""id"": ""501f474f-bf92-41ed-ba4e-10a0c31b5ae0"",
            ""actions"": [
                {
                    ""name"": ""AcceptAction"",
                    ""type"": ""Button"",
                    ""id"": ""f995f3aa-9481-4486-9baf-d8cc6e32c076"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CancelAction"",
                    ""type"": ""Button"",
                    ""id"": ""a3c16359-3ea2-4cb9-a79e-350d9bc38552"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dupa"",
                    ""type"": ""Button"",
                    ""id"": ""b893688d-71b0-4881-a3ea-449164d5c5f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38be5d15-86a4-417a-bd50-e680be20ce78"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CancelAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""882125d8-0bd8-45b1-a324-03169d74d49f"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""AcceptAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f70eb7d-c797-4a4b-81bb-7bdaeb7ca713"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dupa"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        }
    ]
}");
        // GameplayActions
        m_GameplayActions = asset.FindActionMap("GameplayActions", throwIfNotFound: true);
        m_GameplayActions_Movement = m_GameplayActions.FindAction("Movement", throwIfNotFound: true);
        m_GameplayActions_Run = m_GameplayActions.FindAction("Run", throwIfNotFound: true);
        m_GameplayActions_Attack = m_GameplayActions.FindAction("Attack", throwIfNotFound: true);
        m_GameplayActions_Stealth = m_GameplayActions.FindAction("Stealth", throwIfNotFound: true);
        m_GameplayActions_Throw = m_GameplayActions.FindAction("Throw", throwIfNotFound: true);
        m_GameplayActions_PickUp = m_GameplayActions.FindAction("PickUp", throwIfNotFound: true);
        m_GameplayActions_CancelAction = m_GameplayActions.FindAction("CancelAction", throwIfNotFound: true);
        m_GameplayActions_AcceptAction = m_GameplayActions.FindAction("AcceptAction", throwIfNotFound: true);
        // PopUpActions
        m_PopUpActions = asset.FindActionMap("PopUpActions", throwIfNotFound: true);
        // MenuActions
        m_MenuActions = asset.FindActionMap("MenuActions", throwIfNotFound: true);
        m_MenuActions_AcceptAction = m_MenuActions.FindAction("AcceptAction", throwIfNotFound: true);
        m_MenuActions_CancelAction = m_MenuActions.FindAction("CancelAction", throwIfNotFound: true);
        m_MenuActions_Dupa = m_MenuActions.FindAction("Dupa", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GameplayActions
    private readonly InputActionMap m_GameplayActions;
    private List<IGameplayActionsActions> m_GameplayActionsActionsCallbackInterfaces = new List<IGameplayActionsActions>();
    private readonly InputAction m_GameplayActions_Movement;
    private readonly InputAction m_GameplayActions_Run;
    private readonly InputAction m_GameplayActions_Attack;
    private readonly InputAction m_GameplayActions_Stealth;
    private readonly InputAction m_GameplayActions_Throw;
    private readonly InputAction m_GameplayActions_PickUp;
    private readonly InputAction m_GameplayActions_CancelAction;
    private readonly InputAction m_GameplayActions_AcceptAction;
    public struct GameplayActionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public GameplayActionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_GameplayActions_Movement;
        public InputAction @Run => m_Wrapper.m_GameplayActions_Run;
        public InputAction @Attack => m_Wrapper.m_GameplayActions_Attack;
        public InputAction @Stealth => m_Wrapper.m_GameplayActions_Stealth;
        public InputAction @Throw => m_Wrapper.m_GameplayActions_Throw;
        public InputAction @PickUp => m_Wrapper.m_GameplayActions_PickUp;
        public InputAction @CancelAction => m_Wrapper.m_GameplayActions_CancelAction;
        public InputAction @AcceptAction => m_Wrapper.m_GameplayActions_AcceptAction;
        public InputActionMap Get() { return m_Wrapper.m_GameplayActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActionsActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Stealth.started += instance.OnStealth;
            @Stealth.performed += instance.OnStealth;
            @Stealth.canceled += instance.OnStealth;
            @Throw.started += instance.OnThrow;
            @Throw.performed += instance.OnThrow;
            @Throw.canceled += instance.OnThrow;
            @PickUp.started += instance.OnPickUp;
            @PickUp.performed += instance.OnPickUp;
            @PickUp.canceled += instance.OnPickUp;
            @CancelAction.started += instance.OnCancelAction;
            @CancelAction.performed += instance.OnCancelAction;
            @CancelAction.canceled += instance.OnCancelAction;
            @AcceptAction.started += instance.OnAcceptAction;
            @AcceptAction.performed += instance.OnAcceptAction;
            @AcceptAction.canceled += instance.OnAcceptAction;
        }

        private void UnregisterCallbacks(IGameplayActionsActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Stealth.started -= instance.OnStealth;
            @Stealth.performed -= instance.OnStealth;
            @Stealth.canceled -= instance.OnStealth;
            @Throw.started -= instance.OnThrow;
            @Throw.performed -= instance.OnThrow;
            @Throw.canceled -= instance.OnThrow;
            @PickUp.started -= instance.OnPickUp;
            @PickUp.performed -= instance.OnPickUp;
            @PickUp.canceled -= instance.OnPickUp;
            @CancelAction.started -= instance.OnCancelAction;
            @CancelAction.performed -= instance.OnCancelAction;
            @CancelAction.canceled -= instance.OnCancelAction;
            @AcceptAction.started -= instance.OnAcceptAction;
            @AcceptAction.performed -= instance.OnAcceptAction;
            @AcceptAction.canceled -= instance.OnAcceptAction;
        }

        public void RemoveCallbacks(IGameplayActionsActions instance)
        {
            if (m_Wrapper.m_GameplayActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActionsActions @GameplayActions => new GameplayActionsActions(this);

    // PopUpActions
    private readonly InputActionMap m_PopUpActions;
    private List<IPopUpActionsActions> m_PopUpActionsActionsCallbackInterfaces = new List<IPopUpActionsActions>();
    public struct PopUpActionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PopUpActionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_PopUpActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PopUpActionsActions set) { return set.Get(); }
        public void AddCallbacks(IPopUpActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_PopUpActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PopUpActionsActionsCallbackInterfaces.Add(instance);
        }

        private void UnregisterCallbacks(IPopUpActionsActions instance)
        {
        }

        public void RemoveCallbacks(IPopUpActionsActions instance)
        {
            if (m_Wrapper.m_PopUpActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPopUpActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_PopUpActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PopUpActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PopUpActionsActions @PopUpActions => new PopUpActionsActions(this);

    // MenuActions
    private readonly InputActionMap m_MenuActions;
    private List<IMenuActionsActions> m_MenuActionsActionsCallbackInterfaces = new List<IMenuActionsActions>();
    private readonly InputAction m_MenuActions_AcceptAction;
    private readonly InputAction m_MenuActions_CancelAction;
    private readonly InputAction m_MenuActions_Dupa;
    public struct MenuActionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public MenuActionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @AcceptAction => m_Wrapper.m_MenuActions_AcceptAction;
        public InputAction @CancelAction => m_Wrapper.m_MenuActions_CancelAction;
        public InputAction @Dupa => m_Wrapper.m_MenuActions_Dupa;
        public InputActionMap Get() { return m_Wrapper.m_MenuActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActionsActions set) { return set.Get(); }
        public void AddCallbacks(IMenuActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_MenuActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MenuActionsActionsCallbackInterfaces.Add(instance);
            @AcceptAction.started += instance.OnAcceptAction;
            @AcceptAction.performed += instance.OnAcceptAction;
            @AcceptAction.canceled += instance.OnAcceptAction;
            @CancelAction.started += instance.OnCancelAction;
            @CancelAction.performed += instance.OnCancelAction;
            @CancelAction.canceled += instance.OnCancelAction;
            @Dupa.started += instance.OnDupa;
            @Dupa.performed += instance.OnDupa;
            @Dupa.canceled += instance.OnDupa;
        }

        private void UnregisterCallbacks(IMenuActionsActions instance)
        {
            @AcceptAction.started -= instance.OnAcceptAction;
            @AcceptAction.performed -= instance.OnAcceptAction;
            @AcceptAction.canceled -= instance.OnAcceptAction;
            @CancelAction.started -= instance.OnCancelAction;
            @CancelAction.performed -= instance.OnCancelAction;
            @CancelAction.canceled -= instance.OnCancelAction;
            @Dupa.started -= instance.OnDupa;
            @Dupa.performed -= instance.OnDupa;
            @Dupa.canceled -= instance.OnDupa;
        }

        public void RemoveCallbacks(IMenuActionsActions instance)
        {
            if (m_Wrapper.m_MenuActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMenuActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_MenuActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MenuActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MenuActionsActions @MenuActions => new MenuActionsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnStealth(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnCancelAction(InputAction.CallbackContext context);
        void OnAcceptAction(InputAction.CallbackContext context);
    }
    public interface IPopUpActionsActions
    {
    }
    public interface IMenuActionsActions
    {
        void OnAcceptAction(InputAction.CallbackContext context);
        void OnCancelAction(InputAction.CallbackContext context);
        void OnDupa(InputAction.CallbackContext context);
    }
}
