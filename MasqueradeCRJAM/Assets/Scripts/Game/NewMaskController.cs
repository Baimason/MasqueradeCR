using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewMaskController : MonoBehaviour
{
    [SerializeField] private float waitTime = 2;
    [SerializeField] private float fadeTime = 1;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image maskImage;
    [SerializeField] private TMPro.TextMeshProUGUI maskName;
    [SerializeField] private TMPro.TextMeshProUGUI maskDesc;
    
    public void Set(MaskObject.MaskData data)
    {
        maskImage.sprite = data.image;
        maskName.SetText(data.name);
        maskDesc.SetText(data.desc);

        gameObject.SetActive(true);
        StartCoroutine(DoRoutine());
    }

    private IEnumerator DoRoutine()
    {
        ControlMaps inputs = PlayerMovement.Inputs;

        // ZA WARUDO!!! TOKI WO TOMARE!
        inputs.Disable();
        Time.timeScale = 0;
        // Appear
        canvasGroup.alpha = 0;
        float perc = 0;
        while (perc < 1)
        {
            canvasGroup.alpha = perc;
            perc += Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }

        // Wait
        float t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / waitTime;
            yield return null;
        }
        // Disappear
        while (perc > 0)
        {
            canvasGroup.alpha = perc;
            perc -= Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        // ZA WARUDO!!! Toki wa ugokidasu
        Time.timeScale = 1;
        inputs.Enable();
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
}
