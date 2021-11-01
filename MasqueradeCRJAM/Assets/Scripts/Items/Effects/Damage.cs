using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private bool Steal;

    public void Apply(Entity other, Entity self)
    {
        // If "attacking" a mask.
        if (other.Mask != null)
        {
            other.Mask.GetMask();
            self.MaskSlot.Place(other.Mask);
            return;
        }

        // If there is a mask, steal mask.
        if (other.MaskSlot != null && !other.MaskSlot.IsEmpty)
        {
            // Drop mask.
            var m = other.MaskSlot.Drop();
            // Steal.
            if (Steal && self != null)
            {
                m.GetMask();
                self.MaskSlot.Place(m);
            }
        }
        // Else, kill entity.
        else
        {
            other.Kill();
        }
    }
}
