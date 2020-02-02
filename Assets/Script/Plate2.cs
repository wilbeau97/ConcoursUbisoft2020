using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate2 : MonoBehaviour
{
    private bool isPressed;
    [SerializeField] private PhotonView gameManagerView;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            isPressed = true;
            Debug.Log("IsPressed");
            Pressed();
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player2"))
        {
            isPressed = false;
            Debug.Log("leave");
            Pressed();
        }
    }

    private void Pressed()
    {
        gameManagerView.RPC("Plate2", PhotonTargets.All, isPressed);
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
