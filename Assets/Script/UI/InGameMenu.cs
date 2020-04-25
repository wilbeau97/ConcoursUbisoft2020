using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button optionsButton;
    private Button lastSelectButton;

    public bool menuShown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        lastSelectButton = controlsButton;
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
        lastSelectButton = optionsButton;
        SelectButton(optionsMenu.transform.GetChild(0).gameObject.GetComponentInChildren<Button>());
        mainMenu.SetActive(false);
    }

    public void ControlsPressed()
    {
        controlsMenu.SetActive(true);
        lastSelectButton = controlsButton;
        SelectButton(controlsMenu.GetComponentInChildren<Button>());
        mainMenu.SetActive(false);
    }
    
    public void QuitterPressed()
    {
        Application.Quit();
    }

    public void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
        SelectButton(lastSelectButton);
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

    public void SelectButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(null);
        button.Select();
    }

}
