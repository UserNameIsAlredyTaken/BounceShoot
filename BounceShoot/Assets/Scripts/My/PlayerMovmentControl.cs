using System;
using System.Collections;
using UnityEngine;

public class PlayerMovmentControl : MonoBehaviour
{
    
    public float m_Speed = 10f;
    public float m_TurnSpeed = 180f;
    public float m_JumpSpeed = 10f;
   
    
    private Rigidbody m_Rigidbody;
    private float m_ForwardMovmentValue;
    private float m_SidewardMovmentValue;
    private float m_TurnValue;
    private bool m_JumpValue;
    private PlayerColliding _colliding;
    private Climbing _climbing;
    

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        _colliding = GetComponent<PlayerColliding>();
        _climbing = GetComponent<Climbing>();
    }

    private void OnEnable()
    {
        m_ForwardMovmentValue = 0f;
        m_SidewardMovmentValue = 0f;
    }
    
	void Update () {
        m_ForwardMovmentValue = Input.GetAxisRaw("Vertical");
        m_SidewardMovmentValue = Input.GetAxisRaw("Horizontal");
        m_TurnValue = Input.GetAxisRaw("Mouse X");
        m_JumpValue = Input.GetButtonDown("Jump");
          
	}

    private void FixedUpdate()//Moving and turn the player
    {
        Move();
        Turn();
        Jump();
    }

    private void Move()
    {
        Vector3 movementForward = !_climbing.isClimbing ? m_ForwardMovmentValue * m_Speed * Time.deltaTime * transform.forward : Vector3.zero; //block forward movement if climbing
        Vector3 movementSide = m_SidewardMovmentValue * m_Speed * Time.deltaTime * transform.right;
        Vector3 movementUp = m_Rigidbody.velocity.y * m_Speed * Time.deltaTime * transform.up;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movementForward + movementSide + movementUp);
    }

    private void Turn()
    {
        float turn = m_TurnValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    
    private void Jump()
    {
        if (m_JumpValue && _colliding.isOnFloor()) //to start jumping
        {
            var velocity = m_Rigidbody.velocity;
            velocity = new Vector3(velocity.x, m_JumpSpeed, velocity.z);
            m_Rigidbody.velocity = velocity;
        }
        else if (Input.GetButton("Jump") && m_Rigidbody.velocity.y > 0) //if we already jumping and still holding the button, jump higher
        {
            m_Rigidbody.velocity += Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime * Vector3.up;
        }
        else if (m_Rigidbody.velocity.y < 0) //if we falling, fall faster
        {
            m_Rigidbody.velocity += Physics2D.gravity.y * fallMultiplier * Time.deltaTime * Vector3.up;
        }
        
    }

    
    
    private static void ChangeLayersRecursively(Transform trans, int layer) 
    {
        trans.gameObject.layer = layer;
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, layer);
        }
    }
}
