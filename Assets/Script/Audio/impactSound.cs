using System;
using System.Collections;
using System.Collections.Generic;
using Script.Audio;
using UnityEngine;
using UnityEngine.Audio;

public class impactSound : MonoBehaviour
{
    private Collider colliderObjet;
    // Start is called before the first frame update
    void Start()
    {
        colliderObjet = this.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // passe le transform de l'objet actuel pour indiquer la position d'où le son doit venir 
            AudioManager.Instance.Play("dropTerre", transform);
        }
    }


    // Update is called once per frame
    
}
