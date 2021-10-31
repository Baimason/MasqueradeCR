using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillboxManager : MonoBehaviour
{
    private PlayerMovement player;
    private Scene actualScene;//= SceneManager.GetActiveScene();

    void Start()
    {
        findPLayer();
        actualScene = SceneManager.GetActiveScene();
    }

    public void findPLayer()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            SceneManager.LoadScene(actualScene.name);
            //IsCollidingPlayer = false;
        }
    }
}
