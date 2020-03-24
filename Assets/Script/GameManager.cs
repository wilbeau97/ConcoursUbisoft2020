using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class GameManager : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;
    [SerializeField] private Transform spawnPointNotconnected;
    [SerializeField] private GameObject mainCameraForAura;

    [SerializeField] private BigTree tree;
    [SerializeField] private Door[] doorViews;
    [SerializeField] private ObjectiveLight puzzleAcces3Light;
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
                 notLocalPlayer = "Player 2(Clone)";
            }
            else if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
            {
                PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP2.position,
                    Quaternion.identity, 0);
                notLocalPlayer = "Player 1(Clone)";
            }
        }
        else
        {
            Instantiate(playerPrefab, spawnPointNotconnected.position, Quaternion.identity);
        }
    }

    private void Start()
    {
        mainCameraForAura.SetActive(false);
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

        if (nbOfPuzzleSuceeed == 2)
        {
            puzzleAcces3Light.ActivateLight();
        }

        if (!doorViews[nbOfPuzzleSuceeed].alreadyOpen)
        {
            doorViews[nbOfPuzzleSuceeed].OpenDoorRPC();
            nbOfPuzzleSuceeed += 1;
        }
    }
    
    // responsable de la transition après la fin du tuto j
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<TeleporteInGame>().TpInGame();
        player.GetComponentInChildren<PlayerHUD>().ActivateConceptArt();
        GameObject.Find(notLocalPlayer).GetComponent<PlayerNetwork>().DesactivateGraphicsOtherPlayer();
        if (GameObject.Find(notLocalPlayer).GetComponent<Jump>() != null)
        {
            GameObject.Find(notLocalPlayer).GetComponent<Jump>().disableJumpDropSoundForP2();
        }
        
        player.GetComponentInChildren<PlayerHUD>().FadeIn();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
