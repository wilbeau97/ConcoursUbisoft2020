using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1ManagerVariant : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private Transform playerToRespawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.gameObject.GetComponentInChildren<PlayerHUD>().FadeOut();
            playerToRespawn = other.transform;
            StartCoroutine("WaitForAnimation");
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerToRespawn.position = respawnPoint.position;
        playerToRespawn.gameObject.GetComponentInChildren<PlayerHUD>().FadeIn();
    }
}
