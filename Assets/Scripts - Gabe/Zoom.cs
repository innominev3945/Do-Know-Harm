using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour
{
    private bool zoomedIn;
    [SerializeField] private int zoom;
    private Transform camTransform;
    private Vector2 zoomPoint;
    private int call = 0;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = GetComponent<Transform>();
    }

    public void ZoomIn(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!zoomedIn)
            {
                Vector2 pos2D = Mouse.current.position.ReadValue();
                zoomPoint = Camera.main.ScreenToWorldPoint(pos2D);
            }
            Debug.Log("called " + call);
            call++;
            zoomedIn = !zoomedIn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomedIn)
        {
            float posTempX = Mathf.Lerp(camTransform.position.x, zoomPoint.x, Time.deltaTime * 5);
            float posTempY = Mathf.Lerp(camTransform.position.y, zoomPoint.y, Time.deltaTime * 5);
            camTransform.position = new Vector3(posTempX, posTempY, -10);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, Time.deltaTime * 5);
        }
        else
        {
            float posTempX = Mathf.Lerp(camTransform.position.x, 0, Time.deltaTime * 5);
            float posTempY = Mathf.Lerp(camTransform.position.y, 0, Time.deltaTime * 5);
            camTransform.position = new Vector3(posTempX, posTempY, -10);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 5, Time.deltaTime * 5);
        }
    }
}
