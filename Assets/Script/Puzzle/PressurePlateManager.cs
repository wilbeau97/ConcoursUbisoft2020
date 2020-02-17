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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            Debug.Log("Door activated ! ");
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
        Debug.Log("Pressure plate is pressed on PPM");
        printDictio();
    }

    public void PressurePlateIsReleased(string pressurePlateName)
    {
        if (AllPlateMustStayActivated)
        {
            listOfPlates[pressurePlateName] = false;
            Debug.Log("Pressure plate is released");
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

    private void printDictio()
    {
        Debug.Log("instance : " + this.GetInstanceID());
        foreach (var plate in listOfPlates)
        {
            Debug.Log("Key : " + plate.Key + ", Value : " + plate.Value);
        }
    }
    
    
    // public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.isWriting)
    //     {
    //         stream.SendNext(listOfPlates);
    //
    //     } else if (stream.isReading)
    //     {
    //         listOfPlates = (Dictionary<PressurePlate, bool>) stream.ReceiveNext();
    //     }
    // }
}
