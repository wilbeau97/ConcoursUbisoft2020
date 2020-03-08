using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;
    [SerializeField] private Transform spawnPointNotconnected;

    [SerializeField] private BigTree tree;
    [SerializeField] private Door[] doorViews;
    private int nbOfPuzzleSuceeed = 0;
    private GameObject player;
    private string notLocalPlayer;
    private void Awake()
    {
        if (PhotonNetwork.connected)
        {
            if (PlayerManager.LocalPlayerInstance.CompareTag("Player1"))
            {
                //look for player here
                 PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP1.position,
                    Quaternion.identity, 0);
                 notLocalPlayer = "Player2Test(Clone)";
            }
            else if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
            {
                PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP2.position,
                    Quaternion.identity, 0);
                notLocalPlayer = "Player1Test(Clone)";
            }
        }
        else
        {
            Instantiate(playerPrefab, spawnPointNotconnected.position, Quaternion.identity);
        }
    }

    public virtual void OnJoinedLobby()
    {
        Debug.Log("Join lobby");
        RoomOptions room = new RoomOptions();
        room.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("test", room, TypedLobby.Default);
    }

    [PunRPC]
    public void EndedPuzzle()
    {
        tree.Grow();
        PlayerManager.LocalPlayerInstance.GetComponent<PlayerNetwork>().EndedPuzzle();
        OpenNextDoor();
    }

    private void OpenNextDoor()
    {
        if (nbOfPuzzleSuceeed == 0)
        {
            player = GameObject.Find(PlayerManager.LocalPlayerInstance.name + "(Clone)");
            player.GetComponentInChildren<PlayerHUD>().FadeOut();
            StartCoroutine(WaitForAnimation());
        }
        doorViews[nbOfPuzzleSuceeed].OpenDoorRPC();
        nbOfPuzzleSuceeed += 1;
    }
    
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<TeleporteInGame>().TpInGame();
        player.GetComponentInChildren<PlayerHUD>().ActivateConceptArt();
        GameObject.Find(notLocalPlayer).GetComponent<PlayerNetwork>().DesactivateGraphicsOtherPlayer();
        player.GetComponentInChildren<PlayerHUD>().FadeIn();
    }
}
