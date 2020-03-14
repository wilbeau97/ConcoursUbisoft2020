using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;

public class impactSound : MonoBehaviour
{
    private Collider colliderObjet;
    // Start is called before the first frame update
    void Start()
    {
        colliderObjet = this.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dirtGround"))
        {
            AudioManager.Instance.Play("dropTerre");
        }
    }
    // Update is called once per frame
    
}
