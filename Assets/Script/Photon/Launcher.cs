using System;
using UnityEngine;

public class Launcher: Photon.MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject idleMenu;
    private string _gameVersion = "1";

    void Start()
    {
        idleMenu.SetActive(true);
        Connect();
    }
    
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Join lobby");
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("vincentfletan", room, TypedLobby.Default);
    }

    public virtual void OnJoinedRoom()
    {
        idleMenu.SetActive(false);
        startMenu.SetActive(true); 
    }
}