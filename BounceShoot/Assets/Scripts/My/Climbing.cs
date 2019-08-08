using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    public float _slideSpeedUp;
    public float _slideSpeedForward;


    public bool isClimbing;
    private Rigidbody _rb;
    private PlayerColliding _colliding;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _colliding = GetComponent<PlayerColliding>();
    }

    private void Update()
    {
        
        if (_colliding.isHolding() && !_colliding.isOnFloor() && Input.GetButton("Jump"))
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
            isClimbing = true;
            WallSlide();
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            isClimbing = false;
        }
    }

    private void WallSlide()
    {
        var velocity = _rb.velocity;
        var transf = transform;
        velocity = transf.forward * _slideSpeedForward + transf.up * _slideSpeedUp + velocity;
        _rb.velocity = velocity;
    }

    
}
