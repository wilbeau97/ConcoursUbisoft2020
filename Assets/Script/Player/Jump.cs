using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, IPunObservable
{
    [SerializeField] private Animator animator;
    [SerializeField]private float jumpForceY = 7f;
    public float height = 0.05f;
    [SerializeField] private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;
    private static bool canDoubleJump = true;
    private Collider playerCollider;
    [SerializeField] private PhysicMaterial slideMaterial;
    private bool matIsOn = true;
    private PhotonView view;
    private bool isDoubleJumping = false;

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
        if (PhotonNetwork.connected)
        {
            if (!view.isMine) return;
        }
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, out hit, height);
        Debug.DrawRay(transform.position, -Vector3.up, Color.red);

        if (isGrounded)
        {
            if (isDoubleJumping)
            {
                animator.SetTrigger("DoubleJumpEnd");
                isDoubleJumping = false;
            }
            if (hit.collider.CompareTag("Jumpable"))
            {
                if (matIsOn)
                {
                    matIsOn = false;
                    view.RPC("RemoveSlideMaterialRpc", PhotonTargets.All);
                }
            }
            else
            {
                if (!matIsOn)
                {
                    matIsOn = true;
                    view.RPC("AddSlideMaterialRpc", PhotonTargets.All);
                }
            }
            //a terre
            if (Input.GetButtonDown("Jump") && nbJump < 1)
            {
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
            if (nbJump >= 1 && Input.GetButtonDown("Jump"))
            {
                DJumping();
                isDoubleJumping = true;
                nbJump = 0;
            }
        }
    }

    private void Jumping()
    {
        animator.SetTrigger("Jump");
        Vector3 jumpForce;
        jumpForce = new Vector3(0, jumpForceY, 0);
        rb.AddForce(jumpForce, ForceMode.VelocityChange);
    }
    private void DJumping()
    {
        animator.SetTrigger("DoubleJump");
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
