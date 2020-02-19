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

        Debug.DrawRay(this.transform.position, (player.position - this.transform.position),
            Color.magenta);

        hits = Physics.RaycastAll(this.transform.position,
            (player.position - this.transform.position),
            Vector3.Distance(this.transform.position, player.position), mask);

        foreach (RaycastHit hit in hits)
        {
            Renderer r = hit.collider.GetComponent<Renderer>();
            if (r)
            {
                r.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }
    }
}
