using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUp : MonoBehaviour
{
    private PhotonView gm;
    private bool alreadyTrigger = false;
    [SerializeField] private string playerTagToBeLeveledUp; // le tag du joueur qui sera level up 

    private bool player1HasLvledUpJump = false;

    private bool player2HasLvledUpHeavy = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetPhotonView();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag(playerTagToBeLeveledUp) && !alreadyTrigger))
        {
            if (playerTagToBeLeveledUp == "Player1")
            {
                other.GetComponent<Jump>().IncreaseJumpForce();
                other.GetComponent<TutorialP1>().ActivatejumpUgradeForceText();
                alreadyTrigger = true;
            }

            if (playerTagToBeLeveledUp == "Player2")
            {
                other.GetComponent<TelekinesisAbility>().increaseMaxWeight();
                other.GetComponent<TutorialP2>().activateHeavyUpdateText();
                alreadyTrigger = true;
            }
        }
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(alreadyTrigger);
        } else if (stream.isReading)
        {
            alreadyTrigger = (bool) stream.ReceiveNext();
        }
    }
}
