using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpAndDownControl : MonoBehaviour {

    public float m_WeaponRaisingSpeed = 180f;
    public float m_WeaponTurningSpeed = 180f;
    public float m_TopAngelConstraint = -90;
    public float m_BottomAngelConstraint = 90;

    private float m_RaisingValue;
    private float m_TurningValue;

    private void Awake(){
    }
    
	void Start (){		
	}
	
	void Update () {
        m_RaisingValue = -1 * Input.GetAxis("Mouse Y");
        m_TurningValue = Input.GetAxis("Mouse X");
    }

    private void FixedUpdate()
    {
        Raise();
    }

    private void Raise()
    {
        float raiseValue = m_RaisingValue * m_WeaponRaisingSpeed * Time.deltaTime;
        float turnValue = m_TurningValue * m_WeaponTurningSpeed * Time.deltaTime;
        
        transform.Rotate(raiseValue, 0f, 0f, Space.Self);
        transform.Rotate(0f, turnValue, 0f, Space.World);

        float xValue = UnityEditor.TransformUtils.GetInspectorRotation(transform).x;
        if (xValue > m_BottomAngelConstraint || xValue < m_TopAngelConstraint)
        {
            xValue = Mathf.Clamp(xValue, m_TopAngelConstraint, m_BottomAngelConstraint);
            transform.eulerAngles = new Vector3(xValue,
                                            transform.eulerAngles.y,
                                            transform.eulerAngles.z);
        }
    }

    private bool RisingIsAllowed(float raiseValue, float x)
    {
        return (x > 270 && x < 360) || 
               (x >= 0 && x < 90);
    }
}
