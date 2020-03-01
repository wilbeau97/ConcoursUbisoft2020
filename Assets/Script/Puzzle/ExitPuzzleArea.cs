using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPuzzleArea : MonoBehaviour
{
    private PhotonView gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetPhotonView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (other.gameObject.GetPhotonView().isMine) 
            {
                gm.RPC("EndedPuzzle", PhotonTargets.All);
            }
        }
    }
}
