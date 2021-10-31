using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public Enemigo enemigo;

    public void Ataque_Animacion()
    {
        enemigo.Ataque_Animacion();
    }
}
