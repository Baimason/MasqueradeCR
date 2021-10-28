using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] ModifierContainer.Modifier[] modifiers;

    ModifierContainer parentModifiers;

    public void Place(MaskSlot slot)
    {
        AddModifiers(slot);
        SetParent(slot);
        SetPhysics(false);
    }

    internal void Drop()
    {
        RemoveModifiers();
        Unparent();
        SetPhysics(true);
    }

    private void SetParent(MaskSlot slot)
    {
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private void Unparent()
    {
        transform.SetParent(null);
        transform.localScale = Vector3.one;
    }

    private void AddModifiers(MaskSlot slot)
    {
        parentModifiers = slot.GetComponentInParent<ModifierContainer>();
        parentModifiers.AddModifiers(modifiers);
    }

    private void RemoveModifiers()
    {
        if (parentModifiers == null) return;
        parentModifiers.RemoveModifiers(modifiers);
        parentModifiers = null;
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
