using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public AudioSource click;
    public AudioSource injury_clear;

    public void SFXplayClick()
    {
        click.Play();
    }
    public void SFXinjuryClear()
    {
        injury_clear.Play();   
    }
}
