using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    public bool isSpawn;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        isSpawn = true;
        gameObject.SetActive(true);
        StartCoroutine(Disapear());
    }

    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
        isSpawn = false;
    }
}
