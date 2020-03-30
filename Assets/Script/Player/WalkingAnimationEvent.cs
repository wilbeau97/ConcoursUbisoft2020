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

    private FootPrint[] footPrintLeftInit = new FootPrint[4];
    private FootPrint[] footPrintRightInit = new FootPrint[4];
    private FootPrintEnum footprintToSpawn = FootPrintEnum.LEFT;
    private int rightIndex = 0;
    private int leftIndex = 0;

    private void Start()
    {
        for (int i = 0; i < footPrintLeftInit.Length; i++)
        {
            footPrintLeftInit[i] = Instantiate(leftFootPrint).GetComponent<FootPrint>();
        }
        
        for (int i = 0; i < footPrintRightInit.Length; i++)
        {
            footPrintRightInit[i] = Instantiate(rightFootPrint).GetComponent<FootPrint>();
        }
    }

    private void SpawnFootPrint(FootPrintEnum footPrintEnum)
    {
        FootPrint footPrint = footPrintLeftInit[0];
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
        footPrint.gameObject.transform.rotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
        footPrint.Spawn();
    }

    public FootPrint GetRightFootPrint()
    {
        FootPrint footPrint = footPrintRightInit[rightIndex];
        rightIndex++;
        rightIndex %= footPrintRightInit.Length;

        return footPrint;
    }
    
    public FootPrint GetLeftFootPrint()
    {
        FootPrint footPrint = footPrintLeftInit[leftIndex];
        leftIndex++;
        leftIndex %= footPrintLeftInit.Length;
        
        return footPrint;
    }

    public void playStepSound()
    {
        SoundsManager.instance.RandomizeSfx(); // joue les sons de pas aléatoirement 
        SpawnFootPrint(footprintToSpawn);
    }
}
