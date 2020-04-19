using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    private float musicVolume = 1.0f;

    private float sfxVolume = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.parent.parent.name == "Player 1")
        {
            
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OKButtonPressed();
        }
    }

    public void MusicSliderChanged(float value)
    {
        musicVolume = value;
    }

    public void SFXSliderChanged(float value)
    {
        sfxVolume = value;
    }

    public void OKButtonPressed()
    {
        mainMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
