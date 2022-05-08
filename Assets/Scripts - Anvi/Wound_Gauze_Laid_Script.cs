using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wound_Gauze_Laid_Script : MonoBehaviour
{
    private bool allGauzeLaid;

    // Start is called before the first frame update
    void Start()
    {
        allGauzeLaid = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gauzeLaid()
    {
        allGauzeLaid = true;
    }

    public bool hasGauzeBeenLaid()
    {
        return allGauzeLaid;
    }
}
