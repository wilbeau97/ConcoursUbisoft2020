using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Inspiré/basé sur : https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    // Movement speed in units per second.
    public float speed = 1.0F;
    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;
    private bool isGoingForward = false;
    private bool isGoingBackward = false;

    [PunRPC]
    public void MovePlatformForward()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        //Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, endMarker.position);
        isGoingBackward = false;
        isGoingForward = true;
        StartCoroutine(MovePlatformForwardCoroutine());
    }
    public IEnumerator MovePlatformForwardCoroutine()
    {
        while (Vector3.Distance(transform.position, endMarker.position) > 0.1f && isGoingForward)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, endMarker.position, fractionOfJourney);
            yield return null;
        }
    }


    [PunRPC]
    public void MovePlatformBackward()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        //Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, startMarker.position);
        isGoingBackward = true;
        isGoingForward = false;
        StopCoroutine(MovePlatformForwardCoroutine());
        StartCoroutine(movePlatformBackwardCoroutine());
    }

    public IEnumerator movePlatformBackwardCoroutine()
    {
        while (Vector3.Distance(transform.position, startMarker.position) > 0.1f && isGoingBackward)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(transform.position, startMarker.position, fractionOfJourney);
            yield return null;
        }
    }
    
    
    public void OnCollisionEnter(Collision other) // s'assure que le personnage stutter pas avec le déplacement
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.transform.SetParent(gameObject.transform);
        }
    }
    
    public void OnCollisionExit(Collision other)// s'assure que le personnage stutter pas avec le déplacement
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
    
}
