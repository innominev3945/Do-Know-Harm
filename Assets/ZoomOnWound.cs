using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomOnWound : MonoBehaviour
{
    [SerializeField] private Button button;
    private bool zoomedIn;
    public GameObject wound;
    [SerializeField] private int zoom;
    private Transform camTransform;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(ZoomIn);
        zoom = 2;
        camTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void ZoomIn()
    {
        zoomedIn = !zoomedIn;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomedIn)
        {
            float posTempX = Mathf.Lerp(camTransform.position.x, wound.transform.position.x, Time.deltaTime * 5);
            float posTempY = Mathf.Lerp(camTransform.position.y, wound.transform.position.y, Time.deltaTime * 5);
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
