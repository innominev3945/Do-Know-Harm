using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveDeathTransition : MonoBehaviour
{
    [SerializeField] private GameObject stretcher;
    [SerializeField] private GameObject eyeMask;
    private bool moving;
    private bool patientSaved;
    private float startY;
    private Vector3 velocity = Vector3.zero;
    private Vector3 target;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject deathText;
    [SerializeField] private GameObject backgroundFade;


    // Start is called before the first frame update
    void Start()
    {
        patientSaved = false;
        stretcher.SetActive(false);
        startY = transform.position.y;
        target = new Vector3(0, -15, 0);
        saveText.SetActive(false);
        eyeMask.SetActive(false);
        deathText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Debug.Log(moving);
            //float posTempX = Mathf.Lerp(transform.position.x, zoomPoint.x, Time.deltaTime * 5);
            /*float newY = Mathf.Pow((startY - transform.position.y), 2) / 300;
            
            transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Max(newY, 0.001f), transform.position.z);
            stretcher.transform.position = new Vector3(stretcher.transform.position.x, stretcher.transform.position.y - Mathf.Max(newY, 0.001f), transform.position.z);
            

            */
            Vector3 tempVel = velocity;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 1f);
            stretcher.transform.position = Vector3.SmoothDamp(stretcher.transform.position, target, ref tempVel, 1f);
            if (!patientSaved)
            {
                eyeMask.transform.position = Vector3.SmoothDamp(eyeMask.transform.position, target, ref tempVel, 1f);
            }
            if (transform.position.y < -10f)
            {
                moving = false;
            }
        }
    }

    public void SaveTransition()
    {
        saveText.SetActive(true);
        StartCoroutine(MoveSavedPatient());
        //StartCoroutine(FadeTextToFullAlpha(1f, SaveText.GetComponent<TextMesh>()));
    }

    public void DeathTransition()
    {
        deathText.SetActive(true);
        StartCoroutine(MoveDeadPatient());
    }

    IEnumerator MoveSavedPatient()
    {
        patientSaved = true;
        StartCoroutine(FadeTextToFullAlpha(1f, saveText.GetComponent<TextMeshProUGUI>()));
        StartCoroutine(FadeImageToFullAlpha(1f, backgroundFade.GetComponent<Image>()));
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        moving = true;
    }

    IEnumerator MoveDeadPatient()
    {
        StartCoroutine(FadeTextToFullAlpha(1f, deathText.GetComponent<TextMeshProUGUI>()));
        StartCoroutine(FadeImageToFullAlpha(1f, backgroundFade.GetComponent<Image>()));
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(true);
        eyeMask.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        moving = true;
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeTextToZeroAlpha(1f, i));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeImageToFullAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeImageToZeroAlpha(1f, backgroundFade.GetComponent<Image>()));
    }

    public IEnumerator FadeImageToZeroAlpha(float t, Image i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
