using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using Script.Audio;
using UnityEngine;

public class Jump : MonoBehaviour, IPunObservable
{
    [SerializeField] private Animator animator;
    [SerializeField]private float jumpForceY = 7f;
    public float height = 0.1f;
    [SerializeField] private bool canJump = true;
    private int nbJump = 0;
    private Rigidbody rb;
    private static bool canDoubleJump = true;
    private Collider playerCollider;
    [SerializeField] private PhysicMaterial slideMaterial;
    private bool matIsOn = true;
    private PhotonView view;
    private bool isJumpImpactSoundEnabled = true; // sera désactivé après le tutoriel pour pas que p2 entendne les bruits de jump
    private bool isDoubleJumping = false;
    private bool isGrounded = true;


    // Start is called before the first frame update
    void Awake()
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
        if(Input.GetButtonDown("Jump")) { checkInput();}
    }

    private void FixedUpdate()
    {
        // here goes physics 
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, out var hit, height);
        Debug.DrawRay(transform.position, -Vector3.up, Color.magenta);
        if (isGrounded)
        {
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
        }
   
        
    }

    void checkInput()
    {
        if (!isGrounded)
        {
            if (nbJump >= 1)
            {
                Debug.Log("is Double jumping");
                DJumping();
                isDoubleJumping = true;
                nbJump = 0;
            }
        }
        if (isGrounded)
        {
            if (isDoubleJumping)
            {
                animator.SetTrigger("DoubleJumpEnd");
                isDoubleJumping = false;
            }
            //a terre
            if (nbJump < 1)
            {
                Debug.Log("is jumping");
                AudioManager.Instance.Play("jump", transform);
                Jumping();
                nbJump++;
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
        // Debug.Log(" Removed material ");
    }
    
    [PunRPC]
    public void AddSlideMaterialRpc()
    {
        playerCollider.material = slideMaterial;
        // Debug.Log(" Added material ");

    }

    public void OnCollisionEnter(Collision other)
    {
        if (!other.collider.CompareTag("Jumpable"))
        {
            view.RPC("AddSlideMaterialRpc", PhotonTargets.All);
        }
        if ((other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Jumpable")))
        {
            if (isJumpImpactSoundEnabled)
            {
                if (other.relativeVelocity.magnitude > 2)
                {
                    AudioManager.Instance.Play("afterJump", transform);
                    
                    nbJump = 0; // TODO A DEPLACER 
                }
            }
        }
    }

    public void disableJumpDropSoundForP2()
    {
        if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
        {
            isJumpImpactSoundEnabled = false;
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
