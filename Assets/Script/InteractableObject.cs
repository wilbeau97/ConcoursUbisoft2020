using UnityEngine;

public class InteractableObject : MonoBehaviour
{
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
