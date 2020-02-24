using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    
    [SerializeField] private bool isUserConnected= false;
    [SerializeField] private PhotonView doorView;
    [SerializeField] private bool AllPlateMustStayActivated = true;
    private Dictionary<string, bool> listOfPlates = new Dictionary<string, bool>();
    private bool _doorActivated = false;

    private void Start()
    {
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
            _doorActivated = true;
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
    public void PressurePlateIsPressed(string pressurePlateName)
    {
        listOfPlates[pressurePlateName] = true;
        platesVerification();
    }

    public void PressurePlateIsReleased(string pressurePlateName)
    {
        if (AllPlateMustStayActivated)
        {
            listOfPlates[pressurePlateName] = false;
        }
        platesVerification();
    }

    public void OpenDoor()
    {
        if (isUserConnected)
        {
            doorView.RPC("OpenDoorRPC", PhotonTargets.All);
        }
        else
        {
            doorView.GetComponent<Door>().OpenDoorRPC();
        }
    }

    public void CloseDoor()
    {
        if (isUserConnected)
        {
            doorView.RPC("CloseDoorRPC", PhotonTargets.All);
        }
        else
        {
            doorView.GetComponent<Door>().CloseDoorRPC();
        }
    }
    
}
