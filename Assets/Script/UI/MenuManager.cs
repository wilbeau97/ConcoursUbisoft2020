using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject roomMenu;
        [SerializeField] private GameObject typeRoomToJoinPanel;
        [SerializeField] private GameObject typeRoomToCreatePanel;
        [SerializeField] private Button player1Button;
        [SerializeField] private Button startButton;
        [SerializeField] private Button player2Button;
        [SerializeField] private InputField roomNameCreate;
        [SerializeField] private Dropdown roomNameJoin;
        [SerializeField] private Launcher launcher;
        
        // Start is called before the first frame update
        void Start()
        {
            startButton.Select();
        }

        public void Play()
        {
            startMenu.SetActive(false);
            roomMenu.SetActive(true);
            player1Button.Select();
        }

        public void CreateRoom()
        {
            RoomInfo[] rooms = PhotonNetwork.GetRoomList();

            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].Name == roomNameCreate.text)
                {
                    Debug.Log("This room exists !");
                    return;
                }
            } 
            
            launcher.roomName = roomNameCreate.text;
            launcher.Connect();
            roomMenu.SetActive(true);
            startMenu.SetActive(false);
        }

        public void TypeRoomNameToJoin()
        {
            typeRoomToJoinPanel.SetActive(true);
            roomNameJoin.options.Clear();
            
            RoomInfo[] rooms = PhotonNetwork.GetRoomList();
            List<string> roomNames = new List<string>();
            
            for (int i = 0; i < rooms.Length; i++)
            {
                if(rooms[i].PlayerCount < 2)
                    roomNames.Add(rooms[i].Name);
            }
            
            roomNameJoin.AddOptions(roomNames);
        }

        public void TypeRoomNameToCreate()
        {
            typeRoomToCreatePanel.SetActive(true);
        }
        
        public void CancelJoin()
        {
            typeRoomToJoinPanel.SetActive(false);   
            typeRoomToCreatePanel.SetActive(false);   
        }
        
        public void JoinRoom()
        {
            RoomInfo[] rooms = PhotonNetwork.GetRoomList();

            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].Name == roomNameJoin.options[roomNameJoin.value].text)
                {
                    Debug.Log("This room exists !");
                    launcher.roomName = roomNameJoin.options[roomNameJoin.value].text;
                    launcher.Connect();
                    startMenu.SetActive(false);
                    roomMenu.SetActive(true);
                    return;
                }
            }
            
            Debug.Log("Room doesn't exist!");
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void ActivatedButtonPlayer1()
        {
            if(!gameObject.GetPhotonView().isMine)
                player1Button.Select();
        }
        
        public void ActivatedButtonPlayer2()
        {
            if(!gameObject.GetPhotonView().isMine)
                player2Button.Select();
        }
    }

