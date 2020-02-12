using System;
using System.Collections;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private float r;
    private float g;
    private float b;
    private int red;
    private int blue;
    private int green;
    private bool lookingAtObject = false;
    private bool flashingIn;

    
    private void Start()
    {
        r = GetComponent<Renderer>().material.color.r;
        g = GetComponent<Renderer>().material.color.g;
        b = GetComponent<Renderer>().material.color.b;
    }

    private void Update()
    {
        if (lookingAtObject)
        {
            FlashObject();
            lookingAtObject = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player2") || other.CompareTag("Player1"))
        {
            if (other.gameObject.GetPhotonView().isMine)
            {
                other.gameObject.GetComponentInChildren<PlayerHUD>().ShowInteractableHint();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    { 
        if (other.CompareTag("Player2") || other.CompareTag("Player1"))
        {
            if (other.gameObject.GetPhotonView().isMine)
            {
                other.gameObject.GetComponentInChildren<PlayerHUD>().HideInteractableHint();
            }
        }
    }

    public void FlashObject()
    {
        StartCoroutine("StartFlashObject");
    }

    private IEnumerator StartFlashObject()
    {
        while (lookingAtObject)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn)
            {
                if (b <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    red -= 25;
                    blue -= 25;
                    green -= 25;
                }
            }
            else
            {
                if (blue >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    red += 25;
                    blue += 25;
                    green += 25;
                }
            }
        }
    }
}
