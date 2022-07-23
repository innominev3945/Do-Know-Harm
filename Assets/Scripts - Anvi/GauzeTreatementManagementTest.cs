using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauzeTreatementManagementTest : MonoBehaviour
{
    private GameObject gauzeHitBoxManager1;
    private GameObject gauzeHitBoxManager2;

    // Start is called before the first frame update
    void Start()
    {
        gauzeHitBoxManager1 = Instantiate((UnityEngine.Object)Resources.Load("Gauze Hit Box Manager"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

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

        gauzeHitBoxManager1.GetComponent<Gauze_Hit_Box_Manager_Script>().addAllHitBoxes(hitBoxesX, hitBoxesY, woundIDs);

        gauzeHitBoxManager2 = Instantiate((UnityEngine.Object)Resources.Load("Gauze Hit Box Manager"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        List<float> hitBoxesX2 = new List<float>();
        List<float> hitBoxesY2 = new List<float>();
        List<int> woundIDs2 = new List<int>();

        hitBoxesX2.Add(4.79f);
        hitBoxesY2.Add(2.96f);
        woundIDs2.Add(0);

        hitBoxesX2.Add(6.05f);
        hitBoxesY2.Add(2.96f);
        woundIDs2.Add(0);

        hitBoxesX2.Add(7.24f);
        hitBoxesY2.Add(2.96f);
        woundIDs2.Add(0);

        gauzeHitBoxManager2.GetComponent<Gauze_Hit_Box_Manager_Script>().addAllHitBoxes(hitBoxesX2, hitBoxesY2, woundIDs2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
