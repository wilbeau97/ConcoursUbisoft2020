using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
