using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

    public GameObject m_Shell;
    public Transform m_FireTransform;
//    public AudioSource m_ShootingSource;
//    public AudioClip m_ShootingSound;
    public float m_ShootingForce = 10;
    public string m_FireButton = "Fire1";

//    public Vector3 force;

    private bool m_Fired;
    
	void Update () {
        if (Input.GetButtonDown(m_FireButton))
        {
//            m_ShootingSource.clip = m_ShootingSound;
//            m_ShootingSource.Play();
            CmdFire();
//	        Shoot();
        }

//        if (Input.GetButtonDown("Force"))
//        {
//	        GetComponent<Rigidbody>().velocity += force;
//        }
	}
	
	[Client]
	private void Shoot()
	{
		GameObject shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);
		shellInstance.GetComponent<Rigidbody>().velocity = m_ShootingForce * m_FireTransform.up;
		NetworkServer.Spawn(shellInstance);
	}

//	[Command]
//	private void CmdPlaerShot(string playerID)
//	{
//		Debug.Log(playerID + " has been shot");
//	}

    [Command]
    private void CmdFire()
    {
        GameObject shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as GameObject;
        shellInstance.GetComponent<Rigidbody>().velocity = m_ShootingForce * m_FireTransform.up;
        NetworkServer.Spawn(shellInstance);
    }
}
