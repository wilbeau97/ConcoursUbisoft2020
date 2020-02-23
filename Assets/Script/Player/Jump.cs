using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float jumpForceY = 5f;
    private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.1f);
        Vector3 jumpForce = Vector3.zero;

        if (isGrounded)
        {
            //a terre
            if (Input.GetButtonDown("Jump"))
            {
                jumpForce = new Vector3(0, jumpForceY, 0);
                rb.AddForce(jumpForce, ForceMode.VelocityChange);
                nbJump++;
            }
            else
            {
                nbJump = 0;
            }
        }
        else
        {
            //dans les air
            if (nbJump <= 1 && Input.GetButtonDown("Jump"))
            {
                jumpForce = new Vector3(0, jumpForceY, 0);
                rb.AddForce(jumpForce, ForceMode.VelocityChange);
                nbJump++;
            }
        }
    }
}
