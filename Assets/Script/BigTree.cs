using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTree : MonoBehaviour
{
    private Transform tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = GetComponent<Transform>();
    }

    public void Grow()
    {
        //changer 1000 par valeur quelconque
        tree.localScale += new Vector3(0, 1000, 0);
        tree.position += new Vector3(0, 1000 / 2, 0);
    }
}
