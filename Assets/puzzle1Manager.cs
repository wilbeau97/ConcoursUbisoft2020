using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle1Manager : MonoBehaviour
{
    [SerializeField] private GameObject bridge;
    [SerializeField] private PhotonView doorView;

    [PunRPC]
    public void RotateBridgeToPass()
    {
        StopCoroutine("RotateBridgeToBlockRoutine");
        StartCoroutine("RotateBridgeToPassRoutine");
    }

    [PunRPC]
    public void RotateBridgeToBlock()
    {
        StopCoroutine("RotateBridgeToPassRoutine");
        StartCoroutine("RotateBridgeToBlockRoutine");
    }

    public IEnumerator RotateBridgeToPassRoutine()
    {
        //de quelque chose (90) a 0
        Quaternion from = bridge.transform.rotation;
        Quaternion to = bridge.transform.rotation;
        to *= Quaternion.Euler( Vector3.up * 60);
    
        float elapsed = 0.0f;
        while( elapsed < 4f )
        {
            bridge.transform.rotation = Quaternion.Slerp(from, to, elapsed / 4f );
            elapsed += Time.deltaTime;
            yield return null;
        }
        bridge.transform.rotation = to;
    }
    
    public IEnumerator RotateBridgeToBlockRoutine()
    {
        // de quelquechose (0) a 90
        Quaternion from = bridge.transform.rotation;
        Quaternion to = bridge.transform.rotation;
        to *= Quaternion.Euler( Vector3.up * -60);
    
        float elapsed = 0.0f;
        while( elapsed < 4f )
        {
            bridge.transform.rotation = Quaternion.Slerp(from, to, elapsed / 4f );
            elapsed += Time.deltaTime;
            yield return null;
        }
        bridge.transform.rotation = to;
    }
    
    public void OpenDoor()
    {
        doorView.RPC("OpenDoorRPC", PhotonTargets.All);
    }
    
   
}
