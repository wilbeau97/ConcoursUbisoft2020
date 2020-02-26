using System;
using UnityEngine;
using UnityEngine.UI;

namespace Prefab.MazeEnterArea
{
    public class PuzzleEnterArea : MonoBehaviour
    {
        private Text textArea;
        public string objectif;

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
            textArea.text = objectif;
        }
    }
}
