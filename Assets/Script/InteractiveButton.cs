using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{ 
    [SerializeField] private PuzzleManager puzzleManagerView;
    [SerializeField] bool endsPuzzle = false;
    private GameObject playerInteracting;
    private bool canInteract = true;
    private PhotonView _gameManagerPhotonView;


    private void Awake()
    {
        _gameManagerPhotonView = GameObject.FindGameObjectWithTag("GameManager").GetPhotonView();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (!canInteract) return;
        AudioManager.Instance.Play("lever");
        puzzleManagerView.OpenDoor();
        if (playerInteracting)
        {
            playerInteracting.GetComponent<PlayerHUD>().HideInteractableHint();    
        }
        if (endsPuzzle)
        {
            canInteract = false; // on vient empêcher que le joueur peut retrigger la fin (ligne31)
            _gameManagerPhotonView.RPC("EndedPuzzle", PhotonTargets.AllViaServer );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && other.gameObject.GetPhotonView().isMine)
        {
            canInteract = true;
            playerInteracting = other.gameObject;
            other.gameObject.GetComponentInChildren<PlayerHUD>().ShowInteractableHint();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") && other.gameObject.GetPhotonView().isMine)
        {
            canInteract = false;
            other.gameObject.GetComponentInChildren<PlayerHUD>().HideInteractableHint();
        }
    }
}
