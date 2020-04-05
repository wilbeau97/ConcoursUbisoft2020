using UnityEngine;

public class WorldBuilder : MonoBehaviour, IPunObservable
{
    [SerializeField] private GameObject[] exitDoorPrefabs;
    [SerializeField] private GameObject[] enterDoorPrefabs;
    [SerializeField] private GameObject puzzle1Prefab;
    [SerializeField] private GameObject puzzleIAPrefab;
    [SerializeField] private Transform positionPuzzleIA;
    [SerializeField] private GameObject puzzleROFPrefab;
    [SerializeField] private GameObject puzzleMazePrefab;

    private Door[] doorObjectInstantiate = new Door[4];
    private bool isBuild = false;

    public void InstantiateWorld()
    {
        Debug.Log(isBuild);
        if (!isBuild)
        {
            InstantiateDoor();
            InstantiatePuzzlePrefab();
            isBuild = true;
        }
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
        Door[] test = new Door[4];
        int i = 0;
        foreach (GameObject goDoor in enterDoorPrefabs)
        {
            test[i] = GameObject.Find(goDoor.name + "(Clone)").GetComponent<Door>();
            i++;
        }
        return test;
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
