using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate4 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PhotonView puzzle1ManagerView;

    public void OnTriggerEnter(Collider other)
    {
        if (!puzzle1ManagerView.isMine) return;
        if (other.CompareTag("Player2") || other.CompareTag("Player1") || other.CompareTag("InteractablePhysicsObject")) 
        {
            puzzle1ManagerView.RPC("RotateBridgeToPass", PhotonTargets.All);
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (!puzzle1ManagerView.isMine) return;
        if (other.CompareTag("Player2") || other.CompareTag("Player1") || other.CompareTag("InteractablePhysicsObject")) 
        {
            puzzle1ManagerView.RPC("RotateBridgeToBlock", PhotonTargets.All);
        }
    }
}
