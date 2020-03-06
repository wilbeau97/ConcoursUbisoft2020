using System;
using System.Collections;
using System.Collections.Generic;
using Script.UI;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StopAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void TriggerAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void OnTriggerEnter(Collider other)
    {
        //UiScript.Instance.UpdateLife();
        //UiScript.Instance.UpdateEnergy();
    }
}
