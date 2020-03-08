using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialP2 : Tutorial
{
    [SerializeField] private MonoBehaviour telekinesisScript;
    [SerializeField] private Text abilityTutorialText;
    private bool canDeactivate = false;

    // Start is called before the first frame update
    void Start()
    {
        abilityTutorialText.gameObject.SetActive(false);
        telekinesisScript.enabled = false;
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
        abilityTutorialText.gameObject.SetActive(true);
        telekinesisScript.enabled = true;
        canDeactivate = true;
        abilityTutorialText.text =
            "Appuyez sur LT (left trigger) pour utilisé votre pouvoir de télékinésie sur les objets";
    }

    public override void DesactivateTutorial()
    {
        abilityTutorialText.gameObject.SetActive(false);
        this.enabled = false;
    }
}
