using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimationEvent : MonoBehaviour
{
    public void playStepSound()
    {
        SoundsManager.instance.RandomizeSfx(); // joue les sons de pas aléatoirement 
    }

    public void SpawnFootPrint() 
    {
            
    }
}
