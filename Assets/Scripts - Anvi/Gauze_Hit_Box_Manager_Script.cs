using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauze_Hit_Box_Manager_Script : MonoBehaviour
{
    [SerializeField] GameObject hitbox;
    private List<GameObject> allHitBoxesGameObjects;

    public class hitBox
    {
        float xCoord;
        float yCoord;
        int woundID;
        bool gauzeApplied;

        public hitBox(float x, float y, int ID)
        {
            xCoord = x;
            yCoord = y;
            woundID = ID;
            gauzeApplied = false;
        }

        public float getX()
        {
            return xCoord;
        }

        public float getY()
        {
            return yCoord;
        }

        public int getID()
        {
            return woundID;
        }

        public bool isGauzeApplied()
        {
            return gauzeApplied;
        }

        public void setGauzeApplied()
        {
            gauzeApplied = true;
        }
    }

    private List<hitBox> allHitBoxes;

    public List<float> hitBoxesX;
    public List<float> hitBoxesY;
    public List<bool> hitBoxNotCovered;

    public List<float> allHitBoxLocationsX()
    {
        return hitBoxesX;
    }
    public List<float> allHitBoxLocationsY()
    {
        return hitBoxesY;
    }
    public List<bool> allHitBoxNotCovered()
    {
        return hitBoxNotCovered;
    }

    public class wound
    {
        int totalHitBoxes;
        int hitBoxesCovered;

        public wound(int totalHitBoxesRequired)
        {
            totalHitBoxes = totalHitBoxesRequired;
            hitBoxesCovered = 0;
        }

        public void coverOneHitBox()
        {
            hitBoxesCovered++;

            Debug.Log("hitBoxesCovered: " + hitBoxesCovered);
        }

        public bool isAllHitBoxesCovered()
        {
            Debug.Log("hitBoxesCovered: " + hitBoxesCovered + ", totalHitBoxes: " + totalHitBoxes);

            if (totalHitBoxes == hitBoxesCovered)
            {
                return true;
            }
            return false;
        }
    }

    private List<wound> allWounds;

    public List<wound> getAllWounds()
    {
        return allWounds;
    }

    public void notifyHitBoxCovered(float xCoord, float yCoord)
    {
        Debug.Log("Called notifyHitBoxCovered");

        int size = allHitBoxes.Count;
        for (int i = 0; i < size; i++)
        {
            if ((allHitBoxes[i].getX() - xCoord <= 0.01) && (allHitBoxes[i].getX() - xCoord >= -0.01))
            {
                if ((allHitBoxes[i].getY() - yCoord <= 0.01) && (allHitBoxes[i].getY() - yCoord >= -0.01))
                {
                    Debug.Log("Hit box found for given coordinates");

                    if (!allHitBoxes[i].isGauzeApplied())
                    {
                        allHitBoxes[i].setGauzeApplied();
                        allHitBoxesGameObjects[i].GetComponent<SpriteRenderer>().enabled = false;
                        // hitBoxNotCovered[i] = false;
                        int ID = allHitBoxes[i].getID();

                        Debug.Log("Wound ID associated with hit box found: " + ID);

                        allWounds[ID].coverOneHitBox();
                        if (allWounds[ID].isAllHitBoxesCovered())
                        {
                            // TODO: tell wound that all gauze squares have been laid down on it

                            Debug.Log("All gauze squares laid down on wound " + ID);
                        }

                        break;
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize struct lists
        /*
        allHitBoxes = new List<hitBox>();
        allWounds = new List<wound>();
        */

        /*
        // TODO: Every time the gauze is used, specify the locations of the hitboxes below (for the current patient)
        // Use the format:
        // hitBoxesX.Add(X COORDINATE);
        // hitBoxesY.Add(Y COORDINATE);
        // allHitBoxes.Add(new hitBox(X COORDINATE, Y COORDINATE, WOUND ID));
        // *** Note *** : WOUND ID is a number to specify which wound the specific hit box is associated with.
        //                  The must be the same as its index number in the array
        // allWounds.Add(new wound(NUMBER OF HITBOXES ASSOCIATED WITH A WOUND ID));
        // The script will then take care of the rest :)
        // The lines below are an example

        // TODO: remove the following if adding other hit box locations, the following is just an example
        hitBoxesX.Add(-6.4f);
        hitBoxesY.Add(-3.44f);
        allHitBoxes.Add(new hitBox(-6.4f, -3.44f, 0));

        hitBoxesX.Add(-5.63f);
        hitBoxesY.Add(-2.57f);
        allHitBoxes.Add(new hitBox(-5.63f, -2.57f, 0));

        hitBoxesX.Add(-4.86f);
        hitBoxesY.Add(-1.66f);
        allHitBoxes.Add(new hitBox(-4.86f, -1.66f, 0));

        allWounds.Add(new wound(3));

        hitBoxesX.Add(4.79f);
        hitBoxesY.Add(2.96f);
        allHitBoxes.Add(new hitBox(4.79f, 2.96f, 1));

        hitBoxesX.Add(6.05f);
        hitBoxesY.Add(2.96f);
        allHitBoxes.Add(new hitBox(6.05f, 2.96f, 1));

        hitBoxesX.Add(7.24f);
        hitBoxesY.Add(2.96f);
        allHitBoxes.Add(new hitBox(7.24f, 2.96f, 1));

        allWounds.Add(new wound(3));
        */
    }

    public void addHitBox(float xCoord, float yCoord)
    {
        hitBoxesX.Add(xCoord);
        hitBoxesY.Add(yCoord);
        allHitBoxes.Add(new hitBox(xCoord, yCoord, allWounds.Count));

        allWounds.Add(new wound(1));
    }

    public void addAllHitBoxes(List<float> hitBoxesXtemp, List<float> hitBoxesYtemp, List<int> woundIDs)
    {
        if (hitBoxesXtemp.Count == 0 && hitBoxesYtemp.Count == 0 && woundIDs.Count == 0)
        {
            return;
        }

        allHitBoxes = new List<hitBox>();
        allHitBoxesGameObjects = new List<GameObject>();
        allWounds = new List<wound>();
        hitBoxesX = new List<float>();
        hitBoxesY = new List<float>();
        // hitBoxNotCovered = new List<bool>();

        int prevWoundID = woundIDs[0];
        int boxesPerWound = 0;

        for (int i = 0; i < woundIDs.Count; i++)
        {
            if (prevWoundID != woundIDs[i])
            {
                allWounds.Add(new wound(boxesPerWound));
                boxesPerWound = 0;
                prevWoundID = woundIDs[i];
            }

            hitBoxesX.Add(hitBoxesXtemp[i]);
            hitBoxesY.Add(hitBoxesYtemp[i]);
            // hitBoxNotCovered.Add(true);
            allHitBoxes.Add(new hitBox(hitBoxesXtemp[i], hitBoxesYtemp[i], woundIDs[i]));

            allHitBoxesGameObjects.Add(Instantiate(hitbox, new Vector3(hitBoxesXtemp[i], hitBoxesYtemp[i], 0), Quaternion.identity));

            boxesPerWound++;

            Debug.Log("X: " + hitBoxesX[i] + "\nY: " + hitBoxesY[i] + "\nID: " + woundIDs[i]);
        }
        allWounds.Add(new wound(boxesPerWound));

        /*
        List<float> positionsX = allHitBoxLocationsX();
        List<float> positionsY = allHitBoxLocationsY();
        // List<bool> hitBoxNotCovered = gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().allHitBoxNotCovered();
        for (int i = 0; i < positionsX.Count; i++)
        {
            // if (hitBoxNotCovered[i])
            allHitBoxesGameObjects.Add(Instantiate(hitbox, new Vector3(positionsX[i], positionsY[i], 0), Quaternion.identity));
        }
        */
    }

    /*
    public void cleanUp()
    {
        allHitBoxes = null;
        allWounds = null;
        hitBoxesX = null;
        hitBoxesY = null;
    }
    */

    // Update is called once per frame
    void Update()
    {
        
    }
}
