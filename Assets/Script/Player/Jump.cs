using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, IPunObservable
{
    [SerializeField] private Animator animator;
    [SerializeField]private float jumpForceY = 7f;
    public float height = 1.05f;
    [SerializeField] private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;
    private static bool canDoubleJump = true;
    private Collider playerCollider;
    [SerializeField] private PhysicMaterial slideMaterial;
    private bool matIsOn = true;
    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        view = gameObject.GetPhotonView();
    }

    // Update is called once per frame
    void Update()
    {
        if (!view.isMine) return;
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, height);

        Vector3 jumpForce = Vector3.zero;
        
        if (isGrounded)
        {
            if (hit.collider.CompareTag("Jumpable"))
            {
                if (matIsOn && PhotonNetwork.connected)
                {
                    matIsOn = false;
                    view.RPC("RemoveSlideMaterialRpc", PhotonTargets.All);
                }
            }
            else
            {
                if (!matIsOn && PhotonNetwork.connected)
                {
                    matIsOn = true;
                    view.RPC("AddSlideMaterialRpc", PhotonTargets.All);
                }
            }
            //a terre
            if (Input.GetButtonDown("Jump") && nbJump <= 1)
            {
                animator.SetTrigger("Jump");
                Jumping();
                nbJump++;
            }
            else
            {
                nbJump = 0;
            }
        }
        else
        {
            //dans les air
            if (nbJump <= 1 && Input.GetButtonDown("Jump") && canDoubleJump)
            {
                Jumping();
                nbJump = 2;
            }
        }
    }

    private void Jumping()
    {
        Vector3 jumpForce;
        jumpForce = new Vector3(0, jumpForceY, 0);
        rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }

    public void IncreaseAbility()
    {
        canDoubleJump = true;
    }

    public void IncreaseJumpForce()
    {
        jumpForceY = 8f;
    }

    [PunRPC]
    public void RemoveSlideMaterialRpc()
    {
        playerCollider.material = null;
    }
    
    [PunRPC]
    public void AddSlideMaterialRpc()
    {
        
        playerCollider.material = slideMaterial;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Jumpable"))
        {
            view.RPC("AddSlideMaterialRpc", PhotonTargets.All);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(matIsOn);
        } else if (stream.isReading)
        {
            matIsOn = (bool) stream.ReceiveNext();
        }
    }
}
