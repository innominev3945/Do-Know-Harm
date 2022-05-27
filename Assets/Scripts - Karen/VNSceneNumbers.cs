using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNSceneNumbers : MonoBehaviour
{
    public static int curr_scene = 1;

    public void setCurrentScene(int n)
    {
        curr_scene = n;
    }

    public int getCurrentScene()
    {
        return curr_scene;
    }
}
