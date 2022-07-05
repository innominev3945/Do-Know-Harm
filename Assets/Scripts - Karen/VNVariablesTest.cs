using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class VNVariablesTest : MonoBehaviour
{
    [UnityEngine.Serialization.FormerlySerializedAs("variableStorage")]
    [SerializeField] internal VariableStorageBehaviour _variableStorage;

        /// <inheritdoc cref="_variableStorage"/>
    public VariableStorageBehaviour VariableStorage
    {
        get => _variableStorage; 
        set
        {
            _variableStorage = value;
            /*if (_dialogue != null)
            {
                _dialogue.VariableStorage = value;
            }*/
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Sets value of KarenWantsC6Xiao to 7
        VariableStorage.SetValue("$KarenWantsc6Xiao", 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
