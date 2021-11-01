using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CharacterMove
{
	public bool controlBlocked = false;
	public ControlMaps inputs;
	public bool dropping;

    public static ControlMaps Inputs { 
		get {
			var pm = FindObjectOfType<PlayerMovement>();
			if (pm != null) return pm.inputs;
			return null;
		} 
	}

    protected override void Awake()
    {
		base.Awake();
		inputs = new ControlMaps();
		inputs.Player.Jump.performed += _ => Jump();
		inputs.Player.Special.started += _ => Special(0);
		inputs.Player.Special.performed += _ => Special(1);
		inputs.Player.Special.canceled += _ => Special(2);

		inputs.Player.Start.performed += _ => Pause();
	}
    protected override void Special(int state)
    {
		if (!CanUseSpecial()) return;

		if (state != 0 && inputs.Player.Movement.ReadValue<Vector2>().y < -0.5f)
        {
			m_MaskSlot.ExecuteSpecial(2);
			m_MaskSlot.Drop();
			return;
        }

		base.Special(state);
    }
    private void Pause()
    {
		GameSystem.CallPause(inputs);
    }

    protected override bool CanMove()
    {
        return !controlBlocked && base.CanMove();
    }
    protected override bool CanUseSpecial()
    {
        return !controlBlocked && base.CanUseSpecial();
    }

    void Update()
	{
        if (CanMove())
        {
			var run = runSpeed * SpeedMult;
			HorizontalMove = inputs.Player.Movement.ReadValue<Vector2>().x * run;
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

}
