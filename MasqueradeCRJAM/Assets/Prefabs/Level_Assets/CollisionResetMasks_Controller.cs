using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResetMasks_Controller : MonoBehaviour
{
    private PlayerMovement player;
    private bool IsCollidingPlayer = false;
    private MaskObject[] masks;
    private Vector3[] maskPositions;
    private void Awake()
    {
        masks = FindObjectsOfType<MaskObject>();//new MaskObject[FindObjectsOfType<MaskObject>().Length];
        maskPositions = new Vector3[masks.Length];
    }

    void Start()
    {
        findPLayer();
        //activeScene = SceneManager.GetActiveScene();
    }

    void getMaskspos() {
        for (int i = 0; i < masks.Length; i++)
        {
            //maskPositions[i] = masks[i].gameObject.transform
        }
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

    void Update()
    {
        if (player != null)
        {
            if (IsCollidingPlayer)
            {
                //Aqui va el resetmasks
                //ShowTutorial(true);
            }

        }
    }

}
