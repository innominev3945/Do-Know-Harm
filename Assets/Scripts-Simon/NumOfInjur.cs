using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumOfInjur : MonoBehaviour
{
    public Text injuredText;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        injuredText.text = "# of injuries: " + player.numOfInjur;
    }

    // Update is called once per frame
    void Update()
    {
        injuredText.text = "# of injuries: " + player.numOfInjur;
    }
}
