using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Demos.DemoAnimator;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
                AudioManager.Instance.Play("dropTerre", transform);
        }
        if (other.CompareTag("grass"))
        {
            AudioManager.Instance.Play("dropGazon", transform);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Jumpable"))
        {
            // passe le transform de l'objet actuel pour indiquer la position d'où le son doit venir 
            AudioManager.Instance.Play("dropTerre", transform);
        }
        if (other.gameObject.CompareTag("grass") || other.gameObject.CompareTag("Jumpable"))
        {
            // passe le transform de l'objet actuel pour indiquer la position d'où le son doit venir 
            AudioManager.Instance.Play("dropGazon", transform);
        }
    }


}
