using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class MazeDoorOpen : MonoBehaviour
    {
        [SerializeField] private GameObject mazeCubeBlue;
        [SerializeField] private GameObject mazeCubeGreen;
        [SerializeField] private bool endMaze = true;
        [SerializeField] private GameObject endDoorPrefab;
        private PhotonView _gameManagerPhotonView;
        private PhotonView targetRpcView;

        private void Start()
        {
            _gameManagerPhotonView = GameObject.FindWithTag("GameManager").GetComponent<PhotonView>();
            targetRpcView = GameObject.Find(endDoorPrefab.name + "(Clone)").GetPhotonView();
        }

        public void FixedUpdate()
        {
            if (mazeCubeBlue.activeSelf && mazeCubeGreen.activeSelf && endMaze)
            {
                targetRpcView.RPC("OpenDoorRPC",PhotonTargets.All);
                _gameManagerPhotonView.RPC("EndedPuzzle", PhotonTargets.Others);
                endMaze = false;
            }
        }
    }
}
