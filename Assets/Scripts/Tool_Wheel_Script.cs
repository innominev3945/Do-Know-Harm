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

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = Quaternion.identity;
        rotationalConstant = 15f;
        rotation = 0;
        current = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateLeft()
    {
        rotation += rotationalConstant;
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public void RotateRight()
    {
        rotation -= rotationalConstant;
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    public void SelectNorthTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        current = Instantiate(NorthTool, mousePosition, Quaternion.identity);
    }

    public void SelectSouthTool()
    {
        if (current != null)
        {
            Destroy(current);
        }

        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        current = Instantiate(SouthTool, mousePosition, Quaternion.identity);
    }

}
