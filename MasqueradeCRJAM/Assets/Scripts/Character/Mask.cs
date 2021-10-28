using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    

    public void Place(MaskSlot slot)
    {
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        SetPhysics(false);
    }

    internal void Drop()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        SetPhysics(true);
    }

    void SetPhysics(bool v)
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = !v;
            
            if (!v)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
            }
        }
    }
}
