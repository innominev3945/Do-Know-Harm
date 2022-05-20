using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PatientManagerClass;

public class SaveDeathTransition : MonoBehaviour
{
    [SerializeField] private GameObject stretcher;
    [SerializeField] private GameObject eyeMask;
    private bool moving;
    private bool patientSaved;
    private float startY;
    private Vector3 velocity = Vector3.zero;
    private Vector3 target;
    private Vector3 stretcherStartPos;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject deathText;
    [SerializeField] private GameObject backgroundFade;
    [SerializeField] private GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        patientSaved = false;
        stretcherStartPos = stretcher.transform.position;
        stretcher.SetActive(false);
        startY = transform.position.y;
        saveText.SetActive(false);
        eyeMask.SetActive(false);
        deathText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (moving)
        {
            Debug.Log(moving);
            //float posTempX = Mathf.Lerp(transform.position.x, zoomPoint.x, Time.deltaTime * 5);
            *//*float newY = Mathf.Pow((startY - transform.position.y), 2) / 300;
            
            transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Max(newY, 0.001f), transform.position.z);
            stretcher.transform.position = new Vector3(stretcher.transform.position.x, stretcher.transform.position.y - Mathf.Max(newY, 0.001f), transform.position.z);
            

            *//*
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
        }*/
    }

    public void PatientSwitchTransition(float time)
    {
        StartCoroutine(FadeImageToFullAlpha(time, fadePanel.GetComponent<Image>()));
    }

    public void SaveTransition()
    {
        saveText.SetActive(true);
        //StartCoroutine(MoveSavedPatient());
        //StartCoroutine(FadeTextToFullAlpha(1f, SaveText.GetComponent<TextMesh>()));
    }

    public void DeathTransition()
    {
        deathText.SetActive(true);
        StartCoroutine(MoveDeadPatient());
    }


    public void currentPatientDeath(GameObject patient) //param - patient gameobject
    {
        //saveText.setActive(true);
        StartCoroutine(MoveSavedPatient(patient));
    }

    IEnumerator MoveSavedPatient(GameObject patient)
    {
        Vector3 patientStartPos = patient.transform.position;
        Vector3 stretcherTarget = new Vector3(stretcher.transform.position.x, stretcher.transform.position.y - 50, stretcher.transform.position.z);
        target = new Vector3(patientStartPos.x, patientStartPos.y - 50, patientStartPos.z);
        Debug.Log(target);
        patientSaved = true;
        //StartCoroutine(FadeTextToFullAlpha(1f, saveText.GetComponent<TextMeshProUGUI>()));
        //StartCoroutine(FadeImageToFullAlpha(1f, backgroundFade.GetComponent<Image>()));
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        moving = true;
        while (moving)
        {
            //Debug.Log(moving);
            //float posTempX = Mathf.Lerp(transform.position.x, zoomPoint.x, Time.deltaTime * 5);
            /*float newY = Mathf.Pow((startY - transform.position.y), 2) / 300;
            
            transform.position = new Vector3(transform.position.x, transform.position.y - Mathf.Max(newY, 0.001f), transform.position.z);
            stretcher.transform.position = new Vector3(stretcher.transform.position.x, stretcher.transform.position.y - Mathf.Max(newY, 0.001f), transform.position.z);
            

            */
            Vector3 tempVel = velocity;
            patient.transform.position = Vector3.SmoothDamp(patient.transform.position, target, ref velocity, 1f);
            stretcher.transform.position = Vector3.SmoothDamp(stretcher.transform.position, stretcherTarget, ref tempVel, 1f);
            if (!patientSaved)
            {
                eyeMask.transform.position = Vector3.SmoothDamp(eyeMask.transform.position, target, ref tempVel, 1f);
            }
            if (patient.transform.position.y < patientStartPos.y - 30f)
            {
                moving = false;
            }
            yield return null;
        }
        gameObject.GetComponent<PatientManager>().PatientSaveTransitionHelper2();
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(false);
        stretcher.transform.position = stretcherStartPos;
        patient.transform.position = patientStartPos;
        
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
        while (moving)
        {
            //Debug.Log(moving);
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
            yield return null;
        }
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
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeImageToZeroAlpha(t, i));
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

    public void switchPatient()
    {
        StartCoroutine(FadeImageToFullAlpha(1f, fadePanel.GetComponent<Image>()));

    }
}
