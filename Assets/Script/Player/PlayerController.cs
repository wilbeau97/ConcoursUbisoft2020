﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float sensitivity = 5f;
    private PlayerMotor motor;
    private CameraController cam;
    private TelekinesisAbility tk;
    private float speed = 10f;
    private float jumpForceY = 7.5f;
    private bool onGround = true;
    private int nbJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<CameraController>();
        tk = GetComponent<TelekinesisAbility>();
    }

    // Update is called once per frame
    void Update()
    {

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");

        Vector3 movementHorizontal = transform.right * movementX;
        Vector3 movementVerticaltal = transform.forward * movementZ;

        Vector3 velocity = (movementHorizontal + movementVerticaltal).normalized * speed;


        float rotationY = Input.GetAxis("Mouse X");
        float rotationZ = -Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 rotation = new Vector3(0f, rotationY, 0f) * sensitivity;

        Vector3 jumpForce;

        if ((onGround || nbJump <= 1) && Input.GetButtonDown("Jump"))
        {
            jumpForce = new Vector3(0, jumpForceY, 0);
            nbJump += 1;
        }
        else
        {
            jumpForce = Vector3.zero;
        }

        if (Input.GetButton("RotateCam"))
        {
            cam.RotateY(rotation.y);
        }
        else
        {
            motor.Rotate(rotation);
        }

        cam.RotateZ(rotationZ);

        if (Input.GetButtonUp("RotateCam"))
        {
            cam.ReplaceCam();
        }

        if (Input.GetAxis("TelekinesisMove") > 0)
        {
            tk.Pressed();
            tk.MoveObject(rotationZ, rotation.y, transform.position);
        }
        else
        {
            tk.Release();
        }
        
        motor.Move(velocity);
        motor.Jump(jumpForce);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            nbJump = 0;
        }
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
