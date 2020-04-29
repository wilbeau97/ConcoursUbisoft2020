using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prefab.MazeEnterArea
{
    public class PuzzleEnterArea : MonoBehaviour, IPunObservable
    {
        private Text textArea;
        public string objectif;
        private bool player1IsIn = false;
        private bool player2IsIn = false;
        private PhotonView puzzleEnterDoorView;
        [SerializeField] private GameObject enterPuzzleDoor;

        public void Start()
        {
            puzzleEnterDoorView =  GameObject.Find(enterPuzzleDoor.name + "(Clone)").GetPhotonView();
        }

        public void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player1"))
            {
                player1IsIn = true;
                other.GetComponentInChildren<PlayerHUD>().SetObjectifText(objectif);
            }
            
            if(other.CompareTag("Player2"))
            {
                player2IsIn = true;
                other.GetComponentInChildren<PlayerHUD>().SetObjectifText(objectif);
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
                other.GetComponentInChildren<PlayerHUD>().SetDefaultObjectifText();
            }
            if(other.CompareTag("Player2"))
            {
                player2IsIn = false;
                other.GetComponentInChildren<PlayerHUD>().SetDefaultObjectifText();
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
