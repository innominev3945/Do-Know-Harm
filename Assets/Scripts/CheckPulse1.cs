using UnityEngine;
using UnityEngine.EventSystems;

public class CheckPulse1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer hand;
    public Sprite pulseHand;
    public Sprite defaultHand;

    void checkPulse()
    {
        hand.sprite = pulseHand;
    }

    void returnToDefault()
    {
        hand.sprite = defaultHand;
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        hand.sprite = pulseHand;
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        hand.sprite = defaultHand;

    }
}
