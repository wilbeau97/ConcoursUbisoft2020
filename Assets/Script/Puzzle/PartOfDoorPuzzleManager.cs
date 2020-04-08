using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfDoorPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pressurePlates;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void DeactivateAllPressurePlate()
    {
        foreach (GameObject pressurePlate in pressurePlates)
        {
            pressurePlate.SetActive(false);
        }
    }
}
