using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntAnimation : TriggerAnimation
{
    protected Entity _curr;

    public override void Set(Entity other, Entity self)
    {
        _curr = other;
    }

    public override void Reset(Entity other, Entity self)
    {
        _curr = null;
    }

    public void SetInt(int v)
    {
        if (_curr != null) _curr.Anim.SetInteger(hash, v);
    }
}
