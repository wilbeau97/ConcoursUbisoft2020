﻿using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;

public class Jump : MonoBehaviour, IPunObservable
{
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
            if (Input.GetButtonDown("Jump") && nbJump <= 1)
            {
                jumpForce = new Vector3(0, jumpForceY, 0);
                rb.AddForce(jumpForce, ForceMode.VelocityChange);
                AudioManager.Instance.Play("jump", transform);
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
                jumpForce = new Vector3(0, jumpForceY, 0);
                rb.AddForce(jumpForce, ForceMode.VelocityChange);
                AudioManager.Instance.Play("doubleJump", transform);
                nbJump = 2;
            }
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
            AudioManager.Instance.Play("afterJump", transform);
        }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Jumpable"))
        {
            AudioManager.Instance.Play("afterJump", transform);
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
