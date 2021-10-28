using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	public bool jump = false;

	public bool movementBlocked = false;
	public ControlMaps inputs;
	private ModifierContainer m_modifiers;

	private void Awake()
	{
		inputs = new ControlMaps();
		inputs.Player.Jump.performed += _ => Jump();
		m_modifiers = GetComponent<ModifierContainer>();
	}

	void Jump()
    {
        if (!movementBlocked)
        {
			jump = true;            
		}
    }

    void Update()
	{
        if (!movementBlocked)
        {
			var run = runSpeed * SpeedMult;
			horizontalMove = inputs.Player.Movement.ReadValue<Vector2>().x * run;
		}
	}

	void FixedUpdate()
	{
        if (!movementBlocked)
        {
			// Move our character
			controller.Move(horizontalMove * Time.fixedDeltaTime, /*false,*/ jump);
			jump = false;
		}
	}

	private void OnEnable()
	{
		inputs.Enable();
	}

	private void OnDisable()
	{
		inputs.Disable();
	}


	float SpeedMult
	{
		get
		{
			float v = (m_modifiers == null) ? 1 : m_modifiers.GetModifier(ModifierContainer.EMod.SPEED);
			return v;
		}
	}
}
