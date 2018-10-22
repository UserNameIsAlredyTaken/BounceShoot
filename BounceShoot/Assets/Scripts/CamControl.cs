using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Networking;

public class CamControl : NetworkBehaviour
{
    public float maxDistance = 2.2f;
    public float xCamRay = -1.2f;
    public float yCamRay = -1.4f;
    public float zCamRay = -3.4f;

    public float camCastOriginDistance = 0.2f;
    public float xCamOrigin = 0.2f;
    public float yCamOrigin = 0f;
    public float zCamOrigin = 0.2f;
    public float camSphereRadius = 0.5f;
    
    private Vector3 camCastOrigin;
    private Vector3 camCastRay;
    private float currentHitDistance;
    private GameObject cam;
    private const int LOCAL_PLAYER_LAYER = 10;
    private int layerMask = ~(1 << LOCAL_PLAYER_LAYER); //all layers except local player
    
    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        cam.transform.parent = transform;
        cam.transform.localRotation = Quaternion.Inverse(cam.transform.parent.rotation);
    }

    private void FixedUpdate()
    {
        camCastOrigin = xCamOrigin * transform.forward 
                 + yCamOrigin * transform.right 
                 + zCamOrigin * transform.up;
        camCastOrigin.Normalize();
        
        camCastRay = xCamRay * transform.forward
                    + yCamRay * transform.right 
                    + zCamRay * transform.up;
        camCastRay.Normalize();
        
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + camCastOrigin * camCastOriginDistance,camSphereRadius, new Vector3(camCastRay.x, camCastRay.y, camCastRay.z), out hit, maxDistance, layerMask))
        {
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = maxDistance;
        }
        cam.transform.position = transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + camCastOrigin * camCastOriginDistance);
        Debug.DrawLine(transform.position + camCastOrigin * camCastOriginDistance, transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance);
        Gizmos.DrawWireSphere(transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance, camSphereRadius);
    }
}
