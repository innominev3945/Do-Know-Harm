using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scissors_Script : MonoBehaviour
{
    private Vector3 mousePosition;

    private Vector3 mouseDestination;
    private Vector3 direction;
    private Vector3 currentScissorsPosition;
    private float distanceTraveledInOneCut;

    private bool lockPostion;
    private bool cut;
    private bool cutTravel;
    private float angle;

    private const float step = 5f;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);

        mouseDestination = new Vector3(0, 0, 0);
        direction = new Vector3(0, 0, 0);
        currentScissorsPosition = new Vector3(0, 0, 0);
        distanceTraveledInOneCut = 0f;

        lockPostion = false;
        cut = false;
        cutTravel = false;
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (!lockPostion)
        {
            transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
        }
        else
        {
            // rotate scissors according to position of mouse on screen
            angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (cut)
        {
            cutTravel = true;
        }

        if (cutTravel)
        {
            transform.position += direction.normalized * step * Time.deltaTime;

            distanceTraveledInOneCut = (transform.position - currentScissorsPosition).magnitude;

            //Debug.Log(distanceTraveledInOneCut);

            if (distanceTraveledInOneCut > 1f)
            {
                cutTravel = false;
                distanceTraveledInOneCut = 0f;
            }
        }
    }

    public void LockAndRotate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            lockPostion = true;
        }
        else if (context.canceled)
        {
            lockPostion = false;
        }
    }

    public void Cut(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (lockPostion)
            {
                Debug.Log("Cut");

                mouseDestination = mousePosition;
                direction = mouseDestination - transform.position;

                currentScissorsPosition = transform.position;

                //if (direction.magnitude > 2)
                //{
                cut = true;
                    // transform.position += direction.normalized * step;
                //}
            }
        }
        else if (context.canceled)
        {
            Debug.Log("Cut Cancelled");
            cut = false;
        }
    }
}
