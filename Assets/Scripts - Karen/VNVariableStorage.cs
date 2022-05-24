using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System.IO;

public class VNVariableStorage : MonoBehaviour
{
    string filepath { get { return Application.persistentDataPath + Path.DirectorySeparatorChar + "playerSave.json" ;} }


    [UnityEngine.Serialization.FormerlySerializedAs("variableStorage")]
    [SerializeField] internal VariableStorageBehaviour _variableStorage;
    [SerializeField] internal jsonSaver saver;

        /// <inheritdoc cref="_variableStorage"/>
    public VariableStorageBehaviour VariableStorage
    {
        get => _variableStorage; 
        set
        {
            _variableStorage = value;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(!File.Exists(filepath))
        {
            FileStream file = File.Create(filepath);
            file.Close();
            //setNumberValue("$KarenWantsC6Xiao", 7);
            saver.SaveToFile(filepath);
        }
        else
        {
            saver.LoadFromFile(filepath);
        }

        //Load data from the save data
        //saver.LoadFromFile(filepath);
        //Sets value of KarenWantsC6Xiao to 7
    }


    //!!!!!!!!!!!!!!!!!!ATTENTION!!!!!!!!!!!!!!!!!!!!!!!!!
    //          All variableName s MUST start with $
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public void setNumberValue(string variableName, float number)
    {
        saver.SetValue(variableName, number);
    }

    public void setStringValue(string variableName, string value)
    {
        saver.SetValue(variableName, value);
    }

    public void setBoolValue(string variableName, bool value)
    {
        saver.SetValue(variableName, value);
    }


    //USE WHEN PLAYERS SAVE FILE
    public void Save()
    {
        saver.SaveToFile(filepath);
    }

    public void Load()
    {
        saver.LoadFromFile(filepath);
    }

}
