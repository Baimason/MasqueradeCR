using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeController : MonoBehaviour
{
    public FadeController FadeController;
    private PlayerMovement player;
    private bool IsCollidingPlayer = false;


    void Start()
    {
        findPLayer();
        //activeScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            IsCollidingPlayer = true;
        }
    }

    public void findPLayer()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player.gameObject)
        {
            IsCollidingPlayer = false;
        }
    }

    public void ShowTutorial(bool show)
    {
        if (show)
        {
            FadeController.startFadingIn();
        }
        else
        {
            FadeController.startFadingOut();
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (IsCollidingPlayer)
            {
                ShowTutorial(true);                
            }
            else
            {
                ShowTutorial(false);
            }
        }
    }
}
