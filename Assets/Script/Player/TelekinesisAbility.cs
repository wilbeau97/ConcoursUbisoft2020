using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisAbility :  Ability
{
    private bool isInteractable = false;
    [SerializeField] private LayerMask mask;
    private GameObject objectToMove;
    private float angleZ;
    private Vector3 middlePosition;
    private Vector3 playerPosition;
    private bool isPressed;
    [SerializeField] private GameObject cam;
    [SerializeField] private float distance;
    private PlayerNetwork playerNetwork;
    private PhotonView view;

    private void Start()
    {
        playerNetwork = GetComponent<PlayerNetwork>();
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (isInteractable && isPressed)
        {
            
            PerformMovement();
        }
    }

    private void PerformMovement()
    {
        Debug.Log(objectToMove.transform.position.y);
        //changer les valeurs magiques par des valeurs dynamiques
        if (objectToMove.transform.position.y <= 4f && objectToMove.transform.position.y >= 0f)
        {
            objectToMove.transform.RotateAround(playerPosition, -cam.transform.right, angleZ);
        } else if (objectToMove.transform.position.y >= 4f && angleZ < 0f)
        {
            objectToMove.transform.RotateAround(playerPosition, -cam.transform.right, angleZ);
        }
    }

    public override void Interact()
    {
        if (objectToMove != null) return;
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward),
            out hit, distance, mask))
        {
            if (hit.collider.CompareTag("InteractablePhysicsObject"))
            {
                view = GetComponent<PhotonView>();
                view.RPC("SetObjectToMove",PhotonTargets.All, hit.collider.gameObject.name);
                playerNetwork.ChangeOwner(hit.collider);
                Physics.IgnoreCollision(objectToMove.gameObject.GetComponent<Collider>(), transform.gameObject.GetComponent<Collider>());
                if (isPressed)
                {
                    view.RPC("ParentObject", PhotonTargets.All);
                }
                objectToMove.GetComponent<InteractableObject>().StartFlashing();
            }
        }
    }

    [PunRPC]
    public void ParentObject()
    {
        objectToMove.transform.parent = transform;
        isInteractable = true;
    }
    
    [PunRPC]
    public void DeparentObject()
    {
        objectToMove.transform.parent = null;
        isInteractable = false;
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
        view.RPC("RemoveObjectToMove",PhotonTargets.All);
    }

    public override void ActivateTutorial()
    {
        transform.GetChild(0).gameObject.GetComponent<PlayerHUD>().setText(
            "Vous pouvez utilisez la gachette de gauche (LT) pour utilisé la télékinésie sur certain objet");
    }

    public override void DesactivateTutorial()
    {
        transform.GetChild(0).gameObject.GetComponent<PlayerHUD>().DeactivateAbilityText();
    }
}
