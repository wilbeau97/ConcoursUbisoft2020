using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prefab.MazeEnterArea
{
    public class PuzzleEnterArea : MonoBehaviour, IPunObservable
    {
        private Text textArea;
        public string objectif;
        private int nbPlayerInPuzzle = 0;
        private bool player1IsIn = false;
        private bool player2IsIn = false;
        [SerializeField] private PhotonView puzzleEnterDoorView;

        public void Start()
        {
            nbPlayerInPuzzle = 0;
            GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            if (player1 != null)
            {
                Text[] texts = player1.GetComponentsInChildren<Text>();
                foreach (Text text in texts)
                {
                    if (text.name.Equals("ObjectiveText"))
                    {
                        textArea = text;
                    }
                }
            }
            else if (player2 != null)
            {
                Text[] texts = player2.GetComponentsInChildren<Text>();
                foreach (Text text in texts)
                {
                    if (text.name.Equals("ObjectiveText"))
                    {
                        textArea = text;
                    }
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player1"))
            {
                player1IsIn = true;
            }
            
            if(other.CompareTag("Player2"))
            {
                player2IsIn = true;
            }

            if (player1IsIn && player2IsIn)
            {
                puzzleEnterDoorView.RPC("CloseDoorRPC", PhotonTargets.All);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player1"))
            {
                player1IsIn = false;
            }
            if(other.CompareTag("Player2"))
            {
                player2IsIn = false;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(player1IsIn);
                stream.SendNext(player2IsIn);
            } else if (stream.isReading)
            {
                player1IsIn = (bool) stream.ReceiveNext();
                player2IsIn = (bool) stream.ReceiveNext();
            }
        }
    }
}
