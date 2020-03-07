using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private float maxOpeningHeight = 20;
    [SerializeField] private bool releaseRigidBody = true;
    [SerializeField] private float openingSpeed = 1;
    private bool open = false;
    private float initialPositionY;
    

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        if (releaseRigidBody)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        initialPositionY = door.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void OpenDoorRPC()
    {
        StartCoroutine(OpenDoor());
    }

    [PunRPC]
    public void CloseDoorRPC()
    {
        StartCoroutine(CloseDoor());
    }
    

    public IEnumerator OpenDoor()
    {
        StopCoroutine(CloseDoor());
        while (door.position.y <= maxOpeningHeight+ initialPositionY)
        {
            door.position += new Vector3(0, openingSpeed, 0);
            yield return null;
        }
        
        if(releaseRigidBody){_rigidbody.constraints = RigidbodyConstraints.FreezeAll;}
    }
    
    public IEnumerator CloseDoor()
    {
        StopCoroutine(OpenDoor());
        if (releaseRigidBody) {
            _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        } 
        else {
            while (door.position.y > initialPositionY)
            {
                door.position -= new Vector3(0, openingSpeed, 0);
                yield return null;
            }
        }
        yield return null;
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
