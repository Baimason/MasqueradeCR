using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] float movementCooldown;
    [SerializeField] float specialCooldown;

    public void Execute(Entity e)
    {
        if (movementCooldown > 0) e.SetMovementCooldown(movementCooldown);
        if (specialCooldown > 0) e.SetSpecialCooldown(specialCooldown);
    }
}
