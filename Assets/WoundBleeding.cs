using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundBleeding : MonoBehaviour
{

    private SpriteRenderer sprender;
    public bool isBleeding;

    // Start is called before the first frame update
    void Start()
    {
        sprender = GetComponent<SpriteRenderer>();
        sprender.color = Color.red;
        isBleeding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBleeding) // need opacity transition, check Mathf.Lerp and deltaTime thing again
        {
            //float opacityValue = Mathf.Lerp(1f, 0f, Time.deltaTime * 5);
            float opacityValue = 0;
            sprender.color = new Color(sprender.color.r, sprender.color.g, sprender.color.g, opacityValue);
        }
        else
        {
            sprender.color = Color.red;
        }
    }
}
