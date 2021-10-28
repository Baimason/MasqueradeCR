using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                         	
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_JumpAsForce;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private float m_slopeCheckDst = 1;
	[SerializeField] private Vector3 m_slopeCheckOffset;
	[SerializeField] private float m_slopeCheckMax = 45f;
	[SerializeField] private LayerMask m_WhatIsGround;                         
	[SerializeField] private Transform m_GroundCheck;                          
	[SerializeField] private Transform m_CeilingCheck;                         
	

	const float k_GroundedRadius = .2f;
	public bool m_Grounded;
	private int consecutiveJumps;
	const float k_CeilingRadius = .2f; 
	private Rigidbody2D m_Rigidbody2D;
	private ModifierContainer m_modifiers;
	private bool m_FacingRight = true; 
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_modifiers = GetComponent<ModifierContainer>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
        bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}
	
	public void Move(float move, /*bool crouch,*/ bool jump)
	{
		if (m_Grounded || m_AirControl)
		{
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			var origin = transform.position + m_slopeCheckOffset;
			var hit = Physics2D.Raycast(origin, targetVelocity.normalized, m_slopeCheckDst, m_WhatIsGround);
			if (hit)
            {
				var angle = Vector2.Angle(hit.point - (Vector2)origin, hit.normal);
				if (angle > m_slopeCheckMax) targetVelocity = Vector2.zero;
			}
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);			

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
            			
			else if (move < 0 && m_FacingRight)
			{			
				Flip();
			}
		}
		bool canJump = (m_Grounded || consecutiveJumps < MaxJumps);
		if (canJump && jump)
		{
			if (m_Grounded) consecutiveJumps = 0;
			consecutiveJumps++;
			if (m_JumpAsForce) m_Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
			else
            {
				var vel = m_Rigidbody2D.velocity;
				vel.y = JumpForce;
				m_Rigidbody2D.velocity = vel;
            }
			m_Grounded = false;
		}
	}

	public void MoveUpwards()
	{
		m_Rigidbody2D.AddForce(new Vector2(0f, JumpForce));		
	}

	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		transform.localScale = new Vector3(m_FacingRight ? 1 : -1, 1, 1);
	}

    private void OnDrawGizmosSelected()
    {
		var chkPos = transform.position + m_slopeCheckOffset;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(chkPos, chkPos + new Vector3(m_slopeCheckDst, 0));
    }

	float JumpForce { 
		get
        {
			float v = (m_modifiers == null) ? 1 : m_modifiers.GetModifier(ModifierContainer.EMod.JUMPFORCE);
			return m_JumpForce * v;
		} 
	}
	int MaxJumps { 
		get
        {
			float v = (m_modifiers == null) ? 1 : m_modifiers.GetModifier(ModifierContainer.EMod.MAXJUMPS);
			return Mathf.CeilToInt(v);
		}
	}

}
