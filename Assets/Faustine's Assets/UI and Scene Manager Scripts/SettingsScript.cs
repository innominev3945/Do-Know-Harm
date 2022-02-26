using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    float textSpeed = 0;
    float volume = 0;

    public void TextSpeed(float speedVal) //set text speed with slider (dynamic float)
    {
        textSpeed = speedVal;
    }

    public void Volume(float volumeVal) //set volume with slider
    {
        volume = volumeVal;
    }
}
