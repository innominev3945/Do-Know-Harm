using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleWound : MonoBehaviour
{
    public WoundClassTest wound;


    private void Start()
    {
        wound = new WoundClassTest(10f);
        Debug.Log(wound.GetBlood());
    }


}
