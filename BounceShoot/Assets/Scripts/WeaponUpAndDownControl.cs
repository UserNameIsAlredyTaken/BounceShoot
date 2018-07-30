using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpAndDownControl : MonoBehaviour {

    public float m_WeaponRaisingSpeed = 180f;

    private float m_RaisingValue;
    private Rigidbody m_RigidBody;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
        m_RaisingValue = -1 * Input.GetAxis("Mouse Y");        
    }

    private void FixedUpdate()
    {
        Raise();
    }

    private void Raise()
    {
        float raise = m_RaisingValue * m_WeaponRaisingSpeed * Time.deltaTime;
        transform.RotateAround(transform.parent.position, transform.parent.right, raise);
    }
}
