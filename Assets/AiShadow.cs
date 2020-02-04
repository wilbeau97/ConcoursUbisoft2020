using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiShadow : MonoBehaviour
{
    public GameObject shadow;
    public int i = 10;
    private Bounds area;
    public int vitesse = 1;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        area = new Bounds(gameObject.transform.position, new Vector3(i,i,i));
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player1");
        }
        if (area.Contains(target.transform.position))
        {
            Debug.DrawRay(gameObject.transform.position,gameObject.transform.position - target.transform.position, Color.black);
        }
    }
}
