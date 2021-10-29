using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimation : TriggerAnimation
{
    public override void Set(Entity other, Entity self)
    {
        other.Anim.SetBool(hash, true);
    }

    public override void Reset(Entity other, Entity self)
    {
        other.Anim.SetBool(hash, false);
    }
}
