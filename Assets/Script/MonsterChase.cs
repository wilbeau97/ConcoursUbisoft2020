using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public enum EnemiesColors
    {
        Bleu,
        Vert
    };

    public EnemiesColors enemyColor = EnemiesColors.Bleu;

    public Material matBleu;

    public Material matVert;

    public Transform[] pathWaypoints;

    public int waitTimeOnWaypoint = 0;
    
    public float chargeSpeed = 10.0f;

    public float walkSpeed = 5.0f;

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

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();

        playerOne = GameObject.Find("Player1Test (1)");
        playerTwo = GameObject.Find("Player2Test (1)");

        mesh.material = (enemyColor == EnemiesColors.Bleu) ? matBleu : matVert;
    }

    // Update is called once per frame
    void Update()
    {
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
        CheckForChargeOpportunity();
    }

    private void FixedUpdate()
    {
        if (chargePlayer && !collidedPlayer)
        {
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
            if (pathWaypoints.Length > 0)
            {
                Vector3 target = new Vector3(pathWaypoints[waypointCounter].transform.position.x, transform.position.y,
                    pathWaypoints[waypointCounter].transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, target, walkSpeed * Time.deltaTime);
            
                if (transform.position == target)
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
        Debug.DrawRay(transform.position,(pPlayer.transform.position - transform.position));
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
            collidedPlayer = false;
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            player2InRange = false;
            player2LineOfSight = false;
            collidedPlayer = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player1") && enemyColor == EnemiesColors.Bleu)
        {
            Vector3 direction = other.transform.position - transform.position;
            Vector3 force = new Vector3(direction.x * 500, 1000, direction.z * 500);
            other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, transform.position);
            collidedPlayer = true;
        }
        else if (other.gameObject.CompareTag("Player2") && enemyColor == EnemiesColors.Vert)
        {
            Vector3 direction = other.transform.position - transform.position;
            Vector3 force = new Vector3(direction.x * 500, 1000, direction.z * 500);
            other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(force, transform.position);
            chargePlayer = false;
            collidedPlayer = true;
        }
    }
}
