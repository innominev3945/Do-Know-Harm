using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] BGM;
    public AudioClip[] SFX;

    public AudioClip click;

    [SerializeField] AudioSource BGM_Source;

    [SerializeField] AudioSource SFX_Source;

    [SerializeField] AudioSource Click_Sound;

    //********************************************************************************************************
    //              FAAAAAUUUUSSSSSTIIIIIIIINEEEEEEEEEEEEEEEEEEEEEEEEE HEEEEEEEERRRRRRREEEEEEEEEEEEEEEEEE
    //              3 Sound adjustment parameters
    //*********************************************************************************************************
    public float BGM_Volume;// to change background music volume
    public float SFX_Volume;// to change sound effects volume
    public float Click_Volume; // change click sound

    void Awake()
    {
        Click_Sound.clip = click;
    }
    void Update()
    {
        BGM_Source.volume = BGM_Volume;
        SFX_Source.volume = SFX_Volume;
        Click_Sound.volume = Click_Volume;
        BGM_Source.loop = true;
    }

    // Update is called once per frame
    public void PlayBGM(int n)
    {
        BGM_Source.clip = BGM[n];
        BGM_Source.Play();
        
    }

    public void PlaySFX(int n)
    {
        SFX_Source.clip = SFX[n];
        SFX_Source.Play();
    }

    public void PlayClick()
    {
        Click_Sound.Play();
    }
}
