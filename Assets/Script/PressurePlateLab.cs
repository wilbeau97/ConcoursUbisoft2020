using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLab : Photon.MonoBehaviour
{
    private bool isPressed;
    private PhotonView gameManagerView;
    [SerializeField] private int playerTarget;
    [SerializeField] private string punRPCMethodeName;
    [SerializeField] private PressurePlateLab PairPressurePlate;

    public void Start()
    {
        gameManagerView = gameObject.GetPhotonView();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"+playerTarget))
        {
            isPressed = true;
            if(PairPressurePlate.isPressed)Pressed();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"+playerTarget))
        {
            isPressed = false;
            if(PairPressurePlate.isPressed)Pressed();
        }
    }

    private void Pressed()
    {
        gameManagerView.RPC(punRPCMethodeName, PhotonTargets.All);
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}