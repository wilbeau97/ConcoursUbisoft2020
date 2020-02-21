using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            //if (other.gameObject.GetPhotonView().isMine)
            //{
                other.gameObject.GetComponent<Tutorial>().ActivateTutorial();
           // }
        }
    }
}
