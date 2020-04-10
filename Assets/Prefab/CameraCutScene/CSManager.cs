using System.Collections;
using System.Collections.Generic;
using Prefab.CameraCutScene;
using UnityEngine;

public class CSManager : MonoBehaviour
{
    private List<GameObject> cameras = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            cameras.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
