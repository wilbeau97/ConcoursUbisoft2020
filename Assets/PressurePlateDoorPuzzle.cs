using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateDoorPuzzle : MonoBehaviour
{
    [SerializeField] private PhotonView doorToOpen;
    [SerializeField] private PhotonView partOfPuzzleManager;
    // Start is cal led before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (PhotonNetwork.connected)
            {
                doorToOpen.RPC("OpenDoorRPC", PhotonTargets.All);
                partOfPuzzleManager.RPC("DeactivateAllPressurePlate", PhotonTargets.All);
            }
            else
            {
                doorToOpen.gameObject.GetComponent<Door>().OpenDoorRPC();
                partOfPuzzleManager.gameObject.GetComponent<PartOfDoorPuzzleManager>().DeactivateAllPressurePlate();
            }
        }
    }
}
