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

    private void Awake()
    {
		inputs = new ControlMaps();
        inputs.Player.Jump.performed += _ => Jump(); 
    }

	void Jump()
    {
        if (!movementBlocked)
        {
			jump = true;            
		}
    }

    // Update is called once per frame
    void Update()
	{
        if (!movementBlocked)
        {
            horizontalMove = inputs.Player.Movement.ReadValue<Vector2>().x * runSpeed; 
			//Debug.Log(horizontalMove);
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
}
