using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovmentControlTest : NetworkBehaviour
{
    
    public GameObject m_CamPerent;
    public GameObject m_CamPosition;
    public float m_Speed = 10f;
    public float m_TurnSpeed = 180f;
    public float m_JumpSpeed = 10f;
    public Rigidbody m_Rigidbody;
    public Collider col;
    
    private float m_ForwardMovmentValue;
    private float m_SidewardMovmentValue;
    private float m_TurnValue;
    private bool m_JumpValue;
    public float m_DistToGround;

    
    private void Awake()
    {
        m_DistToGround = col.bounds.extents.y;
    }

    private void OnEnable()
    {
        m_ForwardMovmentValue = 0f;
        m_SidewardMovmentValue = 0f;
    }
    
	void Update () {
        if (isLocalPlayer)
        {
            m_ForwardMovmentValue = Input.GetAxis("Vertical");
            m_SidewardMovmentValue = Input.GetAxis("Horizontal");
            m_TurnValue = Input.GetAxis("Mouse X");
            m_JumpValue = Input.GetButtonDown("Jump");
        }        
	}

    private void FixedUpdate()//Moving and turn the player
    {
        Move();
        Turn();
        Jump();
    }

    private void Move()
    {
        Vector3 movment = (transform.forward * m_ForwardMovmentValue + transform.right * m_SidewardMovmentValue) * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movment);
    }

    private void Turn()
    {
        float turn = m_TurnValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    private void Jump()
    {
        if (m_JumpValue && isOnFloor())
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpSpeed, m_Rigidbody.velocity.z);
        }
    }

    private bool isOnFloor()
    {
        return Physics.Raycast(transform.position, Vector3.down, m_DistToGround + 0.1f);
    }
}
