using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    const float MUSIC_VOLUME = 0.2f;

    public static AudioSource LevelMusicInstance;

    [SerializeField] private Canvas introCanvas;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private float titleWait = 2f;
    [SerializeField] private float waitForStart = 5f;

    [SerializeField] private Animator titleAnimator;
    [SerializeField] AudioClip levelMusic;
    [SerializeField] AudioSource titleMusic;
    ControlMaps inputs;
    float waitTimer;

    void Awake()
    {
        GameSystem.ResetState();
        if (LevelMusicInstance != null) Destroy(LevelMusicInstance.gameObject);
        titleMusic.volume = MUSIC_VOLUME;
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
                StartCoroutine(StartGame());
                inputs.Disable();
            }
            else
            {
                // Dismiss credits.
                titleScreen.SetActive(true);
                creditsScreen.SetActive(false);
            }
        }
    }

    private IEnumerator StartGame()
    {
        float wait = waitForStart;
        while (wait > 0)
        {
            wait -= Time.unscaledDeltaTime;
            titleMusic.volume -= Time.unscaledDeltaTime;
            yield return null;
        }

        LevelMusicInstance = new GameObject("Music").AddComponent<AudioSource>();
        LevelMusicInstance.volume = MUSIC_VOLUME;
        LevelMusicInstance.clip = levelMusic;
        LevelMusicInstance.loop = true;
        LevelMusicInstance.spatialBlend = 0;
        LevelMusicInstance.Play();
        DontDestroyOnLoad(LevelMusicInstance.gameObject);

        SceneManager.LoadScene(1);
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
