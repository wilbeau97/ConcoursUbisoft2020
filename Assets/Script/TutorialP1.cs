using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialP1 : Tutorial
{
    [SerializeField] private MonoBehaviour jumpScript;
    [SerializeField] private GameObject abilityTutorialText;
    [SerializeField] private Text jumpUpgradeText;
    [SerializeField] private GameObject jumpUgradeForceText;

    private bool canDeactivate = false;

    // Start is called before the first frame update
    void Start()
    {
        if (abilityTutorialText != null)
        {
            abilityTutorialText.SetActive(false);
        }
        jumpUpgradeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && canDeactivate)
        {
            DesactivateTutorial();
        }
    }

    public override void ActivateTutorial()
    {
        if (abilityTutorialText != null)
        {
            jumpScript.enabled = true;
            canDeactivate = true;
            abilityTutorialText.SetActive(true);
        }
    }
    
    public override void DesactivateTutorial()
    {
        if (abilityTutorialText != null)
        {
            abilityTutorialText.SetActive(false);
        }
        this.enabled = false;
    }
    
    
    public void activateDoubleJumpText()
    {
        StartCoroutine(coroutineJumpText());
        IEnumerator coroutineJumpText()
        {
            jumpUpgradeText.gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            disableDoubleJumpText();
        }
    }

    public void disableDoubleJumpText()
    {
        jumpUpgradeText.gameObject.SetActive(false);
    }
    
    public void ActivatejumpUgradeForceText()
    {
        StartCoroutine(CoroutineJumpUpgradeForceText());
        IEnumerator CoroutineJumpUpgradeForceText()
        {
            jumpUgradeForceText.SetActive(true);
            yield return new WaitForSeconds(5);
            DisableJumpForceText();
        }
    }
    
    public void DisableJumpForceText()
    {
        jumpUgradeForceText.SetActive(false);
    }
    
    
    


}
