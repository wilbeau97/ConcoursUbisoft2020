using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle1Manager : MonoBehaviour
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private PhotonView doorView;
    [SerializeField] private Transform respawnPoint;

    private Transform playerToRespawn;
    
    [PunRPC]
    public void RotateBridgeToPass()
    {
        StopCoroutine("RotateBridgeToBlockRoutine");
        StartCoroutine("RotateBridgeToPassRoutine");
    }

    [PunRPC]
    public void RotateBridgeToBlock()
    {
        StopCoroutine("RotateBridgeToPassRoutine");
        StartCoroutine("RotateBridgeToBlockRoutine");
    }

    public IEnumerator RotateBridgeToPassRoutine()
    {
        Quaternion from = bridge.transform.rotation;
        Quaternion to = Quaternion.Euler( 0,0,0);
    
        float elapsed = 0.0f;
        while( elapsed < 4f )
        {
            bridge.transform.rotation = Quaternion.Slerp(from, to, elapsed / 4f );
            elapsed += Time.deltaTime;
            yield return null;
        }
        bridge.transform.rotation = to;
    }
    
    public IEnumerator RotateBridgeToBlockRoutine()
    {
        Quaternion from = bridge.transform.rotation;
        Quaternion to = Quaternion.Euler(0,90,0);
    
        float elapsed = 0.0f;
        while( elapsed < 4f )
        {
            bridge.transform.rotation = Quaternion.Slerp(from, to, elapsed / 4f );
            elapsed += Time.deltaTime;
            yield return null;
        }
        bridge.transform.rotation = to;
    }
    
    public void OpenDoor()
    {
        doorView.RPC("OpenDoorRPC", PhotonTargets.All);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.gameObject.GetComponentInChildren<PlayerHUD>().FadeOut();
            StartCoroutine("WaitForAnimation");
            playerToRespawn = other.transform;
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerToRespawn.position = respawnPoint.position;
        playerToRespawn.gameObject.GetComponentInChildren<PlayerHUD>().FadeIn();
    }
}
