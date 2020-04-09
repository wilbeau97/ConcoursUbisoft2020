using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class Puzzle1ManagerVariant : MonoBehaviour
{
    [SerializeField] private PartOfDoorPuzzleManager[] partOfDoorPuzzleManagers;
    [SerializeField] private Transform respawnPointP1;
    [SerializeField] private Transform respawnPointP2;

    private GameObject localPlayer;
    private Transform respawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindWithTag(PlayerManager.LocalPlayerInstance.tag);
        
        if (PlayerManager.LocalPlayerInstance.CompareTag("Player1"))
        {
            respawnPoint = respawnPointP1;
            
        } else if(PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
        {
            respawnPoint = respawnPointP2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (localPlayer == null )
        {
            localPlayer = GameObject.FindWithTag(PlayerManager.LocalPlayerInstance.tag);
        }
    }
    [PunRPC]
    private void Respawn()
    {
        Debug.Log("FadeOut" + PlayerManager.LocalPlayerInstance.name);
        localPlayer.GetComponentInChildren<PlayerHUD>().FadeOut();
        StartCoroutine("WaitForAnimation");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            Respawn();
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        localPlayer.transform.position = respawnPoint.position;
        foreach (PartOfDoorPuzzleManager manager in partOfDoorPuzzleManagers)
        {
            manager.Restart();
        }
        yield return new WaitForSeconds(0.5f);
        localPlayer.GetComponentInChildren<PlayerHUD>().FadeIn();
    }
}
