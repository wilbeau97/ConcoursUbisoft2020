using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoiceMenu : MonoBehaviour, IPunObservable
{
   [SerializeField] private PhotonView view;
    [SerializeField] private Button player1Button;
    [SerializeField] private Button player2Button;
    [SerializeField] private Button readyButton;
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private Text playerChosenText;
    
    private bool player1Selected = false;
    private bool player2Selected = false;
    private int ready = 0;

    private void Start()
    {
        readyButton.interactable = false;
    }

    private void Update()
    {
        if (player1Selected && player1Button.interactable)
        {
            player1Button.interactable = false;
            player2Button.Select();
        }
        
        if (player2Selected && player2Button.interactable)
        {
            player2Button.interactable = false;
            player1Button.Select();
        }
        
        if (ready == 2)
        {
            //essayer en local
            view.RPC("LoadGame", PhotonTargets.All);
            ready = 0;
        }
    }

    [PunRPC]
    public void LoadGame()
    {
        Debug.Log("Loading level");
        PhotonNetwork.LoadLevel(1);
    }

    public void Player1Choosen()
    {
        PlayerManager.LocalPlayerInstance = player1Prefab;
        playerChosenText.text = "Vous êtes le: " + PlayerManager.LocalPlayerInstance.name;
        player2Button.interactable = false;
        DisableButtonPlayer1();
        Debug.Log("player1Selected =  " + player1Selected); 
        Debug.Log("player2Selected =  " + player2Selected); 
        
    }
    
    public void Player2Choosen()
    {
        PlayerManager.LocalPlayerInstance = player2Prefab;
        playerChosenText.text = "Vous êtes le: " + PlayerManager.LocalPlayerInstance.name;
        player1Button.interactable = false;
        DisableButtonPlayer2();
        Debug.Log("player1Selected =  " + player1Selected);
        Debug.Log("player2Selected =  " + player2Selected);
    }

    [PunRPC] 
    public void DisableButtonPlayer1()
    {
        player1Selected = true;
        player1Button.interactable = false;
        menuManager.ActivatedButtonPlayer2();
        readyButton.interactable = true;
        readyButton.Select();
    }
    
    [PunRPC]
    public void DisableButtonPlayer2()
    {
        player2Selected = true;
        player2Button.interactable = false;
        menuManager.ActivatedButtonPlayer1();
        readyButton.interactable = true;
        readyButton.Select();
    }

    public void Ready()
    {
        readyButton.interactable = false;
        setReady();
        view.RPC("setReady", PhotonTargets.Others);
        Debug.Log("player1Selected (readyFunc) =  " + player1Selected);
        Debug.Log("player2Selected (readyFunc) =  " + player2Selected);
        Debug.Log(" nb of ready = " + ready);
    }

    [PunRPC]
    public void setReady()
    {
        ready += 1;
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
             stream.SendNext(player1Selected);
             stream.SendNext(player2Selected);
            // stream.SendNext(ready);
        } else if (stream.isReading)
        {
             player1Selected = (bool) stream.ReceiveNext();
             player2Selected = (bool) stream.ReceiveNext();
            // ready = (int) stream.ReceiveNext();
        }
    }
}
