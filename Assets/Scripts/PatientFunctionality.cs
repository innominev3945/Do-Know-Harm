using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodypartClass;
using PatientClass;
using InjuryClass;

public class PatientFunctionality : MonoBehaviour
{
    Patient hero;
    int interval = 1;
    float nextTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        hero = new Patient();
        Vector2 location = new Vector2(1f, 1f);
        Injury bleed = new Injury(1.0f, location);
        hero.GetBodyparts()[0].AddInjury(bleed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            foreach (Bodypart bodypart in hero.GetBodyparts())
            {
                bodypart.UpdateHealth();
            }
            hero.UpdateHealth();
            Debug.Log("Health: " + hero.GetHealth());

            nextTime += interval;
        }
    }
}
