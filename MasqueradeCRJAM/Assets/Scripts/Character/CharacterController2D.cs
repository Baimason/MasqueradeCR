using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                         	
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
	[SerializeField] private bool m_JumpAsForce;
	[SerializeField] private bool m_AirControl = false;
	[SerializeField] private float m_slopeCheckDst = 1;
	[SerializeField] private Vector3[] m_slopeCheckOffset;
	[SerializeField] private float m_slopeCheckMax = 45f;
	[SerializeField] private LayerMask m_WhatIsGround;                         
	[SerializeField] private Transform m_GroundCheck;                          
	[SerializeField] private Transform m_CeilingCheck;      
	
	const float k_GroundedRadius = .2f;
	const float k_CeilingRadius = .2f;

	private bool m_Grounded;
	private int consecutiveJumps;
	private Animator m_Animator;
	private Rigidbody2D m_Rigidbody2D;
	private Entity m_modifiers;
	private Vector3 m_Velocity = Vector3.zero;
	private bool m_FacingRight = true;
	private bool touchingWall = false;
	private bool lastMove;
	private int hashF_MovementValue;

	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
	{
		hashF_MovementValue = Animator.StringToHash("MovementValue");
		m_Animator = GetComponentInChildren<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_modifiers = GetComponent<Entity>();

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

		float v = m_Grounded ? (lastMove ? 1 : 0) : -1;
		m_Animator.SetFloat(hashF_MovementValue, v);
	}
	
	public void Move(float move, /*bool crouch,*/ bool jump)
	{
		lastMove = false;
		if (m_Grounded || m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			touchingWall = false;
            targetVelocity = CheckSlopes(targetVelocity);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }

            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
			if (move != 0) lastMove = true;
        }
        bool canJump = ((m_Grounded || touchingWall) || consecutiveJumps < MaxJumps);
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

    private Vector3 CheckSlopes(Vector3 targetVelocity)
    {
		bool _canWallClimb = CanWallClib;
		foreach (var offset in m_slopeCheckOffset)
        {
			var origin = transform.position + offset;
			var hit = Physics2D.Raycast(origin, targetVelocity.normalized, m_slopeCheckDst, m_WhatIsGround);
			if (hit)
			{
				// _canWallClimb
				if (_canWallClimb)
				{
					touchingWall = true;
				}
				else
				{
					var angle = Vector2.Angle(hit.point - (Vector2)origin, hit.normal);
					if (angle > m_slopeCheckMax)
					{
						targetVelocity = Vector2.zero;
					}
				}
			}
		}

        return targetVelocity;
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
		Gizmos.color = Color.red;
		foreach (var offset in m_slopeCheckOffset)
        {
			var chkPos = transform.position + offset;
			Gizmos.DrawLine(chkPos, chkPos + new Vector3(m_slopeCheckDst, 0));
		}
    }

	float JumpForce { 
		get
        {
			float v = (m_modifiers == null) ? 1 : m_modifiers.GetModifier(Entity.EMod.JUMPFORCE);
			return m_JumpForce * v;
		} 
	}
	int MaxJumps { 
		get
        {
			float v = (m_modifiers == null) ? 1 : m_modifiers.GetModifier(Entity.EMod.MAXJUMPS);
			return Mathf.CeilToInt(v);
		}
	}
	bool CanWallClib
    {
		get
        {
			return (m_modifiers == null) ? false : m_modifiers.GetModifier(Entity.EMod.WALLCLIMB)>1;
		}
    }
	public bool TouchingWall => touchingWall;
}
