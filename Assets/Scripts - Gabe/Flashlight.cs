using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos2D = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 pos3D = new Vector3(pos2D.x, pos2D.y, 0);
        transform.position = pos3D;
    }
}
