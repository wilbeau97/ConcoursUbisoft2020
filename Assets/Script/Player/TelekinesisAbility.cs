using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisAbility : MonoBehaviour
{
    private bool isInteractable = false;
    private GameObject objectToMove;
    private float angleZ;
    private Vector3 middlePosition;
    private bool isObjectRotationSet = false;
    private Vector3 playerPosition;
    private bool isPressed;
    [SerializeField] private GameObject cam;
    
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractablePhysicsObject"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward),
                out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("InteractablePhysicsObject"))
                {
                    //A changer
                    hit.collider.gameObject.GetComponent<InteractableObject>().FlashObject();
                    isInteractable = true;
                    objectToMove = other.gameObject;
                    if (isPressed)
                    {
                        objectToMove.transform.parent = transform;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractablePhysicsObject"))
        {
            isInteractable = false;
            objectToMove = null;
            isObjectRotationSet = false;
        }
    }

    public void MoveObject(float _angleZ, float _angleY, Vector3 _playerPosition)
    {
        if (objectToMove is null) return;
        if (!isObjectRotationSet)
        {
            isObjectRotationSet = true;
        }
        
        angleZ = _angleZ;
        playerPosition = _playerPosition;
    }
    
    public void Pressed()
    {
        if (objectToMove == null)
        {
            return;
        }
        objectToMove.GetComponent<Rigidbody>().isKinematic = true;

            isPressed = true;
    }

    public void Release()
    {
        isPressed = false;
        isInteractable = false;
        if (objectToMove == null)
        {
            return;
        }
        
        objectToMove.GetComponent<Rigidbody>().isKinematic = false;
        objectToMove.transform.parent = null;
            
    }
}
