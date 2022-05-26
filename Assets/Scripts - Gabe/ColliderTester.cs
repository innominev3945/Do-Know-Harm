using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos2D = Mouse.current.position.ReadValue();
        pos2D = Camera.main.ScreenToWorldPoint(pos2D);
        pos2D = new Vector3(pos2D.x, pos2D.y, 0);
        this.transform.position = pos2D;
    }
}
