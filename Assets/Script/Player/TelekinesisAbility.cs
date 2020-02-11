using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisAbility : MonoBehaviour
{
    private bool isInteractable = false;
    public float tkMovementSpeed = 5f;

    private GameObject objectToMove;
    private float angleZ;
    private float angleY;
    private float distance;
    private Vector3 middlePosition;
    private Quaternion objectRotation;
    private bool isObjectRotationSet = false;
    private Vector3 playerPosition;
    private bool isPressed;
    
    [SerializeField]
    private GameObject cam;
    
    [SerializeField]
    private LayerMask playerMask;
    
    // Start is called before the first frame update
    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInteractable && isPressed)
        {
            PerformMovement();
        }
    }

    private void PerformMovement()
    {
        
        objectToMove.transform.rotation = objectRotation;
        
        Transform objectTransform = objectToMove.transform;
        objectTransform.RotateAround(playerPosition, -objectTransform.right, angleZ);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractablePhysicsObject"))
        {
            RaycastHit hit;
            //SphereCast pour ajouter un rayon au raycast
            if (Physics.SphereCast(cam.transform.position, 2f, cam.transform.TransformDirection(Vector3.forward),
                out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("InteractablePhysicsObject"))
                {
                    // Activer le texte du HUD pour l'intéraction
                    // Highlight lobject pouvant etre selectionner
                    // Parenter l'objet a controller si le bouton est appuyer, le deparenter si le bouton est relacher
                    if (isPressed)
                    {
                        objectToMove.transform.parent = transform;
                    }
                    isInteractable = true;
                    objectToMove = other.gameObject;
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
            objectRotation = objectToMove.transform.rotation;
            isObjectRotationSet = true;
        }
        
        angleZ = _angleZ;
        angleY = _angleY;
        playerPosition = _playerPosition;
        distance = Vector3.Distance(objectToMove.transform.position, _playerPosition);
    }
    
    public void Pressed()
    {
        isPressed = true;
    }

    public void Release()
    {
        isPressed = false;
        isInteractable = false;
        if (objectToMove == null) return;
        objectToMove.transform.parent = null;
    }
}
