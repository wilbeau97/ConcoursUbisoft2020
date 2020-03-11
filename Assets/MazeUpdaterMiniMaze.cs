using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUpdaterMiniMaze : MonoBehaviour
{
    private MiniMaze[] miniMazes;
    public GameObject redCube;
    public GameObject blueCube;
    public GameObject greenCube;
    private bool red = false;
    private bool blue = false;
    private bool green = false;
    // Start is called before the first frame update
    void Start()
    {
        miniMazes = GetComponentsInChildren<MiniMaze>();
    }

    // Update is called once per frame
    void Update()
    {
        if (redCube.activeSelf)
        {
            foreach (MiniMaze miniMaze in miniMazes)
            {
                miniMaze.redCube.SetActive(true);
            }
        }

        if (blueCube.activeSelf)
        {
            foreach (MiniMaze miniMaze in miniMazes)
            {
                miniMaze.blueCube.SetActive(true);
            }
        }

        if (greenCube.activeSelf)
        {
            foreach (MiniMaze miniMaze in miniMazes)
            {
                miniMaze.greenCube.SetActive(true);
            }
        }
    }
}
