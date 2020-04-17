using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisAbility :  Ability
{
    [SerializeField]
    private float MAX_HEIGHT = 3.2f;
    private static float TOLERENCE = 0.1f;
    
    [SerializeField] private GameObject cam;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool canLiftHeavyObject = false; 
    // [SerializeField] private bool canRotate = false;
    // Quand on call increase ability, on va vérifier si canRotate == true, sinon, on laisse canLiftHeavyobject a false
    // Sinon, si can rotate == true, on met canLiftHeavyObject à true
    // dans la fonction qui vérifier si on peut intéragir, on rajoute un check, que si canLiftHeavyObject, on va checker si l'objet est 
    // soit : InteractablePhysicsObject ou InteractableHeavyPhysicsObject (tag à créer)
    // pour les objets heavy, ne pas oublier de les rendre plus lourd pour donner plus de crédibilité 
    private bool isInteractable = false;
    private bool isPressed;
    private bool alreadyParent = false;
    private float angleZ;
    private float sensitivity = 3f;
    private static bool canRotate = true;
    private GameObject objectToMove;
    private Vector3 playerPosition;
    private Collider playerCollider;
    private PlayerNetwork playerNetwork;
    private PhotonView view;
    [SerializeField]
    private Transform aimPosition;

    private void Start()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
        view = GetComponent<PhotonView>();
        playerCollider = transform.gameObject.GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        
        if (isInteractable && isPressed)
        {
            float rotationY = 0;
            float rotationX = 0;
            if (Input.GetAxis("TelekinesisRotate") != 0 && canRotate)
            {
                rotationY = -Input.GetAxis("Mouse X") * sensitivity;
                rotationX = -Input.GetAxis("Mouse Y") * sensitivity;
            }
            else
            {
                PerformRotationAroundPlayer();
            }
            PerformRotationAroundItself(rotationX, rotationY);
        }
    }

    private void PerformRotationAroundItself(float rotationX, float rotationY)
    {
        Transform objectToMoveTransform = objectToMove.transform;
        objectToMoveTransform.RotateAround(objectToMoveTransform.position, cam.transform.right, rotationX);
        objectToMoveTransform.RotateAround(objectToMoveTransform.position, cam.transform.up, rotationY);
    }

    private void PerformRotationAroundPlayer()
    {
        float objectToMovePosY = objectToMove.gameObject.GetComponent<Renderer>().bounds.center.y;

        Vector3 test = objectToMove.GetComponent<Renderer>().bounds.size;
        
        float objectToMoveScaleY = GetMaxValueOFVector3(test) / 2;
        float playerPosY = transform.position.y;
        objectToMovePosY += objectToMove.gameObject.GetComponent<Renderer>().bounds.extents.y / 5;
        
        if (objectToMovePosY <= playerPosY + MAX_HEIGHT && objectToMovePosY >=  1f)
        {
            objectToMove.transform.RotateAround(playerPosition, -cam.transform.right, angleZ);
        } else if (objectToMovePosY >= playerPosY + MAX_HEIGHT - TOLERENCE && angleZ < 0f)
        {
            objectToMove.transform.RotateAround(playerPosition, -cam.transform.right, angleZ);
        } else if (objectToMovePosY <= 1f && angleZ > 0)
        {
            objectToMove.transform.RotateAround(playerPosition, -cam.transform.right, angleZ);
        }
    }

    public override void Interact()
    {
        if (objectToMove != null) return;
        RaycastHit hit;
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(aimPosition.position);

        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            if (hit.collider.CompareTag("InteractablePhysicsObject") || (hit.collider.CompareTag("InteractableHeavyPhysicsObject") && canLiftHeavyObject))
            {
                playerNetwork.ChangeOwner(hit.collider);
                view.RPC("SetObjectToMove",PhotonTargets.All, hit.collider.gameObject.name);
                //SetObjectToMove(hit.collider.gameObject.name);
                Physics.IgnoreCollision(objectToMove.gameObject.GetComponent<Collider>(), playerCollider);
                
                if (isPressed)
                {
                    view.RPC("ParentObject", PhotonTargets.All);
                    //ParentObject();
                }
                
                objectToMove.GetComponent<InteractableObject>().StartFlashing();
            }
        }
    }

    [PunRPC]
    public void ParentObject()
    {
        isInteractable = true;
        if (alreadyParent) return;
        
        objectToMove.transform.parent = transform;
        float maxValueOfScale = GetMaxValueOFVector3(objectToMove.GetComponent<Renderer>().bounds.size);
        maxValueOfScale = objectToMove.CompareTag("InteractableHeavyPhysicsObject")
            ? maxValueOfScale
            : maxValueOfScale * 2;
        objectToMove.transform.position = cam.transform.position + cam.transform.forward * (maxValueOfScale + Vector3.Distance(transform.position, cam.transform.position));
        
        if ( objectToMove.transform.position.y < 0)
        {
            objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, 1 , objectToMove.transform.position.z );
        }
        alreadyParent = true;
    }
    
    [PunRPC]
    public void DeparentObject()
    {
        if(objectToMove != null)
            objectToMove.transform.parent = null;
        isInteractable = false;
        alreadyParent = false;
    }

    [PunRPC]
    public void SetObjectToMove(String str)
    {
        objectToMove = GameObject.Find(str);
    }
    
    [PunRPC]
    public void RemoveObjectToMove()
    {
        objectToMove = null;
    }

    public override void SetValue(float _angleZ, Vector3 _playerPosition)
    {
        if (objectToMove is null) return;

        angleZ = _angleZ;
        playerPosition = _playerPosition;
    }

    private float GetMaxValueOFVector3(Vector3 v)
    {
        return Mathf.Max(Mathf.Max(v.x, v.y), v.z);
    }
    
    public override void Pressed()
    {
        isPressed = true;
        if (objectToMove == null)
        {
            return;
        }
        objectToMove.GetComponentInChildren<Rigidbody>().isKinematic = true;
    }

    public override void Release()
    {
        isPressed = false;
        if (objectToMove == null)
        {
            return;
        }
        
        Physics.IgnoreCollision(objectToMove.gameObject.GetComponent<Collider>(), transform.gameObject.GetComponent<Collider>(), false);
        objectToMove.GetComponentInChildren<Rigidbody>().isKinematic = false;
        objectToMove.gameObject.GetComponent<InteractableObject>().StopFlashing();
        view.RPC("DeparentObject", PhotonTargets.All);
        //DeparentObject();
        view.RPC("RemoveObjectToMove",PhotonTargets.All);
        //RemoveObjectToMove();
    }

    public override void IncreaseAbility()
    {
        canRotate = true;
    }

    public void increaseMaxWeight()
    {
        canLiftHeavyObject = true;
    }
}