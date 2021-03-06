using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaskSlot : MonoBehaviour
{
    [SerializeField] private MaskObject currentMask;
    [SerializeField] private Transform positionRef;

    [SerializeField] UnityEvent<Entity,Entity> onStartSpecial, onUseSpecial, onCancelSpecial;
    Entity entity;
    public Entity Entity => entity;
    public bool IsEmpty => currentMask == null;

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

    public MaskObject Drop()
    {
        if (currentMask == null) return null;
        currentMask.Drop();
        var m = currentMask;
        currentMask = null;
        return m;
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
                onStartSpecial?.Invoke(entity, entity);
                break;
            case 1:
                onUseSpecial?.Invoke(entity, entity);
                break;
            case 2:
                onCancelSpecial?.Invoke(entity, entity);
                break;
        }

    }
}
