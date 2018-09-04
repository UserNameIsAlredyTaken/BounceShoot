using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour {
    public LayerMask m_PlayerLayer;
    public float m_LifeTime = 15;
    public float m_Damage = 20;

    
    void Start () {
        Destroy(gameObject, m_LifeTime);
    }
	
	private void OnCollisionEnter(Collision collision)
    {
        int colliderLayerMask = (int)Mathf.Pow(2, collision.gameObject.layer);//get the LayerMask number of the collider
        if (colliderLayerMask == m_PlayerLayer.value)
        {            
            GameObject targetRigidbody = collision.gameObject;
            targetRigidbody.GetComponent<HealthClass>().TakeDamage(m_Damage);
            Destroy(gameObject);
        }
        
    }
}
