using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMove : MonoBehaviour
{
	protected CharacterController2D controller;
	[SerializeField] private UnityEvent onDeath;
	public float runSpeed = 40f;

	[HideInInspector] public bool movementEnabled;
	[HideInInspector] public bool specialEnabled;
	[HideInInspector] public float HorizontalMove = 0f;

	protected bool jump = false;
	protected Entity m_Entity;
	protected MaskSlot m_MaskSlot;

	protected virtual void Awake()
	{
		movementEnabled = true;
		specialEnabled = true;
		controller = GetComponent<CharacterController2D>();
		m_Entity = GetComponent<Entity>();
		m_MaskSlot = GetComponentInChildren<MaskSlot>();
	}

	void FixedUpdate()
	{
		if (CanMove())
		{
			// Move our character
			controller.Move(HorizontalMove * Time.fixedDeltaTime, /*false,*/ jump);
			jump = false;
		}
	}

	protected virtual bool CanUseSpecial()
    {
		return specialEnabled;
	}
	protected virtual bool CanMove()
	{
		return movementEnabled;
	}

	protected virtual void Special(int state)
	{
		if (!CanUseSpecial()) return;
		if (m_MaskSlot != null) m_MaskSlot.ExecuteSpecial(state);
	}

	protected void Jump()
	{
		if (!CanMove()) return;
		jump = true;
	}

	public float SpeedMult
	{
		get
		{
			float v = (m_Entity == null) ? 1 : m_Entity.GetModifier(Entity.EMod.SPEED);
			return v;
		}
	}

    public virtual void OnDeath()
    {
		onDeath?.Invoke();

	}
}
