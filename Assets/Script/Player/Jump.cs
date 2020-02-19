using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float jumpForceY = 5f;
    private bool canJump = true;
    private int nbJump = 0;
    private PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.5f);
        Vector3 jumpForce;
        if ((isGrounded || nbJump < 1) && Input.GetButtonDown("Jump"))
        {
            jumpForce = new Vector3(0, jumpForceY, 0);
            nbJump ++;
        }
        else
        {
            if (isGrounded)
            {
                nbJump = 0;
            }
            jumpForce = Vector3.zero;
        }
        motor.Jump(jumpForce);
    }
}
