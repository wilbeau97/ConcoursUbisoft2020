using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;

public class puzzle1Manager : PuzzleManager
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private PhotonView doorView;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject plateform;

    private AudioSource sonDupont;
    private Transform playerToRespawn;

    private void Start()
    {
        sonDupont = GetComponent<AudioSource>();
    }

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

    [PunRPC]
    public void DownPlateform()
    {
        StartCoroutine(DownPlateformCoroutine());
    }

    private IEnumerator DownPlateformCoroutine()
    {
        AudioManager.Instance.Play("pressurePlateClose", plateform.transform);
        while (plateform.transform.position.y >= -16)
        {
            plateform.transform.position -= new Vector3(0,1,0);
            yield return null;
        }
    }

    public IEnumerator RotateBridgeToPassRoutine()
    {
        Quaternion from = bridge.transform.rotation;
        Quaternion to = Quaternion.Euler( 0,0,0);
        sonDupont.Play();
        float elapsed = 0.0f;
        while( elapsed < 4f )
        {
            bridge.transform.rotation = Quaternion.Slerp(from, to, elapsed / 4f );
            elapsed += Time.deltaTime;
            yield return null;
        }
        bridge.transform.rotation = to;
        sonDupont.Stop();
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
    
    public override void OpenDoor()
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
