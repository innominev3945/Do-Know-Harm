using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingVN : MonoBehaviour
{
    DialogueManager dialoguer;

    [SerializeField] private TextAsset[] txtAssets;
    void Start()
    {
        dialoguer = GetComponent<DialogueManager>();
        dialoguer.StartScene(txtAssets[0]);
    }

    
}
