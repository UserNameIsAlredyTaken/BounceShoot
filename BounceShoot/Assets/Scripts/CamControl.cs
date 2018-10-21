using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Networking;

public class CamControl : NetworkBehaviour
{
    public float maxDistance = 2.2f;
    public float xCoordinate = -1.2f;
    public float yCoordinate = -1.4f;
    public float zCoordinate = -3.4f;
    
    
    private const float RAYCAST_SPHERER_RADIUS = 0.5f;
    private Vector3 origin;
    private Vector3 direction;
    private float currentHitDistance;
    private GameObject cam;
    
    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");//setting player camera to a proper position
        cam.transform.parent = transform;
        cam.transform.localRotation = cam.transform.parent.rotation;
    }

    private void FixedUpdate()
    {
        origin = transform.position;
        
        direction =  xCoordinate * transform.forward + yCoordinate * transform.right + zCoordinate * transform.up;
        direction.Normalize();
        RaycastHit hit;
        if (Physics.SphereCast(origin,RAYCAST_SPHERER_RADIUS, new Vector3(direction.x, direction.y, direction.z), out hit, maxDistance))
        {
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxDistance;
        }
        cam.transform.localPosition = direction * currentHitDistance;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, RAYCAST_SPHERER_RADIUS);
    }
}
