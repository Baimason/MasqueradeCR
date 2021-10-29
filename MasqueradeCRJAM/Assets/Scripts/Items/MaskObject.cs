using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaskObject : MonoBehaviour
{
    [SerializeField] private Entity.Modifier[] modifiers;
    [SerializeField] private bool useDefaultSpecial;
    [SerializeField] private UnityEvent<Entity> onStartSpecial, onUseSpecial, onCancelSpecial;
    Entity parentModifiers;

    public bool UseDefaultSpecial => useDefaultSpecial;

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
        parentModifiers = slot.Entity;
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

    public void ExecuteSpecial(int state)
    {
        switch (state)
        {
            case 0:
                onStartSpecial?.Invoke(parentModifiers);
                break;
            case 1:
                onUseSpecial?.Invoke(parentModifiers);
                break;
            case 2:
                onCancelSpecial?.Invoke(parentModifiers);
                break;
        }
        
    }
}
