using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundClassTest
{
    private float bloodlevel;

    public WoundClassTest(float blood)
    {
        bloodlevel = blood;
    }

    public float GetBlood()
    {
        return bloodlevel;
    }

}
