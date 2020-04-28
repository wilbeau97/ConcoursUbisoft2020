using System.Collections;
using AuraAPI;
using CameraCutScene;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;
    [SerializeField] private Transform spawnPointNotconnected;
    [SerializeField] private GameObject mainCameraForAura;
    [SerializeField] private AuraVolume fog;
    [SerializeField] private BigTree tree;
    [SerializeField] private ObjectiveLight puzzleAcces3Light;
    [SerializeField] private WorldBuilder builder;

    private PlayableDirector playable;
    private int nbOfPuzzleSuceeed = 0;
    private GameObject player;
    private string notLocalPlayer;
    private string localPlayer;
    private bool isCinematicPlaying = false;
    private bool isWorldBuild = false;
    private Door[] doorViews = new Door[4];

    private void Awake()
    {
        if (PhotonNetwork.connected)
        {
            if (PlayerManager.LocalPlayerInstance.CompareTag("Player1"))
            {
                //look for player here
                Debug.Log("Online : player1 Instantiated"); 
                PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP1.position, Quaternion.identity, 0);
                notLocalPlayer = "Player 2(Clone)";
                localPlayer = "Player 1(Clone)";
            }
            else if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
            {
                Debug.Log("Online : player2 Instantiated");
                PhotonNetwork.Instantiate(PlayerManager.LocalPlayerInstance.name, spawnPointP2.position,
                    Quaternion.identity, 0);
                notLocalPlayer = "Player 1(Clone)";
                localPlayer = "Player 2(Clone)";
                builder.InstantiateWorld();
            }
            
        }
        else
        {
            Instantiate(playerPrefab, spawnPointNotconnected.position, Quaternion.identity);
        }
    }

    private void Start()
    {
        playable = GetComponent<PlayableDirector>();
        mainCameraForAura.GetComponent<Camera>().enabled = false;
        mainCameraForAura.gameObject.SetActive(false);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     gameObject.GetPhotonView().RPC("EndedPuzzle", PhotonTargets.All);
        //     //EndCinematic();
        // }

        if (isCinematicPlaying && playable.state != PlayState.Playing)
        {
            isCinematicPlaying = false;
            //SceneManager.LoadScene(1);
            PhotonNetwork.LoadLevel(1);
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
        if (doorViews[0] == null)
        {
            doorViews = builder.GetDoors();
        }
        DecreaseFog();
        if (nbOfPuzzleSuceeed != 0)
        {
            StartCinematique();
            tree.Grow();
            PlayerManager.LocalPlayerInstance.GetComponent<PlayerNetwork>().UpdateUiAfterEndedPuzzle();
        }
        else
        {
            PlayerManager.LocalPlayerInstance.GetComponent<PlayerNetwork>().EndedTutorial();
        }
        
        OpenNextDoor();
    }

    private void StartCinematique()
    {
        CSManager.Instance.StartCs(nbOfPuzzleSuceeed);
    }

    private void DecreaseFog()
    {
        switch(nbOfPuzzleSuceeed)
        {
            case 1:
                fog.volumeShape.fading.heightPlaneFade = 0.075f;
                break;
            case 2:
                fog.volumeShape.fading.heightPlaneFade = 0.05f;
                break;
            case 3:
                fog.volumeShape.fading.heightPlaneFade = 0.025f;
                break;
            case 4:
                fog.density.injectionParameters.enable = false;
                GameObject.Find(PlayerManager.LocalPlayerInstance.name + "(Clone)").GetComponentInChildren<Aura>().frustum.settings.density = 0.008f;
                break;
                    
        }
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
//             puzzleAcces3Light.ActivateLight();       // enlevé car ce n,est plus une seule lumière 
        }

        if (nbOfPuzzleSuceeed == 4)
        {
            EndCinematic();
            nbOfPuzzleSuceeed++;
        }
        else if (!doorViews[nbOfPuzzleSuceeed].alreadyOpen)
        {
            doorViews[nbOfPuzzleSuceeed].OpenDoorRPC();
            nbOfPuzzleSuceeed += 1;
        }
    }

    private void EndCinematic()
    {
        isCinematicPlaying = true;
        mainCameraForAura.SetActive(true);
        PlayerManager.LocalPlayerInstance.GetComponent<PlayerNetwork>().DisableCam();
        mainCameraForAura.GetComponent<Camera>().enabled = true;
        playable.Play();
    }

    // responsable de la transition après la fin du tuto
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        player.GetComponent<TeleporteInGame>().TpInGame();
        player.GetComponentInChildren<PlayerHUD>().ActivateConceptArt();
        GameObject.Find(notLocalPlayer).GetComponent<PlayerNetwork>().DesactivateGraphicsOtherPlayer();
        if (GameObject.Find(notLocalPlayer).GetComponent<Jump>() != null)
        {
            GameObject.Find(notLocalPlayer).GetComponent<Jump>().DisableJumpDropSoundForP2();
        }
        
        player.GetComponentInChildren<PlayerHUD>().FadeIn();
    }

    public string GetLocalPlayerName()
    {
        return localPlayer;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isWorldBuild);
        } else if (stream.isReading)
        {
            isWorldBuild = (bool) stream.ReceiveNext();
        }
    }
}
