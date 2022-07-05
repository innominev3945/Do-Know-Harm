using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip heartBeatSound1, heartBeatSound2, heartBeatSound3, deadSound, winSound;
    static AudioSource audioSrc;
    //public Toggle myToggle;
    // Start is called before the first frame update
    void Start()
    {
        heartBeatSound1 = Resources.Load<AudioClip>("heartbeat1");
        heartBeatSound2 = Resources.Load<AudioClip>("heartbeat2");
        heartBeatSound3 = Resources.Load<AudioClip>("heartbeat3");
        deadSound = Resources.Load<AudioClip>("dieded");
        winSound = Resources.Load<AudioClip>("notdieded");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "heartbeat1":
                audioSrc.Stop();
                audioSrc.PlayOneShot(heartBeatSound1);
                audioSrc.loop = true;
                //loop is not working!!
                break;
            case "heartbeat2":
                audioSrc.Stop();
                audioSrc.PlayOneShot(heartBeatSound2);
                audioSrc.loop = true;
                break;
            case "heartbeat3":
                audioSrc.Stop();
                audioSrc.PlayOneShot(heartBeatSound3);
                audioSrc.loop = true;
                break;
            case "death":
                audioSrc.Stop();
                audioSrc.PlayOneShot(deadSound);
                break;
            case "win":
                audioSrc.Stop();
                audioSrc.PlayOneShot(winSound);
                break;
        }
    }
}
