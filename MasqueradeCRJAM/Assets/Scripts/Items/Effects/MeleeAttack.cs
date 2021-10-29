using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] bool affectOnceEach;
    [SerializeField] bool affectSelf;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector2 bounds;
    [SerializeField] UnityEvent<Entity> effects;

    List<Entity> affectedEntities = new List<Entity>();

    public void ResetEffect()
    {
        affectedEntities.Clear();
    }

    public void Execute(Entity self)
    {
        var scaledOffset = Vector3.Scale(offset, transform.lossyScale);
        var pos = transform.position + scaledOffset;
        var size = Vector3.Scale(bounds, transform.lossyScale);
        var colls = Physics2D.OverlapBoxAll(pos, size, 0);
        foreach (var c in colls)
        {
            var other = c.GetComponentInParent<Entity>();
            if (other != null && (affectSelf || other != self))
            {
                if (!affectedEntities.Contains(other))
                {
                    // Execute effects.
                    if (affectOnceEach) affectedEntities.Add(other);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var scaledOffset = Vector3.Scale(offset, transform.lossyScale);
        var pos = transform.position + scaledOffset;
        var size = Vector3.Scale(bounds, transform.lossyScale);
        Gizmos.DrawWireCube(pos, size);
    }
}
