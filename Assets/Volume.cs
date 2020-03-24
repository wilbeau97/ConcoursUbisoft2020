using System.Collections;
using System.Collections.Generic;
using AuraAPI;
using UnityEngine;

public class Volume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AuraVolume>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
