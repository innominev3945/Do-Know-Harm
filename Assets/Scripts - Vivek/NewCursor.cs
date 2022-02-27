using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Users;

// Fake cursor that behaves similarly to the standard cursor; Useful for injury treatments that require the mouse 
// position to be directly modified (since Unity does not allow for the normal cursor's position to be changed)
public class NewCursor : MonoBehaviour
{
    public PlayerInputActions playerControls;
    private InputAction lookx;
    private InputAction looky;
    private InputAction mouseClick;
    private Vector2 delta;
    private bool axisRestricted;
    private Vector2 axisDirection; 

    void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        lookx = playerControls.Player.LookX;
        looky = playerControls.Player.LookY;
        mouseClick = playerControls.Player.Fire;
        lookx.Enable();
        looky.Enable();
        mouseClick.Enable();
        mouseClick.performed += MousePressed; 
        delta = new Vector2(0f, 0f);
        axisRestricted = false; 
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed; 
        looky.Disable();
        lookx.Disable();
        mouseClick.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = new Vector3(lookx.ReadValue<float>(), looky.ReadValue<float>()) * 0.01f;
        delta = lookDirection;
        if (axisRestricted)
        {
            lookDirection = (Vector2.Dot(lookDirection, axisDirection.normalized)) * axisDirection; 
        }
        var targetPos = transform.position + lookDirection;
        gameObject.transform.position = targetPos;

        targetPos.x = Mathf.Clamp(targetPos.x, 0, Screen.width);
        targetPos.y = Mathf.Clamp(targetPos.y, 0, Screen.height);
    }

    private void MousePressed(InputAction.CallbackContext context) 
    {
        //Debug.Log("Test");
    }

    public bool getSelected()
    {
        if (mouseClick.ReadValue<float>() != 0f)
            return true;
        else
            return false;
    }


    public Vector2 getDelta()
    {
        return delta; 
    }

    public void LimitAlongAxis(Vector2 direction)
    {
        axisRestricted = true;
        axisDirection = direction;
    }

    public void FreeAxis()
    {
        axisRestricted = false; 
    }
}
