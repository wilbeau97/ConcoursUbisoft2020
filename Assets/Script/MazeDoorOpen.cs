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
        public PhotonView targetRpcView;

        public void FixedUpdate()
        {
            if (mazeCubeBlue.activeSelf && mazeCubeGreen.activeSelf && endMaze)
            {
                targetRpcView.RPC("OpenDoorRPC",PhotonTargets.All);
                endMaze = false;
            }
        }
    }
}
