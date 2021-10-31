using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo : MonoBehaviour
{
    //Inicializar variables
    public Animator animacion;
    public Enemigo enemigo;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("jugador"))
        {
            animacion.SetBool("caminar", false);
            animacion.SetBool("correr", false);
            animacion.SetBool("ataque", true);
            enemigo.atacando = true;
            //GetComponent<BoxCollider2D>().enabled = false;
        }
      
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("jugador"))
        {
            animacion.SetBool("caminar", true);
            animacion.SetBool("correr", true);
            animacion.SetBool("ataque", false);
            enemigo.atacando = false;
          //  GetComponent<BoxCollider2D>().enabled = true;
        }

    }


    // Update is called once per frame
    void Update() 
    {
        
    }
}
