using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
    
    [SerializeField] private Transform player;
    private RaycastHit[] hits = null;
    // Start is called before the first frame update
    void Start()
    {
        
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
                    r.enabled = true;
                }
            }
        }

        Debug.DrawRay(this.transform.position, (player.position - this.transform.position),
            Color.magenta);

        hits = Physics.RaycastAll(this.transform.position,
            (player.position - this.transform.position),
            Vector3.Distance(this.transform.position, player.position));

        foreach (RaycastHit hit in hits)
        {
            Renderer r = hit.collider.GetComponent<Renderer>();
            if (r)
            {
                r.enabled = false;
            }
        }
    }
}
