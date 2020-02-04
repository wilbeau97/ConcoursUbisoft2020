using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        PerformJump();
    }

    private void PerformJump()
    {
        rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void Jump(Vector3 force)
    {
        jumpForce = force;
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    private void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
}

