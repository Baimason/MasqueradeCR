using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private string propertyName;
    protected int hash;

    private void Awake()
    {
        hash = Animator.StringToHash(propertyName);
    }

    public virtual void Set(Entity e)
    {
        e.Anim.SetTrigger(hash);
    }

    public virtual void Reset(Entity e)
    {
        e.Anim.ResetTrigger(hash);
    }
}
