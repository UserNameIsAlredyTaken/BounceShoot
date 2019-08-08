using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Networking;

public class CamControl : MonoBehaviour
{
    public Rigidbody _playerBody;
    public float _maxPlayerSqrVelocity;
    public float maxDistance = 2f;
    public float xCamRay = -1f;
    public float yCamRay = -1.5f;
    public float zCamRay = -2.3f;

    public float camCastOriginDistance = 0.7f;
    public float xCamOrigin = 0.12f;
    public float yCamOrigin = 0f;
    public float zCamOrigin = -1f;
    public float camSphereRadius = 0.455f;

    public Vector3 fpcPoint = new Vector3(-0.63f, -1.14f, -1.31f);

    public Vector3 camRotation;
    
    
    private Vector3 camCastOrigin;
    private Vector3 camCastRay;
    private float currentHitDistance;
    public Camera cam;
    private const int LOCAL_PLAYER_LAYER = 10;
    private const int SHELL_LAYER = 11;
    private int layerMask = ~((1 << LOCAL_PLAYER_LAYER) | (1 << SHELL_LAYER)); //all layers except local player and shells
    private int defaultCullingMask;
    
    private void Start()
    {
        cam.transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
        defaultCullingMask = cam.cullingMask;
    }

    private void LateUpdate()
    {
        camCastOrigin = xCamOrigin * transform.forward 
                 + yCamOrigin * transform.right 
                 + zCamOrigin * transform.up;
        camCastOrigin.Normalize();
        
        camCastRay = xCamRay * transform.forward
                    + yCamRay * transform.right 
                    + zCamRay * transform.up;
        camCastRay.Normalize();
        
//        RaycastHit hit;
//        if (Physics.SphereCast(transform.position + camCastOrigin * camCastOriginDistance,camSphereRadius, new Vector3(camCastRay.x, camCastRay.y, camCastRay.z), out hit, maxDistance, layerMask))
//        {
//            currentHitDistance = hit.distance;
//        }
//        else
//        {
//            currentHitDistance = maxDistance;
//        }


//        currentHitDistance = Mathf.Lerp(_lerpMin, _lerpMax, _playerBody.velocity.sqrMagnitude / _maxPlayerSqrVelocity);
        currentHitDistance = maxDistance;
        cam.transform.position = transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance;

        //make local player invisible if camera touches it
        if (Physics.CheckSphere(transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance,camSphereRadius, 1 << LOCAL_PLAYER_LAYER))
        {
//            cam.cullingMask = layerMask;
            cam.transform.localPosition = fpcPoint;
        }
        else
        {
            cam.cullingMask = defaultCullingMask;
        }
    }
    
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Debug.DrawLine(transform.position, transform.position + camCastOrigin * camCastOriginDistance, Color.red);
//        Debug.DrawLine(transform.position + camCastOrigin * camCastOriginDistance, transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance, Color.red);
//        Gizmos.DrawWireSphere(transform.position + camCastOrigin * camCastOriginDistance + camCastRay * currentHitDistance, camSphereRadius);
//    }
}
