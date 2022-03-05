using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MouseScriptClass
{
    public class Mouse_Script : MonoBehaviour
    {
        private bool clicking;
        private bool performed;

        public bool GetClicking() { return clicking; }
        // Start is called before the first frame update
        void Start()
        {
            clicking = false;
            performed = false;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1);
        }

        public void Click(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                clicking = true;
            }
            else if (context.performed)
            {
                performed = !performed;
                if (!performed)
                    clicking = false;
            }
        }
    }
}