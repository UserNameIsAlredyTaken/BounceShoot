using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

    public GameObject m_Shell;
    public Transform m_FireTransform;
    public AudioSource m_ShootingSource;
    public AudioClip m_ShootingSound;
    public float m_ShootingForce = 10;
    public string m_FireButton = "Fire1";

    private bool m_Fired;
    
	void Update () {
        if (isLocalPlayer)
        {
            if (Input.GetButtonDown(m_FireButton))
            {
                m_ShootingSource.clip = m_ShootingSound;
                m_ShootingSource.Play();
                CmdFire();
            }
        }        
	}

    [Command]
    private void CmdFire()
    {
        GameObject shellInstance = (GameObject)Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = m_ShootingForce * m_FireTransform.up;
        NetworkServer.Spawn(shellInstance);
    }
}
