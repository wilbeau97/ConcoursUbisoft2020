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
        InitiateDoor();
        InstantiatePuzzlePrefab();
    }

    private void InstantiatePuzzlePrefab()
    {
        Instantiate(puzzle1Prefab);
        Instantiate(puzzleIAPrefab, positionPuzzleIA);
        Instantiate(puzzleROFPrefab);
        Instantiate(puzzleMazePrefab);
    }

    private void InitiateDoor()
    {
        foreach (GameObject door in doorPrefabs)
        {
            Instantiate(door);
        }
        
    }
}
