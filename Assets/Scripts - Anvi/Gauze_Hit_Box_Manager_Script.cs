using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauze_Hit_Box_Manager_Script : MonoBehaviour
{
    
    public List<float> hitBoxesX;
    public List<float> hitBoxesY;

    public List<float> allHitBoxLocationsX()
    {
        return hitBoxesX;
    }
    public List<float> allHitBoxLocationsY()
    {
        return hitBoxesY;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Every time the gauze is used, specify the locations of the hitboxes below (for the current patient)
        // Use the format:
        // hitBoxesX.Add(X COORDINATE);
        // hitBoxesY.Add(Y COORDINATE);
        // The script will take care of the rest :)
        // The lines below are an example

        // TODO: remove the following if adding other hit box locations, the following is just an example
        hitBoxesX.Add(-6.4f);
        hitBoxesY.Add(-3.44f);

        hitBoxesX.Add(-5.63f);
        hitBoxesY.Add(-2.57f);

        hitBoxesX.Add(-4.86f);
        hitBoxesY.Add(-1.66f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
