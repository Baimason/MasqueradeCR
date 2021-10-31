using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public int fadeSpeed = 2;
    public GameObject[] ObjectToFade;
    
    private SpriteRenderer[] rend;
    private bool mostrando = true;

    void Start()
    {
        rend = new SpriteRenderer[ObjectToFade.Length];

        for (int i = 0; i < ObjectToFade.Length; i++)
        {
            rend[i] = ObjectToFade[i].GetComponent<SpriteRenderer>();
            Color c = rend[i].material.color;
            c.a = 0;
            rend[i].material.color = c;
        }        
    }

    IEnumerator fadeIn()
    {
        for (int i = 0; i < ObjectToFade.Length; i++)
        {
            for (float f = 0.05f; f < 1; f += 0.05f * fadeSpeed)
            {

                Color c = rend[i].material.color;
                c.a = f;
                rend[i].material.color = c;
                yield return new WaitForSeconds(0.05f);
            }            
        }
    }

    public void startFadingIn()
    {
        if (mostrando)
            return;
        StartCoroutine(fadeIn());
        mostrando = true;
    }

    public void startFadingOut()
    {
        if (!mostrando)
            return;
        StartCoroutine(fadeOut());
        mostrando = false;
    }

    IEnumerator fadeOut()
    {
        for (int i = 0; i < ObjectToFade.Length; i++)
        {
            for (float f = 1f; f >= -0.05f; f -= 0.05f * fadeSpeed)
            {

                Color c = rend[i].material.color;
                c.a = f;
                rend[i].material.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
