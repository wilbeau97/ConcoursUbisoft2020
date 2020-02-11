using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject playerChoiceMenu;

        [SerializeField] private Button player1Button;
        [SerializeField] private Button startButton;
        [SerializeField] private Button player2Button;
        // Start is called before the first frame update
        void Start()
        {
            startButton.Select();
        }

        public void Play()
        {
            startMenu.SetActive(false);
            playerChoiceMenu.SetActive(true);
            player1Button.Select();
        }

        public void ActivatedButtonPlayer1()
        {
            player1Button.Select();
        }
        
        public void ActivatedButtonPlayer2()
        {
            player2Button.Select();
        }
    }

