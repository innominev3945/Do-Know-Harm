using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauzeTreatementManagementTest : MonoBehaviour
{
    private GameObject gauzeHitBoxManager;

    // Start is called before the first frame update
    void Start()
    {
        gauzeHitBoxManager = Instantiate((UnityEngine.Object)Resources.Load("Gauze Hit Box Manager"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        List<float> hitBoxesX = new List<float>();
        List<float> hitBoxesY = new List<float>();
        List<int> woundIDs = new List<int>();

        hitBoxesX.Add(-6.4f);
        hitBoxesY.Add(-3.44f);
        woundIDs.Add(0);

        hitBoxesX.Add(-5.63f);
        hitBoxesY.Add(-2.57f);
        woundIDs.Add(0);

        hitBoxesX.Add(-4.86f);
        hitBoxesY.Add(-1.66f);
        woundIDs.Add(0);

        hitBoxesX.Add(4.79f);
        hitBoxesY.Add(2.96f);
        woundIDs.Add(1);

        hitBoxesX.Add(6.05f);
        hitBoxesY.Add(2.96f);
        woundIDs.Add(1);

        hitBoxesX.Add(7.24f);
        hitBoxesY.Add(2.96f);
        woundIDs.Add(1);

        gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().addAllHitBoxes(hitBoxesX, hitBoxesY, woundIDs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
