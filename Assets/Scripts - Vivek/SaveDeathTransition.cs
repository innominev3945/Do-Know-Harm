using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PatientManagerClass;

public class SaveDeathTransition : MonoBehaviour
{
    [SerializeField] private GameObject stretcher;
    //[SerializeField] private GameObject eyeMask;
    private bool moving;
    private bool patientSaved;
    private float startY;
    private Vector3 velocity = Vector3.zero;
    private Vector3 target;
    private Vector3 stretcherStartPos;
    [SerializeField] private GameObject saveBanner;
    [SerializeField] private GameObject deathBanner;
    [SerializeField] private GameObject fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        patientSaved = false;
        stretcherStartPos = stretcher.transform.position;
        stretcher.SetActive(false);
        startY = transform.position.y;
        saveBanner.SetActive(true);
        saveBanner.transform.GetChild(0).gameObject.SetActive(false);
        saveBanner.transform.GetChild(1).gameObject.SetActive(false);

        deathBanner.SetActive(true);
        deathBanner.transform.GetChild(0).gameObject.SetActive(false);
        deathBanner.transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PatientSwitchTransition(float time)
    {
        StartCoroutine(FadeImageToFullAlpha(time, 0.1f, fadePanel));
    }

    /*public void SaveTransition()
    {
        saveText.SetActive(true);
        //StartCoroutine(MoveSavedPatient());
        //StartCoroutine(FadeTextToFullAlpha(1f, SaveText.GetComponent<TextMesh>()));
    }

    public void DeathTransition()
    {
        deathText.SetActive(true);
        StartCoroutine(MoveDeadPatient());
    }*/


    public void currentPatientDeath(GameObject patient) //param - patient gameobject
    {
        //saveText.setActive(true);
        StartCoroutine(MoveDeadPatient(patient));
    }

    public void currentPatientSaved(GameObject patient)
    {
        StartCoroutine(MoveSavedPatient(patient));
    }

    IEnumerator MoveSavedPatient(GameObject patient)
    {
        Vector3 patientStartPos = patient.transform.position;
        Vector3 stretcherTarget = new Vector3(stretcher.transform.position.x, stretcher.transform.position.y - 50, stretcher.transform.position.z);
        target = new Vector3(patientStartPos.x, patientStartPos.y - 50, patientStartPos.z);
        //Debug.Log(target);
        patientSaved = true;
        StartCoroutine(FadeTextToFullAlpha(1f, 1f, saveBanner.transform.GetChild(1).gameObject));
        StartCoroutine(FadeImageToFullAlpha(1f, 1f, saveBanner.transform.GetChild(0).gameObject));
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
            /*if (!patientSaved)
            {
                eyeMask.transform.position = Vector3.SmoothDamp(eyeMask.transform.position, target, ref tempVel, 1f);
            }*/
            if (patient.transform.position.y < patientStartPos.y - 30f)
            {
                moving = false;
            }
            yield return null;
        }
        gameObject.GetComponent<PatientManager>().PatientSaveDeathTransitionHelper2();
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(false);
        stretcher.transform.position = stretcherStartPos;
        patient.transform.position = patientStartPos;
    }

    IEnumerator MoveDeadPatient(GameObject patient)
    {
        Vector3 patientStartPos = patient.transform.position;
        Vector3 stretcherTarget = new Vector3(stretcher.transform.position.x, stretcher.transform.position.y - 50, stretcher.transform.position.z);
        target = new Vector3(patientStartPos.x, patientStartPos.y - 50, patientStartPos.z);
        patientSaved = true;
        StartCoroutine(FadeTextToFullAlpha(1f, 1f, deathBanner.transform.GetChild(1).gameObject));
        StartCoroutine(FadeImageToFullAlpha(1f, 1f, deathBanner.transform.GetChild(0).gameObject));
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        moving = true;
        while (moving)
        {
            Vector3 tempVel = velocity;
            patient.transform.position = Vector3.SmoothDamp(patient.transform.position, target, ref velocity, 1f);
            stretcher.transform.position = Vector3.SmoothDamp(stretcher.transform.position, stretcherTarget, ref tempVel, 1f);
            //eyeMask.transform.position = Vector3.SmoothDamp(eyeMask.transform.position, target, ref tempVel, 1f);
            if (patient.transform.position.y < patientStartPos.y - 30f)
            {
                moving = false;
            }
            yield return null;
        }
        gameObject.GetComponent<PatientManager>().PatientSaveDeathTransitionHelper2();
        yield return new WaitForSeconds(0.6f);
        stretcher.SetActive(false);
        stretcher.transform.position = stretcherStartPos;
        patient.transform.position = patientStartPos;
    }

    /*IEnumerator MoveDeadPatient()
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
    }*/

    public IEnumerator FadeTextToFullAlpha(float t, float pause, GameObject text)
    {
        text.SetActive(true);
        TextMeshProUGUI i = text.GetComponent<TextMeshProUGUI>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeTextToZeroAlpha(t, text));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, GameObject text)
    {
        TextMeshProUGUI i = text.GetComponent<TextMeshProUGUI>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        text.SetActive(false);
    }

    public IEnumerator FadeImageToFullAlpha(float t, float pause, GameObject im)
    {
        im.SetActive(true);
        Image i = im.GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(pause); // prev 0.1f
        StartCoroutine(FadeImageToZeroAlpha(t, im));
    }

    public IEnumerator FadeImageToZeroAlpha(float t, GameObject im)
    {
        Image i = im.GetComponent<Image>();
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        im.SetActive(false);
    }

    /*public void switchPatient()
    {
        StartCoroutine(FadeImageToFullAlpha(1f, fadePanel.GetComponent<Image>()));
    }*/
}
