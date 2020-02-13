using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private Text ObjectviveText;
    [SerializeField] private Text InteractableHintText;
    [SerializeField] private GameObject aim;

    // Start is called before the first frame update
    void Start()
    {
        InteractableHintText.gameObject.SetActive(false);
        aim.SetActive(false);
    }

    public void ShowInteractableHint()
    {
        InteractableHintText.gameObject.SetActive(true);
    }
    
    public void HideInteractableHint()
    {
        InteractableHintText.gameObject.SetActive(false);
    }

    public void ActivateAim()
    {
        aim.SetActive(true);
    }
    
    public void DeactivateAim()
    {
        aim.SetActive(false);
    }
}
