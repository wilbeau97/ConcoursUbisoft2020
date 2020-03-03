using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAura : MonoBehaviour
{
    public float elapsedTime = 0.0f;
    
    private Material mat;

    private bool startedFlashing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Material>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 2)
        {
            startedFlashing = true;
        }
    }
    
    public void StartFlashing()
    {
        if (!startedFlashing)
        {
            startedFlashing = true;
            StartCoroutine("StartFlashObject");
        }
    }

    public void StopFlashing()
    {
        startedFlashing = false;
        StopCoroutine("StartFlashObject");
        
    }

    private IEnumerator StartFlashObject()
    {
        while (startedFlashing)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.75f));
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Random.Range(0.0f, 0.150f) * 100);
        }
    }
}
