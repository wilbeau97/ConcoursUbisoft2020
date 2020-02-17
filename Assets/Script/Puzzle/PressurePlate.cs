using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PressurePlate : MonoBehaviour
{
    private bool _isPressed;
    [SerializeField] private string _activatedByTag; // utilisé pour déterminer ce qui va l'activer
    [SerializeField] private PressurePlateManager pressurePlateManager;
    [SerializeField] private PhotonView gameManagerView;

    private void Start()
    {
        pressurePlateManager.AddPressurePlate(this);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_activatedByTag))
        {
            _isPressed = true;
            Debug.Log("IsPressed on pressurePlatescript");
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
        pressurePlateManager.PressurePlateIsPressed(this);
    }

    private void Released()
    {
        pressurePlateManager.PressurePlateIsReleased(this);
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(_isPressed);
        } else if (stream.isReading)
        {
            _isPressed = (bool) stream.ReceiveNext();
        }
    }
}