using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HideObjects : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    private RaycastHit[] hits = null;
    [SerializeField] private LayerMask mask;

    private void Start()
    {
        mask = ~mask;
    }

    // Update is called once per frame
    void Update()
    {
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                Renderer r = hit.collider.GetComponent<Renderer>();
                if (r)
                {
                    r.shadowCastingMode = ShadowCastingMode.On;
                }
            }
        }
        
        Vector3 playerPosition = player.position + Vector3.up;
        hits = Physics.RaycastAll(transform.position,
            (playerPosition - transform.position),
            Vector3.Distance(transform.position, playerPosition), mask);

        foreach (RaycastHit hit in hits)
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer)
            {
                renderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }
    }
}
