using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
{
    [SerializeField] float pushForce;
    [SerializeField] MaskObject mask;

    private void Awake()
    {
        mask = GetComponentInParent<MaskObject>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != null)
        {
            if (mask.CurrEntity.Rigidbody == collision.attachedRigidbody) return;
            var dir = transform.lossyScale.x >= 0 ? 1 : -1;
            collision.attachedRigidbody.AddForce(Vector2.right * pushForce * dir);
        }
    }
}
