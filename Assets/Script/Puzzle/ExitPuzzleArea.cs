using UnityEngine;

public class ExitPuzzleArea : Photon.MonoBehaviour
{
    private PhotonView gm;
    private bool alreadyTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetPhotonView();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player1") || other.CompareTag("Player2")) && !alreadyTrigger)
        {
            // gm.RPC("EndedPuzzle", PhotonTargets.All);
            alreadyTrigger = true;
        }

        if (other.CompareTag("Player1"))
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
