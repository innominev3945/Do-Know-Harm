using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class NewCursor : MonoBehaviour
{
    private Controls input;

    void Awake()
    {
        input = new PlayerInputActions();
        input.PlayerControls.MoveX.performed += ctx => moveAxis.x = ctx.ReadValue<float>();
        input.PlayerControls.MoveY.performed += ctx => moveAxis.y = ctx.ReadValue<float>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var moveX = input.Default.MouseX.ReadValue();
        var moveY = input.Default.MouseY.ReadValue();
        var mouseDelta = new Vector3(moveX, moveY);
        Debug.Log("Test");
        var targetPos = transform.position + mouseDelta;
        gameObject.transform.position = targetPos; 
    }
}
