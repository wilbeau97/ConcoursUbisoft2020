using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
using UnityEngine;

public class FallOffMapTrigger : MonoBehaviour
{
    private Transform playerToRespawn;
    private Transform terrainY;
    
    // Start is called before the first frame update
    void Start()
    {
        terrainY = GameObject.Find("Terrain").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            other.gameObject.GetComponentInChildren<PlayerHUD>().FadeOut();
            playerToRespawn = other.transform;
            StartCoroutine("WaitForAnimation");
        }
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1f);
        playerToRespawn.gameObject.GetComponentInChildren<PlayerHUD>().FadeIn();
        playerToRespawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        playerToRespawn.position = new Vector3(playerToRespawn.position.x, terrainY.position.y, 
            playerToRespawn.transform.position.z);
        
    }
}
