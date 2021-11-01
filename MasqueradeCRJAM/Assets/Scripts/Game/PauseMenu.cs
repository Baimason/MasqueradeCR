using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private float holdForExitCooldown;
    [SerializeField] private Image holdProgressImage;
    private ControlMaps inputs;

    void Awake()
    {
        inputs = new ControlMaps();
        inputs.Player.Start.performed += OnStart;
        inputs.Player.Special.started += SpecialHold;
        inputs.Player.Special.performed += SpecialRelease;
    }

    private float holdTimer;

    private void Update()
    {
        float perc = (Time.unscaledTime - holdTimer) / holdForExitCooldown;
        holdProgressImage.fillAmount = perc;
    }

    private void SpecialHold(InputAction.CallbackContext obj)
    {
        holdProgressImage.gameObject.SetActive(true);
        holdTimer = Time.unscaledTime;
    }
    private void SpecialRelease(InputAction.CallbackContext obj)
    {
        holdProgressImage.gameObject.SetActive(false);
        if (holdTimer + holdForExitCooldown < Time.unscaledTime)
        {
            // Exit game.
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
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
