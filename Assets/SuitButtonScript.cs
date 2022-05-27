using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SuitButtonScript : MonoBehaviour
{
    private bool undone;
    void Start()
    {
        undone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isUndone()
    {
        return undone;
    }

    public void UndoButton()
    {
        if (!undone)
        {
            Camera.main.GetComponent<SFXPlaying>().SFXstepClear();
        }
        undone = true;
    }

    public void ResetButton()
    {
        undone = false;
    }
}
