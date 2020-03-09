using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]private float jumpForceY = 7f;
    public float height = 1.05f;
    [SerializeField] private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;
    private static bool canDoubleJump = true;
    private Collider playerCollider;
    [SerializeField] private PhysicMaterial slideMaterial;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, height);

        Vector3 jumpForce = Vector3.zero;

       

        if (isGrounded)
        {
            if (hit.collider.CompareTag("Jumpable"))
            {
                playerCollider.material = null;
            }
            else
            {
                playerCollider.material = slideMaterial;
            }
            //a terre
            if (Input.GetButtonDown("Jump") && nbJump <= 1)
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
            if (nbJump <= 1 && Input.GetButtonDown("Jump") && canDoubleJump)
            {
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

    public void IncreaseJumpForce()
    {
        jumpForceY = 8f;
    }

    public void OnCollisionEnter(Collision other)
    {
        playerCollider.material = slideMaterial;
    }
}
