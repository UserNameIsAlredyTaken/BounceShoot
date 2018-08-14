using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingSource;
    public AudioClip m_ShootingSound;
    public float m_ShootingForce = 10;
    public string m_FireButton = "Fire1";

    private bool m_Fired;
    
	void Update () {
        if (Input.GetButtonDown(m_FireButton))
        {
            m_ShootingSource.clip = m_ShootingSound;
            m_ShootingSource.Play();
            Fire();
        }
	}

    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = m_ShootingForce * m_FireTransform.up;
    }
}
