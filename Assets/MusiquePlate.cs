using System;
using System.Collections;
using System.Collections.Generic;
using Script.AnimationGrabItem;
using UnityEngine;

public class MusiquePlate : MonoBehaviour
{
    public void OnTriggerEnter(Collider other1)
    {
        AudioManager.Instance.Play("Chest Opening Sound Legend of Zelda Ocarina of Time");
    }
}
