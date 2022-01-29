using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodypartClass;
using PatientClass;
using InjuryClass;
using InjuryHelperClass; 

public class PatientFunctionality : MonoBehaviour
{
    Patient hero;
    Vector2 location;
    Injury bleed;
    InjuryHelper helper;
    int interval = 1;
    float nextTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        hero = new Patient();
        location = new Vector2(1f, 1f);
        helper = this.GetComponent<InjuryHelper>();
        bleed = new Injury(1.0f, location, helper);
        hero.GetBodyparts()[0].AddInjury(bleed);
        bleed.Treat();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            foreach (Bodypart bodypart in hero.GetBodyparts())
            {
                bodypart.UpdateHealth();
                bodypart.UpdateInjuries();
            }
            hero.UpdateHealth();
            Debug.Log("Health: " + hero.GetHealth());
            nextTime += interval;
        }
    }
}
