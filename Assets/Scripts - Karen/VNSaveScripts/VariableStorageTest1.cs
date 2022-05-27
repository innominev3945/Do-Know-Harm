using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableStorageTest1 : MonoBehaviour
{
    [SerializeField] VNVariableStorage KarenWantsXiao;

    float moneySpent = 200;

    // Start is called before the first frame update
    //Or elsewear
    void Start()
    {
        KarenWantsXiao.setNumberValue("$KarenWantsC6Xiao", moneySpent);
    }


}
