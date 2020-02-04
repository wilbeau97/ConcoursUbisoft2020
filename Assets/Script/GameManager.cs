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
    [SerializeField] private PhotonView doorView2;
    [SerializeField] private PhotonView cubeView;

    private void Start()
    {
        PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, Vector3.zero, Quaternion.identity, 0);
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
