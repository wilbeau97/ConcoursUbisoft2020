using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate4 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PhotonView puzzle1ManagerView;
    [SerializeField] private string rpcMethod;
    [SerializeField] private string[] activatedByTag;

    public void OnTriggerEnter(Collider other)
    {
        if (!puzzle1ManagerView.isMine) return;
        foreach (string tag in activatedByTag)
        {
            if (other.CompareTag(tag)) 
            {
                puzzle1ManagerView.RPC(rpcMethod, PhotonTargets.All);
                break;
            }
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (!puzzle1ManagerView.isMine) return;
        if (other.CompareTag("Player2") || other.CompareTag("Player1") || other.CompareTag("InteractablePhysicsObject")) 
        {
            //puzzle1ManagerView.RPC("RotateBridgeToBlock", PhotonTargets.All);
        }
    }
}
