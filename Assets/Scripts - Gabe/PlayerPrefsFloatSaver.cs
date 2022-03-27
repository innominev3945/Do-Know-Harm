using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsFloatSaver : MonoBehaviour
{
    // credit to Hellium from https://answers.unity.com/questions/1872847/audio-slider-for-multiple-scenes.html

    [SerializeField] private string key;

    public void SetFloat(float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
}
