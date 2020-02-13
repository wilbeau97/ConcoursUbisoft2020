using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisAbility :  Ability
{
    private bool isInteractable = false;
    public LayerMask mask;
    private GameObject objectToMove;
    private float angleZ;
    private Vector3 middlePosition;
    private bool isObjectRotationSet = false;
    private Vector3 playerPosition;
    private bool isPressed;
    [SerializeField] private GameObject cam;
    [SerializeField] private float distance;

    void FixedUpdate()
    {
        if (isInteractable && isPressed)
        {
            PerformMovement();
        }
    }

    private void PerformMovement()
    {
        Transform objectTransform = objectToMove.transform;
        objectTransform.RotateAround(playerPosition, -cam.transform.right, angleZ);
    }

    public override void test()
    {
        if (objectToMove != null) return;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward),
            out hit, distance, mask))
        {
            if (hit.collider.CompareTag("InteractablePhysicsObject"))
            {
                isInteractable = true;
                objectToMove = hit.collider.gameObject;
                if (isPressed)
                {
                    objectToMove.transform.parent = transform;
                }
                objectToMove.GetComponent<InteractableObject>().StartFlashing();
            }
        }
    }

    public override void SetValue(float _angleZ, Vector3 _playerPosition)
    {
        if (objectToMove is null) return;
        if (!isObjectRotationSet)
        {
            isObjectRotationSet = true;
        }
        
        angleZ = _angleZ;
        playerPosition = _playerPosition;
    }
    
    public override void Pressed()
    {
        if (objectToMove == null)
        {
            return;
        }
        objectToMove.GetComponentInChildren<Rigidbody>().isKinematic = true;
        isPressed = true;
    }

    public void Release()
    {
        isPressed = false;
        isInteractable = false;
        isObjectRotationSet = false;
        if (objectToMove == null)
        {
            return;
        }
        objectToMove.GetComponentInChildren<Rigidbody>().isKinematic = false;
        objectToMove.gameObject.GetComponent<InteractableObject>().StopFlashing();
        objectToMove = null;
    }
}
