using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatientManagerClass;

public class EndOfGPScene : MonoBehaviour
{
    public bool called;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!called && this.GetComponent<PatientManager>().GetLevelComplete())
        {
            called = true;
            Debug.Log("Ending Scene in 5 seconds");
            StartCoroutine(EndOfScene());

        }
    }

    IEnumerator EndOfScene()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<ToScene>().LoadSelectedScene();
    }
}
