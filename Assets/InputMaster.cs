// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""MegaMan"",
            ""id"": ""c1653988-1a29-4a66-b2ae-ef4fd646b361"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""435adef7-37ab-4c11-bbca-bec8fedd70a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""955fed29-bf47-4ed4-b072-23a0dbddb549"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bc8359e6-a3c7-40b7-86aa-8bcfe262d060"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""09b8d010-f9ae-46c6-b01d-1a51340d6ded"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""7c1b7d4a-0d5c-4a5f-9c97-80d4b31c5306"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""f3b177b6-4f1c-40be-95ca-e4cd9ae76c46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7346002e-ee8b-4305-9352-6a9d943aed1b"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7c25847-ce34-492e-bfa6-94c23e419599"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow keys"",
                    ""id"": ""3b79396a-212a-4364-9162-727a669742e3"",
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
                    ""id"": ""de185672-042f-41aa-9472-4ced5952c0da"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;RH Arrow Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""97f20e83-6029-4465-b917-3584ada66f9f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;RH Arrow Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""15ae3644-51f7-4f5e-bc22-4faa61022d28"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;RH Arrow Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3f3e8612-3c86-4f30-8e92-6e19804ab701"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;RH Arrow Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD keys"",
                    ""id"": ""c7cae30b-874e-485c-a68f-72b069d249f0"",
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
                    ""id"": ""a0f843ec-74c3-43cf-9364-55efd5a01937"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7f7a8687-3506-417a-a17c-17c21b4c9e78"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1f285bf7-8ed5-482a-97fe-283d9786601c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a579cabb-e617-4f2d-b4fb-f3e35948dc49"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DvorakWASD keys"",
                    ""id"": ""d2076516-d7d7-48be-8d37-6a8707da32b3"",
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
                    ""id"": ""2156624f-8c5f-4c8e-86c9-2729ceebbe62"",
                    ""path"": ""<Keyboard>/comma"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DvorakWASD"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""547fcf34-8ed1-4d8d-8e56-ca8cc9305390"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DvorakWASD"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a1bdb07b-4e1f-473e-940d-c33d1b76d85c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DvorakWASD"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""22196495-a3f3-4203-a510-0e3561066bda"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DvorakWASD"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b7ff1fa7-e84b-4b63-b3ec-3738b42ad189"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""574565d7-0b2e-4c88-a3e7-8e8948f9ac83"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;WASD Keys;DvorakWASD"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c7dc78e-82da-4024-81f2-6db682e4f528"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RH Arrow Keys;WASD Keys"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac0d8b1c-d509-4a70-a389-ff0033f6057b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""129081fb-cc18-4964-b3cd-f2f98355a8d7"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2b84943-41f0-485e-a4b1-03724600f07a"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys;RH Arrow Keys"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""146ba88c-571b-4cf8-930e-cebe764bd299"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;WASD Keys;DvorakWASD"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff16bb36-b3b8-497b-83bd-77830a01671e"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c206137a-e5cb-446a-968e-b8d855e7b36b"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;WASD Keys;DvorakWASD"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b216790-89b3-495e-a6fb-c3c3af3427b3"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys;RH Arrow Keys"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""311b78a0-b01d-4067-b250-8c59abda6335"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32adc784-ba10-4b00-b1f3-1091f734b132"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""RH Arrow Keys;WASD Keys"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fda5f895-96c5-474b-9885-4942a56c9896"",
                    ""path"": ""<Keyboard>/numpadPeriod"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""WASD Keys;LH Arrow Keys;DvorakWASD"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6054ae96-8e0d-4bfd-b563-90ce3aa8b4c4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""LH Arrow Keys;RH Arrow Keys;WASD Keys;DvorakWASD"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""193f44c7-0a84-495a-9638-a325de7d4343"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""LH Arrow Keys"",
            ""bindingGroup"": ""LH Arrow Keys"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""RH Arrow Keys"",
            ""bindingGroup"": ""RH Arrow Keys"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""WASD Keys"",
            ""bindingGroup"": ""WASD Keys"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""DvorakWASD"",
            ""bindingGroup"": ""DvorakWASD"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // MegaMan
        m_MegaMan = asset.FindActionMap("MegaMan", throwIfNotFound: true);
        m_MegaMan_Movement = m_MegaMan.FindAction("Movement", throwIfNotFound: true);
        m_MegaMan_Shoot = m_MegaMan.FindAction("Shoot", throwIfNotFound: true);
        m_MegaMan_Jump = m_MegaMan.FindAction("Jump", throwIfNotFound: true);
        m_MegaMan_Dash = m_MegaMan.FindAction("Dash", throwIfNotFound: true);
        m_MegaMan_Inventory = m_MegaMan.FindAction("Inventory", throwIfNotFound: true);
        m_MegaMan_Menu = m_MegaMan.FindAction("Menu", throwIfNotFound: true);
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

    // MegaMan
    private readonly InputActionMap m_MegaMan;
    private IMegaManActions m_MegaManActionsCallbackInterface;
    private readonly InputAction m_MegaMan_Movement;
    private readonly InputAction m_MegaMan_Shoot;
    private readonly InputAction m_MegaMan_Jump;
    private readonly InputAction m_MegaMan_Dash;
    private readonly InputAction m_MegaMan_Inventory;
    private readonly InputAction m_MegaMan_Menu;
    public struct MegaManActions
    {
        private @InputMaster m_Wrapper;
        public MegaManActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MegaMan_Movement;
        public InputAction @Shoot => m_Wrapper.m_MegaMan_Shoot;
        public InputAction @Jump => m_Wrapper.m_MegaMan_Jump;
        public InputAction @Dash => m_Wrapper.m_MegaMan_Dash;
        public InputAction @Inventory => m_Wrapper.m_MegaMan_Inventory;
        public InputAction @Menu => m_Wrapper.m_MegaMan_Menu;
        public InputActionMap Get() { return m_Wrapper.m_MegaMan; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MegaManActions set) { return set.Get(); }
        public void SetCallbacks(IMegaManActions instance)
        {
            if (m_Wrapper.m_MegaManActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_MegaManActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_MegaManActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_MegaManActionsCallbackInterface.OnMovement;
                @Shoot.started -= m_Wrapper.m_MegaManActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_MegaManActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_MegaManActionsCallbackInterface.OnShoot;
                @Jump.started -= m_Wrapper.m_MegaManActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MegaManActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MegaManActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_MegaManActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MegaManActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MegaManActionsCallbackInterface.OnDash;
                @Inventory.started -= m_Wrapper.m_MegaManActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_MegaManActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_MegaManActionsCallbackInterface.OnInventory;
                @Menu.started -= m_Wrapper.m_MegaManActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_MegaManActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_MegaManActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_MegaManActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public MegaManActions @MegaMan => new MegaManActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_LHArrowKeysSchemeIndex = -1;
    public InputControlScheme LHArrowKeysScheme
    {
        get
        {
            if (m_LHArrowKeysSchemeIndex == -1) m_LHArrowKeysSchemeIndex = asset.FindControlSchemeIndex("LH Arrow Keys");
            return asset.controlSchemes[m_LHArrowKeysSchemeIndex];
        }
    }
    private int m_RHArrowKeysSchemeIndex = -1;
    public InputControlScheme RHArrowKeysScheme
    {
        get
        {
            if (m_RHArrowKeysSchemeIndex == -1) m_RHArrowKeysSchemeIndex = asset.FindControlSchemeIndex("RH Arrow Keys");
            return asset.controlSchemes[m_RHArrowKeysSchemeIndex];
        }
    }
    private int m_WASDKeysSchemeIndex = -1;
    public InputControlScheme WASDKeysScheme
    {
        get
        {
            if (m_WASDKeysSchemeIndex == -1) m_WASDKeysSchemeIndex = asset.FindControlSchemeIndex("WASD Keys");
            return asset.controlSchemes[m_WASDKeysSchemeIndex];
        }
    }
    private int m_DvorakWASDSchemeIndex = -1;
    public InputControlScheme DvorakWASDScheme
    {
        get
        {
            if (m_DvorakWASDSchemeIndex == -1) m_DvorakWASDSchemeIndex = asset.FindControlSchemeIndex("DvorakWASD");
            return asset.controlSchemes[m_DvorakWASDSchemeIndex];
        }
    }
    public interface IMegaManActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
