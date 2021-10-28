using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private bool left = true;
    private float speed = 2f;

    //Moves this GameObject 2 units a second in the forward direction
    void Update()
    {
        if (left)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (left)
        {
            left = false;
        }
        else {
            left = true;
        }
        //speed = speed * -1;
    }
}
