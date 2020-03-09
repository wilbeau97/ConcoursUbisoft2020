using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLastDoor;
    public bool alreadyOpen;
    [SerializeField] private Transform door;
    [SerializeField] private float maxOpeningHeight = 20;
    [SerializeField] private bool releaseRigidBody = true;
    [SerializeField] private float openingSpeed = 1;
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

    [PunRPC]
    public void OpenDoorRPC()
    {
        StartCoroutine(OpenDoor());
        alreadyOpen = true;
    }

    [PunRPC]
    public void CloseDoorRPC()
    {
        StopCoroutine(OpenDoor());
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

        if (releaseRigidBody)
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    
    public IEnumerator CloseDoor()
    {
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
}
