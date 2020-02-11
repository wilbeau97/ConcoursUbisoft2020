﻿using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player2") || other.CompareTag("Player1"))
        {
            other.gameObject.GetComponentInChildren<PlayerHUD>().ShowInteractableHint();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player2") || other.CompareTag("Player1"))
        {
            other.gameObject.GetComponentInChildren<PlayerHUD>().HideInteractableHint();
        }
    }
}
