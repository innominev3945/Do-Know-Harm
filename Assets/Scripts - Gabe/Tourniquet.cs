using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tourniquet : MonoBehaviour
{
    private bool holdingMouse;
    private GameObject arm;
    public List<GameObject> wounds;
    private bool isHeld;
    public bool isSet;
    public bool isTight;
    [SerializeField] private GameObject tab;
    [SerializeField] private LayerMask layerMask;


    //note: https://forum.unity.com/threads/how-would-you-handle-a-getbuttondown-situaiton-with-the-new-input-system.627184/
    //may want to look

    void Start()
    {
        holdingMouse = false;
        isHeld = false;
    }

    void Update()
    {
        if (!isSet)
        {
            isTight = false;
        }
        if (isHeld) // checks if tourniquet is being dragged
        {
            Vector2 pos2D = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = pos2D;
        }
        if (isSet && isTight) // checks if tourniquet is on limb and tightened
        {
            foreach (GameObject wound in wounds)
            {
                if (wound.transform.position.y < this.transform.position.y)
                {
                    wound.GetComponent<Wound>().isBleeding = false;
                }
                else
                {
                    wound.GetComponent<Wound>().isBleeding = true;
                }
            }
        }
        else
        {
            foreach (GameObject wound in wounds)
            {
                wound.GetComponent<Wound>().isBleeding = true;
            }
        }
    }

    public void TourniquetDrag(InputAction.CallbackContext context) // formerly MouseDrag
    {
        Vector2 pos2D = Mouse.current.position.ReadValue(); // is there a way to rid this of mouse dependency? last line left that uses it
        pos2D = Camera.main.ScreenToWorldPoint(pos2D);
        RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject == tab && isSet) // checks if mouse hit tab and tourniquet is on limb
        {
            tab.GetComponent<TourniquetTab>().TabDrag();
        }
        else if (hit.collider != null && hit.collider.gameObject == this.gameObject) // check if tourniquet was clicked on
        {
            isSet = false; // holding tourniquet means it isn't applied

            if (context.performed)
            {
                isHeld = true;
            }
            else
            {
                isHeld = false;
            }
            if (context.canceled) //formerly !isHeld
            {
                //int layerMask = ~(LayerMask.GetMask("Draggable Objects"));
                RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, Vector2.zero, Mathf.Infinity, layerMask); // detect if/which limb tourniquet is placed on
                if (hit2.collider != null && hit2.collider.gameObject.tag == "Limb")
                {
                    arm = hit2.collider.gameObject;
                    wounds.Clear();
                    foreach (Transform child in arm.transform) // fill up wounds list with wounds on selected limb
                    {
                        if (child.tag == "Wound")
                        {
                            wounds.Add(child.gameObject);
                        }
                    }

                    transform.position = new Vector2(arm.transform.position.x, this.transform.position.y);
                    transform.rotation = arm.transform.rotation;

                    float heightDiff = transform.position.y - arm.transform.position.y;
                    Vector3 eulerAngles = arm.transform.rotation.eulerAngles;
                    float xDiff = Mathf.Tan(Mathf.Deg2Rad * eulerAngles.z) * heightDiff;
                    transform.position = new Vector2(arm.transform.position.x - xDiff, transform.position.y);
                    isSet = true;
                }
                else
                {
                    wounds.Clear();
                    isSet = false;
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }
        }
        if (!context.performed) { // bug: moving mouse fast when clicking can keep isHeld on even after released
            isHeld = false;
        }
    }
}
