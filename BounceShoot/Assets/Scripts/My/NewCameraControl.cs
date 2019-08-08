using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraControl : MonoBehaviour
{
    public PlayerMovmentControl _PlayerMovment;
    
    public float _minDist;
    public float _maxDist;
    public Vector3 _camCastRay;
    public Vector3 _camOrigin;
    public float _camCastOriginDistance;
    
    private float _currDist;
    private Camera _cam;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        _camOrigin.Normalize();
        _camCastRay.Normalize();
        _currDist = _maxDist;
        _cam.transform.localPosition = _camOrigin * _camCastOriginDistance + _camCastRay * _currDist;
    }
    
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        var position = transform.parent.position;
//        Debug.DrawLine(position, position + _camOrigin * _camCastOriginDistance, Color.red);
//        Debug.DrawLine(position + _camOrigin * _camCastOriginDistance, position + _camOrigin * _camCastOriginDistance + _camCastRay * _currDist, Color.red);
//        Gizmos.DrawWireSphere(transform.position, 0.45f);
//    }
}
