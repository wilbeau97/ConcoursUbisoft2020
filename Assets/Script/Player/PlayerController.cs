using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerAnimationController animationController;
    private float sensitivity = 5f;
    private PlayerMotor motor;
    private CameraController cam;
    private Ability ability;
    private PlayerHUD hud;
    private float speed = 10f;
    private float speedBack = 5f;
    private bool dontMoveCam = false;
    private FlashAura aura;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        cam = GetComponent<CameraController>();
        ability = GetComponent<Ability>();
        hud = GetComponentInChildren<PlayerHUD>();
        aura = GetComponentInChildren<FlashAura>();
        //animationController = GetComponent<PlayerAnimationController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementZ = Input.GetAxisRaw("Vertical");
        if (animator && animator.parameterCount != 0) // parametercount en attendant que les parametre du p2 soit la
        {
            animator.SetFloat("velX", movementX);
            animator.SetFloat("velY", movementZ);
        }
        

        Vector3 movementHorizontal = transform.right * movementX;
        Vector3 movementVerticaltal = transform.forward * movementZ;

        Vector3 velocity =  movementZ > 0 ? (movementHorizontal + movementVerticaltal).normalized * speed  : (movementHorizontal + movementVerticaltal).normalized * speedBack;

        if (Vector3.Distance(velocity, Vector3.zero) > 0.01f && gameObject.GetPhotonView().isMine)
        {
            aura.elapsedTime = 0;
            aura.StopFlashing();
        }

        float rotationY = Input.GetAxis("Mouse X");
        float rotationZ = -Input.GetAxis("Mouse Y");
        

        Vector3 rotation = new Vector3(0f, rotationY, 0f) * sensitivity;

        if (Input.GetAxis("TelekinesisRotate") != 0)
        {
            dontMoveCam = true;
        }
        
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

        if (!dontMoveCam)
        {
            MoveCamera(rotation, rotationZ * sensitivity);
        }
        else
        {
            dontMoveCam = false;
        }
        
        motor.Move(velocity);
    }

    private void MoveCamera(Vector3 rotation, float rotationZ)
    {
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
    }
}
