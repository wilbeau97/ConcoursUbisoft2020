using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float sensitivity = 2.5f;
    private PlayerMotor motor;
    private CameraController cam;
    public float speed = 10f;
    public float jumpForceY = 5f;
    public bool onGround = true;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<CameraController>();
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

        if (onGround && Input.GetButtonDown("Jump"))
        {
            jumpForce = new Vector3(0, jumpForceY, 0);
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
        
        
        motor.Move(velocity);
        motor.Jump(jumpForce);
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
