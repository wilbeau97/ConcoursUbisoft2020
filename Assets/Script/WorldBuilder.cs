using System;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class WorldBuilder : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject[] exitDoorPrefabs;
    [SerializeField] private GameObject[] enterDoorPrefabs;
    [SerializeField] private GameObject[] puzzle1Prefab;
    [SerializeField] private GameObject[] puzzleIAPrefab;
    [SerializeField] private GameObject[] puzzleROFPrefab;
    [SerializeField] private GameObject[] puzzleMazePrefab;

    private Door[] doorObjectInstantiate = new Door[4];
    private bool isBuild = false;
    private int seed;

    public void InstantiateWorld()
    {
        seed = Random.Range(0, Int32.MaxValue);
        gameObject.GetPhotonView().RPC("SetSeed", PhotonTargets.AllBuffered, seed); 
        InstantiateDoor();
        InstantiatePuzzlePrefab();
    }

    private void InstantiatePuzzlePrefab()
    { 
        int index = Random.Range(0, puzzle1Prefab.Length);
       PhotonNetwork.Instantiate(puzzle1Prefab[index].name, puzzle1Prefab[index].transform.position, puzzle1Prefab[index].transform.rotation, 0);
       index = Random.Range(0, puzzleIAPrefab.Length);
       PhotonNetwork.Instantiate(puzzleIAPrefab[index].name, puzzleIAPrefab[index].transform.position, puzzleIAPrefab[index].transform.rotation, 0);
       index = Random.Range(0, puzzleROFPrefab.Length);
       PhotonNetwork.Instantiate(puzzleROFPrefab[index].name, puzzleROFPrefab[index].transform.position, puzzleROFPrefab[index].transform.rotation, 0);
       index = Random.Range(0, puzzleMazePrefab.Length);
       PhotonNetwork.Instantiate(puzzleMazePrefab[index].name, puzzleMazePrefab[index].transform.position, puzzleMazePrefab[index].transform.rotation, 0);
    }

    private void InstantiateDoor()
    {
        foreach (GameObject door in exitDoorPrefabs)
        {
            PhotonNetwork.Instantiate(door.name, door.transform.position, door.transform.rotation, 0);
        }

        int i = 0;
        foreach (GameObject door in enterDoorPrefabs)
        {
            doorObjectInstantiate[i] = PhotonNetwork.Instantiate(door.name, door.transform.position, door.transform.rotation, 0).GetComponent<Door>();
            i++;
        }
    }

    public Door[] GetDoors()
    {
        Door[] test = new Door[4];
        int i = 0;
        foreach (GameObject goDoor in enterDoorPrefabs)
        {
            test[i] = GameObject.Find(goDoor.name + "(Clone)").GetComponent<Door>();
            i++;
        }
        return test;
    }

    [PunRPC]
    public void SetSeed(int _seed)
    {
        Random.InitState(_seed);
    }
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(isBuild);
        } else if (stream.isReading)
        {
            isBuild = (bool) stream.ReceiveNext();
        }
    }
}
