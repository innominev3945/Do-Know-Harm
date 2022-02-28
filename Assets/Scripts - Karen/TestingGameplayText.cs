using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TestingGameplayText : MonoBehaviour
{
    DialogueManagerForGameplay uut;

    void Start()
    {
        uut = GetComponent<DialogueManagerForGameplay>();
        
    }

    void OnClick(InputValue value)
    {
        //END OF SCRIPT
        
        if (value.isPressed)
        {
            uut.playLine();
        }

    }
}
