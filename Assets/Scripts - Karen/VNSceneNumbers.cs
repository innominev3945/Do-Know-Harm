using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNSceneNumbers : MonoBehaviour
{
    public static int curr_scene = 0;

    public void setCurrentScene(int n)
    {
        curr_scene = n;
    }

    public int getCurrentScene()
    {
        return curr_scene;
    }

    private void Start()
    {
        Debug.Log(curr_scene);
    }

    public void IncrementCurrentScene()
    {
        curr_scene++;
    }
}
