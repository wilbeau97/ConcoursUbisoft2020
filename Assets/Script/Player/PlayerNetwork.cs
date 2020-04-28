using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject[] playerGraphics;
    [SerializeField] private MonoBehaviour[] playerControlScript;
    [SerializeField] private GameObject playerUI;

    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerCamera.GetComponent<Camera>().enabled = true;
        Initialize();
    }


    private void Initialize()
    {
        if (PhotonNetwork.connected)
        {
            //Handle not local player
            if (!photonView.isMine)
            {
                playerCamera.SetActive(false);
                playerUI.SetActive(false);
                foreach (MonoBehaviour script in playerControlScript)
                {
                    script.enabled = false;
                }
            }
        }
    }

    public void DesactivateGraphicsOtherPlayer()
    {
        if (!photonView.isMine)
        {
            foreach (GameObject graphic in playerGraphics)
            {
                graphic.SetActive(false);
            }
            
        }
    }

    public void ChangeOwner(Collider other)
     {
         if (other.gameObject.CompareTag("InteractablePhysicsObject") || other.gameObject.CompareTag("InteractableHeavyPhysicsObject"))
         {
             if (photonView.isMine)
             {  
                 if (other.gameObject.GetPhotonView().ownerId != PhotonNetwork.player.ID)
                 {
                     other.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
                 }
             }
         }
     }

    public void EndedTutorial()
    {
        GetComponent<Ability>().IncreaseAbility();
        transform.GetChild(0).gameObject.GetComponentInChildren<UiScript>().RoutineGainLife();
    }

    public void UpdateUiAfterEndedPuzzle()
    {
        transform.GetChild(0).gameObject.GetComponentInChildren<UiScript>().RoutineGainLife();
    }

    public void DisableCam()
    {
        playerCamera.GetComponent<Camera>().enabled = false;
    }
}
