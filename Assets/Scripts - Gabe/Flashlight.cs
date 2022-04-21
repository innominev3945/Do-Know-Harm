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
        float xVal = Mathf.Lerp(transform.position.x, pos2D.x, Time.deltaTime * 5);
        float yVal = Mathf.Lerp(transform.position.y, pos2D.y, Time.deltaTime * 5);
        float zVal = this.transform.position.z;
        
        Vector3 pos3D = new Vector3(xVal, yVal, zVal);
        transform.position = pos3D;
    }
}
