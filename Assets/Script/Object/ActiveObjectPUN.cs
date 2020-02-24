using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectPUN : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject;
    [PunRPC]
    public void SetActive()
    {
        targetGameObject.SetActive(true);
    }
}
