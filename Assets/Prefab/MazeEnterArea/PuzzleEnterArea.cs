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
        [SerializeField] private PhotonView puzzleEnterDoorView;

        public void Start()
        {
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
            if (other.CompareTag("Player1") || other.CompareTag("Player2"))
            {
                //textArea.text = objectif;
                nbPlayerInPuzzle++;
                if (nbPlayerInPuzzle == 2)
                {
                    puzzleEnterDoorView.RPC("CloseDoorRPC", PhotonTargets.All);
                }
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                stream.SendNext(nbPlayerInPuzzle);
            } else if (stream.isReading)
            {
                nbPlayerInPuzzle = (int) stream.ReceiveNext();
            }
        }
    }
}
