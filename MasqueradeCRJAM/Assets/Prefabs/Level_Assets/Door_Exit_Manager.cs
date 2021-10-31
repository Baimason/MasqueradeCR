using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door_Exit_Manager : MonoBehaviour
{
    private Scene activeScene;
    [SerializeField, Tooltip("The next Scene where the character is loaded after")]
    public string NextLevelName; 
    private Scene DoorLevel;
    private PlayerMovement player;
    //public GameObject TutorialArrow;
    public FadeController FadeController;

    public ControlMaps inputs;
    private bool IsCollidingPlayer = false;

    private void Awake()
    {
        inputs = new ControlMaps();
    }

    void Start()
    {
        findPLayer();
        activeScene = SceneManager.GetActiveScene();
    }

    public void findPLayer()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(NextLevelName);
    }

    public void ShowTutorial(bool show)
    {
        //TutorialArrow.SetActive(show); 
        if (show)
        {
            FadeController.startFadingIn();
        }
        else {
            FadeController.startFadingOut();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            IsCollidingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            IsCollidingPlayer = false;
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (IsCollidingPlayer)
            {
                ShowTutorial(true);
                var keyUp = player.inputs.Player.Movement.ReadValue<Vector2>().y > 0;
                if (keyUp)
                {
                    loadNextScene();
                }
            }
            else {
                ShowTutorial(false);
            }
        }
    }
}
