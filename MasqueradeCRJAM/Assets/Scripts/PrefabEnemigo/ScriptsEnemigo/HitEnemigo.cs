using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("jugador")) {
            print("Da�o");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
