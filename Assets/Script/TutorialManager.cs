using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private bool isStarted = false;
    private bool isUsedAbility = false;
    private bool isPuzzleEnded = false;

    // Update is called once per frame
    void Update()
    {
        if (!isStarted)
        {
            //press start to continue (enlever le texte de debut)
            StartCoroutine("TutoTexte");
            isStarted = true;
        }
    }

    private IEnumerator TutoTexte()
    {
        while (!Input.GetButtonDown("Start"))
        {
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (other.gameObject.GetPhotonView().isMine)
            {
                other.gameObject.GetComponent<Ability>().ActivateTutorial();
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (other.gameObject.GetPhotonView().isMine)
            {
                other.gameObject.GetComponent<Ability>().DesactivateTutorial();
                enabled = false;
            }
        }
    }
}

public enum TutorialEnum
{
    START,
    ABILITY,
    ENDED_PUZZLE
}
