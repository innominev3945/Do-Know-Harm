using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class PlayerPrefsFloatLoader : MonoBehaviour
{
    // credit to Hellium from https://answers.unity.com/questions/1872847/audio-slider-for-multiple-scenes.html
    // see AudioManager.cs from karen's scripts for vn interface stuff

    [SerializeField] private string key1 = "musicVol";
    [SerializeField] private string key2 = "sfxVol";
    [SerializeField] private string key3 = "clickVol";
    [SerializeField] private float defaultValue = 0;
    [SerializeField] private FloatEvent onValueLoadedMusic;
    [SerializeField] private FloatEvent onValueLoadedSFX;
    [SerializeField] private FloatEvent onValueLoadedClick;

    private void Awake()
    {
        onValueLoadedMusic.Invoke(PlayerPrefs.GetFloat(key1, defaultValue));
        onValueLoadedSFX.Invoke(PlayerPrefs.GetFloat(key2, defaultValue));
        onValueLoadedClick.Invoke(PlayerPrefs.GetFloat(key3, defaultValue));
    }
}
