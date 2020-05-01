using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialP2 : Tutorial
{
    [SerializeField] private MonoBehaviour telekinesisScript;
    [SerializeField] private GameObject abilityTutorialText;
    [SerializeField] private GameObject heavyUpgradeText;
    private bool canDeactivate = false;

    // Start is called before the first frame update
    void Start()
    {
        abilityTutorialText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("TelekinesisMove") != 0 && canDeactivate)
        {
            DesactivateTutorial();
        }
    }

    public override void ActivateTutorial()
    {
        abilityTutorialText.SetActive(true);
        telekinesisScript.enabled = true;
        canDeactivate = true;
    }

    public override void DesactivateTutorial()
    {
        abilityTutorialText.SetActive(false);
        this.enabled = false;
    }

    public void activateHeavyUpdateText()
    {
        StartCoroutine(CoroutineHeavyUpdateText());
        IEnumerator CoroutineHeavyUpdateText()
        {
            heavyUpgradeText.SetActive(true);
            yield return new WaitForSeconds(5);
            disableHeavyUpdateText();
        }
    }

    public void disableHeavyUpdateText()
    {
        heavyUpgradeText.SetActive(false);
    }
}
