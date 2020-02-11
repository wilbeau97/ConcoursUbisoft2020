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
    
    [SerializeField]
    private GameObject cam;
    
    [SerializeField]
    private LayerMask playerMask;
    
    // Start is called before the first frame update
    void Start()
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
        
        objectToMove.transform.RotateAround(middlePosition, Vector3.up, angleY);
        Transform objectTransform = objectToMove.transform;
        Quaternion objectRotationLocal = objectTransform.rotation;

        if (objectRotation.x < 0.70 && objectRotation.x > -0.35)
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
        }
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
                Debug.Log("ect");
                if (hit.collider.CompareTag("InteractablePhysicsObject"))
                {
                    // Activer le texte du HUD pour l'intéraction
                    isInteractable = true;
                    objectToMove = hit.collider.gameObject;
                }
            }
            else
            {
                isInteractable = false;
                objectToMove = null;
                isObjectRotationSet = false;
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

    public void MoveObject(float _angleZ, float _angleY, Vector3 _camPosition)
    {
        if (objectToMove is null) return;
        if (!isObjectRotationSet)
        {
            objectRotation = objectToMove.transform.rotation;
            isObjectRotationSet = true;
        }
        
        angleZ = _angleZ;
        angleY = _angleY;
        distance = Vector3.Distance(_camPosition, objectToMove.transform.position);
        CalculateMiddlePoint(_camPosition, objectToMove.transform.position);
    }

    private void CalculateMiddlePoint(Vector3 _pos1, Vector3 _pos2)
    {
        middlePosition = new Vector3((_pos1.x + _pos2.x) / 2, (_pos1.y + _pos2.y) / 2, (_pos1.z + _pos2.z) / 2);
    }
}
