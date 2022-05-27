using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PatientManagerClass;

public class MaleClothingScript : MonoBehaviour
{
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject fold;

    /*private bool button1undone = false;
    private bool button2undone = false;*/
    private bool buttonsOpen;
    private bool open = false;
    [SerializeField] private bool foldHeld;

    void Start()
    {
        fold.SetActive(false);
        button1.SetActive(true);
        button2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (button1.GetComponent<SuitButtonScript>().isUndone() && button2.GetComponent<SuitButtonScript>().isUndone())
        {
            if (!buttonsOpen)
            {
                button1.SetActive(false);
                button2.SetActive(false);
                buttonsOpen = true;
                fold.SetActive(true);
                //Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
            }
        }
        if (buttonsOpen)
        {
            if (foldHeld)
            {
                Vector2 mousePos = Mouse.current.position.ReadValue();
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 foldpos = fold.transform.position;
                if (mousePos.x > foldpos.x + 5)
                {
                    open = true;
                    this.gameObject.SetActive(false);
                    Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                    this.transform.parent.GetComponent<PatientManager>().SetClothingOpen();
                }
            }
        }
    }

    public void enableClothing()
    {
        if (!open)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void disableClothing()
    {
        this.gameObject.SetActive(false);
    }

    public void setFoldHeld(bool i)
    {
        if (buttonsOpen)
        {
            foldHeld = i;
        }
    }

    public bool getState()
    {
        return open;
    }

    public void setState(bool state)
    {
        Debug.Log(state);
        foldHeld = false;
        buttonsOpen = false;
        open = state;
        if (state)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            button1.GetComponent<SuitButtonScript>().ResetButton();
            button2.GetComponent<SuitButtonScript>().ResetButton();
            this.gameObject.SetActive(true);
            button1.SetActive(true);
            button2.SetActive(true);
            fold.SetActive(false);
        }
    }
}
