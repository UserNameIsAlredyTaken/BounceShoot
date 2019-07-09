using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponUpAndDownControl : MonoBehaviour {

    public float m_WeaponRaisingSpeed = 10f;
    public float m_TopAngelConstraint = 90f;
    public float m_BotAngelConstraint = -90f;

    private float m_RaisingValue;
    private Transform m_WeaponTransform;
    private float m_CurrentCumRaise;
    

    private void Awake(){
        m_WeaponTransform = transform.GetChild(0);
    }
	
	private void Update () {
        m_RaisingValue = Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        Raise();
    }

    private void Raise()
    {
        var raiseValue = m_RaisingValue * m_WeaponRaisingSpeed;  
        m_CurrentCumRaise -= raiseValue;
        m_CurrentCumRaise = Mathf.Clamp(m_CurrentCumRaise, m_BotAngelConstraint, m_TopAngelConstraint);
        
        m_WeaponTransform.localEulerAngles = new Vector3(m_CurrentCumRaise, 0, 0);

    }    
}
