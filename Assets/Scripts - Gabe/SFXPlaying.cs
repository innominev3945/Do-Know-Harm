using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public AudioSource click;

    public void SFXplayClick()
    {
        click.Play();
    }
}
