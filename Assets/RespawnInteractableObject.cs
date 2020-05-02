using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnInteractableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("InteractablePhysicsObject") || other.CompareTag("InteractableHeavyPhysicsObject"))
        {
            other.gameObject.GetComponent<InteractableObject>().Respawn();
        }
    }
}
