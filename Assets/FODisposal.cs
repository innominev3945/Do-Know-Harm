using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FODisposal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = new Vector3(150, 700, 0);

        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(temp).x, Camera.main.ScreenToWorldPoint(temp).y, 0);
    }
}
