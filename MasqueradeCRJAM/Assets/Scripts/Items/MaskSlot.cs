using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaskSlot : MonoBehaviour
{
    [SerializeField] private MaskObject currentMask;
    [SerializeField] private Transform positionRef;

    [SerializeField] UnityEvent<Entity> onStartSpecial, onUseSpecial, onCancelSpecial;
    Entity entity;
    public Entity Entity => entity;

    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        if (currentMask != null) Place(currentMask);
    }

    private void LateUpdate()
    {
        if (positionRef != null) transform.position = positionRef.position;
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
        if (currentMask != null && !currentMask.UseDefaultSpecial) currentMask.ExecuteSpecial(state);
        else ExecuteDefaultSpecial(state);
    }

    public void ExecuteDefaultSpecial(int state)
    {
        switch (state)
        {
            case 0:
                onStartSpecial?.Invoke(entity);
                break;
            case 1:
                onUseSpecial?.Invoke(entity);
                break;
            case 2:
                onCancelSpecial?.Invoke(entity);
                break;
        }

    }
}
