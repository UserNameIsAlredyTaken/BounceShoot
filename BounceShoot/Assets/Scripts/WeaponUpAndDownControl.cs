using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpAndDownControl : MonoBehaviour {

    public float m_WeaponRaisingSpeed = 180f;
    public float m_WeaponTurningSpeed = 180f;
    public float m_TopAngelConstraint = 270f;
    public float m_BotAngelConstraint = 90f;

    private float m_RaisingValue;
    private Rigidbody m_Rigidbody;
    

    private void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
	void Start (){		
	}
	
	void Update () {
        m_RaisingValue = -1 * Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        Raise();
    }

    private void Raise()
    {
        float raiseValue = m_RaisingValue * m_WeaponRaisingSpeed * Time.deltaTime;    
        
        if ((raiseValue + transform.eulerAngles.x) <= m_BotAngelConstraint || (raiseValue + transform.eulerAngles.x) >= m_TopAngelConstraint) 
        {
            transform.localRotation = Quaternion.Euler(raiseValue + transform.eulerAngles.x, 0f, 0f);
        }
    }

    private bool RisingIsAllowed(float raiseValue, float x)
    {
        return (x > 270 && x < 360) || 
               (x >= 0 && x < 90);
    }
}
