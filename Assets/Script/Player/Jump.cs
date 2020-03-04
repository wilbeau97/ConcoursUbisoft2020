using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private PhysicMaterial physicsMaterial;
    private Collider playerCollider;
    private float jumpForceY = 7f;
    public float height = 1.01f;
    [SerializeField] private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;
    [SerializeField] private static bool canDoubleJump = true;

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
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up,out hit, height); //1.01
       
        Vector3 jumpForce = Vector3.zero;

        if (isGrounded)
        {
            if (hit.collider.CompareTag("Jumpable"))
            {
                playerCollider.material = null;
            }
            else
            {
                playerCollider.material = physicsMaterial;
            }
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
