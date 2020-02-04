using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testswitchscene : MonoBehaviour
{
    public int tick = 0;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("video", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        tick++;
        if (tick == 60*10)
        {
            SceneManager.UnloadSceneAsync("video", UnloadSceneOptions.None);
        }
    }
}
