using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] doorPrefabs;
    [SerializeField] private GameObject puzzle1Prefab;
    [SerializeField] private GameObject puzzleIAPrefab;
    [SerializeField] private Transform positionPuzzleIA;
    [SerializeField] private GameObject puzzleROFPrefab;
    [SerializeField] private GameObject puzzleMazePrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
       // InstantiateWorld();
    }

    public void InstantiateWorld()
    {
        InitiateDoor();
        InstantiatePuzzlePrefab();
    }

    private void InstantiatePuzzlePrefab()
    {
       PhotonNetwork.Instantiate(puzzle1Prefab.name, puzzle1Prefab.transform.position, puzzle1Prefab.transform.rotation, 0);
       PhotonNetwork.Instantiate(puzzleIAPrefab.name, positionPuzzleIA.transform.position, positionPuzzleIA.transform.rotation, 0);
       PhotonNetwork.Instantiate(puzzleROFPrefab.name, puzzleROFPrefab.transform.position, puzzleROFPrefab.transform.rotation, 0);
       PhotonNetwork.Instantiate(puzzleMazePrefab.name, puzzleMazePrefab.transform.position, puzzleMazePrefab.transform.rotation, 0);
    }

    private void InitiateDoor()
    {
        foreach (GameObject door in doorPrefabs)
        {
            PhotonNetwork.Instantiate(door.name, door.transform.position, door.transform.rotation, 0);
        }
        
    }
}
