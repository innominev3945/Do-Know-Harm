using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

public class PlayerPrefsFloatLoader : MonoBehaviour
{
    // credit to Hellium from https://answers.unity.com/questions/1872847/audio-slider-for-multiple-scenes.html

    [SerializeField] private string key;
    [SerializeField] private float defaultValue = 0;
    [SerializeField] private FloatEvent onValueLoaded;

    private void Awake()
    {
        onValueLoaded.Invoke(PlayerPrefs.GetFloat(key, defaultValue));
    }
}
