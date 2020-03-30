using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        StartCoroutine(Disapear());
    }

    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
