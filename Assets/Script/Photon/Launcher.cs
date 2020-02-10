using System;
using UnityEngine;

public class Launcher: Photon.MonoBehaviour
{
    #region Public Variables


    #endregion


    #region Private Variables


    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject idleMenu;
    /// <summary>
    /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
    /// </summary>
    private string _gameVersion = "1";


    #endregion


    #region MonoBehaviour CallBacks

    void Start()
    {
        idleMenu.SetActive(true);
        Connect();
    }


    #endregion


    #region Public Methods


    
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Join lobby");
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("testJP", room, TypedLobby.Default);
    }

    public virtual void OnJoinedRoom()
    {

        idleMenu.SetActive(false);
        startMenu.SetActive(true);
     }


    #endregion
}