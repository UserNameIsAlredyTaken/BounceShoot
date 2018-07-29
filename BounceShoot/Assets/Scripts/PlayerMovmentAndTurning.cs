using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovmentAndTurning : MonoBehaviour {
    
    public float m_Speed = 10f;
    public float m_TurnSpeed = 180f;
    public float m_PitchRange = 0.2f;
    /*public AudioSource m_MovmentAudio;
    public AudioClip m_MovmentSound;
    public AudioClip m_IdleSound;*/
    
    private Rigidbody m_Rigidbody;
    private float m_ForwardMovmentValue;
    private float m_SidewardMovmentValue;
    private float m_TurnValue;
    private float m_OriginalPitch;
    

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_ForwardMovmentValue = 0f;
        m_SidewardMovmentValue = 0f;
    }
    
	// Use this for initialization
	void Start () {
        //m_OriginalPitch = m_MovmentAudio.pitch;
	}
	
	// Update is called once per frame
	void Update () {
        m_ForwardMovmentValue = Input.GetAxis("Vertical");
        m_SidewardMovmentValue = Input.GetAxis("Horizontal");
        m_TurnValue = Input.GetAxis("Mouse X");
        //FloatSound();
	}

    /*private void FloatSound()//if player is moving, special sound sounds
    {
        if (Mathf.Abs(m_ForwardMovmentValue) < 0.1f && Mathf.Abs(m_SidewardMovmentValue) < 0.1f)
        {
            if (m_MovmentAudio.clip == m_MovmentSound)
            {
                m_MovmentAudio.clip = m_IdleSound;
                m_MovmentAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovmentAudio.Play();
            }
        }
        else
        {
            if (m_MovmentAudio.clip == m_IdleSound)
            {
                m_MovmentAudio.clip = m_MovmentSound;
                m_MovmentAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovmentAudio.Play();
            }
        }
    }*/

    private void FixedUpdate()//Moving and turn the player
    {
        Move();
        Turn();
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
}
