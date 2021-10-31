using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Canvas introCanvas;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private float titleWait = 2f;
    [SerializeField] private float waitForStart = 5f;

    [SerializeField] private Animator titleAnimator;
    ControlMaps inputs;
    float waitTimer;

    void Awake()
    {
        inputs = new ControlMaps();
        inputs.Enable();
        inputs.Player.Jump.performed += OnAccept;
        inputs.Player.Special.performed += OnCancel;
        inputs.Player.Start.performed += OnAccept;
        SetIntro();
    }

    private void SetIntro()
    {
        introCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
    }

    public void DismissIntro()
    {
        introCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);

        waitTimer = Time.unscaledTime + titleWait;
    }

    private void OnAccept(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (waitTimer > Time.unscaledTime) return;

        if (introCanvas.gameObject.activeSelf)
        {
            DismissIntro();
        }
        else
        {
            // Interact with title screens.
            if (titleScreen.activeSelf)
            {
                // Go to game.
                titleAnimator.SetTrigger("StartGame");
                waitTimer = Time.unscaledTime + waitForStart + 1f;
            }
            else
            {
                // Dismiss credits.
                titleScreen.SetActive(true);
                creditsScreen.SetActive(false);
            }
        }
    }

    private void OnCancel(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (waitTimer > Time.unscaledTime) return;

        if (titleScreen.activeSelf)
        {
            titleScreen.SetActive(false);
            creditsScreen.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
