using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    
    [SerializeField] private bool isUserConnected= false;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private bool allPlateMustStayActivated = true;
    private GameManager _gameManager;
    private PhotonView _gameManagerPhotonView;
    private Dictionary<string, bool> listOfPlates = new Dictionary<string, bool>();
    private bool _doorActivated = false;
    private PhotonView doorView;
    private Door door;

    private void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        _gameManagerPhotonView = _gameManager.GetComponent<PhotonView>();
        GameObject doorObject = GameObject.Find(doorPrefab.name + "(Clone)");

        if (doorObject == null)
        {
            doorObject = GameObject.Find(doorPrefab.name);
        }
        
        door = doorObject.GetComponent<Door>();
        doorView = doorObject.GetPhotonView();
        
        
        
        if (PhotonNetwork.connected)
        {
            isUserConnected = true;
        }
    }

    // Update is called once per frame


    private void platesVerification()
    {
        bool allPlateActivated = true;
        foreach (var plate in listOfPlates)
        {
            if (!plate.Value)
            {
                allPlateActivated = false;
                CloseDoor();
            }
        }
        if (allPlateActivated)
        {
            
            OpenDoor();
        }
    }
    public void AddPressurePlate(string pressurePlateName)
    {
        listOfPlates.Add(pressurePlateName, false);
    }
    [PunRPC]
    public void PressurePlateIsPressedRPC(string pressurePlateName)
    {
        PressurePlateIsPressed(pressurePlateName);
    }
    
    [PunRPC]
    public void PressurePlateIsReleaseRPC(string pressurePlateName)
    {
        PressurePlateIsReleased(pressurePlateName);
    }
    public void PressurePlateIsPressed(string pressurePlateName)
    {
        listOfPlates[pressurePlateName] = true;
        platesVerification();
    }

    public void PressurePlateIsReleased(string pressurePlateName)
    {
        if (allPlateMustStayActivated)
        {
            listOfPlates[pressurePlateName] = false;
        }
        platesVerification();
    }

    public void OpenDoor()
    {
        if (isUserConnected)
        {
            door.OpenDoorRPC();
            if (door.isLastDoor && !_doorActivated)
            {
                _gameManagerPhotonView.RPC("EndedPuzzle", PhotonTargets.Others);
                _doorActivated = true;
            }
        }
        else
        {
            doorView.GetComponent<Door>().OpenDoorRPC();
            _doorActivated = true;
        }
        
    }

    public void CloseDoor()
    {
        if (isUserConnected && allPlateMustStayActivated)
        {
            doorView.RPC("CloseDoorRPC", PhotonTargets.All);
        }
        else
        {
            if (allPlateMustStayActivated)
            {
                doorView.GetComponent<Door>().CloseDoorRPC();    
            }
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
    
}
