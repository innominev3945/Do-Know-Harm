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
    [SerializeField] GameObject NorthEastTool;
    [SerializeField] GameObject NorthWestTool;
    [SerializeField] GameObject SouthEastTool;
    [SerializeField] GameObject SouthWestTool;
    private float northEastTriggerTimer;
    private float northWestTriggerTimer;
    private float southEastTriggerTimer;
    private float southWestTriggerTimer;

    private GameObject[] arrayOfTools;
    private int numTools;


    // Start is called before the first frame update
    void Start()
    {
        currentRotation = Quaternion.identity;
        rotationalConstant = 15f;   // TODO: change back to 15f after testing
        rotation = 0;
        current = null;

        numTools = 8;
        // tools are listed in the clockwise direction of the wheel, starting with the north tool
        arrayOfTools = new GameObject[numTools];
        arrayOfTools[0] = NorthTool;
        arrayOfTools[1] = NorthEastTool;
        arrayOfTools[2] = EastTool;
        arrayOfTools[3] = SouthEastTool;
        arrayOfTools[4] = SouthTool;
        arrayOfTools[5] = SouthWestTool;
        arrayOfTools[6] = WestTool;
        arrayOfTools[7] = NorthWestTool;

        northEastTriggerTimer = 0;
        northWestTriggerTimer = 0;
        southEastTriggerTimer = 0;
        southWestTriggerTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (northEastTriggerTimer > 0)
        {
            northEastTriggerTimer -= Time.deltaTime;
        }
        if (northWestTriggerTimer > 0)
        {
            northWestTriggerTimer -= Time.deltaTime;
        }
        if (southEastTriggerTimer > 0)
        {
            southEastTriggerTimer -= Time.deltaTime;
        }
        if (southWestTriggerTimer > 0)
        {
            southWestTriggerTimer -= Time.deltaTime;
        }
    }

    public void RotateLeft(InputAction.CallbackContext context)
    {
        rotation += rotationalConstant;
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rotation);

        if (context.started)
        {
            // shift all tools in the arrayOfTools to the left
            GameObject temp = arrayOfTools[0];
            for (int i = 0; i < numTools - 1; i++)
            {
                arrayOfTools[i] = arrayOfTools[i + 1];
            }
            arrayOfTools[numTools - 1] = temp;
        }
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        rotation -= rotationalConstant;
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rotation);

        if (context.started)
        {
            // shift all tools in the arrayOfTools to the right
            GameObject temp = arrayOfTools[numTools - 1];
            for (int i = numTools - 1; i > 0; i--)
            {
                arrayOfTools[i] = arrayOfTools[i - 1];
            }
            arrayOfTools[0] = temp;
        }
    }

    public void SelectNorthEastTool()
    {
        Debug.Log("Select northeast tool");
        northEastTriggerTimer = 0.5f;
    }

    public void SelectNorthWestTool()
    {
        Debug.Log("Select northwest tool");
        northWestTriggerTimer = 0.5f;
    }

    public void SelectSouthEastTool()
    {
        Debug.Log("Select southeast tool");
        southEastTriggerTimer = 0.5f;
    }

    public void SelectSouthWestTool()
    {
        Debug.Log("Select southwest tool");
        southWestTriggerTimer = 0.5f;
    }

    public void SelectNorthTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (northEastTriggerTimer > 0 && arrayOfTools[1] != null)
        {
            current = Instantiate(arrayOfTools[1], mousePosition, Quaternion.identity);
        }
        else if (northWestTriggerTimer > 0 && arrayOfTools[7] != null)
        {
            current = Instantiate(arrayOfTools[7], mousePosition, Quaternion.identity);
        }
        else if (arrayOfTools[0] != null)
        {
            current = Instantiate(arrayOfTools[0], mousePosition, Quaternion.identity);
        }
    }

    public void SelectSouthTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (southEastTriggerTimer > 0 && arrayOfTools[3] != null)
        {
            current = Instantiate(arrayOfTools[3], mousePosition, Quaternion.identity);
        }
        else if (southWestTriggerTimer > 0 && arrayOfTools[5] != null)
        {
            current = Instantiate(arrayOfTools[5], mousePosition, Quaternion.identity);
        }
        else if (arrayOfTools[4] != null)
        {
            current = Instantiate(arrayOfTools[4], mousePosition, Quaternion.identity);
        }
    }

    public void SelectEastTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (northEastTriggerTimer > 0 && arrayOfTools[1] != null)
        {
            current = Instantiate(arrayOfTools[1], mousePosition, Quaternion.identity);
        }
        else if (southEastTriggerTimer > 0 && arrayOfTools[3] != null)
        {
            current = Instantiate(arrayOfTools[3], mousePosition, Quaternion.identity);
        }
        else if (arrayOfTools[2] != null)
        {
            current = Instantiate(arrayOfTools[2], mousePosition, Quaternion.identity);
        }
    }

    public void SelectWestTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (northWestTriggerTimer > 0 && arrayOfTools[7] != null)
        {
            current = Instantiate(arrayOfTools[7], mousePosition, Quaternion.identity);
        }
        else if (southWestTriggerTimer > 0 && arrayOfTools[5] != null)
        {
            current = Instantiate(arrayOfTools[5], mousePosition, Quaternion.identity);
        }
        else if (arrayOfTools[6] != null)
        {
            current = Instantiate(arrayOfTools[6], mousePosition, Quaternion.identity);
        }
    }

}
