using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingVN : MonoBehaviour
{
    DialogueManager dialoguer;
    private int saveIndex = 0;
    private int saveChapter = 0;

    [SerializeField] private TextAsset[] txtAssets;
    void Start()
    {
        dialoguer = GetComponent<DialogueManager>();

        //Starts the VN Scene corresponding to the text asset at [0]
        dialoguer.StartScene(txtAssets[0]);

        // gets the index in chapter where the player is currently at
        saveIndex = dialoguer.getSaveLocation(); 
        //when save, save the text asset number(chapter number)

        //load a save VN Scene by doing the following:
        //dialoguer.loadVNScene(savedIndex, txtAssets[saveChapter]);


    }

    
}
