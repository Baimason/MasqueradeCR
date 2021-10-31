using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] Transform location;

    public void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, location.position);
    }
}
