using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] float movementCooldown;
    [SerializeField] float specialCooldown;

    public void Execute(Entity other, Entity self)
    {
        if (movementCooldown > 0) other.SetMovementCooldown(movementCooldown);
        if (specialCooldown > 0) other.SetSpecialCooldown(specialCooldown);
    }
}
