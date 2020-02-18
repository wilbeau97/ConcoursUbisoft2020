using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateLab : MonoBehaviour
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
            Debug.Log("IsPressed");
            if(PairPressurePlate.isPressed)Pressed();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"+playerTarget))
        {
            isPressed = false;
            Debug.Log("leave");
            if(PairPressurePlate.isPressed)Pressed();
        }
    }

    private void Pressed()
    {
        gameManagerView.RPC(punRPCMethodeName, PhotonTargets.All, isPressed);
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isPressed);
        } else if (stream.isReading)
        {
            isPressed = (bool) stream.ReceiveNext();
        }
    }
}