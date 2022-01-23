using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMvt : MonoBehaviour
{
    private float StartPosX;
    private float StartPosY;
    private bool selected;

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            StartPosX = mousePosition.x - this.transform.localPosition.x;
            StartPosY = mousePosition.y - this.transform.localPosition.y;
            selected = true;
        }
    }

    public void OnMouseUp()
    {
        selected = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector2(mousePosition.x - StartPosX, mousePosition.y - StartPosY);
        }
    }
}
