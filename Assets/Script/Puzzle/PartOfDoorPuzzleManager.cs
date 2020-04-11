using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfDoorPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pressurePlates;
    [SerializeField] private Door[] doors;

    [PunRPC]
    public void DeactivateAllPressurePlate()
    {
        foreach (GameObject pressurePlate in pressurePlates)
        {
            pressurePlate.SetActive(false);
        }
    }

    public void Restart()
    {
        ActivateAllPressurePlate();
        CloseAllDoor();
    }
    
    private void ActivateAllPressurePlate()
    {
        foreach (GameObject pressurePlate in pressurePlates)
        {
            pressurePlate.SetActive(true);
        }
    }
    
    private void CloseAllDoor()
    {
        foreach (Door door in doors)
        {
            if(door.alreadyOpen)
                door.CloseDoorRPC();
        }
    }
}
