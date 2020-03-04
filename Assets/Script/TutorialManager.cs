using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private bool isActivated = false;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player1") || other.CompareTag("Player2")) && !isActivated)
        {
            if (other.gameObject.GetPhotonView().isMine)
            {
                other.gameObject.GetComponent<Tutorial>().ActivateTutorialPuzzle1();
                isActivated = true;
            }

            if (!PhotonNetwork.connected)
            {
                other.gameObject.GetComponent<Tutorial>().ActivateTutorialPuzzle1();
                isActivated = true;
            }
        }
    }
}
