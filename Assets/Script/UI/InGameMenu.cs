using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private GameObject controlsMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            this.gameObject.SetActive(false);
        }
    }

    public void OptionsPressed()
    {
        optionsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ControlsPressed()
    {
        controlsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
    public void QuitterPressed()
    {
        Application.Quit();
    }
}
