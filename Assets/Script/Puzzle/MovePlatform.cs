using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // source : https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;
    public bool isMoving = false;
    public bool isActivated = false;
    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    

    // Move to the target end position.
    void Update()
    {
        if (isActivated && !isMoving)
        {
            // Keep a note of the time the movement started.
            startTime = Time.time;
            // Calculate the journey length.
            journeyLength = Vector3.Distance(transform.position, endMarker.position);
            isMoving = true;
        }

        if (isMoving)
        {
            MovePlatformForward();
        }
        
    }

    private void MovePlatformForward()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(transform.position, endMarker.position, fractionOfJourney);
    }

    private void MovePlatformBackward()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fractionOfJourney);
    }

    [PunRPC]
    public void ActivatePlatform()
    {
        isActivated = true;
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.transform.SetParent(gameObject.transform);
        }
    }
        
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
    
}
