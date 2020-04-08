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
    private bool _viewIsMine;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        view = gameObject.GetPhotonView();
        if (PhotonNetwork.connected)
        {
            _viewIsMine = view.isMine;
        }
        else
        {
            _viewIsMine = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_viewIsMine)
        {
            if(Input.GetButtonDown("Jump")) { checkInput();}
            if(!isGrounded){CheckRaycast();}    
        }
        
        Debug.DrawRay(transform.position, -Vector3.up, Color.magenta);
        
    }

    private void FixedUpdate()
    {
        if (isDoubleJumping && isGrounded && _viewIsMine)
        {
            animator.SetTrigger("DoubleJumpEnd");
            isDoubleJumping = false;
        }
        

    }

    void CheckRaycast()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, out var hit, height);
        // Debug.Log("Raycast checked and is hitting ? " + isGrounded);
        if (_viewIsMine || !PhotonNetwork.connected)
        {
            if (isGrounded)
            {
                bool isTouchingGround = checkForGroundTagOnObject(hit.collider.gameObject);
                
                if (!isTouchingGround)
                {
                    isGrounded = false;
                    return;
                }
                if (matIsOn) // Si le materiel de slide est ON (donc on glisse)
                {
                    // Et qu'on hit du ground ou jumpable, on l'enlève pour pas glisser
                    if (isTouchingGround)
                    {
                        Debug.Log("MatIsOn and i've hit : " + hit.collider.name);
                        matIsOn = false;
                        view.RPC("RemoveSlideMaterialRpc", PhotonTargets.All);
                    }
                }
                if (!matIsOn && !isTouchingGround) // Si le slide material n'est pas la et ne touche pas le sol/jumpable
                {
                    // on ajoute le material 
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
                // Debug.Log("is Double jumping");
                DJumping();
                isDoubleJumping = true;
                nbJump = 0;
            }
        }
        if (isGrounded)
        {
            //a terre
            if (nbJump < 1)
            {
                // Debug.Log("is jumping");
                AudioManager.Instance.Play("jump", transform);
                Jumping();
                nbJump++;
                CheckRaycast();
            }
        } 
    }
    
    private bool checkForGroundTagOnObject(GameObject gameObject)
    {
        bool isTouchingGround = gameObject.CompareTag("Ground") || gameObject.CompareTag("Jumpable") ||
                                gameObject.CompareTag("InteractablePhysicsObject") ||
                                gameObject.CompareTag("InteractableHeavyPhysicsObject");
        return isTouchingGround;
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
    public void disableJumpDropSoundForP2()
    {
        if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
        {
            isJumpImpactSoundEnabled = false;
        }
    }
    [PunRPC]
    public void RemoveSlideMaterialRpc()
    {
        playerCollider.material = null;
        Debug.Log(" Removed material and isGrounded = " + isGrounded);
    }
    
    [PunRPC]
    public void AddSlideMaterialRpc()
    {
        playerCollider.material = slideMaterial;
        Debug.Log(" Added material and isGrounded = " + isGrounded);

    }

    public void OnCollisionEnter(Collision other)
    {
        bool isTouchingGround = checkForGroundTagOnObject(other.gameObject);
        if (isTouchingGround)
        {
            isGrounded = true;
            nbJump = 0; // TODO A DEPLACER 
            if (isJumpImpactSoundEnabled)
            {
                if (other.relativeVelocity.magnitude > 2)
                {
                    AudioManager.Instance.Play("afterJump", transform);
                    
                }
            }
        }
    }

    public void OnCollisionExit(Collision other)
    {
        bool isTouchingGround = checkForGroundTagOnObject(other.gameObject);
        if (isTouchingGround)
        {
            CheckRaycast();
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
