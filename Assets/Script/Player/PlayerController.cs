using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float sensitivity = 5f;
    private PlayerMotor motor;
    private CameraController cam;
    private Ability ability;
    private PlayerHUD hud;
    private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<CameraController>();
        ability = GetComponent<Ability>();
        hud = GetComponentInChildren<PlayerHUD>();
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

        if (Input.GetAxis("TelekinesisMove") != 0)
        {
            ability.Pressed();
            ability.Interact();
            hud.ActivateAim();
            ability.SetValue(rotationZ, transform.position);
        }
        else
        {
            hud.DeactivateAim();
            ability.Release();
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
    }
}
