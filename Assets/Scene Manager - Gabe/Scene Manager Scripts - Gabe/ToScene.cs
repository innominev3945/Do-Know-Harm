using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    // Attach this to a button (or some input), set the scene in the inspector, and set the onclick attribute appropriately to send the game to the desired scene

    [SerializeField] private string scene;

    public void LoadSelectedScene()
    {
        Debug.Log("Sending to " + scene);
        Loader.Load(scene);
    }
}