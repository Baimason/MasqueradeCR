using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementVector;
    public UnityAction jumpAction;
    public UnityAction specialAction;

    private ControlMaps inputActions;

    void Start()
    {
        inputActions = new ControlMaps();
        inputActions.Enable();
        
        inputActions.Player.Jump.performed += PerformJump;
        inputActions.Player.Special.performed += PerformSpecial;
    }

    private void PerformSpecial(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        specialAction.Invoke();
    }

    private void PerformJump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        jumpAction.Invoke();
    }

    void Update()
    {
        MovementVector = inputActions.Player.Movement.ReadValue<Vector2>();
    }
}
