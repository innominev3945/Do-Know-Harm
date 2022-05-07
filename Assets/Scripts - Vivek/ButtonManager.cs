using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PatientManagerClass;

public class ButtonManager : MonoBehaviour
{
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameObject.Find("PatientManager").GetComponent<PatientManager>().SwitchPatient(this.gameObject.name);
    }
}
