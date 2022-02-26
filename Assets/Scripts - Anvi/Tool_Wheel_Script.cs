using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tool_Wheel_Script : MonoBehaviour
{
    [SerializeField] RectTransform wheel;
    private Quaternion currentRotation;
    private float rotation;
    private float rotationalConstant;
    private Vector3 mousePosition;
    private GameObject current;
    [SerializeField] GameObject NorthTool;
    [SerializeField] GameObject SouthTool;
    [SerializeField] GameObject WestTool;
    [SerializeField] GameObject EastTool;

    private GameObject[] arrayOfTools;
    private int numTools;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = Quaternion.identity;
        rotationalConstant = 30f;   // TODO: change back to 15f after testing
        rotation = 0;
        current = null;

        numTools = 4;
        // tools are listed in the clockwise direction of the wheel, starting with the north tool
        arrayOfTools = new GameObject[numTools];
        arrayOfTools[0] = NorthTool;
        arrayOfTools[1] = EastTool;
        arrayOfTools[2] = SouthTool;
        arrayOfTools[3] = WestTool;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateLeft()
    {
        rotation += rotationalConstant;
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rotation);

        // shift all tools in the arrayOfTools to the left
        GameObject temp = arrayOfTools[numTools - 1];
        for (int i = numTools - 1; i > 0; i--)
        {
            arrayOfTools[i] = arrayOfTools[i - 1];
        }
        arrayOfTools[0] = temp;
    }

    public void RotateRight()
    {
        rotation -= rotationalConstant;
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rotation);

        // shift all tools in the arrayOfTools to the right
        GameObject temp = arrayOfTools[0];
        for (int i = 0; i < numTools - 1; i++)
        {
            arrayOfTools[i] = arrayOfTools[i + 1];
        }
        arrayOfTools[numTools - 1] = temp;
    }

    public void SelectNorthTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        current = Instantiate(arrayOfTools[0], mousePosition, Quaternion.identity);
    }

    public void SelectSouthTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        current = Instantiate(arrayOfTools[2], mousePosition, Quaternion.identity);
    }

    public void SelectEastTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        current = Instantiate(arrayOfTools[1], mousePosition, Quaternion.identity);
    }

    public void SelectWestTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        current = Instantiate(arrayOfTools[3], mousePosition, Quaternion.identity);
    }

}
