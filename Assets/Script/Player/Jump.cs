using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using Script.Audio;
using UnityEngine;
using UnityEngine.Serialization;

public class Jump : MonoBehaviour, IPunObservable
{
    [SerializeField] private Animator animator;
    [SerializeField] private float jumpForceY = 7f;
    [SerializeField] private float doubleJumpForceY = 1.2f;
    [FormerlySerializedAs("height")] public float raycastMaxDistance = 0.1f;
    [SerializeField] private bool canJump = true;
    [SerializeField] private PhysicMaterial slideMaterial;
    [SerializeField] private bool canDoubleJump = true;
    private int _nbJump = 0;
    private Rigidbody _rigidbody;
    private Collider _playerCollider;
    private bool _slideMaterialIsOn = true;
    private PhotonView _photonView;
    private bool _isJumpImpactSoundEnabled = true; // sera désactivé après le tutoriel pour pas que p2 entendne les bruits de jump
    private bool _isDoubleJumping = false;
    private bool _isGrounded = true;
    private bool _viewIsMine = false; // vient checker si c'est le joueur qui jump, sinon on fait rien


    // Start is called before the first frame update
    void Awake()
    {
        GetComponentsAtAwake();
        _viewIsMine = !PhotonNetwork.connected || _photonView.isMine;
    }

    private void GetComponentsAtAwake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerCollider = GetComponent<Collider>();
        _photonView = gameObject.GetPhotonView();
    }

    // Update is called once per frame
    void Update()
    {
        if (_viewIsMine)
        {
            if(Input.GetButtonDown("Jump")) { ReadJumpInput(); }
            if(!_isGrounded) { CheckIfGrounded(); } // dès qu'on est dans les air, on regarde si on est grounded    
        }

        if (_slideMaterialIsOn)
        {
            CheckIfGrounded();
        }
        // Debug.DrawRay(transform.position, -Vector3.up, Color.magenta);
    }

    private void FixedUpdate()
    {
        CheckForDoubleJumpEnd();
    }

    private void CheckForDoubleJumpEnd()
    {
        if (_isDoubleJumping && _isGrounded && _viewIsMine)
        {
            animator.SetTrigger("DoubleJumpEnd");
            _isDoubleJumping = false;
        }
    }

    void ReadJumpInput()
    {   // Fonction qui check quoi faire avec le jump input
        if (!_isGrounded)
        {
            if (_nbJump >= 1)
            {
                DJumping();
            }
        }
        if (_isGrounded)
        {
            //a terre
            if (_nbJump < 1)
            {
                Jumping();
                CheckIfGrounded();
            }
        } 
    }
    
    private void Jumping()
    {
        AudioManager.Instance.Play("jump", transform);
        animator.SetTrigger("Jump");
        var jumpForce = new Vector3(0, jumpForceY, 0);
        _rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        _nbJump++;
    }
    private void DJumping()
    {
        AudioManager.Instance.Play("jump", transform);
        animator.SetTrigger("DoubleJump");
        var doubleJumpForce = new Vector3(0, jumpForceY * doubleJumpForceY, 0);
        _rigidbody.AddForce(doubleJumpForce, ForceMode.VelocityChange);
        _isDoubleJumping = true;
        _nbJump = 0;
    }
    
    void CheckIfGrounded()
    {
        if (!_viewIsMine && PhotonNetwork.connected) return;
        
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, out var hit, raycastMaxDistance);
        if (!_isGrounded) return;
        
        // parfois, le raycast va indiquer qu'il n'est pas grounder, alors que le player l'es 
        // ex: il est sur le bord d'une surface, le raycast, va être legerment out de la box d'une surface
        // Verifier avec le collider permet de faire une double vérification 
        var isTouchingGround = checkForGroundTagOnObject(hit.collider.gameObject);
        if (!isTouchingGround) 
        {
            _isGrounded = false;
            return;
        }
        CheckForSlideMaterial();
    }
    
    private bool checkForGroundTagOnObject(GameObject gameObject)
    {
        bool isTouchingGround = gameObject.CompareTag("Ground") || gameObject.CompareTag("Jumpable") ||
                                gameObject.CompareTag("InteractablePhysicsObject") ||
                                gameObject.CompareTag("InteractableHeavyPhysicsObject");
        return isTouchingGround;
    }
    
    private void CheckForSlideMaterial()
    {
        if (_slideMaterialIsOn) // Si le materiel de slide est ON (donc on glisse)
        {
            // Et qu'on hit du ground ou jumpable, on l'enlève pour pas glisser
            _slideMaterialIsOn = false;
            if(!PhotonNetwork.connected){RemoveSlideMaterialRpc();}
            _photonView.RPC("RemoveSlideMaterialRpc", PhotonTargets.All);
            return;
        }

        if (!_slideMaterialIsOn && _isGrounded) // Si le slide material n'est pas la et ne touche pas le sol/jumpable
        {
            // on ajoute le material 
            _slideMaterialIsOn = true;
            if(!PhotonNetwork.connected){AddSlideMaterialRpc();}
            _photonView.RPC("AddSlideMaterialRpc", PhotonTargets.All);
        }
    }

    public void IncreaseAbility()
    {
        canDoubleJump = true;
    }

    public void IncreaseJumpForce()
    {
        jumpForceY = 8f;
    }
    public void DisableJumpDropSoundForP2()
    {
        if (PlayerManager.LocalPlayerInstance.CompareTag("Player2"))
        {
            _isJumpImpactSoundEnabled = false;
        }
    }
    
    [PunRPC]
    public void RemoveSlideMaterialRpc()
    {
        _playerCollider.material = null;
        // Debug.Log(" Removed material and isGrounded = " + _isGrounded);
    }
    
    [PunRPC]
    public void AddSlideMaterialRpc()
    {
        _playerCollider.material = slideMaterial;
        // Debug.Log(" Added material and isGrounded = " + _isGrounded);
    }

    public void OnCollisionEnter(Collision other)
    {
        var isTouchingGround = checkForGroundTagOnObject(other.gameObject);
        if (!isTouchingGround) return;
        
        _isGrounded = true;
        _nbJump = 0; // TODO A DEPLACER 
        if (!_isJumpImpactSoundEnabled) return; // si on est p2, on joue pas le son apres tuto
        if (other.relativeVelocity.magnitude > 1)
        {
            AudioManager.Instance.Play("afterJump", transform);
        }
    }

    public void OnCollisionExit(Collision other)
    {
        var isTouchingGround = checkForGroundTagOnObject(other.gameObject);
        if (isTouchingGround)
        {
            CheckIfGrounded();
        }
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
