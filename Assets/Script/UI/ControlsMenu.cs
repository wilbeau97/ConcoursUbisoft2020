using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private RawImage image;
    [SerializeField] private Texture controlsP1;
    [SerializeField] private Texture controlsP2;
    [SerializeField] private InGameMenu inGameMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OKButtonPressed();
        }
    }
    
    public void OKButtonPressed()
    {
        inGameMenu.ActivateMainMenu();
    }
}
