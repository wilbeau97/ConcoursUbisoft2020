using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonsterChase : MonoBehaviour, IPunObservable
{
    public enum EnemiesColors
    {
        Bleu,
        Vert
    };

    [SerializeField] private EnemiesColors enemyColor = EnemiesColors.Bleu;

    [SerializeField] private Material matBleu;

    [SerializeField] private Material matVert;

    [SerializeField] private Transform[] pathWaypoints;

    [SerializeField] private int waitTimeOnWaypoint = 0;
    
    [SerializeField] private float chargeSpeed = 10.0f;

    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private Transform[] positionToYeet;
    
    private float elapsedTime = 0;
    private GameObject playerOne;
    private GameObject playerTwo;
    private MeshRenderer mesh;

    private bool player1InRange = false;

    private bool player2InRange = false;

    private bool player1LineOfSight = false;
    private bool player2LineOfSight = false;
    
    private int waypointCounter = 0;

    private bool chargePlayer = false;

    private bool collidedPlayer = false;

    public Vector3 RespawnPoint;
    private PhotonView view;
    private float yeetForce = 100;
    private bool canCharge = true;

    // Start is called before the first frame update
    void Start()
    {
        RespawnPoint = transform.position;
        mesh = GetComponent<MeshRenderer>();
        view = gameObject.GetPhotonView();
        InitPlayer();

        mesh.material = (enemyColor == EnemiesColors.Bleu) ? matBleu : matVert;
    }
    
    private void InitPlayer()
    {
        playerOne = GameObject.FindGameObjectWithTag("Player1");
        playerTwo = GameObject.FindGameObjectWithTag("Player2");
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerOne == null || playerTwo == null)
        {
            InitPlayer();
        }
        // Si le joueur est dans le range du trigger, on regarde si on le voit ou si un objet est dans le champ de vision
        if (player1InRange)
        {
            player1LineOfSight = CheckForLineOfSight(playerOne);
        }

        if (player2InRange)
        {
            player2LineOfSight = CheckForLineOfSight(playerTwo); 
        }
        
        // Fonction qui compare les distances des 2 joueurs par rapport au monstre
        if ((player1InRange || player2InRange) /*&& !collidedPlayer*/)
        {
            CheckForChargeOpportunity();
        }
    }

    private void FixedUpdate()
    {
        if (chargePlayer && canCharge/*!collidedPlayer*/)
        {
            ChangeOwner();
            if (enemyColor == EnemiesColors.Bleu)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerOne.transform.position, chargeSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTwo.transform.position, chargeSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Cycle de marche
            if (pathWaypoints.Length > 0 && !chargePlayer)
            {
                Vector3 target = new Vector3(pathWaypoints[waypointCounter].transform.position.x, transform.position.y,
                    pathWaypoints[waypointCounter].transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
            
                if (Vector3.Distance(transform.position, target) < 0.1f)
                {
                    if (elapsedTime >= waitTimeOnWaypoint)
                    {
                        elapsedTime = 0;
                        waypointCounter++;
                        if (waypointCounter > pathWaypoints.Length - 1)
                        {
                            waypointCounter = 0;
                        }
                    }
                    else
                    {
                        elapsedTime += Time.deltaTime;
                    }
            
                }
            }
        }
    }
    
    private void CheckForChargeOpportunity()
    {
        if (enemyColor == EnemiesColors.Bleu)
        {
            if ((player1LineOfSight && !player2LineOfSight) || (player1LineOfSight && player2LineOfSight
                                                                                   && Vector3.Distance(transform.position,
                                                                                       playerOne.transform.position) <
                                                                                   Vector3.Distance(transform.position,
                                                                                       playerTwo.transform.position)))
            {
                chargePlayer = true;
            }
            else
            {
                chargePlayer = false;
            }
        }
        else
        {
            if ((player2LineOfSight && !player1LineOfSight) || (player2LineOfSight && player1LineOfSight
                                                                                   && Vector3.Distance(transform.position,
                                                                                       playerTwo.transform.position) <
                                                                                   Vector3.Distance(transform.position,
                                                                                       playerOne.transform.position)))
            {
                chargePlayer = true;
            }
            else
            {
                chargePlayer = false;
            }
        }
    }

    private bool CheckForLineOfSight(GameObject pPlayer)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (pPlayer.transform.position - transform.position),
            out hit, Vector3.Distance(pPlayer.transform.position, transform.position) * 100))
        {
            if (!hit.collider.CompareTag(pPlayer.tag))
            {
                return false;
            }
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            player1InRange = true;
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            player2InRange = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            player1InRange = false;
            player1LineOfSight = false;
            chargePlayer = false;
            collidedPlayer = false;
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            player2InRange = false;
            player2LineOfSight = false;
            chargePlayer = false;
            collidedPlayer = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player1") && enemyColor == EnemiesColors.Bleu && canCharge)
        {
            Vector3 closestPoint = CalculateClosestPointToVoid(other.gameObject.transform.position);
            Vector3 direction = closestPoint - transform.position;
            Vector3 force = direction * yeetForce;
            
            other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, transform.position);
            
            chargePlayer = false;
            collidedPlayer = true;
            StartCoroutine(CannotBeCharge());
        }
        else if (other.gameObject.CompareTag("Player2") && enemyColor == EnemiesColors.Vert)
        {
            Vector3 closestPoint = CalculateClosestPointToVoid(other.gameObject.transform.position);
            Vector3 direction = closestPoint - transform.position;
            Vector3 force = direction * yeetForce;
            
            other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, transform.position);
            
            chargePlayer = false;
            collidedPlayer = true;
            StartCoroutine(CannotBeCharge());
        }
    }

    private IEnumerator CannotBeCharge()
    {
        canCharge = false;
        yield return new WaitForSeconds(5f);
        canCharge = true;
    }

    private Vector3 CalculateClosestPointToVoid(Vector3 playerPosition)
    {
        float closestPoint = Mathf.Infinity;
        Vector3 closestPosition = Vector3.zero;
        foreach (Transform pointTransform in positionToYeet)
        {
            float distance = Vector3.Distance(transform.position, pointTransform.position);
            if (distance >= Vector3.Distance(playerPosition, pointTransform.position))
            {
                if (distance < closestPoint)
                {
                    closestPosition = pointTransform.position;
                    closestPoint = distance;
                }
            }
        }

        return closestPosition;
    }

    private void ChangeOwner()
    {
        
        if (view.ownerId != PhotonNetwork.player.ID)
        {
            view.TransferOwnership(PhotonNetwork.player.ID);
        }
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(player1InRange);
            stream.SendNext(player2InRange);
            stream.SendNext(chargePlayer);
            stream.SendNext(collidedPlayer);
        } else if (stream.isReading)
        {
            player1InRange = (bool) stream.ReceiveNext();
            player2InRange = (bool) stream.ReceiveNext();
            chargePlayer = (bool) stream.ReceiveNext();
            collidedPlayer = (bool) stream.ReceiveNext();
        }
    }
}
