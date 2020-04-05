using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] exitDoorPrefabs;
    [SerializeField] private GameObject[] enterDoorPrefabs;
    [SerializeField] private GameObject puzzle1Prefab;
    [SerializeField] private GameObject puzzleIAPrefab;
    [SerializeField] private Transform positionPuzzleIA;
    [SerializeField] private GameObject puzzleROFPrefab;
    [SerializeField] private GameObject puzzleMazePrefab;

    private Door[] doorObjectInstantiate = new Door[4];


    public void InstantiateWorld()
    {
        InstantiateDoor();
        InstantiatePuzzlePrefab();
    }

    private void InstantiatePuzzlePrefab()
    {
       PhotonNetwork.Instantiate(puzzle1Prefab.name, puzzle1Prefab.transform.position, puzzle1Prefab.transform.rotation, 0);
       PhotonNetwork.Instantiate(puzzleIAPrefab.name, positionPuzzleIA.transform.position, positionPuzzleIA.transform.rotation, 0);
       PhotonNetwork.Instantiate(puzzleROFPrefab.name, puzzleROFPrefab.transform.position, puzzleROFPrefab.transform.rotation, 0);
       PhotonNetwork.Instantiate(puzzleMazePrefab.name, puzzleMazePrefab.transform.position, puzzleMazePrefab.transform.rotation, 0);
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
        return doorObjectInstantiate;
    }
}
