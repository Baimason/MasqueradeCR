using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] TitleController titleRef;

    public void Dismiss()
    {
        titleRef.DismissIntro();
    }
}
