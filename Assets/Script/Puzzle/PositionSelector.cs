using System;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PhotonView))]
public class PositionSelector : Photon.MonoBehaviour
    {
        public PuzzleGenerator assignedPuzzleGenerator;
        private bool is1StPlaytrough = true;
        
        public int selectedPosition = 0;
        // liste des positions possible de cet objet pour un premier playthrough
        private Transform gameObjectPosition;
        public Transform[] possiblePositions1StPlaytrough;
        // liste des positions possible de cet objet pour les autres playthrough avec tous les skills
        public Transform[] possiblePositions2NdPlaytrough;
        private string localPlayerName;
    
        void Awake()
        {
            gameObjectPosition = GetComponent<Transform>();
        }

        public void RandomSelectPosition()
        {
            if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
            {
                if (is1StPlaytrough) // si c'est la première playthrough
                {
                    if (possiblePositions1StPlaytrough.Length != 0) // si aucune position, on garde la position actuelle
                    {
                        int index = Random.Range(0, possiblePositions1StPlaytrough.Length);
                        selectedPosition = index;
                        gameObjectPosition.transform.position =
                            possiblePositions1StPlaytrough[index].transform.position;
                    }
                }

                if (!is1StPlaytrough)
                {
                    if (possiblePositions2NdPlaytrough.Length == 0)
                        return; // si aucune position, on garde la dernière position
                    int index = Random.Range(0, possiblePositions2NdPlaytrough.Length);
                    selectedPosition = index;
                    gameObjectPosition.transform.position = possiblePositions2NdPlaytrough[index].transform.position;
                }
            }
        }
        
        // Vient mettre à jour la position du Player2(le p1 est le responsable du choix de position)
        public void updatePositions()
        {
            if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
            {
                // Indique au P2 de mettre à jour sa position avec celle que le P1 à sélectionné 
                photonView.RPC("setObjectPosition", PhotonTargets.OthersBuffered, selectedPosition);
            }
        }
        // fonction qui permet de seter le choix de position fait par l'autre joueur 
        [PunRPC]
        public void setObjectPosition(int arrayPosition)
        {
            Debug.Log("Setting position" + PlayerManager.LocalPlayerInstance.name);
            if (is1StPlaytrough)
            {
                if (possiblePositions1StPlaytrough.Length != 0 &&
                    (arrayPosition <= possiblePositions1StPlaytrough.Length))
                {
                    selectedPosition = arrayPosition;
                    gameObjectPosition.transform.position = possiblePositions1StPlaytrough[arrayPosition].transform.position;
                    return;
                }
            }
            if (!is1StPlaytrough)
            {
                if (possiblePositions2NdPlaytrough.Length != 0 && 
                    (arrayPosition <= possiblePositions2NdPlaytrough.Length))
                {
                    selectedPosition = arrayPosition;
                    gameObjectPosition.transform.position = possiblePositions2NdPlaytrough[arrayPosition].transform.position;
                }
            }
            else
            {
                Debug.LogError("Could not setObject position of Position Selector : " + this.name);
            }
        }
        
        // Détermine si on est au premier playthrough ou que le jeu a été fait au moins une fois
        public void SetIs1StPlaythrough(bool pIs1StPlaythrough)
        {
            is1StPlaytrough = pIs1StPlaythrough;
        }

        public int GetSelectedPosition()
        {
            return selectedPosition;
        }

        public void SetLocalPlayerName(string pLocalPlayerName)
        {
            localPlayerName = pLocalPlayerName;
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (localPlayerName == "Player 1(Clone)") // c'est le player 1 qui vient choisir et envoyé la bonne position de la plateforme
            {
                if (stream.isWriting)
                {
                    Debug.Log("Envoi de la position à l'autre client");
                    stream.SendNext(selectedPosition);
                } else if (stream.isReading)
                {
                    selectedPosition = (int) stream.ReceiveNext();
                }
            }
            else if (stream.isReading)
            {
                Debug.Log("Is reading new position");
                selectedPosition = (int) stream.ReceiveNext();
            }
            
        }
        
    }

