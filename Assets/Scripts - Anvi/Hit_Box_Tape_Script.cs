using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Box_Tape_Script : MonoBehaviour
{
    private int woundID;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: change based on which wound 
        woundID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getWoundID()
    {
        return woundID;
    }
}
