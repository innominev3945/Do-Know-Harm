using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] BGM;
    public AudioClip[] SFX;

    [SerializeField] AudioSource BGM_Source;

    [SerializeField] AudioSource SFX_Source;

    //FAAAAAUUUUSSSSSTIIIIIIIINEEEEEEEEEEEEEEEEEEEEEEEEE HEEEEEEEERRRRRRREEEEEEEEEEEEEEEEEE
    public float BGM_Volume;// to change background music volume
    public float SFX_Volume;// to change sound effects volume
    // Start is called before the first frame update
    void Awake()
    {
        BGM_Volume = 1;
        SFX_Volume = 2;
    }

    void Update()
    {
        BGM_Source.volume = BGM_Volume;
        SFX_Source.volume = SFX_Volume;
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
}
