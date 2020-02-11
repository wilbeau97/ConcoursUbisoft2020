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
        if (isInteractable)
        {
            PerformMovement();
        }
    }

    private void PerformMovement()
    {
        
        objectToMove.transform.rotation = objectRotation;
        
        objectToMove.transform.RotateAround(playerPosition, Vector3.up * 2, angleY);
        Transform objectTransform = objectToMove.transform;
        objectTransform.RotateAround(playerPosition, -objectTransform.right, angleZ);
        
        //ObjectRotation fonctionne pas parce que l'objet ne tourne pas, utiliser position en Y
        
        /*if (objectRotation.x < 0.70 && objectRotation.x > -0.35)
        {
            objectTransform.RotateAround(middlePosition, -objectTransform.right, angleZ);
        }
        else if (angleZ > 0 && objectRotation.x >= 0.70)
        {
            objectTransform.RotateAround(middlePosition, -objectTransform.right, angleZ);
        }
        else if (angleZ < 0 && objectRotation.x <= -0.35)
        {
            objectTransform.RotateAround(middlePosition, -objectTransform.right, angleZ);
        }*/
        
        //juste que ici
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractablePhysicsObject"))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward),
                out hit, Mathf.Infinity))
            {
                Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 10, Color.blue);
                if (hit.collider.CompareTag("InteractablePhysicsObject"))
                {
                    // Activer le texte du HUD pour l'intéraction
                    isInteractable = true;
                    objectToMove = other.gameObject;
                }
                else
                {
                    isInteractable = false;
                    objectToMove = null;
                    angleZ = 0;
                    angleY = 0;
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
}
