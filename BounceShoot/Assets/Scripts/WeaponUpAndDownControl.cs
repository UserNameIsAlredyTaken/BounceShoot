using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponUpAndDownControl : NetworkBehaviour {

    public float m_WeaponRaisingSpeed = 180f;
    public float m_WeaponTurningSpeed = 180f;
    public float m_TopAngelConstraint = 270f;
    public float m_BotAngelConstraint = 90f;

    private float m_RaisingValue;
    private Transform m_WeaponTransform;
    

    private void Awake(){
        m_WeaponTransform = transform.GetChild(0);
    }
    
	void Start (){		
	}
	
	void Update () {
        if (isLocalPlayer)
        {
            m_RaisingValue = -1 * Input.GetAxis("Mouse Y");
        }
    }

    private void FixedUpdate()
    {
        Raise();
    }

    private void Raise()
    {
        float raiseValue = m_RaisingValue * m_WeaponRaisingSpeed * Time.deltaTime;    
        
        if ((raiseValue + m_WeaponTransform.eulerAngles.x) <= m_BotAngelConstraint || (raiseValue + m_WeaponTransform.eulerAngles.x) >= m_TopAngelConstraint) 
        {
            m_WeaponTransform.localRotation = Quaternion.Euler(raiseValue + m_WeaponTransform.eulerAngles.x, 0f, 0f);
        }
    }    
}
