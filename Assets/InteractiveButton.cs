using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{ [SerializeField] private puzzle1Manager puzzle1ManagerView;
    private bool canInteract = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        puzzle1ManagerView.OpenDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            canInteract = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            canInteract = false;
        }
    }
}
