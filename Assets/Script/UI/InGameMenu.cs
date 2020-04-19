using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private Button controlsButton;

    public bool menuShown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        controlsButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            mainMenu.SetActive(false);
            menuShown = false;
        }
    }

    public void OptionsPressed()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ControlsPressed()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    
    public void QuitterPressed()
    {
        Application.Quit();
    }

    public void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        menuShown = true;
    }

    public void DeactivateMainMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        menuShown = false;
    }
}
