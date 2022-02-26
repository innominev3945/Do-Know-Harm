using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TourniquetTab : MonoBehaviour
{
    private GameObject tourniquet;
    private Transform tqTran;
    private bool isTightening;

    private int startQuad;
    private int currentQuad;
    private int rotations;
    private int maxRotations;

    void Start()
    {
        tourniquet = this.transform.parent.gameObject;
        tqTran = tourniquet.transform;
        isTightening = false;
        rotations = 0;
        maxRotations = 4;
    }

    /*  
     *  Divide area around tourniquet into quadrants, set so mouse must enter counterclockwise adjacent quadrant to continue rotation, add 1 to count when full turn completed
     *  If mouse hits tourniquet then reset rotations
    */

    void Update()
    {
        if (isTightening)
        {
            Mouse.current.position.ReadValue();
            Vector2 pos2D = Mouse.current.position.ReadValue();
            pos2D = Camera.main.ScreenToWorldPoint(pos2D);
            RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, Mathf.Infinity);
            if (hit.collider != null && hit.collider.gameObject == tourniquet) // checks if mouse hits tourniquet, resets if true
            {
                rotations = 0;
                isTightening = false;
            }
            else
            {
                int mouseLocation = FindPos(pos2D);
                if (mouseLocation != currentQuad)
                {
                    if (InOrderAdjacent(currentQuad, mouseLocation))
                    {
                        currentQuad = mouseLocation;
                        if (mouseLocation == startQuad)
                        {
                            rotations++;
                        }
                    }
                }
            }
            if (rotations >= maxRotations)
            {
                tourniquet.GetComponent<Tourniquet>().isTight = true;
                isTightening = false;
            }
        }
        else
        {
            rotations = 0;
        }
    }

    private int FindPos(Vector2 pos2D)
    {
        if (pos2D.x < tqTran.position.x) // sets starting quadrant
        {
            if (pos2D.y < tqTran.position.y)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (pos2D.y < tqTran.position.y)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }

    private bool InOrderAdjacent(int a, int b) // checks if a and b are in order and adjacent according to (0, 1, 2, 3)
    {
        if (a < 3)
        {
            if (b == a + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (b == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void TabDrag()
    {
        isTightening = true; // starts tightening input check
        Mouse.current.position.ReadValue();
        Vector2 pos2D = Mouse.current.position.ReadValue();
        pos2D = Camera.main.ScreenToWorldPoint(pos2D);
        startQuad = FindPos(pos2D);
        currentQuad = startQuad;
    }
}
