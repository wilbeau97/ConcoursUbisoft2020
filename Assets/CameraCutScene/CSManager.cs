using System;
using System.Collections.Generic;
using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

namespace CameraCutScene
{
    public class CSManager : MonoBehaviour
    {
        private readonly List<GameObject> cameras = new List<GameObject>();
        public static CSManager Instance;
        private static PhotonView _photonView;
    
        // Start is called before the first frame update
        void Start()
        {
            if (Instance == null && gameObject.activeSelf)
            {
                Instance = this;
                _photonView = gameObject.GetPhotonView();
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    cameras.Add(gameObject.transform.GetChild(i).gameObject);
                }
            }
        }

        public void StartCs(int nbPuzzle)
        {
            string camName = "Camera" + (nbPuzzle + 1);
            _photonView.RPC("RoutineStartCam",PhotonTargets.All, camName);
        }
        
        [PunRPC]
        public void RoutineStartCam(string camName)
        {
            foreach (GameObject camera in cameras)
            {
                if (camera.name.Equals(camName))
                {
                    camera.SetActive(true);
                    break;
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown("c"))
            {
                StartCs(0);
            }
            if (Input.GetKeyDown("v"))
            {
                StartCs(1);
            }
            if (Input.GetKeyDown("b"))
            {
                StartCs(2);
            }
            if (Input.GetKeyDown("n"))
            {
                StartCs(3);
            }
            if (Input.GetKeyDown("m"))
            {
                StartCs(4);
            }
#if UNITY_EDITOR
            if (Input.GetKeyDown("m"))
            {
                RoutineStartCam("Camera5");
            }
#endif
        }
    }
}
