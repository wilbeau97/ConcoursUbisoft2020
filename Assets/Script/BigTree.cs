using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BigTree : MonoBehaviour
{
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;
    private Animator animator;
    private int growSize = 20;
    private int nbTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = nightSkybox;
        animator = GetComponent<Animator>();
    }

    public void Grow()
    {
        animator.SetTrigger("Grow" + nbTime);
        nbTime++;
    }

    public void ChangeSkybox()
    {
        RenderSettings.skybox = daySkybox;
    }
}
