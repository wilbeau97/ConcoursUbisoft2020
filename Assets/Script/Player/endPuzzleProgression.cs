using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPuzzleProgression : Photon.MonoBehaviour
{
    [SerializeField] private string playerTagToLevelUp;
    private PhotonView gm;
    private bool alreadyTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetPhotonView();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTagToLevelUp))
        {
            other.GetComponent<TutorialP1>().activateDoubleJumpText();
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
