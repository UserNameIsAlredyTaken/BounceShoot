using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    private float m_DistToGround;
    private const float RAYCAST_SPHERER_RADIUS = 0.5f;
    private const int LOCAL_PLAYER_LAYER = 10;
    

//    public override void OnStartLocalPlayer()
//    {
//        ChangeLayersRecursively(transform, LOCAL_PLAYER_LAYER);
//        GetComponent<MeshRenderer>().material.color = Color.blue;
//    }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_DistToGround = GetComponent<Collider>().bounds.extents.y;
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
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position, RAYCAST_SPHERER_RADIUS, Vector3.down, out hitInfo, m_DistToGround - RAYCAST_SPHERER_RADIUS + 0.1f);
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
