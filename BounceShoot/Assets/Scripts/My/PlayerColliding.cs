using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    
    public Transform _holdingSperePosition;
    public float _holdingSphereRadius = 0.5f;
    public float _groundSphereRadius = 0.49f;
    
    
    private float m_DistToGround;
    
    private void Awake()
    {
        m_DistToGround = GetComponent<Collider>().bounds.extents.y;
    }
    
    public bool isOnFloor()
    {
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position, _groundSphereRadius, Vector3.down, out hitInfo, m_DistToGround - _groundSphereRadius + 0.1f);
    }
    
    public bool isHolding()
    {
        return Physics.OverlapSphere(_holdingSperePosition.position, _holdingSphereRadius, ~(1 << gameObject.layer)).Length != 0;
    }
    
    private void OnDrawGizmos()
    {
        //Holding sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_holdingSperePosition.position, _holdingSphereRadius);
        
        //Ground sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * (m_DistToGround - _groundSphereRadius + 0.1f), _groundSphereRadius);
    }
}
