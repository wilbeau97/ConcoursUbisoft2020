using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 30f;
    private PlayerMotor motor;
    public float speed = 10f;
    private float jumpForceY = 5f;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {

        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");

        Vector3 movementHorizontal = transform.right * movementX;
        Vector3 movementVerticaltal = transform.forward * movementZ;

        Vector3 velocity = (movementHorizontal + movementVerticaltal).normalized * speed;


        float rotationY = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, -rotationY, 0f) * sensitivity;

        Vector3 jumpForce;
        if (Input.GetButtonUp("Jump"))
        {
            jumpForce = new Vector3(0, jumpForceY, 0);
        } else
        {
            jumpForce = Vector3.zero;
        }

        motor.Move(velocity);
        motor.Rotate(rotation);
        motor.Jump(jumpForce);
    }
}
