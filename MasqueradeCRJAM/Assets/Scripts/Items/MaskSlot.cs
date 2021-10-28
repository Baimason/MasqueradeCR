using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSlot : MonoBehaviour
{
    [SerializeField] private MaskObject currentMask;

    private void Start()
    {
        if (currentMask != null) Place(currentMask);
    }

    public void Place(MaskObject newMask)
    {
        Drop();
        currentMask = newMask;
        newMask.Place(this);
    }

    public void Drop()
    {
        if (currentMask == null) return;
        currentMask.Drop();
    }

    public void ExecuteSpecial(int state)
    {
        if (currentMask != null) currentMask.ExecuteSpecial(state);
    }
}
