using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scissors : MonoBehaviour
{
    private Vector3 mousePosition;

    private Vector3 mouseDestination;
    private Vector3 direction;
    private Vector3 currentScissorsPosition;
    private float distanceTraveledInOneCut;

    private bool lockPostion;
    private bool cutting;
    private bool cutTravel;
    private float angle;

    private GameObject cutObject;

    private const float step = 3f;

    [SerializeField] private GameObject aim;

    // Start is called before the first frame update
    void Start()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);

        mouseDestination = Vector3.zero;
        direction = Vector3.zero;
        currentScissorsPosition = Vector3.zero;
        distanceTraveledInOneCut = 0f;

        lockPostion = false;
        cutting = false;
        cutTravel = false;
        angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);

        if (!lockPostion)
        {
            transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
            cutting = false;
        }
        else if (!cutTravel)
        {
            // rotate scissors according to position of mouse on screen
            angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (cutTravel)
        {

            transform.position += direction.normalized * step * Time.deltaTime;
            //Debug.Log(transform.position);

            distanceTraveledInOneCut = (transform.position - currentScissorsPosition).magnitude;

            //Debug.Log(distanceTraveledInOneCut);

            //Debug.Log(distanceTraveledInOneCut > 1f);

            if (distanceTraveledInOneCut > 0.6f)
            {
                cutTravel = false;
                distanceTraveledInOneCut = 0f;
            }

            objectCutting();
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
            if (lockPostion && !cutTravel)
            {
                mouseDestination = mousePosition;
                direction = mouseDestination - transform.position;

                // rotate scissors according to position of mouse on screen
                angle = Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                currentScissorsPosition = transform.position;

                //if (direction.magnitude > 2)
                //{
                cutTravel = true;
                // transform.position += direction.normalized * step;
                //}
                if (!cutting)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
                    if (hit.collider != null)
                    {
                        cutObject = hit.collider.gameObject;
                    }
                    else
                    {
                        cutObject = null;
                    }
                }
            }
        }
    }

    private void objectCutting()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        aim.transform.position = transform.position;
        if (!cutting && hit.collider != null)
        {
            if (hit.collider.gameObject != cutObject)
            {
                cutObject = hit.collider.gameObject;
               // Debug.Log(hit.collider.gameObject.name);
                cutting = true;
                Debug.Log("started cutting " + cutObject.name);
            }
        }
        else if (cutting && hit.collider != null)
        {
            if (hit.collider.gameObject != cutObject)
            {
                Debug.Log("Cut done (opt1)");
                cutObject = hit.collider.gameObject;
                cutting = false;
            }
        }
        else if (cutting)
        {
            Debug.Log("Cut done (opt2)");
            cutObject = null;
            cutting = false;
        }
    }
}
