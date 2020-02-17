using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    
    [SerializeField] private PhotonView doorView;
    [SerializeField] private bool AllPlateMustStayActivated = true;
    private Dictionary<string, bool> listOfPlates = new Dictionary<string, bool>();
    private bool _doorActivated = false;
    

    // Update is called once per frame
    void Update()
    {
        platesVerification();
    }

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
    }

    public void PressurePlateIsReleased(string pressurePlateName)
    {
        if (AllPlateMustStayActivated)
        {
            listOfPlates[pressurePlateName] = false;
        }
    }

    public void OpenDoor()
    {
        doorView.RPC("OpenDoorRPC", PhotonTargets.All);
    }

    public void CloseDoor()
    {
        doorView.RPC("CloseDoorRPC", PhotonTargets.All);
    }
    
}
