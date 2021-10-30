using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    #region Modifier support
    public enum EMod
    {
        JUMPFORCE, MAXJUMPS, SPEED
    }

    [System.Serializable]
    public class Modifier
    {
        public EMod kind;
        public bool constant;
        public float value = 1;
    }

    private List<Modifier> modifiers = new List<Modifier>();

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
            if (m.kind == type)
            {
                if (m.constant) r += m.value;
                else r *= m.value;
            }
        }
        return r;
    }

    #endregion

    #region Utility Callbacks
    public void SetMovementCooldown(float time)
    {
        StartCoroutine(DoMovementCooldown(time));
    }
    internal void SetSpecialCooldown(float time)
    {
        StartCoroutine(DoSpecialCooldown(time));
    }

    IEnumerator DoMovementCooldown(float timer)
    {
        Move.movementEnabled = false;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        Move.movementEnabled = true;
    }
    
    IEnumerator DoSpecialCooldown(float timer)
    {
        Move.specialEnabled = false;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        Move.specialEnabled = true;
    }
    #endregion

    #region Component references
    private Animator animator;
    private CharacterMove characterMove;
    private MaskSlot maskSlot;
    private MaskObject mask;

    private int hash_Death;

    public Animator Anim => animator;
    public CharacterMove Move => characterMove;
    public MaskSlot MaskSlot => maskSlot;
    public MaskObject Mask => mask;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterMove = GetComponent<CharacterMove>();
        maskSlot = GetComponentInChildren<MaskSlot>();
        if (maskSlot == null) mask = GetComponentInChildren<MaskObject>();
        hash_Death = Animator.StringToHash("Dead");
    }

    internal void Kill()
    {
        if (Move != null)
        {
            if (!Move.enabled) return;
            Move.enabled = false;
        }
        if (Anim != null) Anim.SetBool(hash_Death, true);
    }

    #endregion

}
