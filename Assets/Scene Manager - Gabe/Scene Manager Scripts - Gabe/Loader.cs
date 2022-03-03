using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public static class Loader
{
    public enum Scene // scene names can be entered here OR use the ToScene script and place the desired scene in the inspector fields
    {
        GPdummy,
        LoadingScene,
        MainMenu,
        VNDummy,
    }

    private static Action onLoaderCallback;

    public static void Load(Scene scene)
    {
        // Set the loader callback action to lead the target scene
        onLoaderCallback = () =>
        {
            UnitySceneManager.LoadScene(scene.ToString());
        };

        // Load the loading scene
        UnitySceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void Load(UnityEngine.SceneManagement.Scene scene)
    {
        // Set the loader callback action to lead the target scene
        onLoaderCallback = () =>
        {
            UnitySceneManager.LoadScene(scene.name);
        };

        // Load the loading scene
        UnitySceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback()
    {
        // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
