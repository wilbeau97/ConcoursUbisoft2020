using UnityEngine;

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject playerChoiceMenu;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void Play()
        {
            startMenu.SetActive(false);
            playerChoiceMenu.SetActive(true);
        }
    }

