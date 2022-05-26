using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour
{
    // see: https://youtu.be/J6FfcJpbPXE

    public static GameControl control;
    public int savedIndex;
    public int savedChapter; // add more variables if necessary

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }  
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.index = savedIndex;
        data.chapter = savedChapter;


        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Saved: " + savedIndex + " " + savedChapter);
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            savedIndex = data.index;
            savedChapter = data.chapter;
            Debug.Log("Loaded: " + savedIndex + " " + savedChapter);
        }
    }

    [Serializable]
    class PlayerData // store necessary information here
    {
        public int index;
        public int chapter;
    }
}
