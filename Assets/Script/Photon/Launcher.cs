using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Launcher: Photon.MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject idleMenu;
    [SerializeField] private MenuManager menuManager;
    private string _gameVersion = "1";
    public string roomName = "";
    [SerializeField] private Text roomNameText;
    public List<string> roomNames  = new List<string>();

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
        idleMenu.SetActive(true);
    }
    
    public void Connect()
    {
        roomNameText.text = roomName;
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        if (!PhotonNetwork.JoinOrCreateRoom(roomName, room, TypedLobby.Default))
        {
            menuManager.ChangeCreateRoomText("Impossible de crée la room");
        }

    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        menuManager.OpenStartMenu();
    }

    public virtual void OnJoinedRoom()
    {
        menuManager.Play();
        Debug.Log(PhotonNetwork.room.Name + " room joined");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}