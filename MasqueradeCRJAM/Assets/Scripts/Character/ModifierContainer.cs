using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierContainer : MonoBehaviour
{
    public enum EMod
    {
        JUMPFORCE, MAXJUMPS, SPEED
    }

    [System.Serializable]
    public class Modifier
    {
        public EMod kind;
        public float value = 1;
    }

    List<Modifier> modifiers = new List<Modifier>();

    public void AddModifiers(Modifier[] mods)
    {
        modifiers.AddRange(mods);
    }

    public void RemoveModifiers(Modifier[] mods)
    {
        foreach (var m in mods) modifiers.Remove(m);
    }

    public float GetModifier(EMod type)
    {
        float r = 1;
        foreach (var m in modifiers)
        {
            if (m.kind == type) r *= m.value;
        }
        return r;
    }
}
