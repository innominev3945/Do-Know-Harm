using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ChangeHandSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private SpriteRenderer rend;
    private Sprite DefaultHand, PulseHand;

    private void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        DefaultHand = Resources.Load<Sprite>("DefaultHand");
        PulseHand = Resources.Load<Sprite>("PulseHand");
        rend.sprite = DefaultHand;
    }

    private void Update()
    {

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
}
