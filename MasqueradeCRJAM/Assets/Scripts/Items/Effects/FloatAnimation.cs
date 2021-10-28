using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnimation : IntAnimation
{
    public void SetFloat(float v)
    {
        if (_curr != null) _curr.Anim.SetFloat(hash, v);
    }
}
