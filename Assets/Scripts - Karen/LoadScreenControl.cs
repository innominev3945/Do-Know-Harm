using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenControl : MonoBehaviour
{
    public GameObject KarenSleepy;

    public void activateLoading()
    {
        Debug.Log("Supposed to wakey wakey");
        KarenSleepy.gameObject.SetActive(true);
    }

    public void deactivateLoading()
    {
        KarenSleepy.gameObject.SetActive(false);
    }
}
