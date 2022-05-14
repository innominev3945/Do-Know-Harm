using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InstantiateTape : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    //InstantiateTape is attached to the Tape Spawner item, used to instantiate tape whenever the player mouses over the spawner

    public GameObject tape; //tape prefab that player will drag and drop

   

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(tape, new Vector3(-7.08f, -3.41f, 0), Quaternion.identity); //creates a tape object for player to drag and drop

    }

    // Update is called once per frame
    void Update()
    {


    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //When the player mouses over the spawner, a new tape prefab is instantiated
        Instantiate(tape, new Vector3(-7.08f, -3.41f, 0), Quaternion.identity);

    }

    public void OnPointerDown(PointerEventData eventData)
    {


    }

    public void OnPointerUp(PointerEventData eventData)
    {



    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {



    }
}
