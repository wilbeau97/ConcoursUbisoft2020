using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private InGameMenu inGameMenu;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
        audioManager.musicVolumeMultiplicator = value;
    }

    public void SFXSliderChanged(float value)
    {
        audioManager.sfxVolumeMultiplicator = value;
    }

    public void OKButtonPressed()
    {
        inGameMenu.ActivateMainMenu();
    }
}
