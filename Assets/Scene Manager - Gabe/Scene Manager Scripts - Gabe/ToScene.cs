using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    // Attach this to a button (or some input), set the scene in the inspector, and set the onclick attribute appropriately to send the game to the desired scene

    [SerializeField] private string scene;


    public void LoadSelectedScene() // for the serializefield version
    {
        Debug.Log("Sending to " + scene);
        Loader.Load(scene);
    }

    public void LoadSceneByName(string name) // for call by other scripts
    {
        Debug.Log("Sending to " + name);
        Loader.Load(name);
    }

    public void LoadSceneDepends()
    {

    }
}