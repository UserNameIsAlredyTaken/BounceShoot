using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    
    public Transform _holdingSperePosition;
    public float _holdingSphereRadius = 0.5f;
    public float _groundSphereRadius = 0.49f;

    private float _afterFloorTimePast;
    
    private float _distToGround;
    
    void Awake()
    {
        _distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    
    public bool IsOnFloor()
    {
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position, _groundSphereRadius, Vector3.down, out hitInfo, _distToGround - _groundSphereRadius + 0.1f);
    }
    
    public bool IsHolding()
    {
        return Physics.OverlapSphere(_holdingSperePosition.position, _holdingSphereRadius, ~(1 << gameObject.layer)).Length != 0;
    }

    void Update()
    {
        if (IsOnFloor())
        {
            _afterFloorTimePast = 0;
        }
        else
        {
            _afterFloorTimePast += Time.deltaTime;
        }
    }

    public bool WasOnFloorNSecAgo(float seconds)
    {
        return _afterFloorTimePast < seconds;
    }
    
    
    private void OnDrawGizmos()
    {
        //Holding sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_holdingSperePosition.position, _holdingSphereRadius);
        
        //Ground sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * (_distToGround - _groundSphereRadius + 0.1f), _groundSphereRadius);
    }
}
