using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform door;
    private bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void OpenDoorRpc()
    {
        StartCoroutine(OpenDoor());
    }
    
    private IEnumerator OpenDoor()
    {
        while (door.position.y < 20)
        {
            door.position += new Vector3(0, 1, 0);
            yield return null;
        }

        open = true;
    }
    
    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(open);
        } else if (stream.isReading)
        {
            open = (bool) stream.ReceiveNext();
        }
    }
}
