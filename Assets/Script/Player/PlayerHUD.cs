using System;
using System.Collections;
using System.Collections.Generic;
using CameraCutScene;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private GameObject InteractableHintText;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject tutorialConceptArt;
    [SerializeField] private GameObject canDeactivateText;
    [SerializeField] private GameObject abilityTextExplanation;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private GameObject explainationText;
    [SerializeField] private InGameMenu inGameMenu;
    [SerializeField] private Text ObjectviveText;
    private bool isDesactivated = false;
    private bool canDeactivateConceptArt = false;
    private bool isInGame = true;

    // Start is called before the first frame update
    void Start()
    {
        abilityTextExplanation.SetActive(false);
        aim.SetActive(false);
        tutorialConceptArt.SetActive(false);
        canDeactivateText.SetActive(false);
    }

    private void Update()
    {
        if (canDeactivateConceptArt)
        {
            if (Input.GetButtonDown("Interact"))
            {
                DeactivateConceptArt();
                CSManager.Instance.RoutineStartCam("Camera1");
                isInGame = true;
            }
        }

        if (isInGame)
        {
            if (Input.GetButtonDown("Start"))
            {
                if (inGameMenu.menuShown)
                    inGameMenu.DeactivateMainMenu();
                else
                    inGameMenu.ActivateMainMenu();
            }
        }
    }

    private void DeactivateConceptArt()
    {
       tutorialConceptArt.SetActive(false);
       canDeactivateText.SetActive(false);
       StartCoroutine(ShowExplainationText());
       canDeactivateConceptArt = false;
    }

    private IEnumerator ShowExplainationText()
    {
        explainationText.SetActive(true);
        yield return new WaitForSeconds(10f);
        explainationText.SetActive(false);
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
        InteractableHintText.SetActive(true);
    }
    
    public void HideInteractableHint()
    {
        InteractableHintText.SetActive(false);
    }

    public void ActivateAim()
    {
        aim.SetActive(true);
    }
    
    public void DeactivateAim()
    {
        aim.SetActive(false);
    }

    public void setText(string s)
    {
        abilityTextExplanation.SetActive(true);
        abilityTextExplanation.GetComponentInChildren<Text>().text = s;
    }

    public void DeactivateAbilityText()
    {
        abilityTextExplanation.SetActive(false);
    }

    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void SetObjectifText(string objectif)
    {
        ObjectviveText.text = objectif;
    }

    public void SetDefaultObjectifText()
    {
        ObjectviveText.text = "Rendez-vous à la prochaine porte pour relever le prochain puzzle.";
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
