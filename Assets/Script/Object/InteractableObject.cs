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
    private bool startedFlashing = false;
    private Renderer renderer;
    private Vector3 initalPosition;


    private void Start()
    {
        renderer = GetComponent<Renderer>();
        r = renderer.material.color.r;
        g = renderer.material.color.g;
        b = renderer.material.color.b;
        initalPosition = transform.position;
    }

    private void Update()
    {
        if (lookingAtObject)
        {
            renderer.material.color = new Color32((byte) red, (byte) green, (byte) blue, 255);
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

    public void StartFlashing()
    {
        lookingAtObject = true;
        if (!startedFlashing)
        {
            startedFlashing = true;
            StartCoroutine("StartFlashObject");
        }
    }

    public void StopFlashing()
    {
        startedFlashing = false;
        lookingAtObject = false;
        StopCoroutine("StartFlashObject");
        renderer.material.color = new Color(r,g,b, 255);
    }

    private IEnumerator StartFlashObject()
    {
        while (lookingAtObject)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn)
            {
                if (blue <= 30)
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

    public void Respawn()
    {
        transform.position = initalPosition;
    }
}
