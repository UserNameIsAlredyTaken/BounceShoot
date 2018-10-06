using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponUpAndDownControlTest : NetworkBehaviour {

    public float m_WeaponRaisingSpeed = 180f;
    public float m_WeaponTurningSpeed = 180f;
    public float m_TopAngelConstraint = 270f;
    public float m_BotAngelConstraint = 90f;
    public Transform m_WeaponTransform;

    private float m_RaisingValue;
    

    private void Awake(){
    }
	
	private void Update () {
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
        var raiseValue = m_RaisingValue * m_WeaponRaisingSpeed * Time.deltaTime;    
        
        if ((raiseValue + m_WeaponTransform.eulerAngles.x) <= m_BotAngelConstraint || (raiseValue + m_WeaponTransform.eulerAngles.x) >= m_TopAngelConstraint) 
        {
            m_WeaponTransform.localRotation = Quaternion.Euler(raiseValue + m_WeaponTransform.eulerAngles.x, 0f, 0f);
        }
    }    
}
