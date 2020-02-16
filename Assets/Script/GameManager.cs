using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool plate1 = false;
    private bool plate2 = false;
    [SerializeField] private bool plate3 = false;
    private bool open = false;
    [SerializeField] private bool open2 = false;
    [SerializeField] private PhotonView doorView;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PhotonView doorView2;
    [SerializeField] private PhotonView cubeView;
    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;
    [SerializeField] private Transform spawnPointNotconnected;
    


    private void Start()
    {
        if (PhotonNetwork.connected)
        {
            if (PlayerManager.LocalPlayerInstance.CompareTag("Player1"))
            {
                PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP1.position,
                    Quaternion.identity, 0);
            }
            else if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
            {
                PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP2.position,
                    Quaternion.identity, 0);
            }
        }
        else
        {
            Instantiate(playerPrefab, spawnPointNotconnected.position, Quaternion.identity);
        }
    }
    
    public virtual void OnJoinedLobby()
    {
        Debug.Log("Join lobby");
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("test", room, TypedLobby.Default);
    }

    // Update is called once per frame
    void Update()
    {
        if (plate1 == true && plate2 == true && open == false)
        {
            doorView.RPC("OpenDoorRPC", PhotonTargets.All);
            open = true;
        }

        if (plate3 == true && open2 == false)
        {
            doorView2.RPC("OpenDoorRPC", PhotonTargets.All);
            open2 = true;
        }

        if (plate3 == false && open2 == true)
        {
            doorView2.RPC("CloseDoorRPC", PhotonTargets.All);
            open2 = false;
        }
    }

    [PunRPC] 
    public void Plate1(bool v)
    {
        plate1 = v;
    }
    
    [PunRPC] 
    public void Plate2(bool v)
    {
        plate2 = v;
    }
    
    [PunRPC] 
    public void Plate3(bool v)
    {
        plate3 = v;
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(plate1);
            stream.SendNext(plate2);
            stream.SendNext(plate3);
        } else if (stream.isReading)
        {
            plate1 = (bool) stream.ReceiveNext();
            plate2 = (bool) stream.ReceiveNext();
            plate3 = (bool) stream.ReceiveNext();
        }
    }
}
