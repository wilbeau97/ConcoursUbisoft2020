using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    //[SerializeField] private GameObject playerGraphics;
    [SerializeField] private MonoBehaviour[] playerControlScript;

    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        
        Initialize();
    }


    private void Initialize()
    {
        //Handle not local player
        if(!photonView.isMine)
        {
            playerCamera.SetActive(false);
            //playerGraphics.SetActive(false);
            foreach (MonoBehaviour script in playerControlScript)
            {
                script.enabled = false;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
