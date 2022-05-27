using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsFloatSaver : MonoBehaviour
{
    // credit to Hellium from https://answers.unity.com/questions/1872847/audio-slider-for-multiple-scenes.html

    [SerializeField] private string key1 = "musicVol";
    [SerializeField] private string key2 = "sfxVol";
    [SerializeField] private string key3 = "clickVol";
    [SerializeField] private string key4 = "ambiVol";
    [SerializeField] private string key5 = "txtSpeed";


    public void SetFloatMusic(float value)
    {
        PlayerPrefs.SetFloat(key1, value);
    }
    public void SetFloatSFX(float value)
    {
        PlayerPrefs.SetFloat(key2, value);
    }
    public void SetFloatClick(float value)
    {
        PlayerPrefs.SetFloat(key3, value);
    }
    public void SetFloatAmbi(float value)
    {
        PlayerPrefs.SetFloat(key3, value);
    }
    public void SetFloatTextSpd(float value)
    {
        PlayerPrefs.SetFloat(key3, value);
    }
}
