using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerIA : PuzzleManager
{
    [SerializeField] private PhotonView doorView;
    [SerializeField] private Transform respawnPoint;
    private Transform playerToRespawn;

    public override void OpenDoor()
    {
        doorView.RPC("OpenDoorRPC", PhotonTargets.All);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.gameObject.GetComponentInChildren<PlayerHUD>().FadeOut();
            playerToRespawn = other.transform;
            StartCoroutine("WaitForAnimation");
        } else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = other.GetComponent<MonsterChase>().RespawnPoint;
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerToRespawn.gameObject.GetComponentInChildren<PlayerHUD>().FadeIn();
        playerToRespawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        playerToRespawn.position = respawnPoint.position;
    }
}
