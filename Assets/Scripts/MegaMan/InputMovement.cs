using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MegaManController))]
public class InputMovement : MonoBehaviour
{
    public InputMaster controls;
    private MegaManController controller;
    [Tooltip("When true, output control inputs to console")]
    [SerializeField] private bool m_debug = false;

    private void Awake()
    {
        controls = new InputMaster();
        controller = GetComponent<MegaManController>();
        controls.MegaMan.Movement.performed += ctx => MovePerformed(ctx.ReadValue<Vector2>());
        controls.MegaMan.Movement.canceled += ctx => MoveCanceled(ctx.ReadValue<Vector2>());
        controls.MegaMan.Shoot.performed += _ => ShootPerformed();
        controls.MegaMan.Shoot.canceled += _ => ShootCanceled();
        controls.MegaMan.Jump.performed += _ => JumpPerformed();
        controls.MegaMan.Jump.canceled += _ => JumpCanceled();
        controls.MegaMan.Dash.performed += _ => DashPerformed();
        controls.MegaMan.Dash.canceled += _ => DashCanceled();
        controls.MegaMan.Inventory.performed += _ => InventoryPerformed();
        controls.MegaMan.Inventory.canceled += _ => InventoryCanceled();
        controls.MegaMan.Menu.performed += _ => MenuPerformed();
        controls.MegaMan.Menu.canceled += _ => MenuCanceled();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void MovePerformed(Vector2 direction)
    {
        if (m_debug)
        {
            Debug.Log("MovementPerformed: " + direction);
        }
        controller.SetStickPosition(direction);
    }

    void MoveCanceled(Vector2 direction)
    {
        if (m_debug)
        {
            Debug.Log("MovementCanceled: " + direction);
        }
        controller.SetStickPosition(Vector2.zero);
    }

    void ShootPerformed()
    {
        if (m_debug)
        {
            Debug.Log("ShootPerformed");
        }
        controller.ShootButtonPressed();
    }

    void ShootCanceled()
    {
        if (m_debug)
        {
            Debug.Log("ShootCanceled");
        }
        controller.ShootButtonReleased();
    }

    void JumpPerformed()
    {
        if (m_debug)
        {
            Debug.Log("JumpPerformed");
        }
        controller.JumpButtonPressed();
    }

    void JumpCanceled()
    {
        if (m_debug)
        {
            Debug.Log("JumpCanceled");
        }
        controller.JumpButtonReleased();
    }

    void DashPerformed()
    {
        if (m_debug)
        {
            Debug.Log("DashPerformed");
        }
        controller.DashButtonPressed();
    }

    void DashCanceled()
    {
        if (m_debug)
        {
            Debug.Log("DashCanceled");
        }
    }

    void InventoryPerformed()
    {
        if (m_debug)
        {
            Debug.Log("InventoryPerformed");
        }
    }

    void InventoryCanceled()
    {
        if (m_debug)
        {
            Debug.Log("InventoryCanceled");
        }
    }

    void MenuPerformed()
    {
        if (m_debug)
        {
            Debug.Log("MenuPerformed");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void MenuCanceled()
    {
        if (m_debug)
        {
            Debug.Log("MenuCanceled");
        }
    }
}
