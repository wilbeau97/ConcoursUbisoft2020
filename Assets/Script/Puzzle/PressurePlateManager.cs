using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    
    [SerializeField] private PhotonView doorView;
    [SerializeField] private bool AllPlateMustStayActivated = true;
    private Dictionary<PressurePlate, bool> listOfPlates = new Dictionary<PressurePlate, bool>();
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
                //Debug.Log(plate.Key + "is not pressed");
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
    public void AddPressurePlate(PressurePlate pressurePlate)
    {
        listOfPlates.Add(pressurePlate, false);
        Debug.Log("id pressure plate = " + pressurePlate.GetInstanceID());
    }

    public void PressurePlateIsPressed(PressurePlate pressurePlate)
    {
        listOfPlates[pressurePlate] = true;
        Debug.Log("Pressure plate is pressed on PPM");
    }

    public void PressurePlateIsReleased(PressurePlate pressurePlate)
    {
        if (AllPlateMustStayActivated)
        {
            listOfPlates[pressurePlate] = false;
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
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(listOfPlates);

        } else if (stream.isReading)
        {
            listOfPlates = (Dictionary<PressurePlate, bool>) stream.ReceiveNext();
        }
    }
}
