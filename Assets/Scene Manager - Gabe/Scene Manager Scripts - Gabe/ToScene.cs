using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    // Attach this to a button (or some input), set the scene in the inspector, and set the onclick attribute appropriately to send the game to the desired scene

    [SerializeField] private string scene;
    [SerializeField] private GameObject curr_scene_obj;


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
        int a = curr_scene_obj.GetComponent<VNSceneNumbers>().getCurrentScene();
        if (a == 0)
        {
            curr_scene_obj.GetComponent<VNSceneNumbers>().setCurrentScene(curr_scene_obj.GetComponent<VNSceneNumbers>().getCurrentScene() + 1);
            LoadSceneByName("GPScene1");
        }
        else if (a == 3)
        {
            curr_scene_obj.GetComponent<VNSceneNumbers>().setCurrentScene(curr_scene_obj.GetComponent<VNSceneNumbers>().getCurrentScene() + 1);
            LoadSceneByName("GPScene2");
        }
        else if (a == 1)
        {
            curr_scene_obj.GetComponent<VNSceneNumbers>().setCurrentScene(curr_scene_obj.GetComponent<VNSceneNumbers>().getCurrentScene() + 1);
            LoadSceneByName("Credits Scene");
        }
        else if (a == 6)
        {
            curr_scene_obj.GetComponent<VNSceneNumbers>().setCurrentScene(curr_scene_obj.GetComponent<VNSceneNumbers>().getCurrentScene() + 1);
            LoadSceneByName("MainMenu");
        }
        else
        {
            curr_scene_obj.GetComponent<VNSceneNumbers>().setCurrentScene(curr_scene_obj.GetComponent<VNSceneNumbers>().getCurrentScene() + 1);
            LoadSceneByName("VN - Yarn no journal");
        }
    }
}