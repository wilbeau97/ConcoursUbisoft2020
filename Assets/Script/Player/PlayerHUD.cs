using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Text InteractableHintText;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject tuto;
    [SerializeField] private GameObject tutorialConceptArt;
    [SerializeField] private GameObject canDeactivateText;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Animator fadeAnimator;
    private Text ObjectviveText;
    private bool isDesactivated = false;
    private bool canDeactivateConceptArt = false;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.gameObject.SetActive(false);
        aim.SetActive(false);
        tutorialConceptArt.SetActive(false);
        canDeactivateText.SetActive(false);
    }

    private void Update()
    {
        if (!isDesactivated)
        {
            if (Input.GetButtonDown("Start"))
            {
                DeactivateTuto();
            }
        }

        if (canDeactivateConceptArt)
        {
            if (Input.GetButtonDown("Start"))
            {
                DeactivateConceptArt();
            }
        }
    }

    private void DeactivateConceptArt()
    {
       tutorialConceptArt.SetActive(false);
       canDeactivateText.SetActive(false);
    }
    
    public void ActivateConceptArt()
    {
        tutorialConceptArt.SetActive(true);
        StartCoroutine("ShowConceptArt");
    }

    private IEnumerator ShowConceptArt()
    {
        yield return new WaitForSeconds(3f);
        //show can desactivate
        canDeactivateText.SetActive(true);
        canDeactivateConceptArt = true;
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
    public void DeactivateTuto()
    {
        isDesactivated = true;
        tuto.SetActive(false);
    }

    public void setText(string s)
    {
        tutorialText.gameObject.SetActive(true);
        tutorialText.text = s;
    }

    public void DeactivateAbilityText()
    {
        tutorialText.gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
        
    }
}
