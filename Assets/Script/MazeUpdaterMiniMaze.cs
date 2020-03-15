using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeUpdaterMiniMaze : MonoBehaviour
{
    private MiniMaze[] miniMazes;
    public GameObject redCube;
    public GameObject blueCube;
    public GameObject greenCube;
    private bool red = true;
    private bool blue = true;
    private bool green = true;
    // Start is called before the first frame update
    void Start()
    {
        miniMazes = GetComponentsInChildren<MiniMaze>();
    }

    // Update is called once per frame
    void Update()
    {
        if (redCube.activeSelf && red)
        {
            foreach (MiniMaze miniMaze in miniMazes)
            {
                miniMaze.redCube.SetActive(true);
                red = false;
            }
        }

        if (blueCube.activeSelf && blue)
        {
            foreach (MiniMaze miniMaze in miniMazes)
            {
                miniMaze.blueCube.SetActive(true);
                blue = false;
            }
        }

        if (greenCube.activeSelf && green)
        {
            foreach (MiniMaze miniMaze in miniMazes)
            {
                miniMaze.greenCube.SetActive(true);
                green = false;
            }
        }
    }
}
