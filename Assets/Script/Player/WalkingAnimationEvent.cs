using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum FootPrintEnum
{
    LEFT,
    RIGHT
}
public class WalkingAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject rightFootPrint;
    [SerializeField] private GameObject leftFootPrint;

    private FootPrint[] footPrintLeftInit = new FootPrint[10];
    private FootPrint[] footPrintRightInit = new FootPrint[10];
    private FootPrintEnum footprintToSpawn = FootPrintEnum.LEFT;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            footPrintLeftInit[i] = Instantiate(leftFootPrint).GetComponent<FootPrint>();
        }
        
        for (int i = 0; i < 10; i++)
        {
            footPrintRightInit[i] = Instantiate(rightFootPrint).GetComponent<FootPrint>();
        }
    }

    private void SpawnFootPrint(FootPrintEnum footPrintEnum)
    {
        FootPrint footPrint;
        if (footPrintEnum == FootPrintEnum.LEFT)
        {
            footPrint =  GetLeftFootPrint();
            footprintToSpawn = FootPrintEnum.RIGHT;
        }
        else
        {
            footPrint = GetRightFootPrint();
            footprintToSpawn = FootPrintEnum.LEFT;
        }
        
        footPrint.gameObject.transform.position = transform.position;
        footPrint.Spawn();
    }

    public FootPrint GetRightFootPrint()
    {
        int randomIndex = Random.Range(0, footPrintRightInit.Length - 1);
        FootPrint footPrint = footPrintRightInit[randomIndex];
        if (footPrint.isSpawn)
        {
            footPrint = GetRightFootPrint();
        }
        return footPrint;
    }
    
    public FootPrint GetLeftFootPrint()
    {
        int randomIndex = Random.Range(0, footPrintLeftInit.Length - 1);
        FootPrint footPrint = footPrintLeftInit[randomIndex];
        if (footPrint.isSpawn)
        {
            footPrint = GetLeftFootPrint();
        }
        return footPrint;
    }

    public void playStepSound()
    {
        SoundsManager.instance.RandomizeSfx(); // joue les sons de pas aléatoirement 
        SpawnFootPrint(footprintToSpawn);
    }
}
