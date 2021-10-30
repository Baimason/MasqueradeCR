using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private ControlMaps inputs;

    void Awake()
    {
        inputs = new ControlMaps();
        inputs.Player.Start.performed += OnStart;
    }

    private void OnStart(InputAction.CallbackContext obj)
    {
        GameSystem.ClosePause();
    }

    internal void Set(bool state)
    {
        gameObject.SetActive(state);

        if (state) inputs.Enable();
        else inputs.Disable();
    }
}
