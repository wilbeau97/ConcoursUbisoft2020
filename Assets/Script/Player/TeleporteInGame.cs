using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporteInGame : MonoBehaviour
{
    [SerializeField] private Transform teleportationPoint;

    public void TpInGame()
    {
        transform.position = teleportationPoint.position;
    }
}
