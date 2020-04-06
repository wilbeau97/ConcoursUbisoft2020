using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{ 
    [SerializeField] private PuzzleManager puzzleManagerView;
    private bool canInteract = true;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && other.gameObject.GetPhotonView().isMine)
        {
            canInteract = true;
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
