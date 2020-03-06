using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoiceMenu : MonoBehaviour
{
    [SerializeField] private PhotonView view;
    [SerializeField] private Button player1Button;
    [SerializeField] private Button player2Button;
    [SerializeField] private Button readyButton;
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private MenuManager menuManager;
    
    private bool player1Selected = false;
    private bool player2Selected = false;
    private int ready = 0;

    private void Start()
    {
        readyButton.interactable = false;
    }

    private void Update()
    {
        if (ready == 2)
        {
            view.RPC("LoadPatrick", PhotonTargets.All);
            ready = 0;
        }
    }

    [PunRPC]
    public void LoadPatrick()
    {
        Debug.Log("Load");
        PhotonNetwork.LoadLevel(1);
    }

    public void Player1Choosen()
    {
        PlayerManager.LocalPlayerInstance = player1Prefab;
        Debug.Log(PlayerManager.LocalPlayerInstance.name);
        player2Button.interactable = false;
        view.RPC("DisableButtonPlayer1", PhotonTargets.All);
        
    }
    
    public void Player2Choosen()
    {
        PlayerManager.LocalPlayerInstance = player2Prefab;
        Debug.Log(PlayerManager.LocalPlayerInstance.name);
        player1Button.interactable = false;
        view.RPC("DisableButtonPlayer2", PhotonTargets.All);
    }
    
    [PunRPC]
    private void IsReady()
    {
        if (player1Selected && player2Selected)
        {
            readyButton.interactable = true;
            readyButton.Select();
        }
    }
    
    [PunRPC] 
    public void DisableButtonPlayer1()
    {
        player1Selected = true;
        player1Button.interactable = false;
        menuManager.ActivatedButtonPlayer2();
        IsReady();
    }
    
    [PunRPC]
    public void DisableButtonPlayer2()
    {
        player2Selected = true;
        player2Button.interactable = false;
        menuManager.ActivatedButtonPlayer1();
        IsReady();
    }

    public void Ready()
    {
        if (view.ownerId != PhotonNetwork.player.ID)
        {
            view.TransferOwnership(PhotonNetwork.player.ID);
        }
        readyButton.interactable = false;
        ready += 1;
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(player1Selected);
            stream.SendNext(player2Selected);
            stream.SendNext(ready);
        } else if (stream.isReading)
        {
            player1Selected = (bool) stream.ReceiveNext();
            player2Selected = (bool) stream.ReceiveNext();
            ready = (int) stream.ReceiveNext();
        }
    }
}
