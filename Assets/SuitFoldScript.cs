using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitFoldScript : MonoBehaviour
{
    private bool isHeld;
    void Start()
    {
        isHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hold(bool i)
    {
        isHeld = i;
    }
}
