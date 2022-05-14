using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapControllerSplint : MonoBehaviour
{

    //when the user drops the sprite, the sprite's position will be compared with the position of each snap point
    //if the sprite's distance is close enough to a snap point, we will update the sprite's position to be where that nearby snap point is,
    //thereby snapping the object into place

    public List<Transform> snapPoints;
    public List<DraggableSplint> draggableObjects;
    public float snapRange = 0.5f; //how close the sprite has to be to the snap object for it to snap to that position


    // Start is called before the first frame update
    void Start()
    {
        foreach (DraggableSplint draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }


    //in OnDragEnded, we want to find the snap point that is closest to the object being dragged.
    //if the distance between that nearby snap point is smaller than the snap range,
    //the position of the draggable object will be updated to "snap" to that snap point
    private void OnDragEnded(DraggableSplint draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        //iterates over each snap point
        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition); //calculates the distance between the draggable object and current snap point
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition; //change draggable object position to snap point's position

            //Debug.Log("Snap point angle: " + closestSnapPoint.eulerAngles.z);
            //Debug.Log("Draggable angle: " + draggable.eulerAngles.z);

            draggable.transform.Rotate(0, 0, closestSnapPoint.transform.localRotation.eulerAngles.z);
            //draggable.transform.eulerAngles.z = closestSnapPoint.eulerAngles.z;

        }

    }
}


