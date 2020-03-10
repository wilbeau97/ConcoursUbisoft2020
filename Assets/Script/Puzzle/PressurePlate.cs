using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
[System.Serializable]

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private string playerWriting = "all";
    [SerializeField ]private bool isUserConnected= false;
    private bool _isPressed;
    private string pressurePlateName;
    [SerializeField] private string _activatedByTag; // utilisé pour déterminer ce qui va l'activer
    [SerializeField] private PressurePlateManager pressurePlateManager;
    [SerializeField] private PhotonView PressurePlateManagerPhotonView;
    [SerializeField] private PhotonView gameManagerView;

    private void Start()
    {
        pressurePlateName = this.name;
        pressurePlateManager.AddPressurePlate(pressurePlateName);
        if (PhotonNetwork.connected)
        {
            isUserConnected = true;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_activatedByTag))
        {
            _isPressed = true;
            Pressed();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(_activatedByTag))
        {
            _isPressed = false;
            Released();
        }
    }

    private void Pressed()
    {
        if (isUserConnected)
        {
            if (PressurePlateManagerPhotonView.isMine)
            {
                PressurePlateManagerPhotonView.RPC("PressurePlateIsPressedRPC",
                PhotonTargets.All, pressurePlateName);
            }
        }
        else
        {
            pressurePlateManager.PressurePlateIsPressed(pressurePlateName);
        }
            
        
    }

    private void Released()
    {
        if (isUserConnected)
        {
            if (PressurePlateManagerPhotonView.isMine)
            {
                PressurePlateManagerPhotonView.RPC("PressurePlateIsReleaseRPC",
                    PhotonTargets.All, pressurePlateName);
            }
        }
        else
        {
            pressurePlateManager.PressurePlateIsReleased(pressurePlateName);
        }
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (PlayerManager.LocalPlayerInstance.CompareTag(playerWriting) || playerWriting == "all")
        {
            if (stream.isWriting)
            {
                stream.SendNext(_isPressed);
            } else if (stream.isReading)
            {
                _isPressed = (bool) stream.ReceiveNext();
            }
        }
        else
        {
            if (stream.isReading)
            {
                _isPressed = (bool) stream.ReceiveNext();
            }
        }

    }
}