using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTree : MonoBehaviour
{
    private Transform tree;

    private int growSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        tree = GetComponent<Transform>();
    }

    public void Grow()
    {
        //changer 1000 par valeur quelconque
        growSize = 1000;
        tree.localScale += new Vector3(0, growSize, 0);
        tree.position += new Vector3(0, growSize / 2, 0);
    }
}
