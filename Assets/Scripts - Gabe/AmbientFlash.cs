using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientFlash : MonoBehaviour
{

    [SerializeField] private Light point;

    // Start is called before the first frame update
    void Start()
    {
        point.intensity = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Flash()
    {
        StartCoroutine(FlashDuration(0.25f));
    }

    IEnumerator FlashDuration (float duration)
    {
        point.intensity = 3;
        yield return new WaitForSeconds(duration);
        point.intensity = 1;
    }

}
