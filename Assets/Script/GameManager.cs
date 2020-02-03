using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool plate1 = false;
    private bool plate2 = false;
    private bool open = false;
    [SerializeField] private PhotonView doorView;

    // Update is called once per frame
    void Update()
    {
        if (plate1 == true && plate2 == true && open == false)
        {
            doorView.RPC("OpenDoorRPC", PhotonTargets.All);
            open = true;
        }
    }

    [PunRPC] 
    public void Plate1(bool v)
    {
        plate1 = v;
    }
    
    [PunRPC] 
    public void Plate2(bool v)
    {
        plate2 = v;
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(plate1);
            stream.SendNext(plate2);
        } else if (stream.isReading)
        {
            plate1 = (bool) stream.ReceiveNext();
            plate2 = (bool) stream.ReceiveNext();
        }
    }
}
