using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform door;
    private bool open = false;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
    

    private IEnumerator OpenDoor()
    {
        Debug.Log("ouverture porte");
        while (door.position.y < 20)
        {
            door.position += new Vector3(0, 1, 0);
            yield return null;
        }
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    private IEnumerator CloseDoor()
    {
        // while (door.position.y > 4)
        // {
        //     door.position -= new Vector3(0, 1, 0);
        //     yield return null;
        // }
        _rigidbody.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        yield return null;
    }
}
