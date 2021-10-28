using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimation : TriggerAnimation
{
    public override void Set(Entity e)
    {
        e.Anim.SetBool(hash, true);
    }

    public override void Reset(Entity e)
    {
        e.Anim.SetBool(hash, false);
    }
}
