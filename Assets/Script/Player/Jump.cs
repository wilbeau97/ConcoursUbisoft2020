using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float jumpForceY = 7f;
    public float height = 1.01f;
    [SerializeField] private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;
    private static bool canDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, height); //1.01
        Vector3 jumpForce = Vector3.zero;

        if (isGrounded)
        {
            //a terre
            if (Input.GetButtonDown("Jump") && nbJump <= 1)
            {
                jumpForce = new Vector3(0, jumpForceY, 0);
                Debug.Log("1");
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
            if (nbJump <= 1 && Input.GetButtonDown("Jump") && canDoubleJump)
            {
                Debug.Log("2");
                jumpForce = new Vector3(0, jumpForceY, 0);
                rb.AddForce(jumpForce, ForceMode.VelocityChange);
                nbJump = 2;
            }
        }
    }

    public void IncreaseAbility()
    {
        canDoubleJump = true;
    }
}
