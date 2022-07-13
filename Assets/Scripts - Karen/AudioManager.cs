using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] BGM;
    public AudioClip[] SFX;
    public AudioClip[] Ambience;
    public AudioClip click;

    public AudioClip[] Chapter1_T_1VO;

    private AudioClip[] chapterVO;

    [SerializeField] AudioSource BGM_Source;

    [SerializeField] AudioSource Ambience_Source;

    [SerializeField] AudioSource SFX_Source;

    [SerializeField] AudioSource Click_Sound;

    [SerializeField] AudioSource Voice_Line;

    public Slider BGM_volume;

    public Slider Ambience_volume;

    public Slider SFX_volume;

    public Slider Click_volume;

    //public Slider Voice_volume;

    public void Start()
    {
        BGM_volume.onValueChanged.AddListener (delegate {ValueChangeCheckBGM ();});
        Ambience_volume.onValueChanged.AddListener (delegate {ValueChangeCheckAmbience ();});
        SFX_volume.onValueChanged.AddListener (delegate {ValueChangeCheckSFX ();});
        Click_volume.onValueChanged.AddListener (delegate {ValueChangeCheckClick ();});
    }

    public void ValueChangeCheckBGM()
    {
        BGM_Volume = BGM_volume.value;
        BGM_Source.volume = BGM_Volume;
        
    }
    public void ValueChangeCheckAmbience()
    {
        Ambience_Volume = Ambience_volume.value;
        Ambience_Source.volume = BGM_Volume;
    }
    public void ValueChangeCheckSFX()
    {
        SFX_Volume = SFX_volume.value;
        SFX_Source.volume = SFX_Volume;
        Voice_Line.volume = SFX_Volume;
    }
    public void ValueChangeCheckClick()
    {
        Click_Volume = Click_volume.value;
        Click_Sound.volume = Click_Volume;
        
    }

    //********************************************************************************************************
    //              FAAAAAUUUUSSSSSTIIIIIIIINEEEEEEEEEEEEEEEEEEEEEEEEE HEEEEEEEERRRRRRREEEEEEEEEEEEEEEEEE
    //              3 Sound adjustment parameters
    //*********************************************************************************************************
    private static float BGM_Volume = 0.5f;// to change background music volume
    private static float SFX_Volume = 0.5f;// to change sound effects volume
    private static float Click_Volume = 0.5f; // change click sound
    private static float Ambience_Volume = 0.5f;

    void Awake()
    {
        Click_Sound.clip = click;
        BGM_Source.loop = true;
        Ambience_Source.loop = true;
    }
    // void Update()
    // {
    //     BGM_Source.volume = BGM_Volume;
    //     SFX_Source.volume = SFX_Volume;
    //     Click_Sound.volume = Click_Volume;
    //     Ambience_Source.volume = BGM_Volume;        
    // }

    // Update is called once per frame
    public void PlayBGM(int n)
    {
        if (n < 0)
        {
            BGM_Source.Stop();
            return;
        }
        BGM_Source.clip = BGM[n];
        BGM_Source.Play();
        
    }

    public void PlayAmbience(int n)
    {
        if (n < 0)
        {
            Ambience_Source.Stop();
            return;
        }
        Ambience_Source.clip = Ambience[n];
        Ambience_Source.Play();
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

    public void muteAll()
    {
        BGM_Source.volume = 0;
        SFX_Source.volume = 0;
        Click_Sound.volume = 0;
        Ambience_Source.volume = 0;
        Voice_Line.volume = 0;  
    }

    public void unmuteAll()
    {
        BGM_Source.volume = BGM_Volume;
        SFX_Source.volume = SFX_Volume;
        Click_Sound.volume = Click_Volume;
        Ambience_Source.volume = BGM_Volume;
        Voice_Line.volume = SFX_Volume;
    }

    public void playVoice(int voice_line_num)
    {
        Debug.Log("Attempting to play clip " + voice_line_num);
        Voice_Line.clip = chapterVO[voice_line_num];
        Voice_Line.Play();
    }

    public void setChapter(int chapterNum)
    {
        Debug.Log("Attempted to set chapter");
        if (chapterNum == 0)
        {
            chapterVO = Chapter1_T_1VO;
        }
        else
        {
            chapterVO = Chapter1_T_1VO;
        }
    }
}
