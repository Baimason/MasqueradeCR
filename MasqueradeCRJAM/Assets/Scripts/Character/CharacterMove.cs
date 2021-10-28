using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
	public CharacterController2D controller;

	public float runSpeed = 40f;

	[HideInInspector] public bool movementEnabled;
	[HideInInspector] public bool specialEnabled;
	[HideInInspector] public float HorizontalMove = 0f;

	private bool jump = false;
	private Entity m_modifiers;
	private MaskSlot m_MaskSlot;

	protected virtual void Awake()
	{
		movementEnabled = true;
		specialEnabled = true;
		m_modifiers = GetComponent<Entity>();
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

	protected void Special(int state)
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
			float v = (m_modifiers == null) ? 1 : m_modifiers.GetModifier(Entity.EMod.SPEED);
			return v;
		}
	}
}
