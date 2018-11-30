using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour {
    public LayerMask m_PlayerLayer;
    public LayerMask m_Platform;
    public float m_LifeTime = 15;
    public float m_Damage = 20;
    public bool m_IsDamaging;

    public Collider testColl;
    
    
    void Start () {
        Destroy(gameObject, m_LifeTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        int colliderLayerMask = (int)Mathf.Pow(2, collision.gameObject.layer);//get the LayerMask number of the collider
        if (colliderLayerMask == m_PlayerLayer.value)
        {
            if (m_IsDamaging)
            {
                collision.gameObject.GetComponent<HealthClass>().TakeDamage(m_Damage);
            }
            Destroy(gameObject);
        }
        else if(colliderLayerMask == m_Platform)//make bullets shoot throught platforms from below
        {
            if(collision.collider.bounds.max.y > transform.localPosition.y)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
            }
        }
    }

//    private void OnTriggerEnter(Collider other)
//    {
//        int colliderLayerMask = (int)Mathf.Pow(2, other.gameObject.layer);//get the LayerMask number of the collider
//        if (colliderLayerMask == m_PlayerLayer.value)
//        {            
//            GameObject targetRigidbody = other.gameObject;
////            targetRigidbody.GetComponent<HealthClass>().TakeDamage(m_Damage);
//            Destroy(gameObject);
//        }
//    }
}
