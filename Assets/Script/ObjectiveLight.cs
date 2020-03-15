using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveLight : MonoBehaviour
{
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ActivateLight();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeactivateLight();
        }
    }

    public void ActivateLight()
    {
        light.range = 100;
    }
    
    public void DeactivateLight()
    {
        light.range = 1;
    }
}
