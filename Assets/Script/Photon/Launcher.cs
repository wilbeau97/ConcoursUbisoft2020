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
    private string _gameVersion = "1";
    public string roomName = "";
    [SerializeField] private Text roomNameText;
    public List<string> roomNames  = new List<string>();

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
        idleMenu.SetActive(true);
        startMenu.SetActive(true);
    }
    
    public void Connect()
    {
        roomNameText.text = roomName;
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName, room, TypedLobby.Default);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }

    public virtual void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.room.Name + " room joined");
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}