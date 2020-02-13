using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float jumpForceY = 5f;
    private bool onGround = true;
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
