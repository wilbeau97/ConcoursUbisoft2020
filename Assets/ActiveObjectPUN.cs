using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectPUN : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [PunRPC]
    public void SetActive(Boolean value)
    {
        targetGameObject.SetActive(true);
    }
}
