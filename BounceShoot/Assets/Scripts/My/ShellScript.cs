using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour {
    public LayerMask m_PlayerLayer;
    public LayerMask m_Platform;
    public float m_LifeTime = 15;
    public float m_Damage = 20;
    public bool m_IsDamaging;

    public float yAxisDamageMultiplier;
    
    void Start () {
        Destroy(gameObject, m_LifeTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        int colliderLayerMask = (int)Mathf.Pow(2, collision.gameObject.layer);//get the LayerMask number of the collider
        if (collision.rigidbody != null/* && collision.gameObject.layer != m_PlayerLayer.value*/)
        {
            Debug.Log("DAMAGEEEE1");
            if (m_IsDamaging)
            {
                Vector3 damageDirection = collision.transform.position - transform.position;
                Debug.Log(collision.gameObject.name);
                collision.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(damageDirection.x, damageDirection.y * yAxisDamageMultiplier, damageDirection.z) * m_Damage;
            }
            Destroy(gameObject);
        }
//        int colliderLayerMask = (int)Mathf.Pow(2, collision.gameObject.layer);//get the LayerMask number of the collider
//        if (colliderLayerMask == m_PlayerLayer.value)
//        {
//            Debug.Log("DAMAGEEEE1");
//            if (m_IsDamaging)
//            {
//                Vector3 damageDirection = collision.transform.position - transform.position;
//                Debug.Log(collision.gameObject.name);
//                collision.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(damageDirection.x, damageDirection.y * yAxisDamageMultiplier, damageDirection.z) * m_Damage;
//            }
//            Destroy(gameObject);
//        }
//        else if(colliderLayerMask == m_Platform)//make bullets shoot throught platforms from below
//        {
//            if(collision.collider.bounds.max.y > transform.localPosition.y)
//            {
//                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider, true);
//            }
//        }
    }
}
