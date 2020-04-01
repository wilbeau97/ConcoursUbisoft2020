using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BigTree : MonoBehaviour
{
    private Animator animator;
    private int growSize = 20;
    private int nbTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Grow()
    {
        animator.SetTrigger("Grow" + nbTime);
        nbTime++;
    }

    public void ChangeSkybox()
    {
        Debug.Log("Test");
    }
}
