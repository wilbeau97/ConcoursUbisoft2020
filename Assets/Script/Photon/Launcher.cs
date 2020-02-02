using UnityEngine;

public class Launcher: Photon.MonoBehaviour
{
    #region Public Variables


    #endregion


    #region Private Variables

    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Camera cam;
    /// <summary>
    /// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
    /// </summary>
    string _gameVersion = "1";


    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {


        // #Critical
        // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
        //PhotonNetwork.autoJoinLobby = false;


        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        //PhotonNetwork.automaticallySyncScene = true;
    }


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        Connect();
    }


    #endregion


    #region Public Methods


    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {


        // // we check if we are connected or not, we join if we are, else we initiate the connection to the server.
        // if (PhotonNetwork.connected)
        // {
        //     RoomOptions room = new RoomOptions();
        //     // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
        //     PhotonNetwork.JoinOrCreateRoom("test", room, TypedLobby.Default);
        // }
        // else
        // {
        //     // #Critical, we must first and foremost connect to Photon Online Server.
        //     PhotonNetwork.ConnectUsingSettings(_gameVersion);
        // }
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Join lobby");
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("test", room, TypedLobby.Default);
    }

    public virtual void OnJoinedRoom()
    {
        if (PhotonNetwork.countOfPlayers == 1)
        {
            PhotonNetwork.Instantiate(player1.name, spawnPointP1.position, Quaternion.identity, 0);
        } 
        else if (PhotonNetwork.countOfPlayers == 2)
        {
            PhotonNetwork.Instantiate(player2.name, spawnPointP2.position, Quaternion.identity, 0);
        }
        cam.gameObject.SetActive(false);
    }
    

#endregion
}